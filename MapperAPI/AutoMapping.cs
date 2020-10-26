using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapperAPI
{
    using AutoMapper;
    using MapperAPI.Entities;
    using MapperAPI.Models;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AuthorizedPlanViewProjectDto, AuthorizedPlanViewProject>(); // means you want to map from User to UserDTO
            CreateMap<AuthorizedPlanViewProject, AuthorizedPlanViewProjectDto>();
        }
    }
}
