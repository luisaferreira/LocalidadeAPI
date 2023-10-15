namespace LocalidadesAPI.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> : IRepository where T : class
    {
        Task Atualizar(T entity);
        Task Excluir(object id);
        Task<object> Inserir(T entity, bool identity);
        Task<IEnumerable<T>> Obter();
        Task<T> ObterPorId(object id);
    }
}
