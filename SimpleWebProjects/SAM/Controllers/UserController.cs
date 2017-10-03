using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SAM.Controllers
{
    public class UserController : Controller
    {
        const string OfficeLocationClaimType = "https://brockallen.com/claims/officelocation";

        private Claim[] LoadClaimsForUser(string username)
        {
            var claims = new Claim[]
            {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Email, "username@company.com"),
        new Claim(ClaimTypes.Role, "RoleA"),
        new Claim(ClaimTypes.Role, "RoleB"),
        new Claim(OfficeLocationClaimType, "5W-A1"),
            };
            return claims;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (string.Equals(username, "test", StringComparison.OrdinalIgnoreCase) && string.Equals(password, "pwd", StringComparison.OrdinalIgnoreCase))
            {
                Claim[] claims = LoadClaimsForUser(username);
                var id = new ClaimsIdentity(claims, "Forms");
                var cp = new ClaimsPrincipal(id);

                var token = new SessionSecurityToken(cp);
                var sam = FederatedAuthentication.SessionAuthenticationModule;
                sam.WriteSessionTokenToCookie(token);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}