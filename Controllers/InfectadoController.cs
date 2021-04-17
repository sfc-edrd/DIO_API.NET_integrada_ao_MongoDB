using System;
using API.NET_integrada_ao_MongoDB.Data.Collections;
using API.NET_integrada_ao_MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.NET_integrada_ao_MongoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDb _mongoDB;
        IMongoCollection<Infectado> _infectadoCollection;

        public InfectadoController(Data.MongoDb mongoDb)
        {
            _mongoDB = mongoDb;
            _infectadoCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower()); 
        }
        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadoCollection.InsertOne(infectado);
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados()
        {
            var infectados = _infectadoCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizaInfectado([FromBody] InfectadoDto dto)
        {
            _infectadoCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => 
                    _.DataNascimento == dto.DataNascimento),
                    Builders<Infectado>.Update.Set("sexo", dto.Sexo));

            return Ok($"{dto.Sexo} Atualizado com sucesso");
        }

        [HttpDelete("{dataNasc}")]
        public ActionResult Delete(DateTime dataNasc)
        {
            _infectadoCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => 
                    _.DataNascimento == dataNasc ));
            
            return Ok("Removido com sucesso");
        }


        
    }
}