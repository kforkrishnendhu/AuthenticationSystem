using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthenticationSystem.Data;
using System;
using System.Threading.Tasks;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configure your database context
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddRoles<IdentityRole>() // Configure RoleManager
            .AddDefaultTokenProviders();

        services.AddControllersWithViews();

        // Other services and configuration...
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //if (env.IsDevelopment())
        //{
        //    app.UseDeveloperExceptionPage();
        //    app.UseMigrationsEndPoint();
        //}
        //else
        //{
        //    app.UseExceptionHandler("/Home/Error");
        //    app.UseHsts();
        //}

        //app.UseHttpsRedirection();
        //app.UseStaticFiles();
        //app.UseRouting();

        //// Configure authentication and authorization middleware as needed

        //app.UseEndpoints(endpoints =>
        //{
        //    endpoints.MapControllerRoute(
        //        name: "default",
        //        pattern: "{controller=Home}/{action=Index}/{id?}");
        //    endpoints.MapRazorPages();
        //});

        // Initialize roles and perform any database seeding
        InitializeRolesAndSeedData(app.ApplicationServices).Wait();
    }

    private async Task InitializeRolesAndSeedData(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //var adminRoleName = "Admin";

            //// Check if the "Admin" role exists; if not, create it
            //if (!await roleManager.RoleExistsAsync(adminRoleName))
            //{
            //    var adminRole = new IdentityRole { Name = adminRoleName };
            //    await roleManager.CreateAsync(adminRole);
            //}

            //// Other seeding or initialization logic can go here



            // Find the user with the "admin@example.com" email
            var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = await _userManager.FindByEmailAsync("admin@gmail.com");

            if (user != null)
            {
                // Check if the user is not already in the "Admin" role (optional)
                //var isInRole = await _userManager.IsInRoleAsync(user, "Admin");
                //if (!isInRole)
                //{
                    // Assign the "Admin" role to the user
                    await _userManager.AddToRoleAsync(user, "Admin");
                //}
            }

        }
    }
}
