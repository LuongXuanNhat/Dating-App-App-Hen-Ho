using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using WebDating.DTOs;
using WebDating.Entities;
using WebDating.Interfaces;
using WebDating.Utilities;

namespace WebDating.Controllers
{
    [ApiController]
    public class DatingProfileController : BaseController
    {
        private readonly IDatingService _service;
        public DatingProfileController(IDatingService service)
        {
            _service = service;
        }

        [HttpGet("GetDatingObject")]
        public ActionResult<IEnumerable<EnumT<Gender>>> GetDatingObject()
        {
            var result = Utils.GetAllAccountType<Gender>();
            return Ok(result);
        }

        [HttpGet("GetHeight")]
        public ActionResult<IEnumerable<EnumT<Height>>> GetHeight()
        {
            var result = Utils.GetAllAccountType<Height>();
            return Ok(result);
        }

        [HttpGet("GetWhereToDate")]
        public ActionResult<IEnumerable<EnumT<Provice>>> GetWhereToDate()
        {
            var result = Utils.GetAllAccountType<Provice>();
            return Ok(result);
        }

        [HttpGet("GetUserInterests")]
        public ActionResult<IEnumerable<EnumT<Interest>>> GetUserInterests()
        {
            var result = Utils.GetAllAccountType<Interest>();
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResultDto<DatingProfileVm>>> CreateDatingProfile([FromForm] DatingProfileVm datingProfile)
        {
            IReadOnlyList<UserInterestVm> UserInterestVm;
            {
                String formList = this.Request.Form["UserInterests"];
                UserInterestVm = JsonConvert.DeserializeObject<List<UserInterestVm>>(formList) ?? new();
                datingProfile.UserInterests = UserInterestVm.ToList();
            }
            var result = await _service.InitDatingProfile(datingProfile, User.Identity.Name);
            return Ok(result);
        }
    }
}
