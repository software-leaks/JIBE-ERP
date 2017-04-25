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

using CrystalDecisions.Shared;
using SMS.Business.Infrastructure;

public partial class Technical_INV_CreateConsolidatedPO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDDL();
            BindSupplierForLogisticCompany();
            FillPort();
            BindPOListInGrid("Raised-PO");
            btnSendToSupplier.Enabled = true;

        }

    }

    protected void BindPOListInGrid(string strPORaisedFlag)
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataSet dsPoList = objTechService.GetPOListForLogisticCompany();
                string rowfilter = DDLVessel.SelectedIndex == 0 ? "" : "vessel_code='" + DDLVessel.SelectedValue + "' and ";


                if (strPORaisedFlag == "Raised-PO")
                {
                    DataTable dtPoList = dsPoList.Tables[0];
                    rowfilter = rowfilter + "Line_Type ='O'";
                    dtPoList.DefaultView.RowFilter = rowfilter;

                    grvPOList.DataSource = dtPoList;
                    grvPOList.DataBind();
                    lblMessage.Text = "";
                }
                else if (strPORaisedFlag == "Non-Raised-PO")
                {
                    DataTable dtPoList = dsPoList.Tables[0];
                    rowfilter = rowfilter + "Line_Type NOT LIKE 'O'";
                    dtPoList.DefaultView.RowFilter = rowfilter;
                    grvPOList.DataSource = dtPoList;
                    grvPOList.DataBind();
                    lblMessage.Text = "";
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

    protected void FillDDL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();


            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--ALL--", "0");
            DDLVessel.Items.Insert(0, li);


        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {

        }
    }

    protected void FillPort()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable PortDS = objTechService.getDeliveryPort();
                DDLPort.DataSource = PortDS;
                DDLPort.DataTextField = "Port_Name";
                DDLPort.DataValueField = "Id";
                DDLPort.DataBind();

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

    protected void BindSupplierForLogisticCompany()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtSupplier = new DataTable();
                dtSupplier = objTechService.SelectSupplier();
                DataView dvSupplier = new DataView(dtSupplier);
               

                DDLLogisticCompany.DataSource = dvSupplier;
                DDLLogisticCompany.DataTextField = "SUPPLIER_NAME";
                DDLLogisticCompany.DataValueField = "SUPPLIER";
                DDLLogisticCompany.DataBind();

                // ddl.SelectedIndex = 1;

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

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DDLVessel.Items.Clear();
            DDLVessel.AppendDataBoundItems = true;
            DDLVessel.Items.Add("--Select--");



            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            if (DDLFleet.SelectedValue.ToString() == "0")
            {
                Session["sFleet"] = "0";
                DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
                DDLVessel.DataSource = dtVessel;
                DDLVessel.DataTextField = "Vessel_name";
                DDLVessel.DataValueField = "Vessel_id";
                DDLVessel.DataBind();
                ListItem li = new ListItem("--ALL--", "0");
                DDLVessel.Items.Insert(0, li);
                Session["sVesselCode"] = "0";
            }
            else
            {
                Session["sFleet"] = DDLFleet.SelectedValue;
                DDLVessel.Items.Clear();

                DataTable dtVessel = objVsl.GetVesselsByFleetID(int.Parse(DDLFleet.SelectedValue.ToString()));
                DDLVessel.DataSource = dtVessel;
                DDLVessel.DataTextField = "Vessel_name";
                DDLVessel.DataValueField = "Vessel_ID";
                DDLVessel.DataBind();
                ListItem li = new ListItem("--ALL--", "0");
                DDLVessel.Items.Insert(0, li);
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

    //protected void grvPOList_PageIndexChanged(object source, GridPageChangedEventArgs e)
    //{
    //    try
    //    {
    //        using ( BLL_PURC_Purchase  objTechService = new  BLL_PURC_Purchase ())
    //        {
    //            DataSet dsPoList = objTechService.GetPOListForLogisticCompany();
    //            if (rblRaisedPO.SelectedValue == "1")
    //            {
    //                DataTable dtPoList = dsPoList.Tables[0];
    //                dtPoList.DefaultView.RowFilter = "Line_Type ='O'";
    //                grvPOList.DataSource = dtPoList;
    //                grvPOList.CurrentPageIndex = e.NewPageIndex;
    //                grvPOList.DataBind();
    //                lblMessage.Text = "";
    //            }
    //            else if (rblRaisedPO.SelectedValue == "2")
    //            {
    //                DataTable dtPoList = dsPoList.Tables[0];
    //                dtPoList.DefaultView.RowFilter = "Line_Type NOT LIKE 'O'";
    //                grvPOList.DataSource = dtPoList;
    //                grvPOList.CurrentPageIndex = e.NewPageIndex;
    //                grvPOList.DataBind();
    //                lblMessage.Text = "";
    //            }
    //            dsPoList.Clear();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
    //    }
    //    finally
    //    {

    //    }
    //}

    protected void rblRaisedPO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblRaisedPO.SelectedValue == "1")
        {
            BindPOListInGrid("Raised-PO");
            btnSendToSupplier.Enabled = true;
        }
        else if (rblRaisedPO.SelectedValue == "2")
        {
            btnSendToSupplier.Enabled = false;
            BindPOListInGrid("Non-Raised-PO");
        }
    }

    protected void btnSendToSupplier_Click(object sender, EventArgs e)
    {
        Int32 iCount = 0;
        //string strListID = string.Empty;
        //string strArrInfo = string.Empty;
        try
        {
            string strDelivery_Port = Convert.ToString(DDLPort.SelectedValue);
            string strDelivery_Date = Convert.ToString(tocal.SelectedDate);
            IFormatProvider provider = new System.Globalization.CultureInfo("en-CA", true);
            DateTime dt = DateTime.Parse(strDelivery_Date, provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string strAssign_Agent_Code = Convert.ToString(DDLLogisticCompany.SelectedValue);
            Int32 i32VesselCode = Convert.ToInt32(DDLVessel.SelectedValue);
            Int32 i32CreatedBy = Convert.ToInt32(Session["userid"]);

            //strArrInfo = strDelivery_Port + "," + strDelivery_Date + "," + Convert.ToString(DDLVessel.SelectedItem.Text) + "," + Convert.ToString(DDLLogisticCompany.SelectedValue) + "," + Convert.ToString(DDLLogisticCompany.SelectedItem.Text);

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                foreach (GridDataItem gItem in grvPOList.Items)
                {
                    CheckBox chkForConsolidate = (CheckBox)gItem.FindControl("chkForConsolidate");
                    if (chkForConsolidate.Checked == true)
                    {
                        Label lblID = (Label)gItem.FindControl("lblID");
                        Int32 iID = Convert.ToInt32(lblID.Text.ToString());
                        string strRequisition_Code = Convert.ToString(gItem["Requisition_Code"].Text);
                        string strQuotation_Code = Convert.ToString(gItem["Quotation_Code"].Text) == "&nbsp;" ? "" : Convert.ToString(gItem["Quotation_Code"].Text);
                        string strOrder_Code = Convert.ToString(gItem["Order_Code"].Text) == "&nbsp;" ? "" : Convert.ToString(gItem["Order_Code"].Text);
                        string strDocument_Code = Convert.ToString(gItem["Document_Code"].Text);
                        string strSupplier = Convert.ToString(gItem["Supplier"].Text);
                        //////............. Insert into Table....................
                        int iReturn = objTechService.AddConsolidatedPO(strRequisition_Code, strQuotation_Code, strOrder_Code, strDelivery_Port,
                            dt, strDocument_Code, strRequisition_Code, strAssign_Agent_Code, i32VesselCode, i32CreatedBy);
                        //if (strListID != string.Empty)
                        //{
                        //    strListID = strListID + "," + iID.ToString();
                        //}
                        //else
                        //{
                        //    strListID = iID.ToString();
                        //}
                        iCount++;
                    }

                }
            }
            ////............ Send the required Data for Report purpose..................
            if (iCount == 0)
            {
                lblMessage.Text = "Please select at least one item from the list shown above.";
            }
            else if (iCount == 100)
            {
                lblMessage.Text = "Sorry, you can select MAX 100-items.";
            }
            else
            {
                lblMessage.Text = "";
                Session.Add("i32Vessel_Code", DDLVessel.SelectedValue.ToString());
                Session.Add("strAgent_Code", DDLLogisticCompany.SelectedValue.ToString());
                ResponseHelper.Redirect("~/PURCHASE/ReportForConsolidatedPO.aspx", "blank", "");
            }

        }
        catch (Exception ex)
        {
            //.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
        }
        finally
        {
            if (rblRaisedPO.SelectedValue == "1")
            {
                BindPOListInGrid("Raised-PO");
            }
            else if (rblRaisedPO.SelectedValue == "2")
            {
                BindPOListInGrid("Non-Raised-PO");
            }
        }
    }

    protected void DDLVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblRaisedPO.SelectedValue == "1")
        {
            BindPOListInGrid("Raised-PO");
        }
        else if (rblRaisedPO.SelectedValue == "2")
        {
            BindPOListInGrid("Non-Raised-PO");
        }
    }
}
