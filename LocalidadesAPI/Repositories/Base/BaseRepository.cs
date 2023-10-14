using LocalidadesAPI.Interfaces.Repositories.Base;
using RepositoryHelpers.DataBaseRepository;

namespace LocalidadesAPI.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly Context Context;
        protected readonly CustomRepository<T> CustomRepository;

        public BaseRepository(IConfiguration configuration)
        {
            Context = new Context(configuration);
            CustomRepository = new CustomRepository<T>(Context.Connection);
        }

        public virtual async Task<int> Inserir(T entity) =>
            (int)(await CustomRepository.InsertAsync(entity, false));

        public async Task<T> ObterPorId(int id) =>
           await CustomRepository.GetByIdAsync(id);

        public async Task<IEnumerable<T>> Obter() =>
            await CustomRepository.GetAsync();

        public async Task Excluir(int id) =>
            await CustomRepository.DeleteAsync(id);

        public async Task Atualizar(T entity) =>
            await CustomRepository.UpdateAsync(entity);

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
