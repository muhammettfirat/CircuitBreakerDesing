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
                    // Burada senaryo için iþlemi simüle edebilirsiniz.
                    // Örneðin, bir hizmet çaðýrma veya iþlem yapma
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
            // Bu metot, senaryonuzun gereksinimlerine göre özelleþtirilebilir.
            // Örneðin, bir hizmet çaðýrma veya iþlem yapma iþlemini burada simüle edebilirsiniz.
            // Senaryonuza uygun kodu burada ekleyin.
            Random random = new Random();
            int randomNumber = random.Next(1, 4); // Rastgele bir sayý üretin (1 ile 3 arasý)

            // Senaryo: 1/3 olasýlýkla baþarýlý, 2/3 olasýlýkla baþarýsýz.
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