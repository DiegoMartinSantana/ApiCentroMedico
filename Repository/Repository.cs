
using ApiCentroMedico.Dto.Obras_Sociales;
using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private CentromedicoContext _context;
        private DbSet<TEntity> _dbset;
        public Repository(CentromedicoContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
            
        }
        public void Delete(TEntity entity) =>  _dbset.Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAll() => await _dbset.Select(x=>x).ToListAsync();

        public async Task<TEntity?> GetById(int id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await _dbset.AddAsync(entity);
            await Save();
            // actualiza el id automaticamente
            return entity;

        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _dbset.Entry(entity).State = EntityState.Modified;

        }
    }
}
