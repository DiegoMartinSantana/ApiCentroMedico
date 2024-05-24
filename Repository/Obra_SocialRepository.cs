﻿using ApiCentroMedico.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCentroMedico.Repository
{
    public class Obra_SocialRepository : IRepository<ObrasSociale>
    {
        private DiagnosticoContext _context;
        public Obra_SocialRepository(DiagnosticoContext context)
        {
            _context = context;
        }

        public void Delete(ObrasSociale entity) => _context.Remove(entity); 

        public async Task<IEnumerable<ObrasSociale>> GetAll() => await _context.ObrasSociales.ToListAsync<ObrasSociale>();
        public async Task<ObrasSociale> GetById(int id)
        {
            var Obra = await _context.ObrasSociales.FindAsync(id);
            return Obra == null ? null : Obra;

        }

        public async Task Insert(ObrasSociale entity) => await _context.ObrasSociales.AddAsync(entity); 

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(ObrasSociale entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }
    }
}
