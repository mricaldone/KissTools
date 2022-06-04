using System.IO;
using System.Net;
using System.Text.Json;

namespace KissTools
{
    public static class Networking
    {
        public static NetworkResponse HttpRequest(string url, NetworkMethod method = NetworkMethod.GET)
        {
            HttpWebRequest request = (HttpWebRequest) HttpWebRequest.Create(url);
            request.Method = method.ToString();
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            return new NetworkResponse() { status = response.StatusCode, content = reader.ReadToEnd() };
        }

        public static NetworkResponse<T> HttpRequest<T>(string url, NetworkMethod method = NetworkMethod.GET)
        {
            NetworkResponse<string> response = Networking.HttpRequest(url, method);
            return new NetworkResponse<T>() { status = response.status, content = JsonSerializer.Deserialize<T>(response.content) };
        }

        public enum NetworkMethod
        {
            POST, GET, PUT, HEAD, DELETE, CONNECT, OPTIONS, TRACE, PATCH
        }

        public class NetworkResponse<T>
        {
            public HttpStatusCode status { get; set; }
            public T content { get; set; }
        }

        public class NetworkResponse : NetworkResponse<string> { }

    }
}
