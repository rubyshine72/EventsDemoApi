using EventsDemoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EventsDemoAPI.Data
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Event> Events => Set<Event>();
        public DbSet<Participant> Participants => Set<Participant>();
        public DbSet<MTimezone> MTimezones => Set<MTimezone>();

        public EventsDbContext(DbContextOptions<EventsDbContext> options)
            : base(options) { }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MTimezone>().HasData(GetTimezones());
        }


        private static MTimezone[] GetTimezones()
        {
            MTimezone[] empty = new MTimezone[0];

            try
            {

                string[] p = { Directory.GetCurrentDirectory(), "JsonFiles", "timezones.json" };
                var filePath = Path.Combine(p);
                string strJson = File.ReadAllText(filePath);

                var timezones = JsonConvert.DeserializeObject<MTimezone[]>(strJson);
                if (timezones == null)
                {
                    return empty;
                }

                int id = 1;
                foreach (var timezone in timezones)
                {
                    timezone.Id = id;
                    id++;
                }

                return timezones;
            }
            catch (Exception ex)
            {
                return empty;
            }
        }
    }
}
