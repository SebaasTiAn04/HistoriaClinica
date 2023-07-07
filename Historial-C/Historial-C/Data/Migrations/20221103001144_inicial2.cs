using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historial_C.Data.Migrations
{
    public partial class inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Personas_EmpleadoRegistraId",
                table: "Episodio");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Personas_PacienteId",
                table: "Episodio");

            migrationBuilder.DropIndex(
                name: "IX_Episodio_EmpleadoRegistraId",
                table: "Episodio");

            migrationBuilder.DropColumn(
                name: "EvolucionId",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "NotaId",
                table: "Evolucion");

            migrationBuilder.DropColumn(
                name: "EmpleadoRegistraId",
                table: "Episodio");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAlta",
                table: "Personas",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraAlta",
                table: "Evolucion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraCierre",
                table: "Evolucion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraInicio",
                table: "Evolucion",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Episodio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Episodio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraAlta",
                table: "Episodio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraCierre",
                table: "Episodio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHoraInicio",
                table: "Episodio",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaYHora",
                table: "Epicrisis",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Episodio_EmpleadoId",
                table: "Episodio",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Epicrisis_MedicoId",
                table: "Epicrisis",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Epicrisis_Personas_MedicoId",
                table: "Epicrisis",
                column: "MedicoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodio_Personas_EmpleadoId",
                table: "Episodio",
                column: "EmpleadoId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Episodio_Personas_PacienteId",
                table: "Episodio",
                column: "PacienteId",
                principalTable: "Personas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Epicrisis_Personas_MedicoId",
                table: "Epicrisis");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Personas_EmpleadoId",
                table: "Episodio");

            migrationBuilder.DropForeignKey(
                name: "FK_Episodio_Personas_PacienteId",
                table: "Episodio");

            migrationBuilder.DropIndex(
                name: "IX_Episodio_EmpleadoId",
                table: "Episodio");

            migrationBuilder.DropIndex(
                name: "IX_Epicrisis_MedicoId",
                table: "Epicrisis");

            migrationBuilder.DropColumn(
                name: "FechaAlta",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "FechaYHoraAlta",
                table: "Evolucion");

            migrationBuilder.DropColumn(
                name: "FechaYHoraCierre",
                table: "Evolucion");

            migrationBuilder.DropColumn(
                name: "FechaYHoraInicio",
                table: "Evolucion");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Episodio");

            migrationBuilder.DropColumn(
                name: "FechaYHoraAlta",
                table: "Episodio");

            migrationBuilder.DropColumn(
                name: "FechaYHoraCierre",
                table: "Episodio");

            migrationBuilder.DropColumn(
                name: "FechaYHoraInicio",
                table: "Episodio");

            migrationBuilder.DropColumn(
                name: "FechaYHora",
                table: "Epicrisis");

            migrationBuilder.AddColumn<int>(
                name: "EvolucionId",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotaId",
                table: "Evolucion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PacienteId",
                table: "Episodio",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoRegistraId",
                table: "Episodio",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodio_EmpleadoRegistraId",
                table: "Episodio",
                column: "EmpleadoRegistraId");

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
        }
    }
}
