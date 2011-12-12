using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;

namespace QslEngine
{
    public class PdfEngine
    {
        public static void Test()
        {
            Document doc = new Document(new Rectangle(0,0,mm2p(210),mm2p(297),0),0,0,0,0);
            doc.SetMargins(mm2p(0), mm2p(0), mm2p(15), mm2p(0));

            PdfPTable table = new PdfPTable(3);
            table.TotalWidth = mm2p(196);
            table.SetWidths(new[] { 1, 1, 1 });

            TableEntry qso = new TableEntry { Band = "160m", Callsign = "MM0MMM/M", Mode = "PSK31", Rst = "599", UtcTime = DateTime.UtcNow, MyCall = "GS3PYE/P" };
            TableEntry[] qsos = new[] { qso, qso, qso, qso, qso, qso };
            table.AddCell(PopulateCell(qsos)); table.AddCell(PopulateCell(qsos)); table.AddCell(PopulateCell(qsos));
            table.AddCell(PopulateCell(qsos)); table.AddCell(PopulateCell(qsos)); table.AddCell(PopulateCell(qsos));

            using (FileStream fs = new FileStream(@"C:\temp\pdf.pdf", FileMode.Create))
            {
                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                {
                    doc.Open();
                    doc.Add(table);
                    //doc.Add(new Paragraph("bar"));
                    doc.Close();
                }
            }
        }

        private static PdfPCell GetCell()
        {
            PdfPCell cell = new PdfPCell(new Phrase("Hello cell"));
            cell.FixedHeight = mm2p(38.1);
            cell.Border = 0;
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
            titlePhrase.Add(new Chunk(" confirms the following QSO(s):", s_TitleTextFont));
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

        private static readonly Font s_HeaderFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.ITALIC);
        private static readonly Font s_TableFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.NORMAL);
        private static readonly Font s_MyCallFont = new Font(Font.FontFamily.TIMES_ROMAN, 11, Font.NORMAL);
        private static readonly Font s_TitleTextFont = new Font(Font.FontFamily.TIMES_ROMAN, 9, Font.NORMAL);

        public static float mm2p(double mm)
        {
            return (float)(mm * 2.83464567);
        }
    }
}
