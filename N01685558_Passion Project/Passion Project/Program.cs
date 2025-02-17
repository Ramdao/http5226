using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Passion_Project.Data;
using Passion_Project.Interfaces;
using Passion_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add session services
builder.Services.AddDistributedMemoryCache();  // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Set session timeout duration
    options.Cookie.HttpOnly = true;  // Ensure the cookie is only accessible through HTTP
    options.Cookie.IsEssential = true;  // Mark session cookie as essential
});

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEntriesService, EntriesService>();
builder.Services.AddScoped<ITimelineService, TimelineService>();
builder.Services.AddScoped<IUserTimeline, UserTimelineService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add session middleware before authorization and other middlewares
app.UseSession();

app.UseAuthorization();

// Swagger for API documentation
app.UseSwagger();
app.UseSwaggerUI();

// Default route configuration
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
