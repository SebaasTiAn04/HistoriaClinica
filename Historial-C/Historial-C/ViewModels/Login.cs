using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Historial_C.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(150, MinimumLength = 6, ErrorMessage = ErrorMsg.MsgRange)]
        [Display(Name = "Correo Electronico")]
        [EmailAddress(ErrorMessage = ErrorMsg.MsgFormatEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        public bool Recordarme { get; set; }

    }
}
