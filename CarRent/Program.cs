using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CarRent.DAL.DataModel;
using CarRent.Service;
using CarRent.Repository.Repositories;
using CarRent.DAL;
using CarRent.Repository;
using CarRent.Common.CacheProvider;
using CarRent.Service.Models;
using CarRent.Service.Services.EmailSender;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("CarRentDb");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();//.AddRazorRuntimeCompilation(); 

builder.Services.AddMemoryCache();

builder.Services.AddIdentity<User, IdentityRole>() // use this options if you want to implement email confirmation: (options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddMvc(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddXmlSerializerFormatters();

#region Emails and personal data

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
   o.TokenLifespan = TimeSpan.FromHours(3));

#endregion

builder.Services.AddScoped<DbContext, ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});

#region Services

builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<ICacheProvider, CacheProvider>();

#endregion

#region Repositories

builder.Services.AddScoped<IRepositoryBase<Rent>, RentRepository>();
builder.Services.AddScoped<IRepositoryBase<Vehicle>, VehicleRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();