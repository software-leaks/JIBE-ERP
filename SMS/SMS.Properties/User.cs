using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class UserProperties
    {
        public int UserId
        { get; set; }

        public int CompanyId
        { get; set; }

        public string UserName
        { get; set; }

        public string Password
        { get; set; }

        public string First_Name
        { get; set; }

        public string Middle_Name
        { get; set; }

        public string Last_Name
        { get; set; }

        public DateTime DOB
        { get; set; }

        public string Designation
        { get; set; }

        public string PresentAddress
        { get; set; }

        public string PermanentAddress
        { get; set; }

        public int ApprovalLimit
        { get; set; }

        public string UserType
        { get; set; }

        public DateTime DateOfJoining
        { get; set; }

        public DateTime DateOfProbation
        { get; set; }

        public int DepartmentID
        { get; set; }

        public string NetWkId
        { get; set; }

        public int FleetID
        { get; set; }

        public string Email
        { get; set; }

        public string Phone
        { get; set; }

        public int Created_By
        { get; set; }

        public DateTime Date_Of_Creation
        { get; set; }

        public int Modified_By
        { get; set; }

        public DateTime Date_Of_Modification
        { get; set; }

        public int Deleted_By
        { get; set; }

        public DateTime Date_Of_Deletion
        { get; set; }

        public int ManagerID
        { get; set; }

        public string Mobile
        { get; set; }
    }

    public class UserAccess
    {
        public int Id
        { get; set; }

        public int UserId
        { get; set; }

        public int IsAdmin
        { get; set; }

        public int Menu_Code
        { get; set; }

        public int View
        { get; set; }

        public int Add
        { get; set; }

        public int Edit
        { get; set; }

        public int Delete
        { get; set; }

        public int Approve
        { get; set; }

        public int Admin
        { get; set; }

        public int Unverify
        {
            get;
            set;
        }
        public int Revoke
        {
            get;
            set;
        }
        public int Urgent
        {
            get;
            set;
        }
        public int Close
        {
            get;set;
        }
        public int Unclose
        {
            get;
            set;
        }

        public class Functions
        {
            private string _FunctionName;

            public Functions()
            {
                _FunctionName = "";
            }
            public string FunctionName
            {
                get { return _FunctionName; }
                set { _FunctionName = value; }
            }
            public int Add
            { get; set; }

            public int Edit
            { get; set; }

            public int Delete
            { get; set; }

            public int Approve
            { get; set; }
        }

    }
}
