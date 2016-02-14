using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.Entities
{
    public class ApplicationUser : IdentityUser 
    {
        public virtual ICollection<FileEntity> Files { get; set; }

        public ApplicationUser()
        {
            Files = new List<FileEntity>();
        }
    }
}
