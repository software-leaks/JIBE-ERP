using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Data.Infrastructure;
using System.Xml.Serialization;
using System.Web.Services.Protocols;

/// <summary>
/// Summary description for JibeWebService
/// </summary>
[WebService(Namespace="JibeWebServiceNS")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
 [System.Web.Script.Services.ScriptService]
public partial class JibeWebService : System.Web.Services.WebService
{
    DAL_Infra_UserCredentials objDal = new DAL_Infra_UserCredentials();
    public JibeWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

       
    [WebMethod]
    [SoapDocumentMethod]
    public User UserCredentials(string userid, string password)
    {
        User Usr = new User();
        Usr.UserID = 0;
        DataSet dsuser = objDal.Get_UserCredentials_DL(userid,DMS.DES_Encrypt_Decrypt.Encrypt(password));
        
        if (dsuser.Tables["Login"].Rows.Count > 0)
        {
            Usr.UserID = Convert.ToInt32(dsuser.Tables["Login"].Rows[0]["UserId"].ToString());
        }
        return Usr;
    }

   
}
public class User
{
    // Both types of attributes can be applied. Depending on which type
    // the method used, either one will affect the call.
  
    [XmlElement(ElementName = "UserID")]
    public int UserID;
}

