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
            Mapper.CreateMap<Model.Estacionamento, ViewModels.ListaEstacionamento>()
                .ForMember(ViewModel => ViewModel.Endereco, map => map.MapFrom(model => model.EnderecoFormatado()));

            Mapper.CreateMap<ViewModels.ListaEstacionamento, Model.Estacionamento>()
                .ForAllMembers(map => map.Ignore());

        }
    }
}