using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.DataAccess
{
    public class EntityContext : DbContext
    {
        public EntityContext()
            : base(GetConnectionString())
        {
        }

        private static string GetConnectionString()
        {
            var envConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            return string.IsNullOrWhiteSpace(envConnectionString)
                ? "name=DbConnectionString"
                : envConnectionString;
        }

        
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EntityContext>());
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Producer> Producer { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Movie>()
        //        .HasMany(a => a.ActorMovieRelationships)
        //        .WithOptional()
        //        .WillCascadeOnDelete(true);

        //    modelBuilder.Entity<Actor>()
        //        .HasMany(a => a.ActorMovieRelationships)
        //        .WithOptional()
        //        .WillCascadeOnDelete(true);


        //}
    }
}
