using System;
using System.Collections.Generic;
using System.Text;

namespace MapperAPI.Models

{
    public class PlanViewProjectDto
    {
        public Guid ProjectGuid { get; set; }
        public string ProjectName { get; set; }
        public string ppl_Code { get; set; }

        //public string Description
        //{
        //    get { return Description; }
        //    set { Description = value; }
    }
}
