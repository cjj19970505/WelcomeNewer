using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO;

namespace XJMSCognitive.CustomVision.Prediction
{
    public class PredictionManager
    {
        public string Uri { get; set; }
        public string PredictionKey { get; set; }
        public string IterationId { get; set; }
        
        
    }

    public class UrlPredictionManager:PredictionManager
    {
        public async Task<Response> Infer(string imageUrl)
        {
            var client = new HttpClient();

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["iterationId"] = IterationId;

            client.DefaultRequestHeaders.Add("Prediction-Key", PredictionKey);
            using (StringContent stringContent = new StringContent("{\"Url\": \"" + imageUrl + "\"}"))
            {
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var httpResponse = await client.PostAsync(Uri + queryString, stringContent);
                return JsonConvert.DeserializeObject<Response>(await httpResponse.Content.ReadAsStringAsync());
            }
        }
    }

    public class ImagePredictionManager : PredictionManager
    {
        public async Task<Response> Infer(Stream fileStream)
        {
            var client = new HttpClient();

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["iterationId"] = IterationId;

            client.DefaultRequestHeaders.Add("Prediction-Key", PredictionKey);
            using (BinaryReader binaryReader = new BinaryReader(fileStream))
            {
                var byteData = binaryReader.ReadBytes((int)fileStream.Length);
                //byte[] byteData = new byte[0];
                using(var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    var httpResponse = await client.PostAsync(Uri + queryString, content);
                    return JsonConvert.DeserializeObject<Response>(await httpResponse.Content.ReadAsStringAsync());
                }
            }
        }
    }
}
