using POS_service_customers.Context;
using POS_service_customers.Services;
using POS_service_customers.Utils.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Custom Configurations */
builder.Services.AddDbContext<CustomerDbContext>(opt => opt.UseSqlServer("name=DefaultConnection"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelFilter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors();

app.MapControllers();

app.Run();

