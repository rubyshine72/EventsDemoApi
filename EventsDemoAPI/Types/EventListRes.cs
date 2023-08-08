namespace EventsDemoAPI.Types
{
    public class EventListRes<T>
    {
        public List<T> data { get; set; }
        public int total { get; set; }
        public int start { get; set; }
        public int rows { get; set; }
    }
}
