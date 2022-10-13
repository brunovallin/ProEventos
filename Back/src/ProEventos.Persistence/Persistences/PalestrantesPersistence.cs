using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence.Persistences
{
    public class PalestrantesPersistence : IPalestrantePersistence
    {
        private readonly ProEventosContext _context;
        public PalestrantesPersistence(ProEventosContext context)
        {
            _context = context;
        }
        
        public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteID, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                            .Include(palestrante => palestrante.RedesSociais);

            if(includeEventos)
                query = query
                            .Include(E => E.PalestrantesEventos)
                            .ThenInclude(Pe =>Pe.Evento);
            

            query = query.OrderBy(palestrantes => palestrantes.Id).Where(palestrante => palestrante.Id.Equals(palestranteID));
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                            .Include(palestrante => palestrante.RedesSociais);

            if(includeEventos)
                query = query
                            .Include(E => E.PalestrantesEventos)
                            .ThenInclude(Pe =>Pe.Evento);
            

            query = query.OrderBy(palestrantes => palestrantes.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes.AsNoTracking()
                                            .Include(palestrante => palestrante.RedesSociais);

            if(includeEventos)
                query = query
                            .Include(E => E.PalestrantesEventos)
                            .ThenInclude(Pe =>Pe.Evento);
            

            query = query.OrderBy(palestrantes => palestrantes.Id).Where(palestrante => palestrante.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }
    }
}