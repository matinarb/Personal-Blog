using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PersonalBlog.CoreLayer.Services.Categories;
using PersonalBlog.CoreLayer.Services.Comment;
using PersonalBlog.CoreLayer.Services.FileManager;
using PersonalBlog.CoreLayer.Services.MainPage;
using PersonalBlog.CoreLayer.Services.Posts;
using PersonalBlog.CoreLayer.Services.Users;
using PersonalBlog.DataLayer.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddTransient<IFileManager, FileManager>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IMainPageService, MainPageService>();

// DataBase
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Authorization
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AdminPolicy",builder =>
    {
        builder.RequireRole("Admin");
    });
});

// Authentication
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/login";
    option.LogoutPath = "/logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
    option.AccessDeniedPath = "/AccessDenied";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Default",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapRazorPages();

app.Run();
