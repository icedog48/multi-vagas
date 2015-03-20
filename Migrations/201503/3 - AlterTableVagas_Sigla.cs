using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201502
{
    [Migration(201503191309, "Criação das tabelas de Vaga e Categoria de Vagas")]
    public class AlterTableVagas_Sigla : Migration
    {
        public override void Up()
        {
            Alter.Table("CategoriaVaga").AddColumn("Sigla").AsString(5);
        }

        public override void Down()
        {
            Alter.Table("CategoriaVaga").AlterColumn("Sigla");
        }
    }
}
