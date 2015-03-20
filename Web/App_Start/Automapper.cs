using AutoMapper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModels;

namespace Web.App_Start
{
    public static class Automapper
    {
        public static void Setup()
        {
            #region Estacionamento 

            Mapper.CreateMap<Model.Estacionamento, ViewModels.TabelaEstacionamento>()
                .ForMember(ViewModel => ViewModel.Endereco, map => map.MapFrom(model => model.EnderecoFormatado()));

            Mapper.CreateMap<ViewModels.TabelaEstacionamento, Model.Estacionamento>()
                .ForAllMembers(map => map.Ignore());

            Mapper.CreateMap<Model.Estacionamento, ViewModels.ListaEstacionamento>();

            #endregion

            #region CategoriaVaga

            Mapper.CreateMap<CategoriaVaga, FormularioVaga>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.Estacionamento.Id))
                .ForMember(viewModel => viewModel.Quantidade, map => map.MapFrom(model => model.Vagas.Count()))
                ; 

            Mapper.CreateMap<FormularioVaga, CategoriaVaga>()
                .ForMember(model => model.Estacionamento, map => map.MapFrom(viewModel => new Estacionamento() { Id = viewModel.Estacionamento }))
                .ForMember(model => model.Vagas, map => map.Ignore())
                ;

            Mapper.CreateMap<CategoriaVaga, TabelaVaga>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.Estacionamento.RazaoSocial))
                .ForMember(viewModel => viewModel.Vagas         , map => map.MapFrom(model => model.Vagas.Count()))
                .ForMember(viewModel => viewModel.Categoria     , map => map.MapFrom(model => model.Descricao))
                ;

            Mapper.CreateMap<TabelaVaga, CategoriaVaga>();

            #endregion


            #region Amdin

            Mapper.CreateMap<Usuario, FormularioAdmin>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.Estacionamento.Id))
                .ForMember(viewModel => viewModel.Quantidade, map => map.MapFrom(model => model.Vagas.Count()))
                ;

            Mapper.CreateMap<FormularioVaga, CategoriaVaga>()
                .ForMember(model => model.Estacionamento, map => map.MapFrom(viewModel => new Estacionamento() { Id = viewModel.Estacionamento }))
                .ForMember(model => model.Vagas, map => map.Ignore())
                ;

            Mapper.CreateMap<CategoriaVaga, TabelaVaga>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.Estacionamento.RazaoSocial))
                .ForMember(viewModel => viewModel.Vagas, map => map.MapFrom(model => model.Vagas.Count()))
                .ForMember(viewModel => viewModel.Categoria, map => map.MapFrom(model => model.Descricao))
                ;

            Mapper.CreateMap<TabelaVaga, CategoriaVaga>();

            #endregion
        }
    }
}