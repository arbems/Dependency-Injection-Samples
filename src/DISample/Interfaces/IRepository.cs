namespace DemoDI.Domain
{
    public interface IRepository
    {
        void Add(object entity);
        void Update(object entity);
        void Delete(object entity);
        // Otros métodos comunes
    }
}
