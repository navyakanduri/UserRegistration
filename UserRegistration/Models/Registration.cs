using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UserRegistration.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        //[EmailAddress(ErrorMessage ="invalid email")]
        [Required(ErrorMessage ="Please enter Email")]
		[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}",ErrorMessage ="Please Enter Valid Email")]
		[Remote(action:"UserNameIsExist",controller:"Registration")]
        public string? UserName { get; set; }
		[Required(ErrorMessage = "Please enter Password")]
		public string? Password { get; set; }
		[Required(ErrorMessage = "Please select gender")]
		public string? Gender { get; set; }
		[Required(ErrorMessage = "Please enter Date of birth")]
		public DateTime Dateofbirth  { get; set; }
		[Required(ErrorMessage = "Please select security question")]
		public string? SecurityQuestion { get; set; }
		[Required(ErrorMessage = "Please enter answer")]
		public string? Answer { get; set; }

    }
}
