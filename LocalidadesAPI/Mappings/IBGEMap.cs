using Dapper.FluentMap.Dommel.Mapping;
using LocalidadesAPI.Models;

namespace LocalidadesAPI.Mappings
{
    public class IBGEMap : DommelEntityMap<IBGE>
    {
        public IBGEMap()
        {
            Map(x => x.Codigo)
                .IsKey();
        }
    }
}
