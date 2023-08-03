using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using tech_test_payment.Borders.Dtos.Request;
using tech_test_payment.Borders.Dtos.Response;
using tech_test_payment.Borders.UseCases;
using tech_test_payment.Helpers.Enums;

namespace tech_test_payment.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ICreateSalesUseCase _createSalesUseCase;
        private readonly IGetSalesUseCase _getSalesUseCase;
        private readonly IUpdateStatusUseCase _updateStatusUseCase;

        public SalesController(ICreateSalesUseCase createSalesUseCase, IGetSalesUseCase getSalesUseCase, IUpdateStatusUseCase updateStatusUseCase)
        {
            _createSalesUseCase = createSalesUseCase;
            _getSalesUseCase = getSalesUseCase;
            _updateStatusUseCase = updateStatusUseCase;
        }

        /// <summary>
        /// Get Sales for Id
        /// </summary>
        /// <param name="saleId"></param>
        [HttpGet]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{saleId}")]
        public async Task<IActionResult> GetSales([FromRoute] int saleId)
        {
            try
            {
                var response = await _getSalesUseCase.Execute(saleId);

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Create new Sales
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [ProducesResponseType(typeof(SaleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSales([FromBody] SaleRequest request)
        {
            try
            {
                var response = await _createSalesUseCase.Execute(request);

                return Created(string.Empty, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Update Sales Status
        /// </summary>
        /// <param name="saleId"></param>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{saleId}/status")]
        public async Task<IActionResult> UpdadeSales([FromRoute] int saleId, [FromQuery] StatusEnum status)
        {
            try
            {
                var result = await _updateStatusUseCase.Execute(saleId, status);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}