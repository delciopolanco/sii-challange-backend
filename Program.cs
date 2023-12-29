using System.Text.Json;
using addCard_backend.Context;
using addCard_backend.Services;
using addCard_backend.Utils.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
               options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
           });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* Custom Configurations */
builder.Services.AddDbContext<CreditCardContext>(opt => opt.UseSqlServer("name=DefaultConnection"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<CreditCardRepository>();
builder.Services.AddScoped<SecurityService>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelFilter());
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var creditCardContext = scope.ServiceProvider.GetRequiredService<CreditCardContext>();
        creditCardContext.Database.EnsureCreated();
    }
}

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
