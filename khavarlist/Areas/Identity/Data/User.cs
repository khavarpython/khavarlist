using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace khavarlist.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]
    public override required string UserName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")]  
    public required string FirstName { get; set; }

    [PersonalData]
    [Column(TypeName = "nvarchar(100)")] 
    public required string LastName { get; set; }
}