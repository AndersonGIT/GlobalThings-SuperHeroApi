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
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperHeroApi.Controllers
{
    [Authorize]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("api/usuario/registrarconta")]
        [AllowAnonymous]
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

                return Ok(new { Mensagem = "Usuario Registrado e Autenticado com sucesso.", Token = GetToken(userIdentity.GetUserId()) });
            }
            else
            {
                return InternalServerError(new Exception(result.Errors.FirstOrDefault()));
            }
        }

        [HttpPost]
        [Route("api/usuario/conectarconta")]
        [AllowAnonymous]
        public IHttpActionResult ConectarConta(Usuario usuario)
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
                        return Ok(new {Token = GetToken(userIdentity.GetUserId()) });
                    }
                    else
                    {
                        return BadRequest("Problemas com a autenticação.");
                    }
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

        [HttpPost]
        [Route("api/usuario/desconectarConta")]
        protected IHttpActionResult DesconectarConta(Usuario usuario)
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

        private Object GetToken(string userId)
        {
            var key = ConfigurationManager.AppSettings["JwtKey"];

            var issuer = ConfigurationManager.AppSettings["JwtIssuer"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a List of Claims, Keep claims name short    
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("userid", "userId"));

            //Create Security Token object by giving required parameters    
            var token = new JwtSecurityToken(issuer, //Issure    
                            issuer,  //Audience    
                            permClaims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: credentials);
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new { data = jwt_token };
        }
    }
}
