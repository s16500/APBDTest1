using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test1APBD.Models
{
    public class TeamMember
    {

        public int idTeamMember { get; set; }

        public string firstname { get; set; }
     
        public string taskname { get; set; }

        public string description { get; set; }

        public DateTime deadline { get; set; }
        public string tasktype { get; set; }
        public string projectname { get; set; }

    }
}
