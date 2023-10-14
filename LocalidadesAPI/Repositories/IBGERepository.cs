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

        public async Task<IBGE> ObterPorCodigo(string codigo)
        {
            var parametros = new Dictionary<string, object>()
            {
                { "Codigo", codigo }
            };

            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM IBGE (NOLOCK)");
            sql.AppendLine("WHERE Codigo = @Codigo");

            return (await CustomRepository.GetAsync(sql.ToString(), parametros))?.FirstOrDefault();
        }
    }
}
