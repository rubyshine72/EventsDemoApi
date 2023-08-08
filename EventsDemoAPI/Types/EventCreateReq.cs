namespace EventsDemoAPI.Types
{
    public class EventCreateReq
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime startAt { get; set; }
        public DateTime endAt { get; set; }
        public int timezoneId { get; set; }
        public int[]? participants { get; set; }
    }
}
