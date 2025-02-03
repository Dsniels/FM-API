using System;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Persistence;

public class SeguridadDbContext : IdentityDbContext<Usuario>
{
    public SeguridadDbContext(DbContextOptions<SeguridadDbContext> opts) : base(opts)
    { }


    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);

    }
}
