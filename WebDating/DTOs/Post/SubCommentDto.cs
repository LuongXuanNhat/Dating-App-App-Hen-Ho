using System.ComponentModel.DataAnnotations.Schema;

namespace WebDating.DTOs.Post
{
    public class SubCommentDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        public UserShortDto? UserShort { get; set; }
    }
}
