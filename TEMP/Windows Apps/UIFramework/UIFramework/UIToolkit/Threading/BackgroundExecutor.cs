using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UIToolkit
{
    public class BackgroundExecutor : IBackgroundExecutor
    {
        private readonly ManualResetEvent mre = new ManualResetEvent(true);
        private readonly object syncObj = new Object();
        private readonly ConcurrentQueue<Action> queue;
        private readonly List<bool> workersRunning = new List<bool>();

        private int supressCount;
        private int SupressCount
        {
            get
            {
                return supressCount;
            }
            set
            {
                supressCount = value;
                if (supressCount == 0)
                {
                    StartWorkerIfNeeded();
                }
            }
        }

        private int numberOfWorkerThreads;
        /// <summary>
        /// Gets or sets maximum number of worker threads that will be executing queued work simultanously.
        /// </summary>
        public int NumberOfWorkerThreads
        {
            get
            {
                return numberOfWorkerThreads;
            }
            set
            {
                lock (syncObj)
                {
                    numberOfWorkerThreads = value;
                }
                if (workersRunning.Count < value)
                {
                    var diff = value - workersRunning.Count;
                    for (int i = 0; i < diff; i++)
                    {
                        workersRunning.Add(false);
                    }
                }
                else if (workersRunning.Count > value)
                {
                    var count = workersRunning.Count;
                    for (int i = value; i < count; i++)
                    {
                        workersRunning.RemoveAt(workersRunning.Count - 1);
                    }
                }
            }
        }

        /// <summary>
        /// Event raised when dequeued action is about to be executed
        /// </summary>
        public event EventHandler QueuedActionExecuting;

        /// <summary>
        /// Event raised when dequed action is successfully executed
        /// </summary>
        public event EventHandler QueuedActionExecuted;

        /// <summary>
        /// Event raised if during execution of queued work an error occurs
        /// </summary>
        public event EventHandler<ActionFailedEventArgs> QueuedActionFailed;

        public BackgroundExecutor()
        {
            queue = new ConcurrentQueue<Action>();
            NumberOfWorkerThreads = 1;
        }

        /// <summary>
        /// Puts an action to the execution queue
        /// </summary>
        /// <param name="actionToExecute"></param>
        public void Post(Action actionToExecute)
        {
            lock (syncObj)
            {
                queue.Enqueue(actionToExecute);
            }
            if (SupressCount == 0)
            {
                StartWorkerIfNeeded();
            }
        }

        public IDisposable CreateSuspender()
        {
            return new Suspender(this);
        }

        public void Wait()
        {
            mre.WaitOne();
        }

        private void StartWorkerIfNeeded()
        {
            lock (syncObj)
            {
                for (int i = 0; i < NumberOfWorkerThreads; i++)
                {
                    var workerRunning = workersRunning[i];
                    if (!workerRunning && queue.Any())
                    {
                        mre.Reset();
                        workersRunning[i] = true;
                        var workerIndex = i;
                        Task.Factory.StartNew(() => ExecuteQueuedWork(workerIndex)).ContinueWith(c => StartWorkerIfNeeded());
                    }
                    else if (!queue.Any())
                    {
                        mre.Set();
                    }
                }
            }
        }

        private void ExecuteQueuedWork(int workerIndex)
        {
            Action actionToExecute;
            bool successfullyDequeued;

            lock (syncObj)
            {
                successfullyDequeued = queue.TryDequeue(out actionToExecute);
            }

            if (successfullyDequeued && actionToExecute == null)
            {
                throw new InvalidOperationException("Invalid action queued!");
            }

            if (successfullyDequeued)
            {
                try
                {
                    RaiseQueuedActionExecuting();
                    actionToExecute();
                    RaiseQueuedActionExecuted();
                }
                catch (Exception ex)
                {
                    RaiseQueuedActionFailed(ex);
                }
            }

            workersRunning[workerIndex] = false;
        }

        private void RaiseQueuedActionExecuting()
        {
            var handler = QueuedActionExecuting;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void RaiseQueuedActionExecuted()
        {
            var handler = QueuedActionExecuted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void RaiseQueuedActionFailed(Exception ex)
        {
            var handler = QueuedActionFailed;
            if (handler != null)
            {
                handler(this, new ActionFailedEventArgs(ex));
            }
        }

        private class Suspender : IDisposable
        {
            private BackgroundExecutor be;

            internal Suspender(BackgroundExecutor be)
            {
                this.be = be;
                be.SupressCount++;
            }

            public void Dispose()
            {
                be.SupressCount--;
            }
        }
    }
}