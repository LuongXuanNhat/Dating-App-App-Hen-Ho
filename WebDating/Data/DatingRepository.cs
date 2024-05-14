using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDating.DTOs;
using WebDating.Entities;
using WebDating.Interfaces;

namespace WebDating.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public DatingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DatingProfileVm> Insert(DatingProfile datingProfile)
        {
            await _context.DatingProfiles.AddAsync(datingProfile);
            return _mapper.Map<DatingProfileVm>(datingProfile);
        }

        public async Task InsertUserInterest(IEnumerable<UserInterest> userInterests, int datingId)
        {
            List<UserInterest> interests = userInterests.ToList();
            foreach (var item in interests)
            {
                var userInterest = new UserInterest()
                {
                    DatingProfileId = datingId,
                    InterestName = item.InterestName,
                };
                await _context.UserInterests.AddRangeAsync(userInterest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
