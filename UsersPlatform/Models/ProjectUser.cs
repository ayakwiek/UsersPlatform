using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersPlatform.Areas.Identity.Data;

namespace UsersPlatform.Models
{
    public class ProjectUser
    {
        //joining entity
        public string UserId { get; set; } //foreign key
        public ApplicationUser User { get; set; } //reference navigation property

        public int ProjectId { get; set; } //foreign key
        public Project Project { get; set; } //reference navigation property
    }
}

