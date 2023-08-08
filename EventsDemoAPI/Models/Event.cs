using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventsDemoAPI.Models
{
    public class Event: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(511)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public List<Participant> Participants { get; set; }
        [ForeignKey("MTimezone")]
        public int TimezoneId { get; set; }
        public MTimezone Timezone { get; set; }
    }
}
