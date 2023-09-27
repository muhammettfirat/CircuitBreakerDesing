using System;
using System.Diagnostics;

namespace FiratCircuitBreaker
{
    public class CircuitBreaker
    {
        private int _failureThreshold;
        private int _failureCount = 0;
        private int _successThreshold;
        private int _successCount = 0;
        private CircuitState _state = CircuitState.Closed;
        private DateTime _openStateExpiryTime = DateTime.Now.AddMinutes(1);

        // CircuitBreaker sınıfını başlatırken başarısızlık ve başarı eşiklerini belirleyin.
        public CircuitBreaker(int failureThreshold, int successThreshold)
        {
            _failureThreshold = failureThreshold;
            _successThreshold = successThreshold;
        }

        // Devre kapalı mı (başarılı) olduğunu kontrol eden bir özellik.
        public bool IsClosed => _state == CircuitState.Closed;

        public void Execute(Action action)
        {
            if (_state == CircuitState.Open)
            {
                // Devre açıksa, işlemi gerçekleştirmeyi engelleyin ve bir istisna fırlatın.
                throw new CircuitBreakerOpenException();
            }

            try
            {
                // İşlemi çalıştır.
                action();
                _successCount++;

                if (_successCount >= _successThreshold)
                {
                    // Başarılı işlem sayısı başarı eşiğini aştıysa, devreyi kapat.
                    _state = CircuitState.Closed;
                    _failureCount = 0;
                    _successCount = 0;
                }
            }
            catch (Exception ex)
            {
                // İşlem sırasında bir hata oluştuğunda işlenir.
                _failureCount++;

                if (_failureCount >= _failureThreshold)
                {
                    // Başarısız işlem sayısı başarısızlık eşiğini aştıysa, devreyi aç.
                    _state = CircuitState.Open;
                    // Devre açıldığında, açık durumun ne kadar süreyle devam edeceğini ayarlayın.
                    _openStateExpiryTime = DateTime.Now.AddMinutes(1); // Örnek olarak 1 dakika.
                }

                // Hata istisnasını yeniden fırlat.
                throw ex;
            }
        }

        // Devre durumunu temsil eden bir numaralandırma türü.
        public enum CircuitState
        {
            Closed, // Devre kapalı ve işlemler izin veriliyor.
            Open    // Devre açık ve işlemler engelleniyor.
        }

        // Devre açık olduğunda fırlatılacak özel bir istisna türü.
        public class CircuitBreakerOpenException : Exception
        {
            public CircuitBreakerOpenException() : base("Circuit Breaker is open.")
            {
            }
        }
    }
}