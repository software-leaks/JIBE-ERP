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


public partial class CrewPhotoCropNew : System.Web.UI.Page
{
    String path = HttpContext.Current.Request.PhysicalApplicationPath + "Uploads\\CrewImages\\";
    BLL_Crew_CrewDetails objCrewBLL = new BLL_Crew_CrewDetails();
    BLL_Crew_Admin objCrewAdminBLL = new BLL_Crew_Admin();
    public string HOST = "";
    //public int width = 300, height = 300;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //get URL
            HOST = Request.Url.AbsoluteUri.ToString().Substring(0, Request.Url.AbsoluteUri.ToString().ToLower().IndexOf("crew/crewphotocropnew.aspx"));

            pnlUpload.Visible = true;
            pnlCrop.Visible = false;
        }
        catch (Exception ex)
        {
            UDFLib.WriteExceptionLog(ex);
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            Boolean FileOK = false;
            Boolean FileSaved = false;
            String FileExtension = "";
            string newFileName = "";
            String[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" };

            if (Upload.HasFile)
            {
                FileExtension = Path.GetExtension(Upload.FileName).ToLower();
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (FileExtension == allowedExtensions[i])
                        FileOK = true;
                }
                if (!FileOK)
                {
                    lblError.Text = "Cannot accept files of this type.";
                    lblError.Visible = true;
                    return;
                }
                Upload.PostedFile.SaveAs(path + Upload.FileName);
                System.Drawing.Image img = System.Drawing.Image.FromFile(path + Upload.FileName);

                FileOK = true;
                img.Dispose();
                File.Delete(path + Upload.FileName);


                if (FileOK)
                {

                    Session["WorkingImage"] = Upload.FileName;
                    FileExtension = Path.GetExtension(Session["WorkingImage"].ToString()).ToLower();
                    for (int i = 0; i < allowedExtensions.Length; i++)
                    {
                        if (FileExtension == allowedExtensions[i])
                            FileOK = true;
                    }
                    if (FileOK)
                    {
                        try
                        {
                            Guid GUID = Guid.NewGuid();
                            newFileName = GUID.ToString() + FileExtension;
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
                }

                if (FileSaved)
                {
                    flsetpnlUpload.Visible = pnlUpload.Visible = false;
                    pnlCrop.Visible = true;
                    Session["WorkingImage"] = newFileName;
                    imgCrop.ImageUrl = HOST + "Uploads/CrewImages/" + newFileName;
                    imgPreview.ImageUrl = HOST + "Uploads/CrewImages/" + newFileName;
                }
            }
            else
            {
                lblError.Text = "Select file to upload";
                lblError.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = UDFLib.GetException("SystemError/GeneralMessage");
            lblError.Visible = true;
            UDFLib.WriteExceptionLog(ex);
        }
    }

    protected void btnCancelCrop_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewPhotoCropNew.aspx?ID=" + GetCrewID());
    }

    protected void btnCrop_Click(object sender, EventArgs e)
    {
        try
        {
            string ImageName = Session["WorkingImage"].ToString();
            if (W.Value != "0" && H.Value != "0" && W.Value != "" && H.Value != "")
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
                        int result = 0;
                        if (GetCrewID() == 0)
                        {
                            Session["ADDEDITNEWFILENAME"] = newFileName;
                            File.Delete(path + ImageName);
                            result = 1;
                        }
                        else
                        {
                            File.Delete(path + ImageName);
                            result = objCrewBLL.UPDATE_CrewPhotoURL(GetCrewID(), newFileName);
                        }

                        if (result == 1)
                        {
                            if (GetPageUrl() != "")
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "RedirectToCrewDetails('" + HOST + "Uploads/CrewImages/" + newFileName + "','" + newFileName + "');", true);
                            else
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "RedirectToAddEditPage('" + HOST + "Uploads/CrewImages/" + newFileName + "','" + newFileName + "');", true);
                        }
                        else
                        {
                            Session["WorkingImage"] = null;
                            Response.Redirect("CrewPhotoCropNew.aspx?ID=" + GetCrewID());
                        }
                    }
                }
            }
            else
            {
                Guid GUID = Guid.NewGuid();
                string[] temp = ImageName.Split('.');
                string newFileName = "CIMG_" + GUID.ToString() + "." + temp[temp.Length - 1];
                System.IO.File.Move(path + ImageName, path + newFileName);
                if (File.Exists(path + newFileName))
                {
                    pnlCrop.Visible = false;

                    pnlCropped.Visible = true;
                    int result = 0;
                    if (GetCrewID() == 0)
                    {
                        Session["ADDEDITNEWFILENAME"] = newFileName;
                        File.Delete(path + ImageName);
                        result = 1;
                    }
                    else
                    {
                        File.Delete(path + ImageName);
                        result = objCrewBLL.UPDATE_CrewPhotoURL(GetCrewID(), newFileName);
                    }

                    if (GetPageUrl() != "")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "RedirectToCrewDetails('" + HOST + "Uploads/CrewImages/" + newFileName + "','" + newFileName + "');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script1", "RedirectToAddEditPage('" + HOST + "Uploads/CrewImages/" + newFileName + "','" + newFileName + "');", true);
                }
                else
                {
                    lblError.Text = UDFLib.GetException("SystemError/GeneralMessage");
                    lblError.Visible = true;
                }
            }
        }
        catch (Exception ex) { UDFLib.WriteExceptionLog(ex); }
    }

    static byte[] Crop(string Img, int Width, int Height, int X, int Y)
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

    private int GetCrewID()
    {
        if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
        {
            return int.Parse(Request.QueryString["ID"].ToString());
        }
        else
            return 0;
    }

    private string GetPageUrl()
    {
        if (Request.QueryString["Page"] != null && Request.QueryString["Page"].ToString() != "")
        {
            return Convert.ToString(Request.QueryString["Page"].ToString());
        }
        else
            return "";
    }
}