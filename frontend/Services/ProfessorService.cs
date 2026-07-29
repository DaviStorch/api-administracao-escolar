using System.Text;
using System.Text.Json;
using Frontend.Models;

namespace Frontend.Services{

    public interface IProfessorApiService
    {
        Task<List<Professor>> GetAllAsync();
        Task<Professor?> GetByIdAsync(int id);
        Task<Professor?> CreateAsync(ProfessorCreateViewModel model);
        Task<Professor?> UpdateAsync(int id, ProfessorEditViewModel model);
        Task<bool> DeleteAsync(int id);
    }
    public class ProfessorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ProfessorService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseUrl = configuration["ApiBaseUrl"] ?? "http://localhost:5177/api/";
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            // URL DIRETA E ABSOLUTA.
            var response = await _httpClient.GetAsync($"{_baseUrl}professores");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro na API: {response.StatusCode} - URL chamada: {_baseUrl}professores");
            }
            
            return await response.Content.ReadFromJsonAsync<List<Professor>>() ?? new List<Professor>();
        }
        public async Task<Professor?> GetByIdAsync(int id)
        {
            var url = $"{_baseUrl}professores/{id}";
            Console.WriteLine($"[DEBUG GET BY ID] Chamando URL: {url}");
            
            var response = await _httpClient.GetAsync(url);
            
            Console.WriteLine($"[DEBUG GET BY ID] Status: {response.StatusCode}");
            
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"[DEBUG GET BY ID] Professor não encontrado na API");
                return null;
            }
            
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ERRO DA API] Status: {response.StatusCode}");
                Console.WriteLine($"[ERRO DA API] Mensagem: {erro}");
                return null;
            }
            
            var professor = await response.Content.ReadFromJsonAsync<Professor>();
            
            if (professor is null)
            {
                Console.WriteLine($"[DEBUG GET BY ID] Professor veio null da API");
                return null;
            }
            
            Console.WriteLine($"[DEBUG GET BY ID] Professor encontrado: {professor.Nome}, {professor.Email}");
            
            return professor;
}

        public async Task<bool> CreateAsync(Professor professor)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}professores", professor);
            
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ERRO DA API] Status: {response.StatusCode}");
                Console.WriteLine($"[ERRO DA API] Mensagem: {erro}");
                
                throw new Exception($"A API retornou erro {response.StatusCode}: {erro}");
            }
            
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Professor professor)
        {
            var url = $"{_baseUrl}professores/{id}";
            Console.WriteLine($"[DEBUG UPDATE] Chamando URL: {url}");
            
            var json = JsonSerializer.Serialize(professor);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync(url, content);
            
            Console.WriteLine($"[DEBUG UPDATE] Status da API: {response.StatusCode}");
            
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[DEBUG UPDATE] ERRO DA API: {erro}");
                return false;
            }
            
            Console.WriteLine($"[DEBUG UPDATE] ✅ Sucesso!");
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var url = $"{_baseUrl}professores/{id}";
            Console.WriteLine($"[DEBUG DELETE SERVICE] Chamando URL: {url}");
            
            var response = await _httpClient.DeleteAsync(url);
            
            Console.WriteLine($"[DEBUG DELETE SERVICE] Status: {response.StatusCode}");
            
            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[DEBUG DELETE SERVICE] Erro: {erro}");
                return false;
            }
            
            Console.WriteLine($"[DEBUG DELETE SERVICE] Sucesso!");
            return true;
        }
    }
}