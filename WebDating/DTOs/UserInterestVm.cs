﻿using WebDating.Entities;

namespace WebDating.DTOs
{
    public class UserInterestVm
    {
        public int Id { get; set; }
        public int DatingProfileId { get; set; }
        public Interest InterestName { get; set; }
    }
    public class UserInterestDto
    {
        public int Id { get; set; }
        public int DatingProfileId { get; set; }
        public string InterestName { get; set; }
    }

}
