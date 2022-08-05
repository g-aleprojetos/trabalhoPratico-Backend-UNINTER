using Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrabalhoPratico_Backend.Context;

namespace TrabalhoPratico_Backend.Config
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AplicarSeed(this IApplicationBuilder app)
        {
            using var migrationsScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = migrationsScope.ServiceProvider.GetService<ApiContext>();

            if (dbContext == null) throw new ArgumentNullException(nameof(ApiContext), "Não foi possível instanciar o dbContext.");


            var seed =  new SeedData(dbContext) ;
            seed.AplicarSeed();

            return app;
        }
           
    }
}


