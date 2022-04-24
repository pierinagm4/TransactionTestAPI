using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionTestAPI.Models;

namespace TransactionTestAPI.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly TransactionDBContext _context;
        public ClienteController(TransactionDBContext transactionDBContext)
        {
            _context = transactionDBContext;
        }

        // GET: api/cliente/5
        [HttpGet("{name}")]
        public async Task<IActionResult> GetClienteByName([FromRoute] string name)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Personas.FirstOrDefaultAsync(cli => cli.Nombre == name);
            var persona = await _context.Clientes.FirstOrDefaultAsync(cli => cli.Persona.Nombre == name);

            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente([FromRoute] int id, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/cliente
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Created($"/?name={cliente.Persona.Nombre}", cliente);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(cli => cli.ClienteId == id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.ClienteId == id);
        }

    }
}
