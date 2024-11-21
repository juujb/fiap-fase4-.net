using System.ComponentModel.DataAnnotations;

namespace FIAP.IRRIGACAO.API.ViewModels
{
    public class LocationViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public required string Name { get; set; }
    }
}
