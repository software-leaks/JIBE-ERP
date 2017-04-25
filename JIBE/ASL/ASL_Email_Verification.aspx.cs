using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Business.ASL;
using System.IO;

public partial class ASL_ASL_Email_Verification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["Supp_ID"].ToString()))
        {
            //Supplier_Data_Insert
            DataTable dt = BLL_ASL_Supplier.Supplier_Email_Verification(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"].ToString()));

            if (dt.Rows.Count > 0)
            {
                lbl1.Text = "Email Verification Successfully.";
            }
        }
    }
}