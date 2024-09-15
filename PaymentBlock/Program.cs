using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentBlockAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentBlockAPI", Version = "v1" });
    // ¬ключение XML-комментариев, если они используютс€
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// builder.Services.AddDbContext<PaymentBlockDbContext>(options => options.UseInMemoryDatabase("PaymentBlockDb"));
builder.Services.AddDbContext<PaymentBlockDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentBlockConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
