using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using System.Windows.Forms;
using System.IO;
namespace htmlToPdf
{
    internal class HTMLTOPDF
    {
        public static void convertHtml(string htmlContent)
        {
            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = DinkToPdf.ColorMode.Color,
                    Orientation = DinkToPdf.Orientation.Portrait,
                    PaperSize = DinkToPdf.PaperKind.A4,
                    Out = "output.pdf"  // Output file path
                },
                Objects = {
                new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = "<h1>Hello World</h1><p>This is a PDF conversion from HTML</p>",
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]" },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Footer text" }
                }
            }
            };

            converter.Convert(doc);
            Console.WriteLine("PDF Generated Successfully!");
        }
    }
}
