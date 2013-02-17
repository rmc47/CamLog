using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    public sealed class ClubLogQslAdifHandler
    {
        private readonly ContactStore m_ContactStore;
        private readonly PdfEngine m_PdfEngine;

        public ClubLogQslAdifHandler(ContactStore contactStore, PdfEngine engine)
        {
            m_ContactStore = contactStore;
            m_PdfEngine = engine;
        }

        public int ProcessFile(string filename)
        {

            throw new NotImplementedException();
        }

        private void ParseFile(string filename)
        {
        }
    }
}
