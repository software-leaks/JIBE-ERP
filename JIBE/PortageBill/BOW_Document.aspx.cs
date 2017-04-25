using System;


using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PortageBill;


public partial class PortageBill_BOW_Document : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Data.DataTable dtBow = BLL_PB_PortageBill.Get_BOW_Document(UDFLib.ConvertToInteger(Request.QueryString["Vessel_ID"]), UDFLib.ConvertDateToNull(Request.QueryString["PBill_Date"]));
            gvBOWDocuments.DataSource = dtBow;
            gvBOWDocuments.DataBind();

            if (dtBow.Rows.Count > 0)
            {
                lblpageheader.Text = "Scanned copy of BOW : Vessel - " + dtBow.Rows[0]["Vessel_Name"].ToString() + ", PBill Date - " + dtBow.Rows[0]["PBill_Date"].ToString();

            }
        }
    }
}