using System;
using Core.Entities;

namespace Core.Interfaces;

public interface ITokenService
{
    string CreateToken(Usuario usuario, IList<string>roles);
}
