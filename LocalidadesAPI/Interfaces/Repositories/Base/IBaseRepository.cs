namespace LocalidadesAPI.Interfaces.Repositories.Base
{
    public interface IBaseRepository<T> : IRepository where T : class
    {
        Task Atualizar(T entity);
        Task Excluir(int id);
        Task<int> Inserir(T entity);
        Task<IEnumerable<T>> Obter();
        Task<T> ObterPorId(int id);
    }
}
