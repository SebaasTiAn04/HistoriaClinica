using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models
{
    public class Medico : Empleado
    {
        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [MinLength(6, ErrorMessage = ErrorMsg.MsgDigitsMin)]
        [MaxLength(10, ErrorMessage = ErrorMsg.MsgDigitsMax)]
        public string Matricula { get; set; }

        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }


        public List<Evolucion> Evoluciones { get; set; }
    }
}
