using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Engine;
using System.Diagnostics;

namespace QslEngine
{
    public class PdfEngine
    {
        /// <summary>
        /// For some reason, the horizontal margins are actually bigger than specified. This value corrects for that.
        /// </summary>
        private const double c_HorizontalMarginFudge = -26;

        private static readonly Font s_HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.ITALIC);
        private static readonly Font s_TableFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.NORMAL);
        private static readonly Font s_MyCallFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);
        private static readonly Font s_TitleTextFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.NORMAL);
        private static readonly Font s_FooterTextFont = new Font(Font.FontFamily.TIMES_ROMAN, (float)7.5, Font.NORMAL);

        private static readonly BaseFont s_AddressBaseFont = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, true);
        private static readonly Font s_AddressFont = new Font(s_AddressBaseFont, 9, Font.NORMAL);

        private PdfPTable m_MainTable;
        private int m_LabelsUsed = 0;
        private readonly string m_OurCallsign;
        private readonly IPageLayout m_Layout;

        public bool PrintFooter { get; set; }
        public int QsoPerLabel
        {
            get
            {
                // If we're printing the footer, allow an extra line for that
                return PrintFooter ? m_Layout.QsoPerLabel - 1 : m_Layout.QsoPerLabel;
            }
        }

        public PdfEngine(IPageLayout layout, string ourCallsign, int firstPageOffset)
        {
            m_OurCallsign = ourCallsign;
            m_Layout = layout;
            m_MainTable = new PdfPTable(m_Layout.Columns);
            m_MainTable.TotalWidth = mm2p(196);
            int[] widths = new int[m_Layout.Columns];
            for (int i = 0; i < m_Layout.Columns; i++)
                widths[i] = 1;
            m_MainTable.SetWidths(widths);

            // Offset the first page by an appropriate number of cells
            for (int i = 0; i < firstPageOffset; i++)
                m_MainTable.AddCell(PopulateCell(new LabelEntry()));
            m_LabelsUsed += firstPageOffset;
        }

        public int CalculateLabelCount(List<Contact> entries)
        {
            return (entries.Count + QsoPerLabel - 1) / QsoPerLabel;
        }

        /// <summary>
        /// Add a set of QSOs - all for the same callsign - to the list to be printed
        /// </summary>
        public int AddQSOs(LabelEntry label, List<Contact> entries)
        {
            // For each group of up to n QSOs, print on to one label
            int startIndex = 0;
            int labelsUsedHere = 0;
            while (startIndex < entries.Count)
            {
                List<Contact> labelContacts = entries.GetRange(startIndex, Math.Min(QsoPerLabel, entries.Count - startIndex));
                List<TableEntry> labelEntries = labelContacts.ConvertAll<TableEntry>(c => new TableEntry(c));

                label.QSOs = labelEntries.ToArray();
                m_MainTable.AddCell(PopulateCell(label));
                startIndex += QsoPerLabel;
                m_LabelsUsed++;
                labelsUsedHere++;
            }
            return labelsUsedHere;
        }

        public void AddAddressLabel(Address address)
        {
            PdfPCell cell = GetCell();
            PdfPTable table = new PdfPTable(1);
            AddCell(table, s_AddressFont, string.Format("{0} ({1})", address.Name, address.Callsign));
            Array.ForEach(address.AddressLines, line => AddCell(table, s_AddressFont, line));
            cell.AddElement(table);
            m_MainTable.AddCell(cell);
            m_LabelsUsed++;
        }

        public void PrintDocument(string filename)
        {
            if (m_LabelsUsed == 0)
            {
                throw new InvalidOperationException("No QSOs to print");
            }

            // Pad out to a full row of labels used
            while (m_LabelsUsed % m_Layout.Columns > 0)
            {
                m_MainTable.AddCell(GetCell());
                m_LabelsUsed++;
            }

            Document doc = new Document();
            
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                {
                    bool result = doc.SetPageSize(new Rectangle(0, 0, mm2p(210), mm2p(297), 0));
                    result = doc.SetMargins(mm2p(m_Layout.PageHorizontalMargin + c_HorizontalMarginFudge), mm2p(m_Layout.PageHorizontalMargin + c_HorizontalMarginFudge), mm2p(m_Layout.PageVerticalMargin), mm2p(m_Layout.PageVerticalMargin + c_HorizontalMarginFudge));
                    doc.Open();
                    doc.NewPage();
                    doc.Add(m_MainTable);
                    doc.Close();
                }
            }
            Process.Start(filename);
        }

        private PdfPCell GetCell()
        {
            PdfPCell cell = new PdfPCell();
            cell.FixedHeight = mm2p(m_Layout.LabelHeight);
            cell.Border = 0;
            //cell.BackgroundColor = BaseColor.RED;
            cell.PaddingLeft = mm2p(m_Layout.LabelPaddingHorizontal);
            cell.PaddingRight = mm2p(m_Layout.LabelPaddingHorizontal);
            return cell;
        }

        private PdfPCell PopulateCell(LabelEntry label)
        {
            PdfPCell cell = GetCell();

            PdfPTable qsoTable = new PdfPTable(5);
            qsoTable.SetWidths(m_Layout.ColumnRelativeWidths);
            qsoTable.WidthPercentage = 100f;

            if (label.QSOs != null && label.QSOs.Length > 0)
            {
                AddCell(qsoTable, s_HeaderFont, "Date");
                AddCell(qsoTable, s_HeaderFont, "UTC");
                AddCell(qsoTable, s_HeaderFont, "MHz");
                AddCell(qsoTable, s_HeaderFont, "RST");
                AddCell(qsoTable, s_HeaderFont, "Mode");

                Phrase titlePhrase = new Phrase();
                titlePhrase.Leading = 14.0f;
                titlePhrase.Add(new Chunk(label.MyCall, s_MyCallFont));
                titlePhrase.Add(new Chunk(" confirms the following QSO(s) with ", s_TitleTextFont));
                titlePhrase.Add(new Chunk(label.Callsign, s_MyCallFont));
                titlePhrase.Add(new Chunk(":", s_TitleTextFont));
                cell.AddElement(titlePhrase);
                foreach (TableEntry qso in label.QSOs)
                {
                    AddCell(qsoTable, s_TableFont, qso.UtcTime.ToString("yyyy-MM-dd"));
                    AddCell(qsoTable, s_TableFont, qso.UtcTime.ToString("HHmm"));
                    AddCell(qsoTable, s_TableFont, qso.Band);
                    AddCell(qsoTable, s_TableFont, qso.Rst);
                    AddCell(qsoTable, s_TableFont, qso.Mode);
                }
            }
            cell.AddElement(qsoTable);

            if (PrintFooter)
            {
                Phrase footerPhrase = new Phrase();
                footerPhrase.Add(new Chunk(string.Format("IOTA: {0} ({1}); WAB: {2}; Locator: {3}", label.Location.IotaName, label.Location.IotaRef, label.Location.Wab, label.Location.Locator), s_FooterTextFont));
                cell.AddElement(footerPhrase);
            }

            return cell;
        }

        private static void AddCell(PdfPTable table, Font font, string phrase)
        {
            PdfPCell cell = new PdfPCell(new Phrase(phrase, font));
            cell.Border = 0;
            
            table.AddCell(cell);
        }

        public static float mm2p(double mm)
        {
            return (float)(mm * 2.83464567);
        }
    }
}
