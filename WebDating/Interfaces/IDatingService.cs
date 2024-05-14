using WebDating.DTOs;

namespace WebDating.Interfaces
{
    public interface IDatingService
    {
        Task<ResultDto<DatingProfileVm>> InitDatingProfile(DatingProfileVm datingProfileVm, string userName);
    }
}
