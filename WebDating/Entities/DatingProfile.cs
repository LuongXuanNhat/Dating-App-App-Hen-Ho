using System.ComponentModel.DataAnnotations;

namespace WebDating.Entities
{
    public class DatingProfile
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        // Đối tượng hẹn hò
        public Gender DatingObject { get; set; }
        // Chiều cao
        public Height Height { get; set; } 
        // Địa điểm muốn hẹn hò - Tỉnh thành
        public Provice WhereToDate { get; set; }
        // Sở thích
        public IEnumerable<UserInterest> UserInterests { get; set; }
        public AppUser User { get; set; }
    }
}
