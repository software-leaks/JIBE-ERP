using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMS.Business.LMS;
using System.Data;

public partial class LMS_VideoItems : System.Web.UI.Page
{
    public SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HdnUserID.Value = Session["USERID"].ToString();
            UserAccessValidation();
            BindTrainingItems();


        }
    }
    protected void UserAccessValidation()
    {
        int CurrentUserID = Convert.ToInt32(Session["userid"]);
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());
        SMS.Business.Infrastructure.BLL_Infra_UserCredentials objUser = new SMS.Business.Infrastructure.BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.View == 0)
            Response.Redirect("~/default.aspx?msgid=1");

        if (objUA.Add == 0)
        {

        }
        if (objUA.Edit == 0)
        {


        }
        if (objUA.Approve == 0)
        {

        }
        if (objUA.Delete == 0)
        {


        }


    }

    public void BindTrainingItems()
    {

        int is_Fetch_Count = ucCustomPagerAllStatus.isCountRecord;

        DataTable dt = BLL_LMS_Training.Get_Training_Items_List(UDFLib.ConvertStringToNull(txtSearchItemName.Text), ucCustomPagerAllStatus.CurrentPageIndex, ucCustomPagerAllStatus.PageSize, ref is_Fetch_Count, Convert.ToInt32(Session["USERID"].ToString()));


        dtlItemList.DataSource = dt;
        dtlItemList.DataBind();

        ucCustomPagerAllStatus.CountTotalRec = is_Fetch_Count.ToString();
        ucCustomPagerAllStatus.BuildPager();
        BindItemTreeView();

    }

    protected void btnSearchItem_Click(object sender, EventArgs e)
    {

        dtlItemList.DataSource = null;
        dtlItemList.DataBind();
        tvItemList.Nodes.Clear();
        BindTrainingItems();


    }

    public void BindItemTreeView()
    {

        DataSet dsProgramList = BLL_LMS_Training.Get_Video_Program_List(UDFLib.ConvertStringToNull(txtSearchItemName.Text));

        if (dsProgramList.Tables.Count == 3)
        {

            foreach (DataRow drParent in dsProgramList.Tables[0].Rows)
            {
                bool isprogramvalid = false;
                TreeNode parentNode = new TreeNode(drParent["PROGRAM_DESCRIPTION"].ToString());
                parentNode.NavigateUrl = "#";
                parentNode.ImageUrl = "../Images/LMS_Program.png";


                DataRow[] drChildList_Chapter = dsProgramList.Tables[1].Select("PROGRAM_ID=" + drParent["PROGRAM_ID"].ToString());
                bool iscvalidchapter = false;
                foreach (DataRow drChild1 in drChildList_Chapter)
                {
                    iscvalidchapter = false;
                    TreeNode Child_Chapter = new TreeNode(drChild1["CHAPTER_DESCRIPTION"].ToString());
                    Child_Chapter.NavigateUrl = "#";
                    Child_Chapter.ImageUrl = "../Images/LMS_Chapter.png";

                    DataRow[] drChildList_Items = dsProgramList.Tables[2].Select("CHAPTER_ID=" + drChild1["CHAPTER_ID"].ToString());

                    foreach (DataRow drChild2 in drChildList_Items)
                    {
                        bool isitemvalid = false;


                        // TreeNode Child_Item = new TreeNode(drChild2["ITEM_NAME"].ToString(), "", "../Images/LMS_Video_Play.png", "http://" + HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath + "/Uploads/TrainingItems/" + drChild2["ITEM_PATH"].ToString(), drChild2["ITEM_NAME"].ToString() + ":" + drChild1["CHAPTER_ID"].ToString() + ":" + Session["USERID"].ToString());
                        TreeNode Child_Item = new TreeNode(drChild2["ITEM_NAME"].ToString(), "", "../Images/LMS_Video_Play.png", "../Uploads/TrainingItems/" + drChild2["ITEM_PATH"].ToString(), drChild2["ITEM_NAME"].ToString() + ":" + drChild1["CHAPTER_ID"].ToString() + ":" + Session["USERID"].ToString());

                        if (System.IO.Path.GetExtension(drChild2["ITEM_PATH"].ToString()).Contains((".mp4")))
                        {
                            Child_Chapter.SelectAction = TreeNodeSelectAction.Select;
                            //Child_Chapter.ChildNodes.Add(Child_Item);
                            if (drChild2["ITEM_NAME"].ToString().ToUpper().Trim().Contains(txtSearchItemName.Text.Trim().ToUpper()))
                            {
                                iscvalidchapter = true;
                                isitemvalid = true;
                            }
                            if (txtSearchItemName.Text.Trim().Length > 0)
                            {
                                if (isitemvalid)
                                {
                                    Child_Chapter.ChildNodes.Add(Child_Item);
                                    isprogramvalid = true;
                                }

                            }
                            else
                            {
                                Child_Chapter.ChildNodes.Add(Child_Item);
                                isprogramvalid = true;
                            }
                        }



                    }
                    if (iscvalidchapter)
                    {
                        parentNode.ChildNodes.Add(Child_Chapter);
                        isprogramvalid = true;
                    }

                    //if (txtSearchItemName.Text.Trim().Length > 0)
                    //{
                    //    if (iscvalidchapter)
                    //        parentNode.ChildNodes.Add(Child_Chapter);
                    //}
                    //else
                    //{
                    //    if (iscvalidchapter)
                    //    parentNode.ChildNodes.Add(Child_Chapter);
                    //}


                }
                if (isprogramvalid)
                    tvItemList.Nodes.Add(parentNode);
                parentNode.ExpandAll();
            }


        }
        if (tvItemList.Nodes.Count == 0)
        {
            lblNrf.Visible = true;


        }
        else
        {
            lblNrf.Visible = false;


        }
        String msgretv = String.Format("OnLoad();");
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgret6v", msgretv, true);

    }


}