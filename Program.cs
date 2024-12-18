using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobilePractice.Data;
using MobilePractice.Models;
using MobilePractice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PracticeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<PractitionerService>();
builder.Services.AddScoped<TreatmentService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddSwaggerGen(options =>
{
    // ## USE THIS FOR JWT BEARER Setup with SWAGGER-UI 
    // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    // {
    //     Name = "Authorization",
    //     In = ParameterLocation.Header,
    //     Type = SecuritySchemeType.ApiKey,
    //     Scheme = "Bearer"
    // });

    // options.AddSecurityRequirement(new OpenApiSecurityRequirement
    // {
    //     {
    //         new OpenApiSecurityScheme
    //         {
    //             Reference = new OpenApiReference
    //             {
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             }
    //         },
    //         Array.Empty<string>()
    //     }
    // });
});

//  ** Authentication if using Bearer Tokens
// builder.Services.AddAuthentication().AddJwtBearer(options => {
//     options.TokenValidationParameters = new TokenValidationParameters {
//         ValidateIssuer = false,
//         ValidateAudience = false,
//         ValidateLifetime = true,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
//         ValidateIssuerSigningKey = true,
//         // ValidIssuer = builder.Configuration["Jwt:Issuer"],
//         // ValidAudience = builder.Configuration["Jwt:Audience"],
//     };
// });

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(options =>
            {
                options.Cookie.Name = "Api.Cookies";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                options.SlidingExpiration = true;
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            });
            
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();


app.MapControllers();

app.Run();
