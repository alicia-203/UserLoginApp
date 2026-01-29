using Microsoft.AspNetCore.Mvc;
using UserLoginApp.Business.Services;
using UserLoginApp.Data.Context;
using UserLoginApp.Data.Entities;
using UserLoginApp.Models;

namespace UserLoginApp.Controllers;

public class UsuarioController : Controller
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var usuarios = _context.Usuarios.ToList();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return PartialView("_UsuarioModal", new UsuarioDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(UsuarioDto model)
    {
        if (!ModelState.IsValid)
        return PartialView("_UsuarioModal", model);
    
        if (!SeguridadService.PasswordValida(model.Password))
        {
            ModelState.AddModelError("Password", "La contraseña no cumple las reglas");
            return PartialView("_UsuarioModal", model);
        }

        var usuario = new Usuario
        {
            NombreCompleto = model.NombreCompleto,
            NombreUsuario = model.NombreUsuario,
            Password = HashService.Hash(model.Password),
            Correo = model.Correo,
            Estatus = model.Estatus,
            FechaAlta = DateTime.Now
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Json(new { success = true, redirectUrl = Url.Action("Index", "Usuario"), message = "Usuario guardado correctamente" });
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario == null)
            return NotFound();

        var model = new UsuarioDto
        {
            Id = usuario.Id,
            NombreCompleto = usuario.NombreCompleto,
            NombreUsuario = usuario.NombreUsuario,
            Correo = usuario.Correo,
            Estatus = usuario.Estatus
        };

        return PartialView("_UsuarioModal", model);
    }


}

