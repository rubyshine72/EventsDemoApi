using EntityFrameworkMock;
using EventsDemoAPI.Controllers;
using EventsDemoAPI.Data;
using EventsDemoAPI.Models;
using EventsDemoAPI.tests.DummyData;
using EventsDemoAPI.Types;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EventsDemoAPI.tests
{
    public class EventsControllerTests
    {

        [Fact]
        public void Test_Get_Event_Item_API()
        {
            // Arrange
            int id = 1;

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<EventsDbContext>()
                .UseSqlite(connection)
                .Options;

            var dbContext = new EventsDbContext(options);
            dbContext.Database.EnsureCreated();

            AddDummy.addEvents(dbContext);

            var controller = new EventsController(dbContext);

            // Act
            var actionResult = controller.Get(id);

            // Assert
            var result = actionResult as OkObjectResult;
            var rItem = result.Value as EventWithUserInfo;

            Assert.NotNull(rItem);
            Assert.Equal(rItem.Id, 1);
            Assert.Equal(rItem.Title, "Test1");
        }
    }
}