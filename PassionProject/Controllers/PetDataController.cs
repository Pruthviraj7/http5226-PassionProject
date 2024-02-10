using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PassionProject.Models;
/*<summary>
 
 * PetDataController handles API requests related to pet data, including listing pets,
 * creating new pet listings, and deleting pet entries.
 *
 * Endpoints:
 * 1. ListPets: GET /api/PetData/ListPets
 *    - Retrieves a list of PetDto containing basic information about all pets.
 *
 * 2. CreatePet: POST /api/PetData/CreatePet
 *    - Creates a new pet listing based on the provided Pet object in the request body.
 *
 * 3. DeletePet: DELETE /api/PetData/DeletePet/{petId}
 *    - Deletes the pet entry with the specified PetId.
 *
 * Examples:
 * 1. To list all pets, make a GET request to:
 *    /api/PetData/ListPets
 *
 * 2. To create a new pet listing, make a POST request to:
 *    /api/PetData/CreatePet with the Pet details in the request body.
 *
 * 3. To delete a pet entry with PetId = 1, make a DELETE request to:
 *    /api/PetData/DeletePet/1
 *
 * Responses:
 * - For successful operations, it returns an Ok status.
 * - If the specified pet is not found (for deletion), it returns a 404 Not Found status.
 * - If there are validation errors during pet creation, it returns a BadRequest status
 *   along with model validation errors.

</ summary>*/
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
