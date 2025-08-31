using HtmlAgilityPack;
using System.Net.Http.Headers;
using LibHandler.Models;

namespace LibHandler.Util
{
    internal class BaseApiHandler
    {
        HttpClient httpClient = new HttpClient();
        HttpClient httpDownload = new HttpClient();
        
        public BaseApiHandler()
        {
            ReloadUrls();
        }

        public async Task<List<string>> GetIDs(string request)
        {
            ReloadUrls();

            HttpResponseMessage response = await httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                var html = new HtmlDocument();
                html.LoadHtml(res);

                return ExtractEditionIds(html);
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                return new List<string>();
            }
        }
        
        public static List<string> ExtractEditionIds(HtmlDocument doc)
        {
            var ids = new HashSet<string>();

            // ищем все ссылки <a href="edition.php?id=...">
            var nodes = doc.DocumentNode.SelectNodes("//a[contains(@href,'edition.php?id=')]");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var href = node.GetAttributeValue("href", "");
                    var queryIndex = href.IndexOf("id=");
                    if (queryIndex >= 0)
                    {
                        var id = href.Substring(queryIndex + 3);
                        ids.Add(id);
                    }
                }
            }

            return ids.ToList();
        }

        public async Task<string> GetJSONData(List<string> ids)
        {
            ReloadUrls();

            string idString = string.Join(",", ids);

            HttpResponseMessage response = await httpClient.GetAsync($"json.php?object=e&addkeys=*&ids={idString}");

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine(response.ReasonPhrase);
                return string.Empty;
            }
        }
        public async Task<Dictionary<string, string>> GetDownloadLink(string id)
        {
            Mirror m = MirrorHandler.MainDownloadMirror;

            ReloadUrls();

            var response = await httpDownload.GetAsync($"/edition.php?id={id}");
            
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(res);
                var links = new Dictionary<string, string>();

                var nodes = html.DocumentNode.SelectNodes("//table[@id='tablelibgen']//a[@title]");

                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        var title = node.GetAttributeValue("title", "").Trim();
                        var href = node.GetAttributeValue("href", "").Trim();

                        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(href))
                        {
                            links[title] = href;
                        }
                    }
                }

                return links;
            }
            else
            {
                return new Dictionary<string, string>();
            }   
        }

        public void ReloadUrls()
        {

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(MirrorHandler.MainSearchMirror.FullUrl);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

            httpDownload = new HttpClient();
            httpDownload.BaseAddress = new Uri(MirrorHandler.MainDownloadMirror.FullUrl);
            httpDownload.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        }

    }

}
