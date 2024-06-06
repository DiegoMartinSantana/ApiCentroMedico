namespace ApiCentroMedico.Repository
{
    public interface IRepository<TEntity>
    {
        public Task<IEnumerable<TEntity>> GetAll();
        public Task<TEntity?> GetById(int id);
        public Task<TEntity> Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        public Task Save();
    }
}
