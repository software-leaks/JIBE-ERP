using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.PMS;
using System.Text;
using System.Data;

public partial class Technical_PMS_PMS_CopyItems : System.Web.UI.Page
{
    public BLL_PMS_Library_Items objItems = new BLL_PMS_Library_Items();
    public BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            /* To values settings */

            if (Request.QueryString["VesselCode"] != null && Request.QueryString["VesselCode"] != "")
            {
                BindToVesselDLL();                                                                              //Bind Vessels (To)
                DDLToVessel.SelectedValue = Request.QueryString["VesselCode"].ToString();


                BindFromVesselDLL();                                                                            //Bind Vessels (From)
                DDLFromVessel.SelectedValue = Request.QueryString["VesselCode"].ToString();

            }
            else
            {

                BindToVesselDLL();                                                                                //Bind Vessels (To)
            }


            if (Request.QueryString["ReqsnCode"] != null)
            {
                BindDepartmentDLL(DDLFromfunc, Request.QueryString["ReqsnCode"].ToString());                     // Bind Functions (From)            
                BindDepartmentDLL(DDLTofunc, Request.QueryString["ReqsnCode"].ToString());                       // Bind Functions (To)             
            }

            //if (Request.QueryString["DeptCode"] != null && Request.QueryString["DeptCode"] != "")
            //{
            //    BindDepartmentDLL(DDLFromfunc, Request.QueryString["DeptCode"].ToString());                                            
            //    BindDepartmentDLL(DDLTofunc, Request.QueryString["DeptCode"].ToString());

            //}
            //else
            //{
            //    //BindToDepartmentDLL();
            //    //BindFromDepartmentDLL();
            //}

            DataTable distinctValuesfrom;
            DataView viewfrom = new DataView(GetItemsCountFrom());

            DataTable distinctValuesTo;
            DataView viewTo = new DataView(GetItemsCountTo());





            if (Request.QueryString["SystemCode"] != null && Request.QueryString["SystemCode"] != "")
            {

                distinctValuesfrom = viewfrom.ToTable(true, "System_Code", "System_Description");                  // Bind Catalogue(Form)
                BindFromCatalogueDDL(distinctValuesfrom);

                //DDLFromSystem.SelectedValue = Request.QueryString["SystemCode"].ToString();                    
                distinctValuesTo = viewTo.ToTable(true, "System_Code", "System_Description");
                BindToCatalogueDLL(distinctValuesTo);                                                             // Bind To catalogue(TO)


                distinctValuesfrom = viewfrom.ToTable(true, "SubSystemCode", "Subsystem");                        // Bind subcatalogue (From)
                BindFromSubCatalogueDLL(distinctValuesfrom);

                distinctValuesTo = viewTo.ToTable(true, "SubSystemCode", "SubSystem");                           // Bind Subcatalogue (TO)
                BindToSubCatalogueDLL(distinctValuesTo);
            }


            lblMsg.Text = "";
        }

        lblMsg.Text = "";
    }

    /// <summary>
    /// Bind Vessels dropdownlist (From)
    /// </summary>
    private void BindFromVesselDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFromVessel.Items.Clear();
            DDLFromVessel.DataSource = dtVessel;
            DDLFromVessel.DataTextField = "Vessel_name";
            DDLFromVessel.DataValueField = "Vessel_id";
            DDLFromVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFromVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        } 
    }

    /// <summary>
    /// Bind Vessel dropdownlist (To)
    /// </summary>
    private void BindToVesselDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLToVessel.Items.Clear();
            DDLToVessel.DataSource = dtVessel;
            DDLToVessel.DataTextField = "Vessel_name";
            DDLToVessel.DataValueField = "Vessel_id";
            DDLToVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLToVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Bind DropdownLists as per the requisitionType
    /// </summary>
    private void BindDepartmentDLL(DropDownList ddl, string Reqsns)
    {
        try
        {
            BLL_PURC_Config_PO objBLLPURC = new BLL_PURC_Config_PO();
            ddl.Items.Clear();
            ddl.DataSource = objBLLPURC.PURC_Get_Configured_Functions(Reqsns).Tables[0];
            ddl.AppendDataBoundItems = true;
            ddl.DataTextField = "Function_Name";
            ddl.DataValueField = "ID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));
            ddl.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }



    //private void BindToDepartmentDLL()
    //{
    //    try
    //    {
    //        using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
    //        {
    //            DataTable dtDepartment = new DataTable();
    //            dtDepartment = objBLLPURC.SelectDepartment();
    //            dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";

    //            DDLTofunc.Items.Clear();
    //            DDLTofunc.DataSource = dtDepartment;
    //            DDLTofunc.AppendDataBoundItems = true;
    //            DDLTofunc.DataTextField = "Name_Dept";
    //            DDLTofunc.DataValueField = "Code";
    //            DDLTofunc.DataBind();
    //            DDLTofunc.Items.Insert(0, new ListItem("--SELECT ALL--", "0"));

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}

    /// <summary>
    ///  Bind Catalogue (FROM)
    /// </summary>
    private void BindFromCatalogueDDL(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                DDLFromSystem.Items.Clear();
                DDLFromSystem.DataSource = dt;
                DDLFromSystem.DataTextField = "System_Description";
                DDLFromSystem.DataValueField = "System_Code";
                DDLFromSystem.DataBind();

            }
            else { DDLFromSystem.Items.Clear(); }
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFromSystem.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    ///  Bind Catalogue (To)
    /// </summary>
    private void BindToCatalogueDLL(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                DDLToSystem.Items.Clear();
                DDLToSystem.DataSource = dt;
                DDLToSystem.DataTextField = "System_Description";
                DDLToSystem.DataValueField = "System_Code";
                DDLToSystem.DataBind();

            }
            else { DDLToSystem.Items.Clear(); }
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLToSystem.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    ///  Bind SubCatalogue (To)
    /// </summary>
    public void BindToSubCatalogueDLL(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                DDLToSubsystem.Items.Clear();
                DDLToSubsystem.DataSource = dt;
                DDLToSubsystem.DataTextField = "Subsystem";
                DDLToSubsystem.DataValueField = "SubSystemCode";
                DDLToSubsystem.DataBind();

            }
            else { DDLToSubsystem.Items.Clear(); }
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLToSubsystem.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    ///  Bind SubCatalogue (FROM)
    /// </summary>
    public void BindFromSubCatalogueDLL(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                DDLFromSubsystem.Items.Clear();
                DDLFromSubsystem.DataSource = dt;
                DDLFromSubsystem.DataTextField = "Subsystem";
                DDLFromSubsystem.DataValueField = "SubSystemCode";
                DDLFromSubsystem.DataBind();

            }
            else { DDLFromSubsystem.Items.Clear(); }
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFromSubsystem.Items.Insert(0, li);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// On Function Change Bind Catalogue (From)
    /// </summary>
    protected void DDLFromFunc_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable distinctValues;
            DataView view = new DataView(GetItemsCountFrom());
            distinctValues = view.ToTable(true, "System_Code", "System_Description");

            BindFromCatalogueDDL(distinctValues);

            DDLFromSystem.Items.Clear();
            DDLFromSubsystem.Items.Clear();
            txtFromMachinery.Text = "";
            lblMsg.Text = "";
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// On Function Change Bind System/Catalogue (To)
    /// </summary>
    protected void DDLToFunc_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable distinctValues;
        DataView view = new DataView(GetItemsCountTo());
        distinctValues = view.ToTable(true, "System_Code", "System_Description");
        //GetItemsCountTo();
        BindToCatalogueDLL(distinctValues);

        DDLToSystem.Items.Clear();
        DDLToSubsystem.Items.Clear();
        txtToMachinery.Text = "";
        lblMsg.Text = "";
    }

    /// <summary>
    /// On System/Catalogue Change Bind SubSystem/SubCatalogue (From)
    /// </summary>
    protected void DDLFromSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLFromSubsystem.Items.Clear();
        DataTable distinctValuesfrom;
        DataView viewfrom = new DataView(GetItemsCountFrom());
        distinctValuesfrom = viewfrom.ToTable(true, "SubSystemCode", "Subsystem");
        BindFromSubCatalogueDLL(distinctValuesfrom);

        DDLFromSubsystem.SelectedValue = "0";
        txtFromMachinery.Text = "";
        lblMsg.Text = "";

        //GetItemsCountFrom();


    }

    /// <summary>
    /// On System/Catalogue Change Bind SubSystem/SubCatalogue (To)
    /// </summary>
    protected void DDLToSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLToSubsystem.Items.Clear();
        DataTable distinctValuesTO;
        DataView viewTo = new DataView(GetItemsCountTo());
        distinctValuesTO = viewTo.ToTable(true, "SubSystemCode", "Subsystem");
        BindToSubCatalogueDLL(distinctValuesTO);
        // DDLToSubsystem.SelectedValue = "0";
        txtToMachinery.Text = "";
        lblMsg.Text = "";

        // GetItemsCountTo();

    }

    /// <summary>
    /// On SubSystem/SubCatalogue Change get the items count (From)
    /// </summary>
    protected void DDLFromSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItemsCountFrom();
    }

    /// <summary>
    /// On SubSystem/SubCatalogue Change get the items count (To)
    /// </summary>
    protected void DDLToSubsystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetItemsCountTo();
    }

    /// <summary>
    ///  Gets the Items count on the basis of selected Parameters (TO)
    /// </summary>
    protected DataTable GetItemsCountTo()
    {
        DataSet ds = new DataSet();
        try
        {
            int? isactivestatus = 1;

            ds = objItems.LibraryItemsGetToMove(UDFLib.ConvertStringToNull(DDLToSystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLToSubsystem.SelectedValue)
               , UDFLib.ConvertIntegerToNull(DDLToVessel.SelectedValue), isactivestatus, Request.QueryString["ReqsnCode"].ToString(), DDLTofunc.SelectedValue == "0" ? "" : DDLTofunc.SelectedValue, txtFromMachinery.Text);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
        lblToJobCount.Text = ds.Tables[0].Rows.Count.ToString();
        return ds.Tables[0];

    }

    /// <summary>
    ///  Gets the Items count on the basis of selected Parameters (From)
    /// </summary>
    protected DataTable GetItemsCountFrom()
    {
        DataSet ds = new DataSet();
        try
        {
            int? isactivestatus = 1;
            string sr= Request.QueryString["ReqsnCode"] == null ? "" : Request.QueryString["ReqsnCode"].ToString();
            ds = objItems.LibraryItemsGetToMove(UDFLib.ConvertStringToNull(DDLFromSystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFromSubsystem.SelectedValue)
                , UDFLib.ConvertIntegerToNull(DDLFromVessel.SelectedValue), isactivestatus, sr, DDLFromfunc.SelectedValue == "0" ? "" : DDLFromfunc.SelectedValue, txtFromMachinery.Text);
            lblFromJobCount.Text = ds.Tables[0].Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
       
        return ds.Tables[0]==null? null:ds.Tables[0];

    }

    /// <summary>
    ///  Append or Overwrite Item on another function on Copy Items Button Click
    /// </summary>
    protected void MoveItemsToOtherSubsystem(string action)
    {
        try
        {
            if (action != "APPEND")
            {
                int result = objItems.PURC_Items_COPY_OVERWRITE(DDLFromSystem.SelectedValue, DDLFromSubsystem.SelectedValue
                    , DDLFromVessel.SelectedValue, Session["USERID"].ToString(), DDLToSystem.SelectedValue, DDLToSubsystem.SelectedValue
                    , DDLToVessel.SelectedValue);
            }
            else
            {
                int result = objItems.PURC_Items_COPY_Append(UDFLib.ConvertStringToNull(DDLFromSystem.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFromSubsystem.SelectedValue)
                , UDFLib.ConvertIntegerToNull(DDLFromVessel.SelectedValue)


                , UDFLib.ConvertStringToNull(DDLToSystem.SelectedValue), UDFLib.ConvertStringToNull(DDLToSubsystem.SelectedValue), 1, Session["USERID"].ToString());
            }
            String script = String.Format("parent.RefreshFromchild();parent.hideModal('dvMoveJobsPopUp'); "); /*dvCopyJobsPopUp is replaced with dvMoveJobsPopUp*/
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", script, true);
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    /// <summary>
    /// Move the Selected Items To the Another slected Function on Movr Items Button click
    /// </summary>
    protected void btnMoveItems_Click(object sender, EventArgs e)
    {
        if (DDLTofunc.SelectedIndex != 0)
        {
            MoveItemsToOtherSubsystem("MOVE");
        }
    }


    protected void btnSelectSystem_Click(object sender, ImageClickEventArgs e)
    {

        DDLToSystem.SelectedValue = DDLFromSystem.SelectedValue;
        DataTable distinctValuesTo;
        DataView viewTo = new DataView(GetItemsCountTo());
        distinctValuesTo = viewTo.ToTable(true, "SubSystemCode", "SubSystemCode");
        BindToSubCatalogueDLL(distinctValuesTo);
        UpdPnlTo.Update();

    }

    /// <summary>
    /// On Machinery Search Image Click (From)
    /// </summary>
    protected void imgFromMachinerySearch_Click(object sender, ImageClickEventArgs e)
    {
        DataTable distinctValues;
        DataView view = new DataView(GetItemsCountFrom());
        distinctValues = view.ToTable(true, "System_Code", "System_Description");

        BindFromCatalogueDDL(distinctValues);
    }

    /// <summary>
    /// On Machinery Search Image Click (To)
    /// </summary>
    protected void imgToMachinerySearch_Click(object sender, ImageClickEventArgs e)
    {
        DataTable distinctValues;
        DataView view = new DataView(GetItemsCountFrom());
        distinctValues = view.ToTable(true, "SubSystemCode", "Subsystem");
        BindToCatalogueDLL(distinctValues);
    }

    /// <summary>
    /// On OverWrite Click Insert the selected Items from "From" to Function and Catalogues Selected in "To" 
    /// </summary>
    protected void btnOW_Click(object sender, EventArgs e)
    {
        MoveItemsToOtherSubsystem("OVERWRITE");
    }

    /// <summary>
    /// On Append Click Updates the selected Items from "From" to Function and Catalogues Selected in "To" 
    /// </summary>
    protected void btnApnd_Click(object sender, EventArgs e)
    {
        MoveItemsToOtherSubsystem("APPEND");
    }
}