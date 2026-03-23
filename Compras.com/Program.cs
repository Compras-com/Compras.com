using Compras.com.Data;
using Compras.com.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// 🔥 SESSION (OBRIGATÓRIO)
builder.Services.AddSession();

// BANCO
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=compras.db")
);

// SERVICES
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

// 🔥 SESSION (TEM QUE FICAR AQUI)
app.UseSession();

app.UseAuthorization();

// 🔥 ROTA INICIAL (LOGIN)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();