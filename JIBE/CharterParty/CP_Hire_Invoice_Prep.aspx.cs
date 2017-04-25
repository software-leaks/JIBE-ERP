using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Drawing;

using Telerik.Web.UI;

//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.CP;
using SMS.Properties;

public partial class CP_Hire_Invoice_Prep : System.Web.UI.Page
{
    protected DataTable dtGridItems;
    UserAccess objUA = new UserAccess();
    public int Inv_ID = 0;
    public int CPID = 0;
    public bool pendingApprove = true;
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
            if (Session["CPID"] != null )
            {
                CPID = Convert.ToInt32(Session["CPID"]);
                BindStatus();
                BindInvoices();
                BindBillingItems();
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
        if (chkStatusList.Items.FindByText("APPROVED") != null)
            chkStatusList.Items.FindByText("APPROVED").Selected = true;
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

    //protected void BindInvoices()
    //{
    //    DataTable dt = objHireInv.Get_Hire_InvPrep(UDFLib.ConvertIntegerToNull(Session["CPID"]));

    //    gvHireInvoices.DataSource = dt;
    //    gvHireInvoices.DataBind();
    //    dt.Dispose();
    //}


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

        if (count == 0)
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


    protected void ddlItemgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlItemgroup = (DropDownList)sender;
            GridDataItem item = (GridDataItem)ddlItemgroup.NamingContainer;
            TextBox txtSrno = (TextBox)item.FindControl("txtSrno");
            DataTable dtALL = objHireInv.GET_Hire_Invoice_Item_Group_ALL();
            DataTable selectedTable = dtALL.AsEnumerable()
                                .Where(r => r.Field<string>("Item_Group") == ddlItemgroup.SelectedItem.Text)
                                .CopyToDataTable();
            txtSrno.Text = selectedTable.Rows[0]["Sort_Order"].ToString();
        }
        catch
        {
        }
    }
    

    protected void ibtnView_Click( object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            Inv_ID = Convert.ToInt32(gvHireInvoices.DataKeys[row.RowIndex].Value);
            gvHireInvoices.SelectedIndex = row.RowIndex;
            Label lblStatus =(Label) row.FindControl("lblStatus");
            if (lblStatus.Text.ToUpper() == "APPROVED")
            {
                pendingApprove = false;
                btnSave.Visible = false;
            }
            else
            {
                pendingApprove = true;
                btnSave.Visible = true;
            }
           
            //row.BorderColor = Color.Yellow;
            Session["Inv_ID"] = Inv_ID;
            Session["PreviousRowIndex"] = row.RowIndex;
            BindItems();
            Session["Inv_ID"] = Inv_ID;
            iFrame.Attributes["src"] = "CP_Hire_Invoice_Print.aspx";
        }
        catch { }

    }




        protected void imgPrint_Click( object sender, EventArgs e)
    {
        try
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            Inv_ID = Convert.ToInt32(gvHireInvoices.DataKeys[row.RowIndex].Value);
            Session["Inv_ID"] = Inv_ID;
            //string url = String.Format("OpenPopupWindow('InvicePrint', 'Invoice Print', '../CharterParty/CP_Hire_Invoice_Print.aspx', 'popup', 800, 1200, null, null, false, false, true, null);");
            string url = String.Format("window.open('../CharterParty/CP_Hire_Invoice_Print.aspx','_blank');");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", url, true);
        }
        catch { }

    }

        private void BindBillingItems()
        {
            try
            {
                DataSet ds = objCP.GET_Billing_Items_Detail(UDFLib.ConvertIntegerToNull(Session["CPID"]));
                DataTable dt = ds.Tables[0];
                string[] Pkey_cols = new string[] { "Billing_Item_Id" };
                string[] Hide_cols = new string[] { "Trading_Range_Id", "CPID", "Item_Rate" };
                DataTable dt1 = PivotTable("WEF_Date", "Item_Amount", "Trading_Range_Id", Pkey_cols, Hide_cols, dt);
                if (dt1.Rows.Count > 0)
                {
                    dtGridItems = dt1;
                    gvbillingitems.DataSource = dt1;
                    gvbillingitems.DataBind();

                }
                else
                {
                    gvbillingitems.DataSource = dtGridItems;
                    gvbillingitems.DataBind();

                    //rgdItems.MasterTableView.Columns[9].Visible = false;

                }
    

            }
            catch (Exception ex)
            {
                //lblError.Text = ex.ToString();
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
            finally
            {

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



        protected void gvBillingItems_ItemDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[1].Visible = false;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int i = 0; i < e.Row.Controls.Count; i++)
                {


                    var headerCell = e.Row.Controls[i] as DataControlFieldHeaderCell;
                    if (headerCell.Text.Contains("Item_Group"))
                        headerCell.Text = "Item Group";
                    else if (headerCell.Text.Contains("Item_Description"))
                        headerCell.Text = "Description";
                    else if (headerCell.Text.Contains("Interval_Unit"))
                        headerCell.Text = "Unit";
                    else if (headerCell.Text.Contains("Billing_Interval"))
                        headerCell.Text = "Billing Interval";
                    else if (headerCell.Text.Contains("Item_Rate_Text"))
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
                    if(i == 5)
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;
                    else if (i <5)
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
                    else
                        e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                }

            }

        }

    private void BindItems()
    
    {
        try
        {
            rgdItems.Visible = true;
            DataTable dt = objHireInv.GET_Hire_Invoice_Items(UDFLib.ConvertIntegerToNull(Session["CPID"]), UDFLib.ConvertIntegerToNull(Session["Inv_ID"]));
            if (dt.Rows.Count > 0)
            {
                dtGridItems = dt;
                rgdItems.DataSource = dt;
                rgdItems.DataBind();
                //rgdItems.ShowFooter = false;
            }
            else
            {
                dtGridItems = GetAddTable();
                rgdItems.DataSource = dtGridItems;
                rgdItems.DataBind();
               // rgdItems.MasterTableView.Columns[8].Visible = false;
                //rgdItems.MasterTableView.Columns[9].Visible = false;

            }
            ViewState["dtGridItems"] = dtGridItems;

        }
        catch (Exception ex)
        {
            lblError.Text = ex.ToString();
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }


    private DataTable GetAddTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("Hire_Invoice_Item_Id");
        dt.Columns.Add("Charter_ID"); 
        dt.Columns.Add("Sort_Order");
        dt.Columns.Add("Item_Group");
        dt.Columns.Add("Address_Comm");
        dt.Columns.Add("Item_Amount");
        dt.Columns.Add("Item_Quantity");
        dt.Columns.Add("Charter_Item_Unit");
        dt.Columns.Add("Item_Name");
        dt.Columns.Add("Item_Details");
        dt.Columns.Add("LineTotalDebit");
        dt.Columns.Add("LineTotalCredit");
        
        dt.AcceptChanges();
        //for (int i = 1; i <= 1; i++)
        //{
        //    DataRow dr = dt.NewRow();
        //    dr[0] = i;
        //    dr[1] = i;
        //    dt.Rows.Add(dr);

        //}
        dt.AcceptChanges();
        return dt;
    }

    protected void btnSaveItem_Click(object sender, EventArgs e)
    {

        if (Session["Inv_ID"] != null)
        {
            saveval();
            BindInvoices();
            BindBillingItems();
            iFrame.Attributes["src"] = "CP_Hire_Invoice_Print.aspx";
        }


    }
    private void saveval()
    {
        try
        {
            int i = 0;

            DataTable dtExtraItems = new DataTable();
            dtExtraItems.Columns.Add("Hire_Invoice_Item_Id");
            dtExtraItems.Columns.Add("Sort_Order");
            dtExtraItems.Columns.Add("Item_Group");
            dtExtraItems.Columns.Add("Address_Comm");
            dtExtraItems.Columns.Add("Item_Amount");
            dtExtraItems.Columns.Add("Item_Quantity");
            dtExtraItems.Columns.Add("Charter_Item_Unit");
            dtExtraItems.Columns.Add("Item_Name");
            dtExtraItems.Columns.Add("Item_Details");

            int inc = 1;
            foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
            {
                TextBox txtSrno = (TextBox)(dataItem.FindControl("txtSrno"));
                DropDownList ddlItemgroup = (DropDownList)(dataItem.FindControl("ddlItemgroup") );
                DropDownList Address_Comm = (DropDownList)(dataItem.FindControl("ddlAddComm"));
                TextBox Item_Amount = (TextBox)(dataItem.FindControl("Item_Amount"));
                TextBox Item_Quantity = (TextBox)(dataItem.FindControl("Item_Quantity") );
                TextBox Charter_Item_Unit = (TextBox)(dataItem.FindControl("Charter_Item_Unit"));
                TextBox Item_Name = (TextBox)(dataItem.FindControl("Item_Name"));
                TextBox Item_Details = (TextBox)(dataItem.FindControl("Item_Details"));
                HiddenField lblID = (HiddenField)(dataItem.FindControl("lblID"));
                //string BgtCode = (dataItem.FindControl("ddlBudgetCode") as DropDownList).SelectedValue;

                if (Item_Name.Text.Length > 0 && ddlItemgroup.SelectedItem != null)
                {
                    DataRow dritem = dtExtraItems.NewRow();
                    dritem["Sort_Order"] = txtSrno.Text;
                    if (lblID.Value == null || lblID.Value =="")
                        dritem["Hire_Invoice_Item_Id"] = "0";
                    else
                         dritem["Hire_Invoice_Item_Id"] = lblID.Value;
                    dritem["Item_Group"] = ddlItemgroup.SelectedItem.Text;
                    dritem["Address_Comm"] = Address_Comm.SelectedItem.Text;
                    dritem["Item_Amount"] = Item_Amount.Text == "" ? Convert.DBNull : Convert.ToDecimal(Item_Amount.Text);
                    dritem["Item_Quantity"] = Item_Quantity.Text == "" ? Convert.DBNull : Convert.ToDecimal(Item_Quantity.Text);
                    dritem["Charter_Item_Unit"] = Charter_Item_Unit.Text;
                    dritem["Item_Name"] = Item_Name.Text;
                    dritem["Item_Details"] = Item_Details.Text;
                    dtExtraItems.Rows.Add(dritem);
                    inc++;

                }
            }

            int retval = 0;

            if (dtExtraItems.Rows.Count > 0)
            {
                retval = objHireInv.INS_UPD_Inv_Items(UDFLib.ConvertIntegerToNull(Session["Inv_ID"]), dtExtraItems, UDFLib.ConvertIntegerToNull(Session["USERID"].ToString()));

                //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
                BindItems();
            }
            else
            {
                lblError.Text = "Please provide Item description.";
            }
        }
        catch(Exception ex)
        {
            string err = ex.ToString();
        }

    }


    protected void rgdItems_ItemDataBound(object sender, GridItemEventArgs e)
    {
        foreach (GridDataItem dataItem in rgdItems.MasterTableView.Items)
        {
             DropDownList ddlItemgroup = (DropDownList)dataItem.FindControl("ddlItemgroup");
             DropDownList ddlAddComm = (DropDownList)dataItem.FindControl("ddlAddComm");
            HiddenField hdItemGroup = (HiddenField)dataItem.FindControl("hdItemGroup");
            HiddenField hdAddComm = (HiddenField)dataItem.FindControl("hdAddComm");
            LoadItemGroup(ddlItemgroup);
            if (ddlItemgroup.Items.FindByText(hdItemGroup.Value) != null)
                 ddlItemgroup.SelectedValue = ddlItemgroup.Items.FindByText(hdItemGroup.Value).Value;
            if( ddlAddComm.Items.FindByText(hdAddComm.Value)!= null)
                ddlAddComm.SelectedValue = ddlAddComm.Items.FindByText(hdAddComm.Value).Value;
            TextBox txtSrno = (TextBox)(dataItem.FindControl("txtSrno") as TextBox);
            TextBox Item_Amount = (TextBox)(dataItem.FindControl("Item_Amount") as TextBox);
            TextBox Item_Quantity = (TextBox)(dataItem.FindControl("Item_Quantity") as TextBox);
            HiddenField lblID = (HiddenField)(dataItem.FindControl("lblID") as HiddenField);
            ImageButton btnDelete = (ImageButton)(dataItem.FindControl("ImgDelete") as ImageButton);
           // Address_Comm.Attributes.Add("onKeydown", "return MaskMoney(event)");
            Item_Amount.Attributes.Add("onKeydown", "return MaskMoney(event)");
            Item_Quantity.Attributes.Add("onKeydown", "return MaskMoney(event)");
            txtSrno.Attributes.Add("onkeypress", "return numbersonly(event)");
            
            if (lblID.Value == "0")
            {
                btnDelete.Visible = false;
            }

        }

    }


    protected void LoadItemGroup(DropDownList ddlItemgroup)
    {

        DataTable dt = objHireInv.GET_Hire_Invoice_Item_Group_ALL();
        ddlItemgroup.DataSource = dt;
        ddlItemgroup.DataTextField = "Item_Group";
        ddlItemgroup.DataValueField = "Group_ID";
        ddlItemgroup.DataBind();
    }

    protected void btnAddNewItem_Click(object s, EventArgs e)
    {
        dtGridItems = (DataTable)ViewState["dtGridItems"];
        int RowID = 0;
        string Hire_Invoice_Item_Id = "0";
        foreach (GridDataItem item in rgdItems.MasterTableView.Items)
        {
            HiddenField hdnHire_Invoice_Item = (HiddenField)item.FindControl("lblID");
            if (hdnHire_Invoice_Item.Value != null && hdnHire_Invoice_Item.Value != "")
                Hire_Invoice_Item_Id = hdnHire_Invoice_Item.Value;
            else
                Hire_Invoice_Item_Id = "0";

            dtGridItems.Rows[RowID]["Sort_Order"] = ((TextBox)item.FindControl("txtSrno")).Text;
            dtGridItems.Rows[RowID]["Hire_Invoice_Item_Id"] = Hire_Invoice_Item_Id;
            dtGridItems.Rows[RowID]["Item_Group"] = ((DropDownList)item.FindControl("ddlItemgroup")).SelectedItem.Text;
            dtGridItems.Rows[RowID]["Address_Comm"] = ((DropDownList)item.FindControl("ddlAddComm")).SelectedItem.Text; // == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("Address_Comm")).Text);
            dtGridItems.Rows[RowID]["Item_Amount"] = ((TextBox)item.FindControl("Item_Amount")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("Item_Amount")).Text);
            dtGridItems.Rows[RowID]["Item_Quantity"] = ((TextBox)item.FindControl("Item_Quantity")).Text == "" ? Convert.DBNull : Convert.ToDecimal(((TextBox)item.FindControl("Item_Quantity")).Text);
            dtGridItems.Rows[RowID]["Charter_Item_Unit"] = ((TextBox)item.FindControl("Charter_Item_Unit")).Text;
            dtGridItems.Rows[RowID]["LineTotalDebit"] = 0;
            dtGridItems.Rows[RowID]["LineTotalCredit"] = 0;
            dtGridItems.Rows[RowID]["Item_Name"] = ((TextBox)item.FindControl("Item_Name")).Text;
            dtGridItems.Rows[RowID]["Item_Details"] = ((TextBox)item.FindControl("Item_Details")).Text;
            RowID++;
        }

        DataRow dr = dtGridItems.NewRow();
        //if (rgdItems.Items.Count == 0)
        //{
        //    dr[0] = 1;
        //    dr[1] = 0;
        //}
        //else
        //{
        //    dr[0] = UDFLib.ConvertToInteger(((Label)rgdItems.Items[rgdItems.Items.Count - 1].FindControl("lblSrno")).Text) + 1;
        //    dr[1] = 0;
        //}
        dtGridItems.Rows.Add(dr);
        rgdItems.DataSource = dtGridItems;
        rgdItems.DataBind();

        ViewState["dtGridItems"] = dtGridItems;


    }

    protected void onDelete(object source, CommandEventArgs e)
    {
        try
        {
            int ret = objHireInv.Delete_Hire_Invoice_Items(UDFLib.ConvertIntegerToNull(Session["USERID"]), (UDFLib.ConvertIntegerToNull(e.CommandArgument)));
            BindItems();
        }
        catch(Exception Ex)
        {
            lblError.Text = Ex.ToString();
        }
    }


    protected void gvHireInvoices_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHireInvoices.PageIndex = e.NewPageIndex;
       // BindItems();
        BindInvoices();
    }
}
   
