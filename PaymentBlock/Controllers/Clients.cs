﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentBlockAPI.Data;
using PaymentBlockAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PaymentBlockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly PaymentBlockDbContext dbContext;

        public ClientsController(PaymentBlockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Получить список клиентов
        /// </summary>

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await dbContext.Clients.ToListAsync());
        }

        /// <summary>
        /// Получить данные конкретного клиента
        /// </summary>

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetClient([FromRoute] Guid id)
        {
            var client = await dbContext.Clients.FindAsync(id);
            if (client != null)
            {
                return Ok(client);
            }

            return NotFound("Клиент не найден");
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>

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

        /// <summary>
        /// Изменить данные клиента
        /// </summary>

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

        /// <summary>
        /// Удалить клиента
        /// </summary>

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
    }

}
