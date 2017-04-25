using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SMS.Business.QMS;
using System.IO;

public partial class QMS_SCM_SCM_Drill_Images_Details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDrillAttachments();
        }
    }

    private void BindDrillAttachments()
    {
        DataTable dt = BLL_SCM_Report.SCMGetDrillAttachment(Convert.ToInt32(Request.QueryString["DRILLID"].ToString()), Convert.ToInt32(Request.QueryString["Vessel_ID"].ToString()));

        if (dt.Rows.Count > 0)
        {
            //Bind the header
            txtVessel.Text = dt.Rows[0]["Vessel_Name"].ToString();
            txtDrillDate.Text = dt.Rows[0]["DRILL_DATE"].ToString();
            txtDrillType.Text = dt.Rows[0]["DRILLTYPE"].ToString();
            txtImproSugg.Text = dt.Rows[0]["IMPROVEMENTSSUGGESTED"].ToString();

            //Bind the Photos
            rptDrillImages.DataSource = dt;
            rptDrillImages.DataBind();

            string FileName = dt.Rows[0]["PhotoUrl"].ToString();
            string FilePath = "../../Uploads/SCM/" + Path.GetFileName(dt.Rows[0]["PhotoUrl"].ToString());

            if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
            {
                Random r = new Random();
                string ver = r.Next().ToString();
                frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
            }
        }
    }


    protected void rptDrillImages_ItemCommand(Object sender, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDocument")
            {
                string FileName = e.CommandArgument.ToString();
                string FilePath = "../../Uploads/SCM/" + Path.GetFileName(FileName);
                frmContract.Attributes.Add("src", "../../Images/FileNotFound.png");

                if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
                {
                    Random r = new Random();
                    string ver = r.Next().ToString();
                    frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
                }
            }
        }
        catch { }
         
    }

    protected void rptDrillImages_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            object obj = e.Item.DataItem;
            Image imgDocIcon = (Image)e.Item.FindControl("imgDocIcon");
            if (imgDocIcon != null)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                string icon = Path.GetExtension(dr.Row["PhotoUrl"].ToString()).Replace(".", "");
                if (System.IO.File.Exists(Server.MapPath("~/images/DocTree/" + icon + ".gif")) == true)
                {
                    imgDocIcon.ImageUrl = "~/images/DocTree/" + icon + ".gif";
                }
            }
        }
    }
}