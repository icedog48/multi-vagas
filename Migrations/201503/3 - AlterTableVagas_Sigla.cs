using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201502
{
    [Migration(201503191309, "Adição do campo sigla a tabela CategoriaVaga")]
    public class AlterTableVagas_Sigla : Migration
    {
        public override void Up()
        {
            Alter.Table("CategoriaVaga").AddColumn("Sigla").AsString(5);
        }

        public override void Down()
        {
            Delete.Column("Sigla").FromTable("CategoriaVaga");
        }
    }
}
