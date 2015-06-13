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
    [Migration(201506131542, "Retira o indice Unique da coluna CPF da tabela de Funcionarios")]
    public class AlterFuncionario : Migration
    {
        public override void Up()
        {
            Delete.Index("UQ_CPF_Funcionario").OnTable("Funcionario");
        }

        public override void Down()
        {
            
        }
    }
}
