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
    [Migration(201505260027, "Cria a tabela de Reserva")]
    public class CreateTableReserva : Migration
    {
        public override void Up()
        {
            Create.Table("Reserva")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Cliente_Id").AsInt32().ForeignKey("Cliente", "Id").NotNullable()
                .WithColumn("Vaga_Id").AsInt32().ForeignKey("Vaga", "Id").NotNullable()
                .WithColumn("Data").AsDate().NotNullable()
                ;
        }

        public override void Down()
        {
            Delete.Table("Reserva");
        }
    }
}
