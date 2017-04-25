using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.PURC;
using System.Text;
using System.Collections;
using ClsBLLTechnical;
using SMS.Business.Infrastructure;


public partial class Purchase_CTP_Quotation_Evaluation : System.Web.UI.Page
{

    int lastFixedColumnID = 4;
    int SupplierColumnGroup = 7;
    int itemRateID = 2;
    int itemDiscountID = 3;
    int itemRemarkID = 5;
    int itemNetPriceID = 4;
    int iItemSelectID = 7;
    int itemAvgQtyID = 4;
    int itemAvgPrice = 6;
    int itemNameID = 2;
    int itemPartNumID = 1;
    int itemLibUnitID = 3;
    int itemQtnUnitID = 1;


    Dictionary<int, bool> dicchkOnlyQuoted = new Dictionary<int, bool>();
    Dictionary<int, string> dicQtnSts = new Dictionary<int, string>();
    Dictionary<int, string> dicddlApproval = new Dictionary<int, string>();

    bool isCallFromPageLoad = false;

    MergeGridviewHeader_Info objItemColumn = new MergeGridviewHeader_Info();

    protected void Page_Load(object sender, EventArgs e)
    {
        isCallFromPageLoad = true;
        UserAccessValidation();
        if (!IsPostBack)
        {
            #region ====================== Create datatable of qtns =========================
            hdfquotation_codes_compare.Value = "";
            DataTable dtqtn = new DataTable();
            DataColumn dtcol = new DataColumn("id");
            dtqtn.Columns.Add(dtcol);
            dtqtn.Columns.Add("onlyQuoted");
            dtqtn.Columns.Add("ApprvalStatus");

            dtqtn.PrimaryKey = new DataColumn[] { dtcol };
            DataRow drQtn;
            string[] arrQtnID = Request.QueryString["quotation_ids"].Split('_');
            foreach (string strid in arrQtnID)
            {
                int id = 0;
                if (int.TryParse(strid, out id))
                {
                    drQtn = dtqtn.NewRow();
                    drQtn[0] = id.ToString();
                    drQtn[1] = 0;
                    dtqtn.Rows.Add(drQtn);
                    hdfquotation_codes_compare.Value += id.ToString() + ",";
                }
            }

            ViewState["dtqtns"] = dtqtn;
            #endregion


            hdfUserIDSaveEval.Value = Session["userid"].ToString();
            ViewState["SortDirection"] = "0";
            ViewState["SortColumn"] = "";

            BindSubCatalogue();
            BindSuppliers();
            BindItems();
        }
        else
        {
            BindItems();
        }
    }

    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {


        }
        if (objUA.Edit == 0)
        {
            btnApprove.Visible = false;
            btnFinalizeEval.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {

            // You don't have sufficient previlege to access the requested page.
        }


    }
    private void BindSuppliers()
    {
        rgdSupplierInfo.DataSource = BLL_PURC_CTP.Get_Ctp_Qtn_Eval_Supplier((DataTable)ViewState["dtqtns"]);
        rgdSupplierInfo.DataBind();
        if (!IsPostBack)
        {
            ViewState["isAllApproved"] = true;
            foreach (GridViewRow gr in rgdSupplierInfo.Rows)
            {
                if (((HiddenField)gr.FindControl("hdfQuotation_Status")).Value != "AP")
                {
                    ViewState["isAllApproved"] = false;

                }
                dicQtnSts.Add(Convert.ToInt32(rgdSupplierInfo.DataKeys[gr.RowIndex].Values["Quotation_ID"].ToString()), ((HiddenField)gr.FindControl("hdfQuotation_Status")).Value);
            }

            ViewState["dicQtnSts"] = dicQtnSts;
        }
        if (bool.Parse(ViewState["isAllApproved"].ToString()))
        {
            btnFinalizeEval.Visible = false;
            btnSaveEvaln.Visible = false;
        }

        

    }

    protected void BindItems()
    {
        if (!isCallFromPageLoad)
            SaveItems();

        #region ===================== Merge header columns ============================
        DataTable dtqtn = (DataTable)ViewState["dtqtns"];

        if (objItemColumn.MergedColumns.Count == 0)
        {

            int columnindex = lastFixedColumnID + 1;
            int groupid = 1;
            string GroupCss = "";
            string suppliername = "";
            string portname = "";
            foreach (GridViewRow gr in rgdSupplierInfo.Rows)
            {
                DataRow drqtnid = dtqtn.Rows.Find(rgdSupplierInfo.DataKeys[gr.RowIndex].Values["Quotation_ID"].ToString());
                //get show only quoted item status for filter

                drqtnid["onlyQuoted"] = ((CheckBox)gr.FindControl("chkshowOnlyQuoted")).Checked == true ? "1" : "0";
                drqtnid["ApprvalStatus"] = ((DropDownList)gr.FindControl("ddlApproval")).SelectedValue == "" ? null : ((DropDownList)gr.FindControl("ddlApproval")).SelectedValue;

                dicchkOnlyQuoted.Add(gr.RowIndex, ((CheckBox)gr.FindControl("chkshowOnlyQuoted")).Checked);

                dicddlApproval.Add(gr.RowIndex, ((DropDownList)gr.FindControl("ddlApproval")).SelectedValue);

                if (groupid % 2 == 1)
                    GroupCss = "Ctp-QtnEval-HeaderStyle-css";
                else
                    GroupCss = "Ctp-QtnEval-AltHeaderStyle-css";

                suppliername = ((Label)gr.FindControl("lblSupplierName")).Text;
                suppliername = suppliername.Length > 20 ? suppliername.Substring(0, 19) : suppliername;
                portname = ((Label)gr.FindControl("lblPortName")).Text;
                portname = portname.Length > 20 ? portname.Substring(0, 19) : portname;

                objItemColumn.AddMergedColumns(new int[] { columnindex, columnindex + 1, columnindex + 2, columnindex + 3, columnindex + 4, columnindex + 5, columnindex + 6 }, suppliername + "&nbsp ,&nbsp " + portname, GroupCss);


                columnindex += SupplierColumnGroup;
                groupid++;
            }

            dtqtn.AcceptChanges();

        }

        #endregion


        int is_fetch_count = ucCustomPageritems.isCountRecord;

        rgdQuatationInfo.DataSource = BLL_PURC_CTP.Get_Ctp_Qtn_Eval_Item(dtqtn,
                                                                          UDFLib.ConvertStringToNull(txtitemsearch.Text),
                                                                          UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue),
                                                                          UDFLib.ConvertIntegerToNull(""),
                                                                          ucCustomPageritems.CurrentPageIndex,
                                                                          ucCustomPageritems.PageSize,
                                                                          ref is_fetch_count,
                                                                          UDFLib.ConvertIntegerToNull(ViewState["SortDirection"]),
                                                                          UDFLib.ConvertStringToNull(ViewState["SortColumn"]));

        rgdQuatationInfo.DataBind();

        if (ucCustomPageritems.isCountRecord == 1)
        {
            ucCustomPageritems.CountTotalRec = is_fetch_count.ToString();
            ucCustomPageritems.BuildPager();
        }

        isCallFromPageLoad = false;
    }

    public void BindSubCatalogue()
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtSubSystem = new DataTable();
            string CatalogId = Request.QueryString["Catalogue"];
            dtSubSystem = objTechService.SelectSubCatalogs();

            dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "' or SubSystem_code='0'";
            ddlSubCatalogue.DataTextField = "Subsystem_Description";
            ddlSubCatalogue.DataValueField = "SubSystem_code";
            ddlSubCatalogue.DataSource = dtSubSystem.DefaultView;
            ddlSubCatalogue.DataBind();
            ddlSubCatalogue.Items.FindByText("ALL").Selected = true;

        }
    }
    protected void rgdQuatationInfo_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        #region ================ Header Cells ==========================

        if (e.Row.RowType == DataControlRowType.Header)
        {
            int icss = 1;
            int groupid = 1;
            int icell = 0;
            foreach (TableCell hcell in e.Row.Cells)
            {
                if (icell == 0)
                    hcell.Visible = false;


                if (icell == itemPartNumID)
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnPartNumber";
                    lbtn.Click += lbtnOrderBy;
                    lbtn.CommandArgument = "[" + hcell.Text + "]";
                    lbtn.ForeColor = System.Drawing.Color.Black;
                    lbtn.Text = hcell.Text;

                    hcell.Text = "";
                    hcell.Controls.Add(lbtn);
                }
                else if (icell == itemNameID)
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnItemName";
                    lbtn.Click += lbtnOrderBy;
                    lbtn.CommandArgument = "[" + hcell.Text + "]";
                    lbtn.Text = hcell.Text;
                    lbtn.ForeColor = System.Drawing.Color.Black;
                    hcell.Text = "";
                    hcell.Controls.Add(lbtn);
                }
                else if (icell == itemAvgQtyID)
                {
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnAvgQty";
                    lbtn.Click += lbtnOrderBy;
                    lbtn.CommandArgument = "[" + hcell.Text + "]";
                    lbtn.Text = hcell.Text;
                    lbtn.ForeColor = System.Drawing.Color.Black;
                    hcell.Text = "";
                    hcell.Controls.Add(lbtn);
                }


                if (icell > lastFixedColumnID)
                {


                    if (groupid % 2 == 1)
                        hcell.CssClass = "Ctp-QtnEval-HeaderStyle-css";
                    else
                        hcell.CssClass = "Ctp-QtnEval-AltHeaderStyle-css";

                    // store the actual column id for sorting on rate
                    if (icss == itemRateID)
                    {
                        LinkButton lbtn = new LinkButton();
                        lbtn.ID = "lbtn" + hcell.Text.Split('_')[0];
                        lbtn.Click += lbtnOrderBy;
                        lbtn.CommandArgument = hcell.Text;
                        lbtn.Text = hcell.Text.Split('_')[1];
                        lbtn.ForeColor = System.Drawing.Color.Black;
                        hcell.Text = "";
                        hcell.Controls.Add(lbtn);
                    }

                    else if (icss == iItemSelectID)
                    {
                        CheckBox chkselect = new CheckBox();
                        chkselect.AutoPostBack = false;
                        chkselect.ID = "chk_" + hcell.Text;

                        HiddenField hdf = new HiddenField();
                        hdf.ID = "hdf_" + hcell.Text;
                        hdf.Value = hcell.Text.Split('_')[0].Remove(0, 1);//store the quotation id

                        hcell.Text = "";
                        hcell.Controls.Add(chkselect);
                        hcell.Controls.Add(hdf);
                        chkselect.Attributes.Add("onclick", "javascript:SelectAll('" + hdf.Value + "',this);");

                    }
                    else
                    {
                        hcell.Text = hcell.Text.Split('_')[1];
                    }

                    if (icss == SupplierColumnGroup)
                    {
                        icss = 1;
                        groupid++;
                    }
                    else
                    {
                        icss++;

                    }
                }
                else
                    hcell.CssClass = "hd";



                icell++;
            }
        }

        #endregion

        #region ================ Data Cells ====================================

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[itemAvgQtyID].HorizontalAlign = HorizontalAlign.Right;
            string[] itemname = e.Row.Cells[itemNameID].Text.Split(new string[] { "_`_" }, StringSplitOptions.None);
            e.Row.Cells[itemNameID].Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1]  body=[ Short Desc : " + itemname[0] + "<hr>Long Desc :" + itemname[2] + "<hr> Part No.:" + itemname[1] + "]");
            e.Row.Cells[itemNameID].Text = itemname[0].Length > 50 ? itemname[0].Substring(0, 49) : itemname[0];



            int icss = 1;
            int groupid = 1;
            int icell = 0;
            int cheasestSuppID = 0;
            decimal MinimumRate = 0;
            foreach (TableCell dcell in e.Row.Cells)
            {

                if (icell > lastFixedColumnID)
                {
                    // get the cheapest supp
                    if (icss == itemNetPriceID)
                    {

                        decimal currentNetPrice = 0;
                        if (decimal.TryParse(dcell.Text, out currentNetPrice))
                        {
                            if (MinimumRate == 0)
                            {
                                MinimumRate = currentNetPrice;
                                cheasestSuppID = icell;
                            }
                            else if (currentNetPrice < MinimumRate)
                            {
                                MinimumRate = currentNetPrice;
                                cheasestSuppID = icell;
                            }
                        }
                    }

                    // apply css 
                    if (groupid % 2 == 1)
                        dcell.CssClass = "Ctp-QtnEval-ItemStyle-css";
                    else
                        dcell.CssClass = "Ctp-QtnEval-AltItemStyle-css";

                    //check the defference in unit
                    if (icss == itemQtnUnitID)
                    {
                        if (e.Row.Cells[itemLibUnitID].Text != dcell.Text && dcell.Text != "&nbsp;")
                            dcell.BackColor = System.Drawing.Color.LightPink;
                    }

                    // add image  for remark
                    if (icss == itemRemarkID)
                    {
                        if (dcell.Text.Trim() != "" && dcell.Text != "&nbsp;")
                        {
                            Image imgremark = new Image();
                            imgremark.ID = "img" + icell.ToString();

                            imgremark.ImageUrl = "~/PURCHASE/Image/view1.gif";
                            imgremark.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1]  body=[" + dcell.Text + "]");

                            dcell.Controls.Add(imgremark);
                        }
                    }

                    // text alignment
                    if (icss == itemRateID || icss == itemDiscountID || icss == itemNetPriceID || icss == itemAvgPrice)
                        dcell.HorizontalAlign = HorizontalAlign.Right;
                    else if (icss == itemRemarkID)
                    {
                        dcell.HorizontalAlign = HorizontalAlign.Center;
                    }

                    // add checkbox 
                    if (icss == iItemSelectID)
                    {
                        icss = 1;
                        groupid++;

                        if (dcell.Text != "-1")
                        {
                            string qtncode = ((HiddenField)rgdQuatationInfo.HeaderRow.Cells[icell].Controls[1]).Value;
                            CheckBox chkselect = new CheckBox();
                            chkselect.ID = "chk_" + qtncode;
                            chkselect.Checked = dcell.Text.Split('_')[0] == "0" ? false : true;

                            if (((Dictionary<int, string>)ViewState["dicQtnSts"]).Count > 0)
                                chkselect.Enabled = (((Dictionary<int, string>)ViewState["dicQtnSts"])[Convert.ToInt32(qtncode)] == "AP" ? false : true);

                            HiddenField hdf = new HiddenField();
                            hdf.ID = "hdf_" + qtncode;
                            hdf.Value = dcell.Text.Split('_')[1];//store the qtn_item_id

                            dcell.Controls.Add(chkselect);
                            dcell.Controls.Add(hdf);
                        }
                        else
                        {
                            dcell.Text = "";
                        }
                    }
                    else
                        icss++;
                }
                else
                    dcell.CssClass = "gtdth";

                icell++;
            }
            if (cheasestSuppID != 0)
                e.Row.Cells[cheasestSuppID].BackColor = System.Drawing.Color.Yellow;

        }

        #endregion

    }

    protected void lbtnOrderBy(object s, EventArgs e)
    {
        LinkButton lbtn = (LinkButton)s;

        ViewState["SortDirection"] = ViewState["SortDirection"].ToString() == "0" ? "1" : "0";
        ViewState["SortColumn"] = lbtn.CommandArgument;

        BindItems();
    }


    protected void btnApprove_Click(object sender, EventArgs e)
    {
        SaveItems();
        BindSuppliers();
        DataTable dtQtnAmt = new DataTable();
        dtQtnAmt.Columns.Add("qtnid");
        dtQtnAmt.Columns.Add("amount");
        DataRow dr;
        foreach (GridViewRow gr in rgdSupplierInfo.Rows)
        {
            string hdfGrandTotal = ((HiddenField)gr.FindControl("hdfQuotation_Status")).Value;
            if (hdfGrandTotal != "AP" && UDFLib.ConvertToDecimal(((HiddenField)gr.FindControl("hdfGrandTotal")).Value) > 0)
            {
                dr = dtQtnAmt.NewRow();
                dr["qtnid"] = rgdSupplierInfo.DataKeys[gr.RowIndex].Values["Quotation_ID"].ToString();
                dr["amount"] = ((HiddenField)gr.FindControl("hdfGrandTotal")).Value;

                dtQtnAmt.Rows.Add(dr);
            }
        }

        int sts = BLL_PURC_CTP.Upd_Ctp_Approve_Contract(Convert.ToInt32(Session["userid"]), dtQtnAmt, DateTime.Parse(txtEffectiveDate.Text), DateTime.Parse(txtExpiryDate.Text), txtRemark.Text);
        String msg1 = String.Format("alert('Approved successfuly');window.close();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg123", msg1, true);

    }

    protected void btnSaveEvaln_Click(object s, EventArgs e)
    {

        string FinalQuery = HiddenQuery.Value;

        if (FinalQuery != "")
        {
            BindItems();
            BindSuppliers();

        }

    }

    protected int SaveItems()
    {
        string FinalQuery = HiddenQuery.Value;
        int retVal = 0;
        if (FinalQuery != "")
        {
            TechnicalBAL objtechBAL = new TechnicalBAL();
            retVal = objtechBAL.ExecuteQuery(FinalQuery);
        }
        return retVal;
    }



    protected void rgdQuatationInfo_ItemCreated(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            MergeGridviewHeader.SetProperty(objItemColumn);
            e.Row.SetRenderMethodDelegate(MergeGridviewHeader.RenderHeader);

        }

    }


    protected void rgdSupplierInfo_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow item = (GridViewRow)e.Row;
            if (dicchkOnlyQuoted.Count > 0)
                ((CheckBox)item.FindControl("chkshowOnlyQuoted")).Checked = dicchkOnlyQuoted[item.RowIndex];

            if (dicddlApproval.Count > 0)
                ((DropDownList)item.FindControl("ddlApproval")).Items.FindByValue(dicddlApproval[item.RowIndex]).Selected = true;


        }
    }

    protected void btnSearchItems_Click(object s, EventArgs e)
    {
        BindSuppliers();
        ucCustomPageritems.isCountRecord = 1;
        BindItems();
    }
}

