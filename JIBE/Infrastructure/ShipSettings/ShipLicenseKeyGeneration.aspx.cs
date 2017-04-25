using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

using SMS.Business.Infrastructure;
using SMS.Business.PURC;
using SMS.Business.Crew;
using SMS.Business.PMS;
using System.Text;
using SMS.Properties;
using System.IO;
using System.Security.Cryptography;

public partial class ShipLicenseKeyGeneration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["SORTDIRECTION"] = null;
            ViewState["SORTBYCOLOUMN"] = null;

            BindLicenseKey();
            BindVesselDDL();

            ucCustomPagerItems.PageSize = 20;
        }

    }

    public void BindVesselDDL()
    {
        try
        {

            BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

            DataTable dtVessel = objVsl.Get_VesselList(0, 0, Convert.ToInt32(Session["USERCOMPANYID"].ToString()), "", Convert.ToInt32(Session["USERCOMPANYID"].ToString()));
            DDLVessel.Items.Clear();
            DDLVessel.DataSource = dtVessel;
            DDLVessel.DataTextField = "Vessel_name";
            DDLVessel.DataValueField = "Vessel_id";
            DDLVessel.DataBind();
            ListItem li = new ListItem("--SELECT ALL--", "0");
            DDLVessel.Items.Insert(0, li);
        }
        catch (Exception ex)
        {

        }
    }

    public void BindLicenseKey()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());


        DataTable dt = BLL_Infra_LicenseKey.LicenseKeySearch(UDFLib.ConvertIntegerToNull(DDLVessel.SelectedValue)
            , UDFLib.ConvertIntegerToNull(optStatus.SelectedValue), txtfilter.Text != "" ? txtfilter.Text : null
            , sortbycoloumn, sortdirection , ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);

        

        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }


        if (dt.Rows.Count > 0)
        {
            gvLicenseKey.DataSource = dt;
            gvLicenseKey.DataBind();
        }
        else
        {
            gvLicenseKey.DataSource = dt;
            gvLicenseKey.DataBind();
        }

    }

    protected void gvLicenseKey_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }

    //protected void btnGenerateFile_Click(object sender, EventArgs e)
    //{

    //    if (txtAuthorizationCode.Text != "" && txtAuthorizationCode.Text.ToString().Length == 19)
    //    {
    //        string Authorizedkey = Convert.ToString(txtAuthorizationCode.Text.Trim());

    //        GenKeyLib.Class1 objGenKey = new GenKeyLib.Class1();

    //        string striskeyval = objGenKey.IsKeyValid();

    //        string strkeygenerate = objGenKey.strgenkey;
    //        string strPk = objGenKey.ProductKey(Authorizedkey);

    //        string Encryptvalue = DES_Encrypt_Decrypt.Encrypt(strPk);

    //        filereadwrite objFilereadwrite = new filereadwrite();
    //        objFilereadwrite.FileWrite(Encryptvalue);
 
    //        //int MailID = objBLLCrew.Send_CrewNotification(0, 0, 0, 0, sToEmailAddress, strEmailAddCc, "", strFormatSubject, sbEmailbody.ToString(), "", "MAIL", "", UDFLib.ConvertToInteger(Session["USERID"].ToString()), "DRAFT");
    //        //string uploadpath = @"\\server01\uploads\PmsJobs";

    //        ///* Make Entry for email attachment   */

    //        //BLL_Infra_Common.Insert_EmailAttachedFile(MailID, Attachfilename, uploadpath + @"\" + Attachfilename);



    //       //int val = BLL_FBM_Report.FBMCrewMailSave(Convert.ToInt32(Session["userid"].ToString()), subject, ToMail, "", sbEmailbody.ToString(), ref crewfbmidout);



    //        // MessageBox.Show("File has been created with the name Productkey.exe on the C drive", "File Location", MessageBoxButton.OK, MessageBoxImage.Exclamation);

    //    }
    //    else
    //    {
    //        //MessageBox.Show("Invalid Authorization Code", "InValid Code", MessageBoxButton.OK, MessageBoxImage.Exclamation);
    //    }

    //}
     

    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
   
        BindLicenseKey();
    }
    protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
    {
        DDLVessel.SelectedValue = "0";
        optStatus.SelectedValue = "0";

        BindLicenseKey();
    }

    protected void optRptType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLicenseKey();
    
    }
    protected void btnGenerateKey_Click(object sender, EventArgs e)
    {
        string Passphrase = "JiBEShip";

        foreach (GridViewRow row in gvLicenseKey.Rows)
        {
            if (((CheckBox)row.FindControl("chkStatus")).Checked == true && ((CheckBox)row.FindControl("chkStatus")).Enabled == true)
            {
                Label lblAutorizedKey = (Label)row.FindControl("lblAutorizedKey");
                string LicenseKey = DES_Encrypt_Decrypt.EncryptString(lblAutorizedKey.Text,Passphrase);

                int retval = BLL_Infra_LicenseKey.UpdateLicenseKey(Convert.ToInt32(((Label)row.FindControl("lblID")).Text)
                    , Convert.ToInt32(((Label)row.FindControl("lblVesselID")).Text), LicenseKey, Convert.ToInt32(Session["UserID"].ToString()));
            }
        }

        BindLicenseKey();

    }

}

public class filereadwrite
{
    public void FileWrite(string Encryptedstring)
    {
        
        //string filename = @"\\server01\Uploads\FBM\ProductKey.exe";
        string filename = @"C:\Development\"+System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString()+@"\Uploads\ProductKey.exe";

        try
        {

            FileStream fs = File.Create(filename);
            byte[] info = new UTF8Encoding(true).GetBytes(Encryptedstring);
            fs.Write(info, 0, info.Length);
            fs.Flush();
            fs.Close();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string FileRead(string path)
    {

        string strDecrypted = "";
        string strfiledata = "";

        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exists");

            }
            else
            {
                FileStream fs = File.OpenRead(path);
                byte[] arr = new byte[100];
                System.Text.UTF8Encoding data = new System.Text.UTF8Encoding(true);
                while (fs.Read(arr, 0, arr.Length) > 0)
                {
                    Console.WriteLine(data.GetString(arr));
                    strfiledata = data.GetString(arr);
                }
                string delim = "\0";
                char[] delimiter = delim.ToCharArray();
                string[] split = null;
                for (int x = 1; x <= strfiledata.Length; x++)
                {
                    split = strfiledata.Split(delimiter, x);

                }
                strDecrypted = split[0];

                // Console.WriteLine(strDecrypted);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strDecrypted;
    }
}

public class DES_Encrypt_Decrypt
{

    /// <summary>
    /// Encrypt a string.
    /// </summary>
    /// <param name="originalString">The original string.</param>
    /// <returns>The encrypted string.</returns>
    /// <exception cref="ArgumentNullException">This exception will be 
    /// thrown when the original string is null or empty.</exception>
    public static string EncryptString(string Message, string Passphrase)
    {
        byte[] Results;
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        try
        {
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below


            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object


            // Step 3. Setup the encoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]
            byte[] DataToEncrypt = UTF8.GetBytes(Message);

            // Step 5. Attempt to encrypt the string

            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

        }

        finally
        {
            // Clear the TripleDes and Hashprovider services of any sensitive information
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }
        return Convert.ToBase64String(Results);
        // Step 6. Return the encrypted string as a base64 encoded string
        //return Convert.ToBase64String(Results);
    }

    /// <summary>
    /// Decrypt a crypted string.
    /// </summary>
    /// <param name="cryptedString">The crypted string.</param>
    /// <returns>The decrypted string.</returns>
    /// <exception cref="ArgumentNullException">This exception will be thrown 
    /// when the crypted string is null or empty.</exception>
    public static string DecryptString(string Message, string Passphrase)
    {

        byte[] Results;
        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

        try
        {
            // Step 1. We hash the passphrase using MD5
            // We use the MD5 hash generator as the result is a 128 bit byte array
            // which is a valid length for the TripleDES encoder we use below


            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

            // Step 2. Create a new TripleDESCryptoServiceProvider object


            // Step 3. Setup the decoder
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Step 4. Convert the input string to a byte[]

            byte[] DataToDecrypt = Convert.FromBase64String(Message);


            // Step 5. Attempt to decrypt the string
            ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);

        }

        finally
        {
            // Clear the TripleDes and Hashprovider services of any sensitive information
            TDESAlgorithm.Clear();
            HashProvider.Clear();

        }

        // Step 6. Return the decrypted string in UTF8 format
        return UTF8.GetString(Results);
    }
}