using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspnetIdentityRoleBasedTutorial.Models.BookRoomModel;
using AspnetIdentityRoleBasedTutorial.Models.Donate;
using AspnetIdentityRoleBasedTutorial.Models.ManageRooms;

namespace AspnetIdentityRoleBasedTutorial.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AspnetIdentityRoleBasedTutorial.Models.BookRoomModel.RoomBooking> RoomBooking { get; set; } = default!;
        public DbSet<AspnetIdentityRoleBasedTutorial.Models.Donate.Donation> Donation { get; set; } = default!;
        public DbSet<AspnetIdentityRoleBasedTutorial.Models.ManageRooms.AddRooms> AddRooms { get; set; } = default!;
       



    }
}