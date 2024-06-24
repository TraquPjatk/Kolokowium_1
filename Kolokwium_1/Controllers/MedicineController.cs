using Microsoft.AspNetCore.Mvc;
using Services;
using Kolokwium_1.Dtos;

namespace Zadanie5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicamentService _medicamentService;

        public MedicineController(IMedicamentService medicamentService)
        {
            _medicamentService = medicamentService;
        }

        [HttpGet("{id}")]
        public ActionResult<MedicamentDto> GetMedicine(int id)
        {
            try
            {
                var result = _medicamentService.GetMedicine(id);
                if (result == null)
                {
                    return NotFound("Medicament not found");
                }
                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}