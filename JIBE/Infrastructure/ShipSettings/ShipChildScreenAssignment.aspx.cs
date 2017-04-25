using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.PMS;
using System.Text;
using SMS.Properties;



public partial class Infrastructure_ShipSettings_ShipChildScreenAssignment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            ucCustomPager.PageSize = 30;
            BindAssignScreen();
        }

    }


    public void BindAssignScreen()
    {
        int rowcount = ucCustomPager.isCountRecord;
        
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        string Class_Name = Request.QueryString["Class_Name"].ToString();

        //DataTable dt = BLL_Infra_ShipSettings.Get_Screen_Assign_Search(txtSearch.Text, Class_Name, sortbycoloumn, sortdirection
        //     , ucCustomPager.CurrentPageIndex, ucCustomPager.PageSize, ref rowcount);

        DataTable dt = BLL_Infra_ShipSettings.Get_Screen_Assign_Search(txtSearch.Text, Class_Name, sortbycoloumn, sortdirection
           , null, null, ref rowcount);


        //if (ucCustomPager.isCountRecord == 1)
        //{
        //    ucCustomPager.CountTotalRec = rowcount.ToString();
        //    ucCustomPager.BuildPager();
        //}

        if (dt.Rows.Count > 0)
        {
            gvAssignScreen.DataSource = dt;
            gvAssignScreen.DataBind();
        }
        else
        {
            gvAssignScreen.DataSource = dt;
            gvAssignScreen.DataBind();
        }
    }

     
    protected void imgScreenSearch_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvAssignScreen_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvAssignScreen_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvAssignScreen_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gvAssignScreen_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        foreach (GridViewRow gr in gvAssignScreen.Rows)
        {
            CheckBox chkAssingScreen = (CheckBox)gr.FindControl("chkAssingScreen");

            string lblScreen_ID = ((Label)gr.FindControl("lblScreen_ID")).Text;
            string lblNavModule_ID = ((Label)gr.FindControl("lblNavModule_ID")).Text;


            if ((chkAssingScreen.Checked == true || lblNavModule_ID != "0") && chkAssingScreen.Enabled == true)
            {

                int retval = BLL_Infra_ShipSettings.Ins_Upd_Assign_Child_Screen(Convert.ToInt32(Request.QueryString["Nav_Module_ID"].ToString()), chkAssingScreen.Checked ==true ? 1:0
                    , Convert.ToInt32(lblScreen_ID), Convert.ToInt32(Session["userid"].ToString()));

            }
        }


    }
}