using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NYC_Subway_Stations_API.Interface;
using NYC_Subway_Stations_API.Models;
using NYC_Subway_Stations_API.Models.DAO;
using NYC_Subway_Stations_API.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYC_Subway_Stations_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _subwayClientName = Configuration["SubwayApiName"];
            if (string.IsNullOrEmpty(_subwayClientName))
                throw new Exception("Subway Client Name Not Found");
            _subwayClientUrl = Configuration["SubwayApiUrl"];
            if (string.IsNullOrEmpty(_subwayClientUrl))
                throw new Exception("Subway URL Not Found");
            _connectionString = Configuration.GetConnectionString("Dev");
            if (string.IsNullOrEmpty(_connectionString))
                throw new Exception("Connection String Not Found");
        }

        public IConfiguration Configuration { get; }

        private readonly string _subwayClientName;
        private readonly string _connectionString;
        private readonly string _subwayClientUrl;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "NYC Subway Stations API",
                    Version = "v1",
                    Description = "NET Core API which allows authenticated users to: Retrieve a list of NYC Subway stations, Save a user’s frequently used stations, Retrieve a user’s frequently used stations, Calculate the distance between two user provided stations"
                });
                c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    Description = "Authorization : Bearer \\<token\\> must appear in header",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "ApiAuthorizationScheme"
                });
                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
                c.AddSecurityRequirement(requirement);
            });
            services.AddTransient<IGateway, GatewayService>();
            services.AddTransient<IUser, UserDAO>();
            services.AddTransient<ISubwayStation, SubwayStationDAO>();
            services.AddTransient<IAuth, AuthService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHttpClient(_subwayClientName, client =>
            {
                client.BaseAddress = new Uri(_subwayClientUrl); 
            });
            // Add authentication method with JWT (Json Web Token)
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(Configuration.GetSection("JWT").GetValue<string>("SecretKey"))
                        ),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //Add database connection with Entity Framework
            services.AddDbContext<SubwayStationsContext>(
                options => options
                .UseSqlServer(_connectionString));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {                
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
