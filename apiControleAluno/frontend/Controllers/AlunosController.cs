using Microsoft.AspNetCore.Mvc;
using Frontend.Models;
using Frontend.Services;

namespace Frontend.Controllers;

public class AlunosController : Controller
{
    private readonly IAlunoApiService _alunoApiService;

    public AlunosController(IAlunoApiService alunoApiService)
    {
        _alunoApiService = alunoApiService;
    }

    // GET: Alunos
    public async Task<IActionResult> Index()
    {
        var alunos = await _alunoApiService.GetAllAsync();
        return View(alunos);
    }

    // GET: Alunos/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var aluno = await _alunoApiService.GetByIdAsync(id);
        if (aluno == null)
        {
            return NotFound();
        }
        return View(aluno);
    }

    // GET: Alunos/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Alunos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AlunoCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var created = await _alunoApiService.CreateAsync(model);
            if (created != null)
            {
                TempData["Success"] = "Aluno criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Erro ao criar aluno. Verifique se e-mail ou matrícula já existem.");
        }
        return View(model);
    }

    // GET: Alunos/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var aluno = await _alunoApiService.GetByIdAsync(id);
        if (aluno == null)
        {
            return NotFound();
        }
        var model = new AlunoEditViewModel
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            Email = aluno.Email,
            Curso = aluno.Curso,
            Matricula = aluno.Matricula
        };
        return View(model);
    }

    // POST: Alunos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, AlunoEditViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var updated = await _alunoApiService.UpdateAsync(id, model);
            if (updated != null)
            {
                TempData["Success"] = "Aluno atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Erro ao atualizar aluno. Verifique se e-mail ou matrícula já existem.");
        }
        return View(model);
    }

    // GET: Alunos/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var aluno = await _alunoApiService.GetByIdAsync(id);
        if (aluno == null)
        {
            return NotFound();
        }
        return View(aluno);
    }

    // POST: Alunos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _alunoApiService.DeleteAsync(id);
        if (deleted)
        {
            TempData["Success"] = "Aluno excluído com sucesso!";
        }
        else
        {
            TempData["Error"] = "Erro ao excluir aluno.";
        }
        return RedirectToAction(nameof(Index));
    }
}