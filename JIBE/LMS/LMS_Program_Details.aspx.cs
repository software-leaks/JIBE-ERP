using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using SMS.Business.LMS;
using System.IO;

public partial class LMS_Program_Details : System.Web.UI.Page
{

    private string IsProgram_Scheduled;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            UserAccessValidation();
            hdfProgram_Id.Value = Request.QueryString["Program_Id"];
            bindProgramcategory();

            if (UDFLib.ConvertToInteger(hdfProgram_Id.Value) > 0)
            {
                DataTable dtProgramDetails = BLL_LMS_Training.Get_ProgramDescriptionbyId(Convert.ToInt32(hdfProgram_Id.Value));
                if (dtProgramDetails.Rows.Count > 0)
                {
                    txtProgramName.Text = Convert.ToString(dtProgramDetails.Rows[0]["PROGRAM_Name"]);
                    txtProgramDescription.Text = Convert.ToString(dtProgramDetails.Rows[0]["PROGRAM_DESCRIPTION"]);
                    txtDuration.Text = Convert.ToString(dtProgramDetails.Rows[0]["DURATION"]);
                    ddlProgramCategory.SelectedValue = Convert.ToString(dtProgramDetails.Rows[0]["PROGRAM_CATEGORY_ID"]);
                    ViewState["OrgType"] = dtProgramDetails.Rows[0]["PROGRAM_CATEGORY_ID"];

                    ddlProgramCategory.Enabled = false;
                    hdfProgramCategory.Value = ddlProgramCategory.SelectedValue;
                    if (UDFLib.ConvertStringToNull(dtProgramDetails.Rows[0]["PROGRAM_TYPE"]) == "JIBETRAINING")
                    {
                        rdbVideo.Checked = true;
                    }
                    else
                    {
                        rdbTraining.Checked = true;
                    }
                    IsProgram_Scheduled = Convert.ToString(dtProgramDetails.Rows[0]["PROGRAM_SCHEDULED"]);
                }
            }
            else
            {
                btnNewChapter.Visible = false;
            }

            BindItemTreeView();


        }

        // use javascript to open the add new chapter if program id is not null else create program and open chapter page
        if (UDFLib.ConvertToInteger(hdfProgram_Id.Value) > 0)
        {
            btnNewChapter.OnClientClick = "Show_Chapter_Details(null); return false";
            btnNewChapter.Enabled = true;

        }

        if (IsProgram_Scheduled == "Y")
        {
            txtProgramName.Enabled = false;
            txtProgramDescription.Enabled = false;
            txtDuration.Enabled = false;
            ddlProgramCategory.Enabled = false;
            rdbVideo.Enabled = false;
            rdbTraining.Enabled = false;

            btnSaveProgram.Visible = false;
            btnNewChapter.Visible = false;
        }

    }
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        //if (objUA.View == 0)
        //    Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {
            btnNewChapter.Visible = false;
            btnSaveProgram.Visible = false;
        }
        if (objUA.Edit == 0)
        {
            btnNewChapter.Visible = false;
            btnSaveProgram.Visible = false;

        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindItemTreeView();


    }

    public void bindProgramcategory()
    {
        DataTable dt = BLL_LMS_Training.Get_Program_Category();
        ddlProgramCategory.DataSource = dt;
        ddlProgramCategory.DataTextField = "Prg_Cat_Name";
        ddlProgramCategory.DataValueField = "Prg_Cat_Id";
        ddlProgramCategory.DataBind();
        ddlProgramCategory.SelectedIndex = 2;

    }

    public void BindItemTreeView()
    {

        DataSet dsProgramList = null;

        dsProgramList = BLL_LMS_Training.GET_Program_Details(UDFLib.ConvertToInteger(hdfProgram_Id.Value));


        if (dsProgramList.Tables.Count == 3)
        {
            foreach (DataRow drParent in dsProgramList.Tables[0].Rows)
            {
                TreeNode parentNode = new TreeNode(drParent["PROGRAM_NAME"].ToString());
                parentNode.NavigateUrl = "#";
                parentNode.ImageUrl = "../Images/LMS_Program.png";


                DataRow[] drChildList_Chapter = dsProgramList.Tables[1].Select("PROGRAM_ID=" + drParent["PROGRAM_ID"].ToString());

                foreach (DataRow drChild1 in drChildList_Chapter)
                {

                    TreeNode Child_Chapter = new TreeNode(drChild1["CHAPTER_DESCRIPTION"].ToString());
                    Child_Chapter.NavigateUrl = "LMS_Chapter_Details.aspx?Chapter_ID=" + drChild1["CHAPTER_ID"].ToString() + "&Program_ID=" + hdfProgram_Id.Value + "&ProgramCategory=" + UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue);
                    Child_Chapter.ImageUrl = "../Images/LMS_Chapter.png";

                    DataRow[] drChildList_Items = dsProgramList.Tables[2].Select("CHAPTER_ID=" + drChild1["CHAPTER_ID"].ToString());

                    foreach (DataRow drChild2 in drChildList_Items)
                    {
                        TreeNode Child_Item;
                        if (drChild2["ITEM_TYPE"].ToString() != "FBM")
                        {
                            string ImagePath = "../Images/noneimg.png";

                            if (File.Exists(Server.MapPath("~/Images/DocTree/" + Path.GetExtension(drChild2["ITEM_PATH"].ToString()).Replace(".", "") + ".png")))
                            {
                                ImagePath = "../Images/DocTree/" + Path.GetExtension(drChild2["ITEM_PATH"].ToString()).Replace(".", "") + ".png";
                            }
                            else
                            {
                                if (drChild2["ITEM_TYPE"].ToString() == "VIDEO MATERIALS")
                                {
                                    ImagePath = "../Images/DocTree/mp4.png";
                                }
                                else
                                {
                                    ImagePath = "../Images/DocTree/txt.png";
                                }
                            }
                            string filePath = "";
                            if (File.Exists(Server.MapPath("~/Uploads/TrainingItems/" + drChild2["ITEM_PATH"].ToString())))
                            {
                                filePath = "../Uploads/TrainingItems/" + drChild2["ITEM_PATH"].ToString();

                            }
                            else
                            {
                                filePath = "../FileNotFound.aspx";
                            }
                            Child_Item = new TreeNode(drChild2["ITEM_NAME"].ToString(), "", ImagePath, filePath, drChild2["ITEM_NAME"].ToString());
                            Child_Chapter.ChildNodes.Add(Child_Item);
                            Child_Item.ToolTip = drChild2["ITEM_Description"].ToString();

                        }

                        else
                        {
                            string filePath = "";
                            filePath = "../QMS/FBM/" + drChild2["ITEM_PATH"].ToString();
                            //if (File.Exists(Server.MapPath("~/QMS/FBM/" + drChild2["ITEM_PATH"].ToString())))
                            //{


                            //}
                            //else
                            //{
                            //    filePath = "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/FileNotFound.aspx";
                            //}
                            Child_Item = new TreeNode(drChild2["ITEM_NAME"].ToString(), "", "", filePath, drChild2["ITEM_NAME"].ToString());

                            Child_Chapter.ChildNodes.Add(Child_Item);

                        }

                    }

                    parentNode.ChildNodes.Add(Child_Chapter);

                }

                tvItemList.Nodes.Add(parentNode);
                parentNode.ExpandAll();
            }

        }

        String msgretv = String.Format("OnLoad();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

    }

    protected void btnNewChapter_Click(object sender, EventArgs e)
    {
        if (UDFLib.ConvertToInteger(txtDuration.Text) <= 0 && UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue) != 4)
        {

            string msgmodal = String.Format("alertm('dur')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);
            return;
        }

        if (UDFLib.ConvertToInteger(hdfProgram_Id.Value) == 0)
        {
            string lProgramType = "VESSELTRAINING";
            if (rdbVideo.Checked)
            {
                lProgramType = "JIBETRAINING";
            }

            hdfProgramCategory.Value = ddlProgramCategory.SelectedValue;
            hdfProgram_Id.Value = BLL_LMS_Training.Ins_Program_Details(null, UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue), txtProgramName.Text.Trim(), txtProgramDescription.Text.Trim(), UDFLib.ConvertIntegerToNull(txtDuration.Text.Trim()), lProgramType, Convert.ToInt32(Session["USERID"]), 1).ToString();
        }

        String msgretv = String.Format("OpenPopupWindowBtnID('POP__ChapterDetails', 'Chapter Details','LMS_Chapter_Details.aspx?Chapter_ID=&Program_ID=" + hdfProgram_Id.Value + "&ProgramCategory=" + UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue) + "', 'popup', 840, 1000, null, null, false, false, true, false,'" + btnSearch.ClientID + "')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);


    }



    protected void btnSaveProgram_Click(object sender, EventArgs e)
    {
        string msgmodal;
        if (UDFLib.ConvertToInteger(txtDuration.Text) <= 0 && UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue) != 4)
        {
            msgmodal = String.Format("alertm('dur')");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);
            return;
        }
        string lProgramType = "VESSELTRAINING";
        if (rdbVideo.Checked)
        {
            lProgramType = "JIBETRAINING";
        }
        hdfProgramCategory.Value = ddlProgramCategory.SelectedValue;
        hdfProgram_Id.Value = BLL_LMS_Training.Ins_Program_Details(UDFLib.ConvertIntegerToNull(hdfProgram_Id.Value), UDFLib.ConvertIntegerToNull(ddlProgramCategory.SelectedValue), txtProgramName.Text.Trim(), txtProgramDescription.Text.Trim(), UDFLib.ConvertIntegerToNull(txtDuration.Text.Trim()), lProgramType, Convert.ToInt32(Session["USERID"]), 1).ToString();
        BindItemTreeView();
        if (UDFLib.ConvertIntegerToNull(hdfProgram_Id.Value) != null)
        {
            btnNewChapter.Visible = true;
        }
        ddlProgramCategory.Enabled = false;
        msgmodal = String.Format("alertm('sav')");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", msgmodal, true);

    }


}