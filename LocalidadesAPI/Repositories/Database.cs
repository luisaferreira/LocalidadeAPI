using RepositoryHelpers.DataBase;

namespace LocalidadesAPI.Repositories
{
    public class Database
    {

        public Database() {
            var connection = new Connection()
            {
                Database = RepositoryHelpers.Utils.DataBaseType.SqlServer,
                ConnectionString = ""
            };
        }
    }
}
