namespace DemoApi.Repositories.DB
{
    public interface IDBfunctions
    {
        public Task CreateAsync<Object>(Object obj);
        public Task DeleteAsync<Object>(Guid id);
        public Task DeleteByKeyAsync<Object>(int key);

        public Task<Object> FindByIdAsync<Object>(Guid id);
        public Task<List<Object>> FindAsync<Object>();
        public Task UpdateAsync<Object>(Object obj, Guid id);
        public Task UpdateByKeyAsync<Object>(Object obj, int key);
        public Task<Object> FindByLoginAsync<Object>(string login);
        public Task<Object> FindByKeyAsync<Object>(int key);
    }
}
