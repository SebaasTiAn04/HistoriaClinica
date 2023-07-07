using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models
{
    public class Evolucion
    {
        public int Id { get; set; }

        public Medico Medico { get; set; }
        
        public int MedicoId { get; set; }

        public int EpisodioId { get; set; }
        public Episodio Episodio { get; set; }


        public DateTime FechaYHoraInicio { get;  set; } = DateTime.Now;

        public DateTime FechaYHoraAlta { get; set; } 

        public DateTime FechaYHoraCierre { get; set; } 



        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        public string DescripcionAtencion { get; set; }


        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public Boolean EstadoAbierto { get; set; }
               
        public List<Nota> Notas { get; set; }


    }
}