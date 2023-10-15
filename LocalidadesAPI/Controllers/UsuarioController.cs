using LocalidadesAPI.Helpers;
using LocalidadesAPI.Models;
using LocalidadesAPI.Repositories;
using LocalidadesAPI.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocalidadesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;
        private readonly TokenHandler _tokenHandler;

        public UsuarioController(IConfiguration configuration)
        {
            _usuarioRepository = new UsuarioRepository(configuration);
            _tokenHandler = new TokenHandler(configuration);
        }

        [HttpPost]
        [Route("Cadastrar")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar(string email, string senha)
        {
            if (!ValidacaoHelper.ValidarEmail(email) || !ValidacaoHelper.ValidarSenha(senha))
                return BadRequest("E-mail e/ou senha inválido!");

            var usuarioExiste = await _usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuarioExiste != null)
                return BadRequest("Já existe um usuário cadastrado à esse e-mail!");

            var usuario = new Usuario()
            {
                Email = email,
                Senha = Criptografia.GerarHash(senha)
            };

            var retorno = await _usuarioRepository.Inserir(usuario);

            if (retorno == 0)
                return StatusCode(500, "Erro ao cadastrar usuário!");

            return StatusCode(201, "Usuário cadastrado com sucesso!");
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (!ValidacaoHelper.ValidarEmail(email) || !ValidacaoHelper.ValidarSenha(senha))
                return BadRequest("E-mail e/ou senha inválido!");

            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuario == null)
                return NotFound("Não existe nenhum usuário cadastrado à esse e-mail!");

            if (usuario.Email == email && usuario.Senha == Criptografia.GerarHash(senha))
            {
                var token = await _tokenHandler.GerarToken(usuario);
                return Ok(token);
            }

            return BadRequest("Credenciais inválidas!");
        }
    }
}
