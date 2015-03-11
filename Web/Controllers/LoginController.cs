using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("api/account")]
        public dynamic Login (Usuario usuario) 
        {
            usuario.Id = 1;

            return new 
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Permissoes = new string[] { "equipe_multivagas" }
            }; 
        }
    }
}
