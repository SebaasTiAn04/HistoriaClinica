using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models
{
   public class Especialidad {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public string Nombre { get; set; } 
   }
}
