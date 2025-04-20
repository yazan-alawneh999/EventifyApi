using System.Security.Claims;
using System.Text;
using LearningHub.Core.Common;

using LearningHub.Core.Repository;
using LearningHub.Core.Services;
using LearningHub.Infra.Repository;
using LearningHub.Infra.Services;
using LearningHub.Infra.Util;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using DbContext = LearningHub.Infra.Common.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IAuthRepo, AuthRepo>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UtilService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationResponsecs>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepositorycs>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IBuyTicketRepository, BuyTicketRepository>();
builder.Services.AddScoped<IBuyTicketService, BuyTicketService>();
builder.Services.AddScoped<QRCodeService>();
builder.Services.AddScoped<QRCode>();

builder.Services.AddScoped<IReportsRepository, ReportsRepository>();
builder.Services.AddScoped<IReportsService, ReportsService>();
builder.Services.AddScoped<IBuyTicketRepository, BuyTicketRepository>();
builder.Services.AddScoped<IBuyTicketService, BuyTicketService>();
builder.Services.AddScoped<QRCodeService>();
builder.Services.AddScoped<QRCode>();


builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Configure Swagger to include JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token in the following format: Bearer {your token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Configure JWT authentication
var jwtKey = builder.Configuration["Jwt:Key"] ?? "Yazan_123"; // Fallback to default key for development
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "https://secureapi.com";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "https://secureapi.com";

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        RoleClaimType = ClaimTypes.Role
    };
});



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()  // Allows any frontend
            .AllowAnyMethod()   // Allows any HTTP method (GET, POST, etc.)
            .AllowAnyHeader()); // Allows any headers

});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication(); // **IMPORTANT**: Add this to enable authentication
app.UseAuthorization();

 /*app.UseCors("AllowAngular");*/
app.UseStaticFiles();

var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "images");

app.UseStaticFiles(new StaticFileOptions
    
{
    FileProvider = new PhysicalFileProvider(imagesPath),
    RequestPath = "/images"
});
app.MapControllers();


app.Run();