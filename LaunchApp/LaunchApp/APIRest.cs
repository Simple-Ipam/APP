using System;
using System.Text;
using System.Net.Http;

namespace LaunchApp
{
    public static class APIRest
    {

        public static string Post(string url,string valueFunction)
        {
            using (var client = new HttpClient())
            {

                var objJSON = new Function { function = valueFunction };

                var content = new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(objJSON),Encoding.UTF8,"application/json"
                );
                Console.WriteLine(">[WEB-PRELOAD]>: " + content);
                var res = client.PostAsync(url,content);
                try
                {
                    var resp = res.Result.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(">[WEB]>: "+resp);
                    return resp;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return "ERROR";
                }
            }

        }

    }

    public class Function
    {
        public string function
        {
            get;set;
        }
    }

}
