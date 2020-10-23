using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapperAPI.Domain.Entities
{
    public class AuthorizedPlanViewProject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ppl_Code { get; set; }
        public string Name { get; set; }
    }
}
