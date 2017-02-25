namespace Angular2CoreBase.Data.Models
{
	using Abstract;

	public class ApplicationUser : PersonBase
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public string DisplayName { get; set; }

		public bool Active { get; set; }
	}
}