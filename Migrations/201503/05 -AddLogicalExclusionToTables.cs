using FluentMigrator;
using Model;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201503
{
    [Migration(201503211115, "Adição do campo SituacaoRegistro as tabelas existentes")]
    public class AddLogicalExclusionToTables : Migration
    {
        public override void Up()
        {
            Alter.Table("CategoriaVaga").AddColumn("SituacaoRegistro").AsInt32().NotNullable().WithDefaultValue((int)SituacaoRegistroEnum.ATIVO);
            Alter.Table("Estacionamento").AddColumn("SituacaoRegistro").AsInt32().NotNullable().WithDefaultValue((int)SituacaoRegistroEnum.ATIVO);
            Alter.Table("Funcionario").AddColumn("SituacaoRegistro").AsInt32().NotNullable().WithDefaultValue((int)SituacaoRegistroEnum.ATIVO);
            Alter.Table("Usuario").AddColumn("SituacaoRegistro").AsInt32().NotNullable().WithDefaultValue((int)SituacaoRegistroEnum.ATIVO); 
        }

        public override void Down()
        {
            Delete.Column("SituacaoRegistro").FromTable("CategoriaVaga");
            Delete.Column("SituacaoRegistro").FromTable("Estacionamento");
            Delete.Column("SituacaoRegistro").FromTable("Funcionario");
            Delete.Column("SituacaoRegistro").FromTable("Usuario");
        }
    }
}
