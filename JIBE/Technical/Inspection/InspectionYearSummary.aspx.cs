using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using EO.Pdf;
using System.IO;
using System.Diagnostics;
using SMS.Business.Inspection;
public partial class Technical_Reports_InspectionYearSummary : System.Web.UI.Page
{
    string EndYear = string.Empty;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    [Serializable]
    private class MergedColumnsInfo
    {
        // indexes of merged columns
        public List<int> MergedColumns = new List<int>();
        // key-value pairs: key = the first column index, value = number of the merged columns
        public Hashtable StartColumns = new Hashtable();
        // key-value pairs: key = the first column index, value = common title of the merged columns 
        public Hashtable Titles = new Hashtable();

        //parameters: the merged columns indexes, common title of the merged columns 
        public void AddMergedColumns(int[] columnsIndexes, string title)
        {
            MergedColumns.AddRange(columnsIndexes);
            StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
            Titles.Add(columnsIndexes[0], title);
        }
    }

    private MergedColumnsInfo info
    {
        get  
        {
            if (ViewState["info"] == null)
                ViewState["info"] = new MergedColumnsInfo();
            return (MergedColumnsInfo)ViewState["info"];
        }
    }

   // BLL_Tec_Worklist objWl = new BLL_Tec_Worklist();
    BLL_Tec_Inspection objInsp = new BLL_Tec_Inspection();
    int newtab = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GridViewHelper helper = new GridViewHelper(this.grdSummary);
            helper.RegisterGroup("SUPTYPE", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);

            EndYear = DateTime.Now.Year.ToString();
            ViewState["EndYear"] = EndYear;
            BindInspectionSummary(EndYear);
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            imgLogo.ImageUrl = baseUrl + "Images/company_logo.jpg";
            if (grdSummary.Rows.Count > 0)
            {
                tblStat.Visible = true;


            }
            else
            {
                tblStat.Visible = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "<script> hidedivision(); </script>", false);
            }
            ViewState["newtabnumber"] = newtab;

        }
        else
        {
           

        }
      

    }


    public void BindInspectionSummary(string Year)
    {
        try
        {



            ds = objInsp.GetYearlyInspectionSummary(Year);

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    while (true)
                    {
                        int colcount = ds.Tables[0].Columns.Count;
                        bool found = false;
                        for (int i = 0; i <= colcount - 1; i++)
                        {

                            if (ds.Tables[0].Columns[i].ColumnName.Contains(Convert.ToString(Convert.ToInt32(Year) - 4)))
                            {
                                found = true;
                                ds.Tables[0].Columns.RemoveAt(i);
                                ds.Tables[1].Columns.RemoveAt(i);
                                //colcount -= 1;

                                break;
                            }



                        }

                        if (found == false)
                        {
                            break;
                        }
                    }
                    grdSummary.DataSource = ds.Tables[0];
                    grdSummary.DataBind();

                    GetStatistics();







                }
                else
                {

                }
            }
            else
            {
                grdSummary.DataSource = null;
                grdSummary.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void GetStatistics()
    {

        lblAvgDaysOnBoard.Text = ds.Tables[2].Rows[0][1].ToString();
        lblAvgDaysOnBoardVal.Text =  Math.Round(Convert.ToDouble(ds.Tables[2].Rows[0][2].ToString()),0)+"%";

        lblAvgDaysOnShore.Text = ds.Tables[2].Rows[1][1].ToString();
        lblAvgDaysOnShoreVal.Text =  Math.Round(Convert.ToDouble(ds.Tables[2].Rows[1][2].ToString()),0) + "%";
      
        lblOnBoardDA.Text = ds.Tables[2].Rows[2][1].ToString();
        lblOnBoardDAVal.Text = "$" + ds.Tables[2].Rows[2][2].ToString();

        lblAshoreDA.Text = ds.Tables[2].Rows[3][1].ToString();
        lblAshoreDAVal.Text = "$" + ds.Tables[2].Rows[3][2].ToString();


        lblAvgDAVal.Text = Convert.ToString( ((Convert.ToDouble(ds.Tables[2].Rows[0][2].ToString()) * Convert.ToInt32(ds.Tables[2].Rows[2][2].ToString())) + (Convert.ToDouble(ds.Tables[2].Rows[1][2].ToString()) * Convert.ToInt32(ds.Tables[2].Rows[3][2].ToString())))/100);
        lblAvgDAVal.Text = "$" + Convert.ToString( Math.Round(Convert.ToDouble(lblAvgDAVal.Text), 0));

    }
    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "SUPTYPE")
        {
           // row.BackColor = System.Drawing.Color.LightGray;
            row.Cells[0].Attributes["colspan"] = "17";
            row.Cells[0].Text = "&nbsp;&nbsp;" + row.Cells[0].Text;
            //row.Cells[0].ForeColor = System.Drawing.Color.Black;
            row.Cells[0].Font.Bold = true;
            row.Cells[0].CssClass = "Summary-SubHeaderStyle-css";


        }

      

    }
   
    private void RenderHeader(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count-1; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!info.MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";


                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (info.StartColumns.Contains(i))
                {


                    output.Write(string.Format("<th align='center' style='border:1px solid #000;border-collapse:collapse;background:url(../Images/gridheaderbg-image.png) left 0px repeat-x'  colspan='{0}'>{1}</th>",
                             info.StartColumns[i], info.Titles[i]));


                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)
        for (int i = 0; i < info.MergedColumns.Count; i++)
        {
            //if qtn eval 

            TableCell cell = (TableCell)container.Controls[info.MergedColumns[i]];

            cell.CssClass = "Summary-HeaderStyle-css";
            cell.RenderControl(output);

        }

       
        info.MergedColumns.Clear();
        info.StartColumns.Clear();
        info.Titles.Clear();

      
    }


   
    protected void grdSummary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.SetRenderMethodDelegate(RenderHeader);

        }
 
    }

    protected void grdSummary_DataBound(object sender, EventArgs e)
    {
        try
        {
            int igr = 0, tmp = 0;
            foreach (TableCell cl in grdSummary.HeaderRow.Cells)
            {
              
                    if (igr > 1 && igr < grdSummary.HeaderRow.Cells.Count - 1)
                    {
                        DateTime tempDate = DateTime.Now;
                        string CurrentMonth = tempDate.ToString("MMM");
                        if (igr == 2)
                        {
                            string headername = "";

                            if (cl.Text.Split('_')[1] == DateTime.Now.Year.ToString())
                            {
                                headername = cl.Text.Split('_')[1] + "<br>YTD(" + DateTime.Now.Day.ToString() + " " + CurrentMonth + ") : " + cl.Text.Split('_')[2];
                            }
                            else
                            {
                                headername = cl.Text.Split('_')[1] + "<br>JAN-DEC : " + cl.Text.Split('_')[2];
                            }


                            info.AddMergedColumns(new int[] { igr, igr + 1, igr + 2, igr + 3 }, headername);
                            //tmp = 0;
                        }
                        else if ((tmp % 4) == 0)
                        {
                            string headername = "";
                            if (cl.Text.Split('_')[1] == DateTime.Now.Year.ToString())
                            {
                                headername = cl.Text.Split('_')[1] + "<br>YTD(" + DateTime.Now.Day.ToString() + " " + CurrentMonth + ") : " + cl.Text.Split('_')[2];
                            }
                            else
                            {
                                headername = cl.Text.Split('_')[1] + "<br>JAN-DEC : " + cl.Text.Split('_')[2];
                            }
                            info.AddMergedColumns(new int[] { igr, igr + 1, igr + 2, igr + 3 }, headername);
                            tmp = 0;
                        }
                        cl.Text = cl.Text.Split('_')[0];
                        tmp++;
                    }
                    igr++;
               
            }




            // grdSummary.FooterRow.Cells[1].Text = "Subtotal (% of time)";
            if (ds.Tables[1].Rows.Count > 0)
            {
                grdSummary.FooterRow.Cells[1].Text = "<hr><br>" + ds.Tables[1].Rows[0][1].ToString() + "<hr><br>" + ds.Tables[1].Rows[1][1].ToString() + "<hr><br>" + ds.Tables[1].Rows[2][1].ToString() + "<hr><br>" + ds.Tables[1].Rows[3][1].ToString();
               
                //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                //{

                //}
            }
           
            
            string str=string.Empty;
          

            for (int i = 0; i <= info.MergedColumns.Count - 4; i += 4)
            {

                grdSummary.HeaderRow.Cells[i + 2].Text = "Days In DD";
                grdSummary.HeaderRow.Cells[i + 2].Width = 30;
                grdSummary.FooterRow.Cells[i + 2].Width = 30;
                grdSummary.FooterRow.Cells[i + 2].Text = "<hr><br>" + ds.Tables[1].Rows[0][i + 2].ToString() + "<hr><br>&nbsp;<hr><br>&nbsp;<hr><br>&nbsp;";
                grdSummary.FooterRow.Cells[i + 2].HorizontalAlign = HorizontalAlign.Center;

               //.Split('%')[0].ToString();
                 str = ds.Tables[1].Rows[3][i + 3].ToString();
                 if (str != "")
                 {
                     if (str.Split('%')[0].ToString()== "-0")
                     {
                         ds.Tables[1].Rows[3][i + 3] = "0%";
                     }
                 }

                else if (str == "")
                {
                    ds.Tables[1].Rows[3][i + 3] = " ";
                }

         
                grdSummary.HeaderRow.Cells[i + 3].Text = "Total Days Abroad including DD days";
                grdSummary.HeaderRow.Cells[i + 3].Width = 200;
                grdSummary.FooterRow.Cells[i + 3].Width = 200;
                grdSummary.FooterRow.Cells[i + 3].Text = "<hr><br>" + ds.Tables[1].Rows[0][i + 3].ToString().Trim() + "<hr><br>" + ((Convert.ToString(ds.Tables[1].Rows[1][i + 3]).Trim()) == "" ? "&nbsp;" : Convert.ToString(ds.Tables[1].Rows[1][i + 3])) + "<hr><br>" + ((Convert.ToString(ds.Tables[1].Rows[2][i + 3]).Trim()) == "" ? "&nbsp;" : Convert.ToString(ds.Tables[1].Rows[2][i + 3])) + "<hr><br>" + ((Convert.ToString(ds.Tables[1].Rows[3][i + 3]).Trim()) == "" ? "&nbsp;" : Convert.ToString(ds.Tables[1].Rows[3][i + 3]));
                grdSummary.FooterRow.Cells[i + 3].HorizontalAlign = HorizontalAlign.Center;

                grdSummary.HeaderRow.Cells[i + 4].Text = "% of Time Abroad";
                grdSummary.HeaderRow.Cells[i + 4].Width = 50;
                grdSummary.FooterRow.Cells[i + 4].Width = 50;
                grdSummary.FooterRow.Cells[i + 4].Text = "<hr><br>" + ((Convert.ToString(ds.Tables[1].Rows[0][i + 4]).Trim()) == "" ? "&nbsp;" : Convert.ToString(ds.Tables[1].Rows[0][i + 4]).Trim()) + "<hr><br>&nbsp;<hr><br>&nbsp;<hr><br>&nbsp;";
                grdSummary.FooterRow.Cells[i + 4].HorizontalAlign = HorizontalAlign.Center;

                grdSummary.HeaderRow.Cells[i + 5].Text = "% of Total Days(Tech Supts/Marine Supts)";
                grdSummary.HeaderRow.Cells[i + 5].Width = 50;
                grdSummary.FooterRow.Cells[i + 5].Width = 50;
                grdSummary.FooterRow.Cells[i + 5].Text = "<hr><br>" + ((Convert.ToString(ds.Tables[1].Rows[0][i + 5]).Trim()) == "" ? "&nbsp;" : Convert.ToString(ds.Tables[1].Rows[0][i + 5]).Trim()) + "<hr><br>&nbsp;<hr><br>&nbsp;<hr><br>&nbsp;";
                grdSummary.FooterRow.Cells[i + 5].HorizontalAlign = HorizontalAlign.Center;



               


            }


            grdSummary.HeaderRow.Cells[0].Visible = false;
            grdSummary.FooterRow.Cells[0].Visible = false;
            grdSummary.HeaderRow.Cells[grdSummary.HeaderRow.Cells.Count - 1].Visible = false;
            grdSummary.FooterRow.Cells[grdSummary.FooterRow.Cells.Count - 1].Visible = false;
            grdSummary.HeaderRow.Cells[1].Text = "Attendances - Days Abroad";
            grdSummary.HeaderRow.Cells[1].Attributes.Add("style", "text-align:center;width:500px");
            grdSummary.FooterRow.Cells[1].Attributes.Add("style", "text-align:left;width:850px");

           // grdSummary.HeaderRow.Cells[1].Width = 500;
           // grdSummary.FooterRow.Cells[1].Width = 750;
            foreach (GridViewRow gr in grdSummary.Rows)
            {
                gr.Cells[0].Visible = false;
                gr.Cells[gr.Cells.Count - 1].Visible = false;
                gr.Cells[1].HorizontalAlign = HorizontalAlign.Left;




            }

            for (int i = 0; i < grdSummary.Rows.Count ; i++)
            {
                if (grdSummary.Rows[i].Cells[1].Text == "Subtotal (% of time)")
                {

                    //grdSummary.Rows[i].BorderColor = Color.Black;
                    //grdSummary.Rows[i].BorderStyle = BorderStyle.Solid;
                    //grdSummary.Rows[i].BorderWidth = 1;
                    //grdSummary.Rows[i].Font.Bold = true;


                    grdSummary.Rows[i].Attributes.Add("style", "font-weight:bold;border:Black;border:solid; border:1px;background-color:#999999;color:black");
                }
            }

        }
        catch (Exception ex)
        {
        }

  
    
    }


    protected void BtnPrevious_Click(object sender, EventArgs e)
    {
        ViewState["info"] = null;
        ViewState["EndYear"] = Convert.ToString(Convert.ToInt32(ViewState["EndYear"].ToString()) - 1);
        EndYear = ViewState["EndYear"].ToString();
      

        grdSummary.DataSource = null;
        grdSummary.DataBind();
        
        GridViewHelper helper = new GridViewHelper(this.grdSummary);
        helper.RegisterGroup("SUPTYPE", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
        BindInspectionSummary(EndYear);

        if (grdSummary.Rows.Count > 0)
        {
           
            tblStat.Visible = true;

     
        }
        else
        {
            tblStat.Visible = false;
          
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "<script> hidedivision(); </script>", false);
        }
    }
    protected void BtnNext_Click(object sender, EventArgs e)
    {
        ViewState["info"] = null;
        ViewState["EndYear"] = Convert.ToString(Convert.ToInt32(ViewState["EndYear"].ToString()) + 1);
        EndYear = ViewState["EndYear"].ToString();
        grdSummary.DataSource = null;
        grdSummary.DataBind();
      
        GridViewHelper helper = new GridViewHelper(this.grdSummary);
        helper.RegisterGroup("SUPTYPE", true, true);
        helper.GroupHeader += new GroupEvent(helper_GroupHeader);
       
        BindInspectionSummary(EndYear);

        if (grdSummary.Rows.Count > 0)
        {
            tblStat.Visible = true;
           
        }
        else
        {
            tblStat.Visible = false;
          
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "<script> hidedivision(); </script>", false);
        }
    }
    protected void grdSummary_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void grdSummary_Sorted(object sender, EventArgs e)
    {

    }
    protected void BtnPrintPDF_Click(object sender, ImageClickEventArgs e)
    {
      EO.Pdf.HtmlToPdf.Options.PageSize = new SizeF(11.69f, 8.27f);
        PdfDocument doc=new PdfDocument();

        string GUID = Guid.NewGuid().ToString();
        string filePath = Server.MapPath("~/Uploads/Reports/" + GUID + ".pdf");
       
        EO.Pdf.Runtime.AddLicense("p+R2mbbA3bNoqbTC4KFZ7ekDHuio5cGz4aFZpsKetZ9Zl6TNHuig5eUFIPGe" +
    "tcznH+du5PflEuCG49jjIfewwO/o9dB2tMDAHuig5eUFIPGetZGb566l4Of2" +
    "GfKetZGbdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW6yuCwb6y9xtyxdabw" +
    "+g7kp+rp2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzr" +
    "peb7z7iJWZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihb6W0" +
    "s8uud4SOscufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h8=");
        EO.Pdf.HtmlToPdf.Options.FooterHtmlFormat = "<div style='text-align:center; font-family:Tahoma; font-size:10px;'>Page {page_number} of {total_pages}</div>";
        HtmlToPdf.ConvertHtml(hdnContent.Value, filePath);


        newtab = UDFLib.ConvertToInteger(ViewState["newtabnumber"]);
        newtab++;
        ViewState["newtabnumber"] = newtab;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "hideText", "window.open('../../Uploads/Reports/" + GUID + ".pdf','YSUM" + newtab + "');", true);  // (  this.GetType(), "OpenWindow", "window.open('../../Uploads/InspectionReport.pdf','_newtab');", true);


    }

  
}