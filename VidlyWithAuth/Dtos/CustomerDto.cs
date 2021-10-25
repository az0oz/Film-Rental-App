using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VidlyWithAuth.Models;

namespace VidlyWithAuth.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        //[Min18YearsifAMember]
        public DateTime? Birthdate { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }

        public MemebershipTypeDto MembershipType { get; set; }
    }
}