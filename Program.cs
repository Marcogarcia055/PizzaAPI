using Pizzeria.Repositories;
using Pizzeria.Repositories.Interface;
using Pizzeria.Service;
using Pizzeria.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configurar WebRootPath si no existe wwwroot
builder.Environment.WebRootPath ??= Path.Combine(builder.Environment.ContentRootPath, "wwwroot");

// Leer la cadena de conexión desde appsettings.json
builder.Services.AddControllers();

// Inyección de dependencias para Repositories
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ICorteRepository, CorteRepository>();

// Inyección de dependencias para Services
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<ICorteService, CorteService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()   // Permite cualquier origen (frontend)
                  .AllowAnyMethod()   // Permite GET, POST, PUT, DELETE
                  .AllowAnyHeader();  // Permite cualquier header
        });
});

var app = builder.Build();

// Configuración del pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar CORS
app.UseCors("AllowAll");

// 🔹 Habilitar servir archivos estáticos desde wwwroot
app.UseStaticFiles();

app.MapControllers(); // Habilita los Controllers

app.Run();