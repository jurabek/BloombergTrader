using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BloombergTrader.DataTransferObjects;

namespace BloombergTrader.Server.Services
{
    public class SymbolServices
    {
        public async Task<IEnumerable<SymbolRequest>> SearchSymbol(string keyword)
        {
            string searchUrl = $"http://www.bloomberg.com/markets/symbolsearch?query={keyword}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(searchUrl);
                var content = await response.Content.ReadAsStringAsync();
                return ParseHtmlTable(content);
            }
        }

        private List<SymbolRequest> ParseHtmlTable(string pageContent)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageContent);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='dual_border_data_table alt_rows_stat_table tabular_table']")
                                        .Descendants("tr")
                                        .Skip(1)
                                        .Where(tr => tr.Elements("td").Count() > 1)
                                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                                        .ToList();

            return table.Select(x => new SymbolRequest()
            {
                Symbol = x[0],
                Name = x[1],
                Country = x[2],
                Type = x[3],
                Industry = x[4]
            }).ToList();
        }
    }
}