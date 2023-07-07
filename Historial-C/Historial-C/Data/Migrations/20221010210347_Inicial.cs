using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Historial_C.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Especialidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Dni = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legajo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Matricula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EspecialidadId = table.Column<int>(type: "int", nullable: true),
                    ObraSocial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EvolucionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_Especialidad_EspecialidadId",
                        column: x => x.EspecialidadId,
                        principalTable: "Especialidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoAbierto = table.Column<bool>(type: "bit", nullable: false),
                    EmpleadoRegistraId = table.Column<int>(type: "int", nullable: true),
                    PacienteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodio_Persona_EmpleadoRegistraId",
                        column: x => x.EmpleadoRegistraId,
                        principalTable: "Persona",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Episodio_Persona_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "Persona",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Epicrisis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    EpisodioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Epicrisis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Epicrisis_Episodio_EpisodioId",
                        column: x => x.EpisodioId,
                        principalTable: "Episodio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evolucion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    EpisodioId = table.Column<int>(type: "int", nullable: false),
                    DescripcionAtencion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoAbierto = table.Column<bool>(type: "bit", nullable: false),
                    NotaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evolucion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evolucion_Episodio_EpisodioId",
                        column: x => x.EpisodioId,
                        principalTable: "Episodio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evolucion_Persona_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpicrisisId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Recomendacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnostico_Epicrisis_EpicrisisId",
                        column: x => x.EpicrisisId,
                        principalTable: "Epicrisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nota",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    Mensaje = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EvolucionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nota_Evolucion_EvolucionId",
                        column: x => x.EvolucionId,
                        principalTable: "Evolucion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nota_Persona_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostico_EpicrisisId",
                table: "Diagnostico",
                column: "EpicrisisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Epicrisis_EpisodioId",
                table: "Epicrisis",
                column: "EpisodioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episodio_EmpleadoRegistraId",
                table: "Episodio",
                column: "EmpleadoRegistraId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodio_PacienteId",
                table: "Episodio",
                column: "PacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucion_EpisodioId",
                table: "Evolucion",
                column: "EpisodioId");

            migrationBuilder.CreateIndex(
                name: "IX_Evolucion_MedicoId",
                table: "Evolucion",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_EmpleadoId",
                table: "Nota",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nota_EvolucionId",
                table: "Nota",
                column: "EvolucionId");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_EspecialidadId",
                table: "Persona",
                column: "EspecialidadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Diagnostico");

            migrationBuilder.DropTable(
                name: "Nota");

            migrationBuilder.DropTable(
                name: "Epicrisis");

            migrationBuilder.DropTable(
                name: "Evolucion");

            migrationBuilder.DropTable(
                name: "Episodio");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Especialidad");
        }
    }
}
