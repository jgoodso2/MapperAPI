using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapperAPI.Models

{
    public class AuthorizedPlanViewProjectDto
    {
        public string ppl_Code { get; set; }

        public string Name { get; set; }

        public string plan_id { get; set; }

        public string proj_mgr { get; set; } 
        public string projectSponsor { get; set; }

    }
}
