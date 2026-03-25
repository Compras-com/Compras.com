using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o suporte ao MVC (que é o que desenha as telas)
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

// Configura o banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db"));

var app = builder.Build();

// ESSA É A ÚNICA LINHA QUE PRECISAMOS: 
// Ela apenas cria as tabelas se elas não existirem. Não muda nada nas telas.
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

// ROTA PADRÃO DO .NET
// Se o seu site abria no Login antes, ele voltará a abrir agora.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();