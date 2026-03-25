using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// 🔥 SESSION
builder.Services.AddSession();

// 🔥 BANCO
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db")
);

// 🔥 SERVICES
builder.Services.AddScoped<ProdutoService>(); // <-- FALTAVA
builder.Services.AddScoped<UsuarioService>(); // <-- se tiver login
builder.Services.AddTransient<EmailService>();

var app = builder.Build();

// PIPELINE

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔥 SESSION
app.UseSession();

app.UseAuthorization();

// 🔥 ROTA PADRÃO (LOGIN OK)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();