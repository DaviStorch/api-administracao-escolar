using System.Net.Http.Json;
using Frontend.Models;

namespace Frontend.Services
{
    public interface IAlunoApiService
    {
        Task<List<Aluno>> GetAllAsync();
        Task<Aluno?> GetByIdAsync(int id);
        Task<Aluno?> CreateAsync(AlunoCreateViewModel model);
        Task<Aluno?> UpdateAsync(int id, AlunoEditViewModel model);
        Task<bool> DeleteAsync(int id);
    }

    public class AlunoApiService : IAlunoApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public AlunoApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5000/api/";
        }

        public async Task<List<Aluno>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}alunos");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Aluno>>() ?? new List<Aluno>();
        }

        public async Task<Aluno?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}alunos/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Aluno>();
        }

        public async Task<Aluno?> CreateAsync(AlunoCreateViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiBaseUrl}alunos", model);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Aluno>();
        }

        public async Task<Aluno?> UpdateAsync(int id, AlunoEditViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}alunos/{id}", model);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Aluno>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}alunos/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;
            response.EnsureSuccessStatusCode();
            return true;
        }
    }
}