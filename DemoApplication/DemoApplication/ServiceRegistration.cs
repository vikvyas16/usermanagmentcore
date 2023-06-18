using DemoApplication.BusinessEntity;
using DemoApplication.Repository.Interface;
using DemoApplication.Repository;

namespace DemoApplication
{
    /// <summary>
    /// Startup container configuration
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Configure service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionSettings>(configuration.GetSection("ConnectionStrings"));

            services.AddScoped(typeof(IBaseRepository), typeof(BaseRepository));
            services.AddTransient<IBaseRepository, BaseRepository>();

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddScoped(typeof(IToDoRepository), typeof(ToDoRepository));
            services.AddTransient<IToDoRepository, ToDoRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
