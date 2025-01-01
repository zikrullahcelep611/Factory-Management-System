using FabrikaYonetimSistemi.Core.Repository;
using FabrikaYonetimSistemi.Data.Abstraction;
using FabrikaYonetimSistemi.Entity.Entities;
using FabrikaYonetimSistemi.Service.Services.Abstraction;
using FabrikaYonetimSistemi.Service.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FabrikaYonetimSistemi.Service.Extensions
{
    public static class ServiceLayerExtension 
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IBuildingService, BuildingService>();
            services.AddScoped<IRepository<Building>, Repository<Building>>();
            services.AddScoped<IFactoryService, FactoryService>();
            services.AddScoped<IRepository<Factory>, Repository<Factory>>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IRepository<Storage>, Repository<Storage>>();
            services.AddScoped<IRepository<MaterialTransaction>, Repository<MaterialTransaction>>();
            services.AddScoped<IRepository<MaterialRequest>, Repository<MaterialRequest>>();
            services.AddScoped<IMaterialRequestService, MaterialRequestService>();

            services.AddScoped<IRepository<StorageMaterial>, Repository<StorageMaterial>>();
            services.AddScoped<IStorageMaterialService, StorageMaterialService>();

            services.AddScoped<IMaterialTransactionService, MaterialTransactionService>();

            services.AddScoped<IRepository<Material>, Repository<Material>>();
            services.AddScoped<IMaterialService, MaterialService>();

            return services;
        } 
    }
}
