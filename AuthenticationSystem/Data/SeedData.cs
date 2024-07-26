using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

public class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string adminRoleName = "Admin";
        string adminUserName = "admin@gmail.com";
        string adminPassword = "adminSystem_1";

        // Create the Admin role (if it doesn't exist)
        if (!await roleManager.RoleExistsAsync(adminRoleName))
        {
            await roleManager.CreateAsync(new IdentityRole(adminRoleName));
        }

        // Create the Admin user (if it doesn't exist)
        if (await userManager.FindByNameAsync(adminUserName) == null)
        {
            var adminUser = new IdentityUser { UserName = adminUserName, Email = adminUserName };
            await userManager.CreateAsync(adminUser, adminPassword);

            // Assign the Admin role to the Admin user
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}
