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
    [Migration(201505302106, "Insere os valores default para a tabela tipo de pagamento")]
    public class InsertIntoTipoPagamento : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("TipoPagamento").Row(new { Descricao = "Dinheiro" });
            Insert.IntoTable("TipoPagamento").Row(new { Descricao = "Cartão de débito" });
            Insert.IntoTable("TipoPagamento").Row(new { Descricao = "Cartão de crédito" });
        }

        public override void Down()
        {
            Delete.FromTable("TipoPagamento").AllRows();
        }
    }
}
