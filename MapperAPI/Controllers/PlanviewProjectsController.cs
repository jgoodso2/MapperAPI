using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MapperAPI.Entities;
using MapperAPI.Services;
using MapperAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;

using Microsoft.AspNetCore.Mvc.NewtonsoftJson;



namespace MapperAPI.Controllers
{
    [ApiController]
    [Route("api/Projects")]

    //[Route("[controller]")]

    [RequireHttpsAttribute]
    public class PlanViewProjectsController : ControllerBase
    {
        private ILogger<PlanViewProjectsController> _logger;

        private IProjectInfoRepository _ProjectInfoRepository;
        private readonly IMapper _mapper;

        public PlanViewProjectsController(ILogger<PlanViewProjectsController> logger,

            IProjectInfoRepository ProjectInfoRepository,
             IMapper mapper)
        {
            _logger = logger;
            //_mailService = mailService;
            _ProjectInfoRepository = ProjectInfoRepository;
            _mapper = mapper;
        }

        [HttpGet("{projectUid}")]
        public IActionResult GetPerViewProject(Guid projectUid)
        {
            if (!_ProjectInfoRepository.ProjectExists(projectUid))
            {
                _logger.LogInformation($"Project with id {projectUid} is not a mappable project.");
                return NotFound();
            }
            else
            {
                var PerViewProject = _ProjectInfoRepository.GetProject(projectUid, false);
                var PerViewProjectResult = _mapper.Map<ProjectWithoutPlanViewProjectsDto>(PerViewProject);
                return Ok(PerViewProjectResult); 
                //return Ok("Saul Goodman"); 

            }
        }

        [HttpGet("{projectUid}/PlanViewProjects")]
        public IActionResult GetPlanViewProjects(Guid projectUid)
        {
            try
            {
                if (!_ProjectInfoRepository.ProjectExists(projectUid))
                {
                    _logger.LogInformation($"Project with id {projectUid} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                var PlanViewProjectsForProject = _ProjectInfoRepository.GetPlanViewProjectsForProject(projectUid);
                var PlanViewProjectsForProjectResults =
                                   _mapper.Map<IEnumerable<PlanViewProjectDto>>(PlanViewProjectsForProject);

                return Ok(PlanViewProjectsForProjectResults);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for Project with id {projectUid}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
        [HttpDelete("{projectUid}/PlanViewProjects/{ppl_code}")]
        public IActionResult DeletePlanViewProject(Guid projectUid, string ppl_code)
        {
            if (!_ProjectInfoRepository.ProjectExists(projectUid))
            {
                return NotFound();
            }

            var PlanViewProjectEntity = _ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (PlanViewProjectEntity == null)
            {
                return NotFound();
            }

            _ProjectInfoRepository.DeletePlanViewProject(PlanViewProjectEntity);

            if (!_ProjectInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            // _mailService.Send("Point of interest deleted.",
            //        $"Point of interest {PlanViewProjectEntity.ProjectName} with id {PlanViewProjectEntity.ppl_Code} was deleted.");

            return NoContent();
        }


        [HttpGet("Admin/AuthorizedPlanViewProjects")]
        public IActionResult GetAuthorizedPlanViewProjects()
        {
            var authorizedProjectEntities = _ProjectInfoRepository.GetAuthorizedPlanViewProjects();
            if (authorizedProjectEntities.Count() == 0)
            {
                return Ok(new List<AuthorizedPlanViewProjectDto>());
            }
            var results = _mapper.Map<IEnumerable<AuthorizedPlanViewProjectDto>>(authorizedProjectEntities);

            return Ok(results);
        }

        [HttpGet("Admin/AuthorizedPlanViewProjects/{userID}")]
        public IActionResult GetAuthorizedPlanViewProjects(string userID)
        {

            var authorizedProjectEntities = _ProjectInfoRepository.GetAuthorizedPlanViewProjects(userID);
            if (authorizedProjectEntities.Count() == 0)
            {
                return Ok(new List<AuthorizedPlanViewProjectDto>());
            }
            var results = _mapper.Map<IEnumerable<AuthorizedPlanViewProjectDto>>(authorizedProjectEntities);

            return Ok(results);
        }




        [HttpGet("{projectUid}/PlanViewProjects/{ppl_code}", Name = "GetPlanViewProject")]
        public IActionResult GetPlanViewProject(Guid projectUid, string ppl_code)
        {
            if (!_ProjectInfoRepository.ProjectExists(projectUid))
            {
                return NotFound();
            }

            var PlanViewProject = _ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);

            if (PlanViewProject == null)
            {
                return NotFound();
            }

            var PlanViewProjectResult = _mapper.Map<PlanViewProjectDto>(PlanViewProject);
            return Ok(PlanViewProjectResult);
        }

        [HttpPost("{projectUid}/PlanViewProjects")]
        public IActionResult CreatePlanViewProject(Guid projectUid,
            [FromBody] PlanViewProjectsForCreationDto PlanViewProject)

        {
            if (PlanViewProject == null)
            {
                return BadRequest();
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ProjectInfoRepository.ProjectExists(projectUid))
            {
                return NotFound();
            }

            var finalPlanViewProject = _mapper.Map<PlanViewProject>(PlanViewProject);

            _ProjectInfoRepository.AddPlanViewProjectForProject(projectUid, finalPlanViewProject);

            if (!_ProjectInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPlanViewProjectToReturn = _mapper.Map<Models.PlanViewProjectDto>(finalPlanViewProject);     ////////////

            return CreatedAtRoute("GetPlanViewProject", new
            { projectUid = projectUid, ppl_code = createdPlanViewProjectToReturn.ppl_Code }, createdPlanViewProjectToReturn);
        }

        [HttpPut("{projectUid}/PlanViewProjects/{ppl_code}")]
        public IActionResult UpdatePlanViewProject(Guid projectUid, string ppl_code,
            [FromBody] PlanViewProjectsForUpdateDto PlanViewProject)
        {
            if (PlanViewProject == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ProjectInfoRepository.ProjectExists(projectUid))
            {
                return NotFound();
            }

            var PlanViewProjectEntity = _ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (PlanViewProjectEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(PlanViewProject, PlanViewProjectEntity);

            if (!_ProjectInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }


        [HttpPatch("{projectUid}/PlanViewProjects/{ppl_code}")]
        public IActionResult PartiallyUpdatePlanViewProject(Guid projectUid, string ppl_code,
            [FromBody] JsonPatchDocument<PlanViewProjectsForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_ProjectInfoRepository.ProjectExists(projectUid))
            {
                return NotFound();
            }

            var PlanViewProjectEntity = _ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (PlanViewProjectEntity == null)
            {
                return NotFound();
            }

            var PlanViewProjectToPatch = _mapper.Map<PlanViewProjectsForUpdateDto>(PlanViewProjectEntity);

            patchDoc.ApplyTo(PlanViewProjectToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            TryValidateModel(PlanViewProjectToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(PlanViewProjectToPatch, PlanViewProjectEntity);

            if (!_ProjectInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }


    }
}
/*
namespace ProjectInfo.API.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ProjectInfo.API.Entities;
    using ProjectInfo.API.Models;
    using ProjectInfo.API.Services;
    using System;
    using System.Collections.Generic;

    [Route("api/Projects"), RequireHttps]
    public class PlanViewProjectsController : Controller
    {
        private ILogger<PlanViewProjectsController> _logger;
        private IProjectInfoRepository _ProjectInfoRepository;

        public PlanViewProjectsController(ILogger<PlanViewProjectsController> logger, IProjectInfoRepository ProjectInfoRepository)
        {
            this._logger = logger;
            this._ProjectInfoRepository = ProjectInfoRepository;
        }

        [HttpPost("{projectUid}/PlanViewProjects")]
        public IActionResult CreatePlanViewProject(Guid projectUid, [FromBody] PlanViewProjectsForCreationDto PlanViewProject)
        {
            if (PlanViewProject == null)
            {
                return this.BadRequest();
            }
            if (!base.ModelState.IsValid)
            {
                return this.BadRequest(base.ModelState);
            }
            if (!this._ProjectInfoRepository.ProjectExists(projectUid))
            {
                return this.NotFound();
            }
            PlanViewProject planViewProject = Mapper.Map<PlanViewProject>(PlanViewProject);
            this._ProjectInfoRepository.AddPlanViewProjectForProject(projectUid, planViewProject);
            if (!this._ProjectInfoRepository.Save())
            {
                return this.StatusCode(500, "A problem happened while handling your request.");
            }
            PlanViewProjectDto dto = Mapper.Map<PlanViewProjectDto>(planViewProject);
            return this.CreatedAtRoute("GetPlanViewProject", new { 
                projectUid = projectUid,
                ppl_code = dto.ppl_Code
            }, dto);
        }

        [HttpDelete("{projectUid}/PlanViewProjects/{ppl_code}")]
        public IActionResult DeletePlanViewProject(Guid projectUid, string ppl_code)
        {
            if (!this._ProjectInfoRepository.ProjectExists(projectUid))
            {
                return this.NotFound();
            }
            PlanViewProject planViewProjectForProject = this._ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (planViewProjectForProject == null)
            {
                return this.NotFound();
            }
            this._ProjectInfoRepository.DeletePlanViewProject(planViewProjectForProject);
            return (this._ProjectInfoRepository.Save() ? ((IActionResult) this.NoContent()) : ((IActionResult) this.StatusCode(500, "A problem happened while handling your request.")));
        }

        [HttpGet("Admin/AuthorizedPlanViewProjects")]
        public IActionResult GetAuthorizedPlanViewProjects()
        {
            IEnumerable<AuthorizedPlanViewProjectDto> enumerable2 = Mapper.Map<IEnumerable<AuthorizedPlanViewProjectDto>>(this._ProjectInfoRepository.GetAuthorizedPlanViewProjects());
            return this.Ok(enumerable2);
        }

        [HttpGet("Admin/AuthorizedPlanViewProjects/{userID}")]
        public IActionResult GetAuthorizedPlanViewProjects(string userID)
        {
            IEnumerable<AuthorizedPlanViewProjectDto> enumerable2 = Mapper.Map<IEnumerable<AuthorizedPlanViewProjectDto>>(this._ProjectInfoRepository.GetAuthorizedPlanViewProjects(userID));
            return this.Ok(enumerable2);
        }

        [HttpGet("{projectUid}/PlanViewProjects/{ppl_code}", Name="GetPlanViewProject")]
        public IActionResult GetPlanViewProject(Guid projectUid, string ppl_code)
        {
            if (!this._ProjectInfoRepository.ProjectExists(projectUid))
            {
                return this.NotFound();
            }
            PlanViewProject planViewProjectForProject = this._ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (planViewProjectForProject == null)
            {
                return this.NotFound();
            }
            PlanViewProjectDto dto = Mapper.Map<PlanViewProjectDto>(planViewProjectForProject);
            return this.Ok(dto);
        }

        [HttpGet("{projectUid}/PlanViewProjects")]
        public IActionResult GetPlanViewProjects(Guid projectUid)
        {
            IActionResult result;
            try
            {
                if (!this._ProjectInfoRepository.ProjectExists(projectUid))
                {
                    this._logger.LogInformation($"Project with id {projectUid} wasn't found when accessing points of interest.", Array.Empty<object>());
                    result = this.NotFound();
                }
                else
                {
                    IEnumerable<PlanViewProjectDto> enumerable2 = Mapper.Map<IEnumerable<PlanViewProjectDto>>(this._ProjectInfoRepository.GetPlanViewProjectsForProject(projectUid));
                    result = this.Ok(enumerable2);
                }
            }
            catch (Exception exception)
            {
                object[] args = new object[] { exception };
                this._logger.LogCritical($"Exception while getting points of interest for Project with id {projectUid}.", args);
                result = this.StatusCode(500, "A problem happened while handling your request.");
            }
            return result;
        }

        [HttpPatch("{projectUid}/PlanViewProjects/{ppl_code}")]
        public IActionResult PartiallyUpdatePlanViewProject(Guid projectUid, string ppl_code, [FromBody] JsonPatchDocument<PlanViewProjectsForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return this.BadRequest();
            }
            if (!this._ProjectInfoRepository.ProjectExists(projectUid))
            {
                return this.NotFound();
            }
            PlanViewProject planViewProjectForProject = this._ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (planViewProjectForProject == null)
            {
                return this.NotFound();
            }
            PlanViewProjectsForUpdateDto objectToApplyTo = Mapper.Map<PlanViewProjectsForUpdateDto>(planViewProjectForProject);
            patchDoc.ApplyTo<PlanViewProjectsForUpdateDto>(objectToApplyTo, base.ModelState);
            if (!base.ModelState.IsValid)
            {
                return this.BadRequest(base.ModelState);
            }
            this.TryValidateModel(objectToApplyTo);
            if (!base.ModelState.IsValid)
            {
                return this.BadRequest(base.ModelState);
            }
            Mapper.Map<PlanViewProjectsForUpdateDto, PlanViewProject>(objectToApplyTo, planViewProjectForProject);
            return (this._ProjectInfoRepository.Save() ? ((IActionResult) this.NoContent()) : ((IActionResult) this.StatusCode(500, "A problem happened while handling your request.")));
        }

        [HttpPut("{projectUid}/PlanViewProjects/{ppl_code}")]
        public IActionResult UpdatePlanViewProject(Guid projectUid, string ppl_code, [FromBody] PlanViewProjectsForUpdateDto PlanViewProject)
        {
            if (PlanViewProject == null)
            {
                return this.BadRequest();
            }
            if (!base.ModelState.IsValid)
            {
                return this.BadRequest(base.ModelState);
            }
            if (!this._ProjectInfoRepository.ProjectExists(projectUid))
            {
                return this.NotFound();
            }
            PlanViewProject planViewProjectForProject = this._ProjectInfoRepository.GetPlanViewProjectForProject(projectUid, ppl_code);
            if (planViewProjectForProject == null)
            {
                return this.NotFound();
            }
            Mapper.Map<PlanViewProjectsForUpdateDto, PlanViewProject>(PlanViewProject, planViewProjectForProject);
            return (this._ProjectInfoRepository.Save() ? ((IActionResult) this.NoContent()) : ((IActionResult) this.StatusCode(500, "A problem happened while handling your request.")));
        }

        public string UserIdentity =>
            base.HttpContext.Request.Query["AccountID"].ToString();
    }
}

*/
