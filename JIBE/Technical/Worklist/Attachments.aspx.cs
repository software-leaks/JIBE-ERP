using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.Technical;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Worklist_Attachments : System.Web.UI.Page
{
    BLL_Tec_Worklist objBLL = new BLL_Tec_Worklist();

    protected void Page_Load(object sender, EventArgs e)
    {

        string Vessel_ID = GetQueryString("vid");
        string Worklist_ID = GetQueryString("wlid");
        string WL_Office_ID = GetQueryString("wl_off_id");
        string AttID = GetQueryString("AttID");
        

        if (!IsPostBack)
        {

            if (Vessel_ID != "" && Worklist_ID != "" && WL_Office_ID != "")
            {

               
                
                DataTable dt = objBLL.Get_Worklist_Attachments(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(Worklist_ID), UDFLib.ConvertToInteger(WL_Office_ID), UDFLib.ConvertToInteger(Session["UserID"]));
                lblJob.Text = "Job Code: " + Worklist_ID + "  /  " + dt.Rows[0]["Vessel_Short_Name"].ToString();
                
                DataView dvImage = dt.DefaultView;
                dvImage.RowFilter = "Is_Image='1' ";

                ListView2.DataSource = dvImage;
                ListView2.DataBind();
                hidenTotalrecords.Value = dvImage.Count.ToString();
                HCurrentIndex.Value = "0";
                

                dt.DefaultView.RowFilter = "Is_Image='0'  ";
                rpt1.DataSource = dt.DefaultView;
                rpt1.DataBind();
            
            }




            //if (Vessel_ID != "" && Worklist_ID != "" && WL_Office_ID != "")
            //{
            //    DataTable dt = objBLL.Get_Worklist_Attachments(UDFLib.ConvertToInteger(Vessel_ID), UDFLib.ConvertToInteger(Worklist_ID), UDFLib.ConvertToInteger(WL_Office_ID), UDFLib.ConvertToInteger(Session["UserID"]));

            //    string FilePath = "";
            //    string FileName = "";
            //    rpt1.DataSource = dt;
            //    rpt1.DataBind();

            //    if (dt.Rows.Count > 0)
            //    {
            //        lblJob.Text = "Job Code: " + Worklist_ID + "/" + dt.Rows[0]["Vessel_Short_Name"].ToString();
            //        Session["WL_Attach_Title"] = "Job Code: " + Worklist_ID + "/" + dt.Rows[0]["Vessel_Short_Name"].ToString() + " - Attachments";


            //        if (AttID != "")
            //        {
            //            DataRow[] dr = dt.Select("attachment_id=" + AttID);
            //            if (dr.Length > 0)
            //            {
            //                FileName = dr[0]["Attach_Name"].ToString();
            //                FilePath = "../../Uploads/Technical/" + Path.GetFileName( dr[0]["Attach_Path"].ToString());

            //                if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
            //                {
            //                    Random r = new Random();
            //                    string ver = r.Next().ToString();
            //                    frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            FileName = dt.Rows[0]["Attach_Name"].ToString();
            //            FilePath = "../../Uploads/Technical/" + Path.GetFileName(dt.Rows[0]["Attach_Path"].ToString());

            //            if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
            //            {
            //                Random r = new Random();
            //                string ver = r.Next().ToString();
            //                frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
            //            }
            //        }
            //    }
            //}
        }

      //  this.Title = Session["WL_Attach_Title"].ToString();
    }

    protected void rpt1_ItemCommand(Object sender, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDocument")
            {
                string FileName = e.CommandArgument.ToString();
                string FilePath = "../../Uploads/Technical/" + Path.GetFileName(FileName);
                //frmContract.Attributes.Add("src", "../../Images/FileNotFound.png");
                ResponseHelper.Redirect(FilePath, "_blank",null);

                //if (System.IO.File.Exists(Server.MapPath(FilePath)) == true)
                //{
                //    Random r = new Random();
                //    string ver = r.Next().ToString();
                //    //frmContract.Attributes.Add("src", FilePath + "?ver=" + ver);
                //}
            }
        }
        catch { }
        //UpdatePanel_Frame.Update();
    }

    protected void rpt1_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            object obj = e.Item.DataItem;
            Image imgDocIcon = (Image)e.Item.FindControl("imgDocIcon");
            if (imgDocIcon != null)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;

                string icon = Path.GetExtension(dr.Row["Attach_Path"].ToString()).Replace(".", "");
                if (System.IO.File.Exists(Server.MapPath("~/images/DocTree/" + icon + ".gif")) == true)
                {
                    imgDocIcon.ImageUrl = "~/images/DocTree/" + icon + ".gif";
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

    public string getParams()
    {
        string Vessel_ID = GetQueryString("vid");
        string Worklist_ID = GetQueryString("wlid");
        string WL_Office_ID = GetQueryString("wl_off_id");
        string AttID = GetQueryString("AttID");

        return "vid=" + Vessel_ID + "&wlid=" + Worklist_ID + "&wl_off_id=" + WL_Office_ID;
    }
}