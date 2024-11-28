using System.ComponentModel.DataAnnotations;

namespace FIAP.IRRIGACAO.API.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Insira um e-mail válido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}
