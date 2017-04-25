using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class CrewProperties
    {
        public int CrewID
        {get; set;}

        public string Surname
        {get; set;}

        public string GivenName
        { get; set; }

        public int RankID
        {get; set;}

        public DateTime Available_From_Date
        { get; set; }

        public string Alias
        { get; set; }

        public DateTime DateOfBirth
        {get; set;}

        public string PlaceofBirth
        {get; set;}

        public int Nationality
        {get; set;}

        public string MaritalStatus
        {get; set;}

        public string Telephone
        {get; set;}

        public string Address
        {get; set;}

        public string Mobile
        {get; set;}

        public string Fax
        {get; set;}

        public string EMail
        {get; set;}

        public string NearestInternationalAirport
        {get; set;}
        public int NearestInternationalAirportID
        { get; set; }

        public string Passport_Number
        {get; set;}
        public DateTime? Passport_Issue_Date
        { get; set; }
        public DateTime? Passport_Expiry_Date
        {get; set;}
        public string Passport_PlaceOf_Issue
        {get; set;}
        public int Passport_Country { get; set; }
        public string Seaman_Book_Number
        {get; set;}
        public DateTime? Seaman_Book_Issue_Date
        { get; set; }
        public DateTime? Seaman_Book_Expiry_Date
        {get; set;}
        public string Seaman_Book_PlaceOf_Issue
        {get; set;}
        public int Seaman_Book_Country{ get; set; }
        public string MMC_Number
        { get; set; }
        public DateTime? MMC_Issue_Date
        { get; set; }
        public DateTime? MMC_Expiry_Date
        { get; set; }
        public string MMC_PlaceOf_Issue
        { get; set; }
        public int MMC_Country { get; set; }

        public string TWIC_Number
        { get; set; }
        public DateTime? TWIC_Issue_Date
        { get; set; }
        public DateTime? TWIC_Expiry_Date
        { get; set; }
        public string TWIC_PlaceOf_Issue
        { get; set; }
        public int TWIC_Country { get; set; }

        public int Workedwith_Multinational_Crew
        { get; set; }
        public string MultinationalCrew_Nationalities
        { get; set; }
        public int ManningOfficeID
        {get; set;}

        public int Created_By
        { get; set; }

        public int Modified_By
        { get; set; }

        public int USVisa_Flag
        { get; set; }
        public string USVisa_Number
        { get; set; }
        public DateTime? USVisa_Expiry
        { get; set; }
        public DateTime? USVisa_Issue_Date
        { get; set; }
        public string Allotment_AccType
        { get; set; }

        public DateTime? HireDate
        { get; set; }

        public int UnionID
        { get; set; }

        public int UnionBranch
        { get; set; }
        public int Permanent
        { get; set; }
        public int UnionBook
        { get; set; }
        public int Race { get; set; }
        public int School { get; set; }
        public string SchoolYearGraduated { get; set; }
        public bool Naturaliztion { get; set; }
        public DateTime? NaturaliztionDate { get; set; }
        public string EnglishProficiency { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Waist { get; set; }
        public string ShoeSize  { get; set; }
        public string TShirtSize  { get; set; }
        public string CargoPantSize  { get; set; }
        public string OverallSize { get; set; }
        public string CF1 { get; set; }
        public string CF2 { get; set; }
        public string CF3 { get; set; }
    }
}
