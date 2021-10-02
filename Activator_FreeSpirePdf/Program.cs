using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Activator_FreeSpirePdf
{
    class Program
    {
        static void Main()
        {
            string activatorPath = @"";
            string purePdfPath = @"";
            string activatedPdfPath = @"";
            string rebuilActivatorPath = @"";

            using (PdfDocument purePdf = new PdfDocument(purePdfPath))
            using (FileStream fs = new FileStream(activatorPath, FileMode.Open))
            {
                PdfFont font = new PdfFont(PdfFontFamily.TimesRoman, 1f);
                long containerSize = fs.Length / purePdf.Pages.Count + fs.Length % purePdf.Pages.Count;
                byte[] container = new byte[containerSize];

                int i = 0;
                foreach (PdfPageBase page in purePdf.Pages)
                {
                    int contentSize = fs.Read(container, 0, container.Length);
                    byte[] content = container.Take(contentSize).ToArray();
                    string item = Convert.ToBase64String(content);
                    string pdfText = $"<hs-{i}>{item}<he-{i}>";
                    page.Canvas.DrawString(pdfText, font, PdfBrushes.Transparent, 0, 0);
                    i++;

                    if (fs.Position == fs.Length) break;
                }

                purePdf.SaveToFile(activatedPdfPath); Console.WriteLine(i);
            }

            using (FileStream fs = new FileStream(rebuilActivatorPath, FileMode.Create))
            using (PdfDocument pd = new PdfDocument(activatedPdfPath))
            {
                foreach (PdfPageBase page in pd.Pages)
                {
                    var findCollection = page.FindText("<hs-[0-9]+>.*<he-[0-9]+>", Spire.Pdf.General.Find.TextFindParameter.Regex);
                    if (findCollection.Finds.Length == 0) continue;
                    var base64 = findCollection.Finds[0].MatchText;
                    base64 = Regex.Replace(base64, "<h[se]-[0-9]+>", "");
                    byte[] content = Convert.FromBase64String(base64);
                    fs.Write(content, 0, content.Length);
                }
            }
        }
    }
}
