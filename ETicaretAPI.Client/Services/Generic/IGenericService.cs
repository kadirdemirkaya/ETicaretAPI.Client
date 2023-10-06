using ETicaretAPI.Client.Models.Product.GetAllProduct;

namespace ETicaretAPI.Client.Services.Generic
{
    public interface IGenericService<T, V>
        where T : class?
        where V : class?
    {
        Task<V> GetAsync(V entityResponse, Guid? id, string appUrl);
        Task<V> GetAllAsync(V entityResponse, string appUrl);
        Task<V> AddAsync(T entityRequest, V entityResponse, string appUrl);
        Task<V> UpdateAsync(T entityRequest, V entityResponse, string appUrl);
        Task<V> DeleteAsync(V entityResponse, Guid id, string appUrl);

        Task<V> UpdateAsync<T, V>(T entity, V entityResponse, string appUrl) where T : class where V : class;

        Task<V> GetAsync(string appUrl);
    }
}
