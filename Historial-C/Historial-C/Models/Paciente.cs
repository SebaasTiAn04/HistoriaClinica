using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models
{
    public class Paciente:Persona
    {
        [Required(ErrorMessage =ErrorMsg.MsgRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        [Display(Name = "Obra Social")]
        public string ObraSocial { get; set; }
               
        public List<Episodio> Episodios { get; set; }

        //public int EvolucionId { get; set; } //relacional

        
    }
}
