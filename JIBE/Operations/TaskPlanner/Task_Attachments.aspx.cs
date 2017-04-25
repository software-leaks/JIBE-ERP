using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;
using System.Configuration;

public partial class TaskPlanner_Task_Attachments : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Vessel_ID = GetQueryString("vid");
            string Worklist_ID = GetQueryString("wlid");
            string WL_Office_ID = GetQueryString("wl_off_id");

            if (Vessel_ID != "" && Worklist_ID != "" && WL_Office_ID!= "")
            {
                DataTable dt = objBLL.Get_Worklist_Attachments(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(Worklist_ID), UDFLib.ConvertToInteger(WL_Office_ID), UDFLib.ConvertToInteger(Session["UserID"]));
                
                string FilePath  = "";
                string FileName  = "";
                

                if (dt.Rows.Count == 1)
                {
                    FilePath  = dt.Rows[0]["Attach_Path"].ToString();
                    FileName = dt.Rows[0]["Attach_Name"].ToString();

                    Response.Redirect("~/Uploads/Technical/" + FilePath);
                }
                else if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FilePath = dr["Attach_Path"].ToString();
                        FileName = dr["Attach_Name"].ToString();

                        HyperLink lnk = new HyperLink();
                        lnk.Text = FileName + "<br>";
                        lnk.NavigateUrl = "../../Uploads/Technical/" + FilePath;
                        lnk.Target = "_blank";
                        Panel1.Controls.Add(lnk);
                    }
                    
                }

            }
        }
    }

    private void Load_Attachments(int VESSEL_ID, int WORKLIST_ID, int WL_OFFICE_ID, int UserID)
    {
        DataTable dt = objBLL.Get_Worklist_Attachments(VESSEL_ID, WORKLIST_ID, WL_OFFICE_ID, UserID);

        gvAttachments.DataSource = dt;
        gvAttachments.DataBind();

    }

    protected void gvAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AttachmentPath = DataBinder.Eval(e.Row.DataItem, "Attach_Name").ToString();

            string AttachmentName = System.IO.Path.GetFileName(AttachmentPath);

            HyperLink lnk = (HyperLink)(e.Row.FindControl("lblAttach_Name"));
            if (lnk != null)
            {
                lnk.Text = AttachmentName;
                lnk.NavigateUrl = "~/Uploads/Technical/" + AttachmentName;
            }
        }
    }

    private int GetSessionUserID()
    {
        try
        {
            if (Session["USERID"] != null)
                return int.Parse(Session["USERID"].ToString());
            else
                return 0;
        }
        catch
        {
            return 0;
        }
    }
    public string GetQueryString(string Query)
    {
        try
        {
            if (Request.QueryString[Query] != null)
            {
                return Request.QueryString[Query].ToString();
            }
            else
                return "";
        }
        catch { return ""; }
    }
}