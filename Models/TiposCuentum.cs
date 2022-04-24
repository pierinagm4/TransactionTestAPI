using System;
using System.Collections.Generic;

namespace TransactionTestAPI.Models
{
    public partial class TiposCuentum
    {
        public TiposCuentum()
        {
            Cuenta = new List<Cuenta>();
        }

        public int TipoCuentaId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual List<Cuenta> Cuenta { get; set; }
    }
}
