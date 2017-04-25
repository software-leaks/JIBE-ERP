using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Data.Infrastructure;
using SMS.Business.CP;
using System.Collections.Generic;
using System.Text;
using System.IO;



public partial class CPService
{
    [WebMethod]
    public string UploadFile(byte[] f, string fileName)
    {

        try
        {
            MemoryStream ms = new MemoryStream(f);
            FileStream fs = new FileStream
                (Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["Info_Path"].ToString()) +
                fileName, FileMode.Create);

            ms.WriteTo(fs);
            ms.Close();
            fs.Close();
            fs.Dispose();
            return "OK";
        }
        catch (Exception ex)
        {
            return ex.Message.ToString();
        }
    }

}
