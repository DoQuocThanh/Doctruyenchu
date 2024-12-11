using ApplicationCore.Interfaces;
using ApplicationInfrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebMVC.Common;
using WebMVC.Interfaces;
using WebMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<GenerateJwtToken>();
builder.Services.AddScoped<ImportService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
// Add Service to the service 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIndexService, IndexService>();
builder.Services.AddScoped<IStoryService, StoryService>();

// Đảm bảo IConfiguration đã có sẵn
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

// Add DbContext for MySQL
builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Setting Authentication

// Lấy cấu hình từ appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Đọc giá trị từ cấu hình
var secretKey = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
var expireInDays = int.Parse(jwtSettings["ExpireInDays"]);

// Cấu hình JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            // Lấy token từ cookie
            var token = context.Request.Cookies["JwtToken"];
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token;
            }
            return Task.CompletedTask;
        }
    };

    // Cấu hình token validation parameters từ appsettings.json
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidIssuer = issuer,
        ValidAudience = audience
    };
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

app.UseAuthentication(); // Xác thực JWT
app.UseAuthorization();  // Ủy quyền dựa trên roles/policies

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
