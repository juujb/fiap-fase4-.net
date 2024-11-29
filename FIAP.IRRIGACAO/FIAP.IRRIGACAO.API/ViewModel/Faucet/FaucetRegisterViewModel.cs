using System.ComponentModel.DataAnnotations;

namespace FIAP.IRRIGACAO.API.ViewModel
{
    public class FaucetRegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public required bool IsEnabled { get; set; }

        [Required(ErrorMessage = "A seleção de um local é obrigatória.")]
        [Range(1, long.MaxValue, ErrorMessage = "O local selecionado é inválido.")]
        public long LocationId { get; set; }
    }
}
