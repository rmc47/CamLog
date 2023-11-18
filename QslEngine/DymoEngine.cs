using DymoSDK.Implementations;
using DymoSDK.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace QslEngine
{
    public class DymoEngine
    {
        DymoLabel label;
        DymoSDK.Interfaces.IPrinter printer;
        string labelLayout;

        public DymoEngine()
        {
            DymoSDK.App.Init();
            label = (DymoLabel)(DymoLabel.Instance);

            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resources = asm.GetManifestResourceNames();
            using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream("QslEngine.99012-Address-Connect.dymo"))
            {
                using (var sr = new StreamReader(s))
                {
                     labelLayout = sr.ReadToEnd();
                }
            }

			printer = DymoPrinter.Instance.GetPrinters().Result.FirstOrDefault();
        }

        public void PrintAddress(Address a)
        {
            List<TextSpan> spans = new List<TextSpan>();
            spans.Add(new TextSpan { Text = string.Format("{0} ({1})", a.Name, a.Callsign) });
            foreach (string s in a.AddressLines)
                spans.Add(new TextSpan() { Text = s });

            string compatiblePrinterType;
            bool vertical;
            // Always revert to the original layout
            label.LoadLabelFromXML(labelLayout);
            label.UpdateFirstTextObject(new FormattedText { TextSpans = spans.ToArray() }, out compatiblePrinterType, out vertical);
            DymoPrinter.Instance.PrintLabel(label, printer.Name);
        }
    }
}
