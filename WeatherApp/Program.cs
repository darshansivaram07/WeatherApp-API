using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using WeatherApp.Interface;
using WeatherApp.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddHttpClient("WeatherApi", client =>
{
    client.BaseAddress = new Uri("https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"])),
            RoleClaimType = ClaimTypes.Role 
        };
    });


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" });


    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Enter your JWT token in the format **Bearer &lt;token&gt;**"
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("WeatherAppCorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")  
              .WithMethods("GET", "OPTIONS")         
              .AllowAnyHeader();                    
    });
});


builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddSingleton<TokenGenerationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseCors("WeatherAppCorsPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
