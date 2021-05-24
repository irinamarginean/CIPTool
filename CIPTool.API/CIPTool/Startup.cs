using AspNetCore.Email;
using AutoMapper;
using BusinessLogicLayer.FinancialReports;
using BusinessLogicLayer.Ideas;
using BusinessLogicLayer.Statistics;
using BusinessObjectLayer;
using BusinessObjectLayer.Entities;
using CIPTool.Helpers;
using DataAcessLayer;
using DataAcessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;

namespace CIPTool
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
            services.AddControllers();
            services.AddDbContext<CIPToolContext>();

            services.AddCors();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CIPToolContext>()
                .AddDefaultTokenProviders();

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddHttpContextAccessor();
            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue;
            });

            //services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            //    .AddNegotiate();
            //services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddAuthentication();
            services.AddAuthorization();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<IdeaRepository, IdeaRepository>();
            services.AddScoped<BaseRepository<LeaderResponse>, LeaderResponseRepository>();
            services.AddScoped<BaseRepository<Category>, CategoryRepository>();
            services.AddScoped<BaseRepository<FinancialReportEntity>, FinancialReportRepository>();
            services.AddScoped<BaseRepository<Attachment>, AttachmentRepository>();
            services.AddScoped<BaseRepository<BonusEntity>, BonusRepository>();
            services.AddScoped<BaseRepository<BonusRangeEntity>, BonusRangeRepository>();
            services.AddScoped<BaseRepository<BonusCorrectionFactorEntity>, BonusCorrectionFactorRepository>();

            services.AddScoped<IIdeaService, IdeaService>();
            services.AddScoped<IFinancialReportService, FinancialReportService>();
            services.AddScoped<IStatisticsService, StatisticsService>();

            services.AddTransient<IEmailSender, Helpers.EmailSender>();
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices.CreateScope();
            InitializeDatabase(scope);
        }

        private static void InitializeDatabase(IServiceScope serviceScope)
        {
            var services = serviceScope.ServiceProvider;

            try
            {
                DataSeeder.InitializeAsync(services).Wait();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Error occurred seeding the DB.");
            }
        }
    }
}
