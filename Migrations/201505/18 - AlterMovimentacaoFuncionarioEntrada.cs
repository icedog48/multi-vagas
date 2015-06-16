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
    [Migration(201506152351, "Permite salvar nulo como funcionario de entrada")]
    public class AlterMovimentacaoFuncionarioEntrada : Migration
    {
        public override void Up()
        {
            Alter.Column("Funcionario_Entrada_Id").OnTable("Movimentacao").AsInt32().Nullable();
        }

        public override void Down()
        {
            
        }
    }
}
