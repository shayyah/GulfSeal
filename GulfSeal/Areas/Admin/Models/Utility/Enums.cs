using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GulfSeal.Areas.Admin.Models.Utility
{
    public static class Enums
    {

        public  enum ProjectType { Project = 1, Application = 2}

        public static ProjectType getProjectType(int index)
        {
            if (index == 1)
                return ProjectType.Project;

            return ProjectType.Application; 
        }
         

    }
}