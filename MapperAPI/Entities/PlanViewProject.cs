using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapperAPI.Entities
{
    public class PlanViewProject
    {
        [Required]
     
        public string ProjectName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(200)]
        public string Id { get; set; }

        public string ppl_Code { get; set; }
        public string mappedBy34 { get; set; }

        public string mappedByName { get; set; }

        [ForeignKey("ProjectGuid")]
        public Project Project { get; set; }

        public Guid ProjectGuid { get; set; }
    }
}
/*
 * namespace ProjectInfo.API.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    public class PlanViewProject
    {
        [Required, MaxLength(50)]
        public string ProjectName { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), MaxLength(200)]
        public string Id { get; set; }

        public string ppl_Code { get; set; }

        public string mappedBy34 { get; set; }

        public string mappedByName { get; set; }

        [ForeignKey("ProjectGuid")]
        public ProjectInfo.API.Entities.Project Project { get; set; }

        public Guid ProjectGuid { get; set; }
    }
}

 */
