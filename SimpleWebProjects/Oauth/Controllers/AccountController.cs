using Microsoft.AspNet.Identity;
using Oauth.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Oauth.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        protected readonly AuthRespository repo;
        public AccountController()
        {
            repo = new AuthRespository();
        }

        [HttpPost]
        [Route("register")]
        public HttpResponseMessage RegisterUser(UserModel user)
        {
            IdentityResult result = Task.Run(() =>  repo.RegisterUser(user)).Result;
            if (result == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An error has occured while creating the user");
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

               return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
