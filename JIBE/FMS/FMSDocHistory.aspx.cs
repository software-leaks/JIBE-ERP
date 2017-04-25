using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using SMS.Business.FMS;

public partial class DocHistory : System.Web.UI.Page
{
    BLL_FMS_Document objFMS = new BLL_FMS_Document(); 

    int FileID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        FileID = Convert.ToInt32(Request.QueryString["FileID"]);

      
        DataSet dsOperationDetailsByID = objFMS.GetOprationDetailsByID(FileID);

        dtrOppGrid.DataSource = dsOperationDetailsByID.Tables[0];
        dtrOppGrid.DataBind();
    }

    /// <summary>
    /// this a method for the convert the date in givien format by the user.
    /// </summary>
    /// <param name="strDT"></param>
    /// <param name="format"></param>
    /// <returns></returns>
    public string ConvertDateToString(string strDT, string format)
    {
        if (strDT != "")
        {
            DateTime dt = Convert.ToDateTime(strDT);
            return dt.ToString(format);
        }
        return "";
    }

    protected void GetFile(object sender, CommandEventArgs e)
    {

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string FileID = commandArgs[0].ToString();
        string Version = commandArgs[1].ToString();
        string FilePath = commandArgs[2].ToString();
    }
}
