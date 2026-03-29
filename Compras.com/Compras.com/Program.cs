using Compras.com.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CONEXÃO COM O BANCO PAGO
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)); 

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5160";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// GARANTE QUE AS TABELAS SEJAM CRIADAS
using (var scope = app.Services.CreateScope())
{
    try {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated(); 
    } catch { }
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();