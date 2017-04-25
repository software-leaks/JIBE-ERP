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

using SMS.Business.Infrastructure;
using SMS.Properties;
using Telerik.Web.UI;

using System.IO;
using System.Collections.Generic;


public partial class Infrastructure_USModule_MenuItemsEdit : System.Web.UI.Page
{
    public static Int64 GmodLst_mcode = 0;
    public static Int64 GsubmodLst_mcode = 0;

    BLL_Infra_MenuManagement objBLL = new BLL_Infra_MenuManagement();
    UserAccess objUA = new UserAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        UserAccessValidation();
        if (!IsPostBack)
        {
            fill_mainmodules();
            Load_DepartmentList_filter();
            DataTable dt = objBLL.Get_MenuAccess(null, null);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

        }
    }

    protected void Load_DepartmentList_filter()
    {
        BLL_Infra_Company objCompBLL = new BLL_Infra_Company();
        int iCompID = int.Parse(Session["UserCompanyID"].ToString());
        DataTable dtfilter = objCompBLL.Get_CompanyDepartmentList(iCompID);

        ddlDepartment.Items.Clear();
        ddlDepartment.DataSource = dtfilter;
        ddlDepartment.DataTextField = "VALUE";
        ddlDepartment.DataValueField = "ID";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem("-SELECT ALL-", "0"));
    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();
        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

        if (objUA.IsAdmin == 0)
        {
            if (objUA.View == 0)
                Response.Redirect("~/default.aspx?msgid=1");

            if (objUA.Add == 0)
            {
                Response.Redirect("~/default.aspx?msgid=2");
            }
            if (objUA.Edit == 0)
            {
                Response.Redirect("~/default.aspx?msgid=3");
            }
            if (objUA.Delete == 0)
            {
                Response.Redirect("~/default.aspx?msgid=4");
            }
            if (objUA.Approve == 0)
            {
                Response.Redirect("~/default.aspx?msgid=5");
            }
        }
     
        
    }

    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }

    public void fill_mainmodules()
    {

        try
        {
            lst_module.DataSource = objBLL.GetCollection_AllModules();
            lst_module.DataTextField = "Menu_Short_Discription";
            lst_module.DataValueField = "mcodeseq";

            lst_module.DataBind();

        }
        catch { }


    }

    protected void lst_module_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clear the textboxes values
        imgBtnAddSubMod.Visible = true;
        txt_submodule.Text = "";
        txt_submodule_seq.Text = "";
        string str = lst_module.SelectedValue.ToString().Split(',')[0];
        ViewState["mlcode"] = str;
        txt_link.Text = "";
        txt_link_seq.Text = "";
        txtUrl.Text = "";
        txtSubModuleUrl0.Text = "";
        txtSubModuleUrl.Text = "";
        chkLink.Checked = false;
        chkSuModule.Checked = false;
        chkModule.Checked = false;
        lst_links.Items.Clear();
        ddlDepartment.SelectedValue = "0";

        fill_submodules();
        EditItems(lst_module, "mod");
       
     
            DataTable dt = objBLL.Get_MenuAccess(null, Convert.ToInt32(str));
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
       
    }

    public void EditItems(ListBox lst, string loc)
    {
        ListItem li = lst.SelectedItem;
        string sVal = li.Value.ToString();
        string sSeqNo = sVal.Split(',')[1];

        if (loc == "mod")
        {
            txt_module.Text = li.Text;
            txt_module_seq.Text = sSeqNo;
            txtUrl.Text = "";
            ddlDepartment.SelectedValue = sVal.Split(',')[6];
            if (sVal.Split(',')[5] == "1")
            {
                chkModule.Checked = true;
            }
            else
            {
                chkModule.Checked = false;
            }
            txtSubModuleUrl0.Text = sVal.Split(',')[3];
            //Assign fontawesome class name to textbox
            if (sVal.Split(',')[7] != "")
                txtFontClass.Text = sVal.Split(',')[7];

                
        }
        if (loc == "submod")
        {
            txt_submodule.Text = li.Text;
            txt_submodule_seq.Text = sSeqNo;
            txtUrl.Text = "";
        }

        if (loc == "link")
        {
            txt_link.Text = li.Text;
            txt_link_seq.Text = sSeqNo;
            txtUrl.Text = sVal.Split(',')[2];


            if (sVal.Split(',')[5] == "1")
            {
                chkLink.Checked = true;
            }
            else
            {
                chkLink.Checked = false;
            }

        }

    }

    public void fill_submodules()
    {

        try
        {
            string sall = lst_module.SelectedValue.ToString();
            string[] mcseq = sall.Split(',');

            GmodLst_mcode = Convert.ToInt64(mcseq[0]);

            //DataTable dt = objBLL.Get_MenuAccess(null, (int)GmodLst_mcode);
            //RadGrid1.DataSource = dt;
            //RadGrid1.DataBind();
            lst_submodule.DataSource = objBLL.GetCollection_SubModules(Convert.ToInt32(mcseq[0])); ;
            lst_submodule.DataTextField = "Menu_Short_Discription";
            lst_submodule.DataValueField = "mcodeseq";
            lst_submodule.DataBind();

        }
        catch { }

    }

    protected void lst_submodule_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnAddSubMod.Visible = true;
        txt_link.Text = "";
        txt_link_seq.Text = "";
        txtUrl.Text = "";
        chkSuModule.Checked = false;
        string str=lst_submodule.SelectedValue.ToString().Split(',')[0];
        ViewState["mcode"] = str;
        fill_Links();
        //if (lst_links.Items.Count == 0)
     
            DataTable dt = objBLL.Get_MenuAccess(null, Convert.ToInt32(str));
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
       
        
        EditItems(lst_submodule, "submod");

        //if (txtSubModuleUrl.Text != "")
        {
            RadGrid1.Visible = true;
            //RadGrid1.DataSource = new int[] { 0, 1, 2, 3, 4 };
        }
    }

    public void fill_Links()
    {
        try
        {
            string sall = lst_submodule.SelectedValue.ToString();

            string[] mcseq = sall.Split(',');
            GsubmodLst_mcode = Convert.ToInt64(mcseq[0]);
            ViewState["mcode"] = GsubmodLst_mcode;
            lst_links.DataSource = objBLL.GetCollection_SubModules(Convert.ToInt32(mcseq[0])); ;
            lst_links.DataTextField = "Menu_Short_Discription";
            lst_links.DataValueField = "mcodeseq";
            lst_links.DataBind();
            if (mcseq[5] == "1")
            {
                chkSuModule.Checked = true;
            }
            else
            {
                chkSuModule.Checked = false;
            }
            txtSubModuleUrl.Text = mcseq[2];

        }
        catch { }



    }

    public void getLinks()
    {
        // Changed By Bikash Panigrahi

        try
        {
            string sall = lst_links.SelectedValue.ToString();

            string[] mcseq = sall.Split(',');
            GsubmodLst_mcode = Convert.ToInt64(mcseq[0]);
            ViewState["mcode"] = GsubmodLst_mcode;
            lst_links.DataSource = objBLL.GetCollection_SubModules(Convert.ToInt32(mcseq[0])); ;
            lst_links.DataTextField = "Menu_Short_Discription";
            lst_links.DataValueField = "mcodeseq";
            lst_links.DataBind();
            if (mcseq[5] == "1")
            {
                chkSuModule.Checked = true;
            }
            else
            {
                chkSuModule.Checked = false;
            }
            txtSubModuleUrl.Text = mcseq[2];

        }
        catch { }
    }
    public void MoveItemUp(ListBox lst, string loc)
    {
        int iIndex = lst.SelectedIndex;
        if (iIndex == 0) return;

        //menucode and seqnumber separated by , 

        string mc1seq1_all = lst.SelectedValue.ToString();
        string[] mc1seq1 = mc1seq1_all.Split(',');

        string mc2seq2_all = lst.Items[iIndex - 1].Value.ToString();
        string[] mc2seq2 = mc2seq2_all.Split(',');

        string MenuCode1 = mc1seq1[0];
        string MenuCode2 = mc2seq2[0];


        //call swap sp
        Swap_MenuSeqOrder(Convert.ToInt64(MenuCode1), Convert.ToInt64(MenuCode2));

        //reload list

        if (loc == "mod")
            fill_mainmodules();

        if (loc == "submod")
            fill_submodules();

        if (loc == "link")
            fill_Links();

        lst.SelectedIndex = iIndex - 1;
    }

    public void Swap_MenuSeqOrder(Int64 menucode1, Int64 menucode2)
    {

        try
        {
            objBLL.Swap_MenuSeqOrder(menucode1, menucode2);
        }
        catch { }

    }

    public void MoveItemDown(ListBox lst, string loc)
    {
        int iIndex = lst.SelectedIndex;
        if (iIndex == lst.Items.Count - 1) return;


        //menucode and seqnumber separated by , 

        string mc1seq1_all = lst.SelectedValue.ToString();
        string[] mc1seq1 = mc1seq1_all.Split(',');

        string mc2seq2_all = lst.Items[iIndex + 1].Value.ToString();
        string[] mc2seq2 = mc2seq2_all.Split(',');

        string MenuCode1 = mc1seq1[0];
        string MenuCode2 = mc2seq2[0];

        //call swap sp
        Swap_MenuSeqOrder(Convert.ToInt64(MenuCode1), Convert.ToInt64(MenuCode2));

        //reload list
        if (loc == "mod")
            fill_mainmodules();

        if (loc == "submod")
            fill_submodules();

        if (loc == "link")
            fill_Links();

        lst.SelectedIndex = iIndex + 1;
    }

    protected void img_mod_up_sorting_Click(object sender, ImageClickEventArgs e)
    {
        MoveItemUp(lst_module, "mod");
        EditItems(lst_module, "mod");
    }

    protected void img_mod_down_sotring_Click(object sender, ImageClickEventArgs e)
    {
        MoveItemDown(lst_module, "mod");
        EditItems(lst_module, "mod");
    }

    protected void img_submod_up_Click(object sender, ImageClickEventArgs e)
    {
        MoveItemUp(lst_submodule, "submod");
        EditItems(lst_submodule, "submod");
    }

    protected void img_submod_down_Click(object sender, ImageClickEventArgs e)
    {
        MoveItemDown(lst_submodule, "submod");
        EditItems(lst_submodule, "submod");
    }

    protected void img_link_up_Click(object sender, ImageClickEventArgs e)
    {
        MoveItemUp(lst_links, "link");
        EditItems(lst_links, "link");
    }

    protected void img_link_down_Click(object sender, ImageClickEventArgs e)
    {
        MoveItemDown(lst_links, "link");
        EditItems(lst_links, "link");
    }

    //protected void UpdateMenuSeq(ListBox lst, int baseNo)
    //{
    //    int i = 1;
    //    foreach (ListItem li in lst.Items)
    //    {
    //        int mCode = Convert.ToInt32(li.Value.Split(',')[0]);
    //        int iSeqNo = baseNo + i;
    //        i++;

    //        UpdateMenuSeqDB(mCode, iSeqNo);


    //    }
    //}

    //public void UpdateMenuSeqDB(int mcode, int seqnumber)
    //{

    //    try
    //    {
    //        objBLL.Update_MenuSequence(mcode, seqnumber);

    //    }
    //    catch { }

    //}


    protected void imgbtn_mod_save_Click(object sender, ImageClickEventArgs e)
    {
        string sMenuText = txt_module.Text.Trim().ToString();
        try
        {
            string surl = null;
            if (lst_module.SelectedValue != "")
            {
                surl = txtSubModuleUrl0.Text.Trim();

                int? Menu_Enable = chkModule.Checked ? 1 : 0;

                string imageName = "";
               //Save fontawesome class to database.
                if (!String.IsNullOrWhiteSpace(txtFontClass.Text.ToString()))
                {
                    imageName = "fa "+(txtFontClass.Text.ToString()).Trim();
                    txtFontClass.Text = "";
                }
                else
                    imageName = "fa fa-circle";

                objBLL.Update_MenuText(Convert.ToInt32(lst_module.SelectedValue.Split(',')[0]), sMenuText, surl, null, Menu_Enable, UDFLib.ConvertIntegerToNull(ddlDepartment.SelectedValue), imageName);
            }
        }
        catch { }


        fill_mainmodules();
        saveval(Convert.ToInt32(ViewState["mlcode"]));
    }

    protected void imgbtn_submod_save_Click(object sender, ImageClickEventArgs e)
    {
        string sMenuText = txt_submodule.Text.Trim();
        string sUrl = txtSubModuleUrl.Text.Trim();
        int? Menu_Enable = chkSuModule.Checked ? 1 : 0;

        try
        {
            if (lst_submodule.SelectedValue != "")
                objBLL.Update_MenuText(Convert.ToInt32(lst_submodule.SelectedValue.Split(',')[0]), sMenuText, sUrl, 1, Menu_Enable, null);
          
        }
        catch { }
        if (lst_submodule.SelectedValue != "")
            fill_submodules();
        saveval(Convert.ToInt32(ViewState["mcode"]));

    }

    protected void imgbtn_link_save_Click(object sender, ImageClickEventArgs e)
    {
        string sMenuText = txt_link.Text.Trim().ToString();  //100
        string sUrl = txtUrl.Text.Trim().ToString();
        int? Menu_Enable = chkLink.Checked ? 1 : 0;
        try
        {
            if (lst_links.SelectedValue != "")
                objBLL.Update_MenuText(Convert.ToInt32(lst_links.SelectedValue.Split(',')[0]), sMenuText, sUrl, 1, Menu_Enable, null);
            saveval(Convert.ToInt32( ViewState["lcode"].ToString()));
        }
        catch { }

        if (lst_links.SelectedValue != "")
            fill_Links();


    }

    protected void lst_links_SelectedIndexChanged(object sender, EventArgs e)
    {
        imgBtnAddLink.Visible = true;
        EditItems(lst_links, "link");

        string str = lst_links.SelectedItem.Value.ToString().Split(',')[0];
        ViewState["lcode"] = str;
        DataTable dt = objBLL.Get_MenuAccess(null, Convert.ToInt32(str));
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();
        RadGrid1.Visible = true;
    }

    public void Add_SubModule()
    {

        string userid = Session["USERID"].ToString();
        string surl = null;

        ListItem lstm = lst_module.SelectedItem;

        int menutype = Convert.ToInt32(lstm.Value.ToString().Split(',')[0]);
        int iNewModCode = Convert.ToInt32(lstm.Value.ToString().Split(',')[2]);

        if (txtSubModuleUrl.Text.Trim() == "")
            surl = null;
        else
            surl = txtSubModuleUrl.Text.Trim(); ;

        if (txt_submodule.Text.Trim() == "" || userid == "") return;



        try
        {

            int iNewMenuCode = objBLL.getMaximumMenuCode();

            string sModName = txt_submodule.Text;

            //int sucess = objBLL.Insert_MenuModule(iNewModCode, sModName, userid);
            int? Menu_Enable = chkSuModule.Checked ? 1 : 0;
            int sucess = objBLL.Insert_Lib_Menu(iNewMenuCode, iNewModCode, menutype, sModName, surl, userid, 1, Menu_Enable, null);

            if (sucess == 1)
            {
                //ins.insertLog(menucode.ToString(), "USModule", "LibMenuChild.aspx", "Insert", Session["USERID"].ToString(), "Lib_Menu");
            }
            saveval(iNewMenuCode);
        }
        catch { }

    }


    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox headerChkbox = (CheckBox)sender;
       
            foreach (GridDataItem item in RadGrid1.Items)
            {
                CheckBox childChkbox = (CheckBox)item.FindControl("chkBox");
                childChkbox.Checked = headerChkbox.Checked;
            }
           // headerChkbox.Checked = false;
    }

    private void saveval(int MenuId)
    {
        try
        {
            int i = 0;

            DataTable dt = new DataTable();
            dt.Columns.Add("PKID");
            dt.Columns.Add("Key_Name");

            dt.Columns.Add("Description");

            dt.Columns.Add("Module_Id");
            dt.Columns.Add("Key_Enabled");

            dt.Columns.Add("Menu_Id");


            int inc = 1;
            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                Label lblKey = (Label)(dataItem.FindControl("lblKey"));
                TextBox txtDescription = (TextBox)(dataItem.FindControl("txtDescription"));
                HiddenField hdnTPId = (HiddenField)(dataItem.FindControl("hdnTPId"));
                CheckBox chkBox = (CheckBox)(dataItem.FindControl("chkBox"));

                DataRow dritem = dt.NewRow();
                if (hdnTPId.Value == null || hdnTPId.Value == "")
                    dritem["PKID"] = "0";
                else
                    dritem["PKID"] = hdnTPId.Value;
                dritem["Key_Name"] = lblKey.Text;

                dritem["Description"] = txtDescription.Text;

                dritem["Module_Id"] = 1;
                dritem["Key_Enabled"] = chkBox.Checked;
                dritem["Menu_Id"] = MenuId;



                dt.Rows.Add(dritem);
                inc++;


               
            }

            int retval = 0;

            if (dt.Rows.Count > 0)
            {
                retval = objBLL.Get_Access(Convert.ToInt32(MenuId), GetSessionUserID(), dt);

                //string msgDraft = String.Format("parent.ReloadParent_ByButtonID();");
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "msgDraft", msgDraft, true);
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            string err = ex.ToString();
        }

    }

    public void Add_Links()
    {

        string userid = Session["USERID"].ToString();
        string surl = null;
        int menutype = 0;

        ListItem lstm = lst_submodule.SelectedItem;
        if (lstm == null)
        {
            string js = "alert('Select the parent menu item !!');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", js, true);
        }
        else
        {
            menutype = Convert.ToInt32(lstm.Value.ToString().Split(',')[0]);


            surl = txtUrl.Text.Trim();

            //if (txt_link.Text.Trim() == "" || userid == "" || txtUrl.Text.Trim() == "") return;


            try
            {
                int iNewMenuCode = objBLL.getMaximumMenuCode();
                int iNewModCode = Convert.ToInt32(lstm.Value.ToString().Split(',')[3]);

                string sModName = txt_link.Text;

                //int sucess = objBLL.Insert_MenuModule(iNewModCode, sModName, username);
                int? Menu_Enable = chkLink.Checked ? 1 : 0;
                int sucess = objBLL.Insert_Lib_Menu(iNewMenuCode, iNewModCode, menutype, sModName, surl, userid, 1, Menu_Enable, null);

                if (sucess == 1)
                {
                    //ins.insertLog(menucode.ToString(), "USModule", "LibMenuChild.aspx", "Insert", Session["USERID"].ToString(), "Lib_Menu");
                }
                saveval(iNewMenuCode);
            }
            catch { }
        }
    }

    public void Add_Module()
    {
        string userid = Session["USERID"].ToString();
        string surl = null;

        if (txt_module.Text.Trim() == "" || userid == "") return;


        try
        {

            int iNewMenuCode = objBLL.getMaximumMenuCode();
            int iNewModCode = objBLL.getMaximumModID();
            
            string sModName = txt_module.Text;
            surl = txtSubModuleUrl0.Text.Trim();
            //int sucess = objBLL.Insert_MenuModule(iNewModCode, sModName, userid);
            int? Menu_Enable = chkModule.Checked ? 1 : 0;

            string imageName = "";
            //Save fontawesome class to database.
            if (!String.IsNullOrWhiteSpace(txtFontClass.Text.ToString()))
            {
                imageName = "fa " + (txtFontClass.Text.ToString()).Trim();
                txtFontClass.Text = "";
            }
            else
                imageName = "fa fa-circle";

            int sucess = objBLL.Insert_Lib_Menu(iNewMenuCode, iNewModCode, 0, sModName, surl, userid, null, Menu_Enable, UDFLib.ConvertIntegerToNull(ddlDepartment.SelectedValue), imageName);

            if (sucess == 1)
            {
                //ins.insertLog(menucode.ToString(), "USModule", "LibMenuChild.aspx", "Insert", Session["USERID"].ToString(), "Lib_Menu");
            }
            saveval(iNewMenuCode);
        }
        catch { }

    }

    protected void imgBtnAddMod_Click(object sender, ImageClickEventArgs e)
    {
        if (txt_module.Text != "")
        {
            Add_Module();
            fill_mainmodules();

            txt_module.Text = "";
        }
    }

    protected void imgBtnAddSubMod_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_submodule.Text) && lst_module.SelectedValue != "")
        {
            Add_SubModule();
            fill_submodules();

            txt_submodule.Text = "";
        }
    }

    protected void imgBtnAddLink_Click(object sender, ImageClickEventArgs e)
    {
        //if (txt_link.Text != "" && txtUrl.Text != "" && lst_submodule.SelectedValue != "" && lst_module.SelectedValue != "")
        if (txt_link.Text != "" && lst_submodule.SelectedValue != "")
        {
            Add_Links();
            fill_Links();

            txt_link.Text = "";
            txtUrl.Text = "";
        }

    }

    public void Delete_Items(int menucode)
    {
        string delteby = Session["USERID"].ToString();

        if (delteby == "")
            return;


        try
        {
            int sucess = objBLL.Del_LibMenu(menucode, delteby);
            if (sucess == 1)
            {
                //ins.insertLog(menucode.ToString(), "USModule", "LibMenuChild.aspx", "Insert", Session["USERID"].ToString(), "Lib_Menu");
            }

        }
        catch { }

    }

    protected void imgbtn_moddelete_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_module.SelectedValue != "")
        {
            string menucode = lst_module.SelectedValue.ToString().Split(',')[0];

            if (txt_module.Text.Trim() != "")
            {
                Delete_Items(Convert.ToInt32(menucode));
                //txtSubModuleUrl0.Text = "";
                //chkModule.Checked = false;
            }

            fill_mainmodules();
            txt_module.Text = "";
            txt_module_seq.Text = "";
            txt_submodule.Text = "";
            txtSubModuleUrl.Text = "";
            txt_submodule_seq.Text = "";
            txt_link.Text = "";
            txt_link_seq.Text = "";
            txtUrl.Text = "";
            txtSubModuleUrl0.Text = "";
            ddlDepartment.SelectedValue = "0";
            chkLink.Checked = false;
            chkSuModule.Checked = false;
            chkModule.Checked = false;
            lst_links.Items.Clear();
            lst_submodule.Items.Clear();
            DataTable dt = objBLL.Get_MenuAccess(null, null);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }

    protected void imgbtn_submoddelete_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_submodule.SelectedValue != "")
        {
            string menucode = lst_submodule.SelectedValue.ToString().Split(',')[0];

            if (txt_submodule.Text.Trim() != "")
            {
                Delete_Items(Convert.ToInt32(menucode));
            }

            fill_submodules();
            txt_submodule.Text = "";
            txtSubModuleUrl.Text = "";
            txt_submodule_seq.Text = "";
            txt_link.Text = "";
            txt_link_seq.Text = "";
            txtUrl.Text = "";
            lst_links.Items.Clear();
            DataTable dt = objBLL.Get_MenuAccess(null, null);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }

    protected void imgbtn_linkdelete_Click(object sender, ImageClickEventArgs e)
    {
        if (lst_links.SelectedValue != "")
        {
            string menucode = lst_links.SelectedValue.ToString().Split(',')[0];

            if (txt_link.Text.Trim() != "" && menucode != "")
            {
                Delete_Items(Convert.ToInt32(menucode));
            }

            fill_Links();
            txt_link.Text = "";
            txt_link_seq.Text = "";
            txtUrl.Text = "";
            DataTable dt = objBLL.Get_MenuAccess(null, null);
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }
    }

    protected void OnCheckedChanged(object sender, EventArgs e)
    {
        GridDataItem item = (sender as Button).Parent.Parent as GridDataItem;
        TextBox txtBox = item.FindControl("TextBox1") as TextBox;

    }


    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = e.Item as GridDataItem;
            TextBox txt = new TextBox();

            txt.ID = "TextBox1";
            item["Column2"].Controls.Add(txt);
        }
    }
    //protected void txt_submodule_TextChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = objBLL.Get_MenuAccess(null, null);
    //    RadGrid1.DataSource = dt;
    //    RadGrid1.DataBind();
    //    imgBtnAddSubMod.Visible = true;
    //    lst_submodule.SelectedIndex = -1;
    //    txtSubModuleUrl.Text = "";
    //    RadGrid1.Visible = true;
    //}
    //protected void txt_link_TextChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = objBLL.Get_MenuAccess(null, null);
    //    RadGrid1.DataSource = dt;
    //    RadGrid1.DataBind();
    //    imgBtnAddLink.Visible = true;
    //    lst_links.SelectedIndex = -1;
    //    txtUrl.Text = "";
    //    RadGrid1.Visible = true;
    //}

    //protected void txt_module_TextChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = objBLL.Get_MenuAccess(null, null);
    //    RadGrid1.DataSource = dt;
    //    RadGrid1.DataBind();
    //    imgBtnAddLink.Visible = true;
    //    lst_module.SelectedIndex = -1;
    //    txtUrl.Text = "";
    //    RadGrid1.Visible = true;
    //}
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            Label lblKey = e.Item.FindControl("lblKey") as Label;
            if (lblKey.Text == "Access_Menu") //your condition
            {
                item.Display = false;
            }
        }
    }
  
}
