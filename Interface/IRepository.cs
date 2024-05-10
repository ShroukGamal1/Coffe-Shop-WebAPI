namespace Coffe_Shop_WebAPI.Interface
{
    public interface IRepository<T> where T : class 
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Update(T entity);
        public void Delete(T entity);
        public void Add(T entity);
        public void Save();
        public List<T>? getElements(Func<T, bool> predicate,string? include);
        public T? getElement(Func<T, bool> predicate, string? include);


    }
}
