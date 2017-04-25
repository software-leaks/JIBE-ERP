using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.LMS;
using System.Data;
using System.IO;
using SMS.Properties;
using SMS.Business.Infrastructure;

public partial class LMS_Program_Sync : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindPrograms();
            BindHistorySelection();
            UserAccessValidation();

        }
    }
    UserAccess objUA = new UserAccess();
    BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Admin == 0)
        {
            btnDownloadSelected.Enabled = false;

        }
        ViewState["edit"] = objUA.Edit;
        ViewState["del"] = objUA.Delete;

    }
    public void BindHistorySelection()
    {
        ddlSyncDate.DataSource = BLL_LMS_Training.GET_Programs_To_Sync_Date_List();
        ddlSyncDate.DataBind();
        ddlSyncDate.Items.Insert(0, "--SELECT--");
    }

    public void BindHistory()
    {
        if (ddlSyncDate.SelectedIndex <= 0)
        {
            chkHistory.Checked = false;
            btnDownloadSelected.Visible = true;
            return;
        }

        try
        {
            int is_Fetch_Count = ucCustomPagerProgram_List.isCountRecord;
            DateTime? HISDate = Convert.ToDateTime(ddlSyncDate.SelectedValue);

            DataSet ds_ProgramList_Scheduled = BLL_LMS_Training.GET_Programs_To_Sync_His(ucCustomPagerProgram_List.CurrentPageIndex, ucCustomPagerProgram_List.PageSize, HISDate, ref is_Fetch_Count);



            gvProgram_ListDetails.DataSource = ds_ProgramList_Scheduled.Tables[0];
            gvProgram_ListDetails.DataBind();

            ucCustomPagerProgram_List.CountTotalRec = is_Fetch_Count.ToString();
            ucCustomPagerProgram_List.BuildPager();
        }
        catch (Exception)
        {

            chkHistory.Checked = false;
            btnDownloadSelected.Visible = true;
            return;
        }

    }

    public void BindPrograms()
    {
        int is_Fetch_Count = ucCustomPagerProgram_List.isCountRecord;
        DataSet ds_ProgramList_Scheduled = BLL_LMS_Training.Get_Programs_To_Sync(ucCustomPagerProgram_List.CurrentPageIndex, ucCustomPagerProgram_List.PageSize, ref is_Fetch_Count);



        gvProgram_ListDetails.DataSource = ds_ProgramList_Scheduled.Tables[0];
        gvProgram_ListDetails.DataBind();

        ucCustomPagerProgram_List.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerProgram_List.BuildPager();

    }

    protected void btnDownloadSelected_Click(object sender, EventArgs e)
    {
        string msgmodal = "";
        try
        {

            DataTable tablePIDS = new DataTable();
            tablePIDS.Columns.Add("ID", typeof(int));
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("VALUE", typeof(string));
            foreach (GridViewRow row in gvProgram_ListDetails.Rows)
            {
                if (row.RowType != DataControlRowType.Header)
                {
                    CheckBox checkRow = row.FindControl("checkRow") as CheckBox;
                    TextBox txtProgramIDS = row.FindControl("txtProgramIDS") as TextBox;


                    if (checkRow.Checked)
                    {
                        List<string> PIDS = new List<string>();
                        List<string> lPrograms = txtProgramIDS.Text.Split(',').ToList<string>();

                        foreach (string lProgram in lPrograms)
                        {
                            if (!PIDS.Contains(lProgram.Trim()))
                            {
                                if (UDFLib.ConvertIntegerToNull(lProgram.Trim()) != null)
                                {
                                    tablePIDS.Rows.Add(Convert.ToInt32(lProgram.Trim()));
                                    table.Rows.Add(gvProgram_ListDetails.DataKeys[row.RowIndex].Values[0].ToString(), lProgram.Trim());
                                    PIDS.Add(lProgram.Trim());
                                }

                            }

                        }

                    }
                }

            }

            if (tablePIDS.Rows.Count <= 0)
            {
                msgmodal = String.Format("alert('Vessel(s) not selected for downloading training items!.')");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

                return;
            }



            DataSet ds_ProgramList_Scheduled = BLL_LMS_Training.GET_FileList_To_Sync(tablePIDS);

            int rowindex = 0;
            string zipFile = "";
            List<string> zips = new List<string>();
            foreach (DataRow row in ds_ProgramList_Scheduled.Tables[0].Rows)
            {

                zips.Add(row["ITEM_PATH"].ToString());
                rowindex++;
            }

            if (zips.Count > 0)
            {
                var ac = Server.MapPath("~/Uploads/TrainingItems/");
             
                zipFile = BLL_LMS_Training.RAR(Server.MapPath("~/Uploads/TrainingItems/"), zips);



                int lLastValue = BLL_LMS_Training.Ins_Program_Sync(table, Convert.ToInt32(Session["USERID"]));

                string DownloadFileName = "AttachedProgramDocuments" + "_" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "h" + DateTime.Now.Minute + "m" + DateTime.Now.Second + "s" + ".rar";
                if (File.Exists(Server.MapPath("~/Uploads/TrainingItems/" + zipFile)))
                {
                    File.Move(Server.MapPath("~/Uploads/TrainingItems/" + zipFile), Server.MapPath("~/Uploads/Temp/" + DownloadFileName));

                    ResponseHelper.Redirect("~/Uploads/Temp/" + DownloadFileName, "blank", "");

                }
                else
                {
                    msgmodal = String.Format("alert('Files not present on the Server.')");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);


                }


                BindPrograms();
                BindHistorySelection();
                ddlSyncDate.SelectedIndex = -1;



            }
        }
        catch (Exception ex)
        {
            msgmodal = String.Format("Error :\n " + ex.ToString());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);
        }
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
    protected void chkHistory_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlSyncDate.SelectedIndex <= 0 && chkHistory.Checked)
        {
            string msgmodal = String.Format("alert('Sync Date should not be blank.')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);
            chkHistory.Checked = false;
            return;
        }
        if (chkHistory.Checked == true)
        {
            BindHistory();
            btnDownloadSelected.Visible = false;
        }
        else
        {
            BindPrograms();
            btnDownloadSelected.Visible = true;
        }
    }

    protected void gvProgram_ListDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (chkHistory.Checked)
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }
        else
        {
            if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = true;
            }
        }
    }


    protected void ddlSyncDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSyncDate.SelectedIndex <= 0)
        {
            return;
        }
        if (chkHistory.Checked)
        {
            BindHistory();
            btnDownloadSelected.Visible = false;
        }
    }
}