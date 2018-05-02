using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EksamenM2E22017.Services
{
    public class ApiAccess
    {
        private string endPoint;

        public ApiAccess(string endPoint)
        {
            EndPoint = endPoint;
        }

        public string EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }

        public JToken GetApiResponse(string queryString)
        {
            string url = EndPoint + $"?format=json&action=query&prop=&exintro=&explaintext=&title={queryString}";
            JToken pageValue;
            using (WebClient client = new WebClient())
            {
                string content = client.DownloadString(url);

                // Make a JObject
                var res = JObject.Parse(content);

                // get the list of entities in the pages object
                var entities = (JObject)res["query"]["pages"];

                // gets the first "page extract"
                var entity = entities.Properties().First();

                // gets the values from within the object
                var pageValues = (JObject)entity.Value;

                // use keys here to access values of the object
                pageValue = pageValues["extract"];
            }
            return pageValue;

        }

    }
}
