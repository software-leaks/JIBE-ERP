using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class CP_Remittance_Receipt : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public string Remittance_ID = "0";
    public int CPID = 0;
    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            if (Request.QueryString["CPID"] != null)
            {
                CPID = Convert.ToInt32(Request.QueryString["CPID"]);
                ViewState["CPID"] = CPID.ToString();
            }
            BindRemittance();

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        //if (objUA.Add == 0)
        //{
        //    btnSave.Enabled = false;
        //}
        if (objUA.Edit == 1)
            uaEditFlag = true;
        else
            // btnsave.Visible = false;
            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindRemittance()
    {

        DataTable dt = objCP.Get_Remittance_Receipt_List(UDFLib.ConvertIntegerToNull(ViewState["CPID"]));

        gvRemittance.DataSource = dt;
        gvRemittance.DataBind();
    }

    protected void ClearData()
    {

        ltmessage.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
        BindRemittance();
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        SaveData();
        this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close();", true);
    }

    protected void SaveData()
    {
        int res = 1;
        try
        {
            //res = objCP.INS_Remittance(UDFLib.ConvertIntegerToNull(ViewState["CPID"]), UDFLib.ConvertIntegerToNull(ddlUser.SelectedValue),
            //    txtRemittance.Text, GetSessionUserID());
            ClearData();
            if (res == 1)
                ltmessage.Text = "Remark added successfully.";
              

        }
        catch { }
    }

    protected void ibtnMarkRead_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Remittance_ID = e.CommandArgument.ToString();
            ViewState["Remittance_ID"] = Remittance_ID;
            objCP.Unmatched_Remittance(UDFLib.ConvertIntegerToNull(Session["CPID"]), Remittance_ID, GetSessionUserID());
            BindRemittance();


        }
        catch { }

    }

    protected void gvRemittance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow gvr = e.Row;

                ImageButton ibtnMarkRead = (ImageButton)gvr.FindControl("ibtnMarkRead");
                Literal ltUnmatched = (Literal)gvr.FindControl("ltUnmatched");
                Label lblAllocated = (Label)gvr.FindControl("lblAllocated");
                double AllocatedAmt = Convert.ToDouble(lblAllocated.Text);
                if (AllocatedAmt == 0)
                {
                    ltUnmatched.Visible = true;
                    ibtnMarkRead.Visible = false;
                }


            }
        }
        catch { }
    }
    protected void gvRemittance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRemittance.PageIndex = e.NewPageIndex;
        BindRemittance();
    }
}
   
