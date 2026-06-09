# 📚 Biblioteca API

API REST desenvolvida em C# com ASP.NET Core para gerenciamento de livros de uma biblioteca. O projeto foi criado com o objetivo de praticar conceitos fundamentais de desenvolvimento backend, incluindo arquitetura em camadas, Entity Framework Core, injeção de dependência, persistência de dados e criação de endpoints RESTful.

## 🚀 Tecnologias Utilizadas

* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Swagger / OpenAPI
* Dependency Injection
* Repository Pattern

## 📁 Estrutura do Projeto

```text
BibliotecaApi
│
├── Controllers
├── Data
├── DTOs
├── Models
├── Repositories
├── Migrations
├── Program.cs
└── appsettings.json
```

## 📖 Funcionalidades

* Cadastrar livros
* Consultar todos os livros
* Consultar livro por ID
* Atualizar informações de um livro
* Remover livros do catálogo
* Validação de dados de entrada
* Persistência em banco de dados SQL Server

## 📚 Modelo da Entidade

```csharp
public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string ISBN { get; set; }
    public int AnoPublicacao { get; set; }
    public bool Disponivel { get; set; }
}
```

## 🔗 Endpoints

### Listar todos os livros

```http
GET /api/livros
```

### Buscar livro por ID

```http
GET /api/livros/{id}
```

### Cadastrar livro

```http
POST /api/livros
```

Exemplo de requisição:

```json
{
  "titulo": "Clean Code",
  "autor": "Robert C. Martin",
  "isbn": "9780132350884",
  "anoPublicacao": 2008
}
```

### Atualizar livro

```http
PUT /api/livros/{id}
```

### Remover livro

```http
DELETE /api/livros/{id}
```

## ⚙️ Como Executar o Projeto

### 1. Clonar o repositório

```bash
git clone <url-do-repositorio>
```

### 2. Acessar a pasta do projeto

```bash
cd BibliotecaApi
```

### 3. Restaurar dependências

```bash
dotnet restore
```

### 4. Configurar a string de conexão

No arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BibliotecaDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 5. Criar o banco de dados

```bash
dotnet ef database update
```

### 6. Executar a aplicação

```bash
dotnet run
```

## 📄 Documentação da API

Após iniciar a aplicação, acesse o Swagger:

```text
https://localhost:<porta>/swagger
```

## 🎯 Objetivos de Aprendizado

Este projeto foi desenvolvido para praticar:

* Desenvolvimento de APIs REST
* ASP.NET Core
* Entity Framework Core
* Repository Pattern
* DTOs
* Injeção de Dependência
* Migrations
* Persistência de dados
* Boas práticas de arquitetura backend

