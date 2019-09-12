using ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ERPSolution.Controllers
{
    public class UserApiController : ApiController
    {
        // GET: api/UserApi
        [ResponseType(typeof(UserModelApi))]

        //[Authorize]
        public IHttpActionResult Get(string LOGIN_ID)
        {
            try
            {
                var ob = new UserModelApi().SelectDatabyLoginId(LOGIN_ID);
                return Ok(ob);

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [ResponseType(typeof(UserModelApi))]
        public IHttpActionResult get(string LOGIN_ID, string PUSH_REGI_ID)
        {
            try
            {
                var ob = new UserModelApi() {
                    LOGIN_ID = LOGIN_ID,
                    PUSH_REGI_ID = PUSH_REGI_ID
                };
                var obj = new UserModelApi().registerPush(ob);

                if (obj == null)
                {
                    return Conflict();
                }

                return Created<UserModelApi>("sdddd", obj);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
