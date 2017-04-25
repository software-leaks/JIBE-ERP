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

public partial class ODM_ODM_Entry : System.Web.UI.Page
{
    public string OperationMode = "";
    public Boolean uaEditFlag = false;
    public Boolean uaDeleteFlage = false;
    public int GroupId = 0;
    bool IsallVessels = false;
    public UserAccess objUA = new UserAccess();
    BLL_Infra_VesselLib objBLL = new BLL_Infra_VesselLib();
    BLL_ODM objODM = new BLL_ODM();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtDescription.Attributes.Add("onkeyup", "LimtCharacters(4000)");
        if (!IsPostBack)
        {
            BindODMDeatil();
            Session["dtVessel"] = null;
            Session["GroupId"] = null;
            if (Request.QueryString["ID"] != null &&  Request.QueryString["ID"].ToString() != "0")
            {
                lblCreatedBy.Visible = true;
                BindDepartments();
                GroupId = Convert.ToInt32(Request.QueryString["ID"]);
                Session["GroupId"] = GroupId;
                Session["VesselName"] = "NoName";
                DataTable dt = objODM.Get_ODM_Vessels(GroupId);
                BindODMVessels();
                if (dt.Rows.Count > 0)
                {
                    ddlDepartment.SelectedValue = dt.Rows[0]["ODM_Department_ID"].ToString();
                    if (Convert.ToBoolean(dt.Rows[0]["SendToAll"]))
                    {
                        dvVesselList.Visible = false;
                        ddlVessel.Visible = false;
                        btnVesselAdd.Visible = false;
                        chkVessel.Visible = false;
                        chkVesselAll.Checked = true;
                    }
                    txtDescription.Text = dt.Rows[0]["MSG_TEXT"].ToString();
                    txtSubject.Text = dt.Rows[0]["ODM_SUBJECT"].ToString();
                    lblCreatedBy.Text = "Created By: <b>" + dt.Rows[0]["CreatedUser"].ToString() + "</b>  On: " + dt.Rows[0]["Created_Date"].ToString();
                    if (dt.Rows[0]["UpdatedUser"].ToString() != "")
                        lblUpdatedBy.Text = "Updated By : " + dt.Rows[0]["UpdatedUser"].ToString() + "  On: " + dt.Rows[0]["Updated_Date"].ToString();
                    lblUpdatedBy.Visible = true;
                    btnsave.Text = "Update";
                    lblNotificatin.Text = "Only ODM creator can modify. ";
                    //btnDelete.Visible = true;
                    lblUpload.Visible = true;
                    File_Upload.Visible = true;
                    File_Upload.Enabled = true;
                    btnUpload.Visible = true;
                    BindAttachment();
                }
                //else
                //    lblCreatedBy.Text = "Created By: " + Session["SUPPNAME"].ToString();
            }
            
        }
    }

    


    protected void BindODMDeatil()
    {
        try
        {
            GroupId = Convert.ToInt32(Session["GroupId"]);
            BindDepartments();
            BindVessels();

        }
        catch { }
        {
        }
    }

    protected void ClearControls()
    {
        txtSubject.Text = "";
        txtDescription.Text = "";     
        DataTable dtVessels = (DataTable)Session["dtVesssels"];
        dtVessels.Clear();
        chkVessel.DataSource = dtVessels;
        chkVessel.DataBind();
    }

    
    protected void BindDepartments()
    {

       DataTable dt = objODM.Get_ODM_Departments();
       ddlDepartment.DataSource = dt;
       ddlDepartment.DataTextField = "Deapartment_Name";
       ddlDepartment.DataValueField = "ID";
       ddlDepartment.DataBind();
       ddlDepartment.Items.Insert(0, new ListItem("-Select-", "0"));
    }

    protected void BindVessels()
    {
        DataTable dt = objODM.GET_ODM_VesselsAll();

        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSEL_NAME";
        ddlVessel.DataValueField = "VESSEL_ID";
        ddlVessel.DataBind();
        ddlVessel.Items.Insert(0, new ListItem("-Select-", "0"));
    }





    protected void btnVesselAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = (DataTable)Session["dtVessel"];
        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("Vessel_Id");
            dt.Columns.Add("Vessel_Name");
        }

        DataRow dr = dt.NewRow();


        dr["Vessel_Id"] = ddlVessel.SelectedValue;
        dr["Vessel_Name"] = ddlVessel.SelectedItem.Text;
        dt.Rows.Add(dr);

        ddlVessel.Items.RemoveAt(ddlVessel.SelectedIndex);

        Session["dtVessel"] = dt;
        chkVessel.DataSource = dt;
        chkVessel.DataValueField = "Vessel_Id";
        chkVessel.DataTextField = "Vessel_Name";
        chkVessel.DataBind();
        //chk1.SelectedItem.Selected = true;

        if (chkVessel.Items.Count > 0)
        {
            foreach (ListItem chkitem in chkVessel.Items)
            {
                chkitem.Selected = true;
            }

        }

    }
    protected void btnVesselRemove_Click(object sender, EventArgs e)
    {

        DataTable dt = (DataTable)Session["dtVessel"];
        BindVessels();

        dt.Clear();
        Session["dt"] = dt;
        chkVessel.DataSource = dt;

        chkVessel.DataBind();


    }


    protected void btnsave_Click(object sender, EventArgs e)
    {
        Savedata();

        Session["dtVessel"] = null;
        string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);

    }


    protected void Savedata()
    {
        int GroupId = 0;
        if (Session["GroupId"] != null)
            GroupId = Convert.ToInt32(Session["GroupId"]);
        string status = "";

        int DepartmentId = UDFLib.ConvertToInteger(ddlDepartment.SelectedValue);
        DataTable dtODMVessels = new DataTable();
        dtODMVessels.Columns.Add("ID");
        dtODMVessels.Columns.Add("PID");
        int VesselId = 0;
        int count = 0;
        if (GroupId == 0)
        {
            if (chkVesselAll.Checked)
            {
                IsallVessels = true;
                foreach (ListItem ddlitem in ddlVessel.Items)
                {
                    if (ddlitem.Value != "0")
                    {



                        VesselId = UDFLib.ConvertToInteger(ddlitem.Value);

                        objODM.Insert_ODM_Vessel(txtSubject.Text.Trim(), txtDescription.Text.Trim(), VesselId, DepartmentId, IsallVessels, UDFLib.ConvertToInteger(Session["UserID"].ToString()), ref GroupId);

                    }
                }
            }
            else
            {
                foreach (ListItem chkitem in chkVessel.Items)
                {

                    VesselId = UDFLib.ConvertToInteger(chkitem.Value);
                    objODM.Insert_ODM_Vessel(txtSubject.Text.Trim(), txtDescription.Text.Trim(), VesselId, DepartmentId, IsallVessels, UDFLib.ConvertToInteger(Session["UserID"].ToString()), ref GroupId);
                }
            }
        }
        else
        {
            if (chkVesselAll.Checked)
            {
                IsallVessels = true;
                DataTable dt = objODM.GET_ODM_VesselsAll();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                            count++;

                            DataRow dr2 = dtODMVessels.NewRow();
                            dr2["ID"] = count.ToString();
                            dr2["PID"] = UDFLib.ConvertToInteger(dr["Vessel_Id"]);

                            dtODMVessels.Rows.Add(dr2);

                    }
                }
            }
            else
            {
                foreach (ListItem chkitem in chkVessel.Items)
                {
                    if (chkitem.Selected == true)
                    {
                        count++;
                        DataRow dr = dtODMVessels.NewRow();
                        dr["ID"] = count.ToString();
                        dr["PID"] = UDFLib.ConvertToInteger(chkitem.Value);

                        dtODMVessels.Rows.Add(dr);
                    }
                }
            }

            objODM.Upd_ODM_Vessel(GroupId, dtODMVessels, IsallVessels ,txtSubject.Text.Trim(), txtDescription.Text.Trim(), DepartmentId, UDFLib.ConvertToInteger(Session["UserID"].ToString()));
        }
    }





    protected void chkVesselAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkVesselAll.Checked)
        {
            btnVesselAdd.Visible = false;
            chkVessel.Visible = false;
            dvVesselList.Visible = false;
            ddlVessel.Visible = false;
        }
        else
        {
            BindODMVessels();
            dvVesselList.Visible = true;
            ddlVessel.Visible = true;
            btnVesselAdd.Visible = true;
            chkVessel.Visible = true;
        }
    }
    protected void BindODMVessels()
    {
        GroupId = Convert.ToInt32(Session["GroupId"]);
        DataTable dt1 = objODM.GET_GroupVessels(GroupId);//Load by notificationId
        Session["dtVessel"] = dt1;
        chkVessel.DataSource = dt1;
        chkVessel.DataTextField = "Vessel_Name";
        chkVessel.DataValueField = "Vessel_ID";
        chkVessel.DataBind();
        Session["dtVessels"] = dt1;
        foreach (ListItem chkitem in chkVessel.Items)
        {
            chkitem.Selected = true;
            if (ddlVessel.Items.FindByValue(chkitem.Value) != null)
            {
                ListItem itemToRemove = ddlVessel.Items.FindByValue(chkitem.Value);
                ddlVessel.Items.Remove(itemToRemove);

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
                Session["ODM_Id"] = Request.QueryString["ID"];
                Session["VesselName"] = Request.QueryString["VName"];
            }
        }
        catch { }

    }

    protected void Upload1_Click(object sender, EventArgs e)
    {
        if (File_Upload.FileName != "")
        {
            string strLocalPath = File_Upload.PostedFile.FileName;
            string FileName = Path.GetFileName(strLocalPath);
            string FileExtension = Path.GetExtension(strLocalPath);
            SetSession();
            //string VesselName = UDFLib.ConvertStringToNull(Session["VesselName"]);
            int size =  File_Upload.PostedFile.ContentLength/1024; //In  KB
            if (size < 1)
                size = 1;
            Guid FileGuid = System.Guid.NewGuid();
            int ?UserId = UDFLib.ConvertIntegerToNull(Session["userid"]);
            if (Session["GroupId"] != null)
            {
                GroupId = Convert.ToInt32(Session["GroupId"]);
                string sPath = "..\\Uploads\\ODM\\" + DateTime.Now.ToString("ddMMMyy") + "\\" ;
                if (!Directory.Exists(Server.MapPath(sPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(sPath));
                }
                File_Upload.PostedFile.SaveAs(Server.MapPath(sPath + FileGuid + FileExtension));
                sPath = sPath.Replace("..", "");
                objODM.Ins_ODM_Attachments(GroupId, FileName, (sPath + FileGuid + FileExtension), FileGuid.ToString(), size, UserId);
            }
            BindAttachment();

        }
    }

    protected void gvAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper().Equals("DELETE"))
        {
            GridView _gridView = (GridView)sender;
            int nCurrentRow = Int32.Parse(e.CommandArgument.ToString());
            string FileID = ((Label)gvAttachment.Rows[nCurrentRow].FindControl("lblFileId")).Text;


            if (FileID.ToString() == "")
            {
                DataTable dt = (DataTable)ViewState["TempDtLocation"];
                dt.Rows[nCurrentRow].Delete();
                dt.AcceptChanges();
            }
            else
            {
                int retval = objODM.Attachment_Delete(UDFLib.ConvertIntegerToNull(Session["ODMId"]), UDFLib.ConvertIntegerToNull(Session["userid"]));

                BindAttachment();
            }

        }
    }
    protected void gvAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgAttDelete = (ImageButton)e.Row.FindControl("ImgAttDelete");
        }

    }
    protected void ImgAttDelete_Click(object sender, CommandEventArgs e)
    {

        string[] cmdargs = e.CommandArgument.ToString().Split(',');

        string fileid = cmdargs[0].ToString();
        string fileName = cmdargs[1].ToString();
        string VesselName = UDFLib.ConvertStringToNull(Session["VesselName"]);
        string filepath = "../uploads/ODM/" + VesselName +"/"+ fileName;

        int retval = objODM.Attachment_Delete(UDFLib.ConvertIntegerToNull(fileid), UDFLib.ConvertIntegerToNull(Session["userid"]));


        if (File.Exists(Server.MapPath(filepath)))
            File.Delete(Server.MapPath(filepath));

        BindAttachment();



    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        objODM.Del_Vessel_ODM(UDFLib.ConvertIntegerToNull(Session["ODMId"]), UDFLib.ConvertIntegerToNull(Session["userid"]));

        Response.Redirect("../ODM/Daily_Message_Queue.aspx");
    }
}