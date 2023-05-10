using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Mindr.Domain.OpenId
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser 
    {
        public List<UserRelation> Relations { get; set; }
    }
}
