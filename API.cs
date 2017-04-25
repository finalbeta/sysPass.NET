using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Dynamic;

namespace sysPassApi
{
    public class sysPass
    {
        public sysPass(string APIURL, string APIToken, string BasicAuthUser, string BasicAuthPass)
        {
            this.user = BasicAuthUser;
            this.password = BasicAuthPass;
            this.APIURL = APIURL;
            this.APIToken = APIToken;
            if (user != null) this.basicAuth = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(this.user + ":" + this.password));
        }

        private string user = null;
        private string password;
        private string APIURL;
        private string APIToken = null;
        private string basicAuth = null;
        
        public string jsonResponse(string method, dynamic parameters)
        {
            string jsonParams = null;
            if (parameters != null)
            {
                jsonParams = JsonConvert.SerializeObject(buildRequest(method, parameters));
            }
            return sendRequest(jsonParams);
        }


        public ExpandoObject objectResponse(string method, ExpandoObject parameters)
        {
            string jsonParams = null;
            if (parameters != null)
            {
                jsonParams = JsonConvert.SerializeObject(buildRequest(method,parameters));
            }
            string jsonResponse = sendRequest(jsonParams);
            ExpandoObject response = JsonConvert.DeserializeObject<ExpandoObject>(jsonResponse);
            return response;
        }

        private string sendRequest(string jsonParams)
        {
            WebRequest request = WebRequest.Create(APIURL);
            if (basicAuth != null) request.Headers.Add("Authorization", "Basic " + basicAuth);
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(jsonParams);
                streamWriter.Flush();
                streamWriter.Close();
            }
            
            string jsonResult;
            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        private object buildRequest(string method, ExpandoObject parameters )
        {
            ((dynamic)parameters).authToken = this.APIToken;
            return new { jsonrpc = "2.0", method = method, id = "sysPass.NET", @params = parameters};
        }

    }
}