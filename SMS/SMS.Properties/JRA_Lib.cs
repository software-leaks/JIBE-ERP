using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class JRA_Lib
    {
        public int Work_Categ_ID { get; set; }
        public String Work_Categ_Value { get; set; }
        public string Work_Category_Name { get; set; }
        public int? Work_Categ_Parent_ID { get; set; }
        public int UserID { get; set; }
        public int Mode { get; set; }
        public int Type_ID{ get; set; }
	    public string Type{ get; set; }
	    public string  Type_Value{ get; set; }
	    public string  Type_Display_Text{ get; set; }
	    public string  Type_Description{ get; set; }
        public string Type_Color { get; set; }
        public string DB_Mode { get; set; }
        public int Rating_ID { get; set; }
        public string RiskType{get;set;}
        public int RatingValue { get; set;}


        public string SearchText { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string SearchType { get; set; }

        public string SearchCate { get; set; }
        public int ReturnVal { get; set; }

    }
}
