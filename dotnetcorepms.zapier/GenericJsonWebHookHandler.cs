using System;
using System.Web;
using Microsoft.AspNet.WebHooks;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace dotnetcorepms.zapier
{
    public class GenericJsonWebHookHandler : WebHookHandler
    {
        public GenericJsonWebHookHandler()
        {
            this.Receiver = "genericjson";
        }

        public override Task ExecuteAsync(string receiver, WebHookHandlerContext context)
        {
            // Get JSON from WebHook
            JObject data = context.GetDataOrDefault<JObject>();

            var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data.ToString());
            
            if (context.Id == "z")
            {
                // Received a webhook from zapier
            }

            return Task.FromResult(true);
        }
    }
}
