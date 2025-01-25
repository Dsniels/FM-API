using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Persistence
{
    public class SiaseDbContext : DbContext
    {

        public SiaseDbContext(DbContextOptions<SiaseDbContext> options) : base(options) { }

        public DbSet<Materia> Materia { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Carrera> Carrera { get; set; }

        public DbSet<ComentariosMaterias> ComentariosMaterias { get; set; }

        public DbSet<ComentariosProfesores> ComentariosProfesores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Materia>()
                        .HasIndex(m => new { m.ProfesorID, m.Nombre })
                        .IsUnique();
            modelBuilder.Entity<Profesor>()
                        .HasIndex(p=> new {p.PrimerApellido, p.Nombre, p.SegundoApellido})
                        .IsUnique();
            modelBuilder.Entity<Carrera>()
                        .HasIndex(c=> new {c.Nombre, c.Abreviatura})
                        .IsUnique();

        }
    }
}
