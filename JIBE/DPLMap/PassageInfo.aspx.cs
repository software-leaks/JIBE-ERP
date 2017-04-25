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
using System.Data.SqlClient;
using System.Text;
using SMS.Data;
//using Dal;

public partial class PassageInfo : System.Web.UI.Page
{

    public static string _internalConnection_dpl;
    public static DataSet ds_usercompany = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Map_Load();
            //fill_ship();
            setrights();
            int company_status = Convert.ToInt32(ds_usercompany.Tables[0].Rows[0][0].ToString());
            fill_Company(company_status);
            fill_TechManager(company_status);
            shipbyfleet();
        }
        //default view of the map                                                                                                                                       
        //Default_ViewMap();
    }

    public void shipbyfleet()
    {
        ddl_shipname.DataSource = Ship_Techmanager(ddlTechmanager.SelectedItem.Text.ToString());
        ddl_shipname.DataTextField = "Vessel_Name";
        ddl_shipname.DataValueField = "Vessel_Code";
        ddl_shipname.DataBind();
    }
    
    //identify the rights and set to those things to the dataset ds_usercompany
    public void setrights()
    {
        ds_usercompany = returnrights();//this dataset having company status, techmanager, company name
    }

    public DataSet returnrights()
    {
        int userid = Convert.ToInt32(Session["user"]);
        
        _internalConnection_dpl = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        DataSet ds = new DataSet();
        string query = "exec sp_findusercompany " + userid;


        SqlConnection con = new SqlConnection(_internalConnection_dpl);
        try
        {

            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
            return ds;

        }
        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
        return ds;
    }


    public void fill_Company(int chk_esm)
    {
        DataSet ds = new DataSet();
        ds = data_Company();
        ddlCompany.DataSource = ds;
        ddlCompany.DataTextField = "Company_Name";
        ddlCompany.DataValueField = "Company_Code";

        ddlCompany.DataBind();

        if (chk_esm == 1)
        {
            ddlCompany.Enabled = true;
        }
        else
        {
            ddlCompany.Enabled = false;
        }
        string scompany = ds_usercompany.Tables[0].Rows[0]["Company_Name"].ToString();
        ddlCompany.Items.FindByText(scompany).Selected = true;
    }

    public void fill_TechManager(int chkesm)
    {
        DataSet ds = new DataSet();
        ds = data_TechManager();
        ddlTechmanager.DataSource = ds;
        ddlTechmanager.DataTextField = "Tech_Manager";
        ddlTechmanager.DataBind();

        string stechmanager = ds_usercompany.Tables[0].Rows[0]["Tech_Manager"].ToString();
        ddlTechmanager.Items.FindByText(stechmanager).Selected = true;

        if (chkesm == 1)
        {
            ddlTechmanager.Enabled = true;
        }
        else
        {
            ddlTechmanager.Enabled = false;
        }
    }

    public DataSet data_TechManager()
    {
        _internalConnection_dpl = ConfigurationManager.ConnectionStrings["esmcon"].ConnectionString;
        string qry = "";
        DataSet ds = new DataSet();


        qry = "select Tech_Manager from Lib_Vessels  where Active_Status=1 group by Tech_Manager";
        SqlConnection con = new SqlConnection(_internalConnection_dpl);
        try
        {

            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, qry);
            return ds;

        }
        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
        return ds;

    }


    public DataSet data_Company()
    {
        _internalConnection_dpl = ConfigurationManager.ConnectionStrings["esmcon"].ConnectionString;
        string qry = "";
        DataSet ds = new DataSet();


        qry = "select id,Company_Code,Company_Name from GC_Company where Active_Status=1";
        SqlConnection con = new SqlConnection(_internalConnection_dpl);
        try
        {

            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, qry);
            return ds;

        }
        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
        return ds;

    }

    public void Map_Load()
    {
        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        GoogleMapForASPNet1.GoogleMapObject.Width = "900px"; //900 You can also specify percentage(e.g. 80%) here
        GoogleMapForASPNet1.GoogleMapObject.Height = "600px";//600
        GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 2;
        GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;

        
        
        
        
        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.
        //LINES ARE COMMENTED
        //Specify initial Zoom level.
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 400;
        //Specify Center Point for map. Map will be centered on this point.
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.66619, -79.44268);
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.SATELLITE_MAP;
        
    }


    public void fill_ship()
    {
        string Connectionstring = ConfigurationManager.ConnectionStrings["esmcon"].ToString();
        SqlConnection sqlCon = new SqlConnection(Connectionstring);
        //string queryString = "select UserID,First_Name,Middle_Name,Last_Name,User_Type,Designation,Approval_Limit,CompanyCode,Active_Status from Lib_User";
        string strqry = "";
        strqry = @"SELECT isnull(Vessel_Code,'') as xship, convert(varchar,Telegram_Date,106)as shipdate, 
                    Longitude_Degrees,Longitude_Minutes,Longitude_Seconds,Longitude_N_S,
                    Latitude_Degrees,Latitude_Minutes,Latitude_Seconds,LATITUDE_E_W into #tab1 
                    FROM TEC_Dtl_Telegrams where isnull(Telegram_Date ,'1900-01-01') !='1900-01-01' and isnull(Vessel_Code,'') !='' and isnull(Vessel_Code,'') !='' 

                    select * from #tab1
                    select distinct xship,Vessel_Name from #tab1 t,Lib_Vessels l where t.xship=l.Vessel_Code order by xship drop table #tab1";

        SqlDataAdapter da = new SqlDataAdapter(strqry, Connectionstring);
        DataSet ds = new DataSet();
        da.Fill(ds, "shipname");

        ddl_shipname.DataSource = ds.Tables[1];
        ddl_shipname.DataTextField = "Vessel_Name";
        ddl_shipname.DataValueField = "xship";
        ddl_shipname.DataBind();
        


    }



    protected void btnSimple_Click(object sender, EventArgs e)
    {
        GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;
    }
    protected void btnSatellite_Click(object sender, EventArgs e)
    {
        GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.SATELLITE_MAP;
    }
    protected void btnHybrid_Click(object sender, EventArgs e)
    {
        GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.HYBRID_MAP;

    }
    protected void GoogleMapForASPNet1_Load(object sender, EventArgs e)
    {
        //GoogleMapForASPNet1.GoogleMapObject.Height = "768px";
        //GoogleMapForASPNet1.GoogleMapObject.Width = "1024px";
        //GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 2;
    }

    //this function is getting the information about the active ships on the sea
    public DataSet Ves_getUserName()
    {
        string Connectionstring = ConfigurationManager.ConnectionStrings["esmcon"].ToString();
        SqlConnection sqlCon = new SqlConnection(Connectionstring);
        //string queryString = "select UserID,First_Name,Middle_Name,Last_Name,User_Type,Designation,Approval_Limit,CompanyCode,Active_Status from Lib_User";
        SqlDataAdapter da = new SqlDataAdapter("select * from Dtl_vessel dv,maps_backup m where dv.Vessel_Code=m.VesselId", Connectionstring);
        DataSet ds = new DataSet();
        da.Fill(ds, "usernames");
        return ds;
    }


    






    public void mapLoad()
    {

        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.


        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.Width = "800px"; // You can also specify percentage(e.g. 80%) here
        //GoogleMapForASPNet1.GoogleMapObject.Height = "600px";   

        //Specify initial Zoom level.
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 400;

        //Specify Center Point for map. Map will be centered on this point.
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.66619, -79.44268);

        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.SATELLITE_MAP;
        GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;

        //Add pushpins for map. 
        //This should be done with intialization of GooglePoint class. 
        //ID is to identify a pushpin. It must be unique for each pin. Type is string.
        //Other properties latitude and longitude.

        DataSet ds = new DataSet();
        ds = Ves_getUserName();



        //GooglePoint GP1 = new GooglePoint();
        //GP1.ID = "1";
        ////GP1.Latitude = 43.65669;
        ////GP1.Longitude = -79.44268;
        //GP1.IconImage = "boat.png";
        //GP1.Latitude = double.Parse(txt_latitude.Text.Trim().ToString());
        //GP1.Longitude = double.Parse(txt_lang.Text.Trim().ToString());
        //GP1.InfoHTML = "This Is The Location";
        //GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);

        StringBuilder sb = new StringBuilder();

        string strInfoHtml = "";

        GooglePoint[] gp = new GooglePoint[ds.Tables[0].Rows.Count];

        int i = 0;

        //string str = gp[1].ToString();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            gp[i] = new GooglePoint();
            //int angle = Convert.ToInt32(dr[80].ToString());
            string angle = dr[80].ToString();

            if (angle == "45.00")
                gp[i].IconImage = "UpSide45_f.png";
            if (angle == "135.00")
                gp[i].IconImage = "UpSide_135.png";
            if (angle == "225.00")
                gp[i].IconImage = "Down_225.png";
            if (angle == "315.00")
                gp[i].IconImage = "Down_315.png";



            //gp[i].IconImageHeight = 5;
            //gp[i].IconImageWidth = 5;



            gp[i].Latitude = double.Parse(dr[75].ToString());
            gp[i].Longitude = double.Parse(dr[76].ToString());
            //strInfoHtml = ds.Tables[0].Columns[0].ToString() + ":" + dr[0].ToString() + "<br>" + ds.Tables[0].Columns[1].ToString() + ":" + dr[1].ToString() + "<br>" + ds.Tables[0].Columns[2].ToString() + ":" + dr[2].ToString() + "<br>" + ds.Tables[0].Columns[3].ToString() + ":" + dr[3].ToString() + "<br>" + ds.Tables[0].Columns[4].ToString() + ":" + dr[4].ToString() + "<br>" + ds.Tables[0].Columns[5].ToString() + ":" + dr[5].ToString() + "<br>" + ds.Tables[0].Columns[6].ToString() + ":" + dr[6].ToString() + "<br>" + ds.Tables[0].Columns[7].ToString() + ":" + dr[7].ToString() + "<br>" + ds.Tables[0].Columns[8].ToString() + ":" + dr[8].ToString();
            //string link = "~/FolderTest/TestPage.aspx";

            string link = "FolderTest/TestPage.aspx";

            strInfoHtml = "<a href=" + link + ">" + dr[4].ToString() + "</a>" + "<br>Last Noon Report" + "<br>Crew List";
            //strInfoHtml = "<a href="javascript:window.open('www.4shared.com')">hjhgh</a>";

            gp[i].InfoHTML = strInfoHtml.ToString();

            gp[i].IconShadowWidth = 200;


            gp[i].ToolTip = strInfoHtml.ToString();


            GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);
            i++;
        }

    }

    static int tm_count = 0;



    public void timer_map(int tcount)
    {

        GoogleMapForASPNet1.GoogleMapObject.APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];

        //Specify width and height for map. You can specify either in pixels or in percentage relative to it's container.


        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.Width = "800px"; // You can also specify percentage(e.g. 80%) here
        //GoogleMapForASPNet1.GoogleMapObject.Height = "600px";   

        //Specify initial Zoom level.
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 400;

        //Specify Center Point for map. Map will be centered on this point.
        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.CenterPoint = new GooglePoint("1", 43.66619, -79.44268);

        //LINES ARE COMMENTED
        //GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.SATELLITE_MAP;
        GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.NORMAL_MAP;

        //Add pushpins for map. 
        //This should be done with intialization of GooglePoint class. 
        //ID is to identify a pushpin. It must be unique for each pin. Type is string.
        //Other properties latitude and longitude.

        DataSet ds = new DataSet();
        ds = Ves_getUserName();



        //GooglePoint GP1 = new GooglePoint();
        //GP1.ID = "1";
        ////GP1.Latitude = 43.65669;
        ////GP1.Longitude = -79.44268;
        //GP1.IconImage = "boat.png";
        //GP1.Latitude = double.Parse(txt_latitude.Text.Trim().ToString());
        //GP1.Longitude = double.Parse(txt_lang.Text.Trim().ToString());
        //GP1.InfoHTML = "This Is The Location";
        //GoogleMapForASPNet1.GoogleMapObject.Points.Add(GP1);

        StringBuilder sb = new StringBuilder();

        string strInfoHtml = "";

        GooglePoint[] gp = new GooglePoint[ds.Tables[0].Rows.Count];

        int i = 0;

        //string str = gp[1].ToString();

        foreach (DataRow dr in ds.Tables[0].Rows)
        {

            gp[i] = new GooglePoint();
            //int angle = Convert.ToInt32(dr[80].ToString());
            string angle = dr[80].ToString();

            if (angle == "45.00")
                gp[i].IconImage = "UpSide45_f.png";
            if (angle == "135.00")
                gp[i].IconImage = "UpSide_135.png";
            if (angle == "225.00")
                gp[i].IconImage = "Down_225.png";
            if (angle == "315.00")
                gp[i].IconImage = "Down_315.png";


            GoogleMapForASPNet1.GoogleMapObject.Points[i].Longitude += 0.5;
            GoogleMapForASPNet1.GoogleMapObject.Points[i].Latitude -= 0.5;


            //string temp = "0.5" + tcount.ToString();
            //gp[i].Latitude = double.Parse(dr[75].ToString()) + double.Parse(temp);
            //gp[i].Longitude = double.Parse(dr[76].ToString()) - double.Parse(temp);


            string link = "FolderTest/TestPage.aspx";

            strInfoHtml = "<a href=" + link + ">" + dr[4].ToString() + "</a>" + "<br>Last Noon Report" + "<br>Crew List";
            //strInfoHtml = "<a href="javascript:window.open('www.4shared.com')">hjhgh</a>";

            gp[i].InfoHTML = strInfoHtml.ToString();

            gp[i].IconShadowWidth = 200;


            gp[i].ToolTip = strInfoHtml.ToString();


            GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);
            i++;
        }

    }


    

    protected void ddl_shipname_SelectedIndexChanged(object sender, EventArgs e)
    {

       
        int shipno = Convert.ToInt32(ddl_shipname.SelectedValue.ToString());
        //fill date drop down according to the ship selection
        fill_Date(shipno);
        //ddl_todate.SelectedItem = DateTime.Now.ToString();
        //ddl_todate.SelectedIndex = 3;


        string strtoday = DateTime.Now.ToString("dd MMM yyyy").ToString();


        ddl_todate.SelectedItem.Text = strtoday;





    }


    public void fill_Date(int shipno)
    {
        string Connectionstring = ConfigurationManager.ConnectionStrings["esmcon"].ToString();
        SqlConnection sqlCon = new SqlConnection(Connectionstring);
        //string queryString = "select UserID,First_Name,Middle_Name,Last_Name,User_Type,Designation,Approval_Limit,CompanyCode,Active_Status from Lib_User";
        SqlDataAdapter da = new SqlDataAdapter("select convert(varchar,Telegram_Date,106) as 'dateposition' from TEC_Dtl_Telegrams where Vessel_Code='" + shipno + "'", Connectionstring);
        DataSet ds = new DataSet();
        da.Fill(ds, "shipfrm");

        ddl_frmdate.DataSource = ds;
        ddl_frmdate.DataTextField = "dateposition";
        ddl_frmdate.DataBind();

        ddl_todate.DataSource = ds;
        ddl_todate.DataTextField = "dateposition";
        ddl_todate.DataBind();

    }


    public void fill_toDate(int shipno)
    {
        string Connectionstring = ConfigurationManager.ConnectionStrings["esmcon"].ToString();
        SqlConnection sqlCon = new SqlConnection(Connectionstring);
        //string queryString = "select UserID,First_Name,Middle_Name,Last_Name,User_Type,Designation,Approval_Limit,CompanyCode,Active_Status from Lib_User";
        SqlDataAdapter da = new SqlDataAdapter("select * from ship_passage where Vessel_Code='" + shipno + "'", Connectionstring);
        DataSet ds = new DataSet();
        da.Fill(ds, "shipto");

        ddl_todate.DataSource = ds;
        ddl_todate.DataTextField = "dateposition";
        ddl_todate.DataBind();

    }


    //get the records according to the date selection
    public DataSet Get_ShipRoute(int shipno, string frmdate, string todate)
    {
        string Connectionstring = ConfigurationManager.ConnectionStrings["esmcon"].ToString();
        SqlConnection sqlCon = new SqlConnection(Connectionstring);
        //string queryString = "select UserID,First_Name,Middle_Name,Last_Name,User_Type,Designation,Approval_Limit,CompanyCode,Active_Status from Lib_User";

        string strqrys;

        strqrys = "select  t.Vessel_Code,convert(varchar,Telegram_Date,106) as 'infodate',Latitude_Degrees,isnull(Latitude_Minutes,'0')as 'Latitude_Minutes',isnull(Latitude_Seconds,'0')as 'Latitude_Seconds',LATITUDE_E_W,Longitude_Degrees,isnull(Longitude_Minutes,'0')as 'Longitude_Minutes',isnull(Longitude_Seconds,'0')as 'Longitude_Seconds',Longitude_N_S,Vessel_Course,Wind_Direction,Wind_Force,AVERAGE_SPEED,Next_Port,CARGO_NAME_1,tls.Name,Vessel_Short_Name from TEC_Dtl_Telegrams t inner join Lib_Vessels v  on t.Vessel_Code=v.Vessel_Code inner join TEC_LIB_SYSTEMS_PARAMETERS tls on tls.code=t.Location_Code where t.Vessel_Code="+shipno+" and Telegram_Date between '"+frmdate+"' and '"+todate+"' and t.Latitude_Degrees is not null and t.Longitude_Degrees is not null and t.Latitude_Degrees<>0 and t.Longitude_Degrees<>0 order by Telegram_Date";

        SqlDataAdapter da = new SqlDataAdapter(strqrys, Connectionstring);
        DataSet ds = new DataSet();
        da.Fill(ds, "shiproute");
        return ds;
    }

    public void Defa_Dimension()
    {
        //GoogleMapForASPNet1.GoogleMapObject.Height = "768px";
        //GoogleMapForASPNet1.GoogleMapObject.Width = "1024px";
        //GoogleMapForASPNet1.GoogleMapObject.ZoomLevel = 2;
    }


    protected void btn_route_Click(object sender, EventArgs e)
    {

        //Defa_Dimension();

        //GoogleMapForASPNet1.GoogleMapObject.MapType = GoogleMapType.SATELLITE_MAP;

        GoogleMapForASPNet1.GoogleMapObject.Points.Clear();

        Route_Ship();
      
    }

    public void Route_Ship()
    {
        GoogleMapForASPNet1.GoogleMapObject.Width = "900px"; //   900 You can also specify percentage(e.g. 80%) here
        GoogleMapForASPNet1.GoogleMapObject.Height = "600px";



        int shipno = Convert.ToInt32(ddl_shipname.SelectedValue.ToString());
        string frmdate = ddl_frmdate.SelectedItem.Text.ToString();
        string todate = ddl_todate.SelectedItem.Text.ToString();
        string[] frmVal = frmdate.Split(' ');
        string[] toVal = todate.Split(' ');

        DataSet ds = new DataSet();

        ds = Get_ShipRoute(shipno, frmdate.ToString(), todate.ToString());

        GoogleMapForASPNet1.GoogleMapObject.AutomaticBoundaryAndZoom = false;
        GooglePoint[] gp = new GooglePoint[ds.Tables[0].Rows.Count];

        int i = 0;

        //string str = gp[1].ToString();
        string strInfoHtml = "";
        
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            gp[i] = new GooglePoint();
            gp[i].IconImage = "smalship.png";
            
            string slatitude = Conv_Deg2Decimal_new(Convert.ToDouble(dr["Latitude_Degrees"]), Convert.ToDouble(dr["Latitude_Minutes"]), Convert.ToDouble(dr["Latitude_Seconds"]), dr["LATITUDE_E_W"].ToString());
            string slongitude = Conv_Deg2Decimal_new(Convert.ToDouble(dr["Longitude_Degrees"]), Convert.ToDouble(dr["Longitude_Minutes"]), Convert.ToDouble(dr["Longitude_Seconds"]), dr["Longitude_N_S"].ToString());
            gp[i].Latitude = double.Parse(slatitude.ToString());
            gp[i].Longitude = double.Parse(slongitude.ToString());
            string qstrn = "EditId," + dr[0].ToString();
            string link = "Infrastructure/Vessel/VesselDetails.aspx?x=" + qstrn;
            
            //strInfoHtml = "<font name=arial; size=1;><a href=" + ddl_shipname.SelectedItem.Text.ToString() + ">" + ddl_shipname.SelectedItem.Text.ToString() + "</a>" + "<br>" + dr[1].ToString() + "" + "<br>Last Noon Report<br>Crew List";
            strInfoHtml = "<font name=arial; size=1;>" + ddl_shipname.SelectedItem.Text.ToString() + "<br>Telegram Date:" + dr["infodate"].ToString() + "<br>Location: " + dr["Name"].ToString() + "<br>Latitude: " + dr["Latitude_Degrees"].ToString() + " " + dr["Latitude_Minutes"].ToString() + " " + dr["Latitude_Seconds"].ToString() + " " + dr["LATITUDE_E_W"].ToString() + "<br>Longitude: " + dr["Longitude_Degrees"].ToString() + " " + dr["Longitude_Minutes"].ToString() + " " + dr["Longitude_Seconds"].ToString() + " " + dr["Longitude_N_S"].ToString() + "<br>Course: " + dr["Vessel_Course"].ToString() + "<br>Wind Direction/ Force: " + dr["Wind_Direction"].ToString() + "/" + dr["Wind_Force"].ToString() + "<br>Average speed: " + dr["AVERAGE_SPEED"].ToString() + "knts" + "<br>Next port/ETA: " + dr["Next_Port"].ToString() + " " + "<br>Cargo: " + dr["CARGO_NAME_1"].ToString();

            
            gp[i].InfoHTML = strInfoHtml.ToString();
            //gp[i].IconShadowWidth = 200;
            //gp[i].ToolTip = strInfoHtml.ToString();

            gp[i].ToolTip = gp[i].Address.ToString();

            


            

            GoogleMapForASPNet1.GoogleMapObject.Points.Add(gp[i]);
            i++;
        }
    }


    public string Conv_Deg2Decimal_new(double degree, double mins, double secs, string directions)
    {

        string inputval = "";
        string input = inputval;
        string sign = "";

        double sd = 0;



        if ((directions.ToUpper() == "S") || (directions.ToUpper() == "W"))
        {
            sign = "-";
        }


        sd = (degree) + (mins / 60) + (secs / 3600);

        if (sign == "-")
        {
            sd = sd * (-1);
        }


        sd = Math.Round(sd, 6);
        string sdnew_other = Convert.ToString(sd);
        string sdnew1_other = "";
        sdnew1_other = string.Format("{0:0.000000}", sd);
        return sdnew1_other;


    }


    protected void lbtn_daily_Click(object sender, EventArgs e)
    {
        //Server.Transfer("MapWithSatelliteView.aspx");
        Response.Redirect("Default.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        GoogleMapForASPNet1.GoogleMapObject.Points.Clear();
        

        Route_Ship();
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlTechmanager_SelectedIndexChanged(object sender, EventArgs e)
    {
        //change the ships list while changing the techmanager
        ddl_shipname.DataSource = Ship_Techmanager(ddlTechmanager.SelectedItem.Text.ToString());
        ddl_shipname.DataTextField = "Vessel_Name";
        ddl_shipname.DataValueField = "Vessel_Code";
        ddl_shipname.DataBind();
    }


    public DataSet Ship_Techmanager(string smanager)
    {
        _internalConnection_dpl = ConfigurationManager.ConnectionStrings["esmcon"].ConnectionString;
        DataSet ds = new DataSet();
        string query = "select Vessel_Code,Vessel_Name from Lib_Vessels where Tech_Manager='" + smanager + "' and Active_Status=1 order by Vessel_Name";


        SqlConnection con = new SqlConnection(_internalConnection_dpl);
        try
        {

            ds = SqlHelper.ExecuteDataset(con, CommandType.Text, query);
            return ds;

        }
        catch (Exception ex)
        {
            ds = null;
            return ds;
        }
        return ds;

    }
}
