using System.Collections.Generic;
using RestSharp;

namespace NamebaseDomain.Code
{
    public class Spider
    {

        // private readonly ILogger<Spider> _logger;
        // public Spider(ILogger<Spider> logger)
        // {
        //     _logger = logger;
        // }

        public int height { get; set; }
        public List<Domain> list { get; set; }

        public Spider()
        {
            list = new List<Domain>();
        }

        public void run2()
        {
//"https://www.namebase.io/api/domains/get/rrr";

        }

        public void run()
        {
            RestClient client = new RestClient("https://www.namebase.io/api/domains/popular");

            for (int offset = 0; ; offset += 12)
            {
                RestRequest request = new RestRequest("/" + offset, Method.GET);
                var response = client.Execute<DomainsPopularResponse>(request);

                DomainsPopularResponse result = response.Data;

                if (!result.Success)
                {
                    throw new System.Exception($"!result.Success,offset:{offset}");
                }

                if (result.Domains.Count == 0)
                {
                    break;
                }

                if (offset == 0)
                {
                    height = result.Height;
                }

                result.Domains.ForEach(x =>
                {
                    if (x.Name.IndexOf("-") != -1)
                    {
                        x.Unicode = Nunycode.Punycode.ToUnicode(x.Name);
                    }
                });

                list.AddRange(result.Domains);
            }
        }
    }
}