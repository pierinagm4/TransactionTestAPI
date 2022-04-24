using System;
using System.Collections.Generic;

namespace TransactionTestAPI.Models
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new List<Movimiento>();
        }

        public int CuentaId { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public int TipoId { get; set; }
        public decimal SaldoInicial { get; set; }
        public int Estado { get; set; }
        public int ClienteId { get; set; }

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual TiposCuentum Tipo { get; set; } = null!;
        public virtual List<Movimiento> Movimientos { get; set; }
    }
}
