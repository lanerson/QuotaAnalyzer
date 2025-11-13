using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetEnv;
public class QuotaAnalyzer
{
    HttpClient _httpClient;
    string baseUrl;

    public QuotaAnalyzer()
    {
        Env.Load();
        _httpClient = new HttpClient();
        baseUrl = Environment.GetEnvironmentVariable("API_URL") 
                ?? throw new Exception("Variável API_URL não encontrada no .env");               
    }

    public async Task<double?> GetCotacaoAsync(string ticker)
    {
        string url = $"{baseUrl}{ticker.ToUpper()}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                var root = doc.RootElement;
                var priceElement = root.GetProperty("results")[0].GetProperty("regularMarketPrice");
                double price = priceElement.GetDouble();

                return price;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar cotação de {ticker}: {ex.Message}");
            return null;
        }
    }
    
}
