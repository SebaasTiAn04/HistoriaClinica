using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models
{
    public class Empleado : Persona
    {
        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [MinLength(4, ErrorMessage = ErrorMsg.MsgDigitsMin)]
        [MaxLength(20, ErrorMessage = ErrorMsg.MsgDigitsMax)]
        public string Legajo { get; set; }

        
        public List<Nota> Notas { get; set; }

       
    }
}
