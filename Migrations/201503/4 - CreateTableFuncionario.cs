using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201502
{
    [Migration(201503211100, "Criação da tabela de funcionarios")]
    public class CreateTableFuncionario : Migration
    {
        public override void Up()
        {
            Create.Table("Funcionario")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Estacionamento_Id").AsInt32().ForeignKey("Estacionamento", "Id").NotNullable()
                .WithColumn("Usuario_Id").AsInt32().ForeignKey("Usuario", "Id").NotNullable()
                .WithColumn("Matricula").AsString(10)
                .WithColumn("Nome").AsString(255)
                .WithColumn("Telefone").AsString(11)
                .WithColumn("Logradouro").AsString(255).NotNullable()
                .WithColumn("Bairro").AsString(255).NotNullable()
                .WithColumn("UF").AsString(3).NotNullable()
                .WithColumn("Cidade").AsString(255).NotNullable()
                .WithColumn("CEP").AsString(8).NotNullable()
                .WithColumn("CPF").AsString(11).NotNullable()
                .WithColumn("HoraInicio").AsTime().NotNullable()
                .WithColumn("HoraSaida").AsTime().NotNullable()
                .WithColumn("CargaHoraria").AsTime().NotNullable()
                .WithColumn("DataAdmissao").AsDate().NotNullable()
                .WithColumn("Salario").AsCurrency().NotNullable()
                .WithColumn("Obs").AsString().Nullable()
                ;
        }

        public override void Down()
        {
            Delete.Table("Funcionario");
        }
    }
}
