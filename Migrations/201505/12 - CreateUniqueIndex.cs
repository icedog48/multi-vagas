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
    [Migration(201505241802, "Adiciona uma constraint unique às colunas Email da tabela Cliente e Login da tabela Usuario")]
    public class CreateUniqueIndex : Migration
    {
        public override void Up()
        {
            Alter.Table("Usuario").AlterColumn("Email").AsString(45).NotNullable().Unique("UQ_Email_Usuario");
            
            Alter.Table("Cliente").AlterColumn("Email").AsString(255).NotNullable().Unique("UQ_Email_Cliente");
            Alter.Table("Cliente").AlterColumn("CPF").AsString(11).NotNullable().Unique("UQ_CPF_Cliente");
            
            Alter.Table("Estacionamento").AlterColumn("CNPJ").AsString(14).NotNullable().Unique("UQ_CNPJ_Estacionamento");

            Alter.Table("Funcionario").AlterColumn("CPF").AsString(11).NotNullable().Unique("UQ_CPF_Funcionario");
            Alter.Table("Funcionario").AlterColumn("Matricula").AsString(10).NotNullable().Unique("UQ_Matricula_Funcionario");
        }

        public override void Down()
        {
            Delete.Index("UQ_Email_Usuario");
            
            Delete.Index("UQ_Email_Cliente");
            Delete.Index("UQ_CPF_Cliente");

            Delete.Index("UQ_CNPJ_Estacionamento");

            Delete.Index("UQ_CPF_Funcionario");
            Delete.Index("UQ_Matricula_Funcionario");
        }
    }
}
