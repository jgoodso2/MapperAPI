using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapperAPI.Domain.Entities
{
    public class PlanViewProject
    {
        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(200)]
        public string Id { get; set; }

        public string ppl_Code { get; set; }

        [ForeignKey("ProjectGuid")]
        public Project Project { get; set; }

        public Guid ProjectGuid { get; set; }
    }
}
