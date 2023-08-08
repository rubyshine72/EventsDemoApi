using EventsDemoAPI.Data;
using EventsDemoAPI.Libraries;
using EventsDemoAPI.Models;
using EventsDemoAPI.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventsDemoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EventsDbContext _dbContext;

        public EventsController(EventsDbContext db)
        {
            this._dbContext = db;
        }

        // GET: api/<EventsController>
        [HttpGet]
        [ProducesResponseType(typeof(EventListRes<Event>), 200)]
        public IActionResult Get(int start = 0, int rows = 10)
        {
            try
            {
                int total = _dbContext.Events.Count();
                var events = _dbContext.Events.Skip(start).Take(rows).Include(e => e.Participants).ToList();
                return Ok(new EventListRes<Event>
                {
                    data = events,
                    start = start,
                    rows = rows,
                    total = total
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventWithUserInfo), 200)]
        public IActionResult Get(int id)
        {
            try
            {
                var eventItem = _dbContext.Events.Include(e => e.Participants).Where(e => e.Id == id).FirstOrDefault();
                if (eventItem == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "There is not such event item");
                }
                return Ok(new EventWithUserInfo(eventItem));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/<EventsController>
        [HttpPost]
        [ProducesResponseType(typeof(EventCreateRes<Event>), 200)]
        public IActionResult Post([FromBody] EventCreateReq req)
        {
            try
            {
                var errorMsg = Validation.getEventBodyError(req, _dbContext);
                if (errorMsg != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, errorMsg);
                }

                var tz = _dbContext.MTimezones.Find(req.timezoneId);
                Event newEvent = new Event()
                {
                    Title = req.title,
                    Description = req.description,
                    StartAt = req.startAt,
                    EndAt = req.endAt,
                    Timezone = tz,
                };
                _dbContext.Events.Add(newEvent);
                _dbContext.SaveChanges();

                foreach (var participantId in req.participants)
                {
                    _dbContext.Participants.Add(new Participant
                    {
                        UserId = participantId,
                        EventId = newEvent.Id,
                        IsConfirmed = false,
                    });
                }
                _dbContext.SaveChanges();

                return Ok(new EventCreateRes<Event>()
                {
                    data = newEvent
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EventCreateRes<Event>), 200)]
        public IActionResult Put(int id, [FromBody] EventCreateReq req)
        {
            try
            {
                var errorMsg = Validation.getEventBodyError(req, _dbContext);
                if (errorMsg != null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, errorMsg);
                }

                var tz = _dbContext.MTimezones.Find(req.timezoneId);
                Event evt = _dbContext.Events.Find(id);
                if (evt == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Not valid id");
                }

                evt.Title = req.title;
                evt.Description = req.description;
                evt.StartAt = req.startAt;
                evt.EndAt = req.endAt;
                evt.Timezone = tz;

                _dbContext.SaveChanges();

                // Delete participants not list in participants list
                if (req.participants != null)
                {
                    List<int> userIds = new List<int>(req.participants);
                    var toRemoveItems = _dbContext.Participants.Where(p => p.EventId == id && !userIds.Contains(p.UserId));
                    _dbContext.RemoveRange(toRemoveItems);
                    _dbContext.SaveChanges();
                }

                // Add participants if not exist
                foreach (var participantId in req.participants)
                {
                    var existUser = _dbContext.Participants.Where(p => p.EventId == id && p.UserId == participantId).FirstOrDefault();
                    if (existUser != null)
                    {
                        continue;
                    }

                    _dbContext.Participants.Add(new Participant
                    {
                        UserId = participantId,
                        EventId = evt.Id,
                        IsConfirmed = false,
                    });
                }
                _dbContext.SaveChanges();

                return Ok(new EventCreateRes<Event>()
                {
                    data = evt
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult Delete(int id)
        {
            try
            {
                Event evt = _dbContext.Events.Find(id);
                if (evt == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Not valid id");
                }

                var toRemoveParticipants = _dbContext.Participants.Where(p => p.EventId == id);
                _dbContext.RemoveRange(toRemoveParticipants);
                _dbContext.Remove(evt);
                _dbContext.SaveChanges();

                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Confirm invited event participant
        [Route("invite-confirm/{id}/{userId}")]
        [HttpGet]
        [ProducesResponseType(typeof(bool), 200)]
        public IActionResult InviteConfirm(int id, int userId)
        {
            try
            {
                Event evt = _dbContext.Events.Find(id);
                if (evt == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Not valid id");
                }

                var item = _dbContext.Participants.Where(p => p.EventId == id && p.UserId == userId).FirstOrDefault();
                if (item == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "This user is not invited.");
                }
                item.IsConfirmed = true;
                _dbContext.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
