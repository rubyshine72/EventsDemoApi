using EventsDemoAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDemoAPI.tests.DummyData
{
    internal static class AddDummy
    {
        public static void addEvents(EventsDbContext db)
        {
            db.Events.Add(new Models.Event()
            {
                Id = 1,
                Title = "Test1",
                Description = "Description",
                StartAt = DateTime.Now,
                EndAt = DateTime.Now.AddHours(5),
                TimezoneId = 1,
            });
            db.SaveChanges();
        }
    }
}
