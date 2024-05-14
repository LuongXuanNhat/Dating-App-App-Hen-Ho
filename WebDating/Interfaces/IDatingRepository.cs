using WebDating.DTOs;
using WebDating.Entities;

namespace WebDating.Interfaces
{
    public interface IDatingRepository
    {
        Task<DatingProfileVm> Insert(DatingProfile datingProfile);
        Task InsertUserInterest(IEnumerable<UserInterest> userInterests, int datingId);
    }
}
