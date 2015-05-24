using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201503
{
    [Migration(201503271429, "Alteração da tabela de Estacionamento para adição da coluna Usuario_Id correspondente ao admnistrador do estacionamento")]
    public class AlterTableEstacionamentoAddUsuarioId : Migration
    {
        public override void Up()
        {
            Alter.Table("Estacionamento")
                .AddColumn("Usuario_Id")
                    .AsInt32().NotNullable()
                    .ForeignKey("Usuario", "Id")
                    .SetExistingRowsTo(1);
        }

        public override void Down()
        {
            Delete.Column("Usuario_Id").FromTable("Estacionamento");
        }
    }
}
