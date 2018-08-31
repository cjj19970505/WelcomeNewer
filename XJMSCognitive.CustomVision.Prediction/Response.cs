using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XJMSCognitive.CustomVision.Prediction
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Response
    {
        [JsonProperty]
        public string Id { get; internal set; }
        [JsonProperty]
        public string Project { get; internal set; }
        [JsonProperty]
        public string Iteration { get; internal set; }
        [JsonProperty]
        public string Created { get; internal set; }

        [JsonProperty]
        public Prediction[] Predictions { get; internal set; }

        public string Json { get; private set; }

        internal static Response FromJson(string json)
        {
            //Movie m = JsonConvert.DeserializeObject<Movie>(json);
            Response r = JsonConvert.DeserializeObject<Response>(json);
            r.Json = json;
            return r;
        }

        public override string ToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Id: " + Id);
                sb.AppendLine("Project: " + Project);
                sb.AppendLine("Iteration: " + Iteration);
                sb.AppendLine("Created: " + Created);

                sb.AppendLine("PredictionsCount: " + Predictions.Length);
                foreach (var prediction in Predictions)
                {
                    sb.AppendLine(prediction.ToString());
                }
                return sb.ToString();
            }
            catch(Exception e)
            {
                return "ERROR::"+Json;
            }
            
        }

    }

    public class Prediction
    {
        public double Probability;
        public string TagId;
        public string TagName;
        public Region Region;
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Probability: " + Probability);
            sb.AppendLine("TagId: " + TagId);
            sb.AppendLine("TagName: " + TagName);
            sb.AppendLine("Region: " + Region);
            return sb.ToString();
        }
    }

    public class Region
    {
        public double Left;
        public double Top;
        public double Width;
        public double Height;
        public override string ToString()
        {
            return "(Left: " + Left + ", Top: " + Top + ", Width: " + Width + ", Height:" + Height+")";
        }
    }
}
