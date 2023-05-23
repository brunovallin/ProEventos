using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventoController : ControllerBase
{
    public IEnumerable<Evento> _eventos = new Evento[] {
        new Evento()
        {
            EventoId = 1,
            Tema= "Angular 11 e .NET 5",
            Local = "São Paulo",
            Lote = "1° lote",
            QtdPessoas = 250,
            DataEvento = DateTime.Now.AddDays(5).ToString(),
            ImagemURL = "foto.png"
        },
        new Evento()
        {
            EventoId = 2,
            Tema= "Angular e suas novidades",
            Local = "São Paulo",
            Lote = "2° lote",
            QtdPessoas = 350,
            DataEvento = DateTime.Now.AddDays(8).ToString(),
            ImagemURL = "foto1.png"
        }
    };

    public EventoController()
    {
    }

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
        return _eventos;
    }

    [HttpGet("{id}")]
    public IEnumerable<Evento> GetbyId(int id)
    {
        return _eventos.Where(evento => evento.EventoId == id);
    }


    [HttpPost]
    public string Post()
    {
        return "Exemplo de Post";
    }
    [HttpPut("{id}")]
    public string Put(int id)
    {
        return $"Exemplo de Put com id = {id}";
    }

    [HttpDelete("{id}")]
    public string Delete(int id)
    {
        return $"Exemplo de Delete com id = {id}";
    }
}
