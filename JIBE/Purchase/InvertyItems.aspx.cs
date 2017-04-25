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
using Telerik.Web.UI;
using System.Web.Caching;
using SMS.Business.PURC;
using System.Text;
using ClsBLLTechnical;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Collections.Generic;
using SMS.Business.POLOG;

public partial class InvertyItems : System.Web.UI.Page
{
    //string _constring = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
    public string CurrentUser;
    public int ItemCount;
    public string DateFormat = "";
    BLL_PURC_Common objCommon = new BLL_PURC_Common();
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {

        caltxtDeliveryDate.Format = UDFLib.GetDateFormat();
        DateFormat = UDFLib.GetDateFormat();//Get User date format
        if (!IsPostBack)
        {
            FillDDL();
            pnlNewRequisition.Visible = true;
            pnlViewRequisition.Visible = false;
            pnlMetadata.Visible = false;

        }
    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    private string GetSessionUserName()
    {
        if (Session["USERNAME"] != null)
            return Session["USERNAME"].ToString();
        else
            return "0";
    }
    /// <summary>
    /// Fill DropDownList ,Fleet,POType,AccountType,OwnerName,Urgency,DeliveryPort,ReqsnType,Vessel
    /// </summary>
    public void FillDDL()
    {

        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "Code";
            DDLFleet.DataBind();
            DDLFleet.Items.Insert(0, new ListItem("-SELECT-", "0"));


            DataTable dt = BLL_PURC_Common.PURC_Get_PO_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "PO_Create");
            ddlPOType.DataSource = dt;
            ddlPOType.DataTextField = "PO_Type";
            ddlPOType.DataValueField = "PO_Code";
            ddlPOType.DataBind();
            ddlPOType.Items.Insert(0, new ListItem("-SELECT-", "0"));

            DataTable dtAccountType = BLL_PURC_Common.PURC_Get_Sys_Variable(UDFLib.ConvertToInteger(GetSessionUserID()), "Account_Type");
            ddlAccountType.DataSource = dtAccountType;
            ddlAccountType.DataTextField = "VARIABLE_NAME";
            ddlAccountType.DataValueField = "VARIABLE_CODE";
            ddlAccountType.DataBind();
            ddlAccountType.Items.Insert(0, new ListItem("-SELECT-", "0"));

            DataTable dtOwnerName = BLL_PURC_Common.PURC_Get_Supplier_Type(UDFLib.ConvertToInteger(GetSessionUserID()), "OWNER");
            ddlOwnerName.DataSource = dtOwnerName;
            ddlOwnerName.DataTextField = "Supplier_Name";
            ddlOwnerName.DataValueField = "Supplier_Code";
            ddlOwnerName.DataBind();
            ddlOwnerName.Items.Insert(0, new ListItem("-SELECT-", "0"));

         
            DataTable dtItem = BLL_PURC_Common.PURC_Get_ItemCategory(UDFLib.ConvertToInteger(GetSessionUserID()), "UrgencyLevel");
            ddlUrgency.DataTextField = "Category_Name";
            ddlUrgency.DataValueField = "ID";
            ddlUrgency.DataSource = dtItem;
            ddlUrgency.DataBind();


            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                //DataTable dtPort = objTechService.LibraryGetSystemParameterList("7609", "");
              

                DataTable dtPort = objTechService.getDeliveryPort();
                ddlDeliveryPort.DataSource = dtPort;
                ddlDeliveryPort.DataTextField = "Port_Name";
                ddlDeliveryPort.DataValueField = "Id";
                ddlDeliveryPort.DataBind();
                ddlDeliveryPort.Items.Insert(0, new ListItem("-SELECT-", "0"));
              
            }
           


            ddlReqsnType.Items.Insert(0, new ListItem("-SELECT-", "0"));
            ddlFunction.Items.Insert(0, new ListItem("-SELECT-", "0"));
            ddlCatalogue.Items.Insert(0, new ListItem("-SELECT-", "0"));
            ddlPortCall.Items.Insert(0, new ListItem("-SELECT-", "0"));

            DataTable dtVessel = objVsl.Get_UserVesselList_DL(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), UDFLib.ConvertToInteger(GetSessionUserID()));

            if (DDLFleet.SelectedIndex != 0)
            {
                dtVessel.DefaultView.RowFilter = "Tech_Manager ='" + DDLFleet.SelectedItem.Text + "'";
            }
            ddlVessel.DataSource = dtVessel;
            ddlVessel.DataTextField = "Vessel_name";
            ddlVessel.DataValueField = "Vessel_id";
            ddlVessel.DataBind();


            BindPortCall(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));


        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);

        }
        finally
        {

        }
    }



    /// <summary>
    /// Fill Vessel according to selected fleet
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            ddlVessel.Items.Clear();
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = new DataTable();
            if (chkAssignement.Checked == true)
            {
                dtVessel = objVsl.Get_UserVesselList_DL(int.Parse(DDLFleet.SelectedValue.ToString()), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            else
            {
                dtVessel = objVsl.Get_VesselList(int.Parse(DDLFleet.SelectedValue.ToString()), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            }
            if (dtVessel.Rows.Count > 0)
            {
                ddlVessel.DataSource = dtVessel;
                ddlVessel.DataTextField = "Vessel_name";
                ddlVessel.DataValueField = "Vessel_id";
                ddlVessel.DataBind();
            }
            else
            {
                ddlVessel.Items.Insert(0, new ListItem("-SELECT-", "0"));
            }
            BindPortCall(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckValidation()", true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {

        }
    }
    /// <summary>
    /// Create new reqsn
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void btnRequisition_Click(object s, CommandEventArgs e)
    {
        try
        {
            if (ddlCatalogue.SelectedValue != "0")
            {
                CreateNewRequisition();
            }
            else
            {
                string message1 = "alert('Please Select catalouge for create requisition');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "requisition", message1, true);
            }
        }

        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        finally
        {

        }

    }
    /// <summary>
    /// Check if any reqsn  with same catalogue in Draft mode.
    /// </summary>
    protected void CreateNewRequisition()
    {

        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            IventoryItemData objDoInventory = new IventoryItemData();
            objDoInventory.POType = ddlPOType.SelectedValue.ToString();
            objDoInventory.VesselCode = ddlVessel.SelectedValue.ToString();
            objDoInventory.Department = ddlFunction.SelectedValue.ToString();
            objDoInventory.DocumentNumber = "1";
            objDoInventory.Totalitem = 0;
            objDoInventory.LineType = "R";
            objDoInventory.CreatedBy = GetSessionUserID().ToString();
            objDoInventory.UserName = GetSessionUserName().ToString().Split(new char[] { ' ' })[0];
            objDoInventory.UrgencyCode = ddlUrgency.SelectedValue;
            objDoInventory.Account_Type = ddlAccountType.SelectedValue;
            objDoInventory.RequisitionType = ddlReqsnType.SelectedValue;
            objDoInventory.SystemCode = ddlCatalogue.SelectedValue;
            if (ddlDeliveryPort.SelectedValue != "0" && ddlDeliveryPort.SelectedValue != "")
            {
                objDoInventory.Delivery_Port = Convert.ToInt16(ddlDeliveryPort.SelectedValue);//Convert.ToInt16(Session["DeliveryPort"]);
            }
            else
            {
                objDoInventory.Delivery_Port = 0;
            }
            objDoInventory.Delivery_Date = UDFLib.ConvertToDate(txtDeliveryDate.Text);
            objDoInventory.Port_Call = Convert.ToInt16(ddlPortCall.SelectedValue);
            objDoInventory.Owner_Code = ddlOwnerName.SelectedValue;

            DataSet ds = BLL_PURC_Common.PURC_Checking_Requisition(objDoInventory);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pnlViewRequisition.Visible = true;
                pnlNewRequisition.Visible = false;
                ddlRequisitionList.DataSource = ds.Tables[0];
                ddlRequisitionList.DataTextField = "requisition_code";
                ddlRequisitionList.DataValueField = "DOCUMENT_CODE";
                ddlRequisitionList.DataBind();
                ddlRequisitionList.Items.Insert(0, new ListItem("-SELECT-", "0"));
                ddlRequisitionList.SelectedIndex = 0;
                BindRequisition(ddlRequisitionList.SelectedValue);
            }
            else
            {
                string ReturenID = objTechService.GenerateRequisitionNumber(objDoInventory);
                if (ReturenID != "")//(dtReqsn_DocumentCode.Rows.Count >0)
                {

                    ViewState["flt_DocumentCode"] = ReturenID;//Convert.ToString(dt.Rows[0]["DOCUMENT_CODE"]);
                    Session["DocumentCode"] = ReturenID;

                    Response.Redirect("PURC_Reqn_Items.aspx?DocumentCode=" + ViewState["flt_DocumentCode"] + "");
                }
            }
        }


    }
    #region GENRATE REQUISITION NUMBER


    private DataTable Get_New_Reqsn_Document_Code(string VesselID, string Dept)
    {
        DataTable dtReq = new DataTable();
        try
        {
            BLL_PURC_Purchase objPurcBAL = new BLL_PURC_Purchase();
            dtReq = objPurcBAL.Get_Purc_New_Reqsn_Docment_Code(VesselID, Dept);

        }
        catch
        {
        }
        return dtReq;


    }
    #endregion

    protected void ddlPOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            BindConfiguration();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "CheckValidation()", true);
            
            return;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindPortCall(int? Vessel_ID)
    {
        int? Supply_ID = 0;
        DataTable dt = BLL_POLOG_Register.POLOG_Get_Port_Call(null, Supply_ID, Vessel_ID, 0);

        ddlPortCall.DataSource = dt;
        ddlPortCall.DataTextField = "PORT_NAME";
        ddlPortCall.DataValueField = "Port_Call_ID_Ref";
        ddlPortCall.DataBind();
        ddlPortCall.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void BindConfiguration()
    {

        DataSet DeptDt = BLL_PURC_Common.PURC_Get_Configuration(ddlPOType.SelectedValue);
        DeptDt.Tables[2].PrimaryKey = new[] { DeptDt.Tables[2].Columns["column_name"] };
        if (DeptDt.Tables[0].Rows.Count > 0)
        {
            if (DeptDt.Tables[0].Rows[0]["Delivery_Date"].ToString() == "1")
            {
                trDelivery.Visible = true;
                //if(DeptDt.Tables[0].Rows.Find("Delivery Date").ToString() == "0")

                if (Convert.ToBoolean(DeptDt.Tables[2].Rows.Find("Delivery_Date")["mandatory"]) == true)
                {
                    lblDeliveryAst.Visible = true;
                    rqDeliveryDate.Enabled = true;
                }
                else
                {
                    lblDeliveryAst.Visible = false;
                    rqDeliveryDate.Enabled = false;
                }
            }
            else
            { trDelivery.Visible = false; }
            if (DeptDt.Tables[0].Rows[0]["Vessel_Movement_Date"].ToString() == "1")
            {
                trPortCall.Visible = true;
                if (Convert.ToBoolean(DeptDt.Tables[2].Rows.Find("Delivery_Port")["mandatory"]) == true)
                {
                    lblDeliveryPortast.Visible = true;
                   reqDeliveryPort.Enabled = true;
                }
                else
                {
                    lblDeliveryPortast.Visible = false;
                   reqDeliveryPort.Enabled = false;
                }
            }
            else
            { trPortCall.Visible = false; }
            if (DeptDt.Tables[0].Rows[0]["Auto_Owner_Selection"].ToString() == "1")
            {
                trOwner.Visible = true;
                lblOwnerAst.Visible = true;
                rqOwnerName.Enabled = true;
            }
            else
            {
                trOwner.Visible = false;
                lblOwnerAst.Visible = false;
                rqOwnerName.Enabled = false;
            }
            if (DeptDt.Tables[0].Rows[0]["Delivery_Port"].ToString() == "1")
            {
                trDeliveryPort.Visible = true;

                if (Convert.ToBoolean(DeptDt.Tables[2].Rows.Find("Vessel_Movement")["mandatory"]) == true)
                {
                    lblPortAst.Visible = true;
                    rqPortCall.Enabled = true;
                }
                else
                {
                    lblPortAst.Visible = false;
                    rqPortCall.Enabled = false;
                }
            }
            else
            { trDeliveryPort.Visible = false; }
            if (DeptDt.Tables[0].Rows[0]["Item_Category"].ToString() == "1")
            {
                trItemCategory.Visible = true;
                using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
                {

                    DataTable dtItem = BLL_PURC_Common.PURC_Get_ItemCategory(UDFLib.ConvertToInteger(GetSessionUserID()), "ItemCategory");
                    ddlItemCategory.DataTextField = "Category_Name";
                    ddlItemCategory.DataValueField = "ID";
                    ddlItemCategory.DataSource = dtItem;
                    ddlItemCategory.DataBind();
                }
                BindCatalogue(UDFLib.ConvertToInteger(GetSessionUserID()), "Item_Category", ddlItemCategory.SelectedValue,UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
            }
            else
            {
                trItemCategory.Visible = false;
            }
        }
        else
        {
            trDelivery.Visible = false;
            trDeliveryPort.Visible = false;
            trOwner.Visible = false;
            trPortCall.Visible = false;
        }

        if (Convert.ToBoolean(DeptDt.Tables[2].Rows.Find("Requisition_Reason")["mandatory"]) == true)
        {
            lblReqnReasonAst.Visible = true;
            rqReqnReason.Enabled = true;
        }
        else
        {
            lblReqnReasonAst.Visible = false;
            rqReqnReason.Enabled = false;
        }
        ddlReqsnType.Items.Clear();
       
        ddlReqsnType.DataSource = DeptDt.Tables[1];
        ddlReqsnType.DataTextField = "Reqsn_Name";
        ddlReqsnType.DataValueField = "ID";
        ddlReqsnType.DataBind();
        ddlReqsnType.Items.Insert(0, new ListItem("-SELECT-", "0"));
      

    }

    protected void ddlReqsnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindFunction();
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void BindFunction()
    {

         TechnicalBAL objtechBAL = new TechnicalBAL();
         DataTable dt = BLL_PURC_Common.Get_Function(UDFLib.ConvertToInteger(GetSessionUserID()), UDFLib.ConvertToInteger(ddlReqsnType.SelectedValue));
            ddlFunction.DataTextField = "Function_Name";
            ddlFunction.DataValueField = "ID";
            ddlFunction.DataSource = dt;
            ddlFunction.DataBind();
            ddlFunction.Items.Insert(0, new ListItem("-SELECT-", "0"));
    }
    protected void ddlFunction_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCatalogue(UDFLib.ConvertToInteger(GetSessionUserID()), ddlReqsnType.SelectedValue, ddlFunction.SelectedValue,UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void BindCatalogue(int UserID, string Reqn_Type, string Function,int Vessel_ID)
    {
        ddlCatalogue.Items.Clear();
        DataSet ds = BLL_PURC_Common.SelectCatalog(UserID, Reqn_Type, Function, Vessel_ID);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlCatalogue.DataSource = ds.Tables[0];
            ddlCatalogue.DataTextField = "SYSTEM_DESCRIPTION";
            ddlCatalogue.DataValueField = "SYSTEM_CODE";
            ddlCatalogue.DataBind();
            ddlCatalogue.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
        else
        {
            ddlCatalogue.Items.Insert(0, new ListItem("-SELECT-", "0"));
        }
     
    }
    protected void ddlRequisitionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindRequisition(ddlRequisitionList.SelectedValue);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void BindRequisition(string DocumentCode)
    {
        DataSet ds = BLL_PURC_Common.PURC_Get_RequisitionDeatils(UDFLib.ConvertToInteger(GetSessionUserID()), DocumentCode);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblCreatedDate.Text = ds.Tables[0].Rows[0]["Created_By"].ToString();
            lblCreatedBy.Text = UDFLib.ConvertUserDateFormat(ds.Tables[0].Rows[0]["Date_Of_Creatation"].ToString(), UDFLib.GetDateFormat());
            lblItem.Text = ds.Tables[0].Rows[0]["ItemCount"].ToString();
            lblModifiedDate.Text = ds.Tables[0].Rows[0]["Date_Of_Modification"].ToString();
            Session["DocumentCode"] = ds.Tables[0].Rows[0]["DOCUMENT_CODE"].ToString();
            pnlMetadata.Visible = true;
        }
        else
        {
            pnlMetadata.Visible = false;
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("InvertyItems.aspx");
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("PURC_Reqn_Items.aspx?DocumentCode=" + ViewState["flt_DocumentCode"] + "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void chkAssignement_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlVessel.Items.Clear();
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = new DataTable();
            if (chkAssignement.Checked == true)
            {
                dtVessel = objVsl.Get_UserVesselList_DL(int.Parse(DDLFleet.SelectedValue.ToString()), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()), UDFLib.ConvertToInteger(GetSessionUserID()));
            }
            else
            {
                dtVessel = objVsl.Get_VesselList(int.Parse(DDLFleet.SelectedValue.ToString()), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            }
            ddlVessel.DataSource = dtVessel;
            ddlVessel.DataTextField = "Vessel_name";
            ddlVessel.DataValueField = "Vessel_id";
            ddlVessel.DataBind();
            BindPortCall(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCatalogue(UDFLib.ConvertToInteger(GetSessionUserID()), "Item_Category", ddlItemCategory.SelectedValue,UDFLib.ConvertToInteger(ddlVessel.SelectedValue));
           
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }

    }
    protected void ddlVessel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindPortCall(UDFLib.ConvertIntegerToNull(ddlVessel.SelectedValue));
          
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

}

