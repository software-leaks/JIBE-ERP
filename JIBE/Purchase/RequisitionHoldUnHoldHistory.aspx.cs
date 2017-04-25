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
using SMS.Business.Infrastructure;

public partial class RequisitionHoldUnHoldHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindFleetDLL();
            ddlFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            BindLineTypeDLL();
            //BindRequisitionGrid();
            BindViewReqistiononHold();
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        
        clear();
        //BindRequisitionGrid();
        BindViewReqistiononHold();
    }

    protected void clear()
    {
      
        txtReqCode.Text = "";
        ddlLinetype.SelectedIndex = 0;
        txtdocument.Text = "";
        cmbhold.SelectedValue = "2";
        ddlFleet.SelectedIndex = 0;
        BindVesselDDL();
        txtUsername.Text = "";
 
    }

    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            ddlFleet.Items.Clear();
            ddlFleet.DataSource = FleetDT;
            ddlFleet.DataTextField = "Name";
            ddlFleet.DataValueField = "code";
            ddlFleet.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            ddlFleet.Items.Insert(0, li);
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

            DataTable dtVessel = objVsl.Get_VesselList(UDFLib.ConvertToInteger(ddlFleet.SelectedValue), 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
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


    protected void BindLineTypeDLL()
    {
        try
        {
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                ddlLinetype.DataSource = objTechService.GetREQStatus();
                ddlLinetype.DataTextField = "Description";
                ddlLinetype.DataValueField = "short_code";
                ddlLinetype.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

   

    public void BindViewReqistiononHold()
    {
        int? ddhold = null;
        if (cmbhold.SelectedValue != "2")
            ddhold = UDFLib.ConvertToInteger(cmbhold.SelectedValue);
        //if (cmbhold.SelectedValue == "0")
        //    ddhold = UDFLib.ConvertToInteger(cmbhold.SelectedValue);
        //if (cmbhold.SelectedValue == "1")



            //ddhold = UDFLib.ConvertToInteger(cmbhold.SelectedValue);
        int rowcount = ucCustomPagerItems.isCountRecord;
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
 
        BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase();
        DataTable dt = objTechService.GetRequisitionOnHoldLogHistory(UDFLib.ConvertIntegerToNull(ddlFleet.SelectedValue), UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
            , UDFLib.ConvertStringToNull(ddlLinetype.SelectedValue), txtReqCode.Text != "" ? txtReqCode.Text : null
            , txtUsername.Text.Trim() != "" ? txtUsername.Text.Trim() : null, txtdocument.Text.Trim() != "" ? txtdocument.Text.Trim() : null
            , ddhold, sortbycoloumn, sortdirection
            , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
       


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

        if (dt.Rows.Count > 0)
        {
            rgdonholdgrd.DataSource = dt;
            rgdonholdgrd.DataBind();
        }
        else
        {
            rgdonholdgrd.DataSource = dt;
            rgdonholdgrd.DataBind();
        }
    }

    protected void BindRequisitionGrid()
    {
        try
        {
            string filter = "";

            if (ddlFleet.SelectedValue != "0")
            {
                filter = filter + "FleetCode =" + ddlFleet.SelectedValue.ToString() + " and ";
            }

            if (DDLVessel.SelectedValue != "0")
            {
                filter = filter + "Vessel_Code =" + DDLVessel.SelectedValue.ToString() + " and ";
            }
            if (txtReqCode.Text.Trim() != "")
            {
                filter = filter + "Requisition_Code Like '%" + txtReqCode.Text.Trim() + "%'" + " and ";
            }
            if (ddlLinetype.SelectedValue != "0")
            {
                filter = filter + "LineTypeCode  ='" + ddlLinetype.SelectedValue.ToString() + "'" + " and ";
            }
            if (txtUsername.Text.Trim() != "")
            {
                filter = filter + "User_name Like '%" + txtUsername.Text.Trim() + "%'" + " and ";
            }
            if (txtdocument.Text.Trim() != "")
            {
                filter = filter + "Document_Code Like '%" + txtdocument.Text.Trim() + "%'" + " and ";
            }
            if (cmbhold.SelectedValue.ToString() != "0")
            {
                filter = filter + "OnHold = '" + cmbhold.SelectedItem.ToString() + "' " + " and ";
            }
            if (filter.Length > 10)
            {
                filter = filter.Remove(filter.Length - 4, 4);
            }
            using (BLL_PURC_Purchase objTechService = new BLL_PURC_Purchase())
            {
                DataTable dt = new DataTable();
                dt = objTechService.GetRequisitionOnHoldLogHistory();
                if (filter != "")
                {
                    dt.DefaultView.RowFilter = filter;
                    rgdonholdgrd.DataSource = dt.DefaultView;
                }
                if (filter == "")
                {
                    rgdonholdgrd.DataSource = dt.DefaultView;
                }

                rgdonholdgrd.DataBind();
            }
        }
        catch (Exception ex)
        {

        }

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
    
        BindViewReqistiononHold();

    }

    protected void rgdonholdgrd_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        BindViewReqistiononHold();

    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindVesselDDL();
    }
    
    protected void rgdonholdgrd_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    
}



