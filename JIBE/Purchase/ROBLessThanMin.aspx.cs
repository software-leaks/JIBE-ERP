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
using SMS.Properties;
using SMS.Business.Infrastructure;
public partial class ROBLessThanMin : System.Web.UI.Page
{
     
    string DeptID;
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    BLL_PURC_Purchase objBLLPurc = new BLL_PURC_Purchase();

    
    public string OperationMode = "";

    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        UserAccessValidation();

        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            ucCustomPagerItems.PageSize = 20;


    

            BindFleetDLL();
            DDLFleet.SelectedValue = Session["USERFLEETID"].ToString();
            BindVesselDDL();

            optList.SelectedValue = "SP";

            BindDeptType();

            optList_SelectedIndexChanged(null, null);
            cmbDept_OnSelectedIndexChanged(null, null);

            BindrgdROB();



        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx");

        if (objUA.Add == 0)   
        if (objUA.Edit == 1)
            uaEditFlag = true;
        if (objUA.Delete == 1) uaDeleteFlage = true;


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
                if (vsl != "0")
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
                
            }

        }
        catch (Exception ex)
        {

        }

    }


    public void BindrgdROB()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = objBLLPurc.GET_ROB_Less_Min_Quantity_Search(txtSearchName.Text != "" ? txtSearchName.Text : null, UDFLib.ConvertStringToNull(optList.SelectedValue)
            ,null,null,sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            rgdROB.DataSource = dt;
            rgdROB.DataBind();
        }
        else
        {
            rgdROB.DataSource = dt;
            rgdROB.DataBind();
        }

  
    }

   

    private void BindDepartmentByST_SP()
    {
        try
        {

            using (BLL_PURC_Purchase objBLLPURC = new BLL_PURC_Purchase())
            {
                DataTable dtDepartment = new DataTable();
                dtDepartment = objBLLPURC.SelectDepartment();

                if (optList.SelectedItem != null)
                {
                    ViewState["DeptType"] = optList.SelectedValue;
                  

                    if (optList.SelectedItem.Text == "Spares")
                    {
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                    }
                    else if (optList.SelectedItem.Text == "Stores")
                    {
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                    }
                    else if (optList.SelectedItem.Text == "Repairs")
                    {
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + optList.SelectedValue + "'";
                    }
                    cmbDept.Items.Clear();
                    cmbDept.DataSource = dtDepartment;
                    cmbDept.AppendDataBoundItems = true;
                    ListItem item = new ListItem();
                    item.Value = "ALL";
                    item.Text = "SELECT";
                    cmbDept.Items.Add(item);
                    cmbDept.DataTextField = "Name_Dept";
                    cmbDept.DataValueField = "Code";
                    cmbDept.DataBind();


                    //-----------catalog department------
                    cmbDept.Items.Clear();
                    cmbDept.DataSource = dtDepartment;
                    cmbDept.AppendDataBoundItems = true;
                    cmbDept.DataTextField = "Name_Dept";
                    cmbDept.DataValueField = "Code";
                    cmbDept.DataBind();
                    cmbDept.Items.Insert(0, new ListItem("SELECT", "0"));
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

    protected void optList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ucCustomPagerItems.isCountRecord = 1;


        BindDepartmentByST_SP();

        DDLVessel.SelectedValue = "0";
        ViewState["VesselCode"] = DDLVessel.SelectedValue.ToString();
 
    }

  


    protected void DDLFleet_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindVesselDDL();
        cmbDept.SelectedIndex = 0;

    }
  
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
       
        optList.SelectedIndex = 0;
        txtSearchName.Text = "";
        BindrgdROB();

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
       BindrgdROB();
    }


    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        DataTable dt = objBLLPurc.GET_ROB_Less_Min_Quantity_Search(txtSearchName.Text != "" ? txtSearchName.Text : null, UDFLib.ConvertStringToNull(optList.SelectedValue), null, null, sortbycoloumn, sortdirection
          , null, null, ref  rowcount);
         
        string[] HeaderCaptions = {"Vessel", "System", "Part no.", "Drawing No.", "Item Name" , "ROB Qty", "Min Qty" };
        string[] DataColumnsName = { "Vessel_Name", "System_Description", "Part_Number", "Drawing_Number", "Inventory_Qty", "Inventory_Min" };

        GridViewExportUtil.ShowExcel(dt, HeaderCaptions, DataColumnsName, "ROBLessThanMinQty", "ROBLessThanMinQty", "");

    }


 
    protected void rgdROB_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void rgdROB_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
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
        
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }
    }

    protected void rgdROB_Sorting(object sender, GridViewSortEventArgs se)
    {

        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindrgdROB();

    }


    
}
