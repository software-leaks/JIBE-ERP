using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.JRA;
using System.Collections;

public partial class JRA_JobRiskAssesmentMatrix : System.Web.UI.Page
{
    //protected int widestData;
    //private string _seperator = "|";
    protected void Page_Load(object sender, EventArgs e)
    {

        //widestData = 0;
        if (!IsPostBack)
        {
            //BindGrid();
            //gvJRA.Attributes.Add("style", "word-break:break-all; word-wrap:break-word");
            DataSet dst = BLL_JRA_Matrix.GET_JRA_MATRIX_TABLE();
            DataTable ds = dst.Tables[0];
            if (ds != null && ds.Rows.Count > 0)
                innerData.InnerHtml = ds.Rows[0][0].ToString();

        }

    }

    //public void BindGrid()
    //{
    //    int ColumnCounts = 0;
    //    DataSet ds = BLL_JRA_Matrix.GET_JRA_MATRIX_ROW_DATA();

    //    DataTable dtSeverity = ds.Tables[0];
    //    DataTable dtConsequences = ds.Tables[1];
    //    DataTable dtProbabilty = ds.Tables[2];


    //    //Creating a new Datable 
    //    DataTable dtFinalRows = new DataTable();
    //    string[] RealColumnName = new string[30];

    //    if (dtConsequences != null && dtConsequences.Rows.Count > 0 && dtSeverity != null && dtSeverity.Rows.Count > 0 && dtProbabilty != null && dtProbabilty.Rows.Count > 0)
    //    {

    //        //Adding Datatable Columns header 
    //        dtFinalRows.Columns.Add("Col1", typeof(string));
    //        RealColumnName[ColumnCounts] = "Severity|Cat";
    //        ColumnCounts++;
    //        dtFinalRows.Columns.Add("Col2", typeof(string));
    //        RealColumnName[ColumnCounts] = "Severity|Definition";
    //        ColumnCounts++;

    //        DataView view = new DataView(dtConsequences);
    //        DataTable distinctConsequences = view.ToTable(true, "Type_Display_Text");

    //        for (int i = 0; i < distinctConsequences.Rows.Count; i++)
    //        {
    //            ColumnCounts++;
    //            RealColumnName[ColumnCounts - 1] = distinctConsequences.Rows[i]["Type_Display_Text"].ToString() + "|";
    //            dtFinalRows.Columns.Add("Col" + ColumnCounts.ToString(), typeof(string));

    //        }

    //        for (int j = 0; j < dtProbabilty.Rows.Count; j++)
    //        {

    //            ColumnCounts++;
    //            RealColumnName[ColumnCounts - 1] = dtProbabilty.Rows[j]["LikeliHoodTypeValue"].ToString() + " - " + dtProbabilty.Rows[j]["Type_Display_Text"].ToString() + "|" + dtProbabilty.Rows[j]["Type_Description"].ToString();
    //            dtFinalRows.Columns.Add("Col" + ColumnCounts.ToString(), typeof(string));

    //        }

    //        //Adding Rows to DataTable
    //        for (int k = 0; k < dtSeverity.Rows.Count; k++)
    //        {
    //            string rowsData = "";
    //            rowsData = dtSeverity.Rows[k]["CAT"].ToString() + "_" + dtSeverity.Rows[k]["DEFINITION"].ToString();

    //            DataRow[] result = dtConsequences.Select("Severity_ID = '" + dtSeverity.Rows[k]["CAT"].ToString() + "'");

    //            foreach (var item in result)
    //            {
    //                rowsData += "_" + item["SC_Description"].ToString();
    //            }

    //            for (int i = 0; i < dtProbabilty.Rows.Count; i++)
    //            {
    //                int Probability = int.Parse(dtSeverity.Rows[k]["CAT"].ToString()) * int.Parse(dtProbabilty.Rows[i]["LikeliHoodTypeValue"].ToString());
    //                rowsData += "_" + Probability.ToString();
    //            }

    //            string[] SplitString = rowsData.Split('_');
    //            DataRow row;
    //            if (SplitString != null && SplitString.Length > 0)
    //            {
    //                row = dtFinalRows.NewRow();
    //                for (int i = 0; i < SplitString.Length; i++)
    //                {
    //                    row["Col" + (i + 1).ToString()] = SplitString[i].ToString();
    //                }
    //                dtFinalRows.Rows.Add(row);
    //            }

    //        }

    //    }
    //    for (int m = 0; m < ColumnCounts; m++)
    //    {
    //        dtFinalRows.Columns[m].ColumnName = RealColumnName[m].ToString();
    //    }
    //    gvJRA.DataSource = dtFinalRows;
    //    gvJRA.DataBind();


    //}



    //protected void gvJRA_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    //If row type= header customize header cells
    //    if (e.Row.RowType == DataControlRowType.Header)
    //        CustomizeGridHeader((GridView)sender, e.Row, 2);
    //}
    //private void CustomizeGridHeader(GridView matrixSheet, GridViewRow gridRow, int headerLevels)
    //{
    //    for (int item = 1; item <= headerLevels; item++)
    //    {
    //        //creating new header row
    //        GridViewRow gridviewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
    //        IEnumerable<IGrouping<string, string>> gridHeaders = null;

    //        //reading existing header 
    //        gridHeaders = gridRow.Cells.Cast<TableCell>()
    //                    .Select(cell => GetHeaderText(cell.Text, item))
    //                    .GroupBy(headerText => headerText);

    //        foreach (var header in gridHeaders)
    //        {
    //            TableHeaderCell cell = new TableHeaderCell();


    //            if (item == 2)
    //            {
    //                cell.Text = header.Key.Substring(header.Key.LastIndexOf(_seperator) + 1);
    //            }
    //            else
    //            {
    //                cell.Text = header.Key.ToString();
    //                if (cell.Text.Contains("Severity"))
    //                {
    //                    cell.ColumnSpan = 2;
    //                }
    //            }

    //            gridviewRow.Cells.Add(cell);
    //        }
    //        // Adding new header to the grid
    //        matrixSheet.Controls[0].Controls.AddAt(gridRow.RowIndex, gridviewRow);
    //    }
    //    //hiding existing header
    //    gridRow.Visible = false;
    //}
    //private string GetHeaderText(string headerText, int headerLevel)
    //{

    //    string returnvalue = "";

    //    if (headerLevel == 2)
    //    {
    //        return headerText;
    //    }

    //    if (headerText.Contains(_seperator))
    //    {
    //        returnvalue = headerText.Substring(0, headerText.LastIndexOf(_seperator));
    //    }
    //    return returnvalue;
    //}




    //protected void gvJRA_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    // apply custom formatting to data cells
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        // set formatting for the category cell
    //        TableCell cell = e.Row.Cells[0];
    //        //cell.Width = new Unit("120px");
    //        //cell.Style["border-right"] = "2px solid #666666";
    //        //cell.BackColor = System.Drawing.Color.LightGray;

    //        // set formatting for value cells
    //        for (int i = 0; i < e.Row.Cells.Count; i++)
    //        {
    //            cell = e.Row.Cells[i];

    //            // right-align each of the column cells after the first
    //            // and set the width
    //            cell.HorizontalAlign = HorizontalAlign.Center;
    //            cell.Wrap = true;
    //            if (i >= 0)
    //            {

    //                if (i == 0)
    //                {
    //                    cell.Width = 5;
    //                    cell.Height = 20;
    //                    cell.Attributes.Add("style", "word-break:break-all;word-wrap:normal;");
    //                }

    //                else
    //                {
    //                    cell.Width = 20;
    //                    cell.Height = 20;
    //                    cell.Attributes.Add("style", "word-break:break-all;word-wrap:normal;");
    //                }
    //            }
    //            int CellColor = SetCellColor(cell);
    //            if (i != 0 && CellColor > 0)
    //            {

    //                if (CellColor >= 1 && CellColor <= 6)
    //                    cell.BackColor = System.Drawing.Color.LimeGreen;
    //                if (CellColor >= 8 && CellColor <= 12)
    //                    cell.BackColor = System.Drawing.Color.Yellow;
    //                if (CellColor > 12)
    //                {
    //                    cell.BackColor = System.Drawing.Color.Red;
    //                    cell.ForeColor = System.Drawing.Color.White;
    //                }
    //            }

    //        }
    //    }

    //    // apply custom formatting to the header cells
    //    //if (e.Row.RowType == DataControlRowType.Header)
    //    //{
    //    //    foreach (TableCell cell in e.Row.Cells)
    //    //    {
    //    //        cell.Attributes.Add("style", "white-space:pre-line;word-break:break-all;word-wrap:initial;width:25px;");
    //    //        //cell.Style["border-bottom"] = "2px solid #666666";
    //    //        //cell.BackColor = System.Drawing.Color.LightGray;
    //    //    }
    //    //}
    //}

    //private int GetCellValue(TableCell tc)
    //{
    //    int i = -1;
    //    try
    //    {
    //        i = tc.Text.Length;

    //    }
    //    catch (Exception ex)
    //    {
    //        // do nothing; 
    //    }

    //    return i;
    //}
    //private int SetCellColor(TableCell tc)
    //{
    //    int i = -1;
    //    try
    //    {
    //        i = int.Parse(tc.Text);

    //    }
    //    catch (Exception ex)
    //    {
    //        i = 0;
    //    }

    //    return i;
    //}
}