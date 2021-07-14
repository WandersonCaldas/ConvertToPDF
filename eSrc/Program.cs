using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace eSrc
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = 12436133;

            string strSiteUrl = "http://datavisa-dev:8051/Datavisa/AutoInfracao/ImpressaoNovo.asp?ID=" + id + "&ignoreprint=true";

            var request = (HttpWebRequest)WebRequest.Create(strSiteUrl);
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            var streamReader = new StreamReader(stream, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            //Console.WriteLine(streamReader.ReadToEnd());

            string[] lines = { streamReader.ReadToEnd() };

            var caminho = "";

            File.WriteAllLines(caminho, lines);

            using (FileStream htmlSource = File.Open(caminho, FileMode.Open))
            {
                var caminho_gravar = "";
                using (FileStream pdfDest = File.Open(caminho_gravar, FileMode.OpenOrCreate))
                {
                    ConverterProperties converterProperties = new ConverterProperties();
                    HtmlConverter.ConvertToPdf(htmlSource, pdfDest, converterProperties);
                }
            }

            File.Delete(caminho);
        }
    }
}
