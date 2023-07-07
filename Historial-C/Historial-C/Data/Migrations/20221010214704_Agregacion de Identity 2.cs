using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historial_C.Data.Migrations
{
    public partial class AgregaciondeIdentity2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Persona_EmpleadoRegistraId",
                table: "Episodio");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Persona_PacienteId",
                table: "Episodio");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolucion_Persona_MedicoId",
                table: "Evolucion");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Persona_EmpleadoId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_Persona_Especialidad_EspecialidadId",
                table: "Persona");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persona",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Persona");

            migrationBuilder.RenameTable(
                name: "Persona",
                newName: "Personas");

            migrationBuilder.RenameIndex(
                name: "IX_Persona_EspecialidadId",
                table: "Personas",
                newName: "IX_Personas_EspecialidadId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Personas",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Personas",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Personas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Personas",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "Personas",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Personas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Personas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Personas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Personas",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Personas",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Personas",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Personas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Personas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Personas",
                table: "Personas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Personas_UserId",
                        column: x => x.UserId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Personas_UserId",
                        column: x => x.UserId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Personas_UserId",
                        column: x => x.UserId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonasRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PersonasRoles_Personas_UserId",
                        column: x => x.UserId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonasRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Personas",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Personas",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasRoles_RoleId",
                table: "PersonasRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodio_Personas_EmpleadoRegistraId",
                table: "Episodio",
                column: "EmpleadoRegistraId",
                principalTable: "Personas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodio_Personas_PacienteId",
                table: "Episodio",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolucion_Personas_MedicoId",
                table: "Evolucion",
                column: "MedicoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Personas_EmpleadoId",
                table: "Nota",
                column: "EmpleadoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Especialidad_EspecialidadId",
                table: "Personas",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Personas_EmpleadoRegistraId",
                table: "Episodio");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Personas_PacienteId",
                table: "Episodio");

            migrationBuilder.DropForeignKey(
                name: "FK_Evolucion_Personas_MedicoId",
                table: "Evolucion");

            migrationBuilder.DropForeignKey(
                name: "FK_Nota_Personas_EmpleadoId",
                table: "Nota");

            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Especialidad_EspecialidadId",
                table: "Personas");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PersonasRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Personas",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Personas");

            migrationBuilder.RenameTable(
                name: "Personas",
                newName: "Persona");

            migrationBuilder.RenameIndex(
                name: "IX_Personas_EspecialidadId",
                table: "Persona",
                newName: "IX_Persona_EspecialidadId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Persona",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Persona",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Persona",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Persona",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dni",
                table: "Persona",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Direccion",
                table: "Persona",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Apellido",
                table: "Persona",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Persona",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persona",
                table: "Persona",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodio_Persona_EmpleadoRegistraId",
                table: "Episodio",
                column: "EmpleadoRegistraId",
                principalTable: "Persona",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Episodio_Persona_PacienteId",
                table: "Episodio",
                column: "PacienteId",
                principalTable: "Persona",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evolucion_Persona_MedicoId",
                table: "Evolucion",
                column: "MedicoId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nota_Persona_EmpleadoId",
                table: "Nota",
                column: "EmpleadoId",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persona_Especialidad_EspecialidadId",
                table: "Persona",
                column: "EspecialidadId",
                principalTable: "Especialidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
