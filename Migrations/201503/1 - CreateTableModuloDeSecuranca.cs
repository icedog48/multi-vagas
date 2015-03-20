using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Extensions;

namespace Migrations._201502
{
    [Migration(201503091345, "Criação das tabelas de securança")]
    public class CreateTableModuloDeSecuranca : Migration
    {
        public override void Up()
        {
            Create.Table("Permissao")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(45).NotNullable()
                ;

            Insert.IntoTable("Permissao").Row(new { Nome = "equipe_multivagas" });
            Insert.IntoTable("Permissao").Row(new { Nome = "administrador" });
            Insert.IntoTable("Permissao").Row(new { Nome = "funcionario" });
            Insert.IntoTable("Permissao").Row(new { Nome = "usuario" });

            Create.Table("Perfil")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsString(45).NotNullable()
                ;

            Insert.IntoTable("Perfil").Row(new { Nome = "equipe_multivagas" });
            Insert.IntoTable("Perfil").Row(new { Nome = "administrador" });
            Insert.IntoTable("Perfil").Row(new { Nome = "funcionario" });
            Insert.IntoTable("Perfil").Row(new { Nome = "usuario" });

            Create.Table("PerfilPermissao")
                .WithColumn("Perfil_Id").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Permissao_Id").AsInt32().NotNullable().PrimaryKey()
                ;

            Create.ForeignKey("FK_PerfilPermissao_Perfil").FromTable("PerfilPermissao").ForeignColumn("Perfil_Id").ToTable("Perfil").PrimaryColumn("Id");
            Create.ForeignKey("FK_PerfilPermissao_Permissao").FromTable("PerfilPermissao").ForeignColumn("Permissao_Id").ToTable("Permissao").PrimaryColumn("Id");

            Insert.IntoTable("PerfilPermissao").Row(new { Perfil_Id = 1, Permissao_Id = 1 });
            Insert.IntoTable("PerfilPermissao").Row(new { Perfil_Id = 2, Permissao_Id = 2 });
            Insert.IntoTable("PerfilPermissao").Row(new { Perfil_Id = 3, Permissao_Id = 3 });
            Insert.IntoTable("PerfilPermissao").Row(new { Perfil_Id = 4, Permissao_Id = 4 });

            Create.Table("Usuario")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Email").AsString(45).NotNullable()
                .WithColumn("Senha").AsString(45).NotNullable()
                .WithColumn("Login").AsString(45).NotNullable()
                .WithColumn("Perfil_Id").AsInt32().NotNullable()
                ;

            Create.ForeignKey("FK_Usuario_Perfil").FromTable("Usuario").ForeignColumn("Perfil_Id").ToTable("Perfil").PrimaryColumn("Id");

            Insert.IntoTable("Usuario").Row(new { Email = "contact@multivagas.com", Senha = Encryption.Encrypt("multivagas"), Login = "admin", Perfil_Id = 1 });
        }

        public override void Down()
        {
            Delete.Table("Usuario");

            Delete.Table("PerfilPermissao");

            Delete.Table("Perfil");

            Delete.Table("Permissao");
        }
    }
}
