using System;
namespace WaldoGOP.Models
{
    public class MaineVoter
    {
        public string HHName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string SuffixName { get; set; }

        public string PrimaryAddress1 { get; set; }

        public string PrimaryCity { get; set; }

        public string PrimaryState { get; set; }

        public string PrimaryZip { get; set; }

        public string PrimaryZip4 { get; set; }

        public string PrimaryUnit { get; set; }

        public string PrimaryUnitNumber { get; set; }

        public string SecondaryAddress1 { get; set; }

        public string SecondaryCity { get; set; }

        public string SecondaryState { get; set; }

        public string SecondaryZip { get; set; }

        public string SecondaryZip4 { get; set; }

        public string SecondaryUnit { get; set; }

        public string SecondaryUnitNumber { get; set; }

        public string PrimaryPhone { get; set; }

        public string ObservedParty { get; set; }

        public string OfficialParty { get; set; }

        public string CalculatedParty { get; set; }

        public string CDName { get; set; }

        public string LDName { get; set; }

        public string SDName { get; set; }

        public string CountyName { get; set; }

        public string VoterKey { get; set; }

        public string HHRecId { get; set; }

        public string StateVoterId { get; set; }

        public string ClientId { get; set; }

        public string Moved { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set;  }
    }
}