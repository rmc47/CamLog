using RigCAT.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public sealed class CWMacro
    {
        private ContactStore m_ContactStore;

        public ContactStore ContactStore
        {
            get { return m_ContactStore; }
            set { m_ContactStore = value; LoadMacros(); }
        }

        public WinKey WinKey { get; set; }

        private List<string> m_Macros = new List<string>(12);

        public CWMacro(ContactStore store)
        {
            ContactStore = store;
            LoadMacros();
        }

        public string GetMacro(int number)
        {
            if (number < m_Macros.Count)
                return m_Macros[number];
            else
                return null;
        }

        public void SetMacro(int number, string text)
        {
            m_Macros[number] = text;
        }

        public void SendMacro(int number, Dictionary<string, string> values)
        {
            string s = GetMacro(number);
            if (s == null)
                return;
            
            // Untokenize the macro
            foreach (var kvp in values)
            {
                s = s.Replace("{{" + kvp.Key + "}}", kvp.Value);
            }

            WinKey.SendString(s);
        }

        private void LoadMacros()
        {
            m_Macros = new List<string>
            {
                "CQ GS3PYE/P GS3PYE/P K",
                "{{HISCALL}} {{EXCHTX}}",
                "TU {{MYCALL}}",
                "{{MYCALL}}",
                "{{HISCALL}}",
                "?"
            };
        }
    }
}
