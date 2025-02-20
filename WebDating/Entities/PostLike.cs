using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WebDating.Entities
{
    [Table("PostLike")]
    public class PostLike
    {
        [Key]
        public int Id { get; set; }
        [StringLength(255)]
        public int PostId { get; set; }
        public int? UserId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [Newtonsoft.Json.JsonIgnore] 
        public virtual Post Post { get; set; }
        public virtual AppUser User { get; set; }
    }
  
}
