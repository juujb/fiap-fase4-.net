using System.ComponentModel.DataAnnotations;

namespace FIAP.IRRIGACAO.API.ViewModel
{
    public class LocationRegisterViewModel
    {
        [StringLength(50, ErrorMessage = "O nome não pode exceder 50 caracteres.")]
        public required string Name { get; set; }
    }
}
