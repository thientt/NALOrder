// ***********************************************************************
// Assembly         : NALOrder.Model
// Author           : tranthiencdsp@gmail.com
// Created          : 05-6-2016
//
// Last Modified By : tranthiencdsp@gmail.com
// Last Modified On : 05-6-2016
// ***********************************************************************


using NALOrder.Model;
using NALOrder.Utilities;
using NALOrder.ViewModel;
using Ninject;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

/// <summary>
/// 
/// </summary>
namespace NALOrder.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController:AppController
    {
        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <param name="ReturnUrl">The return URL.</param>
        /// <returns></returns>
        [HttpGet, AllowAnonymous]
        public ActionResult LogIn(string ReturnUrl)
        {
            var userLogin = new UserLoginViewModel()
            {
                ReturnUrl = ReturnUrl
            };

            return View(userLogin);
        }

        /// <summary>
        /// Logins the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public async Task<ActionResult> Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (ModelState.IsValid)
            {
                var userLogin = await UserRepository.LoginAsync(user.Email, user.Password);
                if (userLogin != null)
                {
                    var identities = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name,userLogin.Email),
                    new Claim(ClaimTypes.Role,userLogin.Role.Name),
                    new Claim(ClaimTypes.Email, user.Email)},
                    "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
                    AuthenticationManager.SignIn(identities);

                    return Redirect(GetRedirectUrl(user.ReturnUrl));
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect or User is not yet allowed. Contact System Administrator!");
                    return View(user);
                }
            }
            return View();
        }

        /// <summary>
        /// Logs the out.
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut("ApplicationCookie");

            return RedirectToAction(actionName: "LogIn", controllerName: "Account");
        }

        #region Inject
        [Inject]
        public ILogService LogService { get; set; }

        /// <summary>
        /// Gets or sets the user repository.
        /// </summary>
        /// <value>
        /// The user repository.
        /// </value>
        [Inject]
        public IUsersRepository UserRepository { get; set; }

        /// <summary>
        /// Gets or sets the sites repository.
        /// </summary>
        /// <value>
        /// The sites repository.
        /// </value>
        #endregion

        /// <summary>
        /// Gets the redirect URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        private string GetRedirectUrl(string returnUrl)
        {
            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                var user = User;
                return Url.Action("Index", "Management");
            }

            return returnUrl;
        }
    }
}
