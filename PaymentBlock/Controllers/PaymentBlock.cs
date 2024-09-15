using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentBlockAPI.Data;
using PaymentBlockAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentBlockAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentBlockController : Controller
    {
        private readonly PaymentBlockDbContext dbContext;

        public PaymentBlockController(PaymentBlockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Получение списка клиентов
        /// </summary>
        
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await dbContext.Clients.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClient([FromRoute] Guid id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> AddClient(AddClientRequest addClientRequest)
        {
            var client = new Client()
            {
                Id = Guid.NewGuid(),
                FullName = addClientRequest.FullName,
                Email = addClientRequest.Email,
                Phone = addClientRequest.Phone,
                Address = addClientRequest.Address

            };

            await dbContext.Clients.AddAsync(client);
            await dbContext.SaveChangesAsync();

            return Ok(client);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateClient([FromRoute] Guid id, UpdateClientRequest updateClientRequest)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                client.FullName = updateClientRequest.FullName;
                client.Email = updateClientRequest.Email;
                client.Phone = updateClientRequest.Phone;
                client.Address = updateClientRequest.Address;

                await dbContext.SaveChangesAsync();

                return Ok(client);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteClient([FromRoute] Guid id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                dbContext.Remove(client);
                await dbContext.SaveChangesAsync();

                return Ok(client);
            }

            return NotFound();
        }

        

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


                await dbContext.Blocks.AddAsync(block);
                await dbContext.SaveChangesAsync();

                return Ok(block);
            }

            return NotFound("Клиент не найден");

            

            
        }
    }
}
