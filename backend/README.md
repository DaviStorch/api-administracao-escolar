# API Controle de Alunos

API REST em C# (.NET 9) para controle de alunos com banco de dados MySQL, seguindo Clean Architecture e Repository Pattern.

## 🚀 Tecnologias

- **.NET 9** / ASP.NET Core Web API
- **Entity Framework Core 9** com **Pomelo.EntityFrameworkCore.MySql**
- **MySQL 8.0** (via Docker)
- **Docker / Docker Compose**
- **Repository Pattern** + Dependency Injection

## 📁 Estrutura do Projeto

```
apiControleAluno/
├── Controllers/          # Endpoints da API
│   └── AlunosController.cs
├── Data/                 # Contexto do Entity Framework
│   └── AppDbContext.cs
├── Models/               # Entidades de domínio
│   └── Aluno.cs
├── Repositories/         # Camada de acesso a dados
│   ├── IAlunoRepository.cs
│   └── AlunoRepository.cs
├── Dockerfile
├── docker-compose.yml
├── init.sql              # Script de inicialização do banco
├── appsettings.json
└── Program.cs
```

## 🐳 Como rodar com Docker (Recomendado)

### Pré-requisitos
- Docker Desktop instalado e rodando
- Porta **3307** livre (MySQL)
- Porta **5000** livre (API)

### Passos

```bash
# 1. Clone o repositório
git clone <seu-repositorio>
cd apiControleAluno

# 2. Suba os containers
docker-compose up -d

# 3. Verifique se estão rodando
docker-compose ps

# 4. Acompanhe os logs
docker-compose logs -f mysql   # Aguarde "ready for connections"
docker-compose logs -f api     # Aguarde "Application started"
```

### URLs
- **API:** http://localhost:5000
- **Swagger/OpenAPI:** http://localhost:5000/openapi/v1.json (Development)
- **MySQL:** localhost:3307 (user: `apiuser`, password: `apipass`, db: `controle_aluno`)

---

## 💻 Como rodar localmente (sem Docker para a API)

```bash
# 1. Apenas MySQL no Docker
docker run -d --name mysql-aluno \
  -e MYSQL_ROOT_PASSWORD=rootpassword \
  -e MYSQL_DATABASE=controle_aluno \
  -e MYSQL_USER=apiuser \
  -e MYSQL_PASSWORD=apipass \
  -p 3307:3306 \
  mysql:8.0

# 2. Atualize appsettings.json (ConnectionStrings.DefaultConnection)
# Server=localhost;Database=controle_aluno;User=apiuser;Password=apipass;Port=3307;

# 3. Rode a API
dotnet run --project apiControleAluno.csproj
```

---

## 📚 Endpoints da API

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| GET | `/api/alunos/hello` | Health check |
| GET | `/api/alunos` | Lista todos os alunos |
| GET | `/api/alunos/{id}` | Busca aluno por ID |
| POST | `/api/alunos` | Cria novo aluno |
| PUT | `/api/alunos/{id}` | Atualiza aluno |
| DELETE | `/api/alunos/{id}` | Remove aluno |

---

## 🧪 Exemplos de Testes (cURL)

### Health Check
```bash
curl http://localhost:5000/api/alunos/hello
```

### Listar todos
```bash
curl http://localhost:5000/api/alunos
```

### Buscar por ID
```bash
curl http://localhost:5000/api/alunos/1
```

### Criar aluno
```bash
curl -X POST http://localhost:5000/api/alunos \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao.silva@email.com",
    "curso": "Engenharia de Software",
    "matricula": "2024006"
  }'
```

### Atualizar aluno
```bash
curl -X PUT http://localhost:5000/api/alunos/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "nome": "Ana Silva Santos",
    "email": "ana.silva@email.com",
    "curso": "Engenharia de Software",
    "matricula": "2024001"
  }'
```

### Deletar aluno
```bash
curl -X DELETE http://localhost:5000/api/alunos/1
```

---

## 🧪 Sequência completa de teste

```bash
# 1. Health check
curl http://localhost:5000/api/alunos/hello

# 2. Ver dados iniciais (seed do init.sql)
curl http://localhost:5000/api/alunos

# 3. Criar novo
curl -X POST http://localhost:5000/api/alunos \
  -H "Content-Type: application/json" \
  -d '{"nome":"Teste User","email":"teste@email.com","curso":"Teste","matricula":"999999"}'

# 4. Verificar criação
curl http://localhost:5000/api/alunos

# 5. Atualizar (use o ID retornado, ex: 6)
curl -X PUT http://localhost:5000/api/alunos/6 \
  -H "Content-Type: application/json" \
  -d '{"id":6,"nome":"Teste Atualizado","email":"teste@email.com","curso":"Teste Atualizado","matricula":"999999"}'

# 6. Deletar
curl -X DELETE http://localhost:5000/api/alunos/6
```

---

## 🛑 Parar os containers

```bash
# Para e remove containers (mantém dados no volume)
docker-compose down

# Para e remove TUDO (incluindo volume do banco - próximo sobe limpo)
docker-compose down -v
```

---

## ⚙️ Configuração

### Variáveis de ambiente (docker-compose.yml)

| Variável | Valor padrão | Descrição |
|----------|-------------|-----------|
| `MYSQL_ROOT_PASSWORD` | `rootpassword` | Senha root do MySQL |
| `MYSQL_DATABASE` | `controle_aluno` | Nome do banco |
| `MYSQL_USER` | `apiuser` | Usuário da aplicação |
| `MYSQL_PASSWORD` | `apipass` | Senha do usuário |
| `ASPNETCORE_ENVIRONMENT` | `Development` | Ambiente da API |

### Connection String (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=mysql;Database=controle_aluno;User=apiuser;Password=apipass;Port=3306;"
  }
}
```

> **Nota:** No Docker, o host é `mysql` (nome do serviço). Localmente, use `localhost` e porta `3307`.

---

## 📦 Build da imagem Docker manualmente

```bash
# Build
docker build -t api-controle-aluno .

# Run (requer MySQL rodando separadamente)
docker run -d -p 5000:8080 \
  -e ConnectionStrings__DefaultConnection="Server=host.docker.internal;Database=controle_aluno;User=apiuser;Password=apipass;Port=3307;" \
  api-controle-aluno
```

---

## 🏗️ Próximos passos / Melhorias

- [ ] Validações com FluentValidation
- [ ] Paginação nos endpoints de lista
- [ ] Autenticação/Autorização (JWT)
- [ ] Testes unitários (xUnit + Moq)
- [ ] Testes de integração (Testcontainers)
- [ ] Logging estruturado (Serilog)
- [ ] Health checks customizados
- [ ] Rate limiting
- [ ] Documentação Swagger/OpenAPI completa

---

## 📄 Licença

MIT License - Sinta-se livre para usar e modificar.