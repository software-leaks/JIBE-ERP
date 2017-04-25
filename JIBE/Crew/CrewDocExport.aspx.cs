using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Crew;
using SMS.Business.Infrastructure;
using System.Data;
using System.IO;
using SMS.Business.LMS;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Net;
using System.Text;




public partial class Crew_CrewDocExport : System.Web.UI.Page
{
    BLL_Crew_CrewDetails objCrew = new BLL_Crew_CrewDetails();
    BLL_Infra_UploadFileSize objUploadFilesize = new BLL_Infra_UploadFileSize();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (getQueryString("CrewID") == null)
            Response.Redirect("CrewList.aspx");

        if (!IsPostBack)
        {
            try
            {
                int CrewID = int.Parse(getQueryString("CrewID"));
                
                BindData();

                DataTable dt = objCrew.Get_CrewPersonalDetailsByID(CrewID);
                if (dt.Rows.Count > 0)
                {
                    //filePathexcel(dt);
                    //Get();
                    lblCrewName.Text = dt.Rows[0]["staff_fullname"].ToString();
                    lblRank.Text = dt.Rows[0]["rank_name"].ToString();
                    hdnCrewRank.Value = dt.Rows[0]["rank_name"].ToString();
                    hdnStaffCode.Value = dt.Rows[0]["Staff_Code"].ToString();
                    hdnFirstName.Value = dt.Rows[0]["Staff_Name"].ToString();

                    string Load_Div = String.Format("loadDiv()");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", Load_Div, true);
                }
            }
            catch
            {
            }

        }
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            grdCrewDoc.Columns[grdCrewDoc.Columns.Count - 1].Visible = false;
        }
        if (objUA.Edit == 0)
        {
        }
        if (objUA.Delete == 0)
        {
        }

        if (objUA.Approve == 0)
        {


        }

        //-- MANNING OFFICE LOGIN --
        if (Session["UTYPE"].ToString() == "MANNING AGENT")
        {

        }
        //-- VESSEL MANAGER -- //
        else if (Session["UTYPE"].ToString() == "VESSEL MANAGER")
        {

        }
        else//--- CREW TEAM LOGIN--
        {

        }
    }

    private string getQueryString(string QueryField)
    {
        try
        {
            if (Request.QueryString[QueryField] != null && Request.QueryString[QueryField].ToString() != "")
            {
                return Request.QueryString[QueryField].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    protected void grdCrewDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            string DocFileName = DataBinder.Eval(e.Row.DataItem, "DocFileName").ToString();
            //string FileName = DataBinder.Eval(e.Row.DataItem, "DocName").ToString();
            string FileName = DataBinder.Eval(e.Row.DataItem, "DocTypeName").ToString();
            string Ext = DataBinder.Eval(e.Row.DataItem, "DocFileExt").ToString();
            Label img = (Label)e.Row.FindControl("lblFilePath");
          
            img.Text = Server.MapPath("~") + "\\Uploads\\CrewDocuments\\" + DocFileName + ";" + FileName + ";" + Ext;

        }
    }

    protected void BindData()
    {
        int CrewID = int.Parse(getQueryString("CrewID"));

        string SearchText = txtSearchText.Text;

        DataTable dtDocs = objCrew.Get_Crew_Documents(CrewID, SearchText);

        grdCrewDoc.DataSource = dtDocs;
        grdCrewDoc.DataBind();
    }   

    protected void txtSearchText_TextChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void DownloadFiles(object sender, EventArgs e)
    {
        try
        {
            System.IO.StreamWriter excelDoc;
            string hdnRank = hdnCrewRank.Value.ToString();
            hdnRank = hdnRank.Replace("/", "_");
            hdnRank = hdnRank.Replace(" ", "_");
            excelDoc = new System.IO.StreamWriter(System.IO.Path.Combine(Server.MapPath("~") + @"\" + "Uploads/Temp/", "CrewDetails_" + hdnRank + "_" + hdnFirstName.Value.ToString().Replace(" ", "_") + "_" + hdnStaffCode.Value.ToString() + ".xls"));
           
            string data = hdnCrewDetails.Value.ToString();
            data = data.Replace("\"", "'");
            excelDoc.Write(data);
            excelDoc.Close();
            excelDoc.Dispose();

            string zipFile = "";
            List<string> zips = new List<string>();
            zips.Add("CrewDetails_" + hdnRank + "_" + hdnFirstName.Value.ToString().Replace(" ", "_") + "_" + hdnStaffCode.Value.ToString() + ".xls");
            foreach (GridViewRow row in grdCrewDoc.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string[] args = ((row.FindControl("lblFilePath") as Label).Text).Split(';');
                    string filePath = args[0];
                    string DestPath = Server.MapPath("~") + "\\Uploads\\Temp\\" + UDFLib.ReplaceSpecialCharacter(args[1]) + args[2];
                    File.Copy(filePath, DestPath, true);
                    zips.Add(UDFLib.ReplaceSpecialCharacter(args[1]) + args[2]);
                }
            }

            if (zips.Count > 0)
            {

                zipFile = BLL_LMS_Training.RAR((Server.MapPath("~") + @"\" + "Uploads\\Temp\\"), zips);

                string DownloadFileName = hdnStaffCode.Value.ToString() + "_" + hdnFirstName.Value.ToString().Replace(" ", "_") + "_" + hdnRank + "_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m" + DateTime.Now.Second + "s" + ".rar";
                if (File.Exists(Server.MapPath("~") + @"\" + "Uploads\\Temp\\" + zipFile))
                {
                    File.Move((Server.MapPath("~") + @"\" + "Uploads\\Temp\\" + zipFile), Server.MapPath("~") + @"\" + "Uploads\\Temp\\" + DownloadFileName);

                    ResponseHelper.Redirect("~/Uploads/Temp/" + DownloadFileName, "blank", "");


                }

            }
        }
        catch
        { }

    }

    protected void chk_ALLDocs_CheckedChanged(object sender, EventArgs e)
    {
        Boolean Check = chkAllDocs.Checked;
        foreach (GridViewRow row in grdCrewDoc.Rows)
        {
            (row.FindControl("chkSelect") as CheckBox).Checked = Check;
        }
    }

    protected void filePathexcel( DataTable dt)
    {
        try
        {
            string[] HeaderCaptions = new string[] {"Surname", "First Name", "Rank",  " Nationality", "Date of Birth", "Place of Birth", "Marital Status", "Nearest Itnl. Airport", "Telephone", "Mobile", 
                                                    "Fax", "E-mail",  
                                                     "Passport No", "Place of Issue", "Issue Date", "Expiry Date", "Seaman Bk. No",
                                                        "Place of Issue", "Issue Date", "Expiry Date"};
            string[] DataColumnsName = new string[] {"Staff_Surname", "Staff_Name", "Rank_Name", "Country_Name", "Staff_Birth_Date", "Staff_Born_Place", "MaritalStatus", "NearestAirport",  "Telephone", "Mobile", 
                                                    "Fax", "EMail", "Passport_Number", "Passport_PlaceOf_Issue", "Passport_Issue_Date", "Passport_Expiry_Date", "Seaman_Book_Number",
                                                        "Seaman_Book_PlaceOf_Issue", "Seaman_Book_Issue_date", "Seaman_Book_Expiry_Date"};

            System.IO.StreamWriter excelDoc;
            excelDoc = new System.IO.StreamWriter(System.IO.Path.Combine(Server.MapPath("~") + @"\" + "Uploads/Temp/", "CrewDetails_" + dt.Rows[0]["Rank_Short_Name"].ToString() + "_" + dt.Rows[0]["Staff_Name"].ToString() + "_" + dt.Rows[0]["Staff_Code"].ToString()) + ".xls");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

           
            sb.Append("<TABLE BORDER='0' CELLPADDING='2' CELLSPACING='0' width='100%'>");
            sb.Append("<tr>");
            sb.Append("<td style='text-align:center;background-color: #C8C8C8' colspan='11'><h3>Crew Details : " + dt.Rows[0]["Rank_Short_Name"].ToString() + " " + dt.Rows[0]["Staff_FullName"].ToString() + " " + dt.Rows[0]["Staff_Code"].ToString() + "</h3></td>");
            sb.Append("</tr>");
            sb.Append("</TABLE>");
            sb.Append("<br />");


            sb.Append("<TABLE BORDER='1' CELLPADDING='2' CELLSPACING='2' width='100%'>");
            int k = 0;
            for (int j = 0; j < 5; j++)
            {
                sb.Append("<tr >");
                for (int i = 0; i < 4; i++)
                {
                    sb.Append("<td width='25%' style='background-color: #99CCFF;' align='right'>");
                    sb.Append("<b>" + HeaderCaptions[k] + "</b>");
                    sb.Append("</td>");
                    sb.Append("<td  align='left' width='25%'>");
                    sb.Append(dt.Rows[0][DataColumnsName[k]].ToString());
                    sb.Append("</td>");
                    k = k + 1;
                }
                sb.Append("</tr>");
            }
            sb.Append("<tr >");
            sb.Append("<td width='25%' style='background-color: #F2F2F2;' align='right'>");
            sb.Append("<b>Address :</b>");
            sb.Append("</td>");
            sb.Append("<td width='25%' align='left'>");
            sb.Append(dt.Rows[0]["Address"].ToString());
            sb.Append("</td>");
            sb.Append("<td width='25%' style='background-color: #F2F2F2;' align='right'>");
            sb.Append("<b>Available From :</b>");
            sb.Append("</td>");
            sb.Append("<td width='25%' align='left'>");
            sb.Append(dt.Rows[0]["Available_From_Date"].ToString());
            sb.Append("</td>");
            sb.Append("</tr>");

            sb.Append("</TABLE>");
           

            excelDoc.Write(sb.ToString());
            excelDoc.Close();
            excelDoc.Dispose();


     
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
           
        }
       
    }
    
    public void Get()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:5517/JIBE/Crew/CrewProfile.aspx?P=1&ID=4781");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = null;

            if (response.CharacterSet == null)
            {
                readStream = new StreamReader(receiveStream);
            }
            else
            {
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
            }

            System.IO.StreamWriter excelDoc;
            excelDoc = new System.IO.StreamWriter(System.IO.Path.Combine(Server.MapPath("~") + @"\" + "Uploads/Temp/", "CrewDetails.xls"));//_" + dt.Rows[0]["Rank_Short_Name"].ToString() + "_" + dt.Rows[0]["Staff_Name"].ToString() + "_" + dt.Rows[0]["Staff_Code"].ToString()) + ".xls");
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string data = readStream.ReadToEnd();
            //string htmlContent = new System.Net.WebClient().DownloadString("CrewProfile.aspx?P=1&ID=4781");
            excelDoc.Write(data);
            excelDoc.Close();
            excelDoc.Dispose();
            

            response.Close();
            readStream.Close();
        }

    }
    
}