using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosGameLibraryAPI.Models
{
    public partial class Game
    {
        public Game()
        {
            Rentals = new HashSet<Rental>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        [Unicode(false)]
        public string Name { get; set; } = null!;
        [Column("description")]
        [StringLength(255)]
        [Unicode(false)]
        public string Description { get; set; } = null!;
        [Column("rental_price")]
        public double RentalPrice { get; set; }
        [Column("selling_price")]
        public double? SellingPrice { get; set; }
        [Column("director_name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? DirectorName { get; set; }
        [Column("productor_name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ProductorName { get; set; }
        [Column("launch_date", TypeName = "date")]
        public DateTime LaunchDate { get; set; }
        [Column("lead_character_name")]
        [StringLength(50)]
        [Unicode(false)]
        public string? LeadCharacterName { get; set; }
        [Column("game_platform")]
        [StringLength(50)]
        [Unicode(false)]
        public string GamePlatform { get; set; } = null!;

        [InverseProperty("FkRentalGameNavigation")]
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
