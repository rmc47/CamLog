using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QslEngine;

namespace EngineTests
{
    [TestFixture]
    public class QslLabelTests
    {
        [Test]
        public void CheckAddressLabel()
        {
            QslEngine.PdfEngine pdfe = new PdfEngine (new LayoutAvery7160(), "M0VFC", 0);
            for (int i = 0; i < 23; i++)
                pdfe.AddAddressLabel(new Address { Callsign = "M0VFC", Name = "Robert Chipperfield", AddressLines = new[] { "12", "Some street", "Some village", "Some town", "County", "Country"}});

            pdfe.PrintDocument(@"C:\temp\addresslabel.pdf");
        }
    }
}
