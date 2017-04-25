using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.PURC;
using System.Text;

using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Data;


public partial class Purchase_PURC_Items_Inventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();
            BindDeptType();

            gvInventoryItems.Attributes.Add("bordercolor", "#D8D8D8");
            optList_SelectedIndexChanged(null, null);
            cmbDept_OnSelectedIndexChanged(null, null);
            btnSearch_Click(null, null); 
            //BindItems();
        }


    }


    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.Items.Clear();
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLFleet.Items.Insert(0, li);
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

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
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
                optList.DataSource = DeptDt;
                optList.DataTextField = "Description";
                optList.DataValueField = "Short_Code";
                optList.DataBind();
                optList.SelectedIndex = 0;
            }
 
        }
        catch (Exception ex)
        {

        }
     
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ucCustomPagerItems.RecordCountCaption = "Total Items";

        BindItems();

    }

    protected void BindItems()
    {

        int IsFetch_Count = ucCustomPagerItems.isCountRecord;

        DataTable dtItems = BLL_PURC_Common.Get_Inventory_Item_List(Convert.ToInt32(DDLFleet.SelectedValue),
                                                 Convert.ToInt32(DDLVessel.SelectedValue),
                                                  cmbDept.SelectedValue,
                                                  ddlCatalogue.SelectedValue,
                                                  ddlSubCatalogue.SelectedValue,
                                                  txtSrchPartNo.Text.Trim() == "" ? "0" : txtSrchPartNo.Text.Trim(),
                                                  txtDrawno.Text.Trim() == "" ? "0" : txtDrawno.Text.Trim(),
                                                  txtSrchDesc.Text.Trim() == "" ? "0" : txtSrchDesc.Text.Trim(),
                                                  txtFrom.Text.Trim() == "" ? "1900/01/01" : txtFrom.Text.Trim(),
                                                  txtTo.Text.Trim() == "" ? "2099/01/01" : txtTo.Text.Trim(),
                                                  rbtnRob.Checked == true ? 1 : 0,
                                                  rbtnLatest.Checked == true ? 1 : 0,
                                                  chkInventory_Qty_Less.Checked==true?1:0,
                                                  chkInventory_Qty_Greater.Checked==true?1:0,
                                                  ucCustomPagerItems.CurrentPageIndex,
                                                  ucCustomPagerItems.PageSize,
                                                  chkCritcal.Checked == true ? 1 : 0,
                                                  ref IsFetch_Count
                                                 );


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = IsFetch_Count.ToString();
            ucCustomPagerItems.BuildPager();
        }

        gvInventoryItems.DataSource = dtItems;
        gvInventoryItems.DataBind();



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
    /// <summary>
    /// Change if condition ,Checking Department Type short code and fill derpartment according to department type.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objTechService.SelectDepartment();

                if (optList.SelectedValue == "SP")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";


                }
                else if (optList.SelectedValue == "ST")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";



                }
                else if (optList.SelectedValue == "RP")
                {
                    dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";


                }
                cmbDept.Items.Clear();
                cmbDept.DataTextField = "Name_Dept";
                cmbDept.DataValueField = "Code";
                cmbDept.DataSource = dtDepartment;
                cmbDept.DataBind();
                ListItem li = new ListItem("SELECT ALL", "0");
                cmbDept.Items.Insert(0, li);

            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
      
    }

    protected void cmbDept_OnSelectedIndexChanged(object source, System.EventArgs e)
    {
        try
        {


            string filter;


            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dtCatalog = objTechService.SelectCatalog();



                string department = cmbDept.SelectedValue.ToString();
                string vsl = DDLVessel.SelectedValue;
                filter = "1=1";
                if (vsl != "0" && optList.SelectedValue.ToString().ToUpper()!="ST")
                {
                    filter += "  and  Vessel_Code=" + vsl;
                }


                if (department != "0")
                    filter += "  and  Code='" + department + "'";


                dtCatalog.DefaultView.RowFilter = filter;

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
                else
                {



                }


            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }
    /// <summary>
    /// Pass zero for page size and page number
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, ImageClickEventArgs e)
    {

        int IsFetch_Count = ucCustomPagerItems.isCountRecord;


        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dtItems = BLL_PURC_Common.Get_Inventory_Item_List(Convert.ToInt32(DDLFleet.SelectedValue),
                                                 Convert.ToInt32(DDLVessel.SelectedValue),
                                                  cmbDept.SelectedValue,
                                                  ddlCatalogue.SelectedValue,
                                                  ddlSubCatalogue.SelectedValue,
                                                  txtSrchPartNo.Text.Trim() == "" ? "0" : txtSrchPartNo.Text.Trim(),
                                                  txtDrawno.Text.Trim() == "" ? "0" : txtDrawno.Text.Trim(),
                                                  txtSrchDesc.Text.Trim() == "" ? "0" : txtSrchDesc.Text.Trim(),
                                                  txtFrom.Text.Trim() == "" ? "1900/01/01" : txtFrom.Text.Trim(),
                                                  txtTo.Text.Trim() == "" ? "2099/01/01" : txtTo.Text.Trim(),
                                                  rbtnRob.Checked == true ? 1 : 0,
                                                  rbtnLatest.Checked == true ? 1 : 0,
                                                  chkInventory_Qty_Less.Checked == true ? 1 : 0,
                                                  chkInventory_Qty_Greater.Checked == true ? 1 : 0,
                                                  0,
                                                  0,
                                                  chkCritcal.Checked == true ? 1 : 0,
                                                  ref IsFetch_Count
                                                 );
   

        string[] HeaderCaptions = { "Vessel", "Catalogue", "Sub Catalogue", "Draw. No.","Part Number","Description","Unit","ROB","Update Date","Reqsn. No.","Req Qty.","Adr" };
        string[] DataColumnsName = { "Vessel_Name", "System_Description", "Subsystem_Description", "Drawing_Number", "Part_Number", "Short_Description", "Unit_and_Packings", "Inventory_Qty", "Date_Of_Creatation", "Requisition_Code", "REQUESTED_QTY", "LOCATION" };

        String Header = "Inventry List" + "" + DateTime.Now.ToString();
        GridViewExportUtil.ShowExcel(dtItems, HeaderCaptions, DataColumnsName, "Inventry List", "Inventry List", "");

      

        //GridViewExportUtil.Export("InventoryList" + DateTime.Now.ToShortDateString() + ".xls", this.gvInventoryItems);
    }

    protected void gvInventoryItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (!string.IsNullOrWhiteSpace(UDFLib.ConvertStringToNull(DataBinder.Eval(e.Row.DataItem, "MINMAX_QTY"))))
            {
                (((Label)e.Row.FindControl("lblROB")).Parent as DataControlFieldCell).Attributes.Add("onmousemove", "js_ShowToolTip('<div style=width:100%;height:100%;margin:5px;background-color:transparent;line-height:20px><b> Min Qty : </b>" + DataBinder.Eval(e.Row.DataItem, "MINMAX_QTY").ToString().Split('_')[0] + "<br><b> Max Qty : </b>" + DataBinder.Eval(e.Row.DataItem, "MINMAX_QTY").ToString().Split('_')[1] + "</div>',event,this)");

                (((Label)e.Row.FindControl("lblROB")).Parent as DataControlFieldCell).CssClass = UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "Inventory_Qty")) < UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "MINMAX_QTY").ToString().Split('_')[0]) ? "css-MinQty" : (UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "Inventory_Qty")) > UDFLib.ConvertToDecimal(DataBinder.Eval(e.Row.DataItem, "MINMAX_QTY").ToString().Split('_')[1]) ? "css-MaxQty" : "");
            }

        }


    }
}