using System.ComponentModel.DataAnnotations;

namespace AspnetIdentityRoleBasedTutorial.Models.Donate
{
    public class Donation
    {



        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the item name.")]
        [StringLength(100, ErrorMessage = "Item name cannot exceed 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please enter the value of the item (years of use, etc.).")]
        public int ItemValue { get; set; }

        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email address cannot exceed 100 characters.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter your address.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your phone number.")]
        //[RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        

        public DateTime DonationDate { get; set; } // Optional: You can track when the donation was made
    }

}