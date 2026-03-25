using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurações do seu projeto original
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Conexão com o Banco
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// ESSA LINHA RESOLVE O ERRO 'NO SUCH TABLE' SEM QUEBRAR O SITE
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
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

// ROTA PADRÃO (Onde o site começa)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();