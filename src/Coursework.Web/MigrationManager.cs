using Coursework.Data;
using Coursework.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Type = Coursework.Data.Entities.Type;

namespace Coursework.Web;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        using var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        
        applicationContext.Database.Migrate();

        return webApp;
    }
    
    public static async Task<WebApplication> AddSeedData(this WebApplication webApp)
    {
        using var scope = webApp.Services.CreateScope();
        await using var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        
        var typeLogos = new List<TypeLogo>
        {
            new TypeLogo { Id = Guid.Parse("16e4701f-a978-4a9c-be05-cb60293c1ead"), Path = "./../img/pic6.png" },
            new TypeLogo { Id = Guid.Parse("2934bfb7-9f8c-46d8-93c3-315cd0e7b873"), Path = "./../img/pic5.png" },
            new TypeLogo { Id = Guid.Parse("4043ddea-38a5-49ec-b125-f2171cdeeeec"), Path = "./../img/pic7.png" },
            new TypeLogo { Id = Guid.Parse("425fda4c-b207-400a-a9f9-8e3091989584"), Path = "./../img/pic8.png" }
        };
        var existedTypeLogosIds = await applicationContext.TypeLogos
            .Select(logo => logo.Id)
            .ToListAsync();
        foreach (var typeLogo in typeLogos)
        {
            if (!existedTypeLogosIds.Contains(typeLogo.Id))
            {
                applicationContext.TypeLogos.Add(typeLogo);
            }
        }
        
        var types = new List<Type>
        {
            new Type { Id = Guid.Parse("961ff754-9be6-43d0-88a4-bd97b9d0c8ad"), Name = "Фитнес и спа", LogoId = typeLogos[0].Id},
            new Type { Id = Guid.Parse("746861d0-41b2-4505-945f-3820c30c7ee6"), Name = "Достопримечательности", LogoId = typeLogos[1].Id},
            new Type { Id = Guid.Parse("c45a45f7-30a3-4fac-b37a-6a55525a514d"), Name = "Парки", LogoId = typeLogos[2].Id},
            new Type { Id = Guid.Parse("c2e9c0fd-630d-4a8c-b64a-2e4e75829d7b"), Name = "Рестораны", LogoId = typeLogos[3].Id}
        };
        var existedTypesIds = await applicationContext.Types
            .Select(type => type.Id)
            .ToListAsync();
        foreach (var type in types)
        {
            if (!existedTypesIds.Contains(type.Id))
            {
                applicationContext.Types.Add(type);
            }
        }
        
        var districts = new List<District>
        {
            new District { Id = Guid.Parse("08b2140c-df7d-4ec0-b1f1-aa6cd42d54f3"), Name = "Московский"},
            new District { Id = Guid.Parse("25ad614a-efd6-408d-afcb-f552f68ad133"), Name = "Ново-савиновский"},
            new District { Id = Guid.Parse("694726e7-8e84-4d06-a5cb-7721de763787"), Name = "Приволжский"},
            new District { Id = Guid.Parse("6a13cb8b-ce99-4711-8afb-934ad25f9b29"), Name = "Авиастроительный"},
            new District { Id = Guid.Parse("71f5d7ce-e29e-430a-ac62-d282dac76c9c"), Name = "Вахитовский"},
            new District { Id = Guid.Parse("9d4a8f1b-2a7b-41ce-bf11-247bb9d913bb"), Name = "Кировский"},
            new District { Id = Guid.Parse("d3a2ace4-232e-4e42-9522-fe7878ecf995"), Name = "Советский"}
        };
        var existedDistrictsIds = await applicationContext.Districts
            .Select(district => district.Id)
            .ToListAsync();
        foreach (var district in districts)
        {
            if (!existedDistrictsIds.Contains(district.Id))
            {
                applicationContext.Districts.Add(district);
            }
        }

        await applicationContext.SaveChangesAsync();

        return webApp;
    }
}