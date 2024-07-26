using Microsoft.AspNetCore.Identity;

namespace WebApiDersleri.Models
{
	public class AppUsers:IdentityUser<int>
	{
		public string FullName { get; set; } = null!;
		public DateTime DateAdded { get; set; }

	}
}
