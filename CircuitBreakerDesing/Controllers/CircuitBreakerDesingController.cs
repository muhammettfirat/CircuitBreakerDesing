using System;
using FiratCircuitBreaker;
using Microsoft.AspNetCore.Mvc;

namespace FiratCircuitBreaker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CircuitBreakerDesingController : ControllerBase
    {
       

        public CircuitBreakerDesingController()
        {
        
        }
        [HttpGet]
        [Route("deneme")]
        public async Task Get()
        {
             await CircuitBreakerDesing.CircuitBreaker();
        }
      
    }
}