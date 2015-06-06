using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.ViewModels;
using Storage;
using AutoMapper;
using Newtonsoft.Json;
using Web.Controllers.Attributes;
using Model;
using Service.Interfaces;
using Service.Filters;
using Web.App_Start;


namespace Web.Controllers
{
    [NHSession]
    [MultivagasAuthorize]
    public class RelatoriosController : ApiController
    {

        public RelatoriosController()
        {

        }

    }
}
