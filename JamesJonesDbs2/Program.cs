using JamesJonesApplication.Services;
using JamesJonesDbs2.Models.Database;
using JamesJonesDbs2.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UploadEncryption.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Connction String
builder.Services.AddDbContext<DatabaseContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("JamesJonesDbs3")));

//To allow Session Model Binding
builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/AppUser/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/AppUser/DeniedAccess";
});
//Implement Services:
//Sanitiser Service
builder.Services.AddScoped<SaniteserService>();
//Encryption Service
builder.Services.AddScoped<EncryptionService>();
//File Upload Service
builder.Services.AddScoped<FileUploaderService>();
//Authentication and Authorisation 
builder.Services.AddScoped<AuthRepository>();


builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

//Authenticate and then Authorise
app.UseAuthentication();
app.UseAuthorization();

//Breakdown moment
app.Use(async (context, next) =>
{
    context.Response.Headers.Add(
        "Content-Security-Policy", "default-src 'none'; " +
        "img-src 'self' data:; " +
        "style-src 'self'" +
        "style-src-elem 'self'; " +
        "script-src-elem 'self'; " +
        "connect-src 'self';" +
        "form-action 'self'; " +
        "frame-src youtube.com https://www.youtube.com;");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=63072000; includeSubDomains;");

    await next(context);

});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//This comment here is to say to myself that i am on my most updated and recent application version