using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoffeeService.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("checkout"), Authorize]
        public async Task<ActionResult<string>> CreateCheckoutSession()
        {
            var session = await _paymentService.CreateChecoutSession();
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
