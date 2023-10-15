using LocalidadesAPI.Interfaces.Repositories;
using LocalidadesAPI.Models;
using LocalidadesAPI.Repositories.Base;
using System.Text;

namespace LocalidadesAPI.Repositories
{
    public class IBGERepository : BaseRepository<IBGE>, IIBGERepository
    {
        public IBGERepository(IConfiguration configuration)
            : base(configuration) { }
    }
}
