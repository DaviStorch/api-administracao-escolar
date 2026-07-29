using System.ComponentModel.DataAnnotations;

namespace Frontend.Models;

public class Aluno
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

    [Required(ErrorMessage = "O curso é obrigatório")]
    [StringLength(100, ErrorMessage = "O curso deve ter no máximo 100 caracteres")]
    [Display(Name = "Curso")]
    public string Curso { get; set; } = string.Empty;

    [Required(ErrorMessage = "A matrícula é obrigatória")]
    [StringLength(20, ErrorMessage = "A matrícula deve ter no máximo 20 caracteres")]
    [Display(Name = "Matrícula")]
    public string Matricula { get; set; } = string.Empty;

    [Display(Name = "Criado em")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Atualizado em")]
    public DateTime UpdatedAt { get; set; }
}

public class AlunoCreateViewModel
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

    [Required(ErrorMessage = "O curso é obrigatório")]
    [StringLength(100, ErrorMessage = "O curso deve ter no máximo 100 caracteres")]
    [Display(Name = "Curso")]
    public string Curso { get; set; } = string.Empty;

    [Required(ErrorMessage = "A matrícula é obrigatória")]
    [StringLength(20, ErrorMessage = "A matrícula deve ter no máximo 20 caracteres")]
    [Display(Name = "Matrícula")]
    public string Matricula { get; set; } = string.Empty;
}

public class AlunoEditViewModel
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

    [Required(ErrorMessage = "O curso é obrigatório")]
    [StringLength(100, ErrorMessage = "O curso deve ter no máximo 100 caracteres")]
    [Display(Name = "Curso")]
    public string Curso { get; set; } = string.Empty;

    [Required(ErrorMessage = "A matrícula é obrigatória")]
    [StringLength(20, ErrorMessage = "A matrícula deve ter no máximo 20 caracteres")]
    [Display(Name = "Matrícula")]
    public string Matricula { get; set; } = string.Empty;
}