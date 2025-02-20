using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDating.Entities
{
    [Table("PostReportDetail")]
    public class PostReportDetail
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string? Description { get; set; }
        public DateTime ReportDate { get; set; }
        public bool Checked { get; set; } = false;
        public Report Report { get; set; }
        public Post Post { get; set; }
        public AppUser User { get; set; }
    }


}
