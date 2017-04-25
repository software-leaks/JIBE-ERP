using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.ComponentModel;



public class MergeGridviewHeader
{

    public MergeGridviewHeader()
    {
        MergedColumns.Clear();
        StartColumns.Clear();
        Titles.Clear();
        MergedColumnCss.Clear();
    }



    public static void SetProperty(MergeGridviewHeader_Info columnInfo)
    {
        MergeGridviewHeader.MergedColumns = columnInfo.MergedColumns;
        MergeGridviewHeader.StartColumns = columnInfo.StartColumns;
        MergeGridviewHeader.Titles = columnInfo.Titles;
        MergeGridviewHeader.MergedColumnCss = columnInfo.MergedColumnCss;
    }
    public static void RenderHeader(HtmlTextWriter output, Control container)
    {

        for (int i = 0; i < container.Controls.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[i];
            //stretch non merged columns for two rows
            if (!MergedColumns.Contains(i))
            {
                cell.Attributes["rowspan"] = "2";

                cell.RenderControl(output);
            }
            else //render merged columns common title
                if (StartColumns.Contains(i))
                {
                    if (MergedColumnCss.ContainsKey(i))
                    {
                        output.Write(string.Format("<th align='center' colspan='{0}' class='{2}' >{1}</th>",
                                StartColumns[i], Titles[i], MergedColumnCss[i]));
                    }
                    else
                    {
                        output.Write(string.Format("<th align='center' colspan='{0}' >{1}</th>",
                               StartColumns[i], Titles[i]));
                    }

                }
        }

        //close the first row	
        output.RenderEndTag();
        //set attributes for the second row
        //grid.HeaderStyle.AddAttributesToRender(output);
        //start the second row
        output.RenderBeginTag("tr");



        //render the second row (only the merged columns)

        int currentCssID = 0;
        for (int i = 0; i < MergedColumns.Count; i++)
        {
            TableCell cell = (TableCell)container.Controls[MergedColumns[i]];
            if (MergedColumnCss.ContainsKey(MergedColumns[i]))
                currentCssID = MergedColumns[i];

            if (MergedColumnCss.ContainsKey(currentCssID))
                cell.CssClass = MergedColumnCss[currentCssID];

            cell.RenderControl(output);

        }

        MergedColumns.Clear();
        StartColumns.Clear();
        Titles.Clear();
        MergedColumnCss.Clear();

    }


    // indexes of merged columns
    public static List<int> MergedColumns = new List<int>();
    // key-value pairs: key = the first column index, value = number of the merged columns
    public static Hashtable StartColumns = new Hashtable();
    // key-value pairs: key = the first column index, value = common title of the merged columns 
    public static Hashtable Titles = new Hashtable();

    public static Dictionary<int, string> MergedColumnCss = new Dictionary<int, string>();




}
[Serializable]
public class MergeGridviewHeader_Info
{
    public MergeGridviewHeader_Info()
    {
        MergedColumnCss.Clear();
        MergedColumns.Clear();
        StartColumns.Clear();
        Titles.Clear();
    }
    
    // indexes of merged columns
    public List<int> MergedColumns = new List<int>();
    // key-value pairs: key = the first column index, value = number of the merged columns
    public Hashtable StartColumns = new Hashtable();
    // key-value pairs: key = the first column index, value = common title of the merged columns 
    public Hashtable Titles = new Hashtable();

    public Dictionary<int, string> MergedColumnCss = new Dictionary<int, string>();

    public void AddMergedColumns(int[] columnsIndexes, string title, string Css = "")
    {
        MergedColumns.AddRange(columnsIndexes);
        StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
        Titles.Add(columnsIndexes[0], title);
        if (Css != "")
            MergedColumnCss.Add(columnsIndexes[0], Css);

    }

    public void AddMergedColumns(int[] columnsIndexes, string title)
    {
        MergedColumns.AddRange(columnsIndexes);
        StartColumns.Add(columnsIndexes[0], columnsIndexes.Length);
        Titles.Add(columnsIndexes[0], title);
       

    }
}

