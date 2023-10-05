using AribTask.Comman;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AribTask.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,string username , string email , string password )
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName =username,
                Email = email, 
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, password);
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                }
            }
        }

        public static async Task SeedAdminAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
                //await roleManager.SeedClaimsForAdmin();
            }
        }

        //  private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
        //  {
        //      var adminRole = await roleManager.FindByNameAsync("Admin");
        //      var moduls = Enum.GetValues(typeof(Modules)).Cast<Modules>();
        //      foreach (var item in moduls)
        //      {
        //          await roleManager.AddPermissionClaim(adminRole, item.ToString());
        //      }
        //     
        //  }
        //
        //  public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        //  {
        //      var allClaims = await roleManager.GetClaimsAsync(role);
        //      var allPermissions = Permissions.GeneratePermissionsForModule(module);
        //      foreach (var permission in allPermissions)
        //      {
        //          if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
        //          {
        //              await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
        //          }
        //      }
        //  }
    }
}
