using System;
using System.Security.Claims;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace apisiase.Extensions;

public static class UserManagerExtensions
{
    public static async Task<Usuario> BuscarUsuarioAsync(this UserManager<Usuario> input, ClaimsPrincipal user){
        var email = user?.Claims?.FirstOrDefault(x=> x.Type == ClaimTypes.Email)?.Value;

        var usuario = await input.Users.SingleOrDefaultAsync(x=>x.Email == email);

        return usuario;
    }

}
