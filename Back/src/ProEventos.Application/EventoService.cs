using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this._geralPersist = geralPersist;
            this._eventoPersist = eventoPersist;
        }

        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id,false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
            try
            {  
                var evento = await _eventoPersist.GetEventoByIdAsync(model.Id, false);
                if (evento is null) return null;

                model.Id = evento.Id;

                _geralPersist.Update(model);
                if (await _geralPersist.SaveChangesAsync())
                {
                    return await _eventoPersist.GetEventoByIdAsync(model.Id,false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
               
                var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
                if (evento is null) throw new Exception("Evento não encontrado para excluir.");

                _geralPersist.Delete<Evento>(evento);
                return await _geralPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
                return eventos is null ? null : eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
                return eventos is null ? null : eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);
                return eventos is null ? null : eventos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}