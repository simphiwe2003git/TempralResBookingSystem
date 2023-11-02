using System.ComponentModel.DataAnnotations;

namespace AspnetIdentityRoleBasedTutorial.Models.ManageRooms
{
    public class AddRooms
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string AddRoomNumber { get; set; }
        public string AddRoomType { get; set; }
        public string AddBlockName { get; set;}
        public string AddRoomSatus { get; set;}
    }
}
