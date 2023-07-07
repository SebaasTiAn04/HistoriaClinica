using Historial_C.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Historial_C.ViewModels
{
    public class RegistroUsuario 
    {
        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ErrorMsg.MsgRange)]
        public string Apellido { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(15, MinimumLength = 7, ErrorMessage = ErrorMsg.MsgRange)]
        public string Dni { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(20, MinimumLength = 7, ErrorMessage = ErrorMsg.MsgRange)]
        //[Phone(ErrorMessage = ErrorMsg.MsgFormatPhone)]    (tiraba error al querer crear una persona o cualquier entidad hereditaria de esa misma como (Empleado, Medico, Paciente))
        public string Telefono { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = ErrorMsg.MsgRange)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [StringLength(150, MinimumLength = 6, ErrorMessage = ErrorMsg.MsgRange)]
        [Display(Name = "Correo Electronico")]
        [EmailAddress(ErrorMessage = ErrorMsg.MsgFormatEmail)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrorMsg.MsgRequired)]
        [DataType(DataType.Password)]
        [Display(Name = "confirmar contraseña")]
        [Compare("Password", ErrorMessage = ErrorMsg.PassMissmatch)]
        public string confirmacionPassword { get; set; }
    }
}
