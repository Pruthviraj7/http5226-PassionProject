using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class Pet
    {
        // Describing pet
        [Key]
        public int PetId { get; set;}
        public string PetName { get; set; }

        public int PetAge { get; set;}
        
        public string PetSpecies {  get; set; }

        public string PetBreed {  get; set; }

        public bool PetAdoptionStatus {  get; set; }
        public string PetDescription {  get; set; }
        public virtual ICollection<Adoption> Adoptions { get; set; }
    }
    public class PetDto
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int PetAge { get; set; }
        public string PetSpecies { get; set; }
        public string PetBreed { get; set; }
        public bool PetAdoptionStatus { get; set; }
        public string PetDescription { get; set; }  

      
        public int? AdoptionId { get; set; }

  
        public List<AppDto> Applications { get; set; }
    }
}