using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Oauth.Models
{
    public class AuthRespository
    {
        private AuthContext authContext;
        private UserManager<IdentityUser> userManager;
        public AuthRespository()
        {
            authContext = new AuthContext();
            userManager = new UserManager<IdentityUser>( new UserStore<IdentityUser>(authContext));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);

            return user;
        }
    }
}