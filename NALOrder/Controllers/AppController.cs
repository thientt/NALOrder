using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// 
/// </summary>
namespace NALOrder.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AppController : Controller
    {
        /// <summary>
        /// Gets the authentication manager.
        /// </summary>
        /// <value>
        /// The authentication manager.
        /// </value>
        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                var ctx = Request.GetOwinContext();
                return ctx.Authentication;
            }
        }

        /// <summary>
        /// Gets the name of the current.
        /// </summary>
        /// <value>
        /// The name of the current.
        /// </value>
        public string CurrentName
        {
            get
            {
                var claim = User as ClaimsPrincipal;
                if (claim != null)
                    return claim.FindFirst(ClaimTypes.Name).Value;
                else return string.Empty;
            }
        }

        public string Role
        {
            get
            {
                var claim = User as ClaimsPrincipal;
                if (claim != null)
                {
                    Claim c = claim.FindFirst(ClaimTypes.Role);
                    if (c != null)
                        return c.Value;
                    else return string.Empty;
                }
                else return string.Empty;
            }
        }

        /// <summary>
        /// Gets the site.
        /// </summary>
        /// <value>
        /// The site.
        /// </value>
        public string Site
        {
            get
            {
                var claim = User as ClaimsPrincipal;
                if (claim != null)
                    return claim.FindFirst(ClaimTypes.Country).Value;
                else
                    return string.Empty;
            }
        }
    }
}