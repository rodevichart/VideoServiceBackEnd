using VideoService.Configurations;
using VideoService.Configurations.ServiceConfigurations;
using VideoService.Services;
using VideoService.Services.Interfaces;

namespace VideoService
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
            // 1. Add Authentication Services
            AuthenticationConfigurations.Configure(services, Configuration);
            services.AddAutoMapper();

            services.AddDbContext<VideoServiceDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddOptions<AuthSettings>().Configure(o =>
            {
                o.LifeTime = Convert.ToDouble(Configuration["AuthSettings:LifeTime"]);
                o.Secret = Configuration["AuthSettings:Secret"];
            });

            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICryptService, CryptService>();
//            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<ICoverService, CoverService>();

            services.AddSingleton<IWriteToFileText, WriteToFileText>();

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IWriteToFileText writeToFileText)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                ExceptionHandlerConfigurations.Configure(app, loggerFactory, writeToFileText);
            }
            app.UseStaticFiles();
            if (string.IsNullOrWhiteSpace(env.WebRootPath))
                env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.WebRootPath, "uploads")),
                RequestPath = new PathString("/uploads")
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptionsht
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseHsts();
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // 2. Enable authentication middleware
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
            });

        }
    }
}
