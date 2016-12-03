using System;

namespace UIToolkit
{
    interface IBackgroundExecutor
    {
        /// <summary>
        /// Gets or sets number of worker threads. 
        /// By default 1 thread will execute work.
        /// </summary>
        int NumberOfWorkerThreads { get; set; }

        /// <summary>
        /// Posts an action to the queue and starts execution task if neccessary.
        /// If suspender is created, action will only be queued and execution starts
        /// when suspender is disposed.
        /// </summary>
        /// <param name="actionToExecute">Action to execute</param>
        void Post(Action actionToExecute);

        /// <summary>
        /// When suspender is created, any action posted is only queued. 
        /// Execution will be started when suspender is disposed.
        /// </summary>
        /// <returns>Returns new instance of the suspender as IDisposable</returns>
        IDisposable CreateSuspender();

        /// <summary>
        /// Waits until queue is empty and all work is executed
        /// Thread will be unblocked each time queue is empty
        /// </summary>
        void Wait();

        event EventHandler QueuedActionExecuted;
        event EventHandler QueuedActionExecuting;
        event EventHandler<ActionFailedEventArgs> QueuedActionFailed;
    }
}