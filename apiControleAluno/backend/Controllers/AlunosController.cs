using Microsoft.AspNetCore.Mvc;
using apiControleAluno.Models;
using apiControleAluno.Repositories;

namespace apiControleAluno.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunosController : ControllerBase
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunosController(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    [HttpGet("hello")]
    public ActionResult<string> Hello()
    {
        return Ok("Hello World! 👋 API de Controle de Alunos está funcionando com MySQL!");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
    {
        var alunos = await _alunoRepository.GetAllAsync();
        return Ok(alunos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Aluno>> GetAluno(int id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);
        if (aluno == null)
        {
            return NotFound($"Aluno com ID {id} não encontrado");
        }
        return Ok(aluno);
    }

    [HttpPost]
    public async Task<ActionResult<Aluno>> CreateAluno(Aluno aluno)
    {
        // Check if email already exists
        var existingEmail = await _alunoRepository.GetByEmailAsync(aluno.Email);
        if (existingEmail != null)
        {
            return BadRequest($"E-mail {aluno.Email} já cadastrado");
        }

        // Check if matricula already exists
        var existingMatricula = await _alunoRepository.GetByMatriculaAsync(aluno.Matricula);
        if (existingMatricula != null)
        {
            return BadRequest($"Matrícula {aluno.Matricula} já cadastrada");
        }

        var createdAluno = await _alunoRepository.AddAsync(aluno);
        return CreatedAtAction(nameof(GetAluno), new { id = createdAluno.Id }, createdAluno);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Aluno>> UpdateAluno(int id, Aluno aluno)
    {
        if (id != aluno.Id)
        {
            return BadRequest("ID do aluno não corresponde");
        }

        if (!await _alunoRepository.ExistsAsync(id))
        {
            return NotFound($"Aluno com ID {id} não encontrado");
        }

        // Check if email already exists for another student
        var existingEmail = await _alunoRepository.GetByEmailAsync(aluno.Email);
        if (existingEmail != null && existingEmail.Id != id)
        {
            return BadRequest($"E-mail {aluno.Email} já cadastrado para outro aluno");
        }

        // Check if matricula already exists for another student
        var existingMatricula = await _alunoRepository.GetByMatriculaAsync(aluno.Matricula);
        if (existingMatricula != null && existingMatricula.Id != id)
        {
            return BadRequest($"Matrícula {aluno.Matricula} já cadastrada para outro aluno");
        }

        var updatedAluno = await _alunoRepository.UpdateAsync(aluno);
        return Ok(updatedAluno);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAluno(int id)
    {
        if (!await _alunoRepository.ExistsAsync(id))
        {
            return NotFound($"Aluno com ID {id} não encontrado");
        }

        await _alunoRepository.DeleteAsync(id);
        return NoContent();
    }
}