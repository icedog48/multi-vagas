using FluentMigrator;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201502
{
    [Migration(201505042323, "Criação da tabela de movimentação")]
    public class AlterTableMovimentacao : Migration
    {
        public override void Up()
        {
            Alter.Column("Vaga_Id").OnTable("Movimentacao").AsInt32().Nullable();

            Alter.Column("Placa").OnTable("Movimentacao").AsString(10).Nullable();
        }

        public override void Down()
        {
            Alter.Column("Vaga_Id").OnTable("Movimentacao").AsInt32().NotNullable();

            Alter.Column("Placa").OnTable("Movimentacao").AsString(10).NotNullable();
        }
    }
}
