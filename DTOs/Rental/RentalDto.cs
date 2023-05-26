namespace SophosGameLibraryAPI.DTOs
{
    public class RentalDto
    {
        public int Id { get; set; }
        public GameDto? Game { get; set; }
        public UserDto? User { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

    }
}