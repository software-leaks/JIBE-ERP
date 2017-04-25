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
using SMS.Properties;
using System.Web.UI.HtmlControls;

public partial class ASL_ASL_Supplier_Remarks : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    UserAccess objUAType = new UserAccess();
    public string AmendType = null;
    public string GeneralType = null;
    public string GreenType = null;
    public string YellowType = null;
    public string RedType = null;
    public Boolean uaEditFlag = true;
    public Boolean uaDeleteFlage = true;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            //UserAccessValidation();
            UserAccessTypeValidation();
            btnUpdate.Visible = false;
            ViewState["ReturnSupplierID"] = 0;
            BindRemarksGrid();
        }
    }
    /// <summary>
    /// Checking Access Right
    /// </summary>

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
        {
            pnlRemarks.Visible = false;
            lblMsg.Text = "You don't have sufficient previlege to access the requested information.";
        }
        else
        {
            pnlRemarks.Visible = true;
        }

        if (objUA.Add == 0)
        {
            btnRemarks.Visible = false;
        }
        else
        {
            btnRemarks.Visible = true;
        }
        if (objUA.Edit == 1)
            uaEditFlag = true;
            //btnUpdate.Visible = true;
        //else
            // btnsave.Visible = false;

            if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private string GetSessionSupplierType()
    {
        if (Session["Supplier_Type"] != null)
            return Session["Supplier_Type"].ToString();
        else
            return null;
    }
    protected void UserAccessTypeValidation()
    {
        int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_TypeManagement objType = new BLL_TypeManagement();
        string Variable_Type = "Supplier_Type";
        string Approver_Type = null;
        objUAType = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, GetSessionSupplierType(), Approver_Type);


        if (objUAType.Add == 0)
        {
            btnRemarks.Visible = false;
        }
        else
        {
            btnRemarks.Visible = true;
        }
        if (objUAType.Edit == 1)
            uaEditFlag = true;
        btnUpdate.Visible = true;
        //else
        // btnsave.Visible = false;

        if (objUAType.Delete == 1) uaDeleteFlage = true;

    }
    protected void BindRemarksGrid()
    {
        string SuppID = UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"]);
        //int? SuppID = 2737;
        DataTable dt = BLL_ASL_Supplier.Get_Supplier_Remarks(SuppID);
        gvRemarks.DataSource = dt;
        gvRemarks.DataBind();

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    public string GetSuppID()
    {
        try
        {
           
            if (Request.QueryString["Supp_ID"] != null)
            {
                return Request.QueryString["Supp_ID"].ToString();
            }

            else
                return "0";
        }
        catch { return "0"; }
    }
    protected void btnRemarks_Click(object sender, EventArgs e)
    {
        

        ChkType();
        //int RemarksID = 0;
        string SuppID = UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"]);
        string text = txtRemarks.Text;
        text = text.Replace("\r\n", "<br/>");
        //System.Web.HttpUtility.HtmlEncode(txtRemarks.Text)
        int RetValue = BLL_ASL_Supplier.Supplier_Remarks_Insert(UDFLib.ConvertToInteger(ViewState["ReturnSupplierID"]), UDFLib.ConvertStringToNull(SuppID), AmendType, GeneralType, GreenType, YellowType, RedType, System.Web.HttpUtility.HtmlEncode(txtRemarks.Text),
              UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarksGrid();
        ClearControl();
    }
    protected void ClearControl()
    {
        txtRemarks.Text = "";
        chkChange.Checked = false;
        chkGeneral.Checked = false;
        chkGood.Checked = false;
        chkRed.Checked = false;
        chkWarning.Checked = false;
        ViewState["ReturnSupplierID"] = 0;
        btnRemarks.Text = "Add Remarks";

    }
    protected void ChkType()
    {
        if (chkChange.Checked == true)
        {
            AmendType = "Amendments";//Amendments
        }
        if (chkGeneral.Checked == true)
        {
            GeneralType = "General";//General
        }
        if (chkGood.Checked == true)
        {
            GreenType = "Green Card";//Green Card
        }
        if (chkWarning.Checked == true)
        {

           YellowType = "Yellow Card";//Yellow Card
          
        }
        if (chkRed.Checked == true)
        {

            RedType = "Red Card";//Red Card
            
        }
    }

    protected void lbtnEdit_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        ViewState["ReturnSupplierID"] = UDFLib.ConvertIntegerToNull(arg[0]);
        DataTable dt = new DataTable();
        dt = BLL_ASL_Supplier.Get_Supplier_Remarks_List(Convert.ToInt32(RemarksID));
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtRemarks.Text = dr["REMARKS"].ToString();
            AmendType = dr["AmendType"].ToString();
            GeneralType = dr["GeneralType"].ToString();
            GreenType = dr["GreenType"].ToString();
            YellowType = dr["YellowType"].ToString();
            RedType = dr["RedType"].ToString();
            if(AmendType !="")
            {
                chkChange.Checked = true;
            }
            if (GeneralType != "")
            {
                chkGeneral.Checked = true;
            }
            if (GreenType != "")
            {
                chkGood.Checked = true;
            }
            if (YellowType != "")
            {
                chkWarning.Checked = true;
            }
            if (RedType != "")
            {
                chkRed.Checked = true;
            }
            btnRemarks.Text = "Edit Remarks";
        }
       
        

    }
    protected void lbtnDelete_Click(object s, CommandEventArgs e)
    {
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? RemarksID = UDFLib.ConvertIntegerToNull(arg[0]);
        
        int RetValue = BLL_ASL_Supplier.Delete_Supplier_Remarks(RemarksID, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarksGrid();

    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int? SuppID = UDFLib.ConvertIntegerToNull(Request.QueryString["Supp_ID"]);
        int RetValue = BLL_ASL_Supplier.Supplier_Remarks_Insert(UDFLib.ConvertToInteger(ViewState["ReturnSupplierID"]), UDFLib.ConvertStringToNull(SuppID), AmendType, GeneralType, GreenType, YellowType, RedType, txtRemarks.Text,
               UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        BindRemarksGrid();
        ClearControl();
    }
    protected void gvRemarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton EditButton = (ImageButton)e.Row.FindControl("lbtnEdit");
            ImageButton DeleteButton = (ImageButton)e.Row.FindControl("lbtnDelete");

            ImageButton AmendButton = (ImageButton)e.Row.FindControl("ImgAmend");
            ImageButton GeneralButton = (ImageButton)e.Row.FindControl("imgGeneral");
            ImageButton WarningButton = (ImageButton)e.Row.FindControl("imgWarning");
            ImageButton RedButton = (ImageButton)e.Row.FindControl("imageRed");
            Label lblRemarks = (Label)e.Row.FindControl("lblRemarks");

            string Remarks = DataBinder.Eval(e.Row.DataItem, "REMRAKS").ToString();
              Remarks = Remarks.Replace("\n", "<br />");
             lblRemarks.Text = Remarks;

            if (DataBinder.Eval(e.Row.DataItem, "GeneralType").ToString() != "")
            {
                GeneralButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "AmendType").ToString() != "")
            {
                AmendButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "YellowType").ToString() != "")
            {
                WarningButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "RedType").ToString() != "")
            {
                RedButton.Visible = true;
            }
            if (DataBinder.Eval(e.Row.DataItem, "RType").ToString() == "PO")
            {
                EditButton.Visible = false;
                DeleteButton.Visible = false;
            }
        }
    }
}