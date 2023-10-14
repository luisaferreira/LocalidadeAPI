using LocalidadesAPI.Models;

namespace LocalidadesAPI.Interfaces.Repositories
{
    public interface ITokenHandler
    {
        Task<string> GerarToken(Usuario usuario);
        
    }
}
