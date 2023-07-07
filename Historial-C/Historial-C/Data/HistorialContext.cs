using Historial_C.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Historial_C.Data
{
    public class HistorialContext : IdentityDbContext<IdentityUser<int>,IdentityRole<int>, int>
    {

        public HistorialContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Establecer Nombres para los Identity Stores

            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");

            #endregion
        }
        public DbSet<Historial_C.Models.Diagnostico> Diagnostico { get; set; }

        public DbSet<Historial_C.Models.Empleado> Empleado { get; set; }

        public DbSet<Historial_C.Models.Epicrisis> Epicrisis { get; set; }

        public DbSet<Historial_C.Models.Episodio> Episodio { get; set; }

        public DbSet<Historial_C.Models.Especialidad> Especialidad { get; set; }

        public DbSet<Historial_C.Models.Evolucion> Evolucion { get; set; }

        public DbSet<Historial_C.Models.Medico> Medico { get; set; }

        public DbSet<Historial_C.Models.Nota> Nota { get; set; }

        public DbSet<Historial_C.Models.Paciente> Paciente { get; set; }

        public DbSet<Historial_C.Models.Persona> Persona { get; set; }

        public DbSet<Rol> Roles { get; set; }

        

    }
}
