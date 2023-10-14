using LocalidadesAPI.Interfaces.Repositories.Base;
using LocalidadesAPI.Models;

namespace LocalidadesAPI.Interfaces.Repositories
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> ObterUsuarioPorEmail(string email);
    }
}
