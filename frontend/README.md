# Frontend - Controle de Alunos

Frontend MVC em ASP.NET Core (.NET 9) para o sistema de controle de alunos, consumindo a API REST do backend.

## 🚀 Tecnologias

- **.NET 9** / ASP.NET Core MVC
- **Bootstrap 5.3** (via LibMan)
- **Bootstrap Icons 1.11**
- **jQuery + jQuery Validation** (validação client-side)
- **HttpClient** (comunicação com API)
- **Dependency Injection** (serviços tipados)

## 📁 Estrutura do Projeto

```
frontend/
├── Controllers/
│   ├── HomeController.cs          # Página inicial e privacidade
│   └── AlunosController.cs        # CRUD de alunos (via API)
├── Models/
│   ├── Aluno.cs                   # ViewModels (Aluno, Create, Edit)
│   └── ErrorViewModel.cs
├── Services/
│   └── AlunoApiService.cs         # Cliente HTTP para API backend
├── Views/
│   ├── Shared/
│   │   ├── _Layout.cshtml         # Layout principal (navbar, footer fixo)
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Home/
│   │   ├── Index.cshtml           # Landing page
│   │   └── Privacy.cshtml
│   └── Alunos/
│       ├── Index.cshtml           # Lista com tabela + cards mobile
│       ├── Create.cshtml          # Formulário de criação
│       ├── Edit.cshtml            # Formulário de edição
│       ├── Details.cshtml         # Visualização detalhada
│       └── Delete.cshtml          # Confirmação de exclusão
├── wwwroot/
│   ├── css/
│   │   └── site.css               # Estilos customizados
│   ├── js/
│   │   └── site.js
│   └── lib/                       # Bootstrap, jQuery (via LibMan)
├── appsettings.json               # Configuração da URL da API
├── Program.cs                     # Configuração DI, HttpClient
└── Frontend.csproj
```

## ⚙️ Configuração

### appsettings.json
```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5000/api/"
  }
}
```

> **Importante:** A API backend deve estar rodando em `http://localhost:5000` (ou atualize a `BaseUrl`).

## 🐳 Como Rodar

### Opção 1: Docker Compose (Backend + Frontend juntos)

Adicione ao `docker-compose.yml` do backend:

```yaml
services:
  frontend:
    build:
      context: ../frontend
      dockerfile: Dockerfile
    container_name: apiControleAluno-frontend
    ports:
      - "5001:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ApiSettings__BaseUrl=http://api:8080/api/
    depends_on:
      - api
    networks:
      - api-network
```

### Opção 2: Local (Desenvolvimento)

```bash
# 1. Inicie o backend (MySQL + API)
cd ../backend
docker-compose up -d

# 2. Rode o frontend
cd ../frontend
dotnet run --project Frontend.csproj --urls "http://localhost:5001"
```

### Opção 3: Apenas Frontend (API já rodando)

```bash
cd frontend
dotnet run --urls "http://localhost:5001"
```

## 🌐 URLs

| Ambiente | Frontend | Backend API |
|----------|----------|-------------|
| Local | http://localhost:5001 | http://localhost:5000 |
| Docker | http://localhost:5001 | http://localhost:5000 |

## 🎯 Funcionalidades

### Página Inicial (`/`)
- Landing page com links para Alunos

### Alunos (`/Alunos`)
- **Lista** (`/Alunos`) - Tabela responsiva com paginação visual, cards no mobile
- **Novo** (`/Alunos/Create`) - Formulário com validação
- **Editar** (`/Alunos/Edit/{id}`) - Formulário pré-preenchido
- **Detalhes** (`/Alunos/Details/{id}`) - Visualização completa
- **Excluir** (`/Alunos/Delete/{id}`) - Confirmação com modal

### Validações
- **Client-side:** jQuery Validation + Bootstrap (required, email, maxlength)
- **Server-side:** ModelState + validações da API (400 BadRequest)

### UX/UI
- ✅ Footer fixo no final da página (flexbox)
- ✅ Navbar responsiva com sticky-top
- ✅ Page headers com gradientes coloridos
- ✅ Cards com sombras e hover effects
- ✅ Tabela desktop + cards mobile (breakpoint `md`)
- ✅ Dropdown de ações no mobile
- ✅ Tooltips nos botões de ação
- ✅ Badges coloridos para ID, Curso, Matrícula
- ✅ Alertas de sucesso/erro (TempData)
- ✅ Animações fade-in
- ✅ Bootstrap Icons

## 🔧 Integração com API

### Serviço Tipado (`AlunoApiService`)
```csharp
public interface IAlunoApiService
{
    Task<List<Aluno>> GetAllAsync();
    Task<Aluno?> GetByIdAsync(int id);
    Task<Aluno?> CreateAsync(AlunoCreateViewModel model);
    Task<Aluno?> UpdateAsync(int id, AlunoEditViewModel model);
    Task<bool> DeleteAsync(int id);
}
```

### Configuração HttpClient (Program.cs)
```csharp
builder.Services.AddHttpClient<IAlunoApiService, AlunoApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
```

### Controller (`AlunosController`)
```csharp
public class AlunosController : Controller
{
    private readonly IAlunoApiService _api;
    
    public async Task<IActionResult> Index() 
        => View(await _api.GetAllAsync());
    
    public async Task<IActionResult> Create(AlunoCreateViewModel model) {
        if (ModelState.IsValid) {
            var created = await _api.CreateAsync(model);
            if (created != null) return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Erro na API...");
        }
        return View(model);
    }
    // ... Edit, Details, Delete similares
}
```

## 🎨 Personalização Visual

### Cores dos Headers
| Página | Gradiente |
|--------|-----------|
| Lista | Azul → Roxo (`#0d6efd` → `#6610f2`) |
| Criar | Azul padrão |
| Editar | Amarelo → Laranja (`#ffc107` → `#ff9800`) |
| Detalhes | Azul padrão |
| Excluir | Vermelho (`#dc3545` → `#c82333`) |

### Estilos Customizados (`wwwroot/css/site.css`)
- Variáveis CSS para cores
- Animações fade-in
- Cards com hover
- Tabelas estilizadas
- Input groups com focus
- Responsividade mobile-first

## 📱 Responsividade

| Breakpoint | Comportamento |
|------------|---------------|
| `≥ 768px` (md) | Tabela completa com botões em grupo |
| `< 768px` | Cards empilhados + dropdown de ações |

## 🛠️ Scripts Úteis

```bash
# Build
dotnet build Frontend.csproj

# Run com watch (hot reload)
dotnet watch run --project Frontend.csproj

# Publish
dotnet publish -c Release -o ./publish

# Limpar
dotnet clean
```

## 🐛 Troubleshooting

### API não conecta
- Verifique se backend está rodando: `curl http://localhost:5000/api/alunos/hello`
- Confira `appsettings.json` → `ApiSettings:BaseUrl`
- No Docker: use `http://api:8080/api/` (nome do serviço)

### Erro de porta ocupada (MSB3026)
```bash
taskkill /PID <pid> /F
# ou
dotnet build # tenta novamente
```

### Validação não funciona
- Verifique se `jquery.validate.min.js` e `jquery.validate.unobtrusive.min.js` estão em `wwwroot/lib/`
- Confirme `@section Scripts` nas views com `<script src="~/lib/..."></script>`

## 📦 Build Docker (Opcional)

```dockerfile
# Dockerfile (criar na raiz do frontend)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Frontend.csproj", "."]
RUN dotnet restore "Frontend.csproj"
COPY . .
RUN dotnet publish "Frontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Frontend.dll"]
```

## 🔗 Links Relacionados

- [Backend API](../backend/README.md)
- [Documentação Bootstrap 5](https://getbootstrap.com/docs/5.3/)
- [Bootstrap Icons](https://icons.getbootstrap.com/)
- [ASP.NET Core MVC Docs](https://learn.microsoft.com/aspnet/core/mvc/overview)

---

**Desenvolvido com** ❤️ **usando ASP.NET Core MVC + Bootstrap 5**