using DAL.Entity;
using DAL.Interfaces;
using DAL.SQL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class ConfigurationDAL
    {
        public static void ConfigurateDALService(this IServiceCollection services, string connString)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(connString);
                
            });

            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
                        
            

        }
    }
}
