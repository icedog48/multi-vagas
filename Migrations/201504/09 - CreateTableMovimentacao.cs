using FluentMigrator;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201504
{
    [Migration(201504231640, "Criação da tabela de movimentação")]
    public class CreateTableMovimentacao : Migration
    {
        public override void Up()
        {
            Create.Table("TipoPagamento")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Descricao").AsString(255).NotNullable()
                .WithColumn("Estacionamento_Id").AsInt32().NotNullable().ForeignKey("Estacionamento", "Id");

            Create.Table("Cliente")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Nome").AsString(255).NotNullable()
                .WithColumn("CPF").AsString(11).NotNullable()
                .WithColumn("Telefone").AsString(11).NotNullable()
                .WithColumn("Email").AsString(255).NotNullable()
                .WithColumn("SituacaoRegistro").AsInt32().NotNullable().WithDefaultValue((int)SituacaoRegistroEnum.ATIVO);
                ;

            Create.Table("Movimentacao")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Ticket").AsString(14).NotNullable()
                .WithColumn("Entrada").AsDateTime().NotNullable()
                .WithColumn("Vaga_Id").AsInt32().NotNullable().ForeignKey("Vaga", "Id")
                .WithColumn("Funcionario_Entrada_Id").AsInt32().NotNullable().ForeignKey("Funcionario", "Id")
                .WithColumn("Funcionario_Saida_Id").AsInt32().Nullable().ForeignKey("Funcionario", "Id")
                .WithColumn("Cliente_Id").AsInt32().Nullable().ForeignKey("Cliente", "Id")
                .WithColumn("Placa").AsString(10).NotNullable()
                .WithColumn("TipoPagamento_Id").AsInt32().Nullable().ForeignKey("TipoPagamento", "Id")
                .WithColumn("Saida").AsDateTime().Nullable()
                .WithColumn("ValorPago").AsCurrency().Nullable()
                .WithColumn("SituacaoRegistro").AsInt32().NotNullable().WithDefaultValue((int)SituacaoRegistroEnum.ATIVO);
                ;
        }

        public override void Down()
        {
            Delete.Table("Movimentacao");   
        }
    }
}
