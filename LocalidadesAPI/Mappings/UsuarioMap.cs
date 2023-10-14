using Dapper.FluentMap.Dommel.Mapping;
using LocalidadesAPI.Models;

namespace LocalidadesAPI.Mappings
{
    public class UsuarioMap : DommelEntityMap<Usuario>
    {
        public UsuarioMap()
        {
            Map(x => x.Id)
                .IsKey().IsIdentity();
        }
    }
}
