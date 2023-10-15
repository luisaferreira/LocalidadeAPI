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

        public async Task<IEnumerable<IBGE>> Pesquisar(string pesquisa)
        {
            var parametros = new Dictionary<string, object>()
            {
                { "Pesquisa", $"%{ pesquisa }%" }
            };

            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM IBGE (NOLOCK)");
            sql.AppendLine("WHERE CODIGO LIKE @Pesquisa");
            sql.AppendLine("OR CIDADE LIKE @Pesquisa");
            sql.AppendLine("OR ESTADO LIKE @Pesquisa");

            return await CustomRepository.GetAsync(sql.ToString(), parametros);
        }
    }
}
