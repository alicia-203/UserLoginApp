using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserLoginApp.Data.Context;
using UserLoginApp.Models;

namespace userLoginApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Usuario/Login.cshtml", new LoginDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Usuario/Login.cshtml", model);
            }

            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == model.Usuario && u.Estatus);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View("~/Views/Usuario/Login.cshtml", model);
            }

            var passwordHasher = new PasswordHasher<object>();
            var result = passwordHasher.VerifyHashedPassword(null, usuario.Password, model.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("", "Usuario o contraseña incorrectos");
                return View("~/Views/Usuario/Login.cshtml", model);
            }

            HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);

            return RedirectToAction("Index", "Dashboard"); // Redirige al Dashboard
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
