using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DE SERVIÇOS ---
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Configuração do Banco de Dados SQLite (O arquivo compras.db será criado na raiz)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// --- BLOCO DE CRIAÇÃO DO BANCO (ESSENCIAL PARA O RENDER) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Cria o arquivo compras.db e as tabelas Usuarios, Produtos, etc. automaticamente
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao inicializar o banco: " + ex.Message);
    }
}

// --- CONFIGURAÇÃO DO PIPELINE ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Comentado para evitar conflitos de porta/protocolo no Render
// app.UseHttpsRedirection(); 

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// --- ROTA DE LOGIN (PADRÃO) ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

// --- CONTROLE DE PORTA (O SEGREDO DO EQUILÍBRIO) ---
// Se o Render enviar uma porta, ele usa. Se não, usa a sua 5160 do GitHub.
var port = Environment.GetEnvironmentVariable("PORT") ?? "5160";
app.Run($"http://0.0.0.0:{port}");