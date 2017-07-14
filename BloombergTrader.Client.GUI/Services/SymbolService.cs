using BloombergTrader.Client.Model;
using BloombergTrader.Client.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BloombergTrader.Client.GUI.Services
{
    public class SymbolService : ISymbolService
    { 

        public async Task<IEnumerable<BloombergSymbol>> SearchSymbol(string keyword)
        {
            string searchUrl = $"http://www.bloomberg.com/markets/symbolsearch?query={keyword}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(searchUrl);
                var content = await response.Content.ReadAsStringAsync();
                return ParseHtmlTable(content);
            }
        }

        private List<BloombergSymbol> ParseHtmlTable(string pageContent)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(pageContent);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='dual_border_data_table alt_rows_stat_table tabular_table']")
                                        .Descendants("tr")
                                        .Skip(1)
                                        .Where(tr => tr.Elements("td").Count() > 1)
                                        .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                                        .ToList();

           return table.Select(x => new BloombergSymbol
                                        {
                                            Symbol =   x[0],
                                            Name =     x[1],
                                            Country =  x[2],
                                            Type =     x[3],
                                            Industry = x[4]
                                        })
                       .ToList();
        }
    }
}
