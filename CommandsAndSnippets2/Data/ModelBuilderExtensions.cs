using CommandsAndSnippets2.Application.Users;
using Microsoft.EntityFrameworkCore;
using CommandsAndSnippets2.Models;

namespace CommandsAndSnippets2;

public static class ModelBuilderExtensions
{
    #region Initialization

    /// <summary>
    ///  Setup the database model
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void ConfigureCurrentDbModel(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureDatabaseKeys()
            .SnippetModel()
            .ConfigureUserModel()
            .ConfigureAppContextModel();

        //.ConfigureIdentityModel();
    }

    #endregion

    #region Application Users

    /// <summary>
    ///  Configure App context model
    /// </summary>
    /// <param name="modelBuilder"></param>
    private static void ConfigureAppContextModel(this ModelBuilder builder)
    {
        builder.Entity<ApplicationUserRole>().ToTable("UserRoles");
        builder.Entity<ApplicationUser>(b =>
        {
            b.HasMany<ApplicationUserRole>()
                .WithOne(b => b.User)
                .HasForeignKey(ur => ur.UserId).IsRequired();
        });


        builder.Entity<ApplicationUser>().ToTable("ApplicationUser")
            .HasKey(x => x.Id);


        builder.Entity<ApplicationRole>(b =>
        {
            b.HasKey(r => r.Id);
            b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
            b.ToTable("Roles");
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            b.Property(u => u.Name).HasMaxLength(256);
            b.Property(u => u.NormalizedName).HasMaxLength(256);

            b.HasMany<ApplicationUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            b.HasMany<ApplicationRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        });

        builder.Entity<ApplicationUserRole>().HasOne(x => x.ApplicationRole);
        builder.Entity<ApplicationRoleClaim>().HasOne<ApplicationRole>(x => x.ApplicationRole);
        builder.Entity<ApplicationUserClaim>().HasOne<ApplicationUser>(x => x.ApplicationUser);


        builder.Entity<ApplicationRoleClaim>(b =>
        {
            b.HasKey(rc => rc.Id);
            b.ToTable("RoleClaims");
        });

        builder.Entity<ApplicationUserRole>(b =>
        {
            b.HasKey(r => new {r.UserId, r.RoleId});
            b.ToTable("UserRoles");
        });

        builder.Entity<ApplicationRoleClaim>().ToTable("RoleClaims");
        builder.Entity<ApplicationUserRole>().ToTable("UserRole");


        //.ConfigureIdentityModel();
    }

    #endregion

    #region DatabasePK

    private static ModelBuilder ConfigureDatabaseKeys(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>(x => x.HasKey(x => x.Id));
        modelBuilder.Entity<ApplicationUser>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ApplicationUser>().HasMany(x => x.Favorites);

        modelBuilder.Entity<Snippet>(x => x.HasKey(x => x.Id));
        modelBuilder.Entity<Snippet>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Category>(x => x.HasKey(x => x.Id));
        modelBuilder.Entity<Category>().Property(x => x.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Content>(x => x.HasKey(x => x.Id));
        modelBuilder.Entity<Content>().Property(x => x.Id).ValueGeneratedOnAdd();


        return
            modelBuilder;
    }

    #endregion

    #region Database Models

    private static ModelBuilder SnippetModel(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Snippet>()
            .HasOne(x => x.User)
            .WithMany(x => x.Snippets);

        modelBuilder.Entity<Snippet>()
            .HasMany(x => x.Categories)
            .WithMany(x => x.Snippets);

        modelBuilder.Entity<Snippet>()
            .HasMany(x => x.Contents)
            .WithOne(x => x.Snippet)
            .HasForeignKey(x => x.SnippetId);

        return modelBuilder;
    }

    private static ModelBuilder ConfigureUserModel(this ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<ApplicationUser>()
            .HasMany(x => x.Snippets)
            .WithOne(x => x.User).HasForeignKey(x => x.UserId);

        return modelBuilder;
    }

    #endregion
}