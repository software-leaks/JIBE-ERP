using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using System.Web.UI.HtmlControls;
using SMS.Business.PMS;
using System.Configuration;

public partial class MachineryInformation : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                BindFleetDLL();
                BindVesselDDL();
                DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
                Bindcmb_Function();
                ViewState["DeptType"] = null;
                ViewState["DeptCode"] = null;
                //To display Active Records
                ViewState["ActiveStatus"] = 1;
                ViewState["VesselCode"] = 0;

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;


                BindDepartment();
                BindLocation();

                BindMachineryInfo();



            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    /// <summary>
    /// Bind department using dept_name & Id
    /// </summary>
    private void BindDepartment()
    {
        try
        {
            BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase();
            DataTable dtDepartment = new DataTable();
            dtDepartment = objBLLPURC.SelectDepartment();

            dtDepartment.DefaultView.RowFilter = "Form_Type='SP'";

            cmbDept.DataTextField = "Name_Dept";
            cmbDept.DataValueField = "ID";
            cmbDept.DataSource = dtDepartment;
            cmbDept.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// For binding location using parent type & search keyword
    /// </summary>
    private void BindLocation()
    {
        try
        {
            BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase();
            DataTable dtlocation = objBLLPURC.LibraryGetSystemParameterList("1", "");

            ddllocation.DataTextField = "DESCRIPTION";
            ddllocation.DataValueField = "CODE";
            ddllocation.DataSource = dtlocation;
            ddllocation.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Binding machinery info
    /// </summary>
    /// <param name="search">search keyword</param>
    /// <param name="systemcode">System Id</param>
    /// <param name="systemdesc">description</param>
    /// <param name="systemdesc_FilterType"></param>
    /// <param name="deptType"></param>
    /// <param name="Dept">department</param>
    /// <param name="fleetcode">fleetcode</param>
    /// <param name="ddlvessel">vessel</param>
    /// <param name="maker">maker</param>
    /// <param name="Function">fuction name</param>
    /// <param name="Location">location</param>
    /// <param name="maker_FilterType"></param>
    /// <param name="IsActive">Active status</param>
    /// <param name="sortby">sort by</param>
    /// <param name="sortdirection">sort direction</param>
    /// <param name="pagenumber">pagenumber</param>
    /// <param name="pagesize">return page size</param>
    /// <param name="isfetchcount">fetch count</param>
    /// <returns></returns>
    public void BindMachineryInfo()
    {
        try
        {
            BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();
            int rowcount = ucCustomPagerItems.isCountRecord;



            int? isactivestatus = null;
            if (optdisplayRecordType.SelectedValue != "2")
                isactivestatus = Convert.ToInt32(optdisplayRecordType.SelectedValue);

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

            //string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            // int? sortdirection = null;
            if (ViewState["SORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataSet ds = objJob.LibraryMachineryInforSearch(txtsearchtext.Text != "" ? txtsearchtext.Text : null, null, null, null
                   , "SP", UDFLib.ConvertIntegerToNull(cmbDept.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                   , null, UDFLib.ConvertIntegerToNull(cmb_Function.SelectedValue), UDFLib.ConvertIntegerToNull(ddllocation.SelectedValue), null
                   , isactivestatus
                   , sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


            if (ucCustomPagerItems.isCountRecord == 1)
            {
                ucCustomPagerItems.CountTotalRec = rowcount.ToString();
                ucCustomPagerItems.BuildPager();
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCatalogue.DataSource = ds.Tables[0];
                    gvCatalogue.DataBind();

                    ViewState["ExportData"] = ds.Tables[0];
                }
                else
                {
                    gvCatalogue.DataSource = ds.Tables[0];
                    gvCatalogue.DataBind();
                    ViewState["ExportData"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    /// <summary>
    /// Binding function
    /// </summary>
    public void Bindcmb_Function()
    {
        try
        {
            BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

            DataTable dt = objBLLPurc.LibraryGetSystemParameterList("115", null);

            cmb_Function.DataSource = dt;
            cmb_Function.DataTextField = "DESCRIPTION";
            cmb_Function.DataValueField = "CODE";
            cmb_Function.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }

    /// <summary>
    /// Binding Fleet
    /// </summary>
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
            throw ex;
        }
    }

    /// <summary>
    /// Binding vessel
    /// </summary>
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
            throw ex;
        }
    }

    //public void BindVesselDDL()
    //{
    //    try
    //    {
    //        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

    //        DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(DDLFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
    //        ucDDLVessel.DataTextField = "Vessel_name";
    //        ucDDLVessel.DataValueField = "Vessel_id";
    //        ucDDLVessel.DataSource = dtVessel;
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}


    //public void FillDDL()
    //{
    //    try
    //    {

    //        BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

    //        DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
    //        DDLFleet.DataSource = FleetDT;
    //        DDLFleet.DataTextField = "Name";
    //        DDLFleet.DataValueField = "code";
    //        DDLFleet.DataBind();


    //        DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));

    //        ucDDLVessel.DataTextField = "Vessel_name";
    //        ucDDLVessel.DataValueField = "Vessel_id";
    //        ucDDLVessel.DataSource = dtVessel;


    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindVesselDDL();

    }

    protected void gvCatalogue_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblCalogueActiveSatus = (Label)e.Row.FindControl("lblCalogueActiveSatus");
            Label lblModel = (Label)e.Row.FindControl("lblModel");

            Label lblMaker = (Label)e.Row.FindControl("lblMaker");
            Label lblParticulars = (Label)e.Row.FindControl("lblParticulars");


            Label lblMakerFullDetails = (Label)e.Row.FindControl("lblMakerFullDetails");
            Label lblParticularsFullDetails = (Label)e.Row.FindControl("lblParticularsFullDetails");


            if (lblMakerFullDetails.Text.Length > 20)
                lblMaker.Text = lblMaker.Text + "..";

            if (lblParticularsFullDetails.Text.Length > 20)
                lblParticulars.Text = lblParticulars.Text + "..";



            lblParticulars.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Particulars] body=[" + lblParticularsFullDetails.Text + "]");
            lblMaker.Attributes.Add("Title", "cssbody=[dvbdy1] cssheader=[dvhdr1] header=[Maker] body=[" + lblMakerFullDetails.Text + "]");

            Int64 result = 0;
            if (Int64.TryParse(lblCalogueActiveSatus.Text, out result))
            {
                e.Row.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                //lnkSystemName.ForeColor = (result == 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
            }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "~/purchase/Image/arrowUp.png";
                    else
                        img.Src = "~/purchase/Image/arrowDown.png";

                    img.Visible = true;
                }
            }
        }

    }

    protected void imgCatalogueSearch_Click(object sender, ImageClickEventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;
        BindMachineryInfo();
    }



    protected void btnRetrieve_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;
            BindMachineryInfo();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ucCustomPagerItems.isCountRecord = 1;
            DDLFleet.SelectedValue = "0";
            optdisplayRecordType.SelectedValue = "1";

            BindVesselDDL();

            txtsearchtext.Text = "";
            ddllocation.SelectedValue = "0";
            DDLVessel.SelectedValue = "0";
            cmb_Function.SelectedValue = "0";
            cmbDept.SelectedValue = "0";

            BindMachineryInfo();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void gvCatalogue_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;

            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;

            BindMachineryInfo();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }

    }

    private void SetCatalogueRowSelection()
    {
        gvCatalogue.SelectedIndex = -1;
        for (int i = 0; i < gvCatalogue.Rows.Count; i++)
        {
            if (gvCatalogue.DataKeys[i].Value.ToString().Equals(ViewState["SystemId"].ToString()))
            {
                gvCatalogue.SelectedIndex = i;
            }
        }
    }

    protected void lnbHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Infrastructure/DashBoard_Common.aspx");
        Response.Redirect(ConfigurationManager.AppSettings["DeafaultURL"]);
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            int rowcount = ucCustomPagerItems.isCountRecord;

            BLL_PMS_Library_Jobs objJob = new BLL_PMS_Library_Jobs();

            int? isactivestatus = null;
            if (optdisplayRecordType.SelectedValue != "2")
                isactivestatus = Convert.ToInt32(optdisplayRecordType.SelectedValue);

            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());

            int? sortdirection = null;
            if (ViewState["SORTDIRECTION"] != null)
                sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


            DataSet ds = objJob.LibraryMachineryInforSearch(txtsearchtext.Text != "" ? txtsearchtext.Text : null, null, null, null
                     , "SP", UDFLib.ConvertIntegerToNull(cmbDept.SelectedValue), UDFLib.ConvertIntegerToNull(DDLFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
                     , null, UDFLib.ConvertIntegerToNull(cmb_Function.SelectedValue), UDFLib.ConvertIntegerToNull(ddllocation.SelectedValue), null
                     , isactivestatus
                   , sortbycoloumn, sortdirection, null, null, ref  rowcount);

            string[] HeaderCaptions = { "Vessel", "Function", "Department", "Machinery Name", "Location", "Model", "Sets", "Serial Number", "Particulars", "Maker" };
            string[] DataColumnsName = { "Vessel_Name", "Function_Name", "Department", "System_Name", "Location_Name", "Model", "Set_Instaled", "SrNumber", "Particulars_Full_Details", "MakerName" };

            GridViewExportUtil.ShowExcel(ds.Tables[0], HeaderCaptions, DataColumnsName, "MachineryInformation", "Machinery Information", "");
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
}