using LocalidadesAPI.Helpers;
using LocalidadesAPI.Models;
using LocalidadesAPI.Repositories;
using LocalidadesAPI.Security;
using Microsoft.AspNetCore.Mvc;

namespace LocalidadesAPI.Controllers
{
    [ApiController]
    [Route("/Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioController(IConfiguration configuration)
        {
            _usuarioRepository = new UsuarioRepository(configuration);
        }

        [HttpPost]
        [Route("/Usuario/Cadastrar")]
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
        [Route("/Usuario/Login")]
        public async Task<IActionResult> Login(string email, string senha)
        {
            if (!ValidacaoHelper.ValidarEmail(email) || !ValidacaoHelper.ValidarSenha(senha))
                return BadRequest("E-mail e/ou senha inválido!");

            var usuarioExiste = await _usuarioRepository.ObterUsuarioPorEmail(email);

            if (usuarioExiste == null)
                return NotFound("Não existe nenhum usuário cadastrado à esse e-mail!");

            if (usuarioExiste.Email == email && usuarioExiste.Senha == Criptografia.GerarHash(senha))
                return Ok();

            return BadRequest("Credenciais inválidas!");
        }
    }
}
