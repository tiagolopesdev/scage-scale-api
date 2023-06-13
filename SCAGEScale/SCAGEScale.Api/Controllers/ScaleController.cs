using Microsoft.AspNetCore.Mvc;
using SCAGEScale.Application.DTO;
using SCAGEScale.Application.ServiceSide;
using SCAGEScale.Application.Utils;
using System.Net;

namespace SCAGEScale.Api.Controllers
{
    public class ScaleController : ControllerBase
    {
        private readonly IScaleService _scaleService;
        public ScaleController(IScaleService scaleService) 
        {
            _scaleService = scaleService;
        }

        [HttpPost("generationScale")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RequestResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RequestResponse>> PreviewScale([FromBody] PreviewDto previewDto)
        {
            try
            {
                var response = await _scaleService.PreviewScale(previewDto);

                return Ok(RequestResponse.New("Pré-visualização da escala gerada", response));

            }catch (Exception ex)
            {
                return BadRequest(RequestResponse.Error(ex.Message));
            }
        }
    }
}
