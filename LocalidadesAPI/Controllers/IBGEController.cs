using LocalidadesAPI.Helpers;
using LocalidadesAPI.Interfaces.Repositories;
using LocalidadesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocalidadesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IBGEController : Controller
    {
        private readonly IIBGERepository _IBGERepository;

        public IBGEController(IIBGERepository ibgeRepository)
        {
            _IBGERepository = ibgeRepository;
        }

        [HttpPost]
        [Route("Adicionar")]
        public async Task<IActionResult> AdicionarLocalidade(IBGE ibge)
        {
            if (ibge == null)
                return BadRequest("Objeto não pode ser nulo!");

            if (!ValidacaoHelper.ValidarCodigoIBGE(ibge.Codigo) || !ValidacaoHelper.ValidarSiglaEstado(ibge.Estado) || string.IsNullOrEmpty(ibge.Cidade))
                return BadRequest("Informações inválidas!");

            var localidadeExiste = await _IBGERepository.ObterPorId(ibge.Codigo);

            if (localidadeExiste != null)
                return BadRequest("Já existe uma localidade vinculada à esse código IBGE!");

            var retorno = (int) await _IBGERepository.Inserir(ibge, false);

            if (retorno == 0)
                return StatusCode(500, "Erro ao adicionar localidade!");

            return StatusCode(201, "Localidade adicionada com sucesso!");
        }

        [HttpGet]
        [Route("ObterLocalidades")]
        public async Task<IActionResult> ObterLocalidades()
        {
            var localidades = await _IBGERepository.Obter();

            return StatusCode(200, localidades);
        }

        [HttpGet]
        [Route("ObterLocalidadePorCodigoIBGE/{codigoIBGE}")]
        public async Task<IActionResult> ObterLocalidadePorCodigoIBGE(string codigoIBGE)
        {
            if (!ValidacaoHelper.ValidarCodigoIBGE(codigoIBGE))
                return BadRequest("O código IBGE é inválido!");

            var localidade = await _IBGERepository.ObterPorId(codigoIBGE);

            return StatusCode(200, localidade);
        }

        [HttpDelete]
        [Route("ExcluirLocalidade/{codigo}")]
        public async Task<IActionResult> ExcluirLocalidade(string codigo)
        {
            if (!ValidacaoHelper.ValidarCodigoIBGE(codigo))
                return BadRequest("O código IBGE é inválido!");

            var localidade = await _IBGERepository.ObterPorId(codigo);

            if (localidade == null)
                return NotFound("Não existe nenhuma localidade vinculada à esse código IBGE!");

            await _IBGERepository.Excluir(codigo);

            return StatusCode(200, "Localidade excluída!");
        }

        [HttpPut]
        [Route("AtualizarLocalidade")]
        public async Task<IActionResult> AtualizarLocalidade(IBGE ibge)
        {
            if (ibge == null)
                return BadRequest("Objeto não pode ser nulo!");

            if (!ValidacaoHelper.ValidarCodigoIBGE(ibge.Codigo) || !ValidacaoHelper.ValidarSiglaEstado(ibge.Estado) || string.IsNullOrEmpty(ibge.Cidade))
                return BadRequest("Informações inválidas!");

            var localidadeExiste = await _IBGERepository.ObterPorId(ibge.Codigo);

            if (localidadeExiste == null)
                return NotFound("Não existe nenhuma localidade vinculada à esse código IBGE!");

            await _IBGERepository.Atualizar(ibge);

            return Ok("Localidade atualizada!");
        }

        [HttpGet]
        [Route("Pesquisar/{pesquisa}")]
        public async Task<IActionResult> Pesquisar(string pesquisa)
        {
            if (string.IsNullOrEmpty(pesquisa))
                return BadRequest("Índice de pesquisa vazio!");

            var localidades = await _IBGERepository.Pesquisar(pesquisa);

            return Ok(localidades);
        }
    }
}
