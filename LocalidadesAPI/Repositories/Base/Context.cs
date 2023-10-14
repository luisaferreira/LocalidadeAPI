using LocalidadesAPI.Mappings;
using LocalidadesAPI.Security;
using RepositoryHelpers.DataBase;
using RepositoryHelpers.Mapping;

namespace LocalidadesAPI.Repositories.Base
{
    public class Context
    {
        public Connection Connection { get; set; }

        public Context(IConfiguration configuration)
        {
            Connection = new Connection()
            {
                Database = RepositoryHelpers.Utils.DataBaseType.SqlServer,
                ConnectionString = Criptografia.Descriptografar(configuration.GetSection("ConnectionStrings:LocalidadeAPI").Value,
                        configuration.GetSection("ChaveCriptografia").Value)
            };
            InicializarMaps();
        }

        private void InicializarMaps()
        {
            Mapper.Initialize(c =>
            {
                c.AddMap(new UsuarioMap());
            });
        }
    }
}
