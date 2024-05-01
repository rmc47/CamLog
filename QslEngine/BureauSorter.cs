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
            Callsigns.Remove(string.Empty);
            var callsignDxccs = new Dictionary<string, int>();
            const int batchSize = 100;
            for (int i = 0; i < Math.Ceiling((float)Callsigns.Count / batchSize); i++)
            {
                var lookupList = Callsigns.GetRange(i * batchSize, Math.Min(batchSize, Callsigns.Count - i * batchSize));
                var dxccs = new BulkDxccLookup("").Lookup(lookupList.Select(c => BaseCall(c)));
                foreach (var dxcc in dxccs)
                    callsignDxccs[dxcc.Key] = dxcc.Value;
            }
            CallsignDxccs = callsignDxccs;
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

            return s_AdifSortOrder.IndexOf(adif);
        }

        private List<int> s_AdifSortOrder = new List<int> {
            
            // This list converted in Excel from http://www.arrl.org/files/file/DXCC/2013_Current_Deleted.txt

            223, //G,GX,M* 
            114, //GD,GT* 
            265, //GI,GN* 
            122, //GJ,GH* 
            279, //GM,GS* 
            106, //GU,GP* 
            294, //GW,GC* 

            247, //(1) 
            246, //1A(1) 
            260, //3A* 
            004, //3B6,7 
            165, //3B8 
            207, //3B9 
            049, //3C 
            195, //3C0 
            176, //3D2* 
            489, //3D2* 
            460, //3D2* 
            468, //3DA# 
            474, //3V* 
            293, //3W,XV 
            107, //3X 
            024, //3Y* 
            199, //3Y* 
            018, //4J,4K 
            075, //4L* 
            514, //4O(47)* 
            315, //4S* 
            117, //4U_ITU#* 
            289, //4U_UN* 
            511, //4W(44) 
            336, //4X,4Z#* 
            436, //5A 
            215, //5B,C4,P3* 
            470, //5H,5I* 
            450, //5N* 
            438, //5R 
            444, //5T(2) 
            187, //5U(3) 
            483, //5V 
            190, //5W* 
            286, //5X* 
            430, //5Y,5Z* 
            456, //6V,6W(4)* 
            082, //6Y#* 
            492, //7O(5) 
            432, //7P 
            440, //7Q 
            400, //7T-7Y* 
            062, //8P* 
            159, //8Q 
            129, //8R#* 
            497, //9A(6)* 
            424, //9G(7)#* 
            257, //9H* 
            482, //9I,9J* 
            348, //9K* 
            458, //9L# 
            299, //9M2,4(8)* 
            046, //9M6,8(8)* 
            369, //9N 
            414, //9Q-9T* 
            404, //9U(9) 
            381, //9V(10)* 
            454, //9X(9) 
            090, //9Y,9Z#* 
            402, //A2* 
            160, //A3 
            370, //A4* 
            306, //A5 
            391, //A6 
            376, //A7* 
            304, //A9* 
            372, //AP* 
            318, //B* 
            506, //BS7(11) 
            386, //BU-BX* 
            505, //BV9P(12) 
            157, //C2 
            203, //C3* 
            422, //C5# 
            060, //C6 
            181, //C8,C9* 
            112, //CA-CE#* 
            047, //CE0#* 
            125, //CE0#* 
            217, //CE0#* 
            013, //CE9/KC4^* 
            070, //CM,CO#* 
            446, //CN 
            104, //CP#* 
            272, //CT* 
            256, //CT3* 
            149, //CU* 
            144, //CV-CX#* 
            211, //CY0* 
            252, //CY9* 
            401, //D2,D3 
            409, //D4 
            411, //D6#*(13) 
            230, //DA-DR(14)* 
            375, //DU-DZ,4D-4I#* 
            051, //E3(15) 
            510, //E4(43) 
            191, //E5 
            234, //E5 
            188, //E6* 
            501, //E7(29)#* 
            281, //EA-EH* 
            021, //EA6-EH6* 
            029, //EA8-EH8* 
            032, //EA9-EH9* 
            245, //EI,EJ* 
            014, //EK* 
            434, //EL#* 
            330, //EP,EQ* 
            179, //ER* 
            052, //ES* 
            053, //ET* 
            027, //EU-EW* 
            135, //EX* 
            262, //EY* 
            280, //EZ* 
            227, //F* 
            079, //FG,TO* 
            169, //FH,TO(13)* 
            516, //FJ,TO(49)* 
            162, //FK,TX* 
            512, //FK,TX(45) 
            084, //FM,TO* 
            508, //FO,TO(16)* 
            036, //FO,TX* 
            175, //FO,TX* 
            509, //FO,TX(16)* 
            277, //FP* 
            453, //FR,TO* 
            099, //FT/G,TO(17)* 
            124, //FT/J,E,TO(17)* 
            276, //FT/T,TO* 
            213, //FS,TO* 
            041, //FT/W* 
            131, //FT/X* 
            010, //FT/Z* 
            298, //FW* 
            063, //FY* 
            // ***** UK HOISTED TO TOP *****
            185, //H4* 
            507, //H40(18)* 
            239, //HA,HG* 
            287, //HB* 
            251, //HB0* 
            120, //HC,HD#* 
            071, //HC8,HD8#* 
            078, //HH# 
            072, //HI#* 
            116, //HJ,HK,5J,5K#* 
            161, //HK0#* 
            216, //HK0#* 
            137, //HL,6K-6N* 
            088, //HO,HP#* 
            080, //HQ,HR#* 
            387, //HS,E2* 
            295, //HV 
            378, //HZ* 
            248, //I* 
            225, //IS0,IM0* 
            382, //J2* 
            077, //J3#* 
            109, //J5 
            097, //J6#* 
            095, //J7#* 
            098, //J8# 
            339, //JA-JS,7J-7N* 
            177, //JD1(19)* 
            192, //JD1(20)* 
            363, //JT-JV* 
            259, //JW* 
            118, //JX* 
            342, //JY#* 
            291, //K,W,N,AA-AK# 
            105, //KG4# 
            166, //KH0# 
            020, //KH1# 
            103, //KH2#* 
            123, //KH3#* 
            174, //KH4# 
            197, //KH5# 
            134, //KH5K# 
            110, //KH6,7#* 
            138, //KH7K# 
            009, //KH8#* 
            515, //KH8(48)#* 
            297, //KH9# 
            006, //KL,AL,NL,WL#* 
            182, //KP1# 
            285, //KP2#* 
            202, //KP3,4#* 
            043, //KP5(22)# 
            266, //LA-LN* 
            100, //LO-LW#* 
            254, //LX* 
            146, //LY* 
            212, //LZ* 
            136, //OA-OC#* 
            354, //OD* 
            206, //OE#* 
            224, //OF-OI* 
            005, //OH0* 
            167, //OJ0* 
            503, //OK-OL(23)* 
            504, //OM(23)* 
            209, //ON-OT* 
            221, //OU-OW,OZ* 
            237, //OX* 
            222, //OY* 
            163, //P2(24) 
            091, //P4(25)* 
            344, //P5(26) 
            263, //PA-PI* 
            517, //PJ2(50) 
            520, //PJ4(51) 
            519, //PJ5,6(52) 
            518, //PJ7(53) 
            108, //PP-PY,ZV-ZZ#* 
            056, //PP0-PY0F#* 
            253, //PP0-PY0S#* 
            273, //PP0-PY0T#* 
            140, //PZ 
            061, //R1/F* 
            302, //S0(1),(27) 
            305, //S2* 
            499, //S5(6)* 
            379, //S7 
            219, //S9 
            284, //SA-SM,7S,8S* 
            269, //SN-SR* 
            466, //ST 
            478, //SU 
            236, //SV-SZ,J4* 
            180, //SV/A* 
            045, //SV5,J45* 
            040, //SV9,J49* 
            282, //T2(28) 
            301, //T30 
            031, //T31 
            048, //T32 
            490, //T33 
            232, //T5,6O 
            278, //T7* 
            022, //T8,(21) 
            390, //TA-TC* 
            242, //TF* 
            076, //TG,TD#* 
            308, //TI,TE#* 
            037, //TI9#* 
            406, //TJ 
            214, //TK* 
            408, //TL(30) 
            412, //TN(31) 
            420, //TR(32)* 
            410, //TT(33) 
            428, //TU(34) 
            416, //TY(35) 
            442, //TZ(36)* 
            054, //UA-UI1-7,RA-RZ* 
            126, //UA2,RA2* 
            015, //UA-UI8-0,RA-RZ* 
            292, //UJ-UM 
            130, //UN-UQ* 
            288, //UR-UZ,EM-EO* 
            094, //V2#* 
            066, //V3# 
            249, //V4(37)# 
            464, //V5* 
            173, //V6(38) 
            168, //V7#* 
            345, //V8* 
            001, //VA-VG,VO,VY#* 
            150, //VK,AX#* 
            111, //VK0#* 
            153, //VK0#* 
            038, //VK9C#* 
            147, //VK9L#* 
            171, //VK9M#* 
            189, //VK9N* 
            303, //VK9W#* 
            035, //VK9X#* 
            012, //VP2E(37) 
            096, //VP2M(37) 
            065, //VP2V(37)* 
            089, //VP5* 
            172, //VP6#* 
            513, //VP6(46)* 
            141, //VP8* 
            235, //VP8,LU* 
            238, //VP8,LU* 
            240, //VP8,LU* 
            241, //VP8,LU,CE9,HF0,4K1* 
            064, //VP9* 
            033, //VQ9* 
            321, //VR* 
            324, //VU* 
            011, //VU4* 
            142, //VU7* 
            050, //XA-XI#* 
            204, //XA4-XI4#* 
            480, //XT(39)* 
            312, //XU 
            143, //XW 
            152, //XX9* 
            309, //XY,XZ 
            003, //YA,T6 
            327, //YB-YH(40)* 
            333, //YI* 
            158, //YJ* 
            384, //YK* 
            145, //YL* 
            086, //YN,H6-7,HT#* 
            275, //YO-YR* 
            074, //YS,HU#* 
            296, //YT,YU* 
            148, //YV-YY,4M#* 
            017, //YV0#* 
            452, //Z2 
            502, //Z3(41)* 
            521, //Z8(54) 
            007, //ZA 
            233, //ZB2* 
            283, //ZC4(42)* 
            250, //ZD7* 
            205, //ZD8* 
            274, //ZD9 
            069, //ZF* 
            270, //ZK3* 
            170, //ZL-ZM* 
            034, //ZL7* 
            133, //ZL8* 
            016, //ZL9* 
            132, //ZP#* 
            462, //ZR-ZU#* 
            201, //ZS8* 
        };
    }
}
