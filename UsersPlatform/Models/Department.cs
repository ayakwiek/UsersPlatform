using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UsersPlatform.Areas.Identity.Data;

namespace UsersPlatform.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string DepartmentName { get; set; }

        public ICollection<ApplicationUser> Student { get; set; }
    }

}
