namespace UserLoginApp.Controllers;

using Microsoft.AspNetCore.Mvc;
using UserLoginApp.Data.Context;
using UserLoginApp.Models;

public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new DashboardDto
        {
            TotalUsuarios = _context.Usuarios.Count(),
            UsuariosActivos = _context.Usuarios.Count(x => x.Estatus),
            UsuariosInactivos = _context.Usuarios.Count(x => !x.Estatus)
        };

        return View("~/Views/Usuario/Dashboard.cshtml", model);
    }
}
