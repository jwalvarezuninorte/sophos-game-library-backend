namespace SophosGameLibraryAPI.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = null!;
        public double RentalPrice { get; set; }
        public double? SellingPrice { get; set; }
        public string? DirectorName { get; set; }
        public string? ProductorName { get; set; }
        public DateTime LaunchDate { get; set; }
        public string? LeadCharacterName { get; set; }
        public string GamePlatform { get; set; } = null!;
    }
}