namespace TransactionTestAPI.Models
{
    public class Reporte
    {
        public DateTime Fecha { get; set; }
        public string NombrePersona { get; set; }
        public string NumCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public Boolean Estado { get; set; }
        public string Valor { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
