using Compras.com.Data;
using Compras.com.Models; // Certifique-se que o namespace do seu modelo de Usuário está certo
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DO BANCO PAGO ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)); 

builder.Services.AddControllersWithViews();
builder.Services.AddSession();

var port = Environment.GetEnvironmentVariable("PORT") ?? "5160";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// --- ESTE BLOCO CRIA O BANCO E O USUÁRIO ADMIN AUTOMATICAMENTE ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated(); 

        // Verifica se já existe algum usuário. Se não existir, ele cria o seu.
        if (!context.Usuarios.Any()) 
        {
            context.Usuarios.Add(new Usuario 
            { 
                Nome = "Admin", 
                Email = "admin@admin.com", // Coloque o email que você usa
                Senha = "123" // Coloque a senha que você quer usar
            });
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao configurar banco ou criar admin.");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();