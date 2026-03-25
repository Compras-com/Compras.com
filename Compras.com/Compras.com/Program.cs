using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurações Iniciais
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Configuração do Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// ÚNICA ADIÇÃO NECESSÁRIA: Garante que o banco e as tabelas existam no Render
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

// ROTA PADRÃO (Voltando para Home/Index que é o padrão do VS)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();