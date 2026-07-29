using System.ComponentModel.DataAnnotations;

namespace apiControleAluno.Models;

public class Professor
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Materia { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
}