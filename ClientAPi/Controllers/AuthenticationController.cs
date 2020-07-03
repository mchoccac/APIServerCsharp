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


        /// <summary>
        /// Nota: lo que entiendo es que este apartado se crea los credenciales
        /// 
        /// The PUT /credential  route expects to receive parameters key and shared_secret in the body of the request and proceeds to do the following: 
        /// *If key is already present in the server storage, return the http status code 403
        //  *If key is not present in the server storage yet, store the parameters received and return the http status code 204
        /// </summary>
        [HttpPut]
        [Route("Credential")]
        public IHttpActionResult Credential(ClsCredencial login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Code_credencial usuario = new Code_credencial();

            if (usuario.getBuscarKey(login) == false)
            {
                CodeHMAC hMac = new CodeHMAC();
                var token = hMac.getHMAC256(login.Shared_Secret);
                usuario.setCrearCreden(login, token);

                var claimsIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login.Key),
                    new Claim(ClaimTypes.Role, "Client"),
                }, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
                var mToken = new ClsToken();
                mToken.X_Signature = token;
                mToken.X_Key = login.Key;
                return Ok(mToken);
                
                //?????????
                //return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }


        /// <summary>
        /// Nota: Credential(ClsCredencial login) obtenemos el X_Signature para enviarlos a los mensajes
        /// 
        /// Authentication
        /// Authenticated routes must ﬁrst pass through the following steps before they are accepted for processing:
        /// Header X-Key must be present and it must contain a string that represents a key already known
        /// Header X-Route must be present and it must containt a string representing the route for the request
        /// Header X-Signature must be present and it must contain a string representing the HMAC-SHA256 result achieved based on the following description.
        /// The data to be signed is obtained by getting the key/value pairs of the body parameters, and url parameters if any, along with the X-Route header. These pairs are then separated by semicolon and sorted in lexicographical order.  The HMAC key is the one passed earlier as shared_secret.The resulting digest as a string encoded in hexadecimal is the value to be used in the X-Signature header.
        /// The server must validate that the signature matches the one expected, and in case it does not the server must return a http status code 403.
        /// </summary>
        [HttpPost]
        [Route("Authentication")]
        public IHttpActionResult Authentication(ClsToken login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Code_credencial usuario = new Code_credencial();
            var ftusuario = new ClsCredencial();
            ftusuario.Key = login.X_Key;

            if (usuario.getBuscarKey(ftusuario) == true && usuario.getToken(ftusuario).Equals(login.X_Signature)==true)
            {
                return Ok();
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }

    }
}
