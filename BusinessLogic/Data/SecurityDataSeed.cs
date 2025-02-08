using System;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data;

public class SecurityDataSeed
{
    public static async Task SeedUserAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
    {
        System.Console.WriteLine(await userManager.Users.ToListAsync());
        System.Console.WriteLine(userManager.Users.Any());
        if (!userManager.Users.Any())
        {
            var usuario = new Usuario
            {
                Nombre = "Daniel",
                PrimerApellido = "Salazar",
                SegundoApellido = "Rodriguez",
                UserName = "Dasa",
                Email = "daniel@gmail.com",
                Matricula = 2090297,

            };

            await userManager.CreateAsync(usuario, "DanielSalazar11111$");
        }


        if (!roleManager.Roles.Any())
        {
            var role = new IdentityRole
            {
                Name = "ADMIN"
            };

            var roleAlumno = new IdentityRole{
                Name = "ALUMNO"
            };
            await roleManager.CreateAsync(role);
            await roleManager.CreateAsync(roleAlumno);
        }
    }


}
