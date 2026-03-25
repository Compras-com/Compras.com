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

// CRIAÇÃO AUTOMÁTICA DE TABELAS (Resolve o erro 500)
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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

// ROTA PADRÃO: Tenta abrir a Home. Se o seu sistema exigir login, 
// ele redirecionará automaticamente para a tela de Usuarios/Login.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();