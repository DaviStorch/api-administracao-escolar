using apiControleAluno.Data;
using apiControleAluno.Models;
using Microsoft.EntityFrameworkCore;

namespace apiControleAluno.Repositories;

public class ProfessorRepository : IProfessorRepository
{
    private readonly AppDbContext _context;

    public ProfessorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Professor>> GetAllAsync()
    {
        return await _context.Professores.ToListAsync();
    }

    public async Task<Professor?> GetByIdAsync(int id)
    {
        return await _context.Professores.FindAsync(id);
    }

    public async Task<Professor?> GetByEmailAsync(string email)
    {
        return await _context.Professores
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<Professor?> GetByMateriaAsync(string materia)
    {
        return await _context.Professores
            .FirstOrDefaultAsync(p => p.Materia == materia);
    }

    public async Task<Professor> AddAsync(Professor professor)
    {
        _context.Professores.Add(professor);
        await _context.SaveChangesAsync();
        return professor;
    }

    public async Task<Professor> UpdateAsync(Professor professor)
    {
        // 1. Verifica se existe sem rastrear
        var exists = await _context.Professores.AsNoTracking().AnyAsync(p => p.Id == professor.Id);
        if (!exists)
        {
            throw new Exception($"Professor com ID {professor.Id} não encontrado.");
        }

        // 2. Atualização direta via SQL. IGNORA COMPLETAMENTE o rastreamento do EF Core.
        await _context.Professores
            .Where(p => p.Id == professor.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(p => p.Nome, professor.Nome)
                .SetProperty(p => p.Email, professor.Email)
                .SetProperty(p => p.Materia, professor.Materia)
                .SetProperty(p => p.Ativo, professor.Ativo)
            );

        return professor;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var professor = await GetByIdAsync(id);
        if (professor == null) return false;

        _context.Professores.Remove(professor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Professores.AnyAsync(p => p.Id == id);
    }
}