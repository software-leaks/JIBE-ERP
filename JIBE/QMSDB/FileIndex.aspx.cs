using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.QMSDB;
using SMS.Business.Infrastructure;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class QMSDB_FileIndex : System.Web.UI.Page
{
   
    BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ucCustomPagerItems.PageSize = 20;
            Session["sFleet"] = DDLFleet.SelectedValues;
            Session["sVesselCode"] = DDLVessel.SelectedValues;
            FillDDL();
            DepartmentList();
            BindPublishDoc();
        }
    }
    public void BindPublishDoc()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dtPendingList = BLL_QMSDB_Procedures.QMSDBProcedures_Search((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], UDFLib.ConvertIntegerToNull(lstDept.SelectedValue), UDFLib.ConvertIntegerToNull(lstUser.SelectedValue),txtSearch.Text, sortbycoloumn,sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        gvProcedureList.DataSource = dtPendingList;
        gvProcedureList.DataBind();
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }
    }
    public void FillDDL()
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
            BindPublishDoc();
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }
    protected void gvProcedureList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";

            ImageButton imgViewDocument = (ImageButton)e.Row.FindControl("imgViewDocument");
            string Procedure_Id = imgViewDocument.CommandArgument.ToString();
            imgViewDocument.Attributes.Add("onclick", "javascript:window.open('ViewProcedures.aspx?PROCEDURE_ID=" + Procedure_Id + "&FileStatus=View'); return false;");

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ViewState["SORTBYCOLOUMN"] != null)
            {
                HtmlImage img = (HtmlImage)e.Row.FindControl(ViewState["SORTBYCOLOUMN"].ToString());
                if (img != null)
                {
                    if (ViewState["SORTDIRECTION"] == null || ViewState["SORTDIRECTION"].ToString() == "0")
                        img.Src = "../Images/arrowUp.png";
                    else
                        img.Src = "../Images/arrowDown.png";

                    img.Visible = true;
                }
            }
        }
    }
    protected void gvProcedureList_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPublishDoc();
    }
    protected void DDLVessel_SelectedIndexChanged()
    {
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        BindPublishDoc();
    }
    protected void DDLFleet_SelectedIndexChanged()
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
        Session["sVesselCode"] = DDLVessel.SelectedValues;
        Session["sFleet"] = DDLFleet.SelectedValues;

        BindPublishDoc();
    }
    protected void ImgExpExcel_Click(object sender, ImageClickEventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dtPendingList = BLL_QMSDB_Procedures.QMSDBProcedures_Search((DataTable)Session["sFleet"], (DataTable)Session["sVesselCode"], UDFLib.ConvertIntegerToNull(lstDept.SelectedValue), UDFLib.ConvertIntegerToNull(lstUser.SelectedValue), txtSearch.Text, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        string[] HeaderCaptions = { "Vessel", "Folder Name", "Procedure Code", "Procedure Name", "Department", "Access User" ,"Publish Date","Publish Version","Created By","Approved By"};
        string[] DataColumnsName = { "Vessel_Name", "Folder_Name", "PROCEDURE_CODE", "PROCEDURES_NAME", "Department", "User_name" ,"CREATED_DATE","PUBLISH_VERSION","CREATED_USER","User_name"};

        GridViewExportUtil.ExportToExcel(dtPendingList, HeaderCaptions, DataColumnsName, "ProcedureList", "Procedure List");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
    }
    protected void DepartmentList()
    {
            int iCompID = int.Parse(Session["USERCOMPANYID"].ToString());
            lstDept.DataSource = objCompBLL.Get_CompanyDepartmentList(iCompID);
            lstDept.DataTextField = "VALUE";
            lstDept.DataValueField = "ID";
            lstDept.DataBind();
            lstDept.Items.Insert(0, new ListItem("- ALL -", "0"));
            lstDept.SelectedIndex = 0;
            lstDept_SelectedIndexChanged(null, null);  
      
    }
    protected void lstDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strDept = "";
        BLL_Infra_UserCredentials objInfra = new BLL_Infra_UserCredentials();

        if (lstDept.Items[0].Selected == true)
        {
            int UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
            DataTable dtUsers = objInfra.Get_UserList(UserCompanyID);
            lstUser.DataSource = dtUsers;
            lstUser.DataBind();
            lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
        }
        else
        {
            foreach (ListItem li in lstDept.Items)
            {
                if (li.Selected == true)
                {
                    if (strDept.Length > 0) strDept += ",";
                    strDept += li.Value;
                }
            }

            if (strDept.Length > 0)
            {
                int UserCompanyID = int.Parse(Session["USERCOMPANYID"].ToString());
                DataTable dtUsers = objInfra.Get_UserList_By_Dept_DL(UserCompanyID, strDept);
                lstUser.DataSource = dtUsers;
                lstUser.DataBind();
                lstUser.Items.Insert(0, new ListItem("- ALL -", "0"));
            }
        }
        BindPublishDoc();

    }
    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        BindPublishDoc();
    }
    protected void lstUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPublishDoc();
    }
}