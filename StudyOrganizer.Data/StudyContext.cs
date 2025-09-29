using Microsoft.EntityFrameworkCore;
using StudyOrganizer.Core.Entities;


namespace StudyOrganizer.Data;


public class StudyContext : DbContext
{
    public StudyContext(DbContextOptions<StudyContext> options) : base(options) { }


    public DbSet<User> Users => Set<User>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<Subtopic> Subtopics => Set<Subtopic>();
    public DbSet<StudySession> StudySessions => Set<StudySession>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>()
        .HasMany(m => m.Disciplines)
        .WithOne(d => d.Module)
        .HasForeignKey(d => d.ModuleId)
        .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Discipline>()
        .HasMany(d => d.Subtopics)
        .WithOne(s => s.Discipline)
        .HasForeignKey(s => s.DisciplineId)
        .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Subtopic>()
        .Property(s => s.Description)
        .HasMaxLength(1000);


        modelBuilder.Entity<Subtopic>()
        .Property(s => s.MasteryLevel);


        base.OnModelCreating(modelBuilder);

        //SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>().HasData(
            new Module { Id = 1, Name = "Conhecimentos Gerais" },
            new Module { Id = 2, Name = "Conhecimentos Específicos" }
        );

        modelBuilder.Entity<Discipline>().HasData(
            new Discipline { Id = 1, Name = "Língua Portuguesa", ModuleId = 1 },
            new Discipline { Id = 2, Name = "Raciocínio Lógico", ModuleId = 1 },
            new Discipline { Id = 3, Name = "Informática", ModuleId = 1 },
            new Discipline { Id = 4, Name = "Legislação", ModuleId = 1 },
            new Discipline { Id = 5, Name = "Engenharia de Software", ModuleId = 2 },
            new Discipline { Id = 6, Name = "Banco de Dados", ModuleId = 2 },
            new Discipline { Id = 7, Name = "Redes de Computadores", ModuleId = 2 }
        );

        modelBuilder.Entity<Subtopic>().HasData(
            // Português
            new Subtopic { Id = 1, DisciplineId = 1, Description = "Interpretação de textos", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 2, DisciplineId = 1, Description = "Ortografia oficial", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 3, DisciplineId = 1, Description = "Acentuação gráfica", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },

            // Raciocínio Lógico
            new Subtopic { Id = 4, DisciplineId = 2, Description = "Proposições, conectivos e tabelas-verdade", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 5, DisciplineId = 2, Description = "Problemas de lógica e contagem", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },

            // Informática
            new Subtopic { Id = 6, DisciplineId = 3, Description = "Conceitos de sistemas operacionais", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 7, DisciplineId = 3, Description = "Segurança da informação", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },

            // Legislação
            new Subtopic { Id = 8, DisciplineId = 4, Description = "Constituição Federal - Artigos 37 a 41", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 9, DisciplineId = 4, Description = "Lei de Acesso à Informação (Lei 12.527/2011)", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },

            // Engenharia de Software
            new Subtopic { Id = 10, DisciplineId = 5, Description = "Ciclo de vida de software", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 11, DisciplineId = 5, Description = "UML - Casos de uso, diagramas de classes e sequência", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },

            // Banco de Dados
            new Subtopic { Id = 12, DisciplineId = 6, Description = "Modelo relacional e normalização", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 13, DisciplineId = 6, Description = "SQL - comandos DDL, DML, DCL", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },

            // Redes
            new Subtopic { Id = 14, DisciplineId = 7, Description = "Modelo OSI e TCP/IP", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null },
            new Subtopic { Id = 15, DisciplineId = 7, Description = "Endereçamento IPv4 e IPv6", Status = 0, Notes = null, StartDate = null, EndDate = null, MasteryLevel = null, MaterialUrl = null, Content = null }
        );
    }
}