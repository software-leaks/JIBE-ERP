using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SMS.Business.Infrastructure;
using System.Data;
using SMS.Business.TMSA;
using SMS.Properties;
using AjaxControlToolkit;


public partial class TMSA_KPI_ConKPI : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        UserAccessValidation();
        CalStartDate.Format = UDFLib.GetDateFormat();       //Display the selected date from datepicker as in the same format which has been selected by the user.
        CalEndDate.Format = UDFLib.GetDateFormat();
        if (!IsPostBack)
        {
            loadTabs();
            ViewState["SelectedTab"] = 1;
            Tab1.CssClass = "Clicked";
            MainView.ActiveViewIndex = 0;
            //txtStartDate.Text = DateTime.Now.AddDays(-90).ToString("dd-MM-yyyy");
            //txtEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtStartDate.Text = DateTime.Now.AddDays(-90).ToString(UDFLib.GetDateFormat());
            txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
            BindFleetDLL();
            Load_VesselList();
            GenearteDiv(Tab1.Text);
            LoadData();
           
        }
        //
    
    }

    public void loadTabs()
    {
        DataTable dt = BLL_TMSA_PI.Get_KPI_Category();
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TabPanel tbPanel = new TabPanel();
                tbPanel.ID = "tb" + dr["Category"].ToString();
                tbPanel.HeaderText = dr["Category"].ToString();
                tbPanel.Attributes.Add("OnClick","btnEdit_Click");
                this.TabCon.Controls.Add(tbPanel);
            }
        }
    }

    public UserAccess objUA = new UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    protected void DDLFleet_SelectedIndexChanged()
    {
        Session["sFleet"] = DDLFleet.SelectedValues;
        Load_VesselList();
        Session["Vessel_Id"] = DDLVessel.SelectedValues;
        if (ViewState["SelectedTab"] != null)
        {
            if (Convert.ToInt32(ViewState["SelectedTab"]) == 2)
                OpenTab2();
            else
                OpenTab1();
        }
    }

    protected void DDLVessel_SelectedIndexChanged()
    {

        Session["Vessel_Id"] = DDLVessel.SelectedValues;
        if (ViewState["SelectedTab"] != null)
        {
            if (Convert.ToInt32(ViewState["SelectedTab"]) == 2)
                OpenTab2();
            else
                OpenTab1();
        }
    }

    private void GenearteDiv(string category)
    {
        string sContainer="chartContainer_";
        //if (ViewState["SelectedTab"] != null)
        //{
        //    int iCategory = Convert.ToInt16(ViewState["SelectedTab"]);
        //    if(iCategory > 1)
        //        sContainer = "chartContainer2_";

        //}
        PlaceHolder1.Controls.Clear();
        int rowcount = 0;
        DataTable dt = BLL_TMSA_PI.Get_KPI_List("", null, null, ref rowcount, category).Tables[0];
        var InActiveCount = dt.AsEnumerable().Where(r => r.Field<string>("KPI_STATUS") == "InActive").Count();
        if (InActiveCount > 0)
            dt.Rows.Remove(dt.AsEnumerable().Where(r => r.Field<string>("KPI_STATUS") == "InActive").FirstOrDefault());
        string[] arrKPI = new string [dt.Rows.Count];
        string[] name = new string[dt.Rows.Count];
        string[] URL = new string[dt.Rows.Count];
        for (int k = 0; k < dt.Rows.Count; k++)
        {
            arrKPI[k] = dt.Rows[k]["KPI_ID"].ToString();
            name[k] = dt.Rows[k]["name"].ToString();
            URL[k] = dt.Rows[k]["URL"].ToString();
        }
        hiddenKPIID.Value = String.Join(",", arrKPI);
        hiddenName.Value = String.Join(",", name);
        int totalKPI= dt.Rows.Count;
        int tblRows = 1;
        int tblCols = 1;//--do--
        if (totalKPI > 1)
        {
            if (totalKPI < 3)
            {
                tblRows = 1;
                tblCols = 2;

            }
            else if (totalKPI < 5)
            {
                tblRows = 2;
                tblCols = 2;
            }
            else 
            {
                tblCols = 3;
                tblRows = (int)Math.Ceiling((double)totalKPI / 3);
            }

        }
        

        Table tbl = new Table();
        tbl.Attributes.Add("align", "center");
        
        PlaceHolder1.Controls.Add(tbl);
        string sURL;
        //TableRow tr = new TableRow();
        int iCount = 0;
        for (int i = 0; i < tblRows; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < tblCols; j++)
            {

                TableCell tc = new TableCell();

                HtmlGenericControl newControl = new HtmlGenericControl("div");
                newControl.ID = sContainer + i + j;
                newControl.Attributes.Add("Style", "Height:300px;width:600px;float:left");
                if (iCount < totalKPI)
                {
                    sURL = URL[iCount].ToString();
                    if (sURL!=null && sURL !="")
                        newControl.Attributes.Add("onclick", "redirect('" + sURL + "')");
                }
                newControl.InnerHtml = "";
                tc.Controls.Add(newControl);
                tr.Cells.Add(tc);
                iCount++;

            }
            tbl.Rows.Add(tr);
        }

        hiddenCount.Value = tblRows.ToString();
        hiddenCount1.Value = tblCols.ToString();
    
    }

    private void GenearteDiv2(string category)
    {
        PlaceHolder2.Controls.Clear();
        int rowcount = 0;
        DataTable dt = BLL_TMSA_PI.Get_KPI_List("", null, null, ref rowcount, category).Tables[0];
        var InActiveCount = dt.AsEnumerable().Where(r => r.Field<string>("KPI_STATUS") == "InActive").Count();
        if (InActiveCount > 0)
         dt.Rows.Remove(dt.AsEnumerable().Where(r => r.Field<string>("KPI_STATUS") == "InActive").FirstOrDefault());
        string[] arrKPI = new string[dt.Rows.Count];
        string[] name = new string[dt.Rows.Count];
        string[] URL = new string[dt.Rows.Count];
        for (int k = 0; k < dt.Rows.Count; k++)
        {
            arrKPI[k] = dt.Rows[k]["KPI_ID"].ToString();
            name[k] = dt.Rows[k]["name"].ToString();
            URL[k] = dt.Rows[k]["URL"].ToString();
        }
        hiddenKPIID.Value = String.Join(",", arrKPI);
        hiddenName.Value = String.Join(",", name);
        int totalKPI = dt.Rows.Count;
        int tblRows = 1;
        int tblCols = 1;
        if (totalKPI > 1)
        {
            if (totalKPI < 3)
            {
                tblRows = 1;
                tblCols = 2;

            }
            else if (totalKPI < 5)
            {
                tblRows = 2;
                tblCols = 2;
            }
            else
            {
                tblCols = 3;
                tblRows = (int)Math.Ceiling((double)totalKPI / 3);
            }


        }

        Table tbl = new Table();
        tbl.Attributes.Add("align", "center");
        PlaceHolder2.Controls.Add(tbl);
        //TableRow tr = new TableRow();
        string sURL;
        int iCount = 0;
        for (int i = 0; i < tblRows; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < tblCols; j++)
            {

                TableCell tc = new TableCell();

                HtmlGenericControl newControl = new HtmlGenericControl("div");
                newControl.ID = "chartContainer2_" + i + j;
                newControl.Attributes.Add("Style", "Height:300px;width:600px;float:left");
               // newControl.Attributes.Add("onclick", "redirect()");
                if (iCount < totalKPI)
                {
                    sURL = URL[iCount].ToString();
                    if (sURL != null && sURL != "")
                        newControl.Attributes.Add("onclick", "redirect('" + sURL + "')");
                }
                newControl.InnerHtml = "";
                tc.Controls.Add(newControl);
                tr.Cells.Add(tc);

                iCount++;
            }
            tbl.Rows.Add(tr);
        }

        hiddenCount.Value = tblRows.ToString();
        hiddenCount1.Value = tblCols.ToString();

    }
    public void BindFleetDLL()
    {
        try
        {
            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLFleet.DataSource = FleetDT;
            DDLFleet.DataTextField = "Name";
            DDLFleet.DataValueField = "code";
            DDLFleet.DataBind();
            if (FleetDT.Rows.Count > 0)
            {
                if (Session["USERFLEETID"] != null && Session["USERFLEETID"].ToString() != "0")
                {
                    DDLFleet.SelectItems(new string[] { Session["USERFLEETID"].ToString() });
                }
                else
                {
                    foreach (DataRow dr in FleetDT.Rows)
                    {
                        DDLFleet.SelectItems(new string[] { dr["code"].ToString() });
                    }
                }

            }

            Session["sFleet"] = DDLFleet.SelectedValues;
        }
        catch (Exception ex)
        {

        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


    public void Load_VesselList()
    {
        try
        {
            BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
            DataTable dtVessel = objKPI.Get_Fleet_Vessel_List((DataTable)Session["sFleet"], Convert.ToInt32(Session["USERCOMPANYID"].ToString()), Convert.ToInt32(Session["USERCOMPANYID"].ToString()), GetSessionUserID());
            foreach (DataRow dr in dtVessel.Rows)
            {
                if (dr["Vessel_Id"] == "11" || dr["Vessel_Id"] == "13")
                    dtVessel.Rows.Remove(dr);
            }

            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            if (dtVessel.Rows.Count > 0)
            {
                for (int i = 0; i < dtVessel.Rows.Count; i++)
                {
                    if (dtVessel.Rows[i]["Vessel_id"] != null)
                        DDLVessel.SelectItems(new string[] { dtVessel.Rows[i]["Vessel_id"].ToString() });

                }

            }
            Session["Vessel_Id"] = DDLVessel.SelectedValues;

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }


    }
    public string[] Vessel_Ids;

    public void btnEdit_Click(object s,EventArgs e)
    {
        TabPanel tc = (TabPanel)s;
        GenearteDiv(tc.HeaderText);
        LoadData();
    }

    private void LoadData()
    {
  
        DataTable dt = (DataTable)Session["Vessel_Id"];
        hdnVessel_IDs.Value = null;
        hiddenStart.Value = UDFLib.ConvertToDefaultDt(txtStartDate.Text);   //ConvertToDefaultDt() method is used to change the user selected date format to the default format, the format in which date is saved in database.
        hiddenEnd.Value = UDFLib.ConvertToDefaultDt(txtEndDate.Text);
                
        if (dt.Rows.Count != 0)
        {
           
            Vessel_Ids = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                Vessel_Ids[i] = dr[0].ToString();
                hdnVessel_IDs.Value = hdnVessel_IDs.Value + "," + dr[0].ToString();
                i++;
            }
            hdnVessel_IDs.Value = hdnVessel_IDs.Value.Trim(',');
            if (ViewState["SelectedTab"] != null)
            {
                if (Convert.ToInt32(ViewState["SelectedTab"]) == 2)
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart(2);", true);
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart(1);", true);
            }

            else
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", "showChart(1);", true);
        }
        else
        {

            string msg = String.Format("alert('Please select vessels.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg, true);
        }
    }

    protected void Tab1_Click(object sender, EventArgs e)
    {
        ViewState["SelectedTab"] = 1;
        OpenTab1();
    }
    protected void Tab2_Click(object sender, EventArgs e)
    {
        ViewState["SelectedTab"] = 2;
        OpenTab2();
    }


    private void OpenTab1()
    {
        GenearteDiv(Tab1.Text);
        LoadData();
        Tab1.CssClass = "Clicked";
        Tab2.CssClass = "Initial";
        MainView.ActiveViewIndex = 0;
    }


    private void OpenTab2()
    {
        ViewState["SelectedTab"] = 2;
        GenearteDiv2(Tab2.Text);
        LoadData();
        Tab2.CssClass = "Clicked";
        Tab1.CssClass = "Initial";

        MainView.ActiveViewIndex = 1;
    }

    protected void Tab3_Click(object sender, EventArgs e)
    {
        ViewState["SelectedTab"] = 3;
        Tab1.CssClass = "Initial";
        Tab2.CssClass = "Initial";
       // Tab3.CssClass = "Clicked";
        MainView.ActiveViewIndex = 2;
    }

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToDateTime(txtStartDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
        {
           if( ViewState["SelectedTab"]!= null)
            {
              if( Convert.ToInt32(ViewState["SelectedTab"])==2)
                  OpenTab2();
              else
                  OpenTab1();
            }
            
        }
        else
        {
            string msg2 = String.Format("alert('Start Date should not be greater than End Date !')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msg", msg2, true);
        }
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        ClearSearch();
        LoadData();
    }

    private void ClearSearch()
    {
        txtStartDate.Text = DateTime.Now.AddDays(-90).ToString(UDFLib.GetDateFormat());
        txtEndDate.Text = DateTime.Now.ToString(UDFLib.GetDateFormat());
        BindFleetDLL();
        Load_VesselList();
        GenearteDiv(Tab1.Text);
    }

}