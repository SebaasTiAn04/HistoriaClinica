using Historial_C.Helpers;
using Historial_C.Models;
using Historial_C.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace Historial_C.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Persona> _userManager;
        private SignInManager<Persona> _SignInManager;


        public AccountController(UserManager<Persona> userManager, SignInManager<Persona> signInManager)
        {
            this._userManager = userManager;
            this._SignInManager = signInManager;
        }

        

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([Bind("Nombre", "Apellido", "Dni", "Telefono", "Direccion", "Email", "Password", "confirmacionPassword")] RegistroUsuario model)
        //public async Task<IActionResult> Registrar([Bind("Email", "Password", "confirmacionPassword")] RegistroUsuario model)
        {
            if (ModelState.IsValid)
            {
                //Registracion
                Persona personaACrear = new Persona()
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Dni = model.Dni,
                    Telefono = model.Telefono,
                    Direccion = model.Direccion,
                    Email = model.Email,
                    UserName = model.Email
                };

                //Usamos el metodo CreateAsyng de UserManager
                //y a su vez le damos un tratamiento a la password guardandola en la propiedad passwordHasher de _userManager
                var resultadoCreate = await _userManager.CreateAsync(personaACrear, model.Password);


                //Si pudo crear a la persona 
                if (resultadoCreate.Succeeded)
                {
                    //le asigno el rol usuario normal
                    var resultadAddRole = await _userManager.AddToRoleAsync(personaACrear, Configs.UsuarioRolName);

                    if (resultadAddRole.Succeeded)
                    {
                        await _SignInManager.SignInAsync(personaACrear, isPersistent: false);

                        //Al terminar de registrarse redireccionaremos a
                        //la persona a llenar su formulario para terminar de completar sus datos
                        return RedirectToAction("Edit", "Personas", new { id = personaACrear.Id });
                    }

                    else
                    {
                        ModelState.AddModelError(String.Empty, $"No se pudo agregar el rol de {Configs.UsuarioRolName}");
                    }
                    //Al terminar de registrarse redireccionaremos a
                    //la persona a llenar su formulario para terminar de completar sus datos
                    return RedirectToAction("Edit", "Personas", new { id = personaACrear.Id});
                }

                //Si hubo un inconveniente al crear
                foreach(var error in resultadoCreate.Errors)
                {
                    //Errores al momento de crear
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult IniciarSesion(string returnUrl)
        {
            TempData["ReturnUrl"] = returnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(Login viewModel)
        {
            string returnUrl = TempData["ReturnUrl"] as string;


            if (ModelState.IsValid)
            {
                var resultado = await _SignInManager.PasswordSignInAsync(viewModel.Email,viewModel.Password,viewModel.Recordarme,false);

                if (resultado.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Inicio de sesion invalido");
            }

            
            return View(viewModel);
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccesoDenegado(string returnURL) {
            ViewBag.ReturnURL = returnURL;
            return View();
        }

    }
}
