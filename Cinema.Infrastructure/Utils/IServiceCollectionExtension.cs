using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure.Utils
{
    public static class IServiceCollectionExtension
    {
        public static void AddInMemoryDataBase(this IServiceCollection services)
        {
            services.AddDbContext<CinemaDbContext>(options =>
                options.UseInMemoryDatabase("CinemaDb")
            );
        }
    }
}
