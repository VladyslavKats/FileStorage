using AutoMapper;
using AutoMapperBuilder.Extensions.DependencyInjection;
using Azure.Storage.Blobs;
using FileStorage.BLL;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Mapper;
using FileStorage.BLL.Models;
using FileStorage.BLL.Options;
using FileStorage.BLL.Services;
using FileStorage.DAL;
using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.PL.Common;
using FileStorage.PL.Middlewares;
using FileStorage.PL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FileStorage.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AuthOptions>(Configuration.GetSection("Auth"));
            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
            services.Configure<FilesOptions>(Configuration.GetSection("Files"));
            services.Configure<AzureStorageOptions>(Configuration.GetSection("AzureStorage"));
            services.AddCors(opt => opt.AddPolicy("AllowAll", builder => {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddControllers();
            services.AddDbContext<FileStorageContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>(opt => {
                opt.Password.RequiredLength = 4;
            })
                .AddEntityFrameworkStores<FileStorageContext>()
                .AddDefaultTokenProviders();
            services.AddAutoMapper(typeof(Startup).Assembly, typeof(MapperConfig).Assembly);
            services.AddTransient(typeof(CurrentUser));
            services.AddTransient<IDatabaseInitializer,DatabaseInitializer>();
            services.AddScoped<IStorageUW, StorageUW>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStatisticService, StatisticService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped( _ =>
            {
                return new BlobServiceClient(Configuration["AzureStorage:ConnectionString"]);
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Auth:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Auth:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Auth:Secret"])
                        ),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddSwaggerGen(
                opt => opt.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "File Storage API",
                            Description = "An ASP.NET Core Web API for managing files storage"
                        }
                        )
             );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionHandling();
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
