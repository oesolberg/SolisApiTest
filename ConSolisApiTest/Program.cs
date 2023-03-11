using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;


namespace ConSolisApiTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var solisConnection = new SolisConnection();
            var result = await solisConnection.Execute();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }

    internal class SolisConnection
    {
        private string KeyId = "xxxxxxxxx"; //Api key
        private string SecretKeyId = "xxxxx"; //Api secret key

        private string UserId = "xxxxx"; //id number from the url bar of your inverter detail page; 
        private string InverterSn = "xxxx";//the serial number of your inverter

        static HttpClient client = new HttpClient(new WinHttpHandler());

        public async Task<string>  Execute()
        {
            //var body = "{\"pageNo\":1,\"pageSize\":10}";
            var body = "{\"id\":\""+UserId+"\",\"sn\":\""+InverterSn+"\"}";

            var contentMd5 = Convert.ToBase64String(Md5Convert(body));
            
            var contentType = "application/json";
            //var endPoint = "/v1/api/userStationList";

            var endPoint = "/v1/api/inveterDetail";
            var gmDate = DateTime.UtcNow.ToString("R");
            
            var cadena = $"POST\n{contentMd5}\n{contentType}\n{gmDate}\n{endPoint}";
            
            var signature = CreateSignature(cadena);


            //Create and Post

            client.BaseAddress = new Uri("https://www.soliscloud.com:13333/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Clear();
            
            client.DefaultRequestHeaders.Add("Authorization", $"API {KeyId}:{signature}");
            client.DefaultRequestHeaders.Add("Date", gmDate);
            
            
            HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");
            content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            content.Headers.ContentMD5 = Md5Convert(body);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            try
            {

          
                var response = await client.PostAsync(endPoint, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return String.Empty;
        }
        

        private string CreateSignature(string input)
        {

            var encoding = new System.Text.UTF8Encoding();
            var keyBytes = encoding.GetBytes(SecretKeyId);
            var messageBytes = encoding.GetBytes(input);
            using (var hmacsha1 = new HMACSHA1(keyBytes))
            {
                var hashMessage = hmacsha1.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashMessage);
            }

        }

        private byte[] Md5Convert(string text)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(text);
            return x.ComputeHash(bs);
        }
    }
}
