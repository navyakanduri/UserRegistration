using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models
{
	public class Login
	{
		[Key]
		public int id { get; set; }

		//[EmailAddress(ErrorMessage = "invalid email")]
		[Required]
		public string  UserName { get; set; }
		[Required]
		public string? Password { get; set; }
	}
}
