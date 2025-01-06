namespace TinyUrler_DotNetVersion.Data;

using Microsoft.AspNetCore.Identity;

public class DbSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public DbSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task SeedRolesAsync()
    {
        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        }

        if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
        }
    }
}