using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DE SERVIÇOS ---
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Configuração do Banco de Dados SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

// [ADICIONADO] Configuração de Porta para o Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5160";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// --- CONFIGURAÇÃO DO PIPELINE DE REQUISIÇÕES ---

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// [AJUSTADO] Rota Padrão: Agora o site abre direto no seu Login de Usuarios
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

app.Run();