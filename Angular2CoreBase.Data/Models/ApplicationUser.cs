using Angular2CoreBase.Data.Abstract;

namespace Angular2CoreBase.Data.Models
{
	public class ApplicationUser : PersonBase
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string DisplayName { get; set; }

		public bool Active { get; set; }
	}
}
