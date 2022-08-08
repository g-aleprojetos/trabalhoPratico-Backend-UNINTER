using Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrabalhoPratico_Backend.Context;

namespace TrabalhoPratico_Backend.Config
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AplicarSeed(this IApplicationBuilder app)
        {
            //Intancia dbContext
            using var migrationsScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var dbContext = migrationsScope.ServiceProvider.GetService<ApiContext>();
            //Verifica se o dbContext não esta nulo.
            if (dbContext == null) throw new ArgumentNullException(nameof(ApiContext), "Não foi possível instanciar o dbContext.");

            //Aplica SeedData para popular o banco de dados
            var seed = new SeedData(dbContext);
            seed.AplicarSeed();

            return app;
        }
    }
}


