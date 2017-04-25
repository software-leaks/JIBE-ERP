using System;
using System.Data;
using System.Web.UI.WebControls;
using SMS.Business.QMS;
using SMS.Business.QMSDB;

public partial class DocHistory : System.Web.UI.Page
{
    BLL_QMS_Document objQMS = new BLL_QMS_Document(); 

    int FileID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        FileID = Convert.ToInt32(Request.QueryString["FileID"]);

        DataTable dsGetLastestFileInfoByID = BLL_QMSDB_Procedures.QMSDBProcedures_History(FileID);

        if (dsGetLastestFileInfoByID.Rows.Count > 0)
        {
            DataRow dr = dsGetLastestFileInfoByID.Rows[0];
            lblFileName.Text = dr["FILESNAME"].ToString();
            if (dr["CREATED_BY"].ToString() != "")
                lblCreatedBy.Text = dr["CREATED_BY"].ToString();
            else
                lblCreatedBy.Text = "Office";

            lblCreationDate.Text = ConvertDateToString(dr["CREATED_DATE"].ToString(), "dd-MMM-yy HH:mm");
            //if (dr["Opp_User"].ToString() != "")
            //{
            //    lblLastOperation.Text = dr["Operation_Type"].ToString() + " by " + dr["Opp_User"].ToString();
            //    lblLastOperationDt.Text = ConvertDateToString(dr["Operation_date"].ToString(), "dd-MMM-yy HH:mm");
            //}
        }

       // DataSet dsOperationDetailsByID = objQMS.GetOprationDetailsByID(FileID);

        //dvOperations.DataSource = dsOperationDetailsByID.Tables[0];
        //dvOperations.DataBind();

        dtrOppGrid.DataSource = dsGetLastestFileInfoByID;
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
        DateTime dt = Convert.ToDateTime(strDT);
        return dt.ToString(format);
    }

    protected void GetFile(object sender, CommandEventArgs e)
    {

        string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
        string FileID = commandArgs[0].ToString();
        string Version = commandArgs[1].ToString();
        string FilePath = commandArgs[2].ToString();
    }
}
