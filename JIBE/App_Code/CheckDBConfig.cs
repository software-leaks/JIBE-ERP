using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
//using Dal;

/// <summary>
/// Summary description for CheckDBConfig
/// </summary>
public class CheckDBConfig
{
	public CheckDBConfig()
	{
	}

    public string con_prepare()
    {
        //string Connectionstring = "Data Source=ELOGSRV; Initial Catalog=eLog; User ID=sa;Password = eLog!234;";
        
        StreamReader rdr = null;
        string str_full = null;

        try
        {
            rdr = new StreamReader("c:\\gmap_config.ini");


            while (!(rdr.EndOfStream))
            {
                str_full += rdr.ReadLine();

            }

            string sftemp = str_full.ToString();

            string[] str_etr = sftemp.Split('"');

            for (int j = 0; j < str_etr.Length; j++)
            {
                //Response.Write("<br>" + str_etr[j].ToString());
            }

            string conString_n_table = "Data Source=" + str_etr[1] + "; Initial Catalog=" + str_etr[3] + "; User ID=" + str_etr[5] + "; Password=" + str_etr[7]+"#"+str_etr[9];
            return conString_n_table;

        }
        catch (Exception ex)
        {
            return "";
        }
        return "";
    } 
}
