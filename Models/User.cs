using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosGameLibraryAPI.Models
{
    public partial class User
    {
        public User()
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
        [Column("email")]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; } = null!;
        [Column("phone")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Phone { get; set; }
        [Column("address")]
        [StringLength(50)]
        [Unicode(false)]
        public string Address { get; set; } = null!;
        [Column("birthday", TypeName = "date")]
        public DateTime? Birthday { get; set; }
        [Column("role")]
        [StringLength(50)]
        [Unicode(false)]
        public string Role { get; set; } = null!;

        [InverseProperty("FkRentalUserNavigation")]
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
