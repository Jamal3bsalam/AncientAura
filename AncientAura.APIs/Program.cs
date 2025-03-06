
using AncientAura.Core;
using AncientAura.Core.EmailConfig;
using AncientAura.Core.Entities.Identity;
using AncientAura.Core.Services.Contracts;
using AncientAura.Repository;
using AncientAura.Repository.Data;
using AncientAura.Repository.Data.Contexts;
using AncientAura.Service.Services.EmailService;
using AncientAura.Service.Services.OtpService;
using AncientAura.Service.Services.TokenService;
using AncientAura.Service.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AncientAura.APIs
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // Define the security scheme
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                // Apply the security requirement globally
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
                            new string[] { }
                        }
                    });
            });

            builder.Services.Configure<EmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration"));

            builder.Services.AddScoped<IEmailService, EmailService>();  
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            { 
            
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
                };
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.AllowedUserNameCharacters =
                     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._ ";
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
            })
           .AddEntityFrameworkStores<AncientAuraDbContext>()
           .AddDefaultTokenProviders();

            //builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            //{
            //    options.TokenLifespan = TimeSpan.FromDays(5); // مدة صلاحية التوكين
            //});

            builder.Services.AddDbContext<AncientAuraDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
             
          
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IOtpService,OtpService>();



            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNamingPolicy = null;
                        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    });



            var app = builder.Build();

            //return group of isercieseScope work with liftime scope 
            // وبما ان ال stroe dbcontext شغاله scope ف هترجعها  
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            // return object from storeDbContext .
            var context = services.GetRequiredService<AncientAuraDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();    
            try
            {
                // Update DataBase && Apply Migrations
                await context.Database.MigrateAsync();
                await AncientAuraDbContextSeed.SeedAppUser(userManager);
            }
            catch (Exception ex)
            {
                var loggerfactory = services.GetRequiredService<ILoggerFactory>();
                var logger = loggerfactory.CreateLogger<Program>();
                logger.LogError(ex, "there are problems during apply migrations");
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
