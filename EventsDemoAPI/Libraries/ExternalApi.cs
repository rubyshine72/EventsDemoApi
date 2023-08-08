using Newtonsoft.Json;
using System.Net;

namespace EventsDemoAPI.Libraries
{
    public static class ExternalApi
    {
        public static dynamic? getUserInfo(int id)
        {
            try
            {
                WebClient client = new WebClient();
                var jsonstring = client.DownloadString("https://jsonplaceholder.typicode.com/users/" + id.ToString());
                if (jsonstring == null || jsonstring == "{}")
                {
                    return null;
                }
                dynamic dynObj = JsonConvert.DeserializeObject(jsonstring);
                return dynObj;
            } catch {
                return null;
            }
        }
    }
}
