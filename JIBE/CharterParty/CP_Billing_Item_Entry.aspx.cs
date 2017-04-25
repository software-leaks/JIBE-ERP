using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;
using System.Text;

public partial class CP_Billing_Item_Entry2 : System.Web.UI.Page
{
    UserAccess objUA = new UserAccess();
    protected DataTable dtGridItems;
    public int CPID = 0;
    protected int Billing_Item_Id= 0;
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

            ViewState["ItemId"] = "0";
            if (Convert.ToInt32(ViewState["ItemId"]) != 0)
            {
                btnSave.Text = "Update";
                btnSaveClose.Text = "Update & Close";
            }
            
            LoadItemGroup();
            BindItems();
            BindItemDetails();
        }
    }

    protected void LoadItemGroup()
    {
        DataTable dt = objHireInv.GET_Hire_Invoice_Item_Group_ALL();
        ddlItemgroup.DataSource = dt;
        ddlItemgroup.DataTextField = "Item_Group";
        ddlItemgroup.DataValueField = "Item_Group";
        ddlItemgroup.DataBind();
        ddlItemgroup.Items.Insert(0, new ListItem("-Select-", "0"));
        dt.Dispose();
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
            btnSave.Enabled = false;
            btnSaveClose.Enabled = false;
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

    private void BindItems()
    {
        try
        {
            DataSet ds = objCP.GET_Billing_Items_Detail(UDFLib.ConvertIntegerToNull(Session["CPID"]));
            DataTable dt = ds.Tables[0];
            string[] Pkey_cols = new string[] { "Billing_Item_Id" };
            string[] Hide_cols = new string[] { "Trading_Range_Id", "CPID" };
            DataTable dt1 = PivotTable("WEF_Date", "Item_Amount", "Trading_Range_Id", Pkey_cols, Hide_cols, dt);
            if (dt1.Rows.Count > 0)
            {
                dtGridItems = dt1;
                gvBillingItems.DataSource = dt1;
                gvBillingItems.DataBind();

            }
            else
            {
                gvBillingItems.DataSource = dtGridItems;
                gvBillingItems.DataBind();

                //rgdItems.MasterTableView.Columns[9].Visible = false;

            }
            ViewState["dtGridItems"] = dtGridItems;

        }
        catch (Exception ex)
        {
            //lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
    }





    public static DataTable PivotTable(string PivotColumnName, string PivotValueColumnName, string PivotColumnOrder, string[] PrimaryKeyColumns, string[] HideColumns, DataTable dtTableToPivot)
    {
        StringBuilder sbPKs = new StringBuilder();

        DataTable dtFinalResult = new DataTable();
        DataView dvPivotColumnNames = dtTableToPivot.DefaultView.ToTable(true, new string[] { PivotColumnName, PivotColumnOrder }).DefaultView;
        dvPivotColumnNames.Sort = PivotColumnOrder;

        DataTable dtPivotPrimaryKeys = dtTableToPivot.DefaultView.ToTable(true, PrimaryKeyColumns);


        foreach (DataColumn dcol in dtTableToPivot.Columns)
        {
            if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
            {
                dtFinalResult.Columns.Add(dcol.ColumnName);

            }
        }

        foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
        {
            dtFinalResult.Columns.Add(drCol[0].ToString());
        }


        foreach (DataRow drPK in dtPivotPrimaryKeys.Rows)
        {
            DataRow drNew = dtFinalResult.NewRow();

            foreach (DataColumn dcol in dtTableToPivot.Columns)
            {
                if (dcol.ColumnName != PivotColumnName && dcol.ColumnName != PivotValueColumnName && dcol.ColumnName != PivotColumnOrder)
                {

                    sbPKs.Clear();
                    foreach (string pk in PrimaryKeyColumns)
                    {
                        sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                    }
                    sbPKs.Append(" 1=1  ");

                    DataRow[] drcoll = dtTableToPivot.Select(sbPKs.ToString());//[0][dcol.ColumnName];
                    drNew[dcol.ColumnName] = drcoll[0][dcol.ColumnName];
                }
            }

            foreach (DataRow drCol in dvPivotColumnNames.ToTable().Rows)
            {

                sbPKs.Clear();
                foreach (string pk in PrimaryKeyColumns)
                {
                    sbPKs.Append(pk + " = " + drPK[pk].ToString() + " and ");

                }




                DataRow[] drValue = dtTableToPivot.Select(sbPKs.ToString() + PivotColumnName + " = '" + drCol[0].ToString() + "' ");
                if (drValue.Length > 0)
                    drNew[drCol[0].ToString()] = drValue[0][PivotValueColumnName];
                else
                    drNew[drCol[0].ToString()] = null;
            }

            dtFinalResult.Rows.Add(drNew);
        }


        if (HideColumns != null)
        {
            foreach (string strColToremove in HideColumns)
            {
                if (dtFinalResult.Columns.IndexOf(strColToremove) > -1)
                    dtFinalResult.Columns.Remove(strColToremove);
            }
        }
        return dtFinalResult;
    }
    protected void onUpdate(object source, CommandEventArgs e)
    {
        DataTable dt = new DataTable();
        try
        {
            int Billing_Item_ID = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = (GridViewRow)((ImageButton)source).NamingContainer;
            Billing_Item_ID = Convert.ToInt32(gvBillingItems.DataKeys[row.RowIndex].Value);
            gvBillingItems.SelectedIndex = row.RowIndex;
            ViewState["ItemId"] = Billing_Item_ID;
            BindItems();
            BindItemDetails();


        }
        catch { }

    }


    public void imgUpdate_Click(object sender, EventArgs e)
    {
        ImageButton ibtnUpdate = (ImageButton)sender;
        GridViewRow row = (GridViewRow)ibtnUpdate.NamingContainer;
        int Billing_Item_ID = Convert.ToInt32(gvBillingItems.DataKeys[row.RowIndex].Value);
        gvBillingItems.SelectedIndex = row.RowIndex;
        ViewState["ItemId"] = Billing_Item_ID;
        BindItemDetails();
    }
    protected void gvBillingItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBillingItems.PageIndex = e.NewPageIndex;
        BindItems();
    }

    private void BindItemDetails()
    {
        try
        {
            DataTable dt1 = objCP.Get_Remittance_Receipt_List(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(ViewState["ItemId"])).Tables[0];
            DataTable dt2 = objCP.Get_Remittance_Receipt_List(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(ViewState["ItemId"])).Tables[1];
            if (dt1.Rows.Count > 0)
            {
                 DataRow dr = dt1.Rows[0];
                 ddlItemgroup.SelectedValue = dr["Item_group"].ToString();
                 txtItemDescription.Text = dr["Item_Description"].ToString();
                 txtBilling_Interval.Text = dr["Billing_Interval"].ToString();
                 ddlIntervalUnit.SelectedValue = dr["Billing_Interval_Unit"].ToString();
                 ddlItemRate.SelectedValue = dr["Item_Rate"].ToString(); 
            }

            if (dt2.Rows.Count > 0)
            {
                dtGridItems = dt2;
                rgdItems.DataSource = dt2;
                rgdItems.DataBind();
            }
            else
            {
                dtGridItems = GetAddTable();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();

            }
            ViewState["dtGridItems"] = dtGridItems;

        }
        catch (Exception ex)
        {
           // lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }


    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["CPID"] != null)
        {
            SaveData();
            BindItems();

        }


    }

     public void LoadHour(DropDownList ddlHH)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 24)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlHH.DataSource = lic;
        ddlHH.DataBind();

    }

    public void LoadMin(DropDownList ddlMin)
    {
        ListItemCollection lic = new ListItemCollection();
        int count = 0;
        while (count < 60)
        {
            ListItem li = new ListItem();
            if (count < 10)
            {
                li.Text = "0" + count.ToString();
                li.Value = "0" + count.ToString();
            }
            else
            {
                li.Text = count.ToString();
                li.Value = count.ToString();
            }
            lic.Add(li);
            count++;

        }
        ddlMin.DataSource = lic;
        ddlMin.DataBind();
    }


    protected string AddHourMin(string dtText, string HH, string MIN)
    {
        DateTime dt;

        dt = Convert.ToDateTime(dtText);
        int TotalMin = Convert.ToInt16(HH) * 60 + Convert.ToInt16(MIN);
        dt = dt.AddMinutes(TotalMin);

        return dt.ToString();
    }
    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
            DropDownList ddlLTGMTP = (DropDownList)(dataItem.FindControl("ddlLTGMTP"));
            DropDownList ddlHoursWEFP = (DropDownList)(dataItem.FindControl("ddlHoursWEFP"));
            DropDownList ddlMinsWEFP = (DropDownList)(dataItem.FindControl("ddlMinsWEFP"));
            TextBox txtTradingRange = (TextBox)(dataItem.FindControl("txtTradingRange"));
            TextBox txtItem_Amount = (TextBox)(dataItem.FindControl("txtItem_Amount"));
            TextBox txtPWFF = (TextBox)(dataItem.FindControl("txtPWFF"));
            LoadHour(ddlHoursWEFP);
            LoadMin(ddlMinsWEFP);
            HiddenField hdnDate = (HiddenField)(dataItem.FindControl("hdnDate"));
            if (hdnDate.Value != null && hdnDate.Value != "")
            {
                txtPWFF.Text = Convert.ToDateTime(hdnDate.Value).ToString("dd-MMM-yyyy");
                int hour = Convert.ToDateTime(hdnDate.Value).Hour;
                int Min = Convert.ToDateTime(hdnDate.Value).Minute;
                string strHour ="00";
                string strMin = "00";
                if (hour < 10)
                    strHour = "0" + hour.ToString();
                else
                    strHour = hour.ToString();
                if (Min < 10)
                    strMin = "0" + Min.ToString();
                else
                    strMin =  Min.ToString();
                ddlHoursWEFP.SelectedValue = strHour;
                ddlMinsWEFP.SelectedValue = strMin;
            }

            HiddenField hdnTPId = (HiddenField)(dataItem.FindControl("hdnTPId"));
            txtItem_Amount.Attributes.Add("onkeypress", "return numbersonly(event)");


            if (hdnTPId.Value == "0")
            {
                //ImgDelete.Visible = false;
            }

        }

    }


    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        try
        {

            dtGridItems = (DataTable)ViewState["dtGridItems"];
            int RowID = 0;

            foreach (GridDataItem item in rgdItems.MasterTableView.Items)
            {
                DropDownList ddlLTGMTP = (DropDownList)(item.FindControl("ddlLTGMTP"));
                DropDownList ddlHoursWEFP = (DropDownList)(item.FindControl("ddlHoursWEFP"));
                DropDownList ddlMinsWEFP = (DropDownList)(item.FindControl("ddlMinsWEFP"));
                TextBox txtPWFF = (TextBox)(item.FindControl("txtPWFF"));
                string WEF = "";

                if (txtPWFF.Text != "")
                {
                    WEF = AddHourMin(txtPWFF.Text, ddlHoursWEFP.SelectedValue, ddlMinsWEFP.SelectedValue);
                    dtGridItems.Rows[RowID]["Billing_Item_Amount_Id"] = ((HiddenField)item.FindControl("hdnBilling_Item_Id")).Value;
                    
                    dtGridItems.Rows[RowID]["Trading_Date"] = WEF;
                    dtGridItems.Rows[RowID]["Trading_Range_Id"] = ((HiddenField)item.FindControl("hdnTPId")).Value;
                    dtGridItems.Rows[RowID]["Date_Zone"] = ddlLTGMTP.SelectedValue;
                    dtGridItems.Rows[RowID]["Trading_Range"] = ((TextBox)item.FindControl("txtTradingRange")).Text;
                    dtGridItems.Rows[RowID]["Amount"] = ((TextBox)item.FindControl("txtItem_Amount")).Text;

                    RowID++;
                }
            }

            DataRow dr = dtGridItems.NewRow();
            dr["Amount"] = 0.00;
            dtGridItems.Rows.Add(dr);
            rgdItems.DataSource = dtGridItems;
            rgdItems.DataBind();

            ViewState["dtGridItems"] = dtGridItems;
        }
        catch (Exception ex)
        {
            string sError = ex.ToString();
        }

    }

    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Billing_Item_Amount_Id");
        dt.Columns.Add("Amount");
        dt.Columns.Add("Trading_Range_Id");
        dt.Columns.Add("Trading_Date");
        dt.Columns.Add("Trading_Range");
        dt.Columns.Add("Date_Zone");
        dt.AcceptChanges();
        return dt;
    }


    protected void onDelete(object source, CommandEventArgs e)
    {
        HiddenField lblgrdID = (rgdItems.FindControl("hdnTPId") as HiddenField);
        string[] arg = e.CommandArgument.ToString().Split(',');
        int? ItemID = UDFLib.ConvertIntegerToNull(arg[0]);
    }



    protected void ClearData()
    {
        ltmessage.Text = "";
        ddlItemgroup.SelectedValue = "0";
        txtBilling_Interval.Text = "";
        txtItemDescription.Text = "";
        ddlIntervalUnit.SelectedValue = "Day";
        ddlItemRate.SelectedValue = "Per day Prorata";
        ViewState["ItemId"] = "0";
        BindItemDetails();
        gvBillingItems.SelectedIndex = -1;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        SaveData();
       // BindBunkerDetail();
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        SaveData();
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
    }

    protected void SaveData()
    {
        ltmessage.Text = "";

        int ItemId = -1;
        int res = -1;
        int Trading_Range_Id = -1;
        try
        {
            
            int RowID = 0;
            string Item_Group = ddlItemgroup.SelectedValue;
            string Item_Description = txtItemDescription.Text; 
            int Billing_Interval = Convert.ToInt32(txtBilling_Interval.Text);
            string Billing_Interval_Unit = ddlIntervalUnit.SelectedValue;
            string Item_Rate = ddlItemRate.SelectedValue;
            DataTable dt = GetAddTable();
            foreach (GridDataItem item in rgdItems.MasterTableView.Items)
            {
                DropDownList ddlLTGMTP = (DropDownList)(item.FindControl("ddlLTGMTP"));
                DropDownList ddlHoursWEFP = (DropDownList)(item.FindControl("ddlHoursWEFP"));
                DropDownList ddlMinsWEFP = (DropDownList)(item.FindControl("ddlMinsWEFP"));
                TextBox txtPWFF = (TextBox)(item.FindControl("txtPWFF"));
                int hdnBilling_Item_Id = 0;
                int hdnTPId = 0;
                if (((HiddenField)item.FindControl("hdnBilling_Item_Id")).Value != "")
                    hdnBilling_Item_Id = Convert.ToInt32(((HiddenField)item.FindControl("hdnBilling_Item_Id")).Value);
                if (((HiddenField)item.FindControl("hdnTPId")).Value != "")
                    hdnTPId = Convert.ToInt32(((HiddenField)item.FindControl("hdnTPId")).Value);
                string WEF = "";
                DataRow dritem = dt.NewRow();
                if (txtPWFF.Text != "")
                {
                    WEF = AddHourMin(txtPWFF.Text, ddlHoursWEFP.SelectedValue, ddlMinsWEFP.SelectedValue);
                    dritem["Billing_Item_Amount_Id"] = hdnBilling_Item_Id;
                    dritem["Trading_Date"] = WEF;
                    dritem["Trading_Range_Id"] = hdnTPId;
                    dritem["Date_Zone"] = ddlLTGMTP.SelectedValue;
                    dritem["Trading_Range"] = ((TextBox)item.FindControl("txtTradingRange")).Text;
                    dritem["Amount"] = ((TextBox)item.FindControl("txtItem_Amount")).Text;
                    dt.Rows.Add(dritem);
                }
            }


            if (dt != null)
            {
                ItemId = objCP.INS_UPD_Billing_Item(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(ViewState["ItemId"]), Item_Group, Item_Description, Billing_Interval, Billing_Interval_Unit,Item_Rate, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));

                if (ItemId > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        Trading_Range_Id = objCP.INS_UPD_Trading_RangeByItem(UDFLib.ConvertIntegerToNull(dr["Trading_Range_Id"]), UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertDateToNull(dr["Trading_Date"]), dr["Trading_Range"].ToString(), dr["Date_Zone"].ToString(), UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));
                        dr["Trading_Range_Id"] = Trading_Range_Id;
                    }
                    if (dt.Columns["Trading_Date"] != null)
                        dt.Columns.Remove(dt.Columns["Trading_Date"]);
                    if (dt.Columns["Date_Zone"] != null)
                        dt.Columns.Remove(dt.Columns["Date_Zone"]);
                    if (dt.Columns["Trading_Range"] != null)
                        dt.Columns.Remove(dt.Columns["Trading_Range"]);
                    if (Trading_Range_Id == 0)
                        ltmessage.Text = "WEF date can not be duplicate!";
                    else
                    {
                        res = objCP.INS_UPD_Billing_Item_Price(UDFLib.ConvertIntegerToNull(Session["CPID"]), ItemId, dt, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));

                        
                        BindItems();
                    }
                }

            }
            else
                ltmessage.Text = "No trading range information found!";

            ClearData();
        }
        catch (Exception ex)
        {
            ltmessage.Text = ex.ToString();
        }
    }


    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        ImageButton ibtnDelete = (ImageButton)sender;
        GridViewRow row = (GridViewRow)ibtnDelete.NamingContainer;
        Billing_Item_Id = Convert.ToInt32(gvBillingItems.DataKeys[row.RowIndex].Value);
        objCP.DEL_Billing_Item(Billing_Item_Id, GetSessionUserID());
        gvBillingItems.SelectedIndex = row.RowIndex;
        ViewState["ItemId"] = Billing_Item_Id;
        BindItems();
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {

        ClearData();

    }
    protected void gvBillingItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            for (int i = 0; i < e.Row.Controls.Count; i++)
            {

                if (i == 2 || i == 1)
                    e.Row.Cells[i].Visible = false;
                var headerCell = e.Row.Controls[i] as DataControlFieldHeaderCell;
                if (headerCell.Text.Contains("Item_Group"))
                    headerCell.Text = "Item Group";
                else if (headerCell.Text.Contains("Item_Description"))
                    headerCell.Text = "Description";
                else if (headerCell.Text.Contains("Interval_Unit"))
                    headerCell.Text = "Unit";
                else if (headerCell.Text.Contains("Billing_Interval"))
                    headerCell.Text = "Billing Interval";
                else if (headerCell.Text.Contains("Item_Rate"))
                    headerCell.Text = "Item Rate";

                else if (headerCell != null)
                {
                    if (i > 5)
                        headerCell.Text = "WEF: " + headerCell.Text;

                }

            }

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {


            for (int i = 0; i < e.Row.Controls.Count; i++)
            {
                if (i==2 || i== 1)
                    e.Row.Cells[i].Visible = false;
                if (i <= 5)
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
                else
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
            }

        }
    }
}
   
