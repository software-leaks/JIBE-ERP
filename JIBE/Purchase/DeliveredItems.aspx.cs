using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SMS.Business.PURC;
using Telerik.Web.UI;
using System.Text;
using ClsBLLTechnical;

public partial class Technical_INV_DeliveredItems : System.Web.UI.Page
{

    private double TotalDeliverQty;

    public static string intCatalog = "";

    DataTable VsDtOrder = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            //FillDDL();
            //BindDepartmentByST_SP();

            //FillCatalogDLL();
            btnAddItem.Enabled = false;
            BindRequisitionInfo();
            if (Request.QueryString["intCatalog"].ToString() != null)
            {
                intCatalog = (Request.QueryString["intCatalog"].ToString().Trim());
            }

            lblSrchtitle.Visible = false;
            lblSrchDrawingNo.Visible = false;
            lblSrchPartNo.Visible = false;
            lblSrchDesc.Visible = false;

            txtSrchDrawingNo.Visible = false;
            txtSrchPartNo.Visible = false;
            txtSrchDesc.Visible = false;
            btnItemSearch.Visible = false;
            FillReq_Catalog();
            divCharge.Visible = false;
        }


        lblError.Text = "";
        //divCharge.Visible = false;
    }

    private void BindRequisitionInfo()
    {

        try
        {
            System.Data.DataTable dtReqInfo = new System.Data.DataTable();
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                dtReqInfo = objTechService.SelectRequistionToSupplier(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Document_Code"].ToString());
                lblReqNo.Text = dtReqInfo.DefaultView[0]["REQUISITION_CODE"].ToString();
                lblVessel.Text = dtReqInfo.DefaultView[0]["Vessel_Name"].ToString();
                lblCatalog.Text = dtReqInfo.DefaultView[0]["Name_Dept"].ToString();
                lblToDate.Text = dtReqInfo.DefaultView[0]["requestion_Date"].ToString();
                lblTotalItem.Text = dtReqInfo.DefaultView[0]["TOTAL_ITEMS"].ToString();

                lblReqNo.NavigateUrl = "RequisitionSummary.aspx?REQUISITION_CODE=" + Request.QueryString["Requisitioncode"].ToString() + "&Document_Code=" + Request.QueryString["Document_Code"].ToString() + "&Vessel_Code=" + Request.QueryString["Vessel_Code"].ToString() + "&Dept_Code=" + dtReqInfo.DefaultView[0]["DEPARTMENT"].ToString() + "&" + 1.ToString();

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }



    private void FillCatalogDLL()
    {
        string filter;
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            //cmbCatalog.Items.Clear();
            DataTable dtCatalog = objTechService.SelectCatalog();

            string department = Request.QueryString["Dept_Code"].ToString();



            //string department = cmbDept.SelectedValue.ToString();
            //filter = "dept1='" + department + "'" + " or " + "dept2='" + department + "'" + " or " + "dept3='" + department + "'" + " or " + "dept4='" + department + "'" + " or " + "dept5='" + department + "'" + " or " + "dept6='" + department + "'" + " or " + "dept7='" + department + "'" + " or " + "dept8='" + department + "'" + " or " + "dept9='" + department + "'" + " or " + "dept10='" + department + "'" + " or " + "dept11='" + department + "'" + " or " + "dept12='" + department + "'" + " or " + "dept13='" + department + "'" + " or " + "dept14='" + department + "'" + " or " + "dept15='" + department + "'";
            filter = "dept1='" + department + "'" + " or " + "dept2='" + department + "'" + " or " + "dept3='" + department + "'" + " or " + "dept4='" + department + "'" + " or " + "dept5='" + department + "'" + " or " + "dept6='" + department + "'" + " or " + "dept7='" + department + "'" + " or " + "dept8='" + department + "'" + " or " + "dept9='" + department + "'" + " or " + "dept10='" + department + "'" + " or " + "dept11='" + department + "'" + " or " + "dept12='" + department + "'" + " or " + "dept13='" + department + "'" + " or " + "dept14='" + department + "'" + " or " + "dept15='" + department + "'";
            dtCatalog.DefaultView.RowFilter = filter;




        }
    }

    protected void FillReq_Catalog()
    {

        try
        {
            lblError.Text = "";

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {

                cmbRequisitionList.Items.Clear();

                DataTable dtItemsInv = new DataTable();
                dtItemsInv = objTechService.GetRequitionForOrderItem(Request.QueryString["Vessel_Code"].ToString(), intCatalog.ToString());
                cmbRequisitionList.DataSource = dtItemsInv;
                cmbRequisitionList.Items.Add("--SELECT--");
                cmbRequisitionList.AppendDataBoundItems = true;
                //cmbRequisitionList.Items.Add("--Select--");
                cmbRequisitionList.DataTextField = "Requsitiondetal";
                cmbRequisitionList.DataValueField = "Requsitiondetal";
                cmbRequisitionList.DataBind();

            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    //protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DDLVessel.Items.Clear();
    //        DDLVessel.AppendDataBoundItems = true;
    //        DDLVessel.Items.Add("ALL");

    //         BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ();
    //        DataTable dtVessel = objTechService.SelectVessels();
    //        dtVessel.DefaultView.RowFilter = "Tech_Manager ='" + DDLFleet.SelectedItem.Text + "'";
    //        DDLVessel.DataSource = dtVessel;
    //        DDLVessel.DataTextField = "Vessels";
    //        DDLVessel.DataValueField = "Vessel_Code";
    //        DDLVessel.DataBind();
    //         
    //    }
    //    catch (Exception ex)
    //    {
    //        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    //    }
    //    finally
    //    {

    //    }
    //}

    //protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        BindDepartmentByST_SP();
    //    }
    //    catch (Exception ex)
    //    {
    //        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    //    }
    //    finally
    //    {

    //    }
    //}

    protected void cmbRequisitionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                if (cmbRequisitionList.SelectedItem.Text.ToString() == "--SELECT--")
                {

                    DataTable dtOrderNumber = new DataTable();
                    DataTable dtDeliverItem = new DataTable();
                    dtDeliverItem = objTechService.GetOrderedItems("", Request.QueryString["Vessel_Code"].ToString(), "0", intCatalog.ToString());
                    rgdDeliveredItems.DataBind();

                    btnAddItem.Enabled = false;
                    divCharge.Visible = false;
                }
                else
                {

                    lblError.Text = "";
                    buttonClickStatus.Value = "0";
                    BindOrderItem();
                    txtItems.Text = "0";
                    txtAmount.Text = "0";
                    txtRoundoff.Text = "0";
                    divCharge.Visible = false;
                    btnAddItem.Enabled = true;
                }

            }


        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    private void BindOrderItem()
    {
        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                if (cmbRequisitionList.SelectedItem.Text != "SELECT")
                {
                    //First Retrive the OrderNumber b'coz one requistion may have many order Number
                    DataTable dtOrderNumber = new DataTable();
                    DataTable dtDeliverItem = new DataTable();
                    dtOrderNumber = objTechService.GetRequitionForOrderItem(Request.QueryString["Vessel_Code"].ToString(), intCatalog.ToString());
                    dtOrderNumber.DefaultView.RowFilter = "Requsitiondetal='" + cmbRequisitionList.Items[cmbRequisitionList.SelectedIndex].Text.ToString() + "'";
                    ViewState["VsDtOrder"] = CreateTable(dtOrderNumber.DefaultView);
                    dtDeliverItem = objTechService.GetOrderedItems(cmbRequisitionList.SelectedValue.ToString().Substring(0, cmbRequisitionList.SelectedValue.ToString().IndexOf("/")), Request.QueryString["Vessel_Code"].ToString(), dtOrderNumber.DefaultView[0][0].ToString(), intCatalog.ToString());
                    rgdDeliveredItems.DataSource = dtDeliverItem;
                    ViewState["dtDeliverItem"] = dtDeliverItem;
                    rgdDeliveredItems.DataBind();




                }
                else
                {
                    DataTable dtOrderNumber = new DataTable();
                    DataTable dtDeliverItem = new DataTable();
                    dtDeliverItem = objTechService.GetOrderedItems("0", Request.QueryString["Vessel_Code"].ToString(), "0", intCatalog.ToString());
                    rgdDeliveredItems.DataSource = dtDeliverItem;
                    ViewState["dtDeliverItem"] = dtDeliverItem;
                    rgdDeliveredItems.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    protected void rgdDeliveredItems_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            foreach (TableCell cell in e.Item.Cells)
            {
                cell.ToolTip = cell.Text != "&nbsp;" ? cell.Text : null;
            }
            if (e.Item.Cells[16].Text == "&nbsp;")
            {
                TextBox txt = (TextBox)e.Item.Cells[26].FindControl("txtDeliverdQty");
                txt.ReadOnly = true;
            }
        }
    }

    private string generateDelivedNumber()
    {
        try
        {
            string strDeliver = "";

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                string data = objTechService.getDeliveredNumber("ODE" + DateTime.Now.ToString("yy")); ;
                string Zero = "";
                for (int i = 1; i <= 7 - data.Length; i++)
                {
                    Zero = Zero + "0";
                }
                strDeliver = "ODE" + DateTime.Now.ToString("yy") + Zero + data;

            }
            return strDeliver;

        }
        catch (Exception ex)
        {

            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            return "0";
        }

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder strQuery = new StringBuilder();
            TechnicalBAL objTechBAL = new TechnicalBAL();
            VsDtOrder = (DataTable)ViewState["VsDtOrder"];
            int i = 0;
            string strReqCode = VsDtOrder.Rows[0]["Requisition"].ToString();
            string strDocCode = VsDtOrder.Rows[0]["DOCUMENT_CODE"].ToString();
            string strSuppCode = VsDtOrder.Rows[0]["QUOTATION_SUPPLIER"].ToString();
            string strOrderCode = VsDtOrder.Rows[0]["ORDER_CODE"].ToString();
            string strVesselCode = VsDtOrder.Rows[0]["Vessel_Code"].ToString();
            string strCreatedBy = Session["userid"].ToString();
            string strPayAt = "On Office";
            decimal dclSuppOrdDiscount = Convert.ToDecimal(VsDtOrder.Rows[0]["suppOrderDiscount"].ToString());
            decimal dclTotalPay = 0, dclRoundOffAmt = 0;

            if (txtAmount.Text.Trim() != "")
                dclTotalPay = Convert.ToDecimal(txtAmount.Text.Trim().ToString());
            else
                dclTotalPay = 0;

            if (txtRoundoff.Text.Trim() != "")
                dclRoundOffAmt = Convert.ToDecimal(txtRoundoff.Text.Trim().ToString());
            else
                dclRoundOffAmt = 0;

            string strSystemCode = VsDtOrder.Rows[0]["ITEM_SYSTEM_CODE"].ToString();
            string strDeptCode = VsDtOrder.Rows[0]["DEPARTMENT"].ToString();
            string strDeliverQty = "", strUpdateROBQty = "", strItemIDs = "";

            foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
            {
                TextBox txtDeliverdQty = (TextBox)(dataItem.FindControl("txtDeliverdQty") as TextBox);
                Label txtUpdateROB = (Label)(dataItem.FindControl("txtUpdateROB") as Label);

                if (txtDeliverdQty.Text.Trim() != "")
                {
                    strDeliverQty = strDeliverQty + txtDeliverdQty.Text.ToString() + ",";
                    strUpdateROBQty = strUpdateROBQty + txtUpdateROB.Text.ToString() + ",";

                    strItemIDs = strItemIDs + dataItem["ITEM_REF_CODE"].Text.ToString() + ",";

                    i++;
                }
            }

            if (i == 0)
            {

                String msgNoItem = String.Format("alert('Please enter quantity for deliver.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgNoItem", msgNoItem, true);
                lblError.Text = "Please enter quantity for deliver.";
            }
            else
            {

                int RetValue = objTechBAL.UpdateDelivery(strReqCode, strDocCode, strSuppCode, strOrderCode,
                                                          strVesselCode, strCreatedBy, strPayAt, dclTotalPay,
                                                           dclRoundOffAmt, strSystemCode, strDeptCode, strItemIDs,
                                                            strDeliverQty, strUpdateROBQty, dclSuppOrdDiscount
                                                           );

                DataTable dtDeliverItem = (DataTable)ViewState["dtDeliverItem"];

                foreach (DataRow dr in dtDeliverItem.Rows)
                {
                    if (dr["ITEM_REF_CODE"].ToString() == "")
                    {
                        strQuery.Append("Insert into [PURC_Dtl_Supply_Items]([ID],ITEM_SYSTEM_CODE,[DOCUMENT_CODE],[ITEM_SERIAL_NO],[REQUISITION_CODE],");
                        strQuery.Append("[ORDER_CODE],[Order_Date],[REQUESTED_QTY],[ITEM_SHORT_DESC],[ORDER_QTY],[ORDER_SUPPLIER],[Vessel_Code],[Created_By],");
                        strQuery.Append("[Date_Of_Creatation],DELIVERY_CODE,ORDER_PRICE,ORDER_RATE,DELIVERD_QTY,Item_delivery_Remarks )");
                        strQuery.Append("(select  Top 1 (select max([ID])+1 from [PURC_Dtl_Supply_Items]),ITEM_SYSTEM_CODE,[DOCUMENT_CODE],0,[REQUISITION_CODE],");
                        strQuery.Append("[ORDER_CODE],[Order_Date],1,'");
                        strQuery.Append(dr["Short_Description"].ToString());
                        strQuery.Append("',1,[ORDER_SUPPLIER],[Vessel_Code],[Created_By],[Date_Of_Creatation],DELIVERY_CODE,'");
                        strQuery.Append(dr["Rate"].ToString());
                        strQuery.Append("','");
                        strQuery.Append(dr["Rate"].ToString());
                        strQuery.Append("','1','" + Convert.ToString(dr["Item_delivery_Remarks"]) + "from [PURC_Dtl_Supply_Items]");
                        strQuery.Append(" where [REQUISITION_CODE]= '");
                        strQuery.Append(strReqCode);
                        strQuery.Append("' and ORDER_CODE='");
                        strQuery.Append(strOrderCode);
                        strQuery.Append("')  ");
                    }
                }

                int val = objTechBAL.ExecuteQuery(strQuery.ToString());

                String msg = String.Format("alert('Delivered Items has been save successfully.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
                lblError.Text = "Delivered Items has been save successfully.";

                //cmbRequisitionList.SelectedIndex = 0;
                FillReq_Catalog();
                BindOrderItem();
                RefreshGrid();

                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {
                    //Update the requistion Stage Status
                    DataTable dtQuotationList = new DataTable();
                    dtQuotationList.Columns.Add("Qtncode");
                    dtQuotationList.Columns.Add("amount");

                    objTechService.InsertRequisitionStageStatus(Request.QueryString["Requisitioncode"].ToString(), Request.QueryString["Vessel_Code"].ToString(), Request.QueryString["Document_Code"].ToString(), "DLV", " ", Convert.ToInt32(Session["userid"]), dtQuotationList);

                }



            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }




    private void RefreshGrid()
    {

        try
        {
            DataTable dt = (DataTable)ViewState["dtDeliverItem"];
            dt.Rows.Clear();
            rgdDeliveredItems.DataSource = dt;
            rgdDeliveredItems.DataBind();
            txtItems.Text = "0";
            txtAmount.Text = "0";
            txtRoundoff.Text = "0";
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }

    }


    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        //SaveStatus = 0;
        buttonClickStatus.Value = "0";

        try
        {
            if (cmbRequisitionList.SelectedIndex != -1 && cmbRequisitionList.Items.Count > 1)
            {



            }
            else
            {

                BindOrderItem();
                txtAmount.Text = "0";
                txtItems.Text = "0";
                txtRoundoff.Text = "0";

            }


        }
        catch //(Exception ex)
        {


        }
        finally
        {

        }







    }

    protected void btnZeroAllPending_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable VsDtOrder = new DataTable();
            VsDtOrder = (DataTable)ViewState["VsDtOrder"];
            //SaveStatus = 2;
            buttonClickStatus.Value = "2";

            foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
            {
                dataItem["Pending_Qty"].Text = "0";
            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    protected void btnDeliver_Click(object sender, EventArgs e)
    {

        try
        {
            //SaveStatus = 1;
            buttonClickStatus.Value = "1";

            foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
            {

                TextBox txtDelverQuantiy = (TextBox)(dataItem.FindControl("txtDeliverdQty") as TextBox);
                Label txtUpdateROB = (Label)(dataItem.FindControl("txtUpdateROB") as Label);
                if (txtDelverQuantiy.ReadOnly == false)
                {
                    txtDelverQuantiy.Text = dataItem["Pending_Copy"].Text;

                    dataItem["Pending_Qty"].Text = "0";
                    string strROBQty = dataItem["ROB_QTY"].Text != "&nbsp;" ? dataItem["ROB_QTY"].Text : "0";
                    string strDeliverQty = txtDelverQuantiy.Text;
                    txtUpdateROB.Text = (Convert.ToInt32(strROBQty) + Convert.ToInt32(strDeliverQty)).ToString();
                }
            }


            CalculateTotalAmountnQty();

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    protected void txtDelivered_Change(object sender, EventArgs e)
    {

        CalculateTotalAmountnQty();
    }

    private void CalculateTotalAmountnQty()
    {
        VsDtOrder = (DataTable)ViewState["VsDtOrder"];

        try
        {
            int Total_Items = 0;
            double Total_Qty = 0;
            double Total_Amount = 0;
            double strDeliverItem = 0;
            double Rate = 0;

            foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
            {

                if (dataItem is GridDataItem)
                {
                    TextBox txtDeliverdQty = (TextBox)(dataItem.FindControl("txtDeliverdQty") as TextBox);

                    if (txtDeliverdQty.Text != "")
                    {

                        TextBox txtDelverQuantiy = (TextBox)(dataItem.FindControl("txtDeliverdQty") as TextBox);
                        Label txtUpdateROB = (Label)(dataItem.FindControl("txtUpdateROB") as Label);

                        string strROBQty = dataItem["ROB_QTY"].Text != "&nbsp;" ? dataItem["ROB_QTY"].Text : "0";
                        string strDeliverQty = (txtDelverQuantiy.Text == "0.00" ? "0" : txtDelverQuantiy.Text);
                        txtUpdateROB.Text = (Convert.ToDouble(strROBQty) + Convert.ToDouble(strDeliverQty)).ToString();

                        dataItem["Pending_Qty"].Text = (Convert.ToDouble(dataItem["Pending_Copy"].Text) - (Convert.ToDouble(strDeliverQty))).ToString();
                        if (Convert.ToDouble(dataItem["Pending_Qty"].Text) < 0)
                        {
                            dataItem["Pending_Qty"].Text = "0";
                        }


                        //strDeliverItem = Convert.ToDecimal(txtDeliverdQty.Text.Trim());

                        strDeliverItem = Convert.ToDouble(txtDeliverdQty.Text == "0.00" ? "0" : txtDeliverdQty.Text);
                        Label lblAmount = (Label)(dataItem.FindControl("lblAmount") as Label);

                        Rate = Convert.ToDouble(dataItem["Rate"].Text.ToString());
                        if (txtDelverQuantiy.ReadOnly != true)
                        {
                            lblAmount.Text = (Rate * strDeliverItem).ToString();
                        }

                        Total_Amount += Convert.ToDouble(lblAmount.Text);
                        TotalDeliverQty += Convert.ToDouble(strDeliverItem.ToString());

                        if (Convert.ToDouble(txtDeliverdQty.Text.Trim()) > 0)
                        {
                            Total_Items = Total_Items + 1;
                        }

                        Total_Qty = Total_Qty + Convert.ToDouble(strDeliverItem);
                        //Total_Amount = Total_Amount + Total_Amount;
                    }
                }

            }

            Double SVat = 0;
            Double Ssurcharge = 0;
            Ssurcharge = (Total_Amount - (Total_Amount * Convert.ToDouble(VsDtOrder.Rows[0][6].ToString())) / 100) * Convert.ToDouble(VsDtOrder.Rows[0][8].ToString()) / 100;
            SVat = ((Total_Amount - (Total_Amount * Convert.ToDouble(VsDtOrder.Rows[0][6].ToString())) / 100) + Ssurcharge) * Convert.ToDouble(VsDtOrder.Rows[0][9].ToString()) / 100;
            txtAmount.Text = Math.Round((Total_Amount - ((Total_Amount * Convert.ToDouble(VsDtOrder.Rows[0][6].ToString())) / 100) + Ssurcharge + SVat), 2).ToString("####0.00");

            // txtAmount.Text = Math.Round((Total_Amount - ((Total_Amount * dtvsOrder.Rows[0][6].ToString()) / 100)+ ((Total_Amount * dtvsOrder.Rows[0][8].ToString())/100) + ((Total_Amount * Vat)/100)), 2).ToString("####0.00");

            txtRoundoff.Text = txtRoundoff.Text != "" ? txtRoundoff.Text : "0";

            txtRoundoff.Text = Convert.ToDouble(txtRoundoff.Text).ToString("#0.00");
            txtItems.Text = Total_Items.ToString();

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }

    }

    protected void btnItemSearch_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtSrchDrawingNo.Text != "" && txtSrchDesc.Text != "" && txtSrchPartNo.Text != "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Drawing_Number"].Text.ToLower().IndexOf(txtSrchDrawingNo.Text.Trim()) != -1 || dataItem["Prev_Item_Ref_Code"].Text.ToLower().IndexOf(txtSrchPartNo.Text.Trim()) != -1 || dataItem["Short_Description"].Text.ToLower().IndexOf(txtSrchDesc.Text.Trim()) != -1)
                    {

                        dataItem.Selected = true;


                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text != "" && txtSrchDesc.Text != "" && txtSrchPartNo.Text == "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Drawing_Number"].Text.ToLower().IndexOf(txtSrchDrawingNo.Text.Trim()) != -1 || dataItem["Prev_Item_Ref_Code"].Text.ToLower().IndexOf(txtSrchPartNo.Text.Trim()) != -1)
                    {
                        dataItem.Selected = true;
                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text != "" && txtSrchDesc.Text == "" && txtSrchPartNo.Text != "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Drawing_Number"].Text.ToLower().IndexOf(txtSrchDrawingNo.Text.Trim()) != -1 || dataItem["Prev_Item_Ref_Code"].Text.ToLower().IndexOf(txtSrchPartNo.Text.Trim()) != -1)
                    {
                        dataItem.Selected = true;
                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text == "" && txtSrchDesc.Text != "" && txtSrchPartNo.Text != "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Prev_Item_Ref_Code"].Text.ToLower().IndexOf(txtSrchPartNo.Text.Trim()) != -1 || dataItem["Short_Description"].Text.ToLower().IndexOf(txtSrchDesc.Text.Trim()) != -1)
                    {
                        dataItem.Selected = true;
                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text != "" && txtSrchDesc.Text == "" && txtSrchPartNo.Text == "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Drawing_Number"].Text.ToLower().IndexOf(txtSrchDrawingNo.Text.Trim()) != -1)
                    {
                        dataItem.Selected = true;
                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text == "" && txtSrchDesc.Text != "" && txtSrchPartNo.Text == "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Short_Description"].Text.ToLower().IndexOf(txtSrchDesc.Text.Trim().ToLower()) != -1)
                    {
                        dataItem.Selected = true;
                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text == "" && txtSrchDesc.Text == "" && txtSrchPartNo.Text != "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    if (dataItem["Prev_Item_Ref_Code"].Text.ToLower().IndexOf(txtSrchPartNo.Text.Trim()) != -1)
                    {
                        dataItem.Selected = true;
                    }
                    else
                    {
                        dataItem.Selected = false;
                    }
                }
            }
            else if (txtSrchDrawingNo.Text == "" && txtSrchDesc.Text == "" && txtSrchPartNo.Text == "")
            {
                foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
                {
                    dataItem.Selected = false;

                }
            }
        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }


    }

    private DataTable CreateTable(DataView obDataView)
    {
        if (null == obDataView)
        {
            throw new ArgumentNullException
            ("DataView", "Invalid DataView object specified");
        }

        DataTable obNewDt = obDataView.Table.Clone();
        int idx = 0;
        string[] strColNames = new string[obNewDt.Columns.Count];
        foreach (DataColumn col in obNewDt.Columns)
        {
            strColNames[idx++] = col.ColumnName;
        }

        IEnumerator viewEnumerator = obDataView.GetEnumerator();
        while (viewEnumerator.MoveNext())
        {
            DataRowView drv = (DataRowView)viewEnumerator.Current;
            DataRow dr = obNewDt.NewRow();
            try
            {
                foreach (string strName in strColNames)
                {
                    dr[strName] = drv[strName];
                }
            }
            catch (Exception ex)
            {
                //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            }
            obNewDt.Rows.Add(dr);
        }

        return obNewDt;
    }

    protected void btnAddItem_Click(object sender, EventArgs e)
    {
        //using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
        //{
        if (cmbRequisitionList.SelectedItem.Text.ToString() == "SELECT")
        {
            txtAddChargeDesc.Text = "";
            txtAddnAmount.Text = "";
            divCharge.Visible = false;



        }
        else
        {
            txtAddChargeDesc.Text = "";
            txtAddnAmount.Text = "";
            divCharge.Visible = true;
        }

        //}
    }
    //protected void btncancel_Click(object sender, EventArgs e)
    //{

    //}
    protected void btnAddition_Click(object sender, EventArgs e)
    {
        //DataTable dt = (DataTable)ViewState["dtDeliverItem"];
        DataTable dt = (DataTable)ViewState["dtDeliverItem"];
        int k = 0;
        foreach (GridDataItem dataItem in rgdDeliveredItems.MasterTableView.Items)
        {
            if (dataItem is GridDataItem)
            {
                TextBox txtDeliverdQty = (TextBox)(dataItem.FindControl("txtDeliverdQty") as TextBox);
                Label lblAmount = (Label)(dataItem.FindControl("lblAmount") as Label);
                Label lblrob = (Label)(dataItem.FindControl("txtUpdateROB") as Label);
                TextBox txtDeliveryRemarks = ((TextBox)(dataItem.FindControl("txtDeliveryRemarks")));
                dt.Rows[k]["DELIVERD_QTY1"] = Convert.ToDecimal(txtDeliverdQty.Text == "" ? "0" : txtDeliverdQty.Text);
                dt.Rows[k]["Amount"] = Convert.ToDecimal(lblAmount.Text == "" ? "0" : lblAmount.Text);
                dt.Rows[k]["Item_delivery_Remarks"] = Convert.ToString(txtDeliveryRemarks.Text).Trim();
                //  dt.Rows[k]["DELIVERD_QTY1"] = ;
                k++;
            }
        }
        ViewState["dtDeliverItem"] = AddRow(dt);
        rgdDeliveredItems.DataSource = dt;
        rgdDeliveredItems.DataBind();
        divCharge.Visible = false;

    }

    private DataTable AddRow(DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr[3] = "0.00";
        dr[6] = "0.00";
        //dr[26] = "0.00";
        dr[18] = "0.00";
        dr[22] = txtAddChargeDesc.Text;
        dr[32] = txtAddnAmount.Text;
        dr[31] = "0.00";
        dr[11] = "0.00";
        dr[12] = "0.00";
        dr[14] = "0.00";
        dr[30] = "0.00";
        dt.Rows.Add(dr);
        return dt;
    }

    protected void rgdDeliveredItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {

    }
}


