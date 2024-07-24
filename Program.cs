using GitHubRepositories.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GitHubRepositories.Middlewares;
using Microsoft.AspNetCore.DataProtection;
using System.Net;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var issuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var audience = builder.Configuration.GetSection("Jwt:Audience").Get<string>();
var key = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var gitHubBaseUrl = builder.Configuration.GetSection("gitHub:baseUrl").Get<string>();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.Domain = "localhost";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:4200"); 
            builder.AllowAnyHeader();
            builder.WithMethods(["GET", "POST", "DELETE", "OPTION"]);
            builder.AllowCredentials();
        });
});
builder.Services.AddHttpClient<HttpClient>("github", client => {
    client.BaseAddress = new Uri(gitHubBaseUrl);
    client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("github", "1.0"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddScoped<IUserSession, UserSessionService>();
builder.Services.AddScoped<IRepository, RepositoryService>();
builder.Services.AddScoped<IBookmark, BookmarkService>();
builder.Services.AddScoped<IAuth, AuthService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();
app.UseCors();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
