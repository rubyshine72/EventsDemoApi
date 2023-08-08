using EventsDemoAPI.Data;
using EventsDemoAPI.Types;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace EventsDemoAPI.Libraries
{
    public static class Validation
    {
        public static string? getEventBodyError(EventCreateReq req, EventsDbContext db)
        {
            var tz = db.MTimezones.Find(req.timezoneId);
            if (tz == null)
            {
                return "Not valid timezone";
            }

            if (req.participants != null)
            {
                foreach (var participant in req.participants)
                {
                    var data = ExternalApi.getUserInfo(participant);
                    if (data == null) {
                        return "Participant id: " + participant.ToString() + " is not existing!";
                    }
                }
            }

            return null;
        }
    }
}
