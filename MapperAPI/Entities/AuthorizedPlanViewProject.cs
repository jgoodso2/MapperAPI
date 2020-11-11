using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapperAPI.Entities
{
    public class AuthorizedPlanViewProject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ppl_Code { get; set; }

        public string Name { get; set; }

        public string plan_id { get; set; }

        public string proj_mgr { get; set; }

        public string projectSponsor { get; set; }

        public string alt_pm { get; set; }
    }

    public class AdminUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(10)]
        public string account34 { get; set; }
    }
}