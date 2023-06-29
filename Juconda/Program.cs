using Juconda.Core.Mappings;
using Juconda.Core.Services;
using Juconda.Domain.Models.Users;
using Juconda.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddMvc();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

var connection = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(connection));

builder.Services.AddScoped<InitializeService>();
builder.Services.AddScoped<ShopService>();

builder.Services.AddAuthentication();

builder.Services.AddAutoMapper(c => c.AddProfile(new MappingProfile()));

// добавление сервисов Idenity
builder.Services.AddIdentity<User, Juconda.Domain.Models.Identity.IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;

    options.User.RequireUniqueEmail = false;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(24);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
})
.AddEntityFrameworkStores<AppDbContext>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.AccessDeniedPath = "/admin/Account/AccessDenied";
//    options.LoginPath = "/admin/Account/Login";
//});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
app.Services.CreateScope().ServiceProvider.GetRequiredService<InitializeService>();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapAreaControllerRoute(
    "admin",
    "admin",
    "admin/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    "AdminLogin",
    "admin/Account/Login",
    "admin/{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "UserLogin",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();