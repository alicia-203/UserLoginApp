using System.ComponentModel.DataAnnotations;

namespace UserLoginApp.Models;

public class LoginDto
{
    [Required(ErrorMessage = "El usuario es obligatorio")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; }
}
