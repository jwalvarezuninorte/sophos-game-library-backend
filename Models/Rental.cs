using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SophosGameLibraryAPI.Models
{
    public partial class Rental
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("fk_rental_user")]
        public int FkRentalUser { get; set; }
        [Column("fk_rental_game")]
        public int FkRentalGame { get; set; }
        [Column("date_start", TypeName = "smalldatetime")]
        public DateTime DateStart { get; set; }
        [Column("date_end", TypeName = "smalldatetime")]
        public DateTime DateEnd { get; set; }

        [ForeignKey("FkRentalGame")]
        [InverseProperty("Rentals")]
        public virtual Game FkRentalGameNavigation { get; set; } = null!;
        [ForeignKey("FkRentalUser")]
        [InverseProperty("Rentals")]
        public virtual User FkRentalUserNavigation { get; set; } = null!;
    }
}
