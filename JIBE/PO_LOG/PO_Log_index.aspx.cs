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

using SMS.Business.Infrastructure;
using SMS.Properties;
using SMS.Business.POLOG;


public partial class PO_LOG_PO_Log_index : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_Infra_Port objBLLPort = new BLL_Infra_Port();
    UserAccess objUA = new UserAccess();
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    MergeGridviewHeader_Info objChangeReqstMerge = new MergeGridviewHeader_Info();
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            BindType();
            Load_VesselList();
            BindSupplier();
            BindGrid();
            BindPendingPOGrid();
            //gvPO.Attributes.Add("OnSelectedIndexChanged", "OpenPOLOG();");
        }
        this.registerpostback();
    }
    public void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", 1);


        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-All-", "0"));
    }
    protected void BindPendingPOGrid()
    {
        try
        {
            DataTable dt = BLL_POLOG_Register.POLOG_Get_Pending_Approval(UDFLib.ConvertToInteger(Session["UserID"].ToString()));
            if (dt.Rows.Count > 0)
            {
                divPendingPO.Visible = true;
                gvPendingPO.DataSource = dt;
                gvPendingPO.DataBind();
            }
            else
            {
                divPendingPO.Visible = false;
                gvPendingPO.DataSource = dt;
                gvPendingPO.DataBind();
            }
        }
        catch { }
        {
        }
    }

    public void BindGrid()
    {
        try
        {
            //objChangeReqstMerge.AddMergedColumns(new int[] { 3 }, "Supplier", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 4, 5 }, "PO", "HeaderStyle-center");
            objChangeReqstMerge.AddMergedColumns(new int[] { 6, 7 }, "Invoice", "HeaderStyle-center");

            int rowcount = ucCustomPagerItems.isCountRecord;
            string InvoiceStatus = null;
            DataTable dtType = ChkType();

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            string str;
            if (chkClosePO.Checked == true)
            {
                str = "yes";
            }
            else
            {
                str = "No";
            }
            DataTable dt = BLL_POLOG_Register.POLOG_Get_PO_Search(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                     UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue),
                 UDFLib.ConvertStringToNull(ddlAccClassification.SelectedValue), UDFLib.ConvertStringToNull(ddlAccountType.SelectedValue),
                UDFLib.ConvertStringToNull(InvoiceStatus), dtType, str,
                sortbycoloumn, sortdirection
                , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

            Object sender = new Object();
            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }

            if (dt.Rows.Count > 0)
            {
                try
                {
                    gvPO.DataSource = dt;
                    gvPO.DataBind();
                    if (HdnId.Value != "")
                    {
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = "ID ='" + HdnId.Value + "'";
                        dt = dv.ToTable();
                    }
                    //this.iFrame1.Attributes["src"] = "PO_Log.aspx?SUPPLY_ID=" + dt.Rows[0]["SUPPLY_ID"].ToString() + "&POType=" + dt.Rows[0]["SType"].ToString() + "";
                    gvPO.Rows[0].BackColor = System.Drawing.Color.Yellow;
                    iFrame1.Attributes.Add("src", "PO_Log.aspx?SUPPLY_ID=" + dt.Rows[0]["SUPPLY_ID"].ToString() + "&POType=" + dt.Rows[0]["SType"].ToString() + "");
                    HdnId.Value = "";
                }
                catch
                {
                }

            }
            else
            {
                gvPO.DataSource = dt;
                gvPO.DataBind();
            }


        }
        catch { }
        {
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
        try
        {
            DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()),"PO_TYPE");

            chkType.DataSource = ds.Tables[0];
            chkType.DataTextField = "VARIABLE_NAME";
            chkType.DataValueField = "VARIABLE_CODE";
            chkType.DataBind();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                chkType.Items[i].Selected = true;
                string color = ds.Tables[0].Rows[i]["COLOR_CODE"].ToString();
                chkType.Items[i].Attributes.Add("style", "background-color: " + color + ";");
                //chkType.Attributes.Add("style", "background-color: " + color + ";");
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlPOType.DataSource = ds.Tables[1];
                ddlPOType.DataTextField = "VARIABLE_NAME";
                ddlPOType.DataValueField = "VARIABLE_CODE";
                ddlPOType.DataBind();
            }
            else
            {
                ddlPOType.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            //

            //ddlSupplier.DataSource = ds.Tables[5];
            //ddlSupplier.DataTextField = "Supplier_Name";
            //ddlSupplier.DataValueField = "Supplier_Code";
            //ddlSupplier.DataBind();
            //ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));

            ddlAccountType.DataSource = ds.Tables[2];
            ddlAccountType.DataTextField = "VARIABLE_NAME";
            ddlAccountType.DataValueField = "VARIABLE_CODE";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("-All-", "0"));

            ddlAccClassification.DataSource = ds.Tables[3];
            ddlAccClassification.DataTextField = "VARIABLE_NAME";
            ddlAccClassification.DataValueField = "VARIABLE_CODE";
            ddlAccClassification.DataBind();
            ddlAccClassification.Items.Insert(0, new ListItem("-All-", "0"));

            ddlSupplierType.DataSource = ds.Tables[10];
            ddlSupplierType.DataTextField = "VARIABLE_NAME";
            ddlSupplierType.DataValueField = "VARIABLE_CODE";
            ddlSupplierType.DataBind();
            ddlSupplierType.SelectedValue = "SUPPLIER";
            //ddlSupplierType.Items.Insert(0, new ListItem("-All-", "0"));
        }
        catch { }
        {
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
    protected void gvPO_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindGrid();

    }

    protected void btnGet_Click(object sender, EventArgs e)
    {

        BindGrid();
        BindPendingPOGrid();
        BindType();
        //BindPOType();
    }
    protected void BindPOType()
    {
        DataSet ds = BLL_POLOG_Register.POLOG_Get_Type(UDFLib.ConvertToInteger(Session["UserID"].ToString()), "PO_TYPE");

        chkType.DataSource = ds.Tables[0];
        chkType.DataTextField = "VARIABLE_NAME";
        chkType.DataValueField = "VARIABLE_CODE";
        chkType.DataBind();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string color = ds.Tables[0].Rows[i]["COLOR_CODE"].ToString();
            chkType.Items[i].Attributes.Add("style", "background-color: " + color + ";");
            //chkType.Attributes.Add("style", "background-color: " + color + ";");
        }
    }
    //protected void btnNewPO_Click(object sender, EventArgs e)
    //{
    //    int POID = 0;
    //    string POType = ddlPOType.SelectedValue;
    //    Response.Redirect("../ASL/PO_LOG.aspx?PO_ID=" + POID + "'&POType=" + POType + "'");
    //}
    protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objChangeReqstMerge);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImageButton = (ImageButton)e.Row.FindControl("ImgView");
            Button InvoiceButton = (Button)e.Row.FindControl("btnInvoice");
            Button DeliveryButton = (Button)e.Row.FindControl("btnDelivery");
            Image ImgStatus = (Image)e.Row.FindControl("ImgStatus");
            //LinkButton lblOffice = (LinkButton)e.Row.FindControl("lblOffice");
            Button btnSType = (Button)e.Row.FindControl("btnSType");
            Label lblInvoice_Value = (Label)e.Row.FindControl("lblInvoice");
            Label lblCurrency = (Label)e.Row.FindControl("lblCurrency");
            if (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Invoice_Amount")) > Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalAmount")))
            {
                lblInvoice_Value.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblInvoice_Value.ForeColor = System.Drawing.Color.Black;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency").ToString() != DataBinder.Eval(e.Row.DataItem, "Line_Currency").ToString())
            {
                if (DataBinder.Eval(e.Row.DataItem, "Supplier_Currency") != null)
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Violet;
                }
            }
            else
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.White;
            }
            if (DataBinder.Eval(e.Row.DataItem, "SType").ToString() == "")
            {
                btnSType.Visible = false;
            }
            if (DataBinder.Eval(e.Row.DataItem, "Line_Status").ToString() == "APPROVED")
            {
                InvoiceButton.Visible = true;
                DeliveryButton.Visible = true;
                ImgStatus.Visible = true;
                ImgStatus.ImageUrl = "~/Images/Green_Check.bmp";
            }
            else if (DataBinder.Eval(e.Row.DataItem, "Line_Status").ToString() == "CANCELLED")
            {
                ImgStatus.Visible = true;
                ImgStatus.ImageUrl = "~/Images/Delete.png";
            }
            else if (DataBinder.Eval(e.Row.DataItem, "PO_Closed_By").ToString() != "")
            {
                ImgStatus.Visible = true;
                ImgStatus.ImageUrl = "~/Images/locked.bmp";
            }
            else
            {
                ImgStatus.Visible = false;
            }

            string ColorCode = DataBinder.Eval(e.Row.DataItem, "COLOR_CODE").ToString();
            System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(ColorCode);
            if (col.Name == "White")
            {
                e.Row.Cells[1].BackColor = col;
            }
            else
            {
                e.Row.Cells[1].BackColor = col;
                //lblOffice.ForeColor = System.Drawing.Color.Black;
            }
           
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPO, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
        }



    }
    protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSupplier();
        BindType();
    }
    protected void BindSupplier()
    {
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Supplier(ddlSupplierType.SelectedValue);

        if (dt.Rows.Count > 0)
        {
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataTextField = "Supplier_Name";
            ddlSupplier.DataValueField = "Supplier_Code";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));
        }
        else
        {
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataTextField = "Supplier_Name";
            ddlSupplier.DataValueField = "Supplier_Code";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-All-", "0"));
        }
    }

    //protected void LinkView_Click(object sender, EventArgs e)
    //{

    //    GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
    //    LinkButton img = ((LinkButton)sender);
    //    HdnId.Value = img.CommandArgument.ToString();
    //    gvPO.SelectedIndex = row.RowIndex;
    //    BindGrid();
    //    gvPO.Rows[0].BackColor = System.Drawing.Color.White;
    //    gvPO.Rows[gvPO.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
    //    BindType();
    //}

    protected void ButtonView_Click(object sender, EventArgs e)
    {

        GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
        Button img = ((Button)sender);
        HdnId.Value = "";
        string[] args = img.CommandArgument.ToString().Split(';');
        gvPO.SelectedIndex = row.RowIndex;
        BindGrid();
        if (img.Text == "I")
        {
            iFrame1.Attributes.Add("src", "PO_Log_Invoice_Listing.aspx?SUPPLY_ID=" + args[0] + "&POType=" + args[1] + "");
        }
        else
        {
            iFrame1.Attributes.Add("src", "PO_Log_Delivery_Listing.aspx?SUPPLY_ID=" + args[0] + "&POType=" + args[1] + "");
        }
        gvPO.Rows[0].BackColor = System.Drawing.Color.White;
        gvPO.Rows[gvPO.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
        BindType();
    }

    protected void ImageView_Click(object sender, ImageClickEventArgs e)
    {

        GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
        ImageButton img = ((ImageButton)sender);
        HdnId.Value = img.CommandArgument.ToString();
        gvPO.SelectedIndex = row.RowIndex;
        BindGrid();
        gvPO.Rows[0].BackColor = System.Drawing.Color.White;
        gvPO.Rows[gvPO.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
        BindType();
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PO_Log_index.aspx");
    }

    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        
        string InvoiceStatus = null;
        DataTable dtType = ChkType();
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        string str;
        if (chkClosePO.Checked == true)
        {
            str = "yes";
        }
        else
        {
            str = "No";
        }
        DataTable dt = BLL_POLOG_Register.POLOG_Get_PO_Search(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, UDFLib.ConvertStringToNull(ddlSupplier.SelectedValue),
                                 UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue),
             UDFLib.ConvertStringToNull(ddlAccClassification.SelectedValue), UDFLib.ConvertStringToNull(ddlAccountType.SelectedValue),
            UDFLib.ConvertStringToNull(InvoiceStatus), dtType, str,
            sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);



        string[] HeaderCaptions = { "Created Date", "Office Ref. Code", "Supplier Name", "Line Currency", "Total Amount", "Invoice Amount", "Invoice Count" };
        string[] DataColumnsName = { "Created_Date", "Office_Ref_Code", "Supplier Name", "Line Currency", "TotalAmount", "Invoice_Amount", "Invoice_Count" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "PO LOG", "PO LOG", "");

    }
    protected void registerpostback()
    {
        foreach (GridViewRow row in gvPO.Rows)
        {
            //Label lnkFull = row.FindControl("lblOffice") as Label;
            //ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkFull);

            Button btn = row.FindControl("btnInvoice") as Button;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btn);
            Button btn2 = row.FindControl("btnDelivery") as Button;
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btn2);
        }
    }
    protected void gvPO_SelectedIndexChanged(object sender, EventArgs e)
    {


        gvPO.SelectedIndex = gvPO.SelectedRow.RowIndex;
        //gvPO.SelectedRow

        //Label lnkFull = gvPO.Rows[gvPO.SelectedIndex].FindControl("lblOffice") as Label;

        HdnId.Value = gvPO.DataKeys[gvPO.SelectedIndex].Values[0].ToString();
        //GridViewRow row = (GridViewRow)(gvPO.SelectedRow);
        iFrame1.Attributes.Add("src", "PO_Log.aspx?SUPPLY_ID=" + HdnId.Value.ToString() + "");
        BindGrid();
        gvPO.Rows[0].BackColor = System.Drawing.Color.White;
        gvPO.Rows[gvPO.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
        BindType();


    }
    protected void gvPendingPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        string dataItem = gvPendingPO.MasterTableView.Items.ToString();

        //LinkButton lnkFull = gvPO.Rows[gvPO.SelectedIndex].FindControl("lblOffice") as LinkButton;

        //HdnId.Value = lnkFull.CommandArgument.ToString();
        //GridViewRow row = (GridViewRow)(gvPO.SelectedRow);
        iFrame1.Attributes.Add("src", "PO_Log.aspx?SUPPLY_ID=" + dataItem.ToString() + "");
        BindGrid();
        gvPO.Rows[0].BackColor = System.Drawing.Color.White;
        gvPO.Rows[gvPO.SelectedIndex].BackColor = System.Drawing.Color.Yellow;
        BindType();
    }
}