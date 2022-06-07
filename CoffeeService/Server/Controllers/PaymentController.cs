using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeService.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMonoService _monoPaymentService;

        public PaymentController(IMonoService monoPaymentService)
        {
            _monoPaymentService = monoPaymentService;
        }

        [HttpPost("monocheckout"), Authorize]
        public async Task<ActionResult<string>> CreateCheckoutMono()
        {
            var session = await _monoPaymentService.CreateChecoutSession();
            var responseContent = await session.Content.ReadAsStringAsync();

            if (session != null && session.IsSuccessStatusCode)
            {
                CreateBillResponse response = JsonConvert.DeserializeObject<CreateBillResponse>(responseContent);

                if (response != null)
                    return Ok(response.pageUrl);
            }
            else if (((int)session.StatusCode) == 400) // PASS 
            {
                var response = JsonConvert.DeserializeObject<CreateBillResponseBad>(responseContent);
            }

            return BadRequest();
        }
    }
}
