using AutoMapper;
using WebDating.DTOs;
using WebDating.Entities;
using WebDating.Interfaces;

namespace WebDating.Services
{
    public class DatingService : IDatingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        public DatingService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ResultDto<DatingProfileVm>> InitDatingProfile(DatingProfileVm datingProfileVm, string userName)
        {
            try
            {
                var dating = _mapper.Map<DatingProfile>(datingProfileVm);
                var user = await _uow.UserRepository.GetUserByUsernameAsync(userName);
                dating.UserId = user.Id;
                var result = await _uow.DatingRepository.Insert(dating);
                await _uow.Complete();

                user.IsUpdatedDatingProfile = true;
                _uow.UserRepository.UpdateUser(user);
                _uow.CompleteNotAsync();

                await _uow.DatingRepository.InsertUserInterest(dating.UserInterests, dating.Id);

                return new SuccessResult<DatingProfileVm>(result);
            }
            catch (Exception e)
            {
                return new ErrorResult<DatingProfileVm>(e.Message);
            }
        }
    }
}
