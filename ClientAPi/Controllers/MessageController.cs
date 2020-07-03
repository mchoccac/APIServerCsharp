using System;
using System.Net;
using System.Web.Http;
using ClientAPi.Models;
using ClientAPi.App_Code;

namespace ClientAPi.Controllers
{

    [RoutePrefix("api/Messages")]
    public class MessageController : ApiController
    {

        /// <summary>
        /// 
        /// he POST /message route expects to receive parameters msg and tags in the body of the request (in addition to authentication headers described next) and proceeds to do the following:  
        /// Store the received data and return an unique identiﬁer for it along with the http status code 200
        /// 
        /// 
        /// </summary>
        [HttpPost]
        [Route("message")]
        public IHttpActionResult message(ClsToken login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Code_credencial usuario = new Code_credencial();
            var ftusuario = new ClsCredencial();
            ftusuario.Key = login.X_Key;

            if (usuario.getBuscarKey(ftusuario) == true && usuario.getToken(ftusuario).Equals(login.X_Signature) == true)
            {
                var mensaje = new ClsMessage();

                //puede haber colision  random semialeatorio
                Random rand = new Random();
                int numeroRandom = rand.Next(1, 1000000);
                
                //mensaje.Message = login.Message;
                mensaje.Id = numeroRandom.ToString();
                login.Id = numeroRandom.ToString();
                usuario.setIdentiﬁer(login);
                return Ok(mensaje);
            }
            return StatusCode(HttpStatusCode.Forbidden);

        }


        /// <summary>
        /// 
        /// he GET /message/<id> route expects no parameters (besides the authentication headers) and proceeds to do the following:
        /// Fetch the message by the id given and return it along with the http status code 200
        /// 
        /// 
        /// </summary>
        [HttpGet]
        [Route("id")]
        public IHttpActionResult id(ClsToken login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Code_credencial usuario = new Code_credencial();
            var ftusuario = new ClsCredencial();
            ftusuario.Key = login.X_Key;

            if (usuario.getBuscarKey(ftusuario) == true && usuario.getToken(ftusuario).Equals(login.X_Signature) == true &&
                 usuario.getIdentiﬁerId(login).Equals(login.Id) == true)
            {
                var mensaje = new ClsMessage();
                mensaje.Message = usuario.getIdentiﬁerMessage(login);
                return Ok(mensaje);
            }
            return StatusCode(HttpStatusCode.Forbidden);
            
        }


        /// <summary>
        /// 
        /// The GET /messages/<tag> route expects no parameters (besides the authentication headers) and proceeds to do the following:
        /// Fetch all messages with a given tag and return them along with the http status code 200
        /// 
        /// param = @ClsToken
        ///         Tag = key ; get all keys de todos las peticiones que se creo
        ///         Tag = value ; get all Values de todos las peticiones que se creo
        /// 
        /// </summary>
        [HttpGet]
        [Route("tag")]
        public IHttpActionResult tag(ClsToken login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Code_credencial usuario = new Code_credencial();
            var ftusuario = new ClsCredencial();
            ftusuario.Key = login.X_Key;

            if (usuario.getBuscarKey(ftusuario) == true && usuario.getToken(ftusuario).Equals(login.X_Signature) == true)
            {
                var mensaje = new ClsMessage();
                if (login.Tag.Equals("key"))
                {
                    return Ok(usuario.getAllKey());
                }
                else if (login.Tag.Equals("value"))
                {
                    return Ok(usuario.getAllValue());
                }else {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }
            return StatusCode(HttpStatusCode.Forbidden);
        }
    }
}
