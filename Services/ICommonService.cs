namespace ApiCentroMedico.Services
{
    public interface ICommonService<T,TI,TU>
    {
        public Task<IEnumerable<T>> GetAll(); //usamos i enum porque es solo eso, solo es una coleccion de inf, ahorra memoria
        public Task<T> Insert(TI entity);
        public Task<T> Update(int id,TU entity);
        public Task<T> GetById(int id);
        public Task<T> Delete(int id);
    }
}
