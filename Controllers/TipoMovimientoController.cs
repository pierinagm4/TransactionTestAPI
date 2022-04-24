using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionTestAPI.Models;

namespace TransactionTestAPI.Controllers
{
    [Route("api/tipoMovimiento")]
    [ApiController]
    public class TipoMovimientoController : ControllerBase
    {
        private readonly TransactionDBContext _transactionDBContext;
        public TipoMovimientoController(TransactionDBContext transactionDBContext)
        {
            _transactionDBContext = transactionDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var  result = await _transactionDBContext.TiposMovimientos.ToListAsync();
            return result != null ? Ok(result) : NotFound();
        }

        [HttpGet("{name}")]
        public IResult GetByName([FromRoute] string name)
        {
            TiposMovimiento result = _transactionDBContext.TiposMovimientos.FirstOrDefault(tip => tip.Nombre == name);
            return result != null ? Results.Ok(result) : Results.NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TiposMovimiento tiposMovimiento)
        {
            _transactionDBContext.TiposMovimientos.Add(tiposMovimiento);
            await _transactionDBContext.SaveChangesAsync();
            return Created($"/getByID?id={tiposMovimiento.TipoId}", tiposMovimiento);
        }

        [HttpGet]
        [Route("getByID")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var tipo = await _transactionDBContext.TiposMovimientos.FindAsync(id);
            return Ok(tipo);
        }

    }
}
