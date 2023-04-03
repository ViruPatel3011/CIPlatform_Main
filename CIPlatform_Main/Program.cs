using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Repository.Interface;
using CIPlatform_Main.Repository.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CiPlatformContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped< IRegistartionRepository, RegistrationRepository> ();
builder.Services.AddScoped<ILoginRepository, LoginRepository> ();
builder.Services.AddScoped<IForgotRepository, ForgotRepository> ();
builder.Services.AddScoped<IResetPasswordRepository, ResetPasswordRepository> ();
builder.Services.AddScoped<ILoginRepository, LoginRepository> ();
builder.Services.AddScoped<ILandingPage, LandingPageRepository> ();
builder.Services.AddScoped<IMissionAndRating, MissionAndRatingRepository> ();
builder.Services.AddScoped<IStoryRepository, StoryRepository> ();
builder.Services.AddScoped<IUserRepository, UserRepository> ();
/*builder.Services.AddCloudscribePagination();*/
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
    AddCookie(option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60 * 1);
        option.LoginPath = "/Login/Login";
        option.AccessDeniedPath = "/Login/Login";
    });
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(5);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});



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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=LandingPage}/{id?}");

app.Run();
