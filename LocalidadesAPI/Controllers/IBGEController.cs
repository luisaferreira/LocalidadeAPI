using LocalidadesAPI.Helpers;
using LocalidadesAPI.Models;
using LocalidadesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LocalidadesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IBGEController : Controller
    {
        private readonly IBGERepository _IBGERepository;

        public IBGEController(IConfiguration configuration)
        {
            _IBGERepository = new IBGERepository(configuration);
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarLocalidade(IBGE ibge)
        {
            if (ibge == null)
                return BadRequest("Objeto não pode ser nulo!");

            if (!ValidacaoHelper.ValidarCodigoIBGE(ibge.Codigo) || !ValidacaoHelper.ValidarSiglaEstado(ibge.Estado) || string.IsNullOrEmpty(ibge.Cidade))
                return BadRequest("Informações inválidas!");

            var localidadeExiste = await _IBGERepository.ObterPorCodigo(ibge.Codigo);

            if (localidadeExiste != null)
                return BadRequest("Já existe uma localidade vinculada à esse código IBGE!");

            var retorno = await _IBGERepository.Inserir(ibge);

            if (retorno == 0)
                return StatusCode(500, "Erro ao adicionar localidade!");

            return StatusCode(201, "Localidade adicionada com sucesso!");
        }

        [HttpGet]
        public async Task<IActionResult> ObterLocalidade()
        {
            var localidades = await _IBGERepository.Obter();

            return StatusCode(200, localidades);
        }

        [HttpGet]
        [Route("{codigoIBGE}")]
        public async Task<IActionResult> ObterLocalidadePorCodigoIBGE(string codigoIBGE)
        {
            if (!ValidacaoHelper.ValidarCodigoIBGE(codigoIBGE))
                return BadRequest("O código IBGE é inválido!");

            var localidade = await _IBGERepository.ObterPorCodigo(codigoIBGE);

            return StatusCode(200, localidade);
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirLocalidade(string codigo)
        {
            await _IBGERepository.Excluir(codigo);

            return StatusCode(200);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarLocalidade(IBGE ibge)
        {
            if (ibge == null)
                return BadRequest("Objeto não pode ser nulo!");

            if (!ValidacaoHelper.ValidarCodigoIBGE(ibge.Codigo) || !ValidacaoHelper.ValidarSiglaEstado(ibge.Estado) || string.IsNullOrEmpty(ibge.Cidade))
                return BadRequest("Informações inválidas!");

            var localidadeExiste = await _IBGERepository.ObterPorCodigo(ibge.Codigo);

            if (localidadeExiste == null)
                return NotFound("Não existe nenhuma localidade vinculada à esse código IBGE!");

            await _IBGERepository.Atualizar(ibge);

            return Ok("Localidade atualizada!");
        }
    }
}
