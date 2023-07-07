using Historial_C.Data;
using Historial_C;
using Microsoft.EntityFrameworkCore;

namespace Historial_C
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var app = StartUp.InicializarApps(args);//Pasamos lops argumentos que son recividos en la ejecucion

            
            app.Run();
            
        }
    }
}