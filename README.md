# Lista de Tarefas ASP.NET

Uma aplicação web desenvolvida em **ASP.NET MVC** com **Dapper** e **SQL Server**, que permite criar, editar, excluir, concluir e filtrar tarefas.  
O projeto foi criado com foco em boas práticas, validações de dados e organização simples para aprendizado e portfólio.

---

## 🧩 Funcionalidades

- ✅ **Criar** novas tarefas com título e data limite.  
- ✏️ **Editar** tarefas existentes.  
- 🗑️ **Excluir** tarefas com página de confirmação.  
- 🎯 **Marcar como concluída**.  
- 🔍 **Buscar** tarefas por título.  
- 🕒 **Ordenar** por data de criação ou limite (ascendente/descendente).  
- 🎚️ **Filtrar** por tarefas pendentes, concluídas ou todas.  
- ⚙️ **Validação personalizada**: impede inserir datas anteriores à atual (`FutureOrPresentAttribute`).

---

## 🏗️ Tecnologias Utilizadas

- **ASP.NET MVC 8**
- **C# 12**
- **Dapper** (para consultas SQL)
- **SQL Server**
- **Bootstrap 5**
- **Razor Views**
- **Data Annotations**

---

## 🗄️ Banco de Dados

O projeto utiliza **SQL Server**.  
A string de conexão está configurada em `appsettings.json`:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ListaDeTarefas;Trusted_Connection=True;TrustServerCertificate=True;"
}

# Estrutura da tabela Tarefas:

```sql
CREATE TABLE Tarefas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(40) NOT NULL,
    DataDeCriacao DATETIME NOT NULL,
    DataLimite DATETIME NOT NULL,
    Finalizado BIT DEFAULT 0
);
```

---

```bash
# 1️⃣ Clone este repositório
git clone [https://github.com/Davi-2002/Lista-de-Tarefas-ASP.NET.git](https://github.com/Davi-2002/Lista-de-Tarefas-ASP.NET.git)
cd Lista-de-Tarefas-ASP.NET

# 2️⃣ Abra o projeto no Visual Studio ou Visual Studio Code

# 3️⃣ Configure o banco de dados:
#    - Certifique-se de que o SQL Server esteja em execução.
#    - Execute o script acima para criar o banco e a tabela.

# 4️⃣ Atualize a string de conexão (se necessário)
# Edite o arquivo appsettings.json conforme seu ambiente.

# 5️⃣ Restaure dependências e execute
dotnet restore
dotnet run
```

# 6️⃣ Acesse a aplicação no navegador
https://localhost:xxxx/Tarefas

---

## 💡 Detalhes Técnicos

* As operações de CRUD são realizadas via Dapper, sem o uso de Entity Framework.
* A filtragem, busca e ordenação são tratadas na action `Index` do `TarefasController`.
* O atributo `[FutureOrPresent]` é uma validação personalizada que impede inserir uma data limite anterior à atual.
* O botão Excluir leva a uma página de confirmação, prevenindo exclusões acidentais.
