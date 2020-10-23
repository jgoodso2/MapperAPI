using System;
using System.Collections.Generic;
using System.Text;

namespace MapperAPI.Data
{
    public class ProjectDto
    {
        public Guid ProjectGuid { get; set; }
        public string ProjectName { get; set; }


        public ICollection<PlanViewProjectDto> PlanViewProjects { get; set; }
        = new List<PlanViewProjectDto>();
    }
}
