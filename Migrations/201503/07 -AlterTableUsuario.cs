using FluentMigrator;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201503
{
    [Migration(201503222336, "Alteracao do campo Senha na tabela de Usuarios para 255 caracteres")]
    public class AlterTableUsuario : Migration
    {
        public override void Up()
        {
            Alter.Table("Usuario").AlterColumn("Senha").AsString(255).NotNullable();
        }

        public override void Down()
        {
            
        }
    }
}
