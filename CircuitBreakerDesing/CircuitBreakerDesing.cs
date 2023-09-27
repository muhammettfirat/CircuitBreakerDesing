using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static FiratCircuitBreaker.CircuitBreaker;

namespace FiratCircuitBreaker
{
    public static class CircuitBreakerDesing
    {
      
     
        public static async Task CircuitBreaker()
        {
            // Circuit Breaker ayarları
            int failureThreshold = 3; // Üç başarısız denemeden sonra devre dışı bırak
            int successThreshold = 2; // İki başarılı denemeden sonra devreyi kapat

            // Circuit Breaker örneğini oluşturun
            var circuitBreaker = new CircuitBreaker(failureThreshold, successThreshold);

            try
            {
                // Circuit Breaker içindeki işlemi çağırın
                circuitBreaker.Execute(() =>
                {
                    // Burada Circuit Breaker'ı test edecek bir işlemi çağırabilirsiniz
                    // Örneğin, bir hizmet çağırma veya işlem yapma
                    Console.WriteLine("İşlem başarıyla tamamlandı.");
                });
            }
            catch (CircuitBreakerOpenException)
            {
                // Circuit Breaker devresi açıksa, işlem gerçekleştirilemez
                throw new Exception("Circuit Breaker açık durumda. İşlem gerçekleştirilemiyor.");
            }
            catch (Exception ex)
            {
                throw new Exception("İşlem Başarısız");
            }

        }
      
    }
}
