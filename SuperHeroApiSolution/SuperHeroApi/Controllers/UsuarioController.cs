using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SuperHeroApi.Models.Login;
using Microsoft.Owin.Security;
using System.Web;
using Swashbuckle.Swagger;
using System.Web.UI.WebControls;

namespace SuperHeroApi.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public IHttpActionResult RegistrarConta(RegistrarConta registrarConta)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser() { UserName = registrarConta.Usuario };
            IdentityResult result = manager.Create(user, registrarConta.Senha);

            if (result.Succeeded)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties() { }, userIdentity);

                return Ok("Usuario Registrado e Autenticado com sucesso.");
            }
            else
            {
                return InternalServerError(new Exception(result.Errors.FirstOrDefault()));
            }
        }

        [HttpPost]
        public IHttpActionResult SignIn(Usuario usuario)
        {
            try
            {
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);
                var user = userManager.Find(usuario.UsuarioLogin, usuario.UsuarioSenha);

                if (user != null)
                {
                    var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);

                    if (userIdentity.IsAuthenticated)
                    {
                        
                    }

                    //Response.Redirect("~/Login.aspx");
                    return Ok("Usuario Registrado e Autenticado com sucesso.");
                }
                else
                {
                    return BadRequest("Invalid username or password.");
                }
            }
            catch (Exception exception)
            {
                return InternalServerError(exception);
            }
        }

        protected IHttpActionResult SignOut(object sender, EventArgs e)
        {
            try
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                authenticationManager.SignOut();

                return Ok();
            }
            catch (Exception exception) {
                return InternalServerError(exception);
            }
        }
    }
}
