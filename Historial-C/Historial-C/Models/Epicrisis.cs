using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models {

    public class Epicrisis {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public int MedicoId { get; set; }
        public Medico Medico{ get; set; }

        public DateTime FechaYHora { get; set; } = DateTime.Now;
                
        public int EpisodioId { get; set; }    

        public Episodio Episodio { get; set; }

        public Diagnostico Diagnostico { get; set; }
    }
}