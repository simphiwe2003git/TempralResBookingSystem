using System.ComponentModel.DataAnnotations;

namespace AspnetIdentityRoleBasedTutorial.Models.BookRoomModel
{
    public class RoomBooking
    {


        [Key]
        public int Id { get; set; }
        [Required]

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]

        [Display(Name = "LastName")]

        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Province")]
        public string Province { get; set; }
        [Required]

        public string City { get; set; }

        [Display(Name = "RoomType")]
        public string RoomType { get; set; }
        [Required]
        [Display(Name = "BlockName")]
        public string BlockName { get; set; }
        [Required]
        [Display(Name = "Check In Date")]

        public DateTime CheckInDate { get; set; }
        [Required]
        [Display(Name = "Check out Date")]
        public DateTime CheckOutDate { get; set; }
        [Display(Name = "Booking Status")]
        public BookingStatus BookingStatus { get; set; }

    }
}
