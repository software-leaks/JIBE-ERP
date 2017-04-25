using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.VET;
using System.Data;
using SMS.Business.Infrastructure;
using SMS.Properties;
using System.Web.UI.HtmlControls;

public partial class Technical_Vetting_Vetting_VesselVettingSetting : System.Web.UI.Page
{
    public int Result = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (GetSessionUserID() == 0)
        {
            divLoggout.Visible = true;
            MainContent.Visible = false;
        }
        else
        {
            MainContent.Visible = true;
            divLoggout.Visible = false;

            UserAccessValidation();
            if (MainDiv.Visible)
            {
                if (!IsPostBack)
                {
                    BindGrid();
                    ViewState["SORTDIRECTION"] = 0;
                    ViewState["SORTBYCOLOUMN"] = null;


                }
            }
        }
    }
    
    /// <summary>
    /// Bind Vetting Setting Grid
    /// </summary>
    public void BindGrid()
    {
        
        try
        {
            BLL_VET_VettingLib objBLvtst = new BLL_VET_VettingLib();
          
            string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
            int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
            DataSet ds = new DataSet();

            ds = objBLvtst.VET_Get_VettingSetting(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, sortbycoloumn, sortdirection
                 , ref Result);

            ViewState["Vessel"] = ds.Tables[0];
            ViewState["dtAllVetType"] = ds.Tables[1];
            ViewState["dtAppVetType"] = ds.Tables[2];

            gvVslVtngStng.DataSource = ds.Tables[0];
            gvVslVtngStng.DataBind();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ImgExpExcel.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                ImgExpExcel.Enabled = false;
                btnSave.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }
    protected void gvVslVtngStng_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
                Label lblVesID = (Label)e.Row.FindControl("lblVesselId");
                DataTable dtAllVetType = (DataTable)ViewState["dtAllVetType"];
                DataTable dtAppVetType = (DataTable)ViewState["dtAppVetType"];
                string filterpenexpression = filterpenexpression = " Vessel_ID=" + UDFLib.ConvertToInteger(lblVesID.Text);
                DataRow[] drAppVetType = dtAppVetType.Select(filterpenexpression);
                CheckBoxList chkVTType = (CheckBoxList)e.Row.FindControl("chkVTType");
                chkVTType.DataSource = dtAllVetType;
                chkVTType.DataTextField = "Vetting_Type_Name";
                chkVTType.DataValueField = "Vetting_Type_ID";
                chkVTType.DataBind();

                foreach (DataRow row in drAppVetType)
                {
                    for (int i = 0; i < chkVTType.Items.Count; i++)
                    {
                        if (chkVTType.Items[i].Value == row[0].ToString())
                        {
                            chkVTType.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_VettingLib objBLvtst = new BLL_VET_VettingLib();
            DataTable dtSave = new DataTable();
            dtSave.Columns.Add("VID");
            dtSave.Columns.Add("VTID");
            dtSave.Columns.Add("STATUS");
            foreach (GridViewRow rows in gvVslVtngStng.Rows)
            {
                DataRow dr = dtSave.NewRow();
                int vslId = UDFLib.ConvertToInteger(((Label)rows.FindControl("lblVesselId")).Text);
                dr["VID"] = vslId;
                CheckBoxList chkList = (CheckBoxList)rows.FindControl("chkVTType");
                for (int i = 0; i < chkList.Items.Count; i++)
                {
                    dr = dtSave.NewRow();
                    dr["VID"] = vslId;
                    dr["VTID"] = UDFLib.ConvertToInteger(chkList.Items[i].Value);
                    
                    if (chkList.Items[i].Selected)
                        dr["STATUS"] = 1;
                    else
                        dr["STATUS"] = 0;

                    dtSave.Rows.Add(dr);
                }
            }

            objBLvtst.VET_INS_VslStng(dtSave, Convert.ToInt32(Session["USERID"]));
            string js1 = "alert('Settings saved successfully.');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert1", js1, true);

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
            string js = "alert('" + UDFLib.GetException("SystemError/ GeneralMessage") + "');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }


    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    /// <summary>
    /// Clear All controls
    /// </summary>
    private void ClearControl()
    {
        ViewState["SORTBYCOLOUMN"] = null;
        ViewState["SORTDIRECTION"] = null;      
        txtfilter.Text = string.Empty;

    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControl();
            BindGrid();
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

   
    protected void ImgExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            BLL_VET_VettingLib objBLvtst = new BLL_VET_VettingLib();
       
                string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
                int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
                DataSet ds = new DataSet();

                DataTable dtVessel = objBLvtst.VET_Get_VettingSetting(txtfilter.Text.Trim() != "" ? txtfilter.Text.Trim() : null, sortbycoloumn, sortdirection, ref Result).Tables[0];

                DataTable dtAlltypes = (DataTable)ViewState["dtAllVetType"]; //Getting vetting types from viewstate 
                DataTable dtExport = new DataTable();                       //This DataTable is finally exporting  
                dtExport.Columns.Add("Vesel Id");
                dtExport.Columns.Add("Vessel Name");
                for (int i = 0; i < dtAlltypes.Rows.Count; i++)
                {
                    dtExport.Columns.Add(dtAlltypes.Rows[i][1].ToString());  //this will add all types as header in our export datatable
                }
                for (int i = 0; i < dtVessel.Rows.Count; i++)   // This will add all vessels,id and vessel name in export datatable
                {
                    DataRow dr = dtExport.NewRow();
                    dr[0] = dtVessel.Rows[i][0];
                    dr[1] = dtVessel.Rows[i][1];
                    dtExport.Rows.Add(dr);
                }
                for (int i = 0; i < dtExport.Rows.Count; i++)
                {
                    for (int k = 2; k < dtExport.Columns.Count; k++)
                    {
                        int vessel_id = UDFLib.ConvertToInteger(dtExport.Rows[i][0]);
                        DataTable dtCheckType = new DataTable();
                        dtCheckType = objBLvtst.GetSettingTypeByVessel(null, vessel_id); //This will get all types and active status of particular vessel
                        if (dtCheckType != null && dtCheckType.Rows.Count > 0)
                        {
                            for (int j = 0; j < dtCheckType.Rows.Count; j++)
                            {
                               
                                    if (dtExport.Columns[j + 2].ToString() == dtCheckType.Rows[j][1].ToString() && dtCheckType.Rows[j][3].ToString() == "1") //dtExport.Columns[j + 2] will give type and comparing with checktype's type and active status
                                    {
                                        dtExport.Rows[i][j + 2] = "Yes";
                                    }
                                    else
                                    {
                                        dtExport.Rows[i][j + 2] = "No";
                                    }
                                
                            }
                        }
                    }
                }
                dtExport.Columns.Remove("Vesel Id"); // we dont want this column anymore .
                string[] columnNames;               //to use export functionalites we need to supply coulmn name array
                columnNames = dtExport.Columns.Cast<DataColumn>()
                           .Select(x => x.ColumnName)
                           .ToArray();
                GridViewExportUtil.ShowExcel(dtExport, columnNames, columnNames, "VettingSetting" + "_" + DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), "Vetting Setting", "");
            
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }


    protected void gvVslVtngStng_Sorting(object sender, GridViewSortEventArgs se)
    {
        try
        {
            ViewState["SORTBYCOLOUMN"] = se.SortExpression;
            if (ViewState["SORTDIRECTION"] != null && ViewState["SORTDIRECTION"].ToString() == "0")
                ViewState["SORTDIRECTION"] = 1;
            else
                ViewState["SORTDIRECTION"] = 0;
            BindGrid();
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }


    /// <summary>
    /// Check for access rights
    /// </summary>
    protected void UserAccessValidation()
    {
        try
        {
            int CurrentUserID = GetSessionUserID();
            string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

            BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
            UserAccess objUA = new UserAccess();
            objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            if (objUA.View == 0)
            {
                MainDiv.Visible = false;
                AccessMsgDiv.Visible = true;
            }
            else
            {
                MainDiv.Visible = true;
                AccessMsgDiv.Visible = false;
            }

            if (objUA.Add == 0)
            {
                btnSave.Visible = false;
            }

        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }

    /// <summary>
    /// Get UserId
    /// </summary>
    /// <returns></returns>
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }


}
