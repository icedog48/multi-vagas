using AutoMapper;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ViewModels;
using Utils.Extensions;

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
                var datetime = Convert.ToDateTime(x);

                return datetime.TimeOfDay;
            });

            Mapper.CreateMap<TimeSpan, string>().ConvertUsing(x =>
            {
                return new DateTime(x.Ticks).ToString();
            });

            Mapper.CreateMap<DateTime, string>().ConvertUsing(x => x.ToString("dd/MM/yyyy HH:mm:ss"));
            Mapper.CreateMap<string, DateTime>().ConvertUsing(x => Convert.ToDateTime(x));


            Mapper.CreateMap<DateTime?, string>().ConvertUsing(x => (x.HasValue) ? x.Value.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty);
            Mapper.CreateMap<string, DateTime?>().ConvertUsing(x => (!string.IsNullOrEmpty(x)) ? Convert.ToDateTime(x) : (DateTime?)null);

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

            #endregion

            #region Categoria Vaga

            Mapper.CreateMap<int, CategoriaVaga>().ConvertUsing(x => new CategoriaVaga() { Id = x });
            Mapper.CreateMap<CategoriaVaga, int>().ConvertUsing(x => x.Id);

            Mapper.CreateMap<string, CategoriaVaga>().ConvertUsing(x => (!string.IsNullOrEmpty(x)) ? new CategoriaVaga() { Descricao = x } : null);
            Mapper.CreateMap<CategoriaVaga, string>().ConvertUsing(x => (x == null) ? Convert.ToString(null) : x.Descricao);

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

            Mapper.CreateMap<string, Vaga>().ConvertUsing(x => (!string.IsNullOrEmpty(x)) ? new Vaga() { Codigo = x } : null);
            Mapper.CreateMap<Vaga, string>().ConvertUsing(x => (x == null) ? Convert.ToString(null) : x.Codigo);

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
                .ForMember(model => model.NomeUsuario, map => map.Ignore())
                .ForMember(model => model.Perfil, map => map.Ignore())
                .ForMember(model => model.Senha, map => map.Ignore())
                .ForMember(model => model.AlterarSenha, map => map.Ignore())
                ;

            Mapper.CreateMap<Usuario, UsuarioFormFuncionario>();

            Mapper.CreateMap<UsuarioFormEstacionamento, Usuario>()
                .ForMember(model => model.SituacaoRegistro, map => map.MapFrom(viewModel => (int)SituacaoRegistroEnum.ATIVO))
                .ForMember(model => model.Perfil, map => map.Ignore())
                .ForMember(model => model.Senha, map => map.Ignore())
                .ForMember(model => model.AlterarSenha, map => map.Ignore())
                ;

            Mapper.CreateMap<Usuario, UsuarioFormEstacionamento>();


            Mapper.CreateMap<UsuarioForm, Usuario>()
                .ForMember(model => model.SituacaoRegistro, map => map.MapFrom(viewModel => (int)SituacaoRegistroEnum.ATIVO))
                .ForMember(model => model.Perfil, map => map.Ignore())
                .ForMember(model => model.AlterarSenha, map => map.Ignore())
                .ForMember(model => model.Email, map => map.Ignore())
                .ForMember(model => model.Senha, map => map.MapFrom(viewModel => viewModel.Senha))
                .ForMember(model => model.Id, map => map.Ignore())
                ;

            #endregion Usuario

            #region Funcionario

            Mapper.CreateMap<FuncionarioForm, Funcionario>()
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                .ForMember(model => model.Usuario, map => map.MapFrom(model => model.Usuario))
                .ForMember(model => model.HoraInicio, map => map.MapFrom(model => model.HoraInicio))
                .ForMember(model => model.HoraSaida, map => map.MapFrom(model => model.HoraSaida))
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

            Mapper.CreateMap<string, Cliente>().ConvertUsing(x => (!string.IsNullOrEmpty(x)) ? new Cliente() { Nome = x } : null);
            Mapper.CreateMap<Cliente, string>().ConvertUsing(x => (x == null) ? Convert.ToString(null) : x.Nome);

            Mapper.CreateMap<Cliente, ClienteForm>()
                .ForMember(model => model.Senha, map => map.Ignore())
                .ForMember(model => model.ConfirmacaoSenha, map => map.Ignore())
                ;

            Mapper.CreateMap<ClienteForm, Cliente>()
                .ForMember(model => model.SituacaoRegistro, map => map.MapFrom(viewModel => (int)SituacaoRegistroEnum.ATIVO))
                .ForMember(model => model.Usuario, map => map.Ignore())
                ;

            #endregion Cliente

            #region Movimentacao

            Mapper.CreateMap<MovimentacaoEntradaForm, Movimentacao>()
                .ForMember(model => model.Entrada, map => map.Ignore())
                .ForMember(model => model.FuncionarioEntrada, map => map.Ignore())
                .ForMember(model => model.FuncionarioSaida, map => map.Ignore())
                .ForMember(model => model.Saida, map => map.Ignore())
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                .ForMember(model => model.Ticket, map => map.Ignore())
                .ForMember(model => model.TipoPagamento, map => map.Ignore())
                .ForMember(model => model.ValorPago, map => map.Ignore())
                .ForMember(model => model.Vaga, map => map.MapFrom(viewModel => viewModel.Vaga))
                .ForMember(model => model.Cliente, map => map.MapFrom(viewModel => viewModel.Cliente))
            ;

            Mapper.CreateMap<MovimentacaoSaidaForm, Movimentacao>()
                .ForMember(model => model.Entrada, map => map.Ignore())
                .ForMember(model => model.FuncionarioEntrada, map => map.Ignore())
                .ForMember(model => model.FuncionarioSaida, map => map.Ignore())
                .ForMember(model => model.Saida, map => map.Ignore())
                .ForMember(model => model.SituacaoRegistro, map => map.Ignore())
                .ForMember(model => model.Ticket, map => map.Ignore())
                .ForMember(model => model.TipoPagamento, map => map.MapFrom(viewModel => viewModel.TipoPagamento))
                .ForMember(model => model.ValorPago, map => map.MapFrom(viewModel => viewModel.ValorPago))
                .ForMember(model => model.Vaga, map => map.MapFrom(viewModel => viewModel.Vaga))
                .ForMember(model => model.Cliente, map => map.MapFrom(viewModel => viewModel.Cliente))
            ;
            
            Mapper.CreateMap<Movimentacao, MovimentacaoEntradaForm>()
                .ForMember(viewModel => viewModel.CategoriaVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga))
            ;

            Mapper.CreateMap<Movimentacao, MovimentacaoSaidaForm>()
                .ForMember(viewModel => viewModel.CategoriaVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga))
                .ForMember(viewModel => viewModel.TipoPagamento, map => map.Ignore())
                .ForMember(viewModel => viewModel.ValorVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga.ValorHora))
            ;

            Mapper.CreateMap<Movimentacao, MovimentacaoTable>()
                .ForMember(viewModel => viewModel.Entrada, map => map.MapFrom(model => model.Entrada.ToString("dd/MM/yyyy HH:mm")))
                .ForMember(viewModel => viewModel.Vaga, map => map.MapFrom(model => model.Vaga.Codigo))
            ;

            Mapper.CreateMap<Movimentacao, MovimentacaoPorPeriodoTable>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.FuncionarioEntrada.Estacionamento.RazaoSocial))
                .ForMember(viewModel => viewModel.CategoriaVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga))
                .ForMember(viewModel => viewModel.Placa, map => map.MapFrom(model => model.Placa))
                .ForMember(viewModel => viewModel.Vaga, map => map.MapFrom(model => model.Vaga.Codigo))
                .ForMember(viewModel => viewModel.Data, map => map.MapFrom(model => model.Entrada.Date.ToString("dd/MM/yyyy")))
                .ForMember(viewModel => viewModel.HorasReferencia, map => map.MapFrom(model => model.HorasReferencia))
                .ForMember(viewModel => viewModel.TipoPagamento, map => map.MapFrom(model => model.TipoPagamento))
            ;

            Mapper.CreateMap<Movimentacao, MovimentacaoPorCategoriaTable>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.FuncionarioEntrada.Estacionamento.RazaoSocial))
                .ForMember(viewModel => viewModel.CategoriaVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga))
                .ForMember(viewModel => viewModel.Placa, map => map.MapFrom(model => model.Placa))
                .ForMember(viewModel => viewModel.Vaga, map => map.MapFrom(model => model.Vaga.Codigo))
                .ForMember(viewModel => viewModel.Data, map => map.MapFrom(model => model.Entrada.Date.ToString("dd/MM/yyyy")))
                .ForMember(viewModel => viewModel.HorasReferencia, map => map.MapFrom(model => model.HorasReferencia))
                .ForMember(viewModel => viewModel.TipoPagamento, map => map.MapFrom(model => model.TipoPagamento))
            ;

            Mapper.CreateMap<Movimentacao, MovimentacaoPorEstadiaTable>()
                .ForMember(viewModel => viewModel.Estacionamento, map => map.MapFrom(model => model.FuncionarioEntrada.Estacionamento.RazaoSocial))
                .ForMember(viewModel => viewModel.CategoriaVaga, map => map.MapFrom(model => model.Vaga.CategoriaVaga))
                .ForMember(viewModel => viewModel.Placa, map => map.MapFrom(model => model.Placa))
                .ForMember(viewModel => viewModel.Vaga, map => map.MapFrom(model => model.Vaga.Codigo))
                .ForMember(viewModel => viewModel.Data, map => map.MapFrom(model => model.Entrada.Date.ToString("dd/MM/yyyy")))
                .ForMember(viewModel => viewModel.HorasReferencia, map => map.MapFrom(model => model.HorasReferencia))
                .ForMember(viewModel => viewModel.TipoPagamento, map => map.MapFrom(model => model.TipoPagamento))
            ;

            #endregion Movimentacao            

            #region TipoPagamento

            Mapper.CreateMap<int, TipoPagamento>().ConvertUsing(x => new TipoPagamento() { Id = x });
            Mapper.CreateMap<TipoPagamento, int>().ConvertUsing(x => x.Id);

            Mapper.CreateMap<string, TipoPagamento>().ConvertUsing(x => new TipoPagamento() { Descricao = x });
            Mapper.CreateMap<TipoPagamento, string>().ConvertUsing(x => x.Descricao);

            Mapper.CreateMap<TipoPagamento, TipoPagamentoCombo>();

            #endregion TipoPagamento

            Mapper.AssertConfigurationIsValid();
        }
    }
}