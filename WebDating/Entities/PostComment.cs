using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDating.Entities
{
    [Table("PostComment")]
    public class PostComment
    {
        public PostComment()
        {
            PostSubComments = new HashSet<PostSubComment>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }

        public Post Post { get; set; }
        public AppUser User { get; set; }
        public ICollection<PostSubComment> PostSubComments { get; set; }
    }
  
}
