using Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QslEngine
{
    /// <summary>
    /// Sorts cards for bureau outgoing - callsigns are grouped by DXCC of the base callsign (i.e. DL/M0VFC is grouped as England rather than Germany).
    /// </summary>
    public class BureauSorter
    {
        private const int ADIF_ENGLAND = 223;
        private const int ADIF_WALES = 294;
        private const int ADIF_SCOTLAND = 279;
        private const int ADIF_NORTHERN_IRELAND = 265;
        private const int ADIF_ISLE_OF_MAN = 114;
        private const int ADIF_JERSEY = 122;
        private const int ADIF_GUERNSEY = 106;

        private Dictionary<int, int> m_ModifiedAdifMapping = new Dictionary<int, int>
        {
            { ADIF_ENGLAND, -100 },
            { ADIF_WALES, -99 },
            { ADIF_SCOTLAND, -98 },
            { ADIF_NORTHERN_IRELAND, -97 },
            { ADIF_ISLE_OF_MAN, -96 },
            { ADIF_JERSEY, -95 },
            { ADIF_GUERNSEY, -94 }
        };

        private List<string> Callsigns { get; set; }
        private Dictionary<string, int> CallsignDxccs { get; set; }

        public BureauSorter(IEnumerable<string> allCallsigns)
        {
            Callsigns = allCallsigns.ToList();
            CallsignDxccs = new BulkDxccLookup().Lookup(Callsigns.Select(c => BaseCall(c)));
        }

        public int Sort(List<Contact> contacts1, List<Contact> contacts2)
        {
            if ((contacts1 == null || contacts1.Count == 0) && (contacts2 == null || contacts2.Count == 0))
                return 0;
            if (contacts1 == null || contacts1.Count == 0)
                return 1;
            if (contacts2 == null || contacts2.Count == 0)
                return -1;

            string call1 = BaseCall(contacts1[0].Callsign);
            string call2 = BaseCall(contacts2[0].Callsign);

            int adifComparison = RsgbOrderedAdif(call1).CompareTo(RsgbOrderedAdif(call2));
            if (adifComparison != 0)
                return adifComparison;

            return contacts1[0].Callsign.CompareTo(contacts2[0].Callsign);
        }

        /// <summary>
        /// Extracts the "base" or home callsign from a potentially modified call, e.g:
        ///     M0VFC => M0VFC
        ///     DL/M0VFC => M0VFC
        ///     M0VFC/P => M0VFC
        ///     W6/M0VFC/M => M0VFC
        ///     
        /// Just looks for the largest part between /s, preferring the *last* in the case of ties (e.g. VP2V/K2WH)
        /// </summary>
        /// <param name="callsign"></param>
        /// <returns></returns>
        private static string BaseCall(string callsign)
        {
            return callsign.Split('/').OrderBy(part => part.Length).Last();
        }

        private int RsgbOrderedAdif(string callsign)
        {
            int adif;
            if (!CallsignDxccs.TryGetValue(callsign, out adif))
                throw new ArgumentException("BureauSorter must be provided all callsigns to be sorted at initialisation");

            // If we have an alternative mapping for the ADIF country code, use that - used to hoist UK DXCCs up to the front of the sort order
            int modifiedAdif;
            if (m_ModifiedAdifMapping.TryGetValue(adif, out modifiedAdif))
                return modifiedAdif;
            else
                return adif;
        }
    }
}
