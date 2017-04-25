using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Operation;
using SMS.Business.Technical;
using SMS.Business.Infrastructure;
using SMS.Properties;

public partial class ERLogTankLevelThresHold : System.Web.UI.Page
{
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    public UserAccess objUA = new UserAccess();
    int EditIndex = -1;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        lblLogId.Attributes.Add("style", "visibility:hidden");
        if (!IsPostBack)
        {
            if (Request.QueryString["LOGID"] != null)
            {
              lblLogId.Text = Request.QueryString["LOGID"].ToString();
             
            }
            if (Request.QueryString["VESSELID"] != null)
            {
                ViewState["VESSELID"] = Request.QueryString["VESSELID"].ToString();
            }
            BindViews();
        }

    }

    private void BindViews()
    {
        DataTable dt = BLL_Tec_ErLog.ErLog_ME_00_Get(int.Parse(lblLogId.Text),int.Parse(ViewState["VESSELID"].ToString()));
        if (dt.Rows.Count > 0)
        {
            FormView1.DataSource = dt;
            FormView1.DataBind();

        }

    }
    protected void FormView1_DataBound(object sender, EventArgs e)
    {
        if (FormView1.CurrentMode == FormViewMode.ReadOnly)
        {
            int rowcount = 0;
            Repeater  fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater1");
            if (fvDetails != null)
            {
                fvDetails.DataSource = BLL_Tec_ErLog.ErLog_TANK_LEVELS_EDIT(int.Parse(ViewState["VESSELID"].ToString()));
                fvDetails.DataBind();
            }
           
        }
    }
    protected void cpRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
       e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.ItemIndex == EditIndex)
            {
              
            }
        }


    }
    protected void cpRepeater_ItemCommand(object sender,RepeaterCommandEventArgs e )
    {
        if (e.CommandName == "edit")
        {
            EditIndex = e.Item.ItemIndex;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Repeater fvDetails = (Repeater)FormView1.Row.Cells[0].FindControl("Repeater1");
        foreach (RepeaterItem ritem in fvDetails.Items)
        {
            Label lblid = (Label)ritem.FindControl("lblid");     
            Label lblVessel = (Label)ritem.FindControl("lblVessel");
            TextBox tx1 = (TextBox)ritem.FindControl("txtCYL_OIL_DAY_TK");
            TextBox tx2 = (TextBox)ritem.FindControl("txtME_SUMP");
            TextBox tx3 = (TextBox)ritem.FindControl("txtHEAVY_OIL_SETTL_TK");
            TextBox tx4 = (TextBox)ritem.FindControl("txtHEAVY_OIL_SERV_TK");
            TextBox tx5 = (TextBox)ritem.FindControl("txtBELENDED_OIL");
            TextBox tx6 = (TextBox)ritem.FindControl("txtDO_SERV_TK");
            TextBox tx01 = (TextBox)ritem.FindControl("txtCYL_OIL_DAY_TK_Max");
            TextBox tx02 = (TextBox)ritem.FindControl("txtME_SUMP_Max");
            TextBox tx03 = (TextBox)ritem.FindControl("txtHEAVY_OIL_SETTL_TK_Max");
            TextBox tx04 = (TextBox)ritem.FindControl("txtHEAVY_OIL_SERV_TK_Max");
            TextBox tx05 = (TextBox)ritem.FindControl("txtBELENDED_OIL_Max");
            TextBox tx06 = (TextBox)ritem.FindControl("txtDO_SERV_TK_Max");
            bool valstatus = true; 
            if ((UDFLib.ConvertDecimalToNull(tx1.Text) != null) && (UDFLib.ConvertDecimalToNull(tx01.Text) != null) && (decimal.Parse(tx1.Text) > decimal.Parse(tx01.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx2.Text) != null) && (UDFLib.ConvertDecimalToNull(tx02.Text) != null) && (decimal.Parse(tx2.Text) > decimal.Parse(tx02.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx3.Text) != null) && (UDFLib.ConvertDecimalToNull(tx03.Text) != null) && (decimal.Parse(tx3.Text) > decimal.Parse(tx03.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx4.Text) != null) && (UDFLib.ConvertDecimalToNull(tx04.Text) != null) && (decimal.Parse(tx4.Text) > decimal.Parse(tx04.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx5.Text) != null) && (UDFLib.ConvertDecimalToNull(tx05.Text) != null) && (decimal.Parse(tx5.Text) > decimal.Parse(tx05.Text))) valstatus = false;
            if ((UDFLib.ConvertDecimalToNull(tx6.Text) != null) && (UDFLib.ConvertDecimalToNull(tx06.Text) != null) && (decimal.Parse(tx6.Text) > decimal.Parse(tx06.Text))) valstatus = false;
            if (valstatus)
            {
                int i = BLL_Tec_ErLog.ErLog_TANK_LEVELS_THRESHOLD_Update(UDFLib.ConvertIntegerToNull(lblid.Text), UDFLib.ConvertIntegerToNull(ViewState["VESSELID"].ToString()), UDFLib.ConvertDecimalToNull(tx1.Text), UDFLib.ConvertDecimalToNull(tx2.Text),
                    UDFLib.ConvertDecimalToNull(tx3.Text), UDFLib.ConvertDecimalToNull(tx4.Text), UDFLib.ConvertDecimalToNull(tx5.Text), UDFLib.ConvertDecimalToNull(tx6.Text), UDFLib.ConvertDecimalToNull(tx01.Text),
                    UDFLib.ConvertDecimalToNull(tx02.Text), UDFLib.ConvertDecimalToNull(tx03.Text), UDFLib.ConvertDecimalToNull(tx04.Text), UDFLib.ConvertDecimalToNull(tx05.Text), UDFLib.ConvertDecimalToNull(tx06.Text), Convert.ToInt32(Session["USERID"]));
                string js = "alert('Changes are updated ');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            }
            else
            {
                string js = "alert('Please check your data');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Update", js, true);
            }
           
        }
            

    }
    protected void UserAccessValidation()
    {

        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(Convert.ToInt32(Session["USERID"]), PageURL);
        if (objUA.Edit == 0)
        {
            btnSave.Enabled = false;
            FormView1.Enabled = false;
        }

        if (objUA.View == 0)
        {
            Response.Redirect("~/crew/default.aspx?msgid=1");
        }

        if (objUA.Add == 0)
        {

        }
    }

}