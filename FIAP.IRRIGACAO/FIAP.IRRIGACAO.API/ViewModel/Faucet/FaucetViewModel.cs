using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace FIAP.IRRIGACAO.API.ViewModel
{
    public class FaucetViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O status é obrigatório.")]
        public required bool IsEnabled { get; set; }

        [Required(ErrorMessage = "A seleção de um local é obrigatória.")]
        [Range(1, long.MaxValue, ErrorMessage = "O local selecionado é inválido.")]
        public long LocationId { get; set; }

        public string? LocationName { get; set; }

    }
}
