using System;
using System.Collections.Generic;

namespace TransactionTestAPI.Models
{
    public partial class TiposMovimiento
    {
        public TiposMovimiento()
        {
            Movimientos = new List<Movimiento>();
        }

        public int TipoId { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual List<Movimiento> Movimientos { get; set; }
    }
}
