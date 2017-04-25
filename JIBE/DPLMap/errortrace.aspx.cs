using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Data.SqlClient;
using System.IO;
//using Dal;
using SMS.Data;

public partial class errortrace : System.Web.UI.Page
{
    CheckDBConfig config = new CheckDBConfig();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder strError = new StringBuilder();
        strError.Append("<b><h1>The following ships having improper longitude and latitude Information:</b></h1>");
        strError.Append("<br>");
        DataSet ds = new DataSet();
        ds = Ves_getUserName();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            strError.Append("#");
            strError.Append("Latitude "+dr[3].ToString());
            strError.Append("  Longitude "+dr[4].ToString());
            strError.Append("#");
            
            
        }


        //this.Page.RegisterStartupScript("onclick", "<script language='javascript' type='text/javascript'>createPopUpwindow_err('" + strError + "');</script>");

        //copy the content into the file

        string path = "~/ErrorLog/" + "GoogleMap_Log" + DateTime.Today.Day.ToString() + "-" + DateTime.Today.Month.ToString() + "-" + DateTime.Today.Year.ToString() + ".txt";
        
        if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
        {
            File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
        }


        using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
        {


            w.WriteLine("\r\nLog Entry : ");
            w.WriteLine("{0}", DateTime.Now.ToString());
            string err = strError.ToString();

            string[] str_enter = err.Split('#');

            for (int j = 0; j < str_enter.Length; j++)
            {
                w.Write("\r\n");
                w.Write(str_enter[j]);
            }




            w.WriteLine("__________________________");
            w.Flush();
            w.Close();
        }
        
    }


    public DataSet Ves_getUserName()
    {
        DataSet ds;
        string constring_n_table = config.con_prepare();
        if (constring_n_table != "" || constring_n_table != null)
        {
            try
            {
                string[] con_n_table = constring_n_table.Split('#');
                SqlConnection sqlCon = new SqlConnection(con_n_table[0]);
                string tab_name = con_n_table[1];
                //string qry = "SELECT xmid,xship,xdate,xlt,xlg,xco FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY xship ORDER BY xship,xdate DESC ) AS 'RowNumber', xship,xdate ,xmid,xlt,xlg,xco FROM " + tab_name + "  where xdate is not null and xship is not null ) dt WHERE RowNumber <= 1";
                //string qry = "SELECT xmid,xship,convert(varchar,xdate,106)as shipdate,xlt,xlg,xco FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY xship ORDER BY xship,xdate DESC ) AS 'RowNumber', xship,xdate ,xmid,xlt,xlg,xco FROM " + tab_name + "  where xdate is not null and xship is not null ) dt WHERE RowNumber <= 1";
                //string qry = "SELECT xmid,xship,convert(varchar,xdate,106)as shipdate,xlt,xlg,xco FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY xship ORDER BY xship,xdate DESC ) AS 'RowNumber', xship,xdate ,xmid,xlt,xlg,xco FROM " + tab_name + "  where xdate is not null and xship is not null ) dt WHERE RowNumber <= 1 and xship in(select Vessel_Name from Lib_Vessels where Active_Status=1)";

                string qry = "SELECT xmid,isnull(xship,'') as xship,convert(varchar,xdate,106)as shipdate,xlt,xlg,xco FROM (SELECT ROW_NUMBER() OVER ( PARTITION BY xship ORDER BY xship,xdate DESC ) AS 'RowNumber', xship,xdate ,xmid,xlt,xlg,xco FROM  " + tab_name + "   where isnull(xdate ,'1900-01-01') !='1900-01-01' and isnull(xship,'') !='' ) dt WHERE RowNumber <= 1 and isnull(xship,'') !=''";
                ds = SqlHelper.ExecuteDataset(sqlCon, CommandType.Text, qry);
                return ds;
            }
            catch (Exception ex)
            {
                ds = null;
                //Response.Write(ex.ToString());
                return ds;

            }

        }
        else
        {
            ds = null;
            Response.Write("Error in the Data Configuration");
            return ds;
        }

    }
}
