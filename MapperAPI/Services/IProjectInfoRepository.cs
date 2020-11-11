using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapperAPI.Entities; 

namespace MapperAPI.Services
{
    public interface IProjectInfoRepository
    {
        bool ProjectExists(Guid projectUid);
        IEnumerable<Project> GetProjects();
        Project GetProject(Guid projectUid, bool includePlanViewProjects);
        IEnumerable<PlanViewProject> GetPlanViewProjectsForProject(Guid projectUid);
        PlanViewProject GetPlanViewProjectForProject(Guid projectUid, string ppl_code);
        void AddPlanViewProjectForProject(Guid projectUid, PlanViewProject PlanViewProject);
        void AddProject(Project PlanViewProject);
        void DeletePlanViewProject(PlanViewProject PlanViewProject);
        void DeleteProject(Project Project);

        IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects();
        IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects(string userId);
        bool Save();
    }
}

//namespace ProjectInfo.API.Services
//{
//    using ProjectInfo.API.Entities;
//    using System;
//    using System.Collections.Generic;

//    public interface IProjectInfoRepository
//    {
//        void AddPlanViewProjectForProject(Guid projectUid, PlanViewProject PlanViewProject);
//        void AddProject(Project PlanViewProject);
//        void DeletePlanViewProject(PlanViewProject PlanViewProject);
//        void DeleteProject(Project Project);
//        IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects();
//        IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects(string userId);
//        PlanViewProject GetPlanViewProjectForProject(Guid projectUid, string ppl_code);
//        IEnumerable<PlanViewProject> GetPlanViewProjectsForProject(Guid projectUid);
//        Project GetProject(Guid projectUid, bool includePlanViewProjects);
//        IEnumerable<Project> GetProjects();
//        bool ProjectExists(Guid projectUid);
//        bool Save();
//    }
//}

