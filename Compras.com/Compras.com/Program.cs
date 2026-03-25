using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// // MVC
builder.Services.AddControllersWithViews();

// // SESSION
builder.Services.AddSession();

// // BANCO (Configurado para rodar no Render e no Local)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// --- BLOCO CORINGA: CRIA AS TABELAS AUTOMATICAMENTE ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        // Isso resolve o erro "no such table: Usuarios"
        context.Database.EnsureCreated();
        Console.WriteLine("Banco de dados e tabelas prontos!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao iniciar banco: " + ex.Message);
    }
}
// -----------------------------------------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Habilita a Session que você adicionou no builder
app.UseSession(); 

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();