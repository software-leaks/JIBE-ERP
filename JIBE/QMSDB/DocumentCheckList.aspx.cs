using System;
using System.Data;
using System.Web.UI.WebControls;
using SMS.Business.QMSDB;

public partial class DocumentCheckList : System.Web.UI.Page
{
   
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;
            ucCustomPagerItems.PageSize = 20;
            BindPendingPublishDoc();
        }

        
    }

    public void BindPendingPublishDoc()
    {
        int rowcount = ucCustomPagerItems.isCountRecord;
        DataTable dtPendingList = BLL_QMSDB_Procedures.ProceduresCheckListSearch(5, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref rowcount);
        gvCheckList.DataSource = dtPendingList;
        gvCheckList.DataBind();
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }
    }
    protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
   {
       if (e.Row.RowType == DataControlRowType.DataRow)
       {
          
       }
    }
    protected void gvCheckList_Sorting(object sender, GridViewSortEventArgs e)
    {
        ViewState["SORTBYCOLOUMN"] = e.SortExpression;

        if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
            ViewState["SORTDIRECTION"] = 1;
        else
            ViewState["SORTDIRECTION"] = 0;

        BindPendingPublishDoc();
    }
    /// <summary>
    /// this a method for the convert the date in givien format by the user.
    /// </summary>
    /// <param name="strDT"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ConvertDateToString(string strDT, string format)
    {
        if (strDT != "")
        {
            DateTime dt = Convert.ToDateTime(strDT);
            return dt.ToString(format);
        }
        return "";
    }

    protected void GetFile(object sender, CommandEventArgs e)
    {

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string FileID = commandArgs[0].ToString();       
    }

    protected void DeleteProcedure(object sender, CommandEventArgs e)
    {
        try
        {
            string procedureid = e.CommandArgument.ToString();
            int i = BLL_QMSDB_Procedures.Upd_QMSDBProcedures_Delete(int.Parse(procedureid), int.Parse(Session["USERID"].ToString()));
        }
        catch (Exception ex)
        {
        }
    }
}
