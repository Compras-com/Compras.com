using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Serviços
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Conexão com o Banco de Dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// Bloco que garante a criação das tabelas (visto nos seus logs)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao criar banco: " + ex.Message);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// COMENTE esta linha abaixo para o Render não se perder no redirecionamento
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// ROTA DEFINITIVA: Aponta para Usuarios/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

// AJUSTE DE PORTA: Ele vai usar a 5160 se estiver no Github, 
// ou a porta que o Render mandar quando estiver online.
var port = Environment.GetEnvironmentVariable("PORT") ?? "5160";
app.Run($"http://0.0.0.0:{port}");