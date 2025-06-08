using System.Text;
using API.Authorization.Handler;
using API.Data;
using API.Exceptions.Handlers;
using API.Interfaces;
using API.Models;
using API.Repositories;
using API.Services;
using API.SheduleServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add services to the container.
builder.Services.AddScoped<FileServices>();
builder.Services.AddScoped<IUserRepository ,UserRepository>();
builder.Services.AddScoped<IUserService ,UserServices>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieServices, MovieServices>();
builder.Services.AddScoped<ISeasonRepository, SeasonRepository>();
builder.Services.AddScoped<ISeasonServices, SeasonServices>();
builder.Services.AddScoped<IEpisodeRepository, EpisodeRepository>();
builder.Services.AddScoped<IEpisodeServices, EpisodeServices>();
builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IVideoServices, VideoServices>();
builder.Services.AddScoped<GenereRepository>();
builder.Services.AddScoped<GenereServices>();
builder.Services.AddScoped<AuthenticationServices>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<ExpiredJWTRepository>();
builder.Services.AddHostedService<CleanRefreshTokenService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<PasswordResetCodeRepository>();

builder.Services.AddExceptionHandler<AppExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionHandler>();


builder.Services.AddTransient<EmailSender>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOi...\"",
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer",
                }
            },
            new string[] {}
        }
    });
});


builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme =
        options.DefaultChallengeScheme =
        options.DefaultForbidScheme =
        options.DefaultScheme =
        options.DefaultSignInScheme =
        options.DefaultSignOutScheme =
        JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddAuthorization(
    options =>
{
    options.AddPolicy("Create", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new API.Authorization.Requirements.PermissionRequirements("CAN_CREATE"));
    });

    options.AddPolicy("Update", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new API.Authorization.Requirements.PermissionRequirements("CAN_UPDATE"));
    });

    options.AddPolicy("Delete", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new API.Authorization.Requirements.PermissionRequirements("CAN_DELETE"));
    });

    options.AddPolicy("View", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new API.Authorization.Requirements.PermissionRequirements("CAN_VIEW_VIDEO"));
    });

    options.AddPolicy("CAN_GET_INFO", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new API.Authorization.Requirements.PermissionRequirements("CAN_GET_INFO"));
    });

    options.AddPolicy("Get_Admin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.Requirements.Add(new API.Authorization.Requirements.PermissionRequirements("CAN_GET_ADMIN"));
    });
}
);




var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler("/Error");

app.MapControllers();

app.Run();
