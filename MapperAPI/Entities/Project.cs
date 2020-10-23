using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapperAPI.Entities
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProjectGuid { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        public ICollection<PlanViewProject> PlanViewProjects { get; set; }
               = new List<PlanViewProject>();
    }
}
