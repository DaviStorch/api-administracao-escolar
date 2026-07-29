using System.ComponentModel.DataAnnotations;

namespace Frontend.Models;

public class Professor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    [Display(Name = "Nome completo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A materia é obrigatória")]
    [StringLength(20, ErrorMessage = "A materia deve ter no máximo 20 caracteres")]
    [Display(Name = "Materia")]
    public string Materia { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;

}

public class ProfessorCreateViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    [Display(Name = "Nome completo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A materia é obrigatória")]
    [StringLength(20, ErrorMessage = "A materia deve ter no máximo 20 caracteres")]
    [Display(Name = "Materia")]
    public string Materia { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
}
 
public class ProfessorEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(150, ErrorMessage = "O nome deve ter no máximo 150 caracteres")]
    [Display(Name = "Nome completo")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
    [Display(Name = "E-mail")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A materia é obrigatória")]
    [StringLength(20, ErrorMessage = "A materia deve ter no máximo 20 caracteres")]
    [Display(Name = "Materia")]
    public string Materia { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
}