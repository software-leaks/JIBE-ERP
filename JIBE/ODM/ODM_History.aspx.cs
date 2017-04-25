using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using SMS.Business.PURC;
using SMS.Business.Infrastructure;
using SMS.Business.ODM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class ODM_ODM_History : System.Web.UI.Page
{
    //public Boolean uaEditFlag = false;
    //public Boolean uaDeleteFlage = false;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_ODM objODM = new BLL_ODM();
    protected void Page_Load(object sender, EventArgs e)
    {
        //UserAccessValidation();
        if (!IsPostBack)
        {
            DateTime dt = System.DateTime.Now;
            DateTime Fistdaydate = dt.AddDays(-(dt.Day - 1));

            DateTime LastdayDate = dt.AddMonths(1);
            LastdayDate = LastdayDate.AddDays(-(LastdayDate.Day));
            txtStartDate.Text = Fistdaydate.ToString("dd-MM-yyyy");
            txtEndDate.Text = LastdayDate.ToString("dd-MM-yyyy");
           
                Load_VesselList();
                GetVesselID();
                Load_DepartMent();
                if (Session["Vessel_ID"] != null)
                {
                    ddlvessel.SelectedValue = Session["Vessel_ID"].ToString();
                }

                ViewState["SORTDIRECTION"] = null;
                ViewState["SORTBYCOLOUMN"] = null;
                            
                BindPortODMHistory();
            
        }
    }


    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        // if (objUA.Add == 0) ImgAdd.Visible = false;
        //if (objUA.Edit == 1)
        //    uaEditFlag = true;
        //else
        //    // btnsave.Visible = false;
        //    if (objUA.Delete == 1) uaDeleteFlage = true;

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void BindPortODMHistory()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dtDepartment = new DataTable();
        dtDepartment.Columns.Add("ID");
        if (chkListDepartment.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkListDepartment.Items)
            {
                DataRow dr = dtDepartment.NewRow();
                if (chkitem.Selected)
                {
                   
                    dr["ID"] = chkitem.Value;
                    dtDepartment.Rows.Add(dr);
                }
                
            }
            DataRow dr1 = dtDepartment.NewRow();
            if (dtDepartment.Rows.Count == 0)
            {
                dr1["ID"] = "0";
                dtDepartment.Rows.Add(dr1);
            }


        }

        int RecordCount=0;
        DataTable dtODMList = new DataTable();
        dtODMList = objODM.Search_ODM_History(UDFLib.ConvertIntegerToNull(ddlvessel.SelectedValue), txtSeachText.Text,  UDFLib.ConvertDateToNull(txtStartDate.Text), UDFLib.ConvertDateToNull(txtEndDate.Text), dtDepartment, sortbycoloumn, sortdirection, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref RecordCount);


        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = RecordCount.ToString();
            ucCustomPagerItems.BuildPager();
        }
        gvODMHistory.DataSource = dtODMList;
        gvODMHistory.DataBind();
        iFrame.Visible = false;
        lblRecordCount.Text = RecordCount +" Record(s) found.";

    }




    protected void gvODMHistory_RowDataBound(object sender, GridViewRowEventArgs e)
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

            Label lblVessel = (Label)e.Row.FindControl("lblVessel");
            DataRow drv = ((DataRowView)e.Row.DataItem).Row;

            string vesselList = drv["VesselList"].ToString();

            if (vesselList != null && vesselList != "")
            {
                if (vesselList.Length > 50)
                {
                    lblVessel.Text = vesselList.Substring(0, 49) + "...";

                }
                else
                    lblVessel.Text = vesselList.ToString();

                lblVessel.ToolTip = vesselList.ToString();
            }

            e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.backgroundColor='#FEECEC';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.color='black';this.style.backgroundColor=''";
        }
    }

    protected void gvODMHistory_Sorting(object sender, GridViewSortEventArgs se)
    {
        ViewState["SORTBYCOLOUMN"] = se.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPortODMHistory();
    }
    protected void Load_VesselList()
    {
        DataTable dt = objBLL.Get_VesselList(0, 0, 0, "", 1);

        ddlvessel.DataSource = dt;
        ddlvessel.DataTextField = "VESSEL_NAME";
        ddlvessel.DataValueField = "VESSEL_ID";
        ddlvessel.DataBind();
        ddlvessel.Items.Insert(0, new ListItem("-All Vessel-", "0")); 
    }


    protected void Load_DepartMent()
    {
        DataTable dt = objODM.Get_ODM_Departments();
        chkListDepartment.DataSource = dt;
        chkListDepartment.DataTextField = "Deapartment_Name";
        chkListDepartment.DataValueField = "ID";
        chkListDepartment.DataBind();
        if(chkListDepartment.Items.FindByText("TECHNICAL") != null)
            chkListDepartment.Items.FindByText("TECHNICAL").Selected=true;
        
    }

    public int GetVesselID()
    {
        try
        {

            if (Session["Vessel_ID"] != null)
            {
                return int.Parse(Session["Vessel_ID"].ToString());
            }

            else
                return 0;
        }
        catch { return 0; }
    }

    protected void btnODMMain_Click(object sender, EventArgs e)
    {
        Response.Redirect("../ODM/Daily_Message_Queue.aspx");
    }
    protected void btnportfilter_Click(object sender, ImageClickEventArgs e)
    {
        BindPortODMHistory();
    }





    protected void gvODMHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            iFrame.Visible = true;
            int GroupID = UDFLib.ConvertToInteger(e.CommandArgument.ToString());
            iFrame.Attributes["src"] = "ODM_Detail.aspx?ID=" + GroupID.ToString();


        }
    }
    protected void btnportfilter_Click1(object sender, EventArgs e)
    {
        BindPortODMHistory();
    }
}