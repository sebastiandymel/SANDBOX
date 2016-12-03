using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SEDY.PhoneCore.DSP
{
    public static class HttpExtensions
    {
        public static Task<HttpWebResponse> GetResponseAsync(this HttpWebRequest request)
        {
            var taskComplete = new TaskCompletionSource<HttpWebResponse>();
            request.BeginGetResponse(asyncResponse =>
                                     {
                                         try
                                         {
                                             HttpWebRequest responseRequest = (HttpWebRequest)asyncResponse.AsyncState;
                                             HttpWebResponse someResponse =
                                                 (HttpWebResponse)responseRequest.EndGetResponse(asyncResponse);
                                             taskComplete.TrySetResult(someResponse);
                                         }
                                         catch (WebException webExc)
                                         {
                                             HttpWebResponse failedResponse = (HttpWebResponse)webExc.Response;
                                             taskComplete.TrySetResult(failedResponse);
                                         }
                                     }, request);
            return taskComplete.Task;
        }

        public static Task<Stream> GetStreamAsync(this WebClient request, Uri adress)
        {
            var tcs = new TaskCompletionSource<Stream>();
            request.OpenReadCompleted += (s, e) =>
                                         {
                                             if (e.Error == null)
                                             {
                                                 tcs.SetResult(e.Result);
                                             }
                                             else
                                             {
                                                 tcs.SetException(e.Error);
                                             }
                                         };
            request.OpenReadAsync(adress);
            return tcs.Task;
        }

        public static Task<string> GetStringAsync(this WebClient client, Uri adress)
        {
            var tcs = new TaskCompletionSource<string>();
            client.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error == null)
                {
                    tcs.SetResult(e.Result);
                }
                else
                {
                    tcs.SetException(e.Error);
                }
            };
            client.DownloadStringAsync(adress);
            return tcs.Task;
        }
    }
}