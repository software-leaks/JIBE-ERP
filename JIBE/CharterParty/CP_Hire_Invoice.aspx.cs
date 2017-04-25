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

public partial class CP_Hire_Invoice : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    public int Inv_ID = 0;
    public int CPID = 0;
    public int PortId = 0;
    public string  PortName = "";
    public string OType = "";
    public Boolean uaEditFlag = true;//Test default true
    public Boolean uaDeleteFlage = true;
    BLL_CP_CharterParty objCP = new BLL_CP_CharterParty();
    BLL_CP_HireInvoice objHireInv = new BLL_CP_HireInvoice();
    BLL_Infra_UserCredentials objUserBLL = new BLL_Infra_UserCredentials();
    protected void Page_Load(object sender, EventArgs e)
    {
       // UserAccessValidation();
        if (!IsPostBack)
        {
            if (Session["CPID"] != null)
            {
                CPID = Convert.ToInt32(Session["CPID"]);
                BindStatus();
                BindInvoices();
            }


        }
    }

    protected void BindStatus()
    {
        DataTable dt = objHireInv.GET_Hire_InvStatusList();
        chkStatusList.DataSource = dt;
        chkStatusList.DataTextField = "VARIABLE_NAME";
        chkStatusList.DataValueField = "ID";
        chkStatusList.DataBind();
        chkStatusList.Items[0].Selected = true;
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            ibtnAdd.Enabled = false;
        }
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

    protected void BindInvoices()
    {
        DataTable dtStatus = new DataTable();

        dtStatus.Columns.Add("ID");

        DataTable dtPaymentStatus = new DataTable();
        dtPaymentStatus.Columns.Add("ID");
        dtPaymentStatus.Columns.Add("PaymentStatus");


        foreach (ListItem chkitem in chkStatusList.Items)
        {
            if (chkitem.Selected)
            {
                DataRow dr = dtStatus.NewRow();

                dr["ID"] = chkitem.Value;

                dtStatus.Rows.Add(dr);
            }
        }
        foreach (ListItem chkitem in chkPaymentType.Items)
        {
            int count;
            if (chkitem.Selected)
            {
                DataRow dr = dtPaymentStatus.NewRow();
                dr["ID"] = chkitem.Value;
                dr["PaymentStatus"] = chkitem.Text;

                dtPaymentStatus.Rows.Add(dr);
            }
        }



        DataTable dt = objHireInv.Get_Hire_InvALL(UDFLib.ConvertIntegerToNull(Session["CPID"]), dtStatus, dtPaymentStatus);

        gvHireInvoices.DataSource = dt;
        gvHireInvoices.DataBind();
        dt.Dispose();
        dtStatus.Dispose();
        dtPaymentStatus.Dispose();
    }










    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    GridViewRow gvr = e.Row;

            //    ImageButton ibtnMarkRead = (ImageButton)gvr.FindControl("ibtnMarkRead");
            //    ImageButton ibtnClose = (ImageButton)gvr.FindControl("ibtnClose");
            //    ImageButton ibtnDelete = (ImageButton)gvr.FindControl("lbtnDelete");
            //    int CreatedUserId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CreatedUserId"));
            //    int For_Action_By = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "For_Action_By"));

            //    string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
            //    if (GetSessionUserID() == For_Action_By)
            //        ibtnMarkRead.Visible = true;
            //    if (GetSessionUserID() == CreatedUserId)
            //        ibtnClose.Visible = true;

            //    if (status.ToUpper() == "CLOSE")
            //    {
            //        ibtnDelete.Visible = false;
            //        ibtnMarkRead.Visible = false;
            //        ibtnClose.Visible = true;
            //        ibtnClose.Enabled = false;

            //    }

            //}
        }
        catch { }
    }
    //protected void ibtnAdd_Click(object sender, ImageClickEventArgs e)
    //{

    //   iFrame1.Attributes["src"] = "CP_Hire_Invoice_Entry.aspx?CPID=" + ViewState["CPID"].ToString() + "&InvID=0";

    //}


    protected void ibtnView_Click(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Inv_ID = Convert.ToInt32(e.CommandArgument);
            ViewState["Inv_ID"] = Inv_ID;
           // iFrame1.Attributes["src"] = "CP_Hire_Invoice_Entry.aspx?CPID=" + ViewState["CPID"].ToString() + "&InvID=" + Inv_ID.ToString();


        }
        catch { }

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        BindInvoices();

    }
    protected void chkStatusList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListItem chkitem in chkStatusList.Items)
        {
            if (chkitem.Selected)
                count++;
        }

        if(count == 0)
            chkStatusList.Items[0].Selected = true;
    }
    protected void chkPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        foreach (ListItem chkitem in chkPaymentType.Items)
        {
            if (chkitem.Selected)
                count++;
        }

        if (count == 0)
            chkPaymentType.Items[0].Selected = true;
    }
    }

   
