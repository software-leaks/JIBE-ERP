using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.Crew;
using System.Collections;
using AjaxControlToolkit4;

public partial class CrewDisbursement_DisbFeeLib : System.Web.UI.Page
{
    public string OperationMode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ViewState["Flag"] != null)
        {
            AddTextBox1(Convert.ToInt32(Session["ID"].ToString()));

        }

        if (!IsPostBack)
        {
            ViewState["Flag"] = null;
            UserAccessValidation();
            Load_AgencyFee();
            Load_ProcessingFee();

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            GridView_AgencyFee.Columns[GridView_AgencyFee.Columns.Count - 1].Visible = false;
            GridView_ProcessingFee.Columns[GridView_ProcessingFee.Columns.Count - 1].Visible = false;
        }
        if (objUA.Edit == 0)
        {
            GridView_AgencyFee.Columns[GridView_AgencyFee.Columns.Count - 1].Visible = false;
            GridView_ProcessingFee.Columns[GridView_ProcessingFee.Columns.Count - 1].Visible = false;
        }
        if (objUA.Delete == 0)
        {
        }
        if (objUA.Approve == 0)
        {
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
        {
            Response.Redirect("~/account/Login.aspx");
            return 0;
        }
    }
    protected void Load_AgencyFee()
    {
        try
        {
       
            DataSet ds = BLL_Crew_Disbursement.Get_MOAgencyFee(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr[4] = Convert.ToString(dr[4]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[4])) : "";
                dr[7] = dr[7] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[7])) : "";
                dr[10] = dr[10] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[10])) : "";
                dr[13] = dr[13] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[13])) : "";
                dr[16] = dr[16] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[16])) : "";
                dr[19] = dr[19] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[19])) : "";
                dr[22] = dr[22] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[22])) : "";
                dr[25] = dr[25] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[25])) : "";
                dr[28] = dr[28] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[28])) : "";
                dr[31] = dr[31] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[31])) : "";
                dr[34] = dr[34] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[34])) : "";
                dr[37] = dr[37] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[37])) : "";
                dr[40] = dr[40] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[40])) : "";
                dr[43] = dr[43] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[43])) : "";
                dr[46] = dr[46] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[46])) : "";
                dr[49] = dr[49] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[49])) : "";
                dr[52] = dr[52] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[52])) : "";
                dr[55] = dr[55] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[55])) : "";
                dr[58] = dr[58] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[58])) : "";

            }

            GridView_AgencyFee.DataSource = ds.Tables[0];
            GridView_AgencyFee.DataBind();

            gv_AgencyFeeHistory.DataSource = ds.Tables[1];
            gv_AgencyFeeHistory.DataBind();
            if (gv_AgencyFeeHistory.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gv_AgencyFeeHistory.UseAccessibleHeader = true;

                //This will add the <thead> and <tbody> elements
                gv_AgencyFeeHistory.HeaderRow.TableSection = TableRowSection.TableHeader;

                //This adds the <tfoot> element. 
                //Remove if you don't have a footer row
                //gvTheGrid.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }
        catch
        {
        }
    }
    protected void Load_ProcessingFee()
    {
        try
        {
            DataSet ds = BLL_Crew_Disbursement.Get_MOProcessingFee(0);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                dr[4] = Convert.ToString(dr[4]) != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[4])) : "";
                dr[7] = dr[7] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[7])) : "";
                dr[10] = dr[10] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[10])) : "";
                dr[13] = dr[13] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[13])) : "";
                dr[16] = dr[16] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[16])) : "";
                dr[19] = dr[19] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[19])) : "";
                dr[22] = dr[22] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[22])) : "";
                dr[25] = dr[25] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[25])) : "";
                dr[28] = dr[28] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[28])) : "";
                dr[31] = dr[31] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[31])) : "";
                dr[34] = dr[34] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[34])) : "";
                dr[37] = dr[37] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[37])) : "";
                dr[40] = dr[40] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[40])) : "";
                dr[43] = dr[43] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[43])) : "";
                dr[46] = dr[46] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[46])) : "";
                dr[49] = dr[49] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[49])) : "";
                dr[52] = dr[52] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[52])) : "";
                dr[55] = dr[55] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[55])) : "";
                dr[58] = dr[58] != "" ? UDFLib.ConvertUserDateFormat(Convert.ToString(dr[58])) : "";
            }

            GridView_ProcessingFee.DataSource = ds.Tables[0];
            GridView_ProcessingFee.DataBind();

            gv_ProcessingFeeHistory.DataSource = ds.Tables[1];
            gv_ProcessingFeeHistory.DataBind();
        }
        catch
        {
        }
    }

    protected void GridView_AgencyFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.SetRenderMethodDelegate(RenderHeaderApprHistory);
            e.Row.Style["background-color"] = "HeaderStyle-css";

        }
       

    }


    protected void GridView_AgencyFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ManningOfficeID = UDFLib.ConvertToInteger(GridView_AgencyFee.DataKeys[e.RowIndex].Values[0].ToString());

            decimal Amount_VMT = UDFLib.ConvertToDecimal(GridView_AgencyFee.DataKeys[e.RowIndex].Values[1].ToString());
            decimal Amount_Jnr = UDFLib.ConvertToDecimal(GridView_AgencyFee.DataKeys[e.RowIndex].Values[3].ToString());
            decimal Amount_Rat = UDFLib.ConvertToDecimal(GridView_AgencyFee.DataKeys[e.RowIndex].Values[5].ToString());

            DateTime? VMT_EffectiveDate = UDFLib.ConvertDateToNull(GridView_AgencyFee.DataKeys[e.RowIndex].Values[2].ToString());
            DateTime? Jnr_EffectiveDate = UDFLib.ConvertDateToNull(GridView_AgencyFee.DataKeys[e.RowIndex].Values[4].ToString());
            DateTime? Rat_EffectiveDate = UDFLib.ConvertDateToNull(GridView_AgencyFee.DataKeys[e.RowIndex].Values[6].ToString());

            TextBox txtAmount_VMT = (TextBox)GridView_AgencyFee.Rows[e.RowIndex].FindControl("txtAF_Officers");
            TextBox txtDate_VMT = (TextBox)GridView_AgencyFee.Rows[e.RowIndex].FindControl("txtEffectiveDate");

            TextBox txtAmount_Jnr = (TextBox)GridView_AgencyFee.Rows[e.RowIndex].FindControl("txtAF_Officers_Jnr");
            TextBox txtDate_Jnr = (TextBox)GridView_AgencyFee.Rows[e.RowIndex].FindControl("txtAF_Officers_Jnr_Effdt");

            TextBox txtAmount_Rat = (TextBox)GridView_AgencyFee.Rows[e.RowIndex].FindControl("txtAF_Ratings");
            TextBox txtDate_Rat = (TextBox)GridView_AgencyFee.Rows[e.RowIndex].FindControl("txtAF_Ratings_Effdt");
            if (Amount_VMT != UDFLib.ConvertToDecimal(txtAmount_VMT.Text) || VMT_EffectiveDate != UDFLib.ConvertDateToNull(txtDate_VMT.Text))
                BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, 1, UDFLib.ConvertToDecimal(txtAmount_VMT.Text), UDFLib.ConvertToInteger(txtAmount_VMT.ToolTip), txtDate_VMT.Text, GetSessionUserID(), 1);
            if (Amount_Jnr != UDFLib.ConvertToDecimal(txtAmount_Jnr.Text) || Jnr_EffectiveDate != UDFLib.ConvertDateToNull(txtDate_Jnr.Text))
                BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, 1, UDFLib.ConvertToDecimal(txtAmount_Jnr.Text), UDFLib.ConvertToInteger(txtAmount_Jnr.ToolTip), txtDate_Jnr.Text, GetSessionUserID(), 2);
            if (Amount_Rat != UDFLib.ConvertToDecimal(txtAmount_Rat.Text) || Rat_EffectiveDate != UDFLib.ConvertDateToNull(txtDate_Rat.Text))
                BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, 1, UDFLib.ConvertToDecimal(txtAmount_Rat.Text), UDFLib.ConvertToInteger(txtAmount_Rat.ToolTip), txtDate_Rat.Text, GetSessionUserID(), 3);

            GridView_AgencyFee.EditIndex = -1;
            Load_AgencyFee();
        }
        catch
        {
        }
    }
    protected void GridView_AgencyFee_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_AgencyFee.EditIndex = e.NewEditIndex;
            Load_AgencyFee();
            string HeaderText = GridView_AgencyFee.Columns[0].HeaderText;        

        }
        catch
        {
        }
    }
    protected void GridView_AgencyFee_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_AgencyFee.EditIndex = -1;
            Load_AgencyFee();
        }
        catch
        {
        }
    }


    protected void GridView_ProcessingFee_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int ManningOfficeID = UDFLib.ConvertToInteger(GridView_ProcessingFee.DataKeys[e.RowIndex].Values["ID"].ToString());

            decimal Amount_VMT = UDFLib.ConvertToDecimal(GridView_ProcessingFee.DataKeys[e.RowIndex].Values[1].ToString());
            decimal Amount_Jnr = UDFLib.ConvertToDecimal(GridView_ProcessingFee.DataKeys[e.RowIndex].Values[3].ToString());
            decimal Amount_Rat = UDFLib.ConvertToDecimal(GridView_ProcessingFee.DataKeys[e.RowIndex].Values[5].ToString());

            DateTime? VMT_EffectiveDate = UDFLib.ConvertDateToNull(GridView_ProcessingFee.DataKeys[e.RowIndex].Values[2].ToString());
            DateTime? Jnr_EffectiveDate = UDFLib.ConvertDateToNull(GridView_ProcessingFee.DataKeys[e.RowIndex].Values[4].ToString());
            DateTime? Rat_EffectiveDate = UDFLib.ConvertDateToNull(GridView_ProcessingFee.DataKeys[e.RowIndex].Values[6].ToString());

            TextBox txtAmount_VMT = (TextBox)GridView_ProcessingFee.Rows[e.RowIndex].FindControl("txtAF_Officers");
            TextBox txtDate_VMT = (TextBox)GridView_ProcessingFee.Rows[e.RowIndex].FindControl("txtEffectiveDate");

            TextBox txtAmount_Jnr = (TextBox)GridView_ProcessingFee.Rows[e.RowIndex].FindControl("txtAF_Officers_Jnr");
            TextBox txtDate_Jnr = (TextBox)GridView_ProcessingFee.Rows[e.RowIndex].FindControl("txtAF_Officers_Jnr_Effdt");

            TextBox txtAmount_Rat = (TextBox)GridView_ProcessingFee.Rows[e.RowIndex].FindControl("txtAF_Ratings");
            TextBox txtDate_Rat = (TextBox)GridView_ProcessingFee.Rows[e.RowIndex].FindControl("txtAF_Ratings_Effdt");

            if (Amount_VMT != UDFLib.ConvertToDecimal(txtAmount_VMT.Text) || VMT_EffectiveDate != UDFLib.ConvertDateToNull(txtDate_VMT.Text))
                BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, 2, UDFLib.ConvertToDecimal(txtAmount_VMT.Text), UDFLib.ConvertToInteger(txtAmount_VMT.ToolTip), txtDate_VMT.Text, GetSessionUserID(), 1);
            if (Amount_Jnr != UDFLib.ConvertToDecimal(txtAmount_Jnr.Text) || Jnr_EffectiveDate != UDFLib.ConvertDateToNull(txtDate_Jnr.Text))
                BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, 2, UDFLib.ConvertToDecimal(txtAmount_Jnr.Text), UDFLib.ConvertToInteger(txtAmount_Jnr.ToolTip), txtDate_Jnr.Text, GetSessionUserID(), 2);
            if (Amount_Rat != UDFLib.ConvertToDecimal(txtAmount_Rat.Text) || Rat_EffectiveDate != UDFLib.ConvertDateToNull(txtDate_Rat.Text))
                BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(ManningOfficeID, 2, UDFLib.ConvertToDecimal(txtAmount_Rat.Text), UDFLib.ConvertToInteger(txtAmount_Rat.ToolTip), txtDate_Rat.Text, GetSessionUserID(), 3);


            GridView_ProcessingFee.EditIndex = -1;
            Load_ProcessingFee();
        }
        catch
        {
        }
    }
    protected void GridView_ProcessingFee_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView_ProcessingFee.EditIndex = e.NewEditIndex;
            Load_ProcessingFee();
        }
        catch
        {
        }
    }
    protected void GridView_ProcessingFee_RowCancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView_ProcessingFee.EditIndex = -1;
            Load_ProcessingFee();
        }
        catch
        {
        }
    }

    #region "Function - Agency Merge Grid Header"

    [Serializable]
    private class MergedColumnsInfoAppr
    {
        // indexes of merged columns
        public List<int> MergedColumnsAppr = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumnsAppr = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable TitlesAppr = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumnsAppr(int[] columnsIndexes, string title)
        {
            MergedColumnsAppr.AddRange(columnsIndexes);
            StartColumnsAppr.Add(columnsIndexes[0], columnsIndexes.Length);
            TitlesAppr.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfoAppr infoAppr
    {
        get
        {
            if (ViewState["infoAppr"] == null)
                ViewState["infoAppr"] = new MergedColumnsInfoAppr();
            return (MergedColumnsInfoAppr)ViewState["infoAppr"];
        }
    }

    private void RenderHeaderApprHistory(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!infoAppr.MergedColumnsAppr.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (infoAppr.StartColumnsAppr.Contains(i))
                {


                    output.Write(string.Format("<th align='center'  colspan='{0}'>{1}</th>",
                             infoAppr.StartColumnsAppr[i], infoAppr.TitlesAppr[i]));


                }
        }

        //close the first row 
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < infoAppr.MergedColumnsAppr.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[infoAppr.MergedColumnsAppr[i]];

            cell.CssClass = "HeaderStyle-css";
            cell.RenderControl(output);

        }

        infoAppr.MergedColumnsAppr.Clear();
        infoAppr.StartColumnsAppr.Clear();
        infoAppr.TitlesAppr.Clear();
    }

    #endregion
    #region "Function - Processing Merge Grid Header"

    [Serializable]
    private class MergedColumnsInfoAppr1
    {
        // indexes of merged columns
        public List<int> MergedColumnsAppr1 = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumnsAppr1 = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable TitlesAppr1 = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumnsAppr1(int[] columnsIndexes, string title)
        {
            MergedColumnsAppr1.AddRange(columnsIndexes);
            StartColumnsAppr1.Add(columnsIndexes[0], columnsIndexes.Length);
            TitlesAppr1.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfoAppr1 infoAppr1
    {
        get
        {
            if (ViewState["infoAppr1"] == null)
                ViewState["infoAppr1"] = new MergedColumnsInfoAppr1();
            return (MergedColumnsInfoAppr1)ViewState["infoAppr1"];
        }
    }

    private void RenderHeaderApprHistory1(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];            
            if (!infoAppr1.MergedColumnsAppr1.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else 
                if (infoAppr1.StartColumnsAppr1.Contains(i))
                {

                    output.Write(string.Format("<th align='center'  colspan='{0}'>{1}</th>",
                             infoAppr1.StartColumnsAppr1[i], infoAppr1.TitlesAppr1[i]));

                }
        }
        output.RenderEndTag();
        
        output.RenderBeginTag("tr");


        for (int i = 0; i < infoAppr1.MergedColumnsAppr1.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[infoAppr1.MergedColumnsAppr1[i]];

            cell.CssClass = "HeaderStyle-css";
            cell.RenderControl(output);

        }

        infoAppr1.MergedColumnsAppr1.Clear();
        infoAppr1.StartColumnsAppr1.Clear();
        infoAppr1.TitlesAppr1.Clear();
    }

    #endregion
    protected void GridView_ProcessingFee_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.SetRenderMethodDelegate(RenderHeaderApprHistory1);
            e.Row.Style["background-color"] = "HeaderStyle-css";

        }
    }
    protected void GridView_AgencyFee_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
        //e.Row.Cells[3].Enabled = false;
        foreach (GridViewRow r in GridView_AgencyFee.Rows)
        {
            string s = r.Cells[0].Text;
            string y = r.Cells[1].Text;
        }

        GridViewRow row = e.Row;
        // Intitialize TableCell list
        List<TableCell> columns = new List<TableCell>();
        foreach (DataControlField column in GridView_AgencyFee.Columns)
        {
            //Get the first Cell /Column
            TableCell cell = row.Cells[0];
            // Then Remove it after
            row.Cells.Remove(cell);
            //And Add it to the List Collections
            columns.Add(cell);
        }

        // Add cells
        row.Cells.AddRange(columns.ToArray());
    }
    protected void GridView_AgencyFee_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void onUpdate_Proc(object source, CommandEventArgs e)
    {
        lblError.Text = "";
        string[] arg = new string[2];
        arg = e.CommandArgument.ToString().Split(';');
        Session["ID"] = arg[0];
        Session["Name"] = arg[1];
        pnlTextBoxes.Controls.Clear();
        OperationMode = Session["Name"].ToString() + " - Edit Processing Fee Details";
        Session["FeeType"] = 2;
        AddTextBox(Convert.ToInt32(Session["ID"].ToString()));


        string AddAssignermodal = String.Format("showModal('divadd',false);aftercall();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);

    }
    protected void onUpdate(object source, CommandEventArgs e)
    {
        Session["FeeType"] = 1;
        lblError.Text = "";
        string[] arg = new string[2];
        arg = e.CommandArgument.ToString().Split(';');
        Session["ID"] = arg[0];
        Session["Name"] = arg[1];
        pnlTextBoxes.Controls.Clear();
        OperationMode = Session["Name"].ToString() + " - Edit Agency Fee Details";
        AddTextBox(Convert.ToInt32(Session["ID"].ToString()));

        string AddAssignermodal = String.Format("showModal('divadd',false);aftercall();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);

    }

    protected void AddTextBox(int id)
    {
        DataSet ds;
        if (Session["FeeType"].ToString()=="2")
        {
            ds = BLL_Crew_Disbursement.Get_MOProcessingFee(id); 
        }
        else
         ds = BLL_Crew_Disbursement.Get_MOAgencyFee(id);
        DataTable dt = ds.Tables[0];
        foreach (DataColumn column in dt.Columns)
        {
            string sColumName = "";
            if (column.ColumnName.Contains("Amt"))
            {
                sColumName = column.ColumnName.Replace("Amt", "").TrimEnd().Replace(" ", "_") + "_ID";
            }
            if (column.ColumnName.Contains("Amt") || column.ColumnName.Contains("Date") || column.ColumnName.Contains("DATE"))
            {

                Literal lts = new Literal();
                lts.Text = "<table>";
                pnlTextBoxes.Controls.Add(lts);
                this.CreateTextBox("txt" + column.ColumnName, column.ColumnName, Convert.ToString(dt.Rows[0][column.ColumnName]), column.ColumnName.Contains("Amt") ? Convert.ToString(dt.Rows[0][sColumName]) : ""); //Convert.ToString(dt.Rows[0].Field<string>(0))
                Literal lts1 = new Literal();
                lts1.Text = "</table>";
                pnlTextBoxes.Controls.Add(lts1);
            }
        }
        ViewState["Flag"] = pnlTextBoxes.Controls.Count;

    }
    protected void AddTextBox1(int id)
    {
        DataSet ds;
        if (Session["FeeType"].ToString() == "2")
        {
            ds = BLL_Crew_Disbursement.Get_MOProcessingFee(id);
        }
        else
            ds = BLL_Crew_Disbursement.Get_MOAgencyFee(id);
        DataTable dt = ds.Tables[0];
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataColumn column in dt.Columns)
            {
                string sColumName = "";
                if (column.ColumnName.Contains("Amt"))
                {
                    sColumName = column.ColumnName.Replace("Amt", "").TrimEnd().Replace(" ", "_") + "_ID";
                }
                if (column.ColumnName.Contains("Amt") || column.ColumnName.Contains("Date") || column.ColumnName.Contains("DATE"))
                {
                    Literal lt = new Literal();
                    lt.Text = "<table>";
                    pnlTextBoxes.Controls.Add(lt);
                    this.CreateTextBox("txt" + column.ColumnName, column.ColumnName, Convert.ToString(dt.Rows[0][column.ColumnName]), column.ColumnName.Contains("Amt") ? Convert.ToString(dt.Rows[0][sColumName]) : ""); //Convert.ToString(dt.Rows[0].Field<string>(0))
                    Literal lt1 = new Literal();
                    lt1.Text = "</table>";
                    pnlTextBoxes.Controls.Add(lt1);
                }
            }
        }

    }

  /// <summary>
  /// Creating controls dynamically !
  /// </summary>
  
    private void CreateTextBox(string id, string lablText, string textValue, string feeID)
    {
        Literal lt2 = new Literal();
        lt2.Text = "<tr>";
        pnlTextBoxes.Controls.Add(lt2);
        Label lbl = new Label();
        lbl.ID = id + 1;
        
        TextBox txt = new TextBox();
        Label lblStar = new Label();
        lblStar.ID = lablText + 1;
        lblStar.ForeColor = System.Drawing.Color.Red;
        lblStar.Text = "*";
        txt.ID = id.Replace(" ","_");
        lbl.Text = "  " + lablText + " ";
        txt.Text = textValue;
        txt.ToolTip = feeID;

        Literal lttd = new Literal();
        lttd.Text = "<td>";
        pnlTextBoxes.Controls.Add(lttd);
        lbl.Width = 155;
        lblStar.Width = 5;
        pnlTextBoxes.Controls.Add(lbl);        
        Literal lttd1 = new Literal();
        lttd1.Text = "</td>";
        pnlTextBoxes.Controls.Add(lttd1);

        

        //if (txt.ID.Contains("Date") || txt.ID.Contains("DATE"))
        //{
        //    txt.Attributes.Add("type", "date");            
        //}

        Literal lttstar = new Literal();
        lttstar.Text = "<td>";
        pnlTextBoxes.Controls.Add(lttstar);

        pnlTextBoxes.Controls.Add(lblStar);

        Literal lttdstar3 = new Literal();
        lttdstar3.Text = "</td>";
        pnlTextBoxes.Controls.Add(lttdstar3);

        Literal lttd2 = new Literal();
        lttd2.Text = "<td>";
        pnlTextBoxes.Controls.Add(lttd2);

        if (txt.ID.Contains("Date") || txt.ID.Contains("DATE"))
        {
            AjaxControlToolkit.CalendarExtender calenderDate = new AjaxControlToolkit.CalendarExtender();
            calenderDate.TargetControlID = txt.ID;
            //txt.Attributes.Add("type", "date"); 
            calenderDate.Format = UDFLib.GetDateFormat();
            pnlTextBoxes.Controls.Add(calenderDate);
            pnlTextBoxes.Controls.Add(txt);
            if(!string.IsNullOrEmpty(txt.Text))
            txt.Text = UDFLib.ConvertUserDateFormat(txt.Text, UDFLib.GetDateFormat());
        }
        else
        {
            pnlTextBoxes.Controls.Add(txt);
        }
      

        Literal lttd3 = new Literal();
        lttd3.Text = "</td>";
        pnlTextBoxes.Controls.Add(lttd3);


        Literal lt3 = new Literal();
        lt3.Text = "</tr>";
        pnlTextBoxes.Controls.Add(lt3);
        string AddAssignermodal = String.Format("selectdate()");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "selectdate", AddAssignermodal, true);

    }

    protected void GetTextBoxValues(object sender, EventArgs e)
    {
        try
        {
        int FeeType = 1;
        if (Session["FeeType"].ToString()=="2")
        {
            OperationMode = Session["Name"].ToString() + " - Edit Processing Fee Details";
            FeeType = 2;
        }
        else      
        OperationMode = Session["Name"].ToString() + " - Edit Agency Fee Details";

        lblError.Text = "";   
        DataTable table = new DataTable();
        table.Columns.Add("MoID", typeof(int));
        table.Columns.Add("Amount", typeof(string));
        table.Columns.Add("Date", typeof(string));
        table.Columns.Add("feeID", typeof(string));
        table.Columns.Add("Rank_Category", typeof(string));
       

        var list = pnlTextBoxes.Controls.OfType<TextBox>().ToList<TextBox>();
        int b = list.Count<TextBox>();
        int breack = 0;
        for (int i = 0; i < b; i = i + 2)
        {
            TextBox textBox1 = list[i];
            TextBox textBox2 = list[i + 1];
            string text = textBox1.ID;
            string Rank_Category = textBox1.ID.Replace("txt", "");
            string txtdate = textBox2.ID.Replace("txt", "");
            try
            {
                if (validation(textBox1.Text, Convert.ToDateTime(textBox2.Text).ToString("dd/MM/yyyy"), Rank_Category, txtdate) == 1)
                {
                    DateTime date = Convert.ToDateTime(textBox2.Text);
                    string sDate = date.ToString("MM/dd/yyyy");
                    table.Rows.Add(Session["ID"].ToString(), textBox1.Text, sDate, textBox1.ToolTip, Rank_Category.Replace(" Amt ", ""));
                }
                else
                {
                    string AddAssignermodal1 = String.Format("showModal('divadd',false);aftercall();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal1, true);
                    breack = 1;
                    break;
                }
            }
            catch
            {
                string AddAssignermodal1 = String.Format("showModal('divadd',false);aftercall();");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal1", AddAssignermodal1, true);
                breack = 1;
                break;
            }
        }
        if (breack == 1)
        {
            string AddAssignermodal1 = String.Format("showq'divadd',false);aftercall();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal1, true);
        }
        else
        {
          int result=  BLL_Crew_Disbursement.UPDATE_MODisbursementFee_DL(table, FeeType, GetSessionUserID());
            if (Session["FeeType"].ToString() == "2")
            {
                Load_ProcessingFee();
                if (  result>=1)
                {
                    string AddAssignermodal = String.Format("hideModal('divadd',false);alert('Processing Fee Updated successfully...!');aftercall();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);   
                }
                else
                {
                    string AddAssignermodal = String.Format("showModal('divadd',false);alert('Processing fee is not updated successfully...!');aftercall();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);   
                }
                
            }
            else
            {
                Load_AgencyFee();
                if (result >= 1)
                {
                    string AddAssignermodal = String.Format("hideModal('divadd',false);alert('Agency Fee Updated successfully...!');aftercall();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);
                }
                else
                {
                    string AddAssignermodal = String.Format("showModal('divadd',false);alert('Agency fee is not updated successfully...!');aftercall();");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);  
                }           
                
            }
           
        }


        }
        catch (Exception)
        {
     
        }

    }


    protected int validation(string Amt, string date, string txtname, string txtdate)
    {
        try
        {
        string messege = "";
        DateTime dDate;
        bool isDate = true;
        int Year=0;
        try
        {
            Year = Convert.ToInt32(date.Trim().Substring(6, 2));
        }
        catch (Exception)
        {
            isDate = false;
        }          

         if (DateTime.TryParse(date, out dDate))
        {
            try
            {
                DateTime Date = Convert.ToDateTime(dDate);
               // string sDate = Date.ToString("MM/dd/yyyy");
              if (Date.Year<1900)
              { isDate = false; }  
              else if (Year < 19)
                { isDate = false; }
                else
                isDate = true;
            }
            catch (Exception)
            {isDate = false;}
        }
        else
        {isDate = false;}
        Decimal n;
        bool isNumeric = Decimal.TryParse(Amt, out n);

        if (Amt == "" && date == "")
        {
            messege = "";
            if (txtname=="")
            {
                messege = "Please enter value at " + txtname+".";  
            }
            else
            messege = "Please enter value at " + txtdate+".";

            string AddAssignermodal = String.Format("showModal('divadd',false);alert('" + messege + "');aftercall();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);
            return 0;
        }
        else if (isDate == false)
        {
            //lblError.Text = "";
            //lblError.Text = "Please enter valid date at -" + txtdate + " in (dd/MM/YYYY) format";

            string AddAssignermodal = String.Format("showModal('divadd',false);alert('Please enter valid date at " + txtdate + " in (dd/MM/YYYY) format.');aftercall();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);
            return 0;
        }
        else if (isNumeric == false)
        {
             messege = "";
             messege = "Please enter valid amount at " + txtname + "."; ;

             string AddAssignermodal = String.Format("showModal('divadd',false);alert('" + messege + "');aftercall();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);
            return 0;
        }
     
        else
            return 1;


        }
        catch (Exception ex)
        {
            string AddAssignermodal = String.Format("showModal('divadd',false);alert('" + ex + "');aftercall();");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AddAssignermodal", AddAssignermodal, true);
            return 0; 
        }
    }


    protected void GridView_ProcessingFee_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;     
        //e.Row.Cells[3].Enabled = false;

        foreach (GridViewRow r in GridView_ProcessingFee.Rows)
        {
            string s = r.Cells[0].Text;
            string y = r.Cells[1].Text;
        }

        GridViewRow row = e.Row;
        // Intitialize TableCell list
        List<TableCell> columns = new List<TableCell>();
        foreach (DataControlField column in GridView_AgencyFee.Columns)
        {
            //Get the first Cell /Column
            TableCell cell = row.Cells[0];
            // Then Remove it after
            row.Cells.Remove(cell);
            //And Add it to the List Collections
            columns.Add(cell);
        }

        // Add cells
        row.Cells.AddRange(columns.ToArray());

    }
 
}