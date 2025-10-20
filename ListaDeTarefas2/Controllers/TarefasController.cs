using Dapper;
using ListaDeTarefas2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ListaDeTarefas2.Controllers
{
    public class TarefasController : Controller
    {
        private readonly string _connectionString;
        public TarefasController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection")!;
        }

        //public IActionResult Index()
        //{
        //    //SqlConnection conexao = new SqlConnection(@"Server=localhost;Database=ListaDeTarefas;Trusted_Connection=True;TrustServerCertificate=True;");
        //    //conexao.Open();
        //    using var conexao = new SqlConnection(_connectionString);
        //    var lista = conexao.Query<Tarefa>("SELECT * FROM Tarefas");
        //    ViewData["Title"] = "Lista De Tarefas";
        //    return View(lista);
        //}

        //public IActionResult Index(string? busca)
        //{
        //    using var conexao = new SqlConnection(_connectionString);
        //    IEnumerable<Tarefa> lista;

        //    if (!string.IsNullOrEmpty(busca))
        //    {
        //        var sql = "SELECT * FROM Tarefas WHERE Titulo LIKE '%' + @busca + '%'";
        //        lista = conexao.Query<Tarefa>(sql, new { busca });
        //    }
        //    else
        //    {
        //        lista = conexao.Query<Tarefa>("SELECT * FROM Tarefas");
        //    }

        //    ViewData["Title"] = "Lista De Tarefas";
        //    ViewData["Busca"] = busca;
        //    return View(lista);
        //}

        public IActionResult Index(string? busca, string? ordenarPor, string? filtro)
        {
            using var conexao = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM Tarefas";

            if (!string.IsNullOrEmpty(busca))
            {
                sql += " WHERE Titulo LIKE '%' + @busca + '%'";
                if (filtro == "pendentes")
                    sql += " AND Finalizado = 0";
                else if (filtro == "concluidas")
                    sql += " AND Finalizado = 1";
            }
            else
            {
                if (filtro == "pendentes")
                    sql += " WHERE Finalizado = 0";
                else if (filtro == "concluidas")
                    sql += " WHERE Finalizado = 1";
            }


            sql += ordenarPor switch
            {
                "DataLimiteAsc" => " ORDER BY DataLimite ASC",
                "DataLimiteDesc" => " ORDER BY DataLimite DESC",
                "DataCriacaoAsc" => " ORDER BY DataDeCriacao ASC",
                _ => " ORDER BY DataDeCriacao DESC"
            };

            var lista = conexao.Query<Tarefa>(sql, new { busca });
            ViewData["Title"] = "Lista De Tarefas";
            ViewData["Busca"] = busca;
            ViewData["Filtro"] = filtro;
            ViewData["OrdenarPor"] = ordenarPor;
            return View(lista);
        }


        public IActionResult Create()
        {
            ViewData["Title"] = "Nova Tarefa";
            return View("Form");
        }

        [HttpPost]
        public IActionResult Create(Tarefa tarefa)
        {
            if(!ModelState.IsValid)
            {
                ViewData["Title"] = "Nova Tarefa";
                return View("Form", tarefa);
            }

            using var conexao = new SqlConnection(_connectionString);
            tarefa.DataDeCriacao = DateTime.Now;
            string sql = @"INSERT INTO Tarefas (Titulo, DataDeCriacao, DataLimite) 
                        VALUES (@Titulo, @DataDeCriacao, @DataLimite)";
            conexao.Execute(sql, tarefa);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            using var conexao = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM Tarefas WHERE Id = @Id";
            var tarefa = conexao.QueryFirstOrDefault<Tarefa>(sql, new { Id = id });

            if (tarefa == null)
                return NotFound();

            ViewData["Title"] = "Excluir Tarefa";
            return View("Delete", tarefa);
        }

        [HttpPost]
        public IActionResult Delete(Tarefa tarefa)
        {
            using var conexao = new SqlConnection(_connectionString);
            string sql = @"DELETE FROM Tarefas WHERE Id = @Id";
            conexao.Execute(sql, tarefa);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            using var conexao = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM Tarefas WHERE Id = @Id";
            var tarefa = conexao.QueryFirstOrDefault<Tarefa>(sql, new { Id = id });

            if (tarefa == null)
                return NotFound();

            ViewData["Title"] = "Editar Tarefa";
            return View("Form", tarefa);
        }

        [HttpPost]
        public IActionResult Edit(Tarefa tarefa)
        {
            if (!ModelState.IsValid) return View("Form", tarefa);
            ViewData["Title"] = "Editar Tarefa";
            using var conexao = new SqlConnection(_connectionString);
            string sql = @"UPDATE Tarefas SET Titulo = @Titulo, DataLimite = @DataLimite WHERE Id = @Id";
            conexao.Execute(sql, tarefa);
            return RedirectToAction("Index");
        }

        public IActionResult Concluir(int id)
        {
            using var conexao = new SqlConnection(_connectionString);
            string sql = @"UPDATE Tarefas SET Finalizado = 1 WHERE Id = @Id";
            conexao.Execute(sql, new { Id = id });
            return RedirectToAction("Index");
        }
    }
}
