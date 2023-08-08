using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventsDemoAPI.Models
{
    public class MTimezone
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(127)]
        public string value { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(127)]
        public string abbr { get; set; }
        public float offset { get; set; }
        public bool isdst { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(127)]
        public string text { get; set; }
    }
}
