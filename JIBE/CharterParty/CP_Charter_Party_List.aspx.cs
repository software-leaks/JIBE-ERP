using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit;
using System.Text;

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.CP;


public partial class CP_Charter_Party_List : System.Web.UI.Page
{

    BLL_CP_CharterParty oBLL_CP = new BLL_CP_CharterParty();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    public int Charter_Party_Id = 0;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();


        if (!IsPostBack)
        {
            BindCPStatus();
            BindVessels();
            BindCPSuppliers();

            BindGrid();
        }

    }

   
    protected void BindCPStatus()
    {
        DataTable dt = oBLL_CP.CP_GetStatus_List();

        chkCPStatus.DataSource = dt;
        chkCPStatus.DataTextField = "Variable_Code";
        chkCPStatus.DataValueField = "ID";
        chkCPStatus.DataBind();
        
    }

    protected void SetDefaultStatus()
    {
        int countselected = 0;
        foreach (ListItem chkitem in chkCPStatus.Items)
        {

            if (chkitem.Selected == true)
            {
                countselected++;
            }

        }
        if (countselected == 0)
        {
            if (chkCPStatus.Items.FindByText("FIXED") != null)
                chkCPStatus.Items.FindByText("FIXED").Selected = true;
            if (chkCPStatus.Items.FindByText("REDELIVERED") != null)
                chkCPStatus.Items.FindByText("REDELIVERED").Selected = true;
        }
    }

    protected void BindVessels()
    {
        BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }
    protected void BindCPSuppliers()
    {
        DataTable dt = oBLL_CP.GetCharterSupplier_List();
        ddlSupplierList.DataSource = dt;
        ddlSupplierList.DataTextField = "Supplier_Name";
        ddlSupplierList.DataValueField = "Supplier_Code";
        ddlSupplierList.DataBind();
        ddlSupplierList.Items.Insert(0, new ListItem("-ALL Charterer-", "0"));
    }

    public void BindGrid()
    {
        try
        {
            SetDefaultStatus();
            int rowcount = ucCustomPager1.isCountRecord;

            DataTable dtStatus = ChkStatus();

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataTable dt = oBLL_CP.Get_Charter_Party_List_Search( UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue), ddlSupplierList.SelectedValue,
               dtStatus, sortbycoloumn, sortdirection , ucCustomPager1.CurrentPageIndex, ucCustomPager1.PageSize, ref  rowcount);


            if (ucCustomPager1.isCountRecord == 1)
            {
                ucCustomPager1.CountTotalRec = rowcount.ToString();
                ucCustomPager1.BuildPager();
            }

            //if (dt.Rows.Count > 0)
            //{
                gvCPList.DataSource = dt;
                gvCPList.DataBind();
            //}
        }
        catch { }
    }

    protected DataTable ChkStatus()
   {

        DataTable dtstatus = new DataTable();
        dtstatus.Columns.Add("PID");
           foreach (ListItem chkitem in chkCPStatus.Items)
           {

               DataRow dr = dtstatus.NewRow();
               if (chkitem.Selected == true)
               {
                   dr["PID"] = chkitem.Value;
                   dtstatus.Rows.Add(dr);
               }

           }

           return dtstatus;


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

    protected void gvSupplier_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();

    }
    protected void gvCPList_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridViewRow gvr = e.Row;

            Label lbldocuments = (Label)gvr.FindControl("lbldocuments");
            Label lblStatus = (Label)gvr.FindControl("lblStatus");
            Label lblUptoDate = (Label)gvr.FindControl("lblUptoDate");
            Label lblRedelivery = (Label)gvr.FindControl("lblRedelivery");
            Label lblOverdue = (Label)gvr.FindControl("lblOverdue");
            
            string MissingDoc = DataBinder.Eval(e.Row.DataItem, "Missing_Docs_Flag").ToString();
            string Status = DataBinder.Eval(e.Row.DataItem, "Cp_Status").ToString();

            if(DataBinder.Eval(e.Row.DataItem, "Upto_Date") != null && DataBinder.Eval(e.Row.DataItem, "Upto_Date").ToString() != "")
            {
                DateTime dtUptodate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Upto_Date"));

                lblUptoDate.Text = dtUptodate.ToString("dd-MMM-yyyy");
                if (DateTime.Now.Date > dtUptodate.AddDays(-11) && Status == "FIXED")
                {
                    lblUptoDate.BackColor = System.Drawing.Color.Yellow;
                        
                }
            }

            if (DataBinder.Eval(e.Row.DataItem, "Estimated_ReDelivery_Notice") != null && DataBinder.Eval(e.Row.DataItem, "Estimated_ReDelivery_Notice").ToString() != "")
            {
                DateTime dtRedelivery = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Estimated_ReDelivery_Notice"));

                lblRedelivery.Text = dtRedelivery.ToString("dd-MMM-yyyy");
                if (DateTime.Now.Date > dtRedelivery && Status == "FIXED" && DataBinder.Eval(e.Row.DataItem, "NoticeReceived").ToString() != "1")
                {
                    lblRedelivery.BackColor = System.Drawing.Color.Red;

                }
            }
            if (lblOverdue.Text != "")
            {
                double OverDue = Convert.ToDouble(lblOverdue.Text);
                if(OverDue > 0)
                    lblOverdue.ForeColor = System.Drawing.Color.Red;
            }

            if (MissingDoc != null && MissingDoc == "YES")
            {
                lbldocuments.Visible = true;

            }
            if(Status =="FIXED")
                lblStatus.ForeColor = System.Drawing.Color.Green ;
            lblStatus.Font.Bold = true;
                
        }

           
  }
 
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void gvCPList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string CharterID = e.CommandArgument.ToString();
            Response.Redirect("../CharterParty/CP_Charter_Party_Details.aspx?CPID=" + CharterID);
                
        }
        else if (e.CommandName == "BreakDown")
        {
            string s = e.CommandArgument.ToString();
            //if (ltBreakdown.Text.Length == 0)
            //    ltBreakdown.Text = "No Record found.";

            //     mpeBreakdown.Show();

        }
    }

    protected void ibtnRemark_click(object source, CommandEventArgs e)
    {
        Charter_Party_Id = Convert.ToInt32(e.CommandArgument);

        DataTable dt = oBLL_CP.GeneralRemarks(Charter_Party_Id);

        gvRemarks.DataSource = dt;
        gvRemarks.DataBind();

        string show = String.Format("showDvRemarks('dvRemarks')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showmodal", show, true);

    }




    protected void ibtnDelete_click(object sender, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            Charter_Party_Id = Convert.ToInt32(e.CommandArgument);
            oBLL_CP.Delete_Charterer(Charter_Party_Id, GetSessionUserID());
            BindGrid();
        }
        catch { }

    }

     protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {




        ddlVessel.SelectedValue = "0";
        ddlSupplierList.SelectedValue = "0";

        BindGrid();

    }
}