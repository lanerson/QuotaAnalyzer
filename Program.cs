using System;
using System.Threading.Tasks;
class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Uso: programa.exe <TICKER> <VALOR_MIN> <VALOR_MAX>");
            return;
        }
        string ticker = args[0];
        if (!double.TryParse(args[1], out double minValue))
        {
            Console.WriteLine("Erro: o segundo argumento deve ser um número (VALOR_MIN).");
            return;
        }
        if (!double.TryParse(args[2], out double maxValue))
        {
            Console.WriteLine("Erro: o terceiro argumento deve ser um número (VALOR_MAX).");
            return;
        }
        if (minValue > maxValue)
        {
            minValue += maxValue;
            maxValue = minValue - maxValue;
            minValue -= maxValue;
        }
        QuotaAnalyzer analyzer = new QuotaAnalyzer();
        Email emailSender = new Email();
        int situation = 0;        
        string body;
        try
        {
            while (true)
            {
                double? quote = await analyzer.GetCotacaoAsync(ticker);
                if (quote.HasValue)
                {

                    if(quote > maxValue && situation != 1)
                    {
                        body = $"Cota {ticker} acima do range estipulado, COMPRE!";                        
                        situation = 1;
                    }else if(quote < minValue && situation != -1)
                    {
                        body = $"Cota {ticker} abaixo do range estipulado, VENDA!";                        
                        situation = -1;
                    }else if(minValue < quote && quote < maxValue && situation != 0)
                    {
                        body = $"Cota {ticker} voltou para dentro do range estipulado, Desconsidere o email anterior!";                        
                        situation = 0;
                    }
                    else
                    {
                        body = $"Mesmo status da última requisição";
                        await Task.Delay(TimeSpan.FromMinutes(1));
                        continue;
                    }
                    emailSender.Send($"Cota {ticker}", body);
                    Console.WriteLine(body);
                }
                else
                    Console.WriteLine("Não foi possível obter a cotação.");

                await Task.Delay(TimeSpan.FromMinutes(1));
            }            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro na análise: {ex.Message}");
        }
    }
}