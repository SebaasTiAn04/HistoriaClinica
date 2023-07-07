using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models { 
    public class Episodio {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public string Motivo { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        public string Descripcion { get; set; }

        public DateTime FechaYHoraInicio { get; set; } = DateTime.Now;

        public DateTime FechaYHoraAlta { get; set; } 

        public DateTime FechaYHoraCierre { get; set; }

        public Boolean EstadoAbierto { get ; set ; }

        public List<Evolucion> Evoluciones { get; set; }

        public int EmpleadoId { get; set; }
        public Empleado EmpleadoRegistra { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public int PacienteId { get; set; }
        public Paciente Paciente { get; set; }


       
        public Epicrisis Epicrisis { get; set; }
    }
}