using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using VehicleRental.Business;
using VehicleRental.Models;
using VehicleRental.Models.Identity;
using VehicleRental.Repository;

namespace VehicleRental
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };

                    options.SaveToken = true;

                    options.RequireHttpsMetadata = false;

                    options.ClaimsIssuer = "http://schemas.microsoft.com/ws/2008/06/identity/claims";

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            Debug.WriteLine("================= Jwt recieved");
                            Debug.WriteLine(context.Token);
                            Debug.WriteLine(context.HttpContext.User);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = (context) =>
                        {
                            Debug.WriteLine("======= JWt token validated");
                            Debug.WriteLine(context.SecurityToken);
                            Debug.WriteLine(context.HttpContext.User);
                            context.HttpContext.Items["JwtTokenIsValid"] = true;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = (context) =>
                        {
                            Debug.WriteLine("======= JWt token validation fail");
                            Debug.WriteLine(context.Exception);
                            Debug.WriteLine(context.HttpContext.User);
                            context.HttpContext.Items["JwtTokenIsValid"] = false;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
            }));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "VehicleRentalAPI",
                    Version = "v1"
                });
            });

            services.AddMemoryCache();

            RegisterDependecies(services);
        }

        private void RegisterDependecies(IServiceCollection services)
        {
            // Account
            services.AddTransient<IAccountManager, AccountManager>();

            // Token
            services.AddTransient<ITokenManager, TokenManager>();

            // Vehicle
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddTransient<IVehicleManager, VehicleManager>();

            // Vehicle Type
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddTransient<IVehicleTypeManager, VehicleTypeManager>();

            // Booking
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddTransient<IBookingManager, BookingManager>();

            // Identity Managers
            services.AddTransient<SignInManager<ApplicationUser>>();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<RoleManager<ApplicationRole>>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VehicleRentalAPI v1");
            });

            app.UseAuthentication();
        }
    }
}
