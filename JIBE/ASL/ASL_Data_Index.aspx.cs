using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.ASL;


public partial class ASL_Data_Index : System.Web.UI.Page
{

    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    BLL_Infra_AirPort objBLLAirPort = new BLL_Infra_AirPort();
    BLL_Infra_Country objBLLCountry = new BLL_Infra_Country();
    UserAccess objUA = new UserAccess();
    public string Type = null;
    public string OperationMode = "";
    public string CurrStatus = null;
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    MergeGridviewHeader_Info objContractList = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "myFunction();", true);
         UserAccessValidation();
        //UserAccessTypeValidation();

        objContractList.AddMergedColumns(new int[] { 10, 11 }, "Evaluation", "HeaderStyle-center");
        objContractList.AddMergedColumns(new int[] { 12, 13, 14 }, "Supplier", "HeaderStyle-center");
        objContractList.AddMergedColumns(new int[] { 15, 16 }, "Change", "HeaderStyle-center");
        objContractList.AddMergedColumns(new int[] { 17, 18, 19, 20, 21, 22 }, "Invoice/PO", "HeaderStyle-center");
        this.Form.DefaultButton = this.btnGet.UniqueID;
        if (!IsPostBack)
        {
            if (Request.QueryString["Supplier_Code"] != null)
            {
                txtfilter.Text = Request.QueryString["Supplier_Code"].ToString();
            }
            else
            {
                txtfilter.Text = "";
            }
            chkApproved.Checked = true;
            chkBlack.Checked = true;
            chkCond.Checked = true;
            chkexp.Checked = true;
            chkUnreg.Checked = true;
            BindType();
            BindPort();
            BindScope();
            BindEvaluationStatus();
            BindEvaluationGrid();
            BindChangeRequestGrid();
            BindGrid();
        }
    }
    protected void UserAccessTypeValidation()
    {
        int CurrentUserID = GetSessionUserID();
        //string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_TypeManagement objType = new BLL_TypeManagement();
        string Variable_Type = "Supplier_Type";
        string Variable_Code = "";
        string Approver_Type = null;
        objUA = objType.Get_UserTypeAccess(CurrentUserID, Variable_Type, Variable_Code, Approver_Type);
    }
    protected void BindChangeRequestGrid()
    {
        try
        {

            DataTable dtType = ChkType();
            ChkStatus();
            //DataSet ds = BLL_ASL_Supplier.Get_ChangeRequest_Search(UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertStringToNull(txtfilter.Text));
            DataSet ds = BLL_ASL_Supplier.Get_Pending_CR_List(UDFLib.ConvertToInteger(GetSessionUserID()), txtfilter.Text != "" ? txtfilter.Text : null
          , UDFLib.ConvertIntegerToNull(ddlSupplyPort.SelectedValue), UDFLib.ConvertStringToNull(ddlSupplyType.SelectedValue),
          UDFLib.ConvertStringToNull(ddlEvaluationStatus.SelectedValue), dtType, CurrStatus, chkCredit.Checked ? 1 : 0, txtSupplierDesc.Text.Trim());
            if (ds.Tables[0].Rows.Count > 0)
            {
                divChnageRequest.Visible = true;
                divCR.Visible = true;
                gvChangeRequest.DataSource = ds.Tables[0];
                gvChangeRequest.DataBind();
            }
            else
            {
                divChnageRequest.Visible = false;
                divCR.Visible = false;
                gvChangeRequest.DataSource = ds.Tables[0];
                gvChangeRequest.DataBind();
            }
        }
        catch { }
        {

        }
    }
    protected void BindEvaluationGrid()
    {
        try
        {

            DataTable dtType = ChkType();
            ChkStatus();
            DataTable dt = BLL_ASL_Supplier.Get_Supplier_Eval_Search(UDFLib.ConvertToInteger(GetSessionUserID()), txtfilter.Text != "" ? txtfilter.Text : null
               , UDFLib.ConvertIntegerToNull(ddlSupplyPort.SelectedValue), UDFLib.ConvertStringToNull(ddlSupplyType.SelectedValue),
               UDFLib.ConvertStringToNull(ddlEvaluationStatus.SelectedValue), dtType, CurrStatus, chkCredit.Checked ? 1 : 0, txtSupplierDesc.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                divEvaluation.Visible = true;
                //divEval.Visible = true;
                gvEvaluation.DataSource = dt;
                gvEvaluation.DataBind();
            }
            else
            {
                divEvaluation.Visible = false;
                //divEval.Visible = false;
                gvEvaluation.DataSource = dt;
                gvEvaluation.DataBind();
            }
        }
        catch { }
        {
        }
    }
    protected void BindEvaluationStatus()
    {
        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter_Evaluation(15, "", UDFLib.ConvertToInteger(GetSessionUserID()));

        ddlEvaluationStatus.DataSource = dt;
        ddlEvaluationStatus.DataTextField = "Name";
        ddlEvaluationStatus.DataValueField = "Description";
        ddlEvaluationStatus.DataBind();
        ddlEvaluationStatus.Items.Insert(0, new ListItem("-All-", "0"));

    }
    protected void BindPort()
    {
        DataTable dt = objBLLPort.Get_PortList_Mini();

        ddlSupplyPort.DataSource = dt;
        ddlSupplyPort.DataTextField = "Port_Name";
        ddlSupplyPort.DataValueField = "Port_ID";
        ddlSupplyPort.DataBind();
        ddlSupplyPort.Items.Insert(0, new ListItem("-All Ports-", "0"));
    }
    protected void BindScope()
    {
        DataSet ds = BLL_ASL_Supplier.Get_Supplier_Scope(UDFLib.ConvertStringToNull(Request.QueryString["Supp_ID"]));
        ddlSupplyType.DataSource = ds;
        ddlSupplyType.DataTextField = "Scope_Name";
        ddlSupplyType.DataValueField = "Scope_ID";
        ddlSupplyType.DataBind();
        ddlSupplyType.Items.Insert(0, new ListItem("-All Scopes-", "0"));
    }

    public void BindGrid()
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            DataTable dtType = ChkType();
            ChkStatus();


            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            DataTable dt = BLL_ASL_Supplier.Get_Supplier_Search(txtfilter.Text != "" ? txtfilter.Text : null
                , UDFLib.ConvertIntegerToNull(ddlSupplyPort.SelectedValue), UDFLib.ConvertStringToNull(ddlSupplyType.SelectedValue),
                UDFLib.ConvertStringToNull(ddlEvaluationStatus.SelectedValue), dtType, CurrStatus, chkCredit.Checked ? 1 : 0, txtSupplierDesc.Text.Trim()
                , sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                gvSupplier.DataSource = dt;
                gvSupplier.DataBind();
            }
            else
            {
                gvSupplier.DataSource = dt;
                gvSupplier.DataBind();
            }
        }
        catch { }
        {
        }

    }
    protected void ChkStatus()
    {
        CurrStatus = "";
        if (chkApproved.Checked == true)
        {
            CurrStatus = "Approved";
        }
        if (chkCond.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Conditional";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Conditional";
            }
        }
        if (chkBlack.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Blacklist";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Blacklist";
            }
        }
        if (chkUnreg.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Unregistered";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Unregistered";
            }
        }
        if (chkexp.Checked == true)
        {
            if (CurrStatus == "")
            {
                CurrStatus = "Expired";
            }
            else
            {
                CurrStatus = CurrStatus + "," + "Expired";
            }
        }

    }
    protected DataTable ChkType()
    {

        DataTable dtType = new DataTable();
        dtType.Columns.Add("PKID");
        dtType.Columns.Add("FKID");
        dtType.Columns.Add("Value");
        foreach (ListItem chkitem in chkType.Items)
        {

            DataRow dr = dtType.NewRow();
            if (chkitem.Selected == true)
            {
                dr["FKID"] = chkitem.Selected == true ? 1 : 0;
                dr["Value"] = chkitem.Value;
                dtType.Rows.Add(dr);
            }

        }

        return dtType;


    }
    protected void BindType()
    {

        DataTable dt = BLL_ASL_Supplier.Get_ASL_System_Parameter(1, "", UDFLib.ConvertToInteger(GetSessionUserID()));

        chkType.DataSource = dt;
        chkType.DataTextField = "Name";
        chkType.DataValueField = "Description";
        chkType.DataBind();
        //foreach (ListItem chkitem in chkType.Items)
        //{
        //    chkitem.Selected = true;
        //}
        foreach (ListItem chkitem in chkType.Items)
        {
            if (chkitem.Value == "AGENT")
            {
                chkitem.Selected = true;
            }
            if (chkitem.Value == "SUPPLIER")
            {
                chkitem.Selected = true;
            }
            if (chkitem.Value == "BROKER")
            {
                chkitem.Selected = true;
            }
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

        if (objUA.Add == 0)
        {
            btnNewSupplier0.Enabled = false;
        }
        else
        {
            btnNewSupplier0.Enabled = true;
        }
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        // btnsave.Visible = false;

        //if (objUA.Delete == 1) uaDeleteFlage = true;


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
    protected void gvSupplier_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
        //string rowStyle = "this.style.backgroundColor= 'yellow'";
        //string rowStyleClickedTwice ="this.style.backgroundColor = 'blue'";
        string rowID = String.Empty; 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string ApproveBy = null;
            //string Proposed_By = null;
            //ImageButton ImgReworkButton = (ImageButton)e.Row.FindControl("ImgRework");
            //LinkButton lblSupplierName = (LinkButton)e.Row.FindControl("lblSupplierName");
            ImageButton ImageButton = (ImageButton)e.Row.FindControl("ImgView");

            LinkButton lblasl_status = (LinkButton)e.Row.FindControl("lblASL_Status");
            LinkButton lblApprove_By = (LinkButton)e.Row.FindControl("lblApproveby");
            LinkButton StatusValid = (LinkButton)e.Row.FindControl("lblStatusValid");
            LinkButton SCode = (LinkButton)e.Row.FindControl("lblSCode");
            string str = lblasl_status.Text;

            if (str == "Blacklist")
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;

                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                SCode.ForeColor = System.Drawing.Color.White;
                lblasl_status.ForeColor = System.Drawing.Color.White;
                StatusValid.ForeColor = System.Drawing.Color.White;
            }
             else if (str == "Expired")
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.Black;
                SCode.ForeColor = System.Drawing.Color.Black;
                lblasl_status.ForeColor = System.Drawing.Color.Black;
                StatusValid.ForeColor = System.Drawing.Color.Black;

            }
            else if (str == "Approved")
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[4].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                SCode.ForeColor = System.Drawing.Color.White;
                lblasl_status.ForeColor = System.Drawing.Color.White;
                StatusValid.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                SCode.ForeColor = System.Drawing.Color.Black;
                lblasl_status.ForeColor = System.Drawing.Color.Black;
                StatusValid.ForeColor = System.Drawing.Color.Black;
            }
            //string Type = "1";
            //e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#90D1D4';";
            //e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSupplier, "Select$" + e.Row.RowIndex);
            //e.Row.ToolTip = "Click to select this row.";
            //ImageButton.Attributes.Add("onclick", "javascript:window.open('../ASL/ASL_General_Data.aspx?Supp_ID=" + DataBinder.Eval(e.Row.DataItem, "ID").ToString() + "&Proposed_By=" + Proposed_By + "&Type=" + Type + "&Approved_By=" + ApproveBy + "');return false;");
            //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSupplier, "Select$" + e.Row.RowIndex);
            //e.Row.Attributes["style"] = "cursor:pointer";
            //e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "')");
            //e.Row.Attributes.Add("onClick", "javascript:ChangeColor('" + "gvSupplier','" + (e.Row.RowIndex + 1).ToString() + "')"); 
        }


    }
    //protected void gvChangeRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //if (e.Row.RowType == DataControlRowType.DataRow)
    //{
    //    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ForApproval").ToString()) != GetSessionUserID())
    //    {
    //        lblEvaluation.Text = "ASL Evaluation Pending List";
    //    }
    //    if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ForFinalApproval").ToString()) != GetSessionUserID())
    //    {
    //        lblEvaluation.Text = "ASL Evaluation Pending List";
    //    }
    //}
    //}

    //protected void gvEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ForApproval").ToString()) != GetSessionUserID())
    //        {
    //            lblEvaluation.Text = "ASL Evaluation Pending List";
    //        }
    //        if (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ForFinalApproval").ToString()) != GetSessionUserID())
    //        {
    //            lblEvaluation.Text = "ASL Evaluation Pending List";
    //        }
    //    }
    //}
    protected void btnGet_Click(object sender, EventArgs e)
    {
        BindChangeRequestGrid();
        BindEvaluationGrid();
        BindGrid();
    }


    //protected void gvEvaluation_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        string[] arg = e.CommandArgument.ToString().Split(',');
    //        Session["EvalID"] = UDFLib.ConvertToInteger(arg[1]);
    //        Session["Supp_ID"] = UDFLib.ConvertToInteger(arg[0]);
    //        string EvalID = UDFLib.ConvertStringToNull(arg[1]);
    //        string Supp_ID = UDFLib.ConvertStringToNull(arg[0]);
    //        string Type = "1";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_General_Data.aspx?Supp_ID=" + Supp_ID + "&Type=" + Type + "&EvalID=" + EvalID + "', '_blank');", true);

    //    }
    //}


    //protected void gvChangeRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "Select")
    //    {
    //        string[] arg = e.CommandArgument.ToString().Split(',');
    //        Session["Supplier_Code"] = UDFLib.ConvertToInteger(arg[0]);
    //        string Supplier_Code = UDFLib.ConvertStringToNull(arg[0]);
    //        string Type = "0";
    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../ASL/ASL_General_Data.aspx?Supp_ID=" + Supplier_Code + "&Type=" + Type + "', '_blank');", true);

    //    }
    //}

    protected void gvSupplier_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objContractList);

            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
            ViewState["DynamicHeaderCSS"] = "HeaderStyle-css-2";
        }
    
    }

    protected void ImageRefresh_Click(object sender, EventArgs e)
    {
        ddlEvaluationStatus.SelectedIndex = 0;
        ddlSupplyPort.SelectedIndex = 0;
        ddlSupplyType.SelectedIndex = 0;
        txtfilter.Text = null;
        txtSupplierDesc.Text = null;
        chkApproved.Checked = true;
        chkBlack.Checked = true;
        chkCond.Checked = true;
        chkexp.Checked = true;
        chkUnreg.Checked = true;
        chkCredit.Checked = false;
        BindType();
        BindChangeRequestGrid();
        BindEvaluationGrid();
        BindGrid();
    }
   
    
    protected void gvEvaluation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEvaluation.PageIndex = e.NewPageIndex;
        BindEvaluationGrid();
    }

    protected void gvChangeRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvChangeRequest.PageIndex = e.NewPageIndex;
        BindChangeRequestGrid();
    }
    protected void btnInvoiceStatus_Click(object s, CommandEventArgs e)
    {

        string OCA_URL = null;
        if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
        {
            OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
        }
        string PassString = e.CommandArgument.ToString();
        string OCA_URL1 = OCA_URL + "/PO_LOG/Supplier_Online_Invoice_Status_V2.asp?P=" + PassString + "";

        ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);

    }
    protected void gvEvaluation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSupplier, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
           // e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "')");
        }
    }
    protected void gvChangeRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvSupplier, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
            //e.Row.Attributes.Add("onclick", "javascript:ChangeRowColor('" + e.Row.ClientID + "')");
        }
    }
   
    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
        if (e.CommandName == "View")
        {
            LinkButton lnkView = (LinkButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((LinkButton)lnkView).NamingContainer;
            //int index = Convert.ToInt32(e.CommandArgument);
           
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");               
            }

            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string msg2 = String.Format("OpenASLScreen('"+suppliercode+"');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
       
        if (e.CommandName == "ImgView")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenASLScreen('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgREGD")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenRegisteredData('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            //string OCA_URL1 = "PO_LOG/ASL_Data_Entry.aspx?P=" + PassString + "";

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);
        }
        if (e.CommandName == "ImgEvaluateN")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenScreenEvalaution('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgEvaluateL")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenScreenEHistory('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgSupplierD")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenScreenDocument('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgSupplierR")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenScreenRemarks('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgEmail")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenScreenEmail('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgChangeR")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string StatusType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[4]);
            string msg2 = String.Format("OpenScreenRequest('" + suppliercode + "','" + ProposedType + "','" + StatusType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgChangeH")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string msg2 = String.Format("OpenScreenCHistory('" + suppliercode + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgInvoice")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string PassString = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[3]);
            //string msg2 = String.Format("OpenASLScreen('" + suppliercode + "','" + ProposedType + "');");
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
            string OCA_URL = null;
            if (!Request.Url.AbsoluteUri.Contains(ConfigurationManager.AppSettings["OCA_APP_URL"]))
            {
                OCA_URL = ConfigurationManager.AppSettings["OCA_APP_URL"];
            }
            //string PassString = e.CommandArgument.ToString();
            string OCA_URL1 = OCA_URL + "/PO_LOG/Supplier_Online_Invoice_Status_V2.asp?P=" + PassString + "";

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "javascript:window.open('" + OCA_URL1 + "');", true);
        }
        if (e.CommandName == "ImgPayment")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string Register_Name = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            Session["Register_Name"] = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            string msg2 = String.Format("OpenScreenPayment('" + suppliercode + "','" + Register_Name + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgInvoicePO")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string Register_Name = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            Session["Register_Name"] = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            string msg2 = String.Format("OpenScreenPOInvoice('" + suppliercode + "','" + Register_Name + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgInvoiceWIP")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string Register_Name = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            Session["Register_Name"] = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            string msg2 = String.Format("OpenScreenInvoiceWIP('" + suppliercode + "','" + Register_Name + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgOutstandings")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string Register_Name = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            Session["Register_Name"] = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            string msg2 = String.Format("OpenScreenOutStanding('" + suppliercode + "','" + Register_Name + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgStatistics")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string Register_Name = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            Session["Register_Name"] = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            string msg2 = String.Format("OpenScreenStatistics('" + suppliercode + "','" + Register_Name + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
        if (e.CommandName == "ImgTransaction")
        {
            ImageButton lnkView = (ImageButton)e.CommandSource;
            GridViewRow grdrow = (GridViewRow)((ImageButton)lnkView).NamingContainer;
            foreach (GridViewRow gr in gvSupplier.Rows)
            {
                gr.BackColor = ColorTranslator.FromHtml("#FFFFFF");
            }
            GridViewRow gvRow = gvSupplier.Rows[grdrow.RowIndex];
            gvRow.BackColor = ColorTranslator.FromHtml("#abcdef");
            string suppliercode = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[0]);
            string ProposedType = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[1]);
            string Register_Name = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            Session["Register_Name"] = Convert.ToString(gvSupplier.DataKeys[grdrow.RowIndex].Values[2]);
            string msg2 = String.Format("OpenTransactionLog('" + suppliercode + "','" + Register_Name + "','" + ProposedType + "');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
    }
}