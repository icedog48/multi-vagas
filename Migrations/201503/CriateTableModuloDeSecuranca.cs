using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations._201502
{
    [Migration(201503091345, "Criação das tabelas de securança")]
    public class CriateTableModuloDeSecuranca : Migration
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

            Create.Table("PermissaoPerfil")
                .WithColumn("Id_Perfil").AsInt32().NotNullable().PrimaryKey()
                .WithColumn("Id_Permissao").AsInt32().NotNullable().PrimaryKey()
                ;

            Create.ForeignKey("FK_PermissaoPerfil_Perfil").FromTable("PermissaoPerfil").ForeignColumn("Id_Perfil").ToTable("Perfil").PrimaryColumn("Id");
            Create.ForeignKey("FK_PermissaoPerfil_Permissao").FromTable("PermissaoPerfil").ForeignColumn("Id_Permissao").ToTable("Permissao").PrimaryColumn("Id");

            Insert.IntoTable("PermissaoPerfil").Row(new { Id_Perfil = 1, Id_Permissao = 1 });
            Insert.IntoTable("PermissaoPerfil").Row(new { Id_Perfil = 2, Id_Permissao = 2 });
            Insert.IntoTable("PermissaoPerfil").Row(new { Id_Perfil = 3, Id_Permissao = 3 });
            Insert.IntoTable("PermissaoPerfil").Row(new { Id_Perfil = 4, Id_Permissao = 4 });

            Create.Table("Usuario")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Email").AsString(45).NotNullable()
                .WithColumn("Senha").AsString(45).NotNullable()
                .WithColumn("Login").AsString(45).NotNullable()
                .WithColumn("Id_Perfil").AsInt32().NotNullable()
                ;

            Create.ForeignKey("FK_Usuario_Perfil").FromTable("Usuario").ForeignColumn("Id_Perfil").ToTable("Perfil").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Usuario");

            Delete.Table("PermissaoPerfil");

            Delete.Table("Perfil");

            Delete.Table("Permissao");
        }
    }
}
