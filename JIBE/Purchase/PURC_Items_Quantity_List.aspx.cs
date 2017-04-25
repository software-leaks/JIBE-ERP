using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.PURC;
using System.Text;

public partial class Purchase_PURC_Items_Quantity_List : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindFleetDLL();
            DDLFleet.Select(UDFLib.ConvertStringToNull(Session["USERFLEETID"]));
            BindVesselDDL();
            BindDeptType();
            rbtnDeptType_SelectedIndexChanged(null, null);
            BindItems();
        }


    }


    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));


            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataSource = FleetDT;


        }
        catch (Exception ex)
        {

        }
    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();
            StringBuilder sbFilterFlt = new StringBuilder();
            string VslFilter = "";
            foreach (DataRow dr in DDLFleet.SelectedValues.Rows)
            {
                sbFilterFlt.Append(dr[0]);
                sbFilterFlt.Append(",");
            }

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

            if (sbFilterFlt.Length > 1)
            {
                sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                VslFilter = string.Format("fleetCode in (" + sbFilterFlt.ToString() + ")");
                dtVessel.DefaultView.RowFilter = VslFilter;
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();


        }
        catch (Exception ex)
        {

        }
    }

    public void BindDeptType()
    {
        try
        {

            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable DeptDt = objTechService.GetDeptType();
                rbtnDeptType.DataSource = DeptDt;
                rbtnDeptType.DataTextField = "Description";
                rbtnDeptType.DataValueField = "Short_Code";
                rbtnDeptType.DataBind();
                rbtnDeptType.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {

        }

    }

    protected void ddlCatalogue_OnSelectedIndexChanged(object s, EventArgs e)
    {
        using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
        {
            DataTable dtSubSystem = new DataTable();
            string CatalogId = ddlCatalogue.SelectedValue;
            dtSubSystem = objTechService.SelectSubCatalogs();

            dtSubSystem.DefaultView.RowFilter = "System_Code ='" + CatalogId + "' or SubSystem_code='0'";
            ddlSubCatalogue.Items.Clear();
            ddlSubCatalogue.DataTextField = "subsystem_description";
            ddlSubCatalogue.DataValueField = "subsystem_code";
            ddlSubCatalogue.DataSource = dtSubSystem.DefaultView;
            ddlSubCatalogue.DataBind();
            ListItem li = new ListItem("SELECT ALL", "0");
            ddlSubCatalogue.Items.Insert(0, li);

        }
    }

    protected void rbtnDeptType_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();

                if (rbtnDeptType.SelectedItem.Text == "Spares")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + rbtnDeptType.SelectedValue + "'";


                }
                else if (rbtnDeptType.SelectedItem.Text == "Stores")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + rbtnDeptType.SelectedValue + "'";



                }
                else if (rbtnDeptType.SelectedItem.Text == "Repairs")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + rbtnDeptType.SelectedValue + "'";


                }
                ddlDepartment.Items.Clear();
                ddlDepartment.DataTextField = "Name_Dept";
                ddlDepartment.DataValueField = "Code";
                ddlDepartment.DataSource = dtDepartment;
                ddlDepartment.DataBind();
                ListItem li = new ListItem("SELECT ALL", "0");
                ddlDepartment.Items.Insert(0, li);

            }

        }
        catch (Exception ex)
        {

        }

    }

    protected void ddlDepartment_OnSelectedIndexChanged(object source, System.EventArgs e)
    {
        try
        {
            string filter = null;
            string DeptType = rbtnDeptType.SelectedValue.Equals("ALL") ? "0" : rbtnDeptType.SelectedValue;
            DataTable dtCatalog = BLL_PURC_Purchase.Get_Catalogues(UDFLib.ConvertStringToNull(DeptType), UDFLib.ConvertStringToNull(ddlDepartment.SelectedValue));

            StringBuilder sbFilterFlt = new StringBuilder();

            if (DDLVessel.SelectedValues.Rows.Count > 0 && !rbtnDeptType.SelectedValue.Equals("ST"))
            {
                foreach (DataRow dr in DDLVessel.SelectedValues.Rows)
                {
                    sbFilterFlt.Append(dr[0]);
                    sbFilterFlt.Append(",");
                }

                if (sbFilterFlt.Length > 1)
                {
                    sbFilterFlt.Remove(sbFilterFlt.Length - 1, 1);
                    filter = string.Format(" Vessel_Code in (" + sbFilterFlt.ToString() + ")");
                }

                dtCatalog.DefaultView.RowFilter = filter;

            }

            if (dtCatalog.DefaultView.Count > 0)
            {
                ddlCatalogue.Items.Clear();
                ddlCatalogue.DataTextField = "system_description";
                ddlCatalogue.DataValueField = "system_code";
                ddlCatalogue.DataSource = dtCatalog.DefaultView.ToTable();
                ddlCatalogue.DataBind();
                ListItem li = new ListItem("SELECT ALL", "0");
                ddlCatalogue.Items.Insert(0, li);

            }

        }

        catch (Exception ex)
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        BindVesselDDL();
    }



    protected void btnSearch_Click(object s, EventArgs e)
    {
        BindItems();
    }

    public void BindItems()
    {
        int Record_Count = 0;
        int Latest = chkLatest.Checked == true ? 1 : 0;
        gvPurcItems.DataSource = BLL_PURC_Report.Get_Items_Quantity_List(DDLFleet.SelectedValues, DDLVessel.SelectedValues, UDFLib.ConvertStringToNull(txtSearchItems.Text), UDFLib.ConvertStringToNull(ddlDepartment.SelectedValue),
                                                                        UDFLib.ConvertStringToNull(ddlCatalogue.SelectedValue), UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue), UDFLib.ConvertIntegerToNull(Latest), ucpItems.CurrentPageIndex, ucpItems.PageSize, ref Record_Count);
        gvPurcItems.DataBind();

        ucpItems.CountTotalRec = Record_Count.ToString();
        ucpItems.BuildPager();

    }


    protected void btnExport_Click(object s, EventArgs e)
    {
        int Latest = chkLatest.Checked == true ? 1 : 0;
        int Record_Count = 0;
        DataTable dtItems = BLL_PURC_Report.Get_Items_Quantity_List(DDLFleet.SelectedValues, DDLVessel.SelectedValues, UDFLib.ConvertStringToNull(txtSearchItems.Text), UDFLib.ConvertStringToNull(ddlDepartment.SelectedValue),
                                                                         UDFLib.ConvertStringToNull(ddlCatalogue.SelectedValue), UDFLib.ConvertStringToNull(ddlSubCatalogue.SelectedValue), UDFLib.ConvertIntegerToNull(Latest), null, null, ref Record_Count);
        string[] HeaderCaptions = { };
        string[] DataColumnsName = { };
        string FileName = "";
        string FileHeaderName = "";

        HeaderCaptions = new string[] { "Vessel", "Catalogue", "Sub Catalogue", "Draw. No.", "Part Number", "Short Description", "Long Description", "Min Qty", "Max Qty", "Effective Date" };
        DataColumnsName = new string[] { "Vessel_Name", "System_Description", "Subsystem_Description", "Drawing_Number", "Part_Number", "Short_Description", "Long_Description", "Min_Qty", "Max_Qty", "Effective_Date" };
        FileHeaderName = "Items Min Max Quantity";
        FileName = "MinMaxQuantity";


        GridViewExportUtil.ShowExcel(dtItems, HeaderCaptions, DataColumnsName, FileName, FileHeaderName);



    }
}