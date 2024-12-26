using FabrikaYonetimSistemi.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FabrikaYonetimSistemi.Service.Seed
{
    public class DataInitializer
    {
        public static async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Personnel>>();

            string[] roles = { "Admin", "Personel" };

            // Rolleri oluştur
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int> { Name = role} );
                }
            }

            // Admin kullanıcıyı oluştur
            var adminUser = new Personnel
            {
                UserName = "admin@fabrika.com",
                Email = "admin@fabrika.com",
                Name = "Admin",
                Surname = "User",
                EmailConfirmed = true
            };

            var adminPassword = "Admin123!";

            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
