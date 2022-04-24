using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionTestAPI.Models;

namespace TransactionTestAPI.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly TransactionDBContext _context;
        public CuentasController(TransactionDBContext transactionDBContext)
        {
            _context = transactionDBContext;
        }

        // GET: api/cuentas/5030483
        [HttpGet("{number}")]
        public async Task<IActionResult> GetCuentaByNumber([FromRoute] string number)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == number);
           
            if (cuenta == null)
            {
                return NotFound();
            }
            else {
                var cliente = await _context.Clientes.FindAsync(cuenta.CuentaId);
                var persona = await _context.Personas.FirstOrDefaultAsync(p => p.PersonaId == cliente.PersonaId);
            }
           

            return Ok(cuenta);
        }

        // PUT: api/cuentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCuenta([FromRoute] int id, [FromBody] Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cuenta.CuentaId)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CuentaExists(id))
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

        // POST: api/cuentas
        [HttpPost]
        public async Task<IActionResult> PostCuenta([FromBody] Cuenta cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataCliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == cuenta.ClienteId);
            var datatipo = _context.TiposCuenta.FirstOrDefault(c => c.TipoCuentaId == cuenta.TipoId);
            _context.Cuentas.Add(new Cuenta
            {
                NumeroCuenta = cuenta.NumeroCuenta,
                SaldoInicial = cuenta.SaldoInicial,
                Estado = cuenta.Estado,
                Tipo = datatipo != null ? datatipo : cuenta.Tipo,
                Cliente = dataCliente != null ? dataCliente : cuenta.Cliente
            });
            await _context.SaveChangesAsync();

            return Created($"/?number={cuenta.NumeroCuenta}", cuenta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCuenta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cuenta = await _context.Cuentas.FirstOrDefaultAsync(c => c.CuentaId == id);
            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return Ok(cuenta);
        }

        private bool CuentaExists(int id)
        {
            return _context.Cuentas.Any(e => e.ClienteId == id);
        }

    }
}
