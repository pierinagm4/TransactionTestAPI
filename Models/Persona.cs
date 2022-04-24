using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TransactionTestAPI.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Clientes = new List<Cliente>();
        }

        public int PersonaId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public int Edad { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual List<Cliente> Clientes { get; set; }
    }
}
