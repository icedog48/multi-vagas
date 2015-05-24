using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201503
{
    [Migration(201503171619, "Criação das tabelas de Vaga e Categoria de Vagas")]
    public class CreateTableVagas : Migration
    {
        public override void Up()
        {
            Create.Table("CategoriaVaga")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Estacionamento_Id").AsInt32().NotNullable()
                .WithColumn("Descricao").AsString(45).NotNullable()
                .WithColumn("ValorHora").AsCurrency().NotNullable()
                ;

            Create.ForeignKey("FK_CategoriaVaga_Estacionamento").FromTable("CategoriaVaga").ForeignColumn("Estacionamento_Id").ToTable("Estacionamento").PrimaryColumn("Id");

            Create.Table("Vaga")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("CategoriaVaga_Id").AsInt32().NotNullable()
                .WithColumn("Codigo").AsString(45).NotNullable()
                .WithColumn("Disponivel").AsBoolean().NotNullable()
                ;

            Create.ForeignKey("FK_Vaga_CategoriaVaga").FromTable("Vaga").ForeignColumn("CategoriaVaga_Id").ToTable("CategoriaVaga").PrimaryColumn("Id");            
        }

        public override void Down()
        {
            Delete.Table("Vaga");

            Delete.Table("CategoriaVaga");
        }
    }
}
