using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Survey;
using System.Data;
using System.Configuration;

public partial class Surveys_Attachments : System.Web.UI.Page
{
    BLL_SURV_Survey objBLL = new BLL_SURV_Survey();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Vessel_ID = GetQueryString("vid");
            string Surv_Vessel_ID = GetQueryString("s_v_id");
            string Surv_Details_ID = GetQueryString("s_d_id");
            string OfficeID = GetQueryString("off_id");

            if (Vessel_ID != "" && Surv_Vessel_ID != "" && Surv_Details_ID != "" && OfficeID != "")
            {
                DataTable dt = objBLL.Get_SurvayAttachments(int.Parse(Vessel_ID), int.Parse(Surv_Details_ID));
                string FilePath  = "";
                string FileName  = "";
                string UploadPath = ConfigurationManager.AppSettings["UploadPath"].ToString();

                if (dt.Rows.Count == 1)
                {
                   
                    FilePath  = dt.Rows[0]["Attach_Name"].ToString();
                    FileName  = System.IO.Path.GetFileName(FilePath);
                    FilePath = dt.Rows[0]["Attach_Path"].ToString();
                    Response.Redirect("~/" + UploadPath + "/Survey/" + FilePath);
                }
                else if (dt.Rows.Count > 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        FilePath  = dr["Attach_Name"].ToString();
                        FileName  = System.IO.Path.GetFileName(FilePath);
                        FilePath = dr["Attach_Path"].ToString();
                        //Response.Write("<a href=" + "../" + UploadPath + "/Survey/" + FileName + ">" + FileName + "</a><br>");

                        HyperLink lnk = new HyperLink();
                        lnk.Text = FileName + "<br>";
                        lnk.NavigateUrl = "../" + UploadPath + "/Survey/" + FilePath;
                        lnk.Target = "_blank";
                        Panel1.Controls.Add(lnk);

                    }

                    
                }

            }
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