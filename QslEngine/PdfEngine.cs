using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using Engine;

namespace QslEngine
{
    public class PdfEngine
    {
        private const int c_QsoPerLabel = 4;
        private const int c_Columns = 2;
        private const double c_LabelHeight = 33.9;

        private static readonly Font s_HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.ITALIC);
        private static readonly Font s_TableFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.NORMAL);
        private static readonly Font s_MyCallFont = new Font(Font.FontFamily.TIMES_ROMAN, 10, Font.BOLD);
        private static readonly Font s_TitleTextFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.NORMAL);

        private PdfPTable m_MainTable;
        private int m_LabelsUsed = 0;

        public PdfEngine(string ourCallsign)
        {
            m_MainTable = new PdfPTable(c_Columns);
            m_MainTable.TotalWidth = mm2p(196);
            int[] widths = new int[c_Columns];
            for (int i = 0; i < c_Columns; i++)
                widths[i] = 1;
            m_MainTable.SetWidths(widths);
        }

        public void AddQSOs(List<Contact> entries)
        {
            // For each group of up to n QSOs, print on to one label
            int startIndex = 0;
            while (startIndex < entries.Count)
            {
                List<Contact> labelContacts = entries.GetRange(startIndex, Math.Min(c_QsoPerLabel, entries.Count - startIndex));
                List<TableEntry> labelEntries = labelContacts.ConvertAll<TableEntry>(c =>
                {
                    return new TableEntry
                    {
                        Band = BandHelper.ToMHzString(c.Band),
                        Callsign = c.Callsign,
                        Mode = ModeHelper.ToString(c.Mode),
                        MyCall = "GS3PYE/P",
                        Rst = c.ReportSent,
                        UtcTime = c.StartTime
                    };
                });
                m_MainTable.AddCell(PopulateCell(labelEntries.ToArray()));
                startIndex += c_QsoPerLabel;
                m_LabelsUsed++;
            }
        }

        public void PrintDocument(string filename)
        {
            // Pad out to a full row of labels used
            while (m_LabelsUsed % c_Columns > 0)
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
                    result = doc.SetMargins(mm2p(-20), mm2p(-20), mm2p(0), mm2p(0));
                    doc.Open();
                    doc.NewPage();
                    doc.Add(m_MainTable);
                    doc.Close();
                }
            }
        }

        private static PdfPCell GetCell()
        {
            PdfPCell cell = new PdfPCell();
            cell.FixedHeight = mm2p(c_LabelHeight);
            cell.Border = 1;
            return cell;
        }

        private static PdfPCell PopulateCell(TableEntry[] entries)
        {
            PdfPCell cell = GetCell();

            PdfPTable qsoTable = new PdfPTable(5);
            qsoTable.SetWidths(new [] { 1.6f, 1, 1, 0.8f, 1.2f });
            qsoTable.WidthPercentage = 100f;
            
            AddCell(qsoTable, s_HeaderFont, "Date");
            AddCell(qsoTable, s_HeaderFont, "UTC");
            AddCell(qsoTable, s_HeaderFont, "Band");
            AddCell(qsoTable, s_HeaderFont, "RST");
            AddCell(qsoTable, s_HeaderFont, "Mode");

            Phrase titlePhrase = new Phrase();
            titlePhrase.Add(new Chunk(entries[0].MyCall, s_MyCallFont));
            titlePhrase.Add(new Chunk(" confirms the following QSO(s) with ", s_TitleTextFont));
            titlePhrase.Add(new Chunk(entries[0].Callsign, s_MyCallFont));
            titlePhrase.Add(new Chunk(":", s_TitleTextFont));
            cell.AddElement(titlePhrase);
            foreach (TableEntry qso in entries)
            {
                AddCell(qsoTable, s_TableFont, qso.UtcTime.ToString("yy-MM-dd"));
                AddCell(qsoTable, s_TableFont, qso.UtcTime.ToString("hhmm"));
                AddCell(qsoTable, s_TableFont, qso.Band);
                AddCell(qsoTable, s_TableFont, qso.Rst);
                AddCell(qsoTable, s_TableFont, qso.Mode);
            }
            cell.AddElement(qsoTable);
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
