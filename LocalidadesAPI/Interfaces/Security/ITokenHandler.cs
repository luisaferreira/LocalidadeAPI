using LocalidadesAPI.Models;

namespace LocalidadesAPI.Interfaces.Security
{
    public interface ITokenHandler
    {
        Task<string> GerarToken(Usuario usuario);

    }
}
