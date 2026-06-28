using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace datphongkhachsan.Data.DataModel
{
    public class AccountSys :IdentityUser
    {
    public string Name { get; set; }
    [NotMapped] public bool IsSuperAdmin { get; set; }

    }
}
