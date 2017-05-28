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
            : base("name=DbConnectionString")
        {
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
