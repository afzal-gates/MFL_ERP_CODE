using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Linq;
using System.Net;

namespace System.Web.Http.Filters
{
    public class ExternalReqAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (IsAuthorized(actionContext))
            {
                base.OnAuthorization(actionContext);
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
            
        }

        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            HttpRequestHeaders requestHeaders = request.Headers;
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("_security_code", out values) && values.First() == "qYysfxpEgBsR1v2Ox+NPefw9d2vYtzWddg68gupxJN0")
            {
                return true;
            }
            return false ;
            
        }


        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Content = new StringContent("Sorry!!! No Valid Security Code")
            };
        }
    }
}