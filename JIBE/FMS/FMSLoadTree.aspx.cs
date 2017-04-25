using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SMS.Business.FMS;
using System.IO;

public partial class FMS_LoadTree : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document();

    protected void Page_Load(object sender, EventArgs e)
    {
        string sTree = "";
        string dir;
        int pid = 0;
        int showArchivedForms;

       //To visible or not archived files
        if (Session["ShowArchivedForms"] == null)
        {
            showArchivedForms = 0;
        }
        else
        {
            showArchivedForms = Convert.ToInt32(Session["ShowArchivedForms"]);
        }


        if (Request.Form["dir"] == null || Request.Form["dir"].Length <= 0 || Request.Form["dir"] == "DOCUMENTS")
            dir = "FMSL/";
        else
            dir = Server.UrlDecode(Request.Form["dir"]);

        pid = UDFLib.ConvertToInteger(Request.Form["pid"]);

        String originalPath = Request.UrlReferrer.AbsolutePath.ToString();
        String parentDirectory = UDFLib.GetPageURL(originalPath);

        DataSet ds = objFMS.getFolderAsync(UDFLib.ConvertToInteger(Session["Userid"]), pid, parentDirectory, showArchivedForms);

        DataTable dtDir = ds.Tables[0];
        DataTable dtFiles = ds.Tables[1];

        sTree = "<ul class=\"jqueryFileTree\" style=\"display: none;\">\n";

        foreach (DataRow dir_child in dtDir.Rows)
            sTree += "\t<li class=\"directory collapsed\"><a href=\"#\" id=" + dir_child["ID"] + " rel=\"" + dir + dir_child["LogFileID"] + "/\">" + dir_child["LogFileID"] + "</a></li>\n";


        foreach (DataRow fi in dtFiles.Rows)
        {
            string ext = "";
            string filename = fi["LogFileID"].ToString();
            int res = objFMS.FMS_Get_FormScheduleStatus(Convert.ToInt32(fi["ID"]));

            if (Path.GetExtension(filename).Length > 1)
                ext = Path.GetExtension(filename).Substring(1).ToLower();

            if (fi["Active_Status"].ToString() == "0")
            {
                ext = "del";
            }
            string DocVersion = "0";

            //Get details of files in tree.
            DataSet dsGetLastestFileInfoByID = objFMS.GetLastestFileInfoByID(UDFLib.ConvertToInteger(fi["ID"].ToString()), showArchivedForms);
            string FileInfo = string.Empty;

            if (dsGetLastestFileInfoByID.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsGetLastestFileInfoByID.Tables[0].Rows[0];
                string FileName = "<td><b>Form Name:</b></td><td> " + dr["LogFileID"].ToString() + "</td>";
                string FormType = "<td><b>Form Type:</b></td><td> " + dr["Form_Type"].ToString() + "</td>";
                string Department = "<td><b> Department:</b> </td><td> " + dr["Department"].ToString() + "</td>";
                string SchStatus = string.Empty;
                string CreatedBy = string.Empty;

                if (dr["Created_User"].ToString() != "")
                    CreatedBy = "<td><b>Created By:</b></td><td> " + dr["Created_User"].ToString() + "</td>";
                else
                    CreatedBy = "<td><b>Created By:</b></td><td> Office</td>";

                string CreationDate = "<td><b>Creation Date:</b></td><td> " + dr["Date_Of_Creatation"].ToString() + "</td>";
                string LastOperation = "<td><b>Last action:</b></td><td> ";
                string LastOperationDate = "<td><b>Last action date:</b></td><td> ";

                if (dr["Opp_User"].ToString() != "")
                {
                    LastOperation = "<td><b>Last action:</b></td><td> " + dr["Operation_Type"].ToString() + " by " + dr["Opp_User"].ToString() + "</td>";
                    LastOperationDate = "<td><b>Last action date:</b></td><td> " + dr["Operation_date"].ToString() + "</td>";
                }

                string LatestVersion = string.Empty;

                if (dr["Version"].ToString() != "")
                {
                    LatestVersion = "<td><b>Version:</b></td><td> " + dr["Version"].ToString() + "</td>";
                }

                string Remark = "<td><b>Remarks:</b></td><td colspan=3> " + fi["Remark"].ToString() + "</td>";

                if (res == 1)
                {
                    SchStatus = "<td><b>Schedule Status:</b></td><td colspan=3> Scheduled </td>";
                }
                else if (res == 2)
                {
                    SchStatus = "<td><b>Schedule Status:</b></td><td colspan=3> Assign to Vessel</td>";
                }
                else if (res == 3)
                {
                    SchStatus = "<td><b>Schedule Status:</b></td><td colspan=3>Not Scheduled</td>";
                }
                FileInfo = "<table><tr>" + FileName + " " + LatestVersion + "</tr><tr>" + FormType + " " + Department + "</tr><tr>" + CreatedBy + " " + LastOperation + "</tr><tr>" + CreationDate + " " + LastOperationDate + "</tr><tr>" + Remark + "</tr><tr>" + SchStatus + "</tr></table>";
            }

            if (res == 1)
            {
                sTree += "\t<li class=\"file ext_" + ext + "\"><a  onMouseOver= ' js_ShowToolTip(\"" + FileInfo + "\",event,this);'  href=\"#\" id=" + fi["ID"] + " rel=\"" + dir + filename + "\"    ><img src='../Images/Clock-002.png'  height='12px'/>" + filename + "</a></li>\n";
            }
            else if (res == 2)
            {
                sTree += "\t<li class=\"file ext_" + ext + "\"><a  onMouseOver= ' js_ShowToolTip(\"" + FileInfo + "\",event,this);'  href=\"#\" id=" + fi["ID"] + " rel=\"" + dir + filename + "\"    ><img src='../Images/Check_List_001.png' height='12px' />" + filename + "</a></li>\n";
            }
            else if (res == 3)
            {
                sTree += "\t<li class=\"file ext_" + ext + "\"><a  onMouseOver= ' js_ShowToolTip(\"" + FileInfo + "\",event,this);'  href=\"#\" id=" + fi["ID"] + " rel=\"" + dir + filename + "\"    ><img src='../Images/Clock-001.png' height='12px' />" + filename + "</a></li>\n";
            }
            else
            {
                sTree += "\t<li class=\"file ext_" + ext + "\"><a  onMouseOver= ' js_ShowToolTip(\"" + FileInfo + "\",event,this);'  href=\"#\" id=" + fi["ID"] + " rel=\"" + dir + filename + "\"    >" + filename + "</a></li>\n";
            }


        }
        sTree += "</ul>";
        tree.Text = sTree;

    }




}