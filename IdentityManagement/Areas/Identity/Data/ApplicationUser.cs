using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityManagement.Areas.Identity.Data
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		[PersonalData]
		public DateTime DOB { get; set; }
	}
}
