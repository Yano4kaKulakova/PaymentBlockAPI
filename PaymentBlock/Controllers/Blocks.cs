using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentBlockAPI.Data;
using PaymentBlockAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentBlockAPI.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class BlocksController : Controller
    {
        private readonly PaymentBlockDbContext dbContext;

        public BlocksController(PaymentBlockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Получить список блокировок клиентов
        /// </summary>

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await dbContext.Blocks.ToListAsync());
        }
    }
}