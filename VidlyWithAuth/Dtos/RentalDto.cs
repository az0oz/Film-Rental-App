using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyWithAuth.Models;

namespace VidlyWithAuth.Dtos
{
    public class RentalDto
    {
        public int Id { get; set; }
        [Required]
        public Movie Movie { get; set; }
        [Required]
        public Customer Customer { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}