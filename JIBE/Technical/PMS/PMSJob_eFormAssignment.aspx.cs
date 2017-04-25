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



public partial class Technical_PMS_PMSJob_eFormAssignment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            ucCustomPager.PageSize = 30;
            BindeFormAssign();
        }

    }


    public void BindeFormAssign()
    {

        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

        int rowcount = ucCustomPager.isCountRecord;
        
        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());

        int? Vessel_ID = null;
        if (!string.IsNullOrEmpty(Request.QueryString["Vessel_ID"]))
            Vessel_ID = UDFLib.ConvertIntegerToNull(Request.QueryString["Vessel_ID"].ToString());

        int? Job_ID = null;
        if (!string.IsNullOrEmpty(Request.QueryString["Job_ID"]))
            Job_ID = UDFLib.ConvertIntegerToNull(Request.QueryString["Job_ID"].ToString());

        // DataTable dt = objJobs.LibraryGetJOB_eFORM_MAPPING_SEARCH(txtSearch.Text, Vessel_ID, sortbycoloumn, sortdirection
        //     , ucCustomPager.CurrentPageIndex, ucCustomPager.PageSize, ref rowcount);

        DataTable dt = objJobs.LibraryGetJOB_eFORM_MAPPING_SEARCH(txtSearch.Text, Vessel_ID,Job_ID, sortbycoloumn, sortdirection
           , null, null, ref rowcount);


        //if (ucCustomPager.isCountRecord == 1)
        //{
        //    ucCustomPager.CountTotalRec = rowcount.ToString();
        //    ucCustomPager.BuildPager();
        //}

        if (dt.Rows.Count > 0)
        {
            gvAssigneForm.DataSource = dt;
            gvAssigneForm.DataBind();
        }
        else
        {
            gvAssigneForm.DataSource = dt;
            gvAssigneForm.DataBind();
        }
    }

     
    protected void imgScreenSearch_Click(object sender, ImageClickEventArgs e)
    {

        BindeFormAssign();

    }
    protected void gvAssigneForm_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void gvAssigneForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvAssigneForm_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gvAssigneForm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

        foreach (GridViewRow gr in gvAssigneForm.Rows)
        {
            CheckBox chkAssingeForm = (CheckBox)gr.FindControl("chkAssingeForm");

            string lblForm_ID = ((Label)gr.FindControl("lblForm_ID")).Text;
            string lblJob_ID = ((Label)gr.FindControl("lblJob_ID")).Text;
            string lbl_ID = ((Label)gr.FindControl("lbl_ID")).Text;
  
            if ((chkAssingeForm.Checked == true || lblJob_ID != "") && chkAssingeForm.Enabled == true)
            {

                int retval = objJobs.LibrarySaveJob_eForm_Mapping(UDFLib.ConvertIntegerToNull(lbl_ID), UDFLib.ConvertIntegerToNull(Request.QueryString["Vessel_ID"].ToString())
                        , UDFLib.ConvertIntegerToNull(Request.QueryString["Job_ID"].ToString()), UDFLib.ConvertIntegerToNull(lblForm_ID)
                        , chkAssingeForm.Checked == true ? 1 : 0
                        , Convert.ToInt32(Session["userid"].ToString()));

            }
        }


        string msg = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);


    }
}