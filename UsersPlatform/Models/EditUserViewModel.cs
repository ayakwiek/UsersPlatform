using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersPlatform.Models
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            DepartmentsList = new List<Department>();
            Roles = new List<string>();
        }

        public string Id { get; set; }
        public List<Department> DepartmentsList { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public string DepartmentName { get; set; }

        
    }
}
