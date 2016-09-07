namespace Unosquare.Tubular.Tests.Database
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using System.Data.Common;
    using Microsoft.Data.Sqlite;
    public class SampleEntities : DbContext
    {
        //public SampleEntities(DbConnection connection)
        //    : base(connection, true)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "MyDb.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }


        public void Fill()
        {
            var colors = new[] {"red", "yellow", "blue"};
            var category = new[] {"A", "B"};

            for (var i = 0; i < 100; i++)
            {
                var rand = new Random();
               
                Things.Add(new Thing()
                {
                    Date = DateTime.UtcNow.AddDays(-i),
                    Id = i+1,
                    Name = "Name - " + i,
                    Bool = (i % 2) == 0,
                    DecimalNumber = (i % 3 == 0) ? 10.100m : 20.2002m,
                    Number = rand.NextDouble() * 20,
                    Category = category[rand.Next(0, category.Length - 1)],
                    Color = colors[rand.Next(0, colors.Length - 1)],
                });
            }

            SaveChanges();
        }

        public DbSet<Thing> Things { get; set; }
    }
}