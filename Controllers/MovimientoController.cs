using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionTestAPI.Models;

namespace TransactionTestAPI.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly TransactionDBContext _context;
        public MovimientoController(TransactionDBContext transactionDBContext)
        {
            _context = transactionDBContext;
        }

        // GET: api/Movimientos/5030483
        [HttpGet("{number}")]
        public async Task<IActionResult> GetMovimientoByNumber([FromRoute] string number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimiento = await _context.Movimientos.FirstOrDefaultAsync(c => c.Cuenta.NumeroCuenta == number);

            if (movimiento == null)
            {
                return NotFound();
            }
            else
            {
                var cliente = await _context.TiposCuenta.FindAsync(movimiento.TipoId);
                var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.CuentaId == movimiento.CuentaId);
                if (cuenta != null) {
                    var clientes = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == cuenta.ClienteId);
                    if (clientes != null)
                    {
                        var persona = await _context.Personas.FirstOrDefaultAsync(c => c.PersonaId == clientes.PersonaId);
                    }
                }
            }

            return Ok(movimiento);
        }

        // PUT: api/Movimientos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimiento([FromRoute] int id, [FromBody] Movimiento Movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Movimiento.MovimientoId)
            {
                return BadRequest();
            }

            _context.Entry(Movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimientoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movimientos
        [HttpPost]
        public async Task<IActionResult> PostMovimiento([FromBody] Movimiento movimiento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataCuenca = _context.Cuentas.FirstOrDefault(c => c.CuentaId == movimiento.CuentaId);
            var datatipo = _context.TiposMovimientos.FirstOrDefault(c => c.TipoId == movimiento.TipoId);
            var saldo = dataCuenca.SaldoInicial;
            decimal calculo = 0;

            if (saldo > movimiento.Monto)
            {
                _context.Movimientos.Add(new Movimiento
                {
                    Monto = movimiento.Monto,
                    Estado = movimiento.Estado,
                    Tipo = datatipo != null ? datatipo : movimiento.Tipo,
                    Cuenta = dataCuenca != null ? dataCuenca : movimiento.Cuenta
                });
                await _context.SaveChangesAsync();

                String num = dataCuenca != null ? dataCuenca.NumeroCuenta : movimiento.Cuenta.NumeroCuenta;

                if (movimiento.TipoId == 2) {
                    calculo = dataCuenca.SaldoInicial - movimiento.Monto;
                }
                else {
                    calculo = dataCuenca.SaldoInicial + movimiento.Monto;
                }

                _context.Cuentas.Add(new Cuenta
                {
                    NumeroCuenta = dataCuenca.NumeroCuenta,
                    SaldoInicial = calculo,
                    Estado = dataCuenca.Estado,
                    Tipo = dataCuenca.Tipo,
                    Cliente = dataCuenca.Cliente
                });
                await _context.SaveChangesAsync();

                return Created($"/?number={num}", movimiento);
            }
            else {
                if (movimiento.TipoId != 2)
                {
                    _context.Movimientos.Add(new Movimiento
                    {
                        Monto = movimiento.Monto,
                        Estado = movimiento.Estado,
                        Tipo = datatipo != null ? datatipo : movimiento.Tipo,
                        Cuenta = dataCuenca != null ? dataCuenca : movimiento.Cuenta
                    });
                    await _context.SaveChangesAsync();

                    calculo = dataCuenca.SaldoInicial + movimiento.Monto;

                    _context.Cuentas.Add(new Cuenta
                    {
                        NumeroCuenta = dataCuenca.NumeroCuenta,
                        SaldoInicial = calculo,
                        Estado = dataCuenca.Estado,
                        Tipo = dataCuenca.Tipo,
                        Cliente = dataCuenca.Cliente
                    });
                    await _context.SaveChangesAsync();

                    String num = dataCuenca != null ? dataCuenca.NumeroCuenta : movimiento.Cuenta.NumeroCuenta;
                    return Created($"/?number={num}", movimiento);
                }
                else {
                    return StatusCode(418, "Saldo no disponible.");
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movimiento = await _context.Movimientos.FirstOrDefaultAsync(c => c.MovimientoId == id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return Ok(movimiento);
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MovimientoId == id);
        }
    }
}
