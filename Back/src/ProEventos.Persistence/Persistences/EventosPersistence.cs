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
    public class EventosPersistence : IEventosPersistence
    {
        private readonly ProEventosContext _context;
        public EventosPersistence(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoID, bool includePalestrantes=false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                                            .Include(evento => evento.Lotes)
                                            .Include(evento => evento.RedesSociais);

            if(includePalestrantes)
                query = query
                        .Include(E => E.PalestrantesEventos)
                        .ThenInclude(Pe =>Pe.Palestrante);
            
            
            query = query.OrderBy(eventos => eventos.Id)
                .Where(e=>e.Id.Equals(eventoID));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes=false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                                            .Include(evento => evento.Lotes)
                                            .Include(evento => evento.RedesSociais);

            if(includePalestrantes)
                query = query
                            .Include(E => E.PalestrantesEventos)
                            .ThenInclude(Pe =>Pe.Palestrante);
            

            query = query.OrderBy(eventos => eventos.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes=false)
        {
            IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                                            .Include(evento => evento.Lotes)
                                            .Include(evento => evento.RedesSociais);

            if(includePalestrantes)
                query = query
                        .Include(E => E.PalestrantesEventos)
                        .ThenInclude(Pe =>Pe.Palestrante);
            
            
            query = query.OrderBy(eventos => eventos.Id)
                .Where(e=>e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}