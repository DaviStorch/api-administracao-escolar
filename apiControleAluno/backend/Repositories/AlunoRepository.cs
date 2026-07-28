using Microsoft.EntityFrameworkCore;
using apiControleAluno.Data;
using apiControleAluno.Models;
using apiControleAluno.Repositories;

namespace apiControleAluno.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAllAsync()
        {
            return await _context.Alunos.AsNoTracking().ToListAsync();
        }

        public async Task<Aluno?> GetByIdAsync(int id)
        {
            return await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Aluno?> GetByEmailAsync(string email)
        {
            return await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<Aluno?> GetByMatriculaAsync(string matricula)
        {
            return await _context.Alunos.AsNoTracking().FirstOrDefaultAsync(a => a.Matricula == matricula);
        }

        public async Task<Aluno> AddAsync(Aluno aluno)
        {
            aluno.CreatedAt = DateTime.UtcNow;
            aluno.UpdatedAt = DateTime.UtcNow;
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<Aluno> UpdateAsync(Aluno aluno)
        {
            aluno.UpdatedAt = DateTime.UtcNow;
            _context.Alunos.Update(aluno);
            await _context.SaveChangesAsync();
            return aluno;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null) return false;

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Alunos.AnyAsync(a => a.Id == id);
        }
    }
}