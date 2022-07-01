using AutoMapper;
using AutoMapperBuilder.Extensions.DependencyInjection;
using FileStorage.BLL;
using FileStorage.BLL.Common;
using FileStorage.BLL.Interfaces;
using FileStorage.BLL.Models;
using FileStorage.DAL;
using FileStorage.DAL.EF;
using FileStorage.DAL.Interfaces;
using FileStorage.DAL.Models;
using FileStorage.PL.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace FileStorage.PL
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
            var authOptioinsCofiguration = Configuration.GetSection("Auth");
            var smtpOptioinsCofiguration = Configuration.GetSection("Smtp");
            var filesOptionsConfiguration = Configuration.GetSection("Files");

            services.Configure<AuthOptions>(authOptioinsCofiguration);
            services.Configure<SmtpOptions>(smtpOptioinsCofiguration);
            services.Configure<FilesOptions>(filesOptionsConfiguration);
            
           


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







            
            services.AddAutoMapperBuilder(builder =>
            {
                builder.Profiles.AddRange(new Profile[] { new MapperConfigViewModel(), new MapperConfig() });
            });




            services.AddTransient<IStorageUW, StorageUW>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IStatisticService, StatisticService>();
            services.AddScoped<IEmailService, EmailService>();


            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,

                        ValidateLifetime = true,

                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
