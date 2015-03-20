using Model;
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
    public class AdminService : CRUDService<Usuario>, IAdminService
    {
        public AdminService(IRepository<Usuario> repository, AdminValidator validator)
            : base(repository, validator)
        {
           
        }

        public override void Add(Usuario obj)
        {
            obj.Senha = Encryption.Encrypt(obj.Senha);
            obj.Perfil = new Perfil() { Id = (int)PerfilEnum.ADMIN };

            base.Add(obj);
        }

        public override void Update(Usuario obj)
        {
            obj.Senha = Encryption.Encrypt(obj.Senha);

            base.Update(obj);
        }
    }
}
