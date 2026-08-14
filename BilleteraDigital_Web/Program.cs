using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// --- 1. ZONA DE SERVICIOS (ANTES DEL BUILD) ---
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Si alguien intenta entrar sin sesión, lo manda aquí
        options.ExpireTimeSpan = TimeSpan.FromHours(2); // La sesión dura 2 horas
    });

// ¡La construcción de la app va justo después de registrar los servicios!
var app = builder.Build();


// --- 2. ZONA DE MIDDLEWARES (DESPUÉS DEL BUILD) ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// El orden estricto de seguridad:
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();