using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class Application
    {
        [Key]
        public int AppId { get; set; }

        public int AdoptId { get; set; }

        public int PetId { get; set; }

        public DateTime AppSubmission { get; set; }

        public bool AppStatus { get; set; }

        public string AppComments { get; set; }

        // an application has an user id
        // an user has many applications

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int? AdoptionId { get; set; }
    }

    public class AppDto
    {
        public int AppId { get; set; }
        public int AdoptId { get; set; }
        public int PetId { get; set; }
        public DateTime AppSubmission { get; set; }
        public bool AppStatus { get; set; }
        public string AppComments { get; set; }

        public string UserId { get; set; }
        public int? AdoptionId { get; set; }

        public string PetName { get; set; }
        public int PetAge { get; set; }
        public string PetSpecies { get; set; }
        public string PetBreed { get; set; }
        public bool PetAdoptionStatus { get; set; }
        public string PetDescription { get; set; }
   
        public List<AppDto> Applications { get; set; }
    }
}
