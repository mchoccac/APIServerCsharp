using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClientAPi.Models;
using ClientAPi.App_Code;
using System.Security.Claims;

namespace ClientAPi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class AuthenticationController : ApiController
    {

        [HttpPost]
        [Route("Authentication")]
        public IHttpActionResult Authenticate(ClsCredencial login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            CodeHMAC hMac = new CodeHMAC();

            var token = hMac.getHMAC256(login.Key + login.Shared_Secret);

            Code_credencial usuario = new Code_credencial();

            usuario.setCrearCreden(login,token);           

            if (usuario.getBuscarMemoria(login) == false)
            {
                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login.Key),
                    new Claim(ClaimTypes.Role, "Client"),
                },"ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);

                return Ok(token);
            }

            return StatusCode(HttpStatusCode.Forbidden);
        }

    }
}
