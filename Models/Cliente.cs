using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TransactionTestAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new List<Cuenta>();
        }

        public int ClienteId { get; set; }

        public int PersonaId { get; set; }
        public string Clave { get; set; } = null!;
        public int Estado { get; set; }

        public virtual Persona Persona { get; set; } = null!;
        public virtual List<Cuenta> Cuenta { get; set; }
    }
}
