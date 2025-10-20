using ListaDeTarefas2.Validators;
using System.ComponentModel.DataAnnotations;

namespace ListaDeTarefas2.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(40, ErrorMessage = "O título não pode ter mais de 40 caracteres.")]
        public string Titulo { get; set; } = string.Empty;
        public DateTime DataDeCriacao { get; set; }
        [Required(ErrorMessage = "A data limite é obrigatória.")]
        [FutureOrPresent]
        public DateTime DataLimite { get; set; }
        public bool Finalizado { get; set; } = false;
    }
}
