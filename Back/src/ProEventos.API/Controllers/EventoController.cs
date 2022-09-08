    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProEventos.API.Model;

    namespace ProEventos.API.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class EventoController : ControllerBase
        {

            public IEnumerable<Evento> _evento = new Evento[]
            {
                new Evento()
                {
                    EventoID = 1,
                    Tema = "Angular e .NET 5",
                    Local = "Belo Horizonte",
                    Lote = "1º Lote",
                    QtdPessoas = 250,
                    DataEvento = DateTime.Now.AddDays(2).ToString()
                },
                new Evento()
                {
                    EventoID = 2,
                    Tema = "Angular e suas Novidades",
                    Local = "São Paulo",
                    Lote = "2º Lote",
                    QtdPessoas = 350,
                    DataEvento = DateTime.Now.AddDays(2).ToString()
                }
            };

            public EventoController()
            {
            }

            [HttpGet]
            public IEnumerable<Evento> Get()
            {
                return _evento;
            }

            [HttpGet("{id}")]
            public IEnumerable<Evento> GetById(int id)
            {
                return _evento.Where(evento => evento.EventoID.Equals(id));
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
    }
