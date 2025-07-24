using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public interface IExchangeService
    {
        public Task<List<ExchangeRate>>? GetExchangeRateAsync(DateTime startDate, DateTime endDate);
        public Task<List<ExchangeRate>>? GetExchangeRatesAsync(DateTime startDate, DateTime endDate);
    }

    public class ExchangeService : IExchangeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://www.nbrm.mk/KLServiceNOV/";

        public ExchangeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExchangeRate>>? GetExchangeRateAsync(DateTime startDate, DateTime endDate)
        {
            var url = $"{_baseUrl}GetExchangeRate?StartDate={startDate:dd.MM.yyyy}&EndDate={endDate:dd.MM.yyyy}&format=json";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception("The datas could not be retrieved from the NBRM service.");

            var content = await response.Content.ReadAsStringAsync();
            var rates = JsonSerializer.Deserialize<List<ExchangeRate>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return rates;
        }
        
        public async Task<List<ExchangeRate>>? GetExchangeRatesAsync(DateTime startDate, DateTime endDate)
        {
            var url = $"{_baseUrl}GetExchangeRates?StartDate={startDate:dd.MM.yyyy}&EndDate={endDate:dd.MM.yyyy}&format=json";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) throw new Exception("The datas could not be retrieved from the NBRM service.");
            
            var content = await response.Content.ReadAsStringAsync();
            var rates = JsonSerializer.Deserialize<List<ExchangeRate>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return rates;
        }
    }
}