using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PassionProject.Models;

namespace PassionProject.Controllers
{
    public class PetDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        [Route("api/PetData/ListPets")]
        public List<PetDto> ListPets()
        {
            List<Pet> Pets = db.Pets.ToList();
            List<PetDto> PetDtos = new List<PetDto>();
            Pets.ForEach(b => PetDtos.Add(new PetDto()
            {
                PetId = b.PetId,
                PetName = b.PetName,
                PetAge = b.PetAge,
                PetSpecies = b.PetSpecies,
                PetBreed = b.PetBreed,
                PetAdoptionStatus = b.PetAdoptionStatus,
                PetDescription = b.PetDescription
            }));
            return PetDtos;
        }

        [HttpPost]
        [Route("api/PetData/CreatePet")]
        public IHttpActionResult CreatePet(Pet pet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pets.Add(pet);
            db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("api/PetData/DeletePet/{petId}")]
        public IHttpActionResult DeletePet(int petId)
        {
            Pet pet = db.Pets.Find(petId);
            if (pet == null)
            {
                return NotFound();
            }

            db.Pets.Remove(pet);
            db.SaveChanges();

            return Ok();
        }
    }
}
