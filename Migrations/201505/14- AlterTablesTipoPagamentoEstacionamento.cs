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
    [Migration(201505302043, "Cria a tabela de Reserva")]
    public class AlterTablesTipoPagamentoEstacionamento : Migration
    {
        public override void Up()
        {
            Delete.ForeignKey("FK_TipoPagamento_Estacionamento_Id_Estacionamento_Id").OnTable("TipoPagamento");

            Delete.Column("Estacionamento_Id").FromTable("TipoPagamento");

            Delete.Column("CargaHoraria").FromTable("Funcionario");
        }

        public override void Down()
        {
            Alter.Table("TipoPagamento").AddColumn("Estacionamento_Id").AsInt32().ForeignKey("Estacionamento", "Id");

            Alter.Table("Funcionario").AddColumn("CargaHoraria").AsTime();
        }
    }
}
