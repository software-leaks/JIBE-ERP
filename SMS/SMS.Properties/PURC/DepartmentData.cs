using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;

 
namespace SMS.Properties
{

    public class DepartmentData
    {
        public DepartmentData()
        {

        }
        private string _deptID;
        private string _dept_Code;
        private string _dept_Name;
        private string _form_type;
        private string _Vessel_Code;
        private string _Link;
        private string _Current_User;
        private string _Ac_Classi_Code;
        private string _Approval_Group_Code;

         
        public string DeptID
        {
            get { return _deptID; }
            set { _deptID = value; }

        }
         
        public string Dept_code
        {
            get { return _dept_Code; }
            set { _dept_Code = value; }
        }
         
        public string Dept_name
        {
            get { return _dept_Name; }
            set { _dept_Name = value; }
        }

         
        public string FormType
        {
            get { return _form_type; }

            set { _form_type = value; }
        }
       
         
        public string Vessel_Code
        {
            get { return _Vessel_Code; }
            set { _Vessel_Code = value; }
        }
         

        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }
         
        public string CurrentUser
        {
            get { return _Current_User; }
            set { _Current_User = value; }
        }


        public string Ac_Clssification_Code
        {

            get { return _Ac_Classi_Code; }
            set { _Ac_Classi_Code = value; }
        
        
        }
        public string Approval_Group_Code 
        {

            get { return _Approval_Group_Code; }
            set { _Approval_Group_Code = value; }


        }
      
    }  

}
