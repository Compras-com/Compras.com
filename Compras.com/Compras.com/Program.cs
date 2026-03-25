using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIGURAÇÃO DE SERVIÇOS
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// 2. CONFIGURAÇÃO DO BANCO (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// 3. BLOCO CORINGA: CRIA O BANCO E AS TABELAS AO LIGAR
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();
        Console.WriteLine("Banco de dados e tabelas verificados com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao iniciar banco: " + ex.Message);
    }
}

// 4. CONFIGURAÇÃO DO AMBIENTE
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

// 5. ROTA PRINCIPAL (Ajustada para abrir no Login)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuarios}/{action=Login}/{id?}");

app.Run();