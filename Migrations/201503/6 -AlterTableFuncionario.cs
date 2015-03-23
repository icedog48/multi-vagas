using FluentMigrator;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201502
{
    [Migration(201503220744, "Adição do campo CargaHoraria na tabela de funcionarios")]
    public class AlterTableFuncionario : Migration
    {
        public override void Up()
        {
            Alter.Table("CategoriaVaga").AddColumn("CargaHoraria").AsTime().NotNullable().WithDefaultValue(new TimeSpan(0,0,0));
        }

        public override void Down()
        {
            Delete.Column("CargaHoraria").FromTable("CategoriaVaga");
        }
    }
}
