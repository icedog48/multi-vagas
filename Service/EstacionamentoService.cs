using Model;
using Model.Common;
using Service.Common;
using Service.Filters;
using Service.Interfaces;
using Service.Validations;
using Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Service
{
    public class EstacionamentoService : MultiVagasCRUDService<Estacionamento>, IEstacionamentoService
    {
        private readonly IUsuarioService usuarioService;
        private readonly IFuncionarioService funcionarioService;
        private readonly IRepository<TipoPagamento> tipoPagamentoRepository;

        public EstacionamentoService
            (
                IRepository<Estacionamento> repository, 
                EstacionamentoValidator validator, 
                IUsuarioService usuarioService, 
                IFuncionarioService funcionarioService, 
                Usuario usuarioLogado,
                IRepository<TipoPagamento> tipoPagamentoRepository
            )
            : base(repository, validator, usuarioLogado)
        {
            this.usuarioService = usuarioService;
            this.funcionarioService = funcionarioService;
            this.tipoPagamentoRepository = tipoPagamentoRepository;
        }    

        protected virtual void RegistrarAdministrador(Usuario obj)
        {
            obj.Perfil = new Perfil(PerfilEnum.ADMIN);
            
            usuarioService.RegistrarComSenhaDefault(obj);
        }

        protected override IQueryable<Estacionamento> GetActiveItems()
        {
            var query = repository.Items.Where(x => x.SituacaoRegistro == SituacaoRegistroEnum.ATIVO);            

            if (usuarioLogado.TemPerfil(PerfilEnum.ADMIN))
            {
                query = query.Where(estacionamento => estacionamento.Usuario.Id == usuarioLogado.Id);
            }
            else if (usuarioLogado.TemPerfil(PerfilEnum.FUNCIONARIO))
            {
                var funcionario = funcionarioService.GetFuncionarioByUsuario(usuarioLogado);

                query = query.Where(estacionamento => estacionamento.Id == funcionario.Estacionamento.Id);
            }

            return query;
        }

        public override void Add(Estacionamento obj)
        {
            repository.ExecuteTransaction(() => 
            {
                obj.Usuario = usuarioService.GetByEmail(obj.Email);

                if (obj.Usuario == null)
                {
                    obj.Usuario = new Usuario() 
                    {
                        Email = obj.Email,
                        NomeUsuario = obj.RazaoSocial
                    };

                    RegistrarAdministrador(obj.Usuario);
                }
                else if (obj.Usuario.SituacaoRegistro == SituacaoRegistroEnum.INATIVO)
                {
                    usuarioService.ResetSenha(obj.Usuario);
                }

                base.Add(obj);
            });
        }

        public override void Update(Estacionamento estacionamento)
        {
            var usuarioAntigo = GetAdministradorById(estacionamento.Id);

            estacionamento.Usuario = usuarioService.GetByEmail(estacionamento.Email);

            repository.ExecuteTransaction(() => 
            {
                if (estacionamento.Usuario == null)
                {
                    estacionamento.Usuario = new Usuario()
                    {
                        Email = estacionamento.Email,
                        NomeUsuario = estacionamento.RazaoSocial
                    };

                    RegistrarAdministrador(estacionamento.Usuario);
                }
                else if (estacionamento.Usuario.SituacaoRegistro == SituacaoRegistroEnum.INATIVO)
                {
                    estacionamento.Usuario.NomeUsuario = estacionamento.RazaoSocial;
                    usuarioService.ResetSenha(estacionamento.Usuario);
                }
                else if (!estacionamento.Usuario.NomeUsuario.Equals(estacionamento.RazaoSocial))
                {
                    estacionamento.Usuario.NomeUsuario = estacionamento.RazaoSocial;
                    usuarioService.Update(estacionamento.Usuario);
                }

                base.Update(estacionamento);

                //Caso o usuário tenha sido alterado, remove o antigo

                bool alterouUsuario = estacionamento.Usuario.Id != usuarioAntigo.Id;

                if (alterouUsuario && PossoExcluirUsuarioAntigo(estacionamento, usuarioAntigo)) usuarioService.Remove(usuarioAntigo);
            });
        }

        protected virtual bool PossoExcluirUsuarioAntigo(Estacionamento estacionamento, Usuario usuarioAntigo)
        {
            var administraOutrosEstacionamentos = repository.Items.Any(x => x.Usuario.Id == usuarioAntigo.Id);

            // Só posso excluir o usuario caso ele nao administre outros estacionamentos.
            return !administraOutrosEstacionamentos && usuarioAntigo.TemPerfil(PerfilEnum.ADMIN); 
        }

        protected virtual Usuario GetAdministradorById(int estacionamentoId)
        {
            return this.GetById(estacionamentoId).Usuario;
        }
        
        public virtual Usuario VerficaLogin(string login)
        {
            var usuario = usuarioService.GetByEmail(login);

            if (usuario != null && !usuario.TemPerfil(PerfilEnum.ADMIN)) throw new UnauthorizedAccessException("O login informado não possui perfil de administrador.");

            return usuario;
        }

        public IEnumerable<TipoPagamento> GetListTipoPagamento(int estacionamentoId)
        {
            return tipoPagamentoRepository.Items;
        }

        public override void Remove(Estacionamento obj)
        {
            obj.Usuario = GetAdministradorById(obj.Id);
            obj.Usuario.SituacaoRegistro = SituacaoRegistroEnum.INATIVO;

            repository.ExecuteTransaction(() => 
            {
                usuarioService.Update(obj.Usuario);

                base.Remove(obj);
            });
        }
    }

}
