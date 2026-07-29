using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Frontend.Services;

namespace Frontend.Controllers; // <-- NOTE: Namespace do Frontend

public class ProfessoresController : Controller
{
    private readonly ProfessorService _professorService;

    public ProfessoresController(ProfessorService professorService)
    {
        _professorService = professorService;
    }

    // GET: Professores
    public async Task<IActionResult> Index()
    {
        var professores = await _professorService.GetAllAsync();
        return View(professores);
        }
    // GET: Professores/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var professor = await _professorService.GetByIdAsync(id.Value);
        if (professor is null)
        {
            return NotFound();
        }
        return View(professor);
    }

    // GET: Professores/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Professores/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Professor professor)
    {
        if (ModelState.IsValid)
        {
            var success = await _professorService.CreateAsync(professor);
            if (success)
            {
                TempData["Success"] = "Professor criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Erro ao criar professor. Verifique se os dados estão corretos.");
        }
        return View(professor);
    }
        
    // GET: Professores/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        
        var professor = await _professorService.GetByIdAsync(id.Value);
        if (professor is null) return NotFound();
        
        var viewModel = new ProfessorEditViewModel
        {
            Id = professor.Id,
            Nome = professor.Nome,
            Email = professor.Email,
            Materia = professor.Materia,
            Ativo = professor.Ativo
        };
        
        return View(viewModel); 
    }

    // POST: Professores/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProfessorEditViewModel model)
    {
        Console.WriteLine($"[DEBUG EDIT POST] Método chamado! ID da URL: {id}, ID do Model: {model?.Id}");
        
        if (id != model.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            Console.WriteLine("[DEBUG EDIT POST] ModelState INVÁLIDO!");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($" - {error.ErrorMessage}");
            }
            return View(model);
        }

        // 🔥 CONVERSÃO: Transforma o ViewModel de volta em Model
        var professor = new Professor
        {
            Id = model.Id,
            Nome = model.Nome,
            Email = model.Email,
            Materia = model.Materia,
            Ativo = model.Ativo
        };

        var success = await _professorService.UpdateAsync(id, professor);
        
        if (success)
        {
            TempData["SuccessMessage"] = "Professor atualizado com sucesso!";
            return RedirectToAction(nameof(Index));
        }
        
        TempData["ErrorMessage"] = "Erro ao atualizar professor.";
        return View(model);
    }

    // GET: Professores/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var professor = await _professorService.GetByIdAsync(id.Value);
        if (professor is null)
        {
            return NotFound();
        }

        return View(professor);
    }

    // POST: Professores/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Console.WriteLine($"[DEBUG DELETE] Método chamado com ID: {id}");
        
        var success = await _professorService.DeleteAsync(id);
        
        Console.WriteLine($"[DEBUG DELETE] Resultado do Service: {success}");
        
        if (success)
        {
            Console.WriteLine($"[DEBUG DELETE] Professor {id} excluído com sucesso");
            TempData["Success"] = "Professor removido com sucesso!";
        }
        else
        {
            Console.WriteLine($"[DEBUG DELETE] Falha ao excluir professor {id}");
            TempData["Error"] = "Erro ao remover professor.";
        }
        return RedirectToAction(nameof(Index));
    }
}