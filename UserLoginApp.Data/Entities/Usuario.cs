using System.ComponentModel.DataAnnotations;

namespace UserLoginApp.Data.Entities
{
  public class Usuario
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string NombreCompleto { get; set; }

    [Required]
    [StringLength(50)]
    public string NombreUsuario { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Correo { get; set; }

    [Required]
    public bool Estatus { get; set; }

    [Required]
    public DateTime FechaAlta { get; set; }

    public DateTime? FechaModificacion { get; set; }
  }
}
