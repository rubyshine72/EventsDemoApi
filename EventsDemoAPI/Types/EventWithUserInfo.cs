using EventsDemoAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EventsDemoAPI.Libraries;

namespace EventsDemoAPI.Types
{
    public class ParticipantWithUserInfo : Participant
    {
        public ParticipantWithUserInfo(Participant part)
        {
            this.Id = part.Id;
            this.UserId = part.UserId;
            this.EventId = part.EventId;
            this.IsConfirmed = part.IsConfirmed;
            this.CreatedAt = part.CreatedAt;
            this.UpdatedAt = part.UpdatedAt;
            this.User = ExternalApi.getUserInfo(part.UserId);
        }
        public dynamic? User;
    }

    public class EventWithUserInfo: Event
    {  
        public EventWithUserInfo(Event evt)
        {
            this.Id = evt.Id;
            this.Title = evt.Title;
            this.Description = evt.Description;
            this.StartAt = evt.StartAt;
            this.EndAt = evt.EndAt;
            this.Participants = evt.Participants.Select(p => new ParticipantWithUserInfo(p)).ToList();
            this.TimezoneId = evt.TimezoneId;
            this.Timezone = evt.Timezone;
            this.CreatedAt = evt.CreatedAt;
            this.UpdatedAt = evt.UpdatedAt;
        }

        public List<ParticipantWithUserInfo> Participants;
    }
}
