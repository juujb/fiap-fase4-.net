using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.IRRIGACAO.API.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Realiza o login do usuário.
        /// </summary>
        /// <param name="model">Modelo contendo email, senha e a flag de lembrar o login.</param>
        /// <returns>Token JWT ou erro de autenticação.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Login realizado com sucesso." });
            }

            return Unauthorized("Credenciais inválidas.");
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="model">Modelo contendo os dados do usuário a ser criado.</param>
        /// <returns>Confirmação de criação ou erros de validação.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Usuário criado com sucesso!" });
            }

            return BadRequest(result.Errors.Select(e => e.Description));
        }

        /// <summary>
        /// Realiza o logout do usuário.
        /// </summary>
        /// <returns>Confirmação de logout.</returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Logout realizado com sucesso." });
        }
    }
}
