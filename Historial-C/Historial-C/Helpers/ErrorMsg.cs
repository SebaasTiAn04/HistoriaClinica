namespace Historial_C.Helpers
{
    public static class ErrorMsg
    {
        public const string MsgRequired = "{0} es requerido.";
        public const string MsgUserPassword = "La contrasenia no puede ser la misma que el nombre del usuario.";
        public const string MsgDigitsMinPassword = "La contrasenia debe tener al menos {1} caracteres.";
        public const string MsgDigitsMaxPassword = "La contrasenia no puede tener mas de {1} caracteres.";
        public const string MsgDigitsConsecutivePassword = "La contrasenia no puede contener numeros consecutivos.";
        public const string MsgDigitsMin = "{0} debe tener al menos {1} caracteres.";
        public const string MsgDigitsMax = "{0} no puede tener mas de {1} caracteres.";
        public const string MsgRange = "{0} debe estar comprendido entre {2} y {1} caracteres";
        public const string MsgFormatEmail = "{0} no es un correo electronico valido.";
        public const string MsgFormatPhone = "{0} no es un telefono valido}.";
        public const string MsgFecha = "La fecha {0} debe ser entre {1} y {2}.";
        public const string PassMissmatch = "El campo {0} no coincide";
    }
}
