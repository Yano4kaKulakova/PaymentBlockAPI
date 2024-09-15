using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentBlockAPI.Data;
using PaymentBlockAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentBlockAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BlockPaymentController : Controller
    {
        private readonly PaymentBlockDbContext dbContext;

        public BlockPaymentController(PaymentBlockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }       

        /// <summary>
        /// Заблокировать платежи клиента
        /// </summary>

        [HttpPost]
        [Route("{id:guid}")]
        public async Task<IActionResult> AddBlock([FromRoute] Guid id, AddBlockRequest addBlockRequest)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client != null)
            {


                var block = new Block()
                {
                    BlockId = Guid.NewGuid(),
                    BlockDateTime = DateTime.Now.ToString(),
                    ClientId = id,
                    Reason = addBlockRequest.Reason,

                };

                client.Status = "Заблокирован";

                await dbContext.Blocks.AddAsync(block);
                await dbContext.SaveChangesAsync();

                return Ok(block);
            }

            return NotFound("Клиент не найден");
        }

        /// <summary>
        /// Разблокировать платежи клиента
        /// </summary>

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClient([FromRoute] Guid id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                var block = await dbContext.Blocks.Where(b => b.ClientId == id && b.UnlockDateTime == " ").FirstOrDefaultAsync();
                if (block != null)
                {
                    block.UnlockDateTime = DateTime.Now.ToString();
                    client.Status = "Активен";
                    await dbContext.SaveChangesAsync();
                    return Ok("Клиент разблокирован");
                }

                return NotFound("Клиент не заблокирован");
            }

            return NotFound("Клиент не найден");
        }

        /// <summary>
        /// Проверить статус и причину блокировки клиента
        /// </summary>

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> CheckClient([FromRoute] Guid id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                
                if (client.Status == "Заблокирован")
                {
                    var block = await dbContext.Blocks.Where(b => b.ClientId == id && b.UnlockDateTime == " ").FirstOrDefaultAsync();
                    return Ok(client.Status + "\n" + block.Reason);
                }

                return Ok(client.Status);
            }

            return NotFound("Клиент не найден");
        }

    }

}
