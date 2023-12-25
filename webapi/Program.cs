using webapi.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using webapi.Models.Settings;
using webapi.Services.Email;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Azure.Security.KeyVault.Secrets;
using webapi.Services.PasswordHasher;

var builder = WebApplication.CreateBuilder(args);
/*var secretKey = builder.Configuration["Jwt:SecretKey"];*/

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IEmailServices, EmailServices>();

builder.Services.AddSingleton<UserServices>();

builder.Services.AddSingleton<QuestionServices>();
builder.Services.AddSingleton<AnswerServices>();

builder.Services.AddSingleton<DecreeServices>();
builder.Services.AddSingleton<ArticleServices>();
builder.Services.AddSingleton<ClauseServices>();
builder.Services.AddSingleton<PointServices>();

builder.Services.AddSingleton<LicenseServices>();
builder.Services.AddSingleton<TitleServices>();
builder.Services.AddSingleton<LicenseTitleServices>();

builder.Services.AddSingleton<NewsServices>();

builder.Services.AddSingleton<TrafficFineServices>();
builder.Services.AddSingleton<TrafficFineTypeServices>();

builder.Services.AddSingleton<TrafficSignServices>();
builder.Services.AddSingleton<TrafficSignTypeServices>();

builder.Services.AddSingleton<ExaminationServices>();
builder.Services.AddSingleton<ExaminationResultServices>();
builder.Services.AddSingleton<ExaminationQuestionServices>();

builder.Services.AddSingleton<EmailServices>();
builder.Services.AddSingleton<FileServices>();

// var client = new SecretClient(new Uri(builder.Configuration.GetSection("KeyVaultSettings:VaultUri").Value!.ToString()), new DefaultAzureCredential());
// if (builder.Environment.IsProduction())
// {
    var keyVaultUri = builder.Configuration.GetSection("KeyVaultSettings:VaultUri");
    var keyVaultClientId = builder.Configuration.GetSection("KeyVaultSettings:ClientId");
    var keyVaultClientSecret = builder.Configuration.GetSection("KeyVaultSettings:ClientSecret");
    var keyVaultDirectoryId = builder.Configuration.GetSection("KeyVaultSettings:DirectoryId");

    var credentials = new ClientSecretCredential(keyVaultDirectoryId.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString());
    builder.Configuration.AddAzureKeyVault(keyVaultUri.Value!.ToString(), keyVaultClientId.Value!.ToString(), keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

    var client = new SecretClient(new Uri(keyVaultUri.Value!.ToString()), credentials);
    builder.Services.AddSingleton(client);
// }

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policy",
        builder =>
        {
            builder.WithOrigins("https://trafficlearn.azurewebsites.net", "https://trafficlearn-admin.vercel.app", "https://traffic-admin.vercel.app", "https://localhost:5173", "http://localhost:5173", "https://localhost:7220")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Traffic Learn API",
        Description = "ASP.NET Core Web API with MongoDB"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "TrafficLearn";
})
.AddCookie(options =>
{
    /*options.LoginPath = builder.Configuration["Authentication:Google:LoginPath"];*/
    options.LoginPath = client.GetSecret("Authentication-Google-LoginPath").Value.Value.ToString();
})
.AddGoogle("TrafficLearn", googleOptions =>
{
    /*googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];*/
    googleOptions.ClientId = client.GetSecret("Authentication-Google-ClientId").Value.Value.ToString();
    googleOptions.ClientSecret = client.GetSecret("Authentication-Google-ClientSecret").Value.Value.ToString();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = "TrafficLearnBackend",
        ValidIssuer = "TrafficLearnBackend",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(client.GetSecret("Jwt-SecretKey").Value.Value.ToString()))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseCors("policy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();