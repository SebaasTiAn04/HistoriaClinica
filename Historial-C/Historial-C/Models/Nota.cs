using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models
{
	public class Nota
	{
        public int Id { get; set; }
         
        public int EmpleadoId { get; set; }//prop relacional

        public Empleado Empleado { get; set; } //navegacional

        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        public string Mensaje { get; set; }

        public DateTime FechaYHora { get;} = DateTime.Now;

        public int EvolucionId { get; set; } //prop relacional

        public Evolucion Evolucion { get; set; } //navegacional

        
    }
}