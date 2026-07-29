using Microsoft.AspNetCore.Mvc;
using apiControleAluno.Models; // <-- CORRETO: Usa o Model do Backend, não do Frontend
using apiControleAluno.Repositories;

namespace apiControleAluno.Controllers;

[ApiController] // <-- Indica que é uma API (retorna JSON, não HTML)
[Route("api/[controller]")] // <-- Define a rota base como /api/professores
public class ProfessoresController : ControllerBase // <-- APIs usam ControllerBase, não Controller
{
    private readonly IProfessorRepository _professorRepository;

    public ProfessoresController(IProfessorRepository professorRepository)
    {
        _professorRepository = professorRepository;
    }

    [HttpGet("hello")]
    public IActionResult Hello()
    {
        return Ok("Hello World! 👋 API de Controle de Professores está funcionando!");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Professor>>> GetProfessores()
    {
        var professores = await _professorRepository.GetAllAsync();
        return Ok(professores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Professor>> GetProfessor(int id)
    {
        var professor = await _professorRepository.GetByIdAsync(id);
        
        // Verificação única e correta
        if (professor is null)
        {
            return NotFound($"Professor com ID {id} não encontrado");
        }
        
        // APIs retornam Ok() com o objeto (que vira JSON), NUNCA View()
        return Ok(professor); 
    }

    [HttpPost]
    public async Task<ActionResult<Professor>> CreateProfessor(Professor professor)
    {
        var existingEmail = await _professorRepository.GetByEmailAsync(professor.Email);
        if (existingEmail is not null)
        {
            return BadRequest($"E-mail {professor.Email} já cadastrado");
        }

        var createdProfessor = await _professorRepository.AddAsync(professor);
        
        // Retorna 201 Created e a localização do novo recurso
        return CreatedAtAction(nameof(GetProfessor), new { id = createdProfessor.Id }, createdProfessor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfessor(int id, Professor professor)
    {
        if (id != professor.Id)
        {
            return BadRequest("ID do professor não corresponde");
        }

        try
        {
            await _professorRepository.UpdateAsync(professor);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfessor(int id)
    {
        if (!await _professorRepository.ExistsAsync(id))
        {
            return NotFound($"Professor com ID {id} não encontrado");
        }

        await _professorRepository.DeleteAsync(id);
        return NoContent(); // 204 No Content é o padrão para DELETE bem-sucedido
    }
}