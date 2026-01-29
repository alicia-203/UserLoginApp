using System.ComponentModel.DataAnnotations;

namespace UserLoginApp.Models;

public class UsuarioDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre completo es obligatorio")]
    public string NombreCompleto { get; set; }

    [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
    public string NombreUsuario { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Correo inválido")]
    public string Correo { get; set; }

    [Required]
    public bool Estatus { get; set; }
}
