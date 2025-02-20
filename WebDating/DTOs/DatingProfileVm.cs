using WebDating.Entities;

namespace WebDating.DTOs
{
    public class DatingProfileVm
    {
        public int Id { get; set; }
        // Đối tượng hẹn hò
        public Gender DatingObject { get; set; }
        // Chiều cao
        public Height Height { get; set; }
        // Địa điểm muốn hẹn hò - Tỉnh thành
        public Provice WhereToDate { get; set; }
        // Sở thích
        public IEnumerable<UserInterestVm> UserInterests { get; set; }
    }
    public class DatingProfileDto
    {
        public int Id { get; set; }
        // Đối tượng hẹn hò
        public string DatingObject { get; set; }
        // Chiều cao
        public string Height { get; set; }
        // Địa điểm muốn hẹn hò - Tỉnh thành
        public string WhereToDate { get; set; }
        // Sở thích
        public List<UserInterestDto> UserInterests { get; set; }
    }
}
