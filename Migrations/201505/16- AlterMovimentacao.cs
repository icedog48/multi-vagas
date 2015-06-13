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
    [Migration(201506091659, "Insere os valores default para a tabela tipo de pagamento")]
    public class AlterMovimentacao : Migration
    {
        public override void Up()
        {
            Alter.Table("Movimentacao").AddColumn("Estadia").AsInt32().Nullable();
        }

        public override void Down()
        {
            
        }
    }
}
