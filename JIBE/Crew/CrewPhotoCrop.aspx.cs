using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

using SD = System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using SMS.Business.Crew;


public partial class CrewPhotoCrop : System.Web.UI.Page
{
    String path = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\CrewImages\\";
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdminBLL = new BLL_Crew_Admin();


    protected void Page_Load(object sender, EventArgs e)
    {
        int CrewID = GetCrewID();
        string sPhotoName = "";
        if (!IsPostBack)
        {
            if (CrewID > 0)
            {
                DataTable dt = objCrewAdminBLL.ExecuteQuery("select photourl from  CRW_LIB_Crew_Details where id=" + CrewID);
                sPhotoName = dt.Rows[0]["PhotoURL"].ToString();
                if (sPhotoName.Length > 0)
                {
                    Session["WorkingImage"] = sPhotoName;
                    pnlUpload.Visible = false;
                    pnlCrop.Visible = true;
                    imgCrop.ImageUrl = "../Uploads/CrewImages/" + Session["WorkingImage"].ToString();
                    imgPreview.ImageUrl = "../Uploads/CrewImages/" + Session["WorkingImage"].ToString();

                    pnlUpload.Visible = false;
                    pnlCrop.Visible = true;
                }
                else
                {
                    pnlUpload.Visible = true;
                    pnlCrop.Visible = true;
                }
            }
            else
            {
                sPhotoName = Convert.ToString(Session["ADDEDITNEWCREWID"]);
                Session["ADDEDITNEWCREWID"] = null;
                if (sPhotoName.Length > 0)
                {
                    Session["WorkingImage"] = sPhotoName;
                    pnlUpload.Visible = false;
                    pnlCrop.Visible = true;
                    imgCrop.ImageUrl = "../Uploads/CrewImages/" + Session["WorkingImage"].ToString();
                    imgPreview.ImageUrl = "../Uploads/CrewImages/" + Session["WorkingImage"].ToString();

                    pnlUpload.Visible = false;
                    pnlCrop.Visible = true;
                }
                else
                {
                    pnlUpload.Visible = true;
                    pnlCrop.Visible = true;
                }
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Boolean FileOK = false;
        Boolean FileSaved = false;
        String FileExtension = "";

        if (Upload.HasFile)
        {
            Session["WorkingImage"] = Upload.FileName;
            FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" };
            for (int i = 0; i < allowedExtensions.Length; i++)
            {
                if (FileExtension == allowedExtensions[i])
                {
                    FileOK = true;
                }
            }
        }

        if (FileOK)
        {
            try
            {
                Guid GUID = Guid.NewGuid();
                string newFileName = GUID.ToString() + "." + FileExtension;
                string SaveTo = path + newFileName;

                Upload.PostedFile.SaveAs(SaveTo);
                FileSaved = true;
            }
            catch (Exception ex)
            {
                lblError.Text = "File could not be uploaded." + ex.Message.ToString();
                lblError.Visible = true;
                FileSaved = false;
            }
        }

        else
        {
            lblError.Text = "Cannot accept files of this type.";
            lblError.Visible = true;
        }

        if (FileSaved)
        {
            pnlUpload.Visible = false;
            pnlCrop.Visible = true;
            imgCrop.ImageUrl = "../Uploads/CrewImages/" + Session["WorkingImage"].ToString();
            imgPreview.ImageUrl = "../Uploads/CrewImages/" + Session["WorkingImage"].ToString();
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("crewdetails.aspx?ID=" + GetCrewID());
    }
    protected void btnCancelCrop_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewPhotoCrop.aspx?ID=" + GetCrewID());
    }

    protected void btnCrop_Click(object sender, EventArgs e)
    {
        try
        {
            string ImageName = Session["WorkingImage"].ToString();
            if (W.Value != "")
            {
                int w = Convert.ToInt32(W.Value);

                int h = Convert.ToInt32(H.Value);

                int x = Convert.ToInt32(X.Value);

                int y = Convert.ToInt32(Y.Value);



                byte[] CropImage = Crop(path + ImageName, w, h, x, y);

                using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
                {

                    ms.Write(CropImage, 0, CropImage.Length);

                    using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                    {
                        Guid GUID = Guid.NewGuid();
                        string[] temp = ImageName.Split('.');

                        string newFileName = "CIMG_" + GUID.ToString() + "." + temp[temp.Length - 1];


                        string SaveTo = path + newFileName;

                        CroppedImage.Save(SaveTo, CroppedImage.RawFormat);

                        pnlCrop.Visible = false;

                        pnlCropped.Visible = true;

                        imgCropped.ImageUrl = "../Uploads/CrewImages/" + newFileName;


                        int result = 0;
                        if (GetCrewID() == 0)
                        {
                            Session["ADDEDITNEWFILENAME"] = newFileName;
                            result = 1;
                        }
                        else
                            result = objCrewBLL.UPDATE_CrewPhotoURL(GetCrewID(), newFileName);

                        if (result == 1)
                            HttpContext.Current.Response.Redirect(GetReturnURL());
                        else
                            HttpContext.Current.Response.Write("Error !! Try it again !!<br><a href='" + GetReturnURL() + "'>Back</a>");
                    }
                }
            }
            else
            {
                HttpContext.Current.Response.Redirect(GetReturnURL());
            }
        }
        catch { }
    }

    static byte[] Crop(string Img, int Width, int Height, int X, int Y)
    {

        try
        {

            using (SD.Image OriginalImage = SD.Image.FromFile(Img))
            {

                using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                {

                    bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);

                    using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                    {

                        Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                        Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                        Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);

                        MemoryStream ms = new MemoryStream();

                        bmp.Save(ms, OriginalImage.RawFormat);

                        return ms.GetBuffer();

                    }

                }

            }

        }

        catch (Exception Ex)
        {

            throw (Ex);

        }

    }

    private int GetCrewID()
    {
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
        {
            return int.Parse(Request.QueryString["ID"].ToString());
        }
        else
            return 0;
    }
    private string GetReturnURL()
    {
        try
        {
            if (Request.QueryString["ParentPageName"] != null && Request.QueryString["ParentPageName"].ToString() != "")
            {
                string ParentPageName = Request.QueryString["ParentPageName"].ToString();
                if (ParentPageName == "AddEditCrew.aspx")
                    return "AddEditCrew.aspx?ID=" + GetCrewID();
                else if (ParentPageName == "AddEditCrewNew.aspx")
                    return "AddEditCrewNew.aspx?crewid=" + GetCrewID();
                else
                    return "CrewDetails.aspx?ID=" + GetCrewID();
            }
            else
            {
                if (Request.QueryString["ReturnURL"] != null && Request.QueryString["ReturnURL"].ToString() != "")
                    return Request.QueryString["ReturnURL"].ToString();
                else
                    return "CrewDetails.aspx?ID=" + GetCrewID();
            }
        }
        catch
        {
            return "CrewDetails.aspx?ID=" + GetCrewID();
        }
    }


    //private string GetReturnURL()
    //{
    //    try
    //    {
    //        if (Request.QueryString["ReturnURL"] != null && Request.QueryString["ReturnURL"].ToString() != "")
    //            return Request.QueryString["ReturnURL"].ToString();
    //        else
    //            return "CrewDetails.aspx?ID=" + GetCrewID();
    //    }
    //    catch
    //    {
    //        return "CrewDetails.aspx?ID=" + GetCrewID();
    //    }
    //}
}