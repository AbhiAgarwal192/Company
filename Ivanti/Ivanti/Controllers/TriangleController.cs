using Ivanti.Binders;
using Ivanti.Manager.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Ivanti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriangleController : ControllerBase
    {
        private readonly ITriangleManager _triangleManager;
        private readonly ILogger<TriangleController> _logger;
        public TriangleController(ITriangleManager triangleManager, ILogger<TriangleController> logger)
        {
            _triangleManager = triangleManager;
            _logger = logger;
        }

        [HttpGet("{triangleNumber}/coordinates")]
        [Produces("application/json")]
        public IActionResult GetTriangleCoordinates(string triangleNumber )
        {
            if (string.IsNullOrEmpty(triangleNumber))
            {
                _logger.LogInformation($" TriangleController :: GetTriangleCoordinates :: Triangle Name cannot be null or empty. Input : {triangleNumber}");
                return BadRequest("Triangle Name cannot be null or empty.");
            }
            var coordinates = _triangleManager.GetCoordinates(triangleNumber.Trim());
            return new OkObjectResult(coordinates);
        }

        [HttpGet("name")]
        public IActionResult GetTriangleName([ModelBinder(BinderType = typeof(StringToListBinder))] List<int[]> coordinates)
        {
            if (coordinates.Count == 0 || coordinates.Count!=3)
            {
                _logger.LogInformation(" TriangleController :: GetTriangleName :: Please provide valid set of coordinates.");
                return BadRequest("Please provide valid set of coordinates.");
            }
            string triangleName = _triangleManager.GetTriangle(coordinates);
            return Ok(triangleName);
        }
    }
}
