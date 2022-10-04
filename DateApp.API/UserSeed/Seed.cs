using DateApp.Entity.DataContext;
using DateApp.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DateApp.API.UserSeed
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("UserSeed/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
           foreach(var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Dursukok34.");
            }

            
        }
    }
}
