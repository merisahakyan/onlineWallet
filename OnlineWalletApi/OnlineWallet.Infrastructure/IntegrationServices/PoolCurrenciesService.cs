using Newtonsoft.Json;
using Onlinewallet.Core.Models.SeedModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace OnlineWallet.Infrastructure.IntegrationServices
{
    public class PoolCurrenciesService : IPoolCurrenciesService
    {
        readonly HttpClient client;
        public PoolCurrenciesService()
        {
            client = new HttpClient();
        }

        public async Task<List<CurrencySeedModel>> PoolCurrenciesAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var result = new List<CurrencySeedModel>();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(responseBody);
                XmlNodeList parentNode = xmlDoc.GetElementsByTagName("Cube");
                var innerCube = parentNode[0].ChildNodes[0];

                foreach (XmlNode childrenNode in innerCube.ChildNodes)
                {
                    var x = childrenNode.Attributes["currency"].Value;
                    result.Add(new CurrencySeedModel
                    {
                        Currency = childrenNode.Attributes["currency"].Value,
                        Rate = Convert.ToDecimal(childrenNode.Attributes["rate"].Value),
                    });
                }

                return result;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }
    }
}
