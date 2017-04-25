using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using SMS.Business.Infrastructure;
using SMS.Business.ODM;
using SMS.Business.VM;
using System.Data;
using System.Web.UI.HtmlControls;
using SMS.Properties;

public partial class ODM_ODM_Detail : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    private long lCurrentRecord = 0;
    private long lRecordsPerRow = 200;
    private int GroupId = 0;
    bool IsAllPorts = false;
    bool IsallVessels = false;
    bool IsAllUsers = false;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_ODM objODM = new BLL_ODM();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null &&  Request.QueryString["ID"].ToString() != "0")
            {

                GroupId = Convert.ToInt32(Request.QueryString["ID"]);
                Session["GroupId"] = GroupId;
                //Session["VesselName"] = "NoName";
                DataTable dt = objODM.Get_ODM_Vessels(GroupId);
                if (dt.Rows.Count > 0)
                {
                    lblCreatedBy.Text = "Created By: <b>" + dt.Rows[0]["CreatedUser"].ToString() + "</b>  On: " + dt.Rows[0]["Created_Date"].ToString();

                    lblDep.Text = dt.Rows[0]["Department_Name"].ToString();
                    lblVesselName.Text = dt.Rows[0]["Vessels"].ToString();
                    lblVesselName.Visible = true;
                    lblDescription.Text = dt.Rows[0]["MSG_TEXT"].ToString();
                    lblSubject.Text = dt.Rows[0]["ODM_SUBJECT"].ToString();

                    BindAttachment();
                }
    
            }
        }
    }

    protected void BindAttachment()
    {
        if (Session["GroupId"] != null)
        {
            int? ID = UDFLib.ConvertIntegerToNull(Session["GroupId"]);

            DataTable dt = objODM.GET_ODM_Attachments(ID);

            gvAttachment.DataSource = dt;
            gvAttachment.DataBind();
        }

    }
    public void SetSession()
    {
        try
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "0")
            {
                Session["GroupId"] = Request.QueryString["ID"];
                //Session["VesselName"] = Request.QueryString["VName"];
            }
        }
        catch { }

    }




  

}