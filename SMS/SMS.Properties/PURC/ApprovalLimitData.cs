using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;

 
namespace SMS.Properties
{
  

    public class ApprovalLimitData
    {
        public ApprovalLimitData()
        {

        }
        private string _ID;
        private string _UserID;
        private string _Approval_Limit;
        private string _Category;
        private string _Current_User;
        private string _Vessel_Code;
        

         
        public string  ID
        {
            get { return _ID; }
            set { _ID = value; }

        }
         
        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }
         
        public string Approval_Limit
        {
            get { return _Approval_Limit; }
            set { _Approval_Limit = value; }
        }

         
        public string Category
        {
            get { return _Category; }

            set { _Category = value; }
        }
       
       
       
         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }

         
        public string Vessel_Code
        {
            get { return _Vessel_Code; }
            set { _Vessel_Code = value; }
        }
      
    }  

}
