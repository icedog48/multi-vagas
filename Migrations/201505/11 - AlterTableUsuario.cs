using FluentMigrator;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201505
{
    [Migration(201505241050, "Adição das colunas AlterarSenha na tabela de Usuario e Usuario_Id na tabela Cliente")]
    public class AlterTableUsuario : Migration
    {
        public override void Up()
        {
            Alter.Table("Usuario").AddColumn("AlterarSenha").AsBoolean().NotNullable().WithDefaultValue(true);
            
            Alter.Table("Cliente").AddColumn("Usuario_Id").AsInt32().NotNullable().ForeignKey("Usuario", "Id");

            Alter.Table("Cliente").AlterColumn("Telefone").AsString(11).NotNullable();
        }

        public override void Down()
        {
            Delete.Column("AlterarSenha").FromTable("Usuario");

            Delete.Column("Usuario_Id").FromTable("Cliente");
        }
    }
}
