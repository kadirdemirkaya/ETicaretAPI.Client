using AutoMapper;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ETicaretAPI.Client.Services.Generic
{
    public class GenericService<T, V> : IGenericService<T, V>
        where T : class?
        where V : class?
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public GenericService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient, IMapper mapper)
        {
            this.httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", httpContextAccessor.HttpContext.Session.GetString("JWToken"));
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = httpClient;
            this.mapper = mapper;
        }

        public async Task<V> GetAllAsync(V entityResponse, string appUrl)
        {
            var response = await httpClient.GetAsync(appUrl);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var allProducts = JsonConvert.DeserializeObject<V>(result);

            return allProducts;
        }

        public async Task<V> AddAsync(T? entityRequest, V? entityResponse, string appUrl)
        {
            var json = JsonConvert.SerializeObject(entityRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(appUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<V>(result);
            return resultResponse;
        }

        public async Task<V> DeleteAsync(V entityResponse, Guid Id, string appUrl)
        {
            httpClient.DefaultRequestHeaders.Add("Id", Id.ToString());
            var response = await httpClient.DeleteAsync(appUrl);

            string result = await response.Content.ReadAsStringAsync();
            var deleteProductResponse = JsonConvert.DeserializeObject<V>(result);

            httpClient.DefaultRequestHeaders.Remove("Id");

            response.EnsureSuccessStatusCode();

            return deleteProductResponse;
        }

        public async Task<V> GetAsync(V entityResponse, Guid? id, string appUrl)
        {
            if (id is not null)
            {
                httpClient.DefaultRequestHeaders.Add("Id", id.ToString());
            }

            var response = await httpClient.GetAsync(appUrl);
            response.EnsureSuccessStatusCode();

            if (id is not null)
            {
                httpClient.DefaultRequestHeaders.Remove("Id");
            }

            var result = await response.Content.ReadAsStringAsync();
            var productDto = JsonConvert.DeserializeObject<V>(result);
            return productDto;
        }

        public async Task<V> UpdateAsync(T entityRequest, V entityResponse, string appUrl)
        {
            var json = JsonConvert.SerializeObject(entityRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(appUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var updateResponse = JsonConvert.DeserializeObject<V>(result);

            return updateResponse;
        }

        public async Task<V> UpdateAsync<T, V>(T entity, V entityResponse, string appUrl)
            where T : class
            where V : class
        {
            var json = JsonConvert.SerializeObject(entity);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(appUrl, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var updateResponse = JsonConvert.DeserializeObject(result, typeof(V)) as V;

            return updateResponse;
        }

        public async Task<V> GetAsync(string appUrl)
        {
            var response = await httpClient.GetAsync(appUrl);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            var productDto = JsonConvert.DeserializeObject<V>(result);
            return productDto;
        }
    }
}
