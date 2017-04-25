using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Technical;

public partial class Technical_Worklist_SupdtInspReport : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();

    protected void Page_Load(object sender, EventArgs e)
    {
        Search_Worklist();

        BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();
        DataTable dt = objBLL.Generate_Report(UDFLib.ConvertToInteger(Request.QueryString["SchDetailId"]), UDFLib.ConvertToInteger(Request.QueryString["ShowImages"]));
        dvinspectionReport.InnerHtml = dt.Rows[0][0].ToString();
    }


    protected void Search_Worklist()
    {
        try
        {
            
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("All", typeof(int));
            dtStatus.Columns.Add("Pending", typeof(int));
            dtStatus.Columns.Add("Completed", typeof(int));
            dtStatus.Columns.Add("Reworked", typeof(int));
            dtStatus.Columns.Add("Verified", typeof(int));
            dtStatus.Columns.Add("Overdue", typeof(int));

            //string JobStaus ="";

            dtStatus.Rows.Add(1,0,0,0,0,0);

           
            DataTable dtFilter = new DataTable();
            dtFilter.Columns.Add("PRM_NAME", typeof(string));
            dtFilter.Columns.Add("PRM_VALUE", typeof(object));

            dtFilter.Rows.Add(new object[] { "@FLEET_ID", null });
            dtFilter.Rows.Add(new object[] { "@VESSEL_ID", null });
            dtFilter.Rows.Add(new object[] { "@ASSIGNOR", null });
            dtFilter.Rows.Add(new object[] { "@DEPT_SHIP", null });
            dtFilter.Rows.Add(new object[] { "@DEPT_OFFICE", null });
            dtFilter.Rows.Add(new object[] { "@PRIORITY", null });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_NATURE", null });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_PRIMARY", null });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_SECONDARY", null });
            dtFilter.Rows.Add(new object[] { "@CATEGORY_MINOR", null, });
            dtFilter.Rows.Add(new object[] { "@JOB_DESCRIPTION", null });
            dtFilter.Rows.Add(new object[] { "@JOB_STATUS", null });

            dtFilter.Rows.Add(new object[] { "@dtJOB_Status", dtStatus });

            dtFilter.Rows.Add(new object[] { "@JOB_TYPE", 0 });
            dtFilter.Rows.Add(new object[] { "@PIC", null });
            dtFilter.Rows.Add(new object[] { "@JOB_MODIFIED_IN", null });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_FROM", null });
            dtFilter.Rows.Add(new object[] { "@DATE_RAISED_TO", null });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_FROM", null });
            dtFilter.Rows.Add(new object[] { "@DATE_CMPLTN_TO", null });
            dtFilter.Rows.Add(new object[] { "@DEFER_TO_DD", null });
            dtFilter.Rows.Add(new object[] { "@SENT_TO_SHIP", null });
            dtFilter.Rows.Add(new object[] { "@HAVING_REQ_NO", null });
            dtFilter.Rows.Add(new object[] { "@FLAGGED_FOR_MEETING", null });
            dtFilter.Rows.Add(new object[] { "@INSPECTOR", null });
            dtFilter.Rows.Add(new object[] { "@PAGE_INDEX", null });
            dtFilter.Rows.Add(new object[] { "@PAGE_SIZE", null });
            dtFilter.Rows.Add(new object[] { "@Company_ID", null });          
            dtFilter.Rows.Add(new object[] { "@WL_TYPE", null });
            dtFilter.Rows.Add(new object[] { "@InspectionID", null });
            //if (ViewState["SchDetailId"] != null)
            //{
            //    dtFilter.Rows.Add(new object[] { "@InspectionID", Convert.ToInt32(ViewState["SchDetailId"].ToString()) });
            //}
           
            dtFilter.Rows.Add(new object[] { "@SortBy", null });
            dtFilter.Rows.Add(new object[] { "@SORT_DIRECTION", null });
            int Record_Count = 0;

            DataTable taskTable = objBLL.Get_WorkList_Index(dtFilter, ref Record_Count);

            DataTable dtPKIDs = taskTable.DefaultView.ToTable(true, new string[] { "WORKLIST_ID", "VESSEL_ID", "OFFICE_ID" });
            dtPKIDs.PrimaryKey = new DataColumn[] { dtPKIDs.Columns["WORKLIST_ID"], dtPKIDs.Columns["VESSEL_ID"], dtPKIDs.Columns["OFFICE_ID"] };
            Session["WORKLIST_PKID_NAV"] = dtPKIDs;
          
        }
        catch (Exception ex)
        {
            //////.WriteError(this.GetType().Name.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name.ToString(), ex);
            //string js = "alert('Error in loading data!! Error: " + UDFLib.ReplaceSpecialCharacter(ex.Message) + "');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
}