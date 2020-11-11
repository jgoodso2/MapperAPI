using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MapperAPI.Entities;
using Microsoft.EntityFrameworkCore; 

namespace MapperAPI.Services
{
    public class ProjectInfoRepository : IProjectInfoRepository
    {
        private MapperContext _context;
        public ProjectInfoRepository(MapperContext context)
        {
            _context = context;
        }
        public void AddProject(Project project)
        {
            _context.Projects.Add(project);
        }

        public void AddPlanViewProjectForProject(Guid projectUid, PlanViewProject PlanViewProject)
        {
            var Project = GetProject(projectUid, false);
            Project.PlanViewProjects.Add(PlanViewProject);
        }

        public bool ProjectExists(Guid projectUid)
        {
            return _context.Projects.Any(c => c.ProjectGuid == projectUid);
        }

        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.OrderBy(c => c.ProjectName).ToList();
        }

        public Project GetProject(Guid projectUId, bool includePlanViewProjects)
        {
            if (includePlanViewProjects)
            {
                return _context.Projects.Include(c => c.PlanViewProjects)   //////////////////////////////////////////////
                    .Where(c => c.ProjectGuid == projectUId).FirstOrDefault();
            }

            return _context.Projects.Where(c => c.ProjectGuid == projectUId).FirstOrDefault();
        }

        public PlanViewProject GetPlanViewProjectForProject(Guid projectUid, string ppl_code)
        {
            return _context.PlanViewProjects
               .Where(p => p.ProjectGuid == projectUid && p.ppl_Code == ppl_code).FirstOrDefault();
        }

        public IEnumerable<PlanViewProject> GetPlanViewProjectsForProject(Guid projectUid)
        {
            return _context.PlanViewProjects
                           .Where(p => p.ProjectGuid == projectUid).ToList();
        }

        public void DeletePlanViewProject(PlanViewProject PlanViewProject)
        {
            _context.PlanViewProjects.Remove(PlanViewProject);
        }

        public void DeleteProject(Project Project)
        {
            foreach (var planViewProject in Project.PlanViewProjects)
            {
                DeletePlanViewProject(planViewProject);
            }
            _context.Projects.Remove(Project);
        }

        public IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects()
        {
            return _context.AuthorizedPlanViewProjects.ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects(string userId)
        {
            //if( AdminUser )

            //  if userID is in the AdminUsers table then
            // return _context.AuthorizedPlanViewProjects.ToList();
            //select * from AuthorizedPlanViewProjects 
            /// where alt_pm = userId
            /// 
            //_context.Projects.Any(c => c.ProjectGuid == projectUid);
            if (_context.AdminUsers.Any(u => u.account34 == userId))
            {
                return _context.AuthorizedPlanViewProjects.ToList();
            }
            else
            {
                return _context.AuthorizedPlanViewProjects.Where(p => p.alt_pm == userId); 
            }
            //var foo = _context.Projects; 
            //var bar = _context.AdminUsers; 
            //var allAdmins = _context.AdminUsers.ToList();

            //var authProjs = _context.AuthorizedPlanViewProjects.Where(p => p.alt_pm == "ejz7102"); 
             

            //throw new NotImplementedException();
        }
    }
}
//namespace ProjectInfo.API.Services
//{
//    using Microsoft.EntityFrameworkCore;
//    using Microsoft.EntityFrameworkCore.Query;
//    using ProjectInfo.API.Entities;
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Linq.Expressions;
//    using System.Reflection;
//    using System.Runtime.CompilerServices;

//    public class ProjectInfoRepository : IProjectInfoRepository
//    {
//        private ProjectContext _context;

//        public ProjectInfoRepository(ProjectContext context)
//        {
//            this._context = context;
//        }

//        public void AddPlanViewProjectForProject(Guid projectUid, PlanViewProject PlanViewProject)
//        {
//            this.GetProject(projectUid, false).PlanViewProjects.Add(PlanViewProject);
//        }

//        public void AddProject(Project project)
//        {
//            this._context.Projects.Add(project);
//        }

//        public void DeletePlanViewProject(PlanViewProject PlanViewProject)
//        {
//            this._context.PlanViewProjects.Remove(PlanViewProject);
//        }

//        public void DeleteProject(Project Project)
//        {
//            foreach (PlanViewProject project in Project.PlanViewProjects)
//            {
//                this.DeletePlanViewProject(project);
//            }
//            this._context.Projects.Remove(Project);
//        }

//        public IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects() =>
//            (IEnumerable<AuthorizedPlanViewProject>)((IEnumerable<AuthorizedPlanViewProject>)this._context.AuthorizedPlanViewProjects).ToList<AuthorizedPlanViewProject>();

//        public IEnumerable<AuthorizedPlanViewProject> GetAuthorizedPlanViewProjects(string userId)
//        {
//            <> c__DisplayClass12_0 class_;
//            ParameterExpression expression = Expression.Parameter((Type)typeof(AdminUser), "a");
//            ParameterExpression[] parameters = new ParameterExpression[] { expression };
//            if (((IQueryable<AdminUser>)this._context.AdminUsers).Any<AdminUser>(Expression.Lambda<Func<AdminUser, bool>>(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(AdminUser.get_account34)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass12_0)), (FieldInfo)fieldof(<> c__DisplayClass12_0.userId))), parameters)))
//            {
//                return (IEnumerable<AuthorizedPlanViewProject>)((IEnumerable<AuthorizedPlanViewProject>)this._context.AuthorizedPlanViewProjects).ToList<AuthorizedPlanViewProject>();
//            }
//            expression = Expression.Parameter((Type)typeof(AuthorizedPlanViewProject), "p");
//            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
//            return (IEnumerable<AuthorizedPlanViewProject>)((IEnumerable<AuthorizedPlanViewProject>)((IQueryable<AuthorizedPlanViewProject>)this._context.AuthorizedPlanViewProjects).Where<AuthorizedPlanViewProject>(Expression.Lambda<Func<AuthorizedPlanViewProject, bool>>(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(AuthorizedPlanViewProject.get_alt_pm)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass12_0)), (FieldInfo)fieldof(<> c__DisplayClass12_0.userId))), expressionArray2))).ToList<AuthorizedPlanViewProject>();
//        }

//        public PlanViewProject GetPlanViewProjectForProject(Guid projectUid, string ppl_code)
//        {
//            <> c__DisplayClass7_0 class_;
//            ParameterExpression expression = Expression.Parameter((Type)typeof(PlanViewProject), "p");
//            ParameterExpression[] parameters = new ParameterExpression[] { expression };
//            return ((IQueryable<PlanViewProject>)this._context.PlanViewProjects).Where<PlanViewProject>(Expression.Lambda<Func<PlanViewProject, bool>>(Expression.AndAlso(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(PlanViewProject.get_ProjectGuid)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass7_0)), (FieldInfo)fieldof(<> c__DisplayClass7_0.projectUid)), false, (MethodInfo)methodof(Guid.op_Equality)), Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(PlanViewProject.get_ppl_Code)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass7_0)), (FieldInfo)fieldof(<> c__DisplayClass7_0.ppl_code)))), parameters)).FirstOrDefault<PlanViewProject>();
//        }

//        public IEnumerable<PlanViewProject> GetPlanViewProjectsForProject(Guid projectUid)
//        {
//            <> c__DisplayClass8_0 class_;
//            ParameterExpression expression = Expression.Parameter((Type)typeof(PlanViewProject), "p");
//            ParameterExpression[] parameters = new ParameterExpression[] { expression };
//            return (IEnumerable<PlanViewProject>)((IEnumerable<PlanViewProject>)((IQueryable<PlanViewProject>)this._context.PlanViewProjects).Where<PlanViewProject>(Expression.Lambda<Func<PlanViewProject, bool>>(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(PlanViewProject.get_ProjectGuid)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass8_0)), (FieldInfo)fieldof(<> c__DisplayClass8_0.projectUid)), false, (MethodInfo)methodof(Guid.op_Equality)), parameters))).ToList<PlanViewProject>();
//        }

//        public Project GetProject(Guid projectUId, bool includePlanViewProjects)
//        {
//            <> c__DisplayClass6_0 class_;
//            ParameterExpression expression;
//            if (!includePlanViewProjects)
//            {
//                expression = Expression.Parameter((Type)typeof(Project), "c");
//                ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
//                return ((IQueryable<Project>)this._context.Projects).Where<Project>(Expression.Lambda<Func<Project, bool>>(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(Project.get_ProjectGuid)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass6_0)), (FieldInfo)fieldof(<> c__DisplayClass6_0.projectUId)), false, (MethodInfo)methodof(Guid.op_Equality)), expressionArray3)).FirstOrDefault<Project>();
//            }
//            expression = Expression.Parameter((Type)typeof(Project), "c");
//            ParameterExpression[] parameters = new ParameterExpression[] { expression };
//            IIncludableQueryable<Project, ICollection<PlanViewProject>> queryable1 = EntityFrameworkQueryableExtensions.Include<Project, ICollection<PlanViewProject>>((IQueryable<Project>)this._context.Projects, Expression.Lambda<Func<Project, ICollection<PlanViewProject>>>(Expression.Property(expression, (MethodInfo)methodof(Project.get_PlanViewProjects)), parameters));
//            expression = Expression.Parameter((Type)typeof(Project), "c");
//            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
//            return ((IQueryable<Project>)queryable1).Where<Project>(Expression.Lambda<Func<Project, bool>>(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(Project.get_ProjectGuid)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass6_0)), (FieldInfo)fieldof(<> c__DisplayClass6_0.projectUId)), false, (MethodInfo)methodof(Guid.op_Equality)), expressionArray2)).FirstOrDefault<Project>();
//        }

//        public IEnumerable<Project> GetProjects()
//        {
//            ParameterExpression expression = Expression.Parameter((Type)typeof(Project), "c");
//            ParameterExpression[] parameters = new ParameterExpression[] { expression };
//            return (IEnumerable<Project>)((IEnumerable<Project>)((IQueryable<Project>)this._context.Projects).OrderBy<Project, string>(Expression.Lambda<Func<Project, string>>(Expression.Property(expression, (MethodInfo)methodof(Project.get_ProjectName)), parameters))).ToList<Project>();
//        }

//        public bool ProjectExists(Guid projectUid)
//        {
//            <> c__DisplayClass4_0 class_;
//            ParameterExpression expression = Expression.Parameter((Type)typeof(Project), "c");
//            ParameterExpression[] parameters = new ParameterExpression[] { expression };
//            return ((IQueryable<Project>)this._context.Projects).Any<Project>(Expression.Lambda<Func<Project, bool>>(Expression.Equal(Expression.Property(expression, (MethodInfo)methodof(Project.get_ProjectGuid)), Expression.Field(Expression.Constant(class_, (Type)typeof(<> c__DisplayClass4_0)), (FieldInfo)fieldof(<> c__DisplayClass4_0.projectUid)), false, (MethodInfo)methodof(Guid.op_Equality)), parameters));
//        }

//        public bool Save() =>
//            this._context.SaveChanges() >= 0;

//        [Serializable, CompilerGenerated]
//        private sealed class <>c
//        {
//            public static readonly ProjectInfoRepository.<>c<>9 = new ProjectInfoRepository.<>c();
//    }
//}
//}
