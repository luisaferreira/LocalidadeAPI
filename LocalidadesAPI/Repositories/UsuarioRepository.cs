using LocalidadesAPI.Interfaces.Repositories;
using LocalidadesAPI.Models;
using LocalidadesAPI.Repositories.Base;
using System.Text;

namespace LocalidadesAPI.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) 
            : base(configuration) { }

        public override async Task<int> Inserir(Usuario usuario) =>
            (int)(await CustomRepository.InsertAsync(usuario, true));

        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            var parametros = new Dictionary<string, object>()
            {
                { "Email", email }
            };

            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM USUARIO");
            sql.AppendLine("WHERE EMAIL = @Email");

            var retorno = await CustomRepository.GetAsync(sql.ToString(), parametros);

            return retorno?.FirstOrDefault();
        }
    }
}
