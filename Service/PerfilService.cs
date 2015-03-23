using Model;
using Model.Common;
using Service.Common;
using Service.Common.Interfaces;
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
    public class PerfilService : IPerfilService
    {
        private IRepository<Perfil> repository;

        public PerfilService
            (
                IRepository<Perfil> repository
            )            
        {
            this.repository = repository;
        }

        public Perfil GetById(int id)
        {
            return repository.Get(id);
        }

        public IEnumerable<Perfil> GetAll()
        {
            return repository.Items;
        }

        public IEnumerable<Perfil> GetPerfisFuncionario() 
        {
            var admin = (int)PerfilEnum.ADMIN;

            var funcionario = (int)PerfilEnum.FUNCIONARIO;

            return repository.Items.Where(x => x.Id == admin || x.Id == funcionario);
        }
    }

}
