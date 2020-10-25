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
     //[Route("api/Projects")]

    [Route("[controller]")]

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
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55)
                
            })
            .ToArray();
        }

        [HttpGet("Admin/AuthorizedPlanViewProjects")]
        public IActionResult GetAuthorizedPlanViewProjects()
        {
            var authorizedProjectEntities = _ProjectInfoRepository.GetAuthorizedPlanViewProjects();
            var results = _mapper.Map<IEnumerable<AuthorizedPlanViewProjectDto>>(authorizedProjectEntities);

            return Ok(results);
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

            var finalPlanViewProject = _mapper.Map<Entities.PlanViewProject>(PlanViewProject);

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
    }
}
