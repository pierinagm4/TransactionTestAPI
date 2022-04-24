using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionTestAPI.Models;

namespace TransactionTestAPI.Controllers
{
    [Route("api/reporte")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly TransactionDBContext _context;
        public ReporteController(TransactionDBContext transactionDBContext)
        {
            _context = transactionDBContext;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> GetMovimientoByNumber([FromRoute] string number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Reporte> listrep = new List<Reporte>();
            var movimiento = await _context.Movimientos.Where(x => x.Cuenta.NumeroCuenta == number).ToListAsync();

            if (movimiento == null)
            {
                return NotFound();
            }
            else
            {
                foreach (Movimiento obj in movimiento) {
                    var tipo = await _context.TiposCuenta.FindAsync(obj.TipoId);
                    var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.CuentaId == obj.CuentaId);
                    if (cuenta != null)
                    {
                        var clientes = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == cuenta.ClienteId);
                        if (clientes != null)
                        {
                           var persona = await _context.Personas.FirstOrDefaultAsync(c => c.PersonaId == clientes.PersonaId);
                        }

                        var valor = "";
                        decimal saldoDisp= 0;
                        if (obj.TipoId == 2) {
                            valor = "-" + obj.Monto;
                            saldoDisp = obj.Cuenta.SaldoInicial - obj.Monto;
                        }
                        else {
                            valor = "+" + obj.Monto;
                            saldoDisp = obj.Cuenta.SaldoInicial + obj.Monto;
                        }

                        Reporte rep = new Reporte
                        {
                            Fecha = obj.Fecha,
                            NombrePersona = obj.Cuenta.Cliente.Persona.Nombre,
                            NumCuenta = obj.Cuenta.NumeroCuenta,
                            TipoCuenta = obj.Cuenta.Tipo.Nombre,
                            SaldoInicial = obj.Cuenta.SaldoInicial,
                            Estado = obj.Estado == 1 ? true : false,
                            Valor = valor,
                            SaldoDisponible = saldoDisp
                        };

                        listrep.Add(rep);
                    }
                }
            }

            return Ok(listrep);
        }


    }
}
