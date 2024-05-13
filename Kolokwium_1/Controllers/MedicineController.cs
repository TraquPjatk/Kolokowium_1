using Microsoft.AspNetCore.Mvc;
using Services;

namespace Zadanie5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMedicamentService _medicamentService;


        public MedicineController(IMedicamentService medicamentService)
        {
            _medicamentService = medicamentService;
        }
        
        [HttpGet("{id}")]
        public ActionResult GetMedicine(int id)
        {
            try
            {
                _medicamentService.GetMedicine(id);
                return StatusCode(StatusCodes.Status200OK);
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