using LocalidadesAPI.Interfaces.Repositories.Base;
using LocalidadesAPI.Models;

namespace LocalidadesAPI.Interfaces.Repositories
{
    public interface IIBGERepository : IBaseRepository<IBGE> 
    {
        Task<IEnumerable<IBGE>> Pesquisar(string pesquisa);
    }
}
