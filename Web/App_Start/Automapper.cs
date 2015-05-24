using AutoMapper;
using Model;
using Model.Common;
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
            #region Tipos fora do modelo

            Mapper.CreateMap<DateTime, TimeSpan>().ConvertUsing(x => x.TimeOfDay);
            Mapper.CreateMap<TimeSpan, DateTime>().ConvertUsing(x => new DateTime(x.Ticks));

            Mapper.CreateMap<string, TimeSpan>().ConvertUsing(x =>
            {
                var HH = 0;

                if (x.Length > 1) HH = Convert.ToInt32(x.Substring(0, 2));

                var mm = 0;

                if (x.Length > 4) mm = Convert.ToInt32(x.Substring(3, 2));

                var ss = 0;

                if (x.Length > 7) ss = Convert.ToInt32(x.Substring(6, 2));

                return new TimeSpan(HH, mm, ss);
            });

            Mapper.CreateMap<TimeSpan, string>().ConvertUsing(x =>
            {
                return x.ToString("hh':'mm':'ss");
            });

            #endregion

            #region Estacionamento

            Mapper.CreateMap<Estacionamento, int>().ConvertUsing(x => x.Id);
            Mapper.CreateMap<int, Estacionamento>().ConvertUsing(x => new Estacionamento() { Id = x });

            Mapper.CreateMap<Estacionamento, string>().ConvertUsing(x => x.RazaoSocial);
            Mapper.CreateMap<string, Estacionamento>().ConvertUsing(x => new Estacionamento() { RazaoSocial = x });

            Mapper.CreateMap<Estacionamento, EstacionamentoTable>()
                .ForMember(ViewModel => ViewModel.Endereco, map => map.MapFrom(model => model.EnderecoFormatado()));

            Mapper.CreateMap<EstacionamentoTable, Estacionamento>()
                .ForAllMembers(map => map.Ignore());

            Mapper.CreateMap<Estacionamento, EstacionamentoCombo>();

            Mapper.CreateMap<Estacionamento, EstacionamentoForm>();

            Mapper.CreateMap<EstacionamentoForm, Estacionamento>()
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                .ForMember(model => model.Usuario, map => map.Ignore())
                ;

            Mapper.CreateMap<Estacionamento, EstacionamentoFormAdministrador>();

            Mapper.CreateMap<EstacionamentoFormAdministrador, Estacionamento>()
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                ;

            #endregion

            #region Categoria Vaga

            Mapper.CreateMap<int, CategoriaVaga>().ConvertUsing(x => new CategoriaVaga() { Id = x });
            Mapper.CreateMap<CategoriaVaga, int>().ConvertUsing(x => x.Id);

            Mapper.CreateMap<CategoriaVaga, CategoriaVagaForm>()
                .ForMember(viewModel => viewModel.Quantidade, map => map.MapFrom(model => model.Vagas.Count()))
                ; 

            Mapper.CreateMap<CategoriaVagaForm, CategoriaVaga>()
                .ForMember(model => model.Vagas, map => map.Ignore())
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                ;

            Mapper.CreateMap<CategoriaVaga, CategoriaVagaTable>()
                .ForMember(viewModel => viewModel.Vagas         , map => map.MapFrom(model => model.Vagas.Count()))
                .ForMember(viewModel => viewModel.Categoria     , map => map.MapFrom(model => model.Descricao))
                ;

            Mapper.CreateMap<Vaga, VagaCombo>()
                .ForMember(model => model.CategoriaVaga, map => map.MapFrom(viewModel => viewModel.CategoriaVaga.Descricao))
                ;

            Mapper.CreateMap<CategoriaVaga, CategoriaVagaCombo>();
            
            #endregion

            #region Vaga

            Mapper.CreateMap<int, Vaga>().ConvertUsing(x => new Vaga() { Id = x });
            Mapper.CreateMap<Vaga, int>().ConvertUsing(x => x.Id);

            #endregion Vaga

            #region Perfil

            Mapper.CreateMap<Perfil, int>().ConvertUsing(x => x.Id);
            Mapper.CreateMap<int, Perfil>().ConvertUsing(x => new Perfil() { Id = x });

            Mapper.CreateMap<Perfil, string>().ConvertUsing(x => x.Nome);
            Mapper.CreateMap<string, Perfil>().ConvertUsing(x => new Perfil() { Nome = x });

            Mapper.CreateMap<Perfil, PerfilCombo>();

            #endregion Perfil

            #region Usuario

            Mapper.CreateMap<UsuarioFormFuncionario, Usuario>()
                .ForMember(model => model.SituacaoRegistro, map => map.MapFrom(viewModel => (int)SituacaoRegistroEnum.ATIVO))
                .ForMember(model => model.Login, map => map.Ignore())
                .ForMember(model => model.Perfil, map => map.Ignore())
                .ForMember(model => model.Senha, map => map.Ignore())
                ;

            Mapper.CreateMap<Usuario, UsuarioFormFuncionario>();

            Mapper.CreateMap<UsuarioFormEstacionamento, Usuario>()
                .ForMember(model => model.SituacaoRegistro, map => map.MapFrom(viewModel => (int)SituacaoRegistroEnum.ATIVO))
                .ForMember(model => model.Perfil, map => map.Ignore())
                .ForMember(model => model.Senha, map => map.Ignore())
                ;

            Mapper.CreateMap<Usuario, UsuarioFormEstacionamento>();

            #endregion Usuario

            #region Funcionario

            Mapper.CreateMap<FuncionarioForm, Funcionario>()
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                .ForMember(model => model.Usuario, map => map.MapFrom(model => model.Usuario))
                .ForMember(model => model.HoraInicio, map => map.MapFrom(model => model.HoraInicio))
                .ForMember(model => model.HoraSaida, map => map.MapFrom(model => model.HoraSaida))
                .ForMember(model => model.CargaHoraria, map => map.MapFrom(model => model.CargaHoraria))
                ;

            Mapper.CreateMap<Funcionario, FuncionarioForm>()
                .ForMember(model => model.Usuario, map => map.MapFrom(model => model.Usuario))
                ;

            Mapper.CreateMap<Funcionario, FuncionatioTable>()
                .ForMember(model => model.Perfil, map => map.MapFrom(model => model.Usuario.Perfil))
                ;

            #endregion Funcionario

            #region Cliente

            Mapper.CreateMap<int?, Cliente>().ConvertUsing(x => (x.HasValue) ? new Cliente() { Id = x.Value } : null);
            Mapper.CreateMap<Cliente, int?>().ConvertUsing(x => (x == null) ? Convert.ToInt32(null) : x.Id);

            #endregion Cliente

            #region Movimentacao

            Mapper.CreateMap<MovimentacaoForm, Movimentacao>()
                .ForMember(model => model.Entrada, map => map.Ignore())
                .ForMember(model => model.FuncionarioEntrada, map => map.Ignore())
                .ForMember(model => model.FuncionarioSaida, map => map.Ignore())
                .ForMember(model => model.Saida, map => map.Ignore())
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                .ForMember(model => model.Ticket, map => map.Ignore())
                .ForMember(model => model.TipoPagamento, map => map.Ignore())
                .ForMember(model => model.ValorPago, map => map.Ignore())
                ;

            Mapper.CreateMap<Movimentacao, MovimentacaoForm>()
                .ForMember(viewModel => viewModel.CategoriaVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga))
            ;

            Mapper.CreateMap<Movimentacao, MovimentacaoTable>()
                .ForMember(viewModel => viewModel.Entrada, map => map.MapFrom(model => model.Entrada.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(viewModel => viewModel.Vaga, map => map.MapFrom(model => model.Vaga.Codigo))
                ;

            #endregion Movimentacao            

            #region TipoPagamento

            Mapper.CreateMap<int, TipoPagamento>().ConvertUsing(x => new TipoPagamento() { Id = x });

            Mapper.CreateMap<TipoPagamento, TipoPagamentoCombo>();

            #endregion TipoPagamento

            Mapper.AssertConfigurationIsValid();
        }
    }
}