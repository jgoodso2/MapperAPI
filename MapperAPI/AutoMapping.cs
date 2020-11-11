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
            //CreateMap<AuthorizedPlanViewProjectDto, AuthorizedPlanViewProject>();  
            CreateMap<AuthorizedPlanViewProject, AuthorizedPlanViewProjectDto>();
            CreateMap<PlanViewProject, PlanViewProjectDto>();
            CreateMap<Project, ProjectDto>();
            CreateMap<PlanViewProject, PlanViewProjectDto>();
            CreateMap<PlanViewProject, PlanViewProjectsForCreationDto>();
            CreateMap<PlanViewProjectsForCreationDto, PlanViewProject>();
            CreateMap<ProjectWithoutPlanViewProjectsDto, Project>();
            CreateMap<Project, ProjectWithoutPlanViewProjectsDto>();

        }
    }
}
