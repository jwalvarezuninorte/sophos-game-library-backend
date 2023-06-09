namespace SophosGameLibraryAPI.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Address { get; set; } = null!;
        public DateTime? Birthday { get; set; }
        public string Role { get; set; } = null!;

        public List<GameDto>? Games { get; set; } = null!;
    }
}