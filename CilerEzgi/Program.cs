using CilerEzgi.Data;
using CilerEzgi.Entities;
using CilerEzgi.Models;
using Ideio.Core.Tools;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddScoped<IMailSender,MailSender>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.AccessDeniedPath = "/AccessDenied";
    x.LogoutPath = "/Admin/Logout";
    x.Cookie.Name = "Admin";
    x.Cookie.MaxAge = TimeSpan.FromDays(10);
    x.ExpireTimeSpan = TimeSpan.FromHours(3); // ?? 1 saat oturum süresi
    x.SlidingExpiration = true; // ?? Her iþlemde süre yenilenir
    x.Cookie.IsEssential = true;
    x.Cookie.SameSite = SameSiteMode.Lax; // ?? None yerine Lax önerilir
    x.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use((context, next) =>
{
    var dbcontext = context.RequestServices.GetRequiredService<DatabaseContext>();

    DataRequestModel.ClearData();

    

    DataRequestModel.SiteSetting =
        dbcontext.SiteSettings.FirstOrDefault()
        ?? new SiteSetting(); // null kalmasýn

    DataRequestModel.Policies =
        dbcontext.Policies.ToList();

    return next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
         );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
