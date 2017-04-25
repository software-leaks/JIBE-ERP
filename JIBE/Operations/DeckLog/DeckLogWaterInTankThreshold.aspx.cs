using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;
using SMS.Business.Operations;


public partial class DeckLogWaterInTankThreshold : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            BindWaterInTankThrasholdList();

        }
    }


    public void BindWaterInTankThrasholdList()
    {
        DataTable dt = BLL_OPS_DeckLog.Get_DeckLogBook_Water_In_Tank_Thrashold_List(int.Parse(Request.QueryString["Vessel_ID"].ToString()), int.Parse(Request.QueryString["LogBookId"].ToString()));

        if (dt.Rows.Count > 0)
        {
            txtSoundingMin.Text = dt.Rows[0]["MIN_Water_In_Tank_Sounding"].ToString();
            txtSoundingMax.Text = dt.Rows[0]["MAX_Water_In_Tank_Sounding"].ToString();

            if (dt.Rows[0]["Active_Status"].ToString() == "0")
            {
                btnSave.Enabled = false;
            }
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        int retVal = BLL_OPS_DeckLog.Insert_DeckLogBook_Water_In_Tank_Thrashold(int.Parse(Request.QueryString["Vessel_ID"].ToString())
         , UDFLib.ConvertDecimalToNull(txtSoundingMin.Text), UDFLib.ConvertDecimalToNull(txtSoundingMax.Text)
         , int.Parse(Session["UserID"].ToString()));

        String msg = String.Format("javascript:parent.fnReloadParent();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);

    }
}