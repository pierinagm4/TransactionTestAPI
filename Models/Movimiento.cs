using System;
using System.Collections.Generic;

namespace TransactionTestAPI.Models
{
    public partial class Movimiento
    {
        public int MovimientoId { get; set; }
        public int CuentaId { get; set; }
        public int TipoId { get; set; }
        public decimal Monto { get; set; }
        public int Estado { get; set; }

        public DateTime Fecha { get; set; }

        public virtual Cuenta Cuenta { get; set; } = null!;
        public virtual TiposMovimiento Tipo { get; set; } = null!;
    }
}
