using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace API.NET_integrada_ao_MongoDB.Data.Collections
{
    public class Infectado
    {
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }

        public Infectado(DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
    }
}