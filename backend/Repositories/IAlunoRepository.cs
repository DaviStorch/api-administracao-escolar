using apiControleAluno.Models;

namespace apiControleAluno.Repositories
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAllAsync();
        Task<Aluno?> GetByIdAsync(int id);
        Task<Aluno?> GetByEmailAsync(string email);
        Task<Aluno?> GetByMatriculaAsync(string matricula);
        Task<Aluno> AddAsync(Aluno aluno);
        Task<Aluno> UpdateAsync(Aluno aluno);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}