
# Events API Demo

A minified version of the Events module API based on wireframes. The API should provide endpoints for events CRUD

## Database

This demo API uses SQLite, the code generate the `EventsDemoAPI/events.db` file when running DB update command in `/EventsDemoAPI` folder.
```
cd EventsDemoAPI
```

You need to install the EntityFramework tools at first.
```
dotnet tool install --global dotnet-ef --version 7.*
```

Now you are ready for running DB update from migration files


Just running the following command for generating our DB file. It will create `/EventsDemoAPI/events.db` file.

```
dotnet ef database update
```

For now, we have 3 tables, `Events`, `Participants` and `MTimezones`.

`Events` table will have the events data, `Participants` table will have the information of participants of every events, and `MTimezones` table already have the timezones information over the world.

If you add/change the Model classes in `/Models` folder, then you should run the following commands for generating migration files and updating the DB.

```
dotnet ef migrations add events -o Data/Migrations
dotnet ef database update
```

And if you want to cancel the latest migration file, then run the following command.
```
dotnet ef migrations remove
```

## Build & Running
Build
```
cd EventsDemoAPI
dotnet build
```

Running for development
```
cd EventsDemoAPI
dotnet watch
```

Or you can build and run in Visual Studio.

## Test
The file `EventsControllerTests.cs` in EventsDemoAPI.tests project has a simple unit test.
You can run it in Visual Studio or via dotnet cli command.


## API
The demo API has 5 API endpoints for now.
The swagger has details about the API endpoints. 
[Swagger UI](http://localhost:5225/swagger/index.html)

- List API [GET] `/api/events`
This api will returns events list. This endpoint has 2 query parameters (start, rows).
Start parameter is offset of event list while the rows is count of events that will retrieve.

![API Screenshot](https://i.ibb.co/8dFZBGg/list.png)


