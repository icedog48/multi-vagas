using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations._201502
{
    [Migration(201502261754, "Criação da tabela de estacionamento")]
    public class CreateTableEstacionamento : Migration
    {
        public override void Up()
        {
            Create.Table("Estacionamento")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("CNPJ").AsString(10).NotNullable()
                .WithColumn("RazaoSocial").AsString(255).NotNullable()
                .WithColumn("Vagas").AsInt32().NotNullable()
                .WithColumn("Telefone").AsString(10).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("Logradouro").AsString(255).NotNullable()
                .WithColumn("Bairro").AsString(255).NotNullable()
                .WithColumn("UF").AsString(3).NotNullable()
                .WithColumn("Cidade").AsString(255).NotNullable()
                .WithColumn("CEP").AsString(8).NotNullable()
                .WithColumn("ConfirmaSaida").AsBoolean().NotNullable()
                .WithColumn("PermiteReserva").AsBoolean().NotNullable()
            ;
        }

        public override void Down()
        {
            Delete.Table("Estacionamento");
        }
    }
}
