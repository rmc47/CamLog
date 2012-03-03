using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Engine
{
    public static class CabrilloExporter
    {
        const string zxa_callUsed = "G6UW"; // Callsign sent
        const string zxa_locatorSent = "JO02af"; // Locator sent
        const string zxa_contest = "RSGB-UKAC"; // Contest name (optional)
        const string zxa_operators = "G3ZAY M0ZXA"; // Operators
        const string zxa_claimedScore = ""; // Claimed score (optional)

        public static void ExportContacts(IEnumerable<Contact> contacts, string exportLocation)
        {
            using (StreamWriter writer = new StreamWriter(exportLocation))
            {
                writer.WriteLine("START-OF-LOG: 2.0");
                writer.WriteLine("CREATED-BY: M0VFC CamLog");
                if (string.IsNullOrEmpty(zxa_contest))
                {
                    writer.WriteLine("CONTEST: " + zxa_contest.ToUpper());
                }
                writer.WriteLine("CALLSIGN: " + zxa_callUsed.ToUpper());
                if (string.IsNullOrEmpty(zxa_claimedScore))
                {
                    writer.WriteLine("CLAIMED SCORE: " + zxa_claimedScore);
                }
                writer.WriteLine("OPERATORS: " + zxa_operators.ToUpper());
                writer.WriteLine("");
                foreach (Contact c in contacts)
                    ExportContact(c, writer);
            }
        }

        private static void ExportContact(Contact c, StreamWriter writer)
        {
           writer.WriteLine("QSO: {0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}",
                zxa_ensureLength(BandHelper.ToMHzString(c.Band), 4, ' ', true),
                ModeHelper.ToCabrilloString(c.Mode),
                c.StartTime.ToString("yyyy-MM-dd HHmm"),
                zxa_ensureLength(zxa_callUsed, 15, ' ', false).ToUpper(),
                zxa_locatorSent.ToUpper(),
                zxa_ensureLength(c.ReportSent, 3, ' ', false),
                zxa_ensureLength(c.SerialSent.ToString(), 3, '0', true),
                zxa_ensureLength(c.Callsign, 15, ' ', false).ToUpper(),
                c.LocatorReceived.ToString().ToUpper(),
                zxa_ensureLength(c.ReportReceived, 3, ' ', false),
                zxa_ensureLength(c.SerialReceived.ToString(), 3, '0', true));
        }

        ///<summary>
        /// This allows a minimum length to be
        /// defined for the export fields, to
        /// improve readability for Humans.
        ///
        /// zxa_string is string to adjust
        /// zxa_length is the minimum length
        /// zxa_filler is the character to add to lengthen the string
        /// zxa_front selects fill at front (1) or back (0) of string
        ///
        ///</summary>
        private static string zxa_ensureLength(string zxa_string, int zxa_length, char zxa_filler, bool zxa_front)
        {
            if (zxa_front == true) // If to fill from front
            {
                while (zxa_string.Length < zxa_length) // If string is too short
                {
                    zxa_string = zxa_filler + zxa_string; // Lengthen string with filler character
                }
                return zxa_string; // Return string
            }
            else // If to fill from end
            {
                while (zxa_string.Length < zxa_length) // If string is too short
                {
                    zxa_string = zxa_string + zxa_filler; // Lengthen string with filler character
                }
                return zxa_string; // Return string
            }
        }
    }
}