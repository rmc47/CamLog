using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Reg1TestHeader
    {
        public string ContestName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Callsign { get; set; }
        public Locator Locator { get; set; }
        public string Section { get; set; }
        public Band Band { get; set; }
        public string Club { get; set; }
        public string ContactName { get; set; }
        public string ContactCall { get; set; }
        public string ContactAddress1 { get; set; }
        public string ContactAddress2 { get; set; }
        public string ContactCity { get; set; }
        public string ContactCounty { get; set; }
        public string ContactPostCode { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string Operators { get; set; }
        public string Transmitter { get; set; }
        public string Receiver { get; set; }
        public int Power { get; set; }
        public string Antenna { get; set; }
        public int HeightAboveGround { get; set; }
        public int HeightAboveSea { get; set; }
        public int Qsos { get; set; }
        public int Multipliers { get; set; }
        public int Points { get; set; }
        public int TotalScore { get; set; }
        public string OdxCall { get; set; }
        public Locator OdxLocator { get; set; }
        public int OdxDistance { get; set; }

        public string HeaderText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[REG1TEST;1]\r\n");
                sb.AppendFormat("TName={0}\r\n", ContestName);
                sb.AppendFormat("TDate={0};{1}\r\n", StartDate.ToString("yyMMdd"), EndDate.ToString("yyMMdd"));
                sb.AppendFormat("PCall={0}\r\n", Callsign);
                sb.AppendFormat("PWWLo={0}\r\n", Locator);
                sb.AppendFormat("PSect={0}\r\n", Section);
                sb.AppendFormat("PBand={0}\r\n", BandHelper.ToString(Band));
                sb.AppendFormat("PClub={0}\r\n", Club);
                sb.AppendFormat("RName={0}\r\n", ContactName);
                sb.AppendFormat("RCall={0}\r\n", ContactCall);
                sb.AppendFormat("RAdr1={0}\r\nRAdr2={1}\r\n", ContactAddress1, ContactAddress2);
                sb.AppendFormat("RCity={0}\r\nRCoun={1}\r\n", ContactCity, ContactCounty);
                sb.AppendFormat("RPoCo={0}\r\n", ContactPostCode);
                sb.AppendFormat("RPhon={0}\r\nRHBBS={1}\r\n", ContactPhone, ContactEmail);
                sb.AppendFormat("MOpe1={0}\r\n", Operators);
                sb.AppendFormat("STXEq={0}\r\nSRXEq={1}\r\n", Transmitter, Receiver);
                sb.AppendFormat("SPowe={0}\r\n", Power);
                sb.AppendFormat("SAnte={0}\r\nSAntH={1};{2}\r\n", Antenna, HeightAboveGround, HeightAboveSea);
                sb.AppendFormat("CQSOs={0};{1}\r\n", Qsos, Multipliers);
                sb.AppendFormat("CQSOP={0}\r\nCToSc={1}\r\n", Points, TotalScore);
                sb.AppendFormat("CODXC={0};{1};{2}\r\n", OdxCall, OdxLocator, OdxDistance);
                sb.AppendFormat("[Remarks]\r\n");
                sb.AppendFormat("[QSORecords;{0}]\r\n", Qsos);
                return sb.ToString();
            }
        }
    }
}
