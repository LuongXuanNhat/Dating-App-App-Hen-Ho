using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDating.Entities
{
    [Table("Post")]
    public class Post
    {
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostLikes = new HashSet<PostLike>();
            PostReportDetails = new HashSet<PostReportDetail>();
        }

        [Key]
        [StringLength(255)]
        public int Id { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; } = false;

        public AppUser User { get; set; }
        public ICollection<PostComment> PostComments { get; set; }
        public ICollection<ImagePost> Images { get; set; }
        public ICollection<PostLike> PostLikes { get; set; }
        public ICollection<PostReportDetail> PostReportDetails { get; set; }

    }
  
}
