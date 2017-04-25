using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Globalization;

using SMS.Business.Infrastructure;


public partial class VesselDetails : System.Web.UI.Page
{
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    IFormatProvider iFormatProvider = CultureInfo.GetCultureInfo("en-GB");


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // TreeviewBind();
            

            StringBuilder transmittedQueryStrings = new StringBuilder();
            string x = "";
            string y = "";

            // Parse query strings if there are any
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (transmittedQueryStrings.Length > 0)
                    transmittedQueryStrings.Append("&amp;");
                transmittedQueryStrings.Append(string.Format("{0}={1}", key, Request.QueryString[key]));
                x = string.Format("{0}", key).ToUpper();
                y = string.Format("{0}", Request.QueryString[key]).ToUpper();
            }

            if (x == "VIEWID" || x == "PRINTID")
            {
                ViewVesselDetails(int.Parse(y.ToString().Trim()));
                
            }
        }
    }
    public void ViewVesselDetails(int Key)
    {

        DataTable dtVessel = objBLL.GetVesselDetails_ByID(Key);
        FormView1.DataSource = dtVessel;
        FormView1.DataBind();
        FormView3.DataSource = dtVessel;
        FormView3.DataBind();
        FormView4.DataSource = dtVessel;
        FormView4.DataBind();
        FormView5.DataSource = dtVessel;
        FormView5.DataBind();
        FormView6.DataSource = dtVessel;
        FormView6.DataBind();
        FormView7.DataSource = dtVessel;
        FormView7.DataBind();
        ShipImg.ImageUrl = "~/Vessel/ShipImage/" + dtVessel.Rows[0]["Vessel_Image"].ToString();
        TankImg.ImageUrl = "~/Vessel/shipLayout/" + dtVessel.Rows[0]["Vessel_Tank_Image"].ToString();
        lblShipName.Text = dtVessel.Rows[0]["Vessel_Short_Name"].ToString();

        DataSet ds = objBLL.GetInmarsatValues_ForVesselID(Key);

        DataTable dt1 = ds.Tables[0];
        DataTable dt2 = ds.Tables[0];
        dt1 = RemoveDuplicateRows(dt1, "terminal");
        dt2 = RemoveDuplicateRows(dt2, "channel");
       // GridView1.DataSource = ds.Tables["Dtl_IID_INMARSAT"].DefaultView;
       // GridView1.DataBind();
        //DataTable SourceDt = new DataTable();



        for (int i = 0; i < dt1.Rows.Count + 1; i++)
        {
            TableRow tr = new TableRow();
            for (int j = 0; j < 5; j++)
            {
                TableCell tc = new TableCell();
                Label lbl = new Label();
                tc.Text = "";
                tc.BorderWidth = 1;

                tr.Cells.Add(tc);
                if (j == 0 && i > 0)
                {
                    tc.Text = "<b>Inmarsat-</b>" + dt1.Rows[i - 1]["terminal"].ToString();
                }

            }
            tr.BorderStyle = BorderStyle.Solid;
            Table1.Rows.Add(tr);
            if (i >=1)
            {
                switch (ds.Tables[0].Rows[i - 1]["Channel"].ToString())
                {
                    case "Telex":
                        {
                            Table1.Rows[i].Cells[1].Text = ds.Tables[0].Rows[i - 1]["Phone"].ToString();
                            break;
                        }
                    case "Telephone":
                        {
                            Table1.Rows[i ].Cells[2].Text = ds.Tables[0].Rows[i - 1]["Phone"].ToString();
                            break;
                        }
                    case "Fax":
                        {
                            Table1.Rows[i ].Cells[3].Text = ds.Tables[0].Rows[i - 1]["Phone"].ToString();
                            break;
                        }
                    case "Data":
                        {
                            Table1.Rows[i].Cells[4].Text = ds.Tables[0].Rows[i - 1]["Phone"].ToString();
                            break;
                        }

                }

            }

        }
        Table1.Rows[0].Cells[0].Text = "";
        Table1.Rows[0].Cells[1].Text = "<b>Telex</b>";
        Table1.Rows[0].Cells[2].Text = "<b>Telephone</b>";
        Table1.Rows[0].Cells[3].Text = "<b>Fax</b>";
        Table1.Rows[0].Cells[4].Text = "<b>Data</b>";
        //GridView1.DataSource = dt2;
        //GridView1.DataBind();
    }
    public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
    {
        Hashtable hTable = new Hashtable();
        ArrayList duplicateList = new ArrayList();

        foreach (DataRow drow in dTable.Rows)
        {
            if (hTable.Contains(drow[colName]))
                duplicateList.Add(drow);
            else
                hTable.Add(drow[colName], string.Empty);
        }

        foreach (DataRow dRow in duplicateList)
            dTable.Rows.Remove(dRow);

        return dTable;
    }  

    }







