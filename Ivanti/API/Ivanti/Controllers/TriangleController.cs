using Ivanti.Binders;
using Ivanti.Manager.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

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

        /// <summary>
        /// Get Triangle Coordinates From Triangle Name
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/triangle/A1/coordinates
        ///
        /// </remarks>
        /// <param name="triangleNumber"></param>
        /// <returns></returns>
        [HttpGet("{triangleNumber}/coordinates")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<int[]>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetTriangleCoordinates(string triangleNumber)
        {
            if (string.IsNullOrEmpty(triangleNumber))
            {
                _logger.LogInformation($" TriangleController :: GetTriangleCoordinates :: Triangle Name cannot be null or empty. Input : {triangleNumber}");
                return BadRequest("Triangle Name cannot be null or empty.");
            }
            var coordinates = _triangleManager.GetCoordinates(triangleNumber.Trim());
            return new OkObjectResult(coordinates);
        }

        /// <summary>
        /// Get Triangle Name From Coordinates
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/triangle/name?coordinates=[{50,50},{60,50},{60,60}]
        ///
        /// </remarks>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        [HttpGet("name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest,Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetTriangleName([ModelBinder(BinderType = typeof(StringToListBinder))] [FromQuery] List<int[]> coordinates)
        {
            if (coordinates.Count == 0 || coordinates.Count!=3)
            {
                _logger.LogInformation(" TriangleController :: GetTriangleName :: Please provide valid set of coordinates.");
                return BadRequest("Please provide valid set of coordinates.");
            }

            var triangleResponse = _triangleManager.GetTriangle(coordinates);
            if (!triangleResponse.IsValid)
            {
                return BadRequest(triangleResponse.Message);
            }

            return Ok(triangleResponse.Message);
        }
    }
}
