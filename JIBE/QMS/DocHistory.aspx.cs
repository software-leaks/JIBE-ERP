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

using SMS.Business.QMS;

public partial class DocHistory : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document(); 

    int FileID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        //FileID = Convert.ToInt32(Request.QueryString["FileID"]);
        FileID = Convert.ToInt32(Request.Params["FileID"]);

        DataSet dsGetLastestFileInfoByID = objQMS.GetLastestFileInfoByID(FileID);

        if (dsGetLastestFileInfoByID.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dsGetLastestFileInfoByID.Tables[0].Rows[0];
            lblFileName.Text = dr["LogFileID"].ToString();
            if (dr["Created_User"].ToString() != "")
                lblCreatedBy.Text = dr["Created_User"].ToString();
            else
                lblCreatedBy.Text = "Office";

            lblCreationDate.Text = ConvertDateToString(dr["Date_Of_Creatation"].ToString(), "dd-MMM-yy HH:mm");
            if (dr["Opp_User"].ToString() != "")
            {
                lblLastOperation.Text = dr["Operation_Type"].ToString() + " by " + dr["Opp_User"].ToString();
                lblLastOperationDt.Text = ConvertDateToString(dr["Operation_date"].ToString(), "dd-MMM-yy HH:mm");
            }
        }

        DataSet dsOperationDetailsByID = objQMS.GetOprationDetailsByID(FileID);

        //dvOperations.DataSource = dsOperationDetailsByID.Tables[0];
        //dvOperations.DataBind();

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
