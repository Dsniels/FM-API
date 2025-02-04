using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Logic;

public class TokenService : ITokenService
{

    private readonly SymmetricSecurityKey _key;
    private readonly IConfiguration _config;
    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
    }

    public string CreateToken(Usuario usuario, IList<string> roles)
    {
        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
            new Claim(JwtRegisteredClaimNames.Name, usuario.Nombre),
            new Claim(JwtRegisteredClaimNames.FamilyName, usuario.PrimerApellido),
            new Claim("matricula", usuario.Matricula.ToString())
        };
        System.Console.WriteLine("Dn");
        if(roles != null && roles.Count > 0){
            foreach(var role in roles){
                claims.Add(new Claim(ClaimTypes.Role, role));
                System.Console.WriteLine(role);
            }
        }


        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);


        var TokenConfiguration = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(30),
            SigningCredentials = credentials,
            Issuer = _config["Token:Issuer"]
        };


        var tokenHandler = new JwtSecurityTokenHandler();
        var token  = tokenHandler.CreateToken(TokenConfiguration);
        return tokenHandler.WriteToken(token);


    }
}
