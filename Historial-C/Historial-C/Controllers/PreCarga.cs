using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Historial_C.Models;
using Historial_C.Data;
using Microsoft.EntityFrameworkCore;
using Historial_C.Helpers;
using Microsoft.Extensions.Primitives;

namespace Historial_C.Controllers
{
    public class PreCarga : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly HistorialContext _context;

        private readonly List<string> roles = new List<string>() { "Usuario", "Empleado", "Medico" };

        public PreCarga(UserManager<Persona> userManager, RoleManager<Rol> roleManager, HistorialContext context)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;


        }

        public async Task<IActionResult> Seed()
        {

            CrearEspecialidades().Wait();
            CrearRoles().Wait();
            CrearUsuario().Wait();
            CrearEmpleado().Wait();
            CrearMedico().Wait();
            CrearPaciente().Wait();
            crearEpisodio().Wait();//
            crearEvolucion().Wait();//
            crearEpicrisis().Wait();//
            crearDiagnostico().Wait();//
            crearNota().Wait();//

            return RedirectToAction("Index", "Home", new { mensaje = "Proceso Seed Finalizado" });
        }


        private async Task CrearRoles()
        {
            foreach (var rolName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(rolName))
                {
                    await _roleManager.CreateAsync(new Rol(rolName));
                }
            }
        }

        private async Task crearNota()
        {
            Nota nota1 = new Nota { EmpleadoId = 3, Mensaje = "Paciente indica que ya se siente mejor y manifiesta que quiere irse a su casa.", EvolucionId = 1 };
            nota1.Empleado = await _context.Empleado.FindAsync(nota1.EmpleadoId);
            nota1.Evolucion = await _context.Evolucion.FindAsync(nota1.EvolucionId);

            if (!NotaExists(nota1.Mensaje, nota1.EmpleadoId, nota1.EvolucionId))
            {
                _context.Nota.Add(nota1);
                await _context.SaveChangesAsync();
            }

            Nota nota2 = new Nota { EmpleadoId = 4, Mensaje = "Paciente indica que se cansó de esperar y se retira.", EvolucionId = 2 };
            nota2.Evolucion = await _context.Evolucion.FindAsync(nota2.EvolucionId);
            nota2.Empleado = await _context.Empleado.FindAsync(nota2.EmpleadoId);
            if (!NotaExists(nota2.Mensaje, nota2.EmpleadoId, nota2.EvolucionId))
            {
                _context.Nota.Add(nota2);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

        }

        private async Task crearDiagnostico()
        {
            Diagnostico diagnostico1 = new Diagnostico { EpicrisisId = 1, Descripcion = "Cortes en la frente", Recomendacion = "volver para sacar putos de corte en frente" };
            if (!DiagnosticoExists(diagnostico1.EpicrisisId, diagnostico1.Descripcion))
            {
                _context.Diagnostico.Add(diagnostico1);
                await _context.SaveChangesAsync();
            }


            Diagnostico diagnostico2 = new Diagnostico { EpicrisisId = 2, Descripcion = "Desgarre de ligamentos", Recomendacion = "Reposo 1 mes con yeso, volver transcurrido el mes" };
            if (!DiagnosticoExists(diagnostico2.EpicrisisId, diagnostico2.Descripcion))
            {
                _context.Diagnostico.Add(diagnostico2);
                await _context.SaveChangesAsync();
            }



        }

        private async Task crearEpicrisis()
        {
            Epicrisis epicrisis1 = new Epicrisis { MedicoId = 5, EpisodioId = 1};
            epicrisis1.Medico = await _context.Medico.FindAsync(epicrisis1.MedicoId);
            epicrisis1.Episodio = await _context.Episodio.FindAsync(epicrisis1.EpisodioId);
            if (!EpicrisisExists(epicrisis1.MedicoId, epicrisis1.EpisodioId, epicrisis1.FechaYHora))
            {
                _context.Epicrisis.Add(epicrisis1);
                await _context.SaveChangesAsync();

            }

            Epicrisis epicrisis2 = new Epicrisis { MedicoId = 6, EpisodioId = 2 };
            epicrisis2.Medico = await _context.Medico.FindAsync(epicrisis2.MedicoId);
            epicrisis2.Episodio = await _context.Episodio.FindAsync(epicrisis2.EpisodioId);
            if (!EpicrisisExists(epicrisis2.MedicoId, epicrisis2.EpisodioId, epicrisis2.FechaYHora))
            {
                _context.Epicrisis.Add(epicrisis2);
                await _context.SaveChangesAsync();

            }

        }

        private async Task crearEvolucion()
        {
            Evolucion evolucion1 = new Evolucion { MedicoId = 5, EpisodioId = 1, DescripcionAtencion = "Cirujia exitosa (union de ligamentos), se traslada a enyesar pierna", EstadoAbierto = true };
            evolucion1.Medico = await _context.Medico.FindAsync(evolucion1.MedicoId);
            evolucion1.Episodio = await _context.Episodio.FindAsync(evolucion1.EpisodioId);
            if (!EvolucionExists(evolucion1.MedicoId, evolucion1.EpisodioId, evolucion1.DescripcionAtencion))
            {
                _context.Evolucion.Add(evolucion1);
                await _context.SaveChangesAsync();
            }

            Evolucion evolucion2 = new Evolucion { MedicoId = 6, EpisodioId = 2, DescripcionAtencion = "corte en frente tratada con 7 puntos", EstadoAbierto = true };
            evolucion2.Medico = await _context.Medico.FindAsync(evolucion2.MedicoId);
            evolucion2.Episodio = await _context.Episodio.FindAsync(evolucion2.EpisodioId);
            if (!EvolucionExists(evolucion2.MedicoId, evolucion2.EpisodioId, evolucion2.DescripcionAtencion))
            {
                _context.Evolucion.Add(evolucion2);
                await _context.SaveChangesAsync();
            }



        }

        private async Task crearEpisodio()
        {
            Episodio episodio1 = new Episodio { Motivo = "Accidente de transito", Descripcion = "Cortes en la frente", EstadoAbierto = true, EmpleadoId = 3, PacienteId = 7 };
            episodio1.Paciente = await _context.Paciente.FindAsync(episodio1.PacienteId);
            episodio1.EmpleadoRegistra = await _context.Empleado.FindAsync(episodio1.EmpleadoId);
            if (!EpisodioExists(episodio1.Motivo, episodio1.Descripcion, episodio1.PacienteId, episodio1.EmpleadoId, episodio1.FechaYHoraInicio))
            {
                _context.Episodio.Add(episodio1);
                await _context.SaveChangesAsync();
            }


            Episodio episodio2 = new Episodio { Motivo = "Tobillo torcido mientras corria", Descripcion = "Desgarre de ligamentos", EstadoAbierto = true, EmpleadoId = 4, PacienteId = 8 };
            episodio2.Paciente = await _context.Paciente.FindAsync(episodio2.PacienteId);
            episodio2.EmpleadoRegistra = await _context.Empleado.FindAsync(episodio2.EmpleadoId);
            _context.Episodio.Add(episodio2);
            if (!EpisodioExists(episodio2.Motivo, episodio2.Descripcion, episodio2.PacienteId, episodio2.EmpleadoId, episodio2.FechaYHoraInicio))
            {
                _context.Episodio.Add(episodio2);
                await _context.SaveChangesAsync();
            }

        }

        private async Task CrearEspecialidades()
        {
            Especialidad especialidad1 = new Especialidad { Nombre = "Urologo" };
            if (!EspecialidadExists(especialidad1.Nombre))
            {
                _context.Especialidad.Add(especialidad1);
            }


            Especialidad especialidad2 = new Especialidad { Nombre = "Traumatologia" };
            if (!EspecialidadExists(especialidad2.Nombre))
            {
                _context.Especialidad.Add(especialidad2);
            }

            Especialidad especialidad3 = new Especialidad { Nombre = "Cardiologia" };
            if (!EspecialidadExists(especialidad3.Nombre))
            {
                _context.Especialidad.Add(especialidad3);
            }


            Especialidad especialidad4 = new Especialidad { Nombre = "Clinica Medica" };
            if (!EspecialidadExists(especialidad4.Nombre))
            {
                _context.Especialidad.Add(especialidad4);
            }

            Especialidad especialidad5 = new Especialidad { Nombre = "Neurologia" };
            if (!EspecialidadExists(especialidad5.Nombre))
            {
                _context.Especialidad.Add(especialidad5);
            }

            Especialidad especialidad6 = new Especialidad { Nombre = "Otorrinonaringologia" };
            if (!EspecialidadExists(especialidad6.Nombre))
            {
                _context.Especialidad.Add(especialidad6);
            }

            Especialidad especialidad7 = new Especialidad { Nombre = "Gastroenterologo" };
            if (!EspecialidadExists(especialidad6.Nombre))
            {
                _context.Especialidad.Add(especialidad7);
            }


            Especialidad especialidad8 = new Especialidad { Nombre = "Flebologia" };
            if (!EspecialidadExists(especialidad8.Nombre))
            {
                _context.Especialidad.Add(especialidad8);
            }

            Especialidad especialidad9 = new Especialidad { Nombre = "Dermatologia" };
            if (!EspecialidadExists(especialidad9.Nombre))
            {
                _context.Especialidad.Add(especialidad9);
            }

            Especialidad especialidad10 = new Especialidad { Nombre = "Pediatria" };
            if (!EspecialidadExists(especialidad9.Nombre))
            {
                _context.Especialidad.Add(especialidad10);
            }

            await _context.SaveChangesAsync();
        }


        private async Task CrearUsuario()
        {
            Persona usuario1 = new Persona
            {
                Nombre = "Maria",
                Apellido = "Gomez",
                Dni = "11114444",
                Telefono = "11222555",
                Direccion = "calle 102",
                Email = "Maria@ort.edu.ar",
                UserName = "Maria@ort.edu.ar"
            };


            if (!PersonaExists(usuario1.Dni))
            {
                var resultado1 = await _userManager.CreateAsync(usuario1, Configs.PasswordGenerica);
                if (resultado1.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario1, Configs.UsuarioRolName);
                }
            }

            Persona usuario2 = new Persona { Nombre = "Marta", Apellido = "Gaona", Dni = "22228888", Telefono = "11332244", Direccion = "calle 923", Email = "Marta@ort.edu.ar", UserName = "Marta@ort.edu.ar" };

            if (!PersonaExists(usuario2.Dni))
            {
                var resultado2 = await _userManager.CreateAsync(usuario2, Configs.PasswordGenerica);
                if (resultado2.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario2, Configs.UsuarioRolName);
                }
            }

        }


        private async Task CrearEmpleado()
        {
            Persona empleado1 = new Empleado { Legajo = "1000", Nombre = "Eduardo", Apellido = "Perez", Dni = "11111111", Telefono = "11222222", Direccion = "calle 444", Email = "eudardo@ort.edu.ar", UserName = "eudardo@ort.edu.ar" };



            if (!PersonaExists(empleado1.Dni))
            {
                var resultado1 = await _userManager.CreateAsync(empleado1, Configs.PasswordGenerica);
                if (resultado1.Succeeded)
                {
                    await _userManager.AddToRoleAsync(empleado1, Configs.EmpleadoRolName);
                }
            }

            Persona empleado2 = new Empleado { Legajo = "1001", Nombre = "Juan", Apellido = "Perez", Dni = "22222222", Telefono = "11333333", Direccion = "calle 555", Email = "juan@ort.edu.ar", UserName = "juan@ort.edu.ar" };

            if (!PersonaExists(empleado2.Dni))
            {
                var resultado2 = await _userManager.CreateAsync(empleado2, Configs.PasswordGenerica);
                if (resultado2.Succeeded)
                {
                    await _userManager.AddToRoleAsync(empleado2, Configs.EmpleadoRolName);
                }
            }

        }

        private async Task CrearMedico()
        {
            Persona medico1 = new Medico { Legajo = "1002", Matricula = "1111/224", Nombre = "Adrian", Apellido = "Diaz", Dni = "44444444", Telefono = "11222243", Direccion = "calle 934", Email = "adrian@ort.edu.ar", UserName = "adrian@ort.edu.ar", EspecialidadId = 1 };

            if (!PersonaExists(medico1.Dni))
            {
                var resultado1 = await _userManager.CreateAsync(medico1, Configs.PasswordGenerica);
                if (resultado1.Succeeded)
                {
                    await _userManager.AddToRoleAsync(medico1, Configs.MedicoRolName);
                }

            }


            Persona medico2 = new Medico { Legajo = "1003", Matricula = "2222/332", Nombre = "damian", Apellido = "Lopez", Dni = "55555555", Telefono = "11333852", Direccion = "calle 694", Email = "damian@ort.edu.ar", UserName = "damian@ort.edu.ar", EspecialidadId = 2 };

            if (!PersonaExists(medico2.Dni))
            {
                var resultado2 = await _userManager.CreateAsync(medico2, Configs.PasswordGenerica);
                if (resultado2.Succeeded)
                {
                    await _userManager.AddToRoleAsync(medico2, Configs.MedicoRolName);

                }
            }
        }

        private async Task CrearPaciente()
        {
            Persona paciente1 = new Paciente { Nombre = "Pablo", Apellido = "Pedrozo", Dni = "12722845", Telefono = "1123929433", Direccion = "Suipacha 123", Email = "pablo@ort.edu.ar", ObraSocial = "Omint", UserName = "pablo@ort.edu.ar", };

            if (!PersonaExists(paciente1.Dni))
            {
                var resultado1 = await _userManager.CreateAsync(paciente1, Configs.PasswordGenerica);
                if (resultado1.Succeeded)
                {
                    await _userManager.AddToRoleAsync(paciente1, Configs.UsuarioRolName);
                }

            }


            Persona paciente2 = new Paciente { Nombre = "Matias", Apellido = "Allioti", Dni = "23499455", Telefono = "1192348455", Direccion = "calle 694", Email = "matias@ort.edu.ar", ObraSocial = "Omint", UserName = "matias@ort.edu.ar" };

            if (!PersonaExists(paciente2.Dni))
            {
                var resultado2 = await _userManager.CreateAsync(paciente2, Configs.PasswordGenerica);
                if (resultado2.Succeeded)
                {
                    await _userManager.AddToRoleAsync(paciente2, Configs.UsuarioRolName);

                }
            }
        }

        private bool PersonaExists(string dni)
        {
            return _context.Persona.Any(p => p.Dni.Equals(dni));
        }

        private bool EspecialidadExists(string nombre)
        {
            return _context.Especialidad.Any(e => e.Nombre.Equals(nombre));
        }

        private bool NotaExists(string mensaje, int empleadoId, int evolucionId)
        {
            bool existe = false;

            Nota notaMensajeId = _context.Nota.FirstOrDefault(m => m.Mensaje.Equals(mensaje));
            Nota notaEmpleadoId = _context.Nota.FirstOrDefault(m => m.EmpleadoId == empleadoId);
            Nota notaEvolucionId = _context.Nota.FirstOrDefault(m => m.EvolucionId == evolucionId);
            if(notaMensajeId != null && notaEmpleadoId != null && notaEvolucionId!= null)
            {
                existe = true;
            }
            return existe;
        }

        private bool DiagnosticoExists(int epicrisisId, string descripcion)
        {
            bool existe = false;

            Diagnostico diagnosticoEpicrisisId = _context.Diagnostico.FirstOrDefault(m => m.EpicrisisId == epicrisisId);
            Diagnostico diagnosticoDescripcion = _context.Diagnostico.FirstOrDefault(m => m.Descripcion.Equals(descripcion));
            if (diagnosticoEpicrisisId != null && diagnosticoDescripcion != null )
            {
                existe = true;
            }
            return existe;
        }

        private bool EpicrisisExists(int medicoId, int episodioId, DateTime fecha)
        {
            bool existe = false;

            Epicrisis epicrisisMedicoId = _context.Epicrisis.FirstOrDefault(m => m.MedicoId == medicoId);
            Epicrisis epicrisisEpisodioId = _context.Epicrisis.FirstOrDefault(m => m.EpisodioId == episodioId);
            Epicrisis epicrisisFecha = _context.Epicrisis.FirstOrDefault(m => m.FechaYHora.Equals(fecha));
            if (epicrisisMedicoId != null && epicrisisEpisodioId != null && epicrisisFecha != null)
            {
                existe = true;
            }
            return existe;
        }
        private bool EvolucionExists(int medicoId, int episodioId, string descripcion)
        {
            bool existe = false;

            Evolucion evolucionMedicoId = _context.Evolucion.FirstOrDefault(m => m.MedicoId == medicoId);
            Evolucion evolucionEpisodioId = _context.Evolucion.FirstOrDefault(m => m.EpisodioId == episodioId);
            Evolucion evolucionFecha = _context.Evolucion.FirstOrDefault(m => m.DescripcionAtencion.Equals(descripcion));
            if (evolucionMedicoId != null && evolucionMedicoId != null && evolucionMedicoId != null)
            {
                existe = true;
            }
            return existe;
        }

        private bool EpisodioExists(string motivo, string descripcion, int pacienteId, int empleadoId, DateTime fecha)
        {
            bool existe = false;

            Episodio episodioMotivo = _context.Episodio.FirstOrDefault(m => m.Motivo.Equals(motivo));
            Episodio episodioDescripcion = _context.Episodio.FirstOrDefault(m => m.Descripcion.Equals(descripcion));
            Episodio episodioPacienteId = _context.Episodio.FirstOrDefault(m => m.PacienteId == pacienteId);
            Episodio episodioEmpleadoId = _context.Episodio.FirstOrDefault(m => m.EmpleadoId == empleadoId);
            Episodio episodioFecha = _context.Episodio.FirstOrDefault(m => m.FechaYHoraInicio.Equals(fecha));
            if (episodioMotivo != null && episodioDescripcion != null && episodioPacienteId != null && episodioEmpleadoId != null && episodioFecha != null)
            {
                existe = true;
            }
            return existe;
        }
    }

}