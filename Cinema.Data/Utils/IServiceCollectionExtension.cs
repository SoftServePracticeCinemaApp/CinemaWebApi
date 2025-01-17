using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Data.Utils
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
