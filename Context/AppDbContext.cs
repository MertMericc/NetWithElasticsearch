﻿using Microsoft.EntityFrameworkCore;

namespace NetWithElasticsearch.Context;

public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(local);Database=TestDb;Integrated Security=True;TrustServerCertificate=True");
    }

    public DbSet<Travel> Travels { get; set; }
}

public sealed class Travel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}
