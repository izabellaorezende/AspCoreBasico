using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Treinamento.Models;

namespace Treinamento.Context
{
    public class Contexto : DbContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<Produto> Produto { get; set; }

        #region CONSTRUCTOR
        public Contexto(DbContextOptions<Contexto> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;

        }
        #endregion

        #region ON MODEL CREATING
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var implementedConfigTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => !t.IsAbstract
                            && !t.IsGenericTypeDefinition
                            && t.GetTypeInfo().ImplementedInterfaces.Any(i =>
                                i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configType in implementedConfigTypes)
            {
                dynamic config = Activator.CreateInstance(configType);
                modelBuilder.ApplyConfiguration(config);
            }

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region ON CONFIGURING
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Context"));
        }
        #endregion

        #region SaveChanges
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.GetType().GetProperty("DataInclusao") != null)
                    {
                        if (entry.Entity.GetType().GetProperty("DataInclusao").GetValue(entry.Entity, null) != null)
                        {
                            entry.Property("DataInclusao").CurrentValue = DateTime.Now;

                            if (entry.Entity.GetType().GetProperty("DataAlteracao") != null)
                                entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                        }
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.GetType().GetProperty("DataAlteracao") != null)
                    {
                        if (entry.Entity.GetType().GetProperty("DataAlteracao").GetValue(entry.Entity, null) != null)
                        {
                            entry.Property("DataInclusao").IsModified = false;
                            entry.Property("DataAlteracao").CurrentValue = DateTime.Now;
                        }
                    }
                }

                if (entry.Entity.GetType().GetProperty("Ativo") != null)
                {
                    if (entry.Entity.GetType().GetProperty("Ativo").GetValue(entry.Entity, null) == null)
                        entry.Property("Ativo").CurrentValue = true;
                }
            }

            return base.SaveChanges();
        }
        #endregion
    }
}
