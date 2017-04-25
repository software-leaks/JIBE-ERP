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
using System.Data.SqlClient;
using SMS.Business.QMS;
using System.Globalization;
using System.Drawing;

public partial class SyncViewLog : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document();
    IFormatProvider iFormatProvider = new System.Globalization.CultureInfo("en-GB", true);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            bindDropDownData();
            GetDefaultDate();
        }
    }

    /// <summary>
    /// bind & display the resultant grid based on the filter critria.
    /// </summary>
    public void BindData()
    {
        
        string queryText = getQuery();
        DataSet dsSyncData = objQMS.getSyncdataFromDB(queryText);
        Session["QMSData"]=dsSyncData;
        GrdSyncQMSlog.DataSource = dsSyncData.Tables[0];
        GrdSyncQMSlog.DataBind();
        lblDocumentCount.Text = dsSyncData.Tables[0].Rows.Count + " Records Found";
    }

    /// <summary>
    /// this is use for the set the default date setting while page loading.
    /// </summary>
    public void GetDefaultDate()
    {
        if (txtFromDate.Text == "" && txtToDate.Text == "")
        {
            txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            DateTime dt = new DateTime(2009, 10, 01);
            txtFromDate.Text = dt.ToString("dd/MM/yyyy");
        }
    }

    /// <summary>
    /// bind the combo box with the Vessel name & Vessel Code from the database.
    /// </summary>
    public void bindDropDownData()
    {
        //binds the vesselCode DropDown here

        ddlVesselCode.Items.Clear();
        DataSet dsVesselList = objQMS.getVesselList();
        ddlVesselCode.DataSource = dsVesselList.Tables[0];
        ddlVesselCode.DataTextField = "Name";
        ddlVesselCode.DataValueField = "Vessel_Code";
        ddlVesselCode.DataBind();
        ddlVesselCode.Items.Insert(0, new ListItem("Select Vessel", "0"));

      

        //binds the Manuals DropDown here
        LoadFoldersFromFilePath();
    }

    /// <summary>
    /// bind the combo box with the folder name by the first hierarchy  
    /// of the saved file path from the database.
    /// </summary>
    protected void LoadFoldersFromFilePath()
    {
        DataSet ds = objQMS.getFilpath();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            string[] DirectoryList = dr[0].ToString().Split('\\');
            if (DirectoryList.Length > 3)
            {
                if (ddlManual1.Items.FindByValue(DirectoryList[4]) == null)
                {
                    ddlManual1.Items.Add(new ListItem(DirectoryList[4], DirectoryList[4]));
                }
            }
        }
        ddlManual1.Items.Insert(0, new ListItem("Select", "Select Manual"));

    }

    /// <summary>
    /// make the query string based on the filter condition.
    /// </summary>
    /// <returns></returns>
    public string getQuery()
    {
        string Querry = "";
        string sFilter = "";

       // Querry = " SELECT LogFileID,LogDate, Convert(varchar,LogDate,103) LogDateText,LOGManuals1,LOGManuals2 from QMSdtls_Log ";

        Querry = @"SELECT     QMSdtls_Log.LogFileID, QMSdtls_Log.LogDate, CONVERT(varchar, QMSdtls_Log.LogDate, 103) AS LogDateText, QMSdtls_Log.LOGManuals1, 
                      QMSdtls_Log.LOGManuals2, 
                    (CRW_Lib_Crew_Details.Staff_Name +' '+CRW_Lib_Crew_Details.Staff_Midname+' '+ 
                                          CRW_Lib_Crew_Details.Staff_Surname) as UserName
                    FROM         QMSdtls_Log INNER JOIN
                                          CRW_Lib_Crew_Details ON QMSdtls_Log.UserID = CRW_Lib_Crew_Details.ID";



        sFilter += " WHERE QMSdtls_Log.Vessel_Code!=0 ";
        if (ddlVesselCode.SelectedIndex > 0)
        {
            sFilter += " and QMSdtls_Log.Vessel_Code= " + ddlVesselCode.SelectedValue;
        }

        if (DDlUser.SelectedIndex > 0)
        {
            sFilter += " and QMSdtls_Log.UserID=" + DDlUser.SelectedValue;
        }

        if (ddlManual1.SelectedIndex > 0)
        {
            sFilter += " and QMSdtls_Log.LOGManuals1='" + ddlManual1.SelectedValue + "'";
        }

        if (txtToDate.Text != "" && txtFromDate.Text != "")
        {


            DateTime sFromDate = DateTime.Parse(txtFromDate.Text, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime sToDate = DateTime.Parse(txtToDate.Text, iFormatProvider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            sToDate = sToDate.AddDays(1);
            sFilter += " and Convert(datetime,LogDate,103) between '" + sFromDate + "' AND '" + sToDate + "'";

        }

        Querry += sFilter + " order by LogFileID  DESC ";
        return Querry;

    }
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void GrdSyncQMSlog_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        ViewState["z_sortexpresion"] = e.SortExpression;
        if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
            SortGridView(sortExpression, "DESC");
        }
        else
        {
            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
            SortGridView(sortExpression, "ASC");
        }
    }


    private void SortGridView(string sortExpression, string direction)
    {
        DataSet DS = (DataSet)Session["QMSData"];
        DataView dv = new DataView(DS.Tables[0]);
        dv.Sort = sortExpression + " " + direction;

        this.GrdSyncQMSlog.DataSource = dv;
        GrdSyncQMSlog.DataBind();
    }

    public System.Web.UI.WebControls.SortDirection GridViewSortDirection
     {
        get
        {
            if (ViewState["sortDirectionTEXT"] == null)
                ViewState["sortDirectionTEXT"] = System.Web.UI.WebControls.SortDirection.Ascending;

            return (System.Web.UI.WebControls.SortDirection)ViewState["sortDirectionTEXT"];
        }
        set
        {
            ViewState["sortDirectionTEXT"] = value;
        }
    }
    protected void GrdSyncQMSlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GrdSyncQMSlog.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void ddlVesselCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        //binds the UserList DropDown here
        int SelectedVesselCode = Convert.ToInt32(ddlVesselCode.SelectedValue);
        DataSet dsUserList = objQMS.FillDDUserByVessel(SelectedVesselCode);
        DDlUser.DataSource = dsUserList.Tables[0];
        DDlUser.DataTextField = "User_name";
        DDlUser.DataValueField = "userid";
        DDlUser.DataBind();
        DDlUser.Items.Insert(0, new ListItem("Select User", "0"));
    }
}
