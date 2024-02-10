using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Adoption
    {
        [Key]
        public int AdoptionId { get; set; }

        [ForeignKey("Pet")]
        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }

        [ForeignKey("Application")]
        public int AppId { get; set; }
        public virtual Application Application { get; set; }
    }
}