using System;
using FiratCircuitBreaker;
using Microsoft.AspNetCore.Mvc;

namespace FiratCircuitBreaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitBreakerDesingController : ControllerBase
    {
        private readonly CircuitBreaker _circuitBreaker;

        public CircuitBreakerDesingController(CircuitBreaker circuitBreaker)
        {
            _circuitBreaker = circuitBreaker;
        }
        [HttpGet]
        [Route("Get")]
        public async Task Get()
        {
             await CircuitBreakerDesing.CircuitBreaker();
        }
        [HttpGet("performAction")]
        public IActionResult PerformAction()
        {
            try
            {
                _circuitBreaker.Execute(() =>
                {
                    // Burada senaryo i�in i�lemi sim�le edebilirsiniz.
                    // �rne�in, bir hizmet �a��rma veya i�lem yapma
                    SimulateScenario();
                });

                return Ok("Operation succeeded");
            }
            catch (CircuitBreaker.CircuitBreakerOpenException)
            {
                return BadRequest("Circuit Breaker is open");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        private void SimulateScenario()
        {
            // Bu metot, senaryonuzun gereksinimlerine g�re �zelle�tirilebilir.
            // �rne�in, bir hizmet �a��rma veya i�lem yapma i�lemini burada sim�le edebilirsiniz.
            // Senaryonuza uygun kodu burada ekleyin.
            Random random = new Random();
            int randomNumber = random.Next(1, 4); // Rastgele bir say� �retin (1 ile 3 aras�)

            // Senaryo: 1/3 olas�l�kla ba�ar�l�, 2/3 olas�l�kla ba�ar�s�z.
            if (randomNumber == 1)
            {
                Console.WriteLine("Scenario: Successful operation");
            }
            else
            {
                Console.WriteLine("Scenario: Failed operation");
                throw new Exception("Simulated failure");
            }
        }
    }
}