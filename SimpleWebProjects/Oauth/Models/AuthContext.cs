using Microsoft.AspNet.Identity.EntityFramework;

namespace Oauth.Models
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext():base("authContext")
        {

        }
    }
}