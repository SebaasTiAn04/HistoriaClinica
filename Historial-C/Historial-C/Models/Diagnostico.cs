using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Historial_C.Models { 

    public class Diagnostico  {

        public int Id { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public int EpicrisisId { get; set; }
        public Epicrisis Epicrisis { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        public string Recomendacion { get; set; }
    }
}
