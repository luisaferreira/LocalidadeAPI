using LocalidadesAPI.Interfaces.Repositories.Base;
using LocalidadesAPI.Models;

namespace LocalidadesAPI.Interfaces.Repositories
{
    public interface IIBGERepository : IBaseRepository<IBGE>
    {
        Task<IBGE> ObterPorCodigo(string codigo);
        Task Excluir(string codigo);
    }
}
