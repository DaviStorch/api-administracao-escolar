using apiControleAluno.Models;

namespace apiControleAluno.Repositories;

public interface IProfessorRepository
{
    Task<IEnumerable<Professor>> GetAllAsync();
    Task<Professor?> GetByIdAsync(int id);
    Task<Professor?> GetByEmailAsync(string email);
    Task<Professor?> GetByMateriaAsync(string materia);
    Task<Professor> AddAsync(Professor professor);
    Task<Professor> UpdateAsync(Professor professor);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}