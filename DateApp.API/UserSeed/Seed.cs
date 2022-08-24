using DateApp.Entity.DataContext;
using DateApp.Entity.Entities;
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
        public static async Task SeedUsers(DataContextModel context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("UserSeed/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
           foreach(var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

                 context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
