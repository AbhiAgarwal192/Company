using Ivanti.Binders;
using Ivanti.Constants;
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
        private readonly ITriangleService _triangleService;
        private readonly ILogger<TriangleController> _logger;
        public TriangleController(ITriangleService triangleService, ILogger<TriangleController> logger)
        {
            _triangleService = triangleService;
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
        /// <param name="triangleName"></param>
        /// <returns></returns>
        [HttpGet("{triangleName}/coordinates")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<int[]>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult GetTriangleCoordinates(string triangleName)
        {
            if (string.IsNullOrEmpty(triangleName))
            {
                _logger.LogInformation($" TriangleController :: GetTriangleCoordinates :: Triangle Name cannot be null or empty. Input : {triangleName}");
                return BadRequest(Messages.TriangleNameNullOrEmpty);
            }
            var coordinates = _triangleService.GetCoordinates(triangleName.Trim());
            if (coordinates.Count == 3)
            {
                return new OkObjectResult(coordinates);
            }

            return BadRequest(Messages.TriangleNameIsNotValid);

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
                return BadRequest(Messages.ProvideValidSetOfCoordinates);
            }

            var triangleResponse = _triangleService.GetTriangle(coordinates);
            if (!triangleResponse.IsValid)
            {
                return BadRequest(triangleResponse.Value);
            }

            return Ok(triangleResponse.Value);
        }
    }
}
