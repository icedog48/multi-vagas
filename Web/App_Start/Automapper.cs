using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.App_Start
{
    public static class Automapper
    {
        public static void Setup() 
        {
            Mapper.CreateMap<Model.Estacionamento, ViewModels.Estacionamento>();
            Mapper.CreateMap<ViewModels.Estacionamento, Model.Estacionamento>();

        }
    }
}