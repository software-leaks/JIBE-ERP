using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

using SMS.Business.Infrastructure;
using SMS.Business.OCAAdmin;
using System.Text;
using System.IO;
using AjaxControlToolkit4;
using System.Drawing;




public partial class FileUpload : System.Web.UI.Page
{

    BLL_Infobase objINFO = new BLL_Infobase();
    string strConn = null;

   
    protected void Page_Load(object sender, EventArgs e)
    
    {
        
        //Button1.Visible = false;
        //strConn = ConfigurationManager.ConnectionStrings["smsconn"].ConnectionString;
        try
        {
           
            Label1.Text = "";
            if (!IsPostBack)
            {
                UserAccessValidation();
                if (Session["USERID"] != null)
                {
               
                    LoadTree();
                    Panel1.Visible = false;
                    //bindGrid();
                   Session["Filename"] = null;
                    Session["extension"] = null;
                   Session["OriginalFile"] = null;
                }

            }
            else {
                PostbackData();
            }
            
          
        }
        catch (Exception ex)
        {
            string js = "alert('" + ex.Message + "')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", js, true);
        }

    }

    protected void UserAccessValidation()
    {
        int CurrentUserID = GetSessionUserID();
        string PageURL = UDFLib.GetPageURL(Request.Path.ToUpper());

        SMS.Properties.UserAccess objUA = new SMS.Properties.UserAccess();
        BLL_Infra_UserCredentials objUser = new BLL_Infra_UserCredentials();

        objUA = objUser.Get_UserAccessForPage(CurrentUserID, PageURL);

            //if (objUA.View == 0)
            //    Response.Redirect("~/default.aspx?msgid=1");

    }
    private int GetSessionUserID()
    {
        if (Session["USERID"] != null)
            return int.Parse(Session["USERID"].ToString());
        else
            return 0;
    }
  
    private void LoadTree()
    {
        try
        {
            DataTable dtDepartment = objINFO.GET_UserDeptmentList(GetSessionUserID());
            TreeNode L0 = new TreeNode("Folders", "1");
            treeDeptFolders.Nodes.Add(L0);
            if (dtDepartment.Rows.Count > 0)
            {
                int nodeIndex = 0;


                foreach (DataRow dr in dtDepartment.Rows)
                {
                    TreeNode L1 = new TreeNode(dr["VALUE"].ToString(), dr["ID"].ToString());

                    L0.ChildNodes.Add(L1);

                    int Dept_Id = UDFLib.ConvertToInteger(dr["ID"]);
                    DataTable dtFlderList = objINFO.GET_UserFolderList(GetSessionUserID(), Dept_Id);
                    if (dtFlderList.Rows.Count > 0)
                    {
                        foreach (DataRow drFolder in dtFlderList.Rows)
                        {
                            TreeNode inner;
                            inner = new TreeNode(drFolder["FOLDER_NAME"].ToString(), drFolder["ID"].ToString());
                            L1.ChildNodes.Add(inner);
                          //  treeDeptFolders.Nodes[nodeIndex].CollapseAll();
                            nodeIndex++;
                        }
                    }

                }

            }
            treeDeptFolders.CollapseAll();
        }
        catch
        {
        }


    }


    protected void btnInitializeMenu_Click(object sender, EventArgs e)
    {
        //if (lstUserList.SelectedValue == "" || lstModuleList.SelectedValue == "")
        //return;

        //int iMenu = 0;
        //int iView = 0;
        //int iAdd = 0;
        //int iEdit = 0;
        //int iDelete = 0;
        //int iApprove = 0;
        //int iMod_Code = 0;

        //iMod_Code = int.Parse(lstModuleList.SelectedValue);

        //objMenuBLL.Initialize_User_Menu(int.Parse(lstUserList.SelectedValue), iMod_Code, iMenu, iView, iAdd, iEdit, iDelete, iApprove, int.Parse(getSessionString("USERID")));

        //GridView1.DataBind();
    }


    /// <summary>
    /// Show tree view path
    /// </summary>
    /// 
  
    /// <summary>
    /// TreeView SelectedNodeChange event.Genearate Dynamic textbox and dropdownlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    int rc, cc = 0;
    int noOfControls = 0;
    List<string> listText;
    List<string> listDDL;
    string[] txtArray = null;
    string[] ddlArray = null;
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            Label1.Text = "";
            Panel1.Visible = false;
            Button1.Visible = false;
            PlaceHolder1.Controls.Clear();
            primaryDDL.Items.Clear();
            pnlTitle.Visible = false;
            pnlDDL.Visible = false;
            TreeNode node = this.treeDeptFolders.SelectedNode;
            ViewState["node"] = node.Value;
            ViewState["Parent"] = node.Parent.Value;
            DataSet dset=objINFO.Get_AssignedAttributes(Convert.ToInt32(node.Value));
            DataTable bindTable =dset.Tables[0];
            
            int rowscount = bindTable.Rows.Count;
            int colscount = bindTable.Columns.Count;
            LiteralControl lc = new LiteralControl("<table>");
            PlaceHolder1.Controls.Add(lc); 
            listText = new List<string>();
            listDDL = new List<string>();
            if (bindTable.Rows.Count == 0 || bindTable.Columns.Count == 0)
            {
                Panel1.Visible = false;
                Button1.Visible = false;
            }
            for (int r = 0; r < rowscount; r++)
            {
                DataSet dsOwner = objINFO.GET_UserRightDetails(Convert.ToInt32(node.Parent.Value), Convert.ToInt32(node.Value));
                var IsOwner = (from c in dsOwner.Tables[1].AsEnumerable()
                               where c.Field<int>("User_ID") ==Convert.ToInt32( Session["USERID"])
                                  select c).FirstOrDefault();
                if(Convert.ToBoolean( IsOwner["IsOwner"])==true)
                {
                    Panel1.Visible = true;
                    Button1.Visible = true;

                }
                pnlTitle.Visible = true;
                pnlDDL.Visible = true;
                txtDescription.Text = "";
                txtTitle.Text = "";
                for (int c = 0; c < colscount; c++)
                {

                    if (c == 2)
                    {
                        Label lb = new Label();
                        lb.ID = (r.ToString() + c.ToString());
                        lb.Text = bindTable.Rows[r][c].ToString();
                        LiteralControl literalBreak = new LiteralControl("<tr>");
                        PlaceHolder1.Controls.Add(literalBreak);
                        LiteralControl l0 = new LiteralControl("<td width=\"50%\">");
                        PlaceHolder1.Controls.Add(l0);
                        PlaceHolder1.Controls.Add(lb);
                        LiteralControl l1 = new LiteralControl("</td>");
                        PlaceHolder1.Controls.Add(l1);
                        noOfControls++;
                    }


                    if (c == 3)
                    {
                        if (bindTable.Rows[r][c].ToString().ToLower() == "list")
                        {
                           
                            string query = bindTable.Rows[r][5].ToString().ToLower();
                            DropDownList DDL = new DropDownList();
                            DDL.ID = (r.ToString() + c.ToString());
                            DDL.AutoPostBack = false;
                            DDL.Width = 150;
                            DDL.Attributes.Add("runat", "Server");
                            LiteralControl l2 = new LiteralControl("<td  width=\"50%\">");
                            PlaceHolder1.Controls.Add(l2);
                            PlaceHolder1.Controls.Add(DDL);
                            LiteralControl l3 = new LiteralControl("</td>");
                            PlaceHolder1.Controls.Add(l3);
                            LiteralControl l4 = new LiteralControl("</tr>");
                            PlaceHolder1.Controls.Add(l4);
                            DataTable dtBind = objINFO.Info_ExecuteQuery(query);
                            DDL.DataSource = dtBind;
                            DDL.DataTextField = dtBind.Columns[1].ToString();//supplier_name
                            DDL.DataValueField = dtBind.Columns[0].ToString();//supplier_id
                            DDL.DataBind();
                            DDL.Items.Insert(0, "---Select---");
                            listDDL.Add(DDL.ID);
                            noOfControls++;
                        }
                        else
                        {
                            TextBox tb = new TextBox();
                            tb.ID = (r.ToString() + c.ToString());
                            tb.Attributes.Add("runat", "Server");
                            LiteralControl l5 = new LiteralControl("<td  width=\"50%\">");
                            PlaceHolder1.Controls.Add(l5); 
                            PlaceHolder1.Controls.Add(tb);
                            LiteralControl l6 = new LiteralControl("</td>");
                            PlaceHolder1.Controls.Add(l6);
                            LiteralControl l7 = new LiteralControl("</tr>");
                            PlaceHolder1.Controls.Add(l7);
                            listText.Add(tb.ID);
                            noOfControls++;

                        }
                     
                    }
                     
                   
                  
                }
                ViewState["count"] = noOfControls;
                txtArray = listText.ToArray();
                ddlArray=listDDL.ToArray();
                ViewState["txtArray"] = txtArray;
                ViewState["DDLArray"] = ddlArray;
               
            }
            Button1.Visible = true;
            pnlTitle.Visible = true;
            pnlDDL.Visible = true;
            Panel1.Visible = true;
            DataTable dtBindDDL = dset.Tables[1];

            string dtName = null;
            string dv = null;
            string dx = null;
            foreach (DataRow drr in dtBindDDL.Rows)
            {
                dtName = drr["Command_SQL"].ToString();
            }
            lblPrimary.Text = dtBindDDL.Rows[0]["Link_DisplayName"].ToString();
            dx = dtBindDDL.Rows[0]["Link_Value_Field"].ToString();
            dv = dtBindDDL.Rows[0]["Link_Text_Field"].ToString();
            try
            {
                DataTable dtBind2 = objINFO.Info_ExecuteQuery(dtName);
                if (dtBind2.Rows.Count > 0)
                {
                    primaryDDL.DataSource = dtBind2;
                    primaryDDL.DataTextField = dv;
                    primaryDDL.DataValueField = dx;
                    primaryDDL.DataBind();
                    primaryDDL.Items.Insert(0, "---Select---");
                }
            }
            catch
            {
            }
            LiteralControl le = new LiteralControl("</table>");
            PlaceHolder1.Controls.Add(le);
            bindGrid();
        }
        catch
        {
        }
    }
    /// <summary>
    /// Read Data from dynamic controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (primaryDDL.SelectedIndex != 0)
            {
                if (HttpContext.Current.Session["Filename"] != null)
                {
                    List<string> UpdateList = new List<string>();
                    string[] UpdateArray = null;
                    Label1.Text = "";
                    int i = 0;
                    int j = 0;
                    string controlid = null;
                    foreach (Control c in PlaceHolder1.Controls)
                    {
                        if (c is TextBox)
                        {
                            string[] arr = (string[])ViewState["txtArray"];
                            controlid = arr[i].ToString();
                            TextBox tb = PlaceHolder1.FindControl(controlid) as TextBox;
                            if (tb != null)
                            {
                                // Label1.Text += tb.Text;
                                UpdateList.Add(tb.Text);
                            }
                            i++;
                        }
                        if (c is DropDownList)
                        {
                            string[] arr = (string[])ViewState["DDLArray"];
                            controlid = arr[j].ToString();
                            DropDownList tb = PlaceHolder1.FindControl(controlid) as DropDownList;
                            if (tb != null)
                            {
                                //   Label1.Text += tb.SelectedItem.ToString();
                                UpdateList.Add(tb.SelectedItem.Text);
                            }
                            j++;
                        }

                    }
                    UpdateArray = UpdateList.ToArray();
                    DataTable dtAttrib = objINFO.Get_AssignedAttributes(Convert.ToInt32(ViewState["node"])).Tables[0].DefaultView.ToTable(false, "Attribute_ID");
                    DataColumn newColumn = new DataColumn("AttributeValue", typeof(System.String));
                    newColumn.AllowDBNull = true;
                    dtAttrib.Columns.Add(newColumn);
                    int k = 0;
                    foreach (DataRow row in dtAttrib.Rows)
                    {
                        row["AttributeValue"] = UpdateArray[k];
                        k++;
                    }
                    string savePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Infobase\\");
                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                    }
                    string fileName =Session["Filename"].ToString();
                    FileInfo fi = new FileInfo(savePath + fileName);
                    double size = fi.Length;
                    string title = txtTitle.Text;
                    string desc = txtDescription.Text;
                    string linkvalue = primaryDDL.SelectedValue.ToString();
                    //     string res = objINFO.Insert_UploadedFiles(Convert.ToInt32(ViewState["node"]),Session["OriginalFile"].ToString(), Session["extension"].ToString(), (int)(size / 1024f), dtAttrib, Convert.ToInt32(Session["USERID"]));
                    string res = objINFO.Insert_UploadedFiles(Convert.ToInt32(ViewState["node"]),Session["OriginalFile"].ToString(), Session["extension"].ToString(), (int)(size / 1024f),linkvalue, desc,title, dtAttrib, Convert.ToInt32(Session["USERID"]));
                    res = res.PadLeft(8, '0');
                    string F1 = Mid(res, 0, 2);
                    string F2 = Mid(res, 2, 2);
                    string F3 = Mid(res, 4, 2);
                    if (!Directory.Exists(savePath + F1 + "\\" + F2 + "\\" + F3))
                    {
                        Directory.CreateDirectory(savePath + F1 + "\\" + F2 + "\\" + F3);
                    }

                    // File.Move(savePath + fileName, savePath.Replace(fileName, "") + res + Session["extension"].ToString());
                    File.Move(savePath + fileName, savePath + F1 + "\\" + F2 + "\\" + F3 + "\\" + res + Session["extension"].ToString());
                    bindGrid();
                   Session["Filename"] = null;
                    Session["extension"] = null;
                   Session["OriginalFile"] = null;

                    i = 0;
                    j = 0;
                    foreach (Control c in PlaceHolder1.Controls)
                    {
                        if (c is TextBox)
                        {
                            string[] arr = (string[])ViewState["txtArray"];
                            controlid = arr[i].ToString();
                            TextBox tb = PlaceHolder1.FindControl(controlid) as TextBox;
                            if (tb != null)
                            {
                                tb.Text = "";
                            }
                            i++;
                        }
                        if (c is DropDownList)
                        {
                            string[] arr = (string[])ViewState["DDLArray"];
                            controlid = arr[j].ToString();
                            DropDownList tb = PlaceHolder1.FindControl(controlid) as DropDownList;
                            if (tb != null)
                            {
                                tb.SelectedIndex = 0;
                            }
                            j++;
                        }

                    } Label1.Visible = true;
                    Label1.Text = "File has been successfully uploaded";
                    Label1.ForeColor = Color.Blue;
                    txtTitle.Text = "";
                    txtDescription.Text = "";
                    // #3498DB
                }
                else
                {
                    Label1.Visible = true;
                    Label1.Text = "***Please add an attachment";
                    Label1.ForeColor = Color.Red;
                }
            }
            else
            {
                Label1.Text = "Select from "+lblPrimary.Text;
                Label1.ForeColor = Color.Red;
                Session["Filename"] = null;
            }
        }
        catch { }
     
    }
   

    public static string Mid(string param, int startIndex, int length)
    {
        string result = param.Substring(startIndex, length);
        return result;
    }
    //void SaveFile(HttpPostedFile file)
    //{
    //    string savePath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Infobase\\");
    //    string fileName = FileUpload1.FileName;
    //    string pathToCheck = savePath + fileName;
    //    savePath += fileName;
    //    FileUpload1.SaveAs(savePath);
    //}



    /// <summary>
    /// Maintain the state for treeview controls
    /// </summary>
    protected void PostbackData()
    {

        try
        {
            if (ViewState["node"] != null)
            {
              
                PlaceHolder1.Controls.Clear();
                pnlTitle.Visible = false;
                pnlDDL.Visible = false;
                DataSet dset = objINFO.Get_AssignedAttributes(Convert.ToInt32(ViewState["node"]));
                DataTable bindTable = dset.Tables[0];
            
                int rowscount = bindTable.Rows.Count;
                int colscount = bindTable.Columns.Count;
                LiteralControl lc = new LiteralControl("<table>");
                PlaceHolder1.Controls.Add(lc); 
                for (int r = 0; r < rowscount; r++)
                {
                    DataSet dsOwner = objINFO.GET_UserRightDetails(Convert.ToInt32(ViewState["parent"]), Convert.ToInt32(ViewState["node"]));
                    var IsOwner = (from c in dsOwner.Tables[1].AsEnumerable()
                                   where c.Field<int>("User_ID") == Convert.ToInt32(Session["USERID"])
                                   select c).FirstOrDefault();
                    if (Convert.ToBoolean(IsOwner["IsOwner"]) == true)
                    {
                        Panel1.Visible = true;
                        Button1.Visible = true;

                    }
                    for (int c = 0; c < colscount; c++)
                    {
                        if (c == 3)
                        {
                            if (bindTable.Rows[r][c].ToString().ToLower() == "list")
                            {
                                string query = bindTable.Rows[r][5].ToString().ToLower();
                                DropDownList DDL = new DropDownList();
                                DDL.ID = (r.ToString() + c.ToString());
                                DDL.AutoPostBack = false;
                                DDL.Width = 150;
                                DDL.Attributes.Add("runat", "Server");
                                LiteralControl l2 = new LiteralControl("<td width=\"50%\">");
                                PlaceHolder1.Controls.Add(l2);
                                PlaceHolder1.Controls.Add(DDL);
                                LiteralControl l3 = new LiteralControl("</td>");
                                PlaceHolder1.Controls.Add(l3);
                                LiteralControl l4 = new LiteralControl("</tr>");
                                PlaceHolder1.Controls.Add(l4);
                                DataTable dtBind = objINFO.Info_ExecuteQuery(query);
                                DDL.DataSource = dtBind;
                                DDL.DataTextField = dtBind.Columns[1].ToString();
                                DDL.DataValueField = dtBind.Columns[0].ToString();
                                DDL.DataBind();
                                DDL.Items.Insert(0, "---Select---");
                                noOfControls++;
                            }
                            else
                            {
                                TextBox tb = new TextBox();
                                tb.ID = (r.ToString() + c.ToString());
                                tb.Attributes.Add("runat", "Server");
                                LiteralControl l5 = new LiteralControl("<td width=\"50%\">");
                                PlaceHolder1.Controls.Add(l5);
                                PlaceHolder1.Controls.Add(tb);
                                LiteralControl l6 = new LiteralControl("</td>");
                                PlaceHolder1.Controls.Add(l6);
                                LiteralControl l7 = new LiteralControl("</tr>");
                                PlaceHolder1.Controls.Add(l7);
                                noOfControls++;
                            }
                        }
                        else if (c == 2)
                        {
                            Label lb = new Label();
                            lb.ID = (r.ToString() + c.ToString());
                            lb.Text = bindTable.Rows[r][c].ToString();
                            LiteralControl literalBreak = new LiteralControl("<tr>");
                            PlaceHolder1.Controls.Add(literalBreak);
                            LiteralControl l0 = new LiteralControl("<td width=\"50%\">");
                            PlaceHolder1.Controls.Add(l0);
                            PlaceHolder1.Controls.Add(lb);
                            LiteralControl l1 = new LiteralControl("</td>");
                            PlaceHolder1.Controls.Add(l1);
                            noOfControls++;
                        }
                        Button1.Visible = true;
                        pnlTitle.Visible = true;
                        pnlDDL.Visible = true;
                    }
                    ViewState["count"] = noOfControls;
                    
                }
                Button1.Visible = true;
                pnlTitle.Visible = true;
                pnlDDL.Visible = true;
                LiteralControl le = new LiteralControl("</table>");
                PlaceHolder1.Controls.Add(le);
                bindGrid();
            }

        }
        catch
        {
        }
    }

    /// <summary>
    /// Ajax File Upload save as Guid and store file info in sessions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="file"></param>

    protected void AjaxFileUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs file)
    {
        try
        {
            Byte[] fileBytes = file.GetContents();
            string sPath = Server.MapPath("\\" + System.Configuration.ConfigurationManager.AppSettings["APP_NAME"].ToString() + "\\uploads\\Infobase");
            Guid GUID = Guid.NewGuid();
            string Flag_Attach = GUID.ToString() + Path.GetExtension(file.FileName);
            string FullFilename=  Path.Combine(sPath, GUID.ToString() + Path.GetExtension(file.FileName));
            FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite);
            fileStream.Write(fileBytes, 0, fileBytes.Length);
            fileStream.Close();
           Session["Filename"] = Flag_Attach;
            Session["extension"] = Path.GetExtension(file.FileName);
           Session["OriginalFile"] = Path.GetFileName(file.FileName);
           Label1.Visible = false ;
        

        }
        catch (Exception ex)
        {

        }

    }

   

    public void lbtnPreview_Click(object sender,EventArgs e)
    {
         string sPath =  "../Uploads/Infobase/";
        string crewDocPath = ((LinkButton)sender).CommandArgument;
        string F1 = Mid(crewDocPath, 0, 2);
        string F2 = Mid(crewDocPath, 2, 2);
        string F3 = Mid(crewDocPath, 4, 2);
        string filename = ((LinkButton)sender).Text;
        string extension = Path.GetExtension(sPath + F1 + "//" + F2 + "//" + F3 + "//" + filename);
        string js = "previewDocument('" +sPath+ F1 + "/" + F2 + "/" + F3 + "/"+crewDocPath +extension+ "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "loadfile", js, true);
       
        //Display Data

        //int i = 0;
        //       int j = 0;
        //string controlid=null;
        //foreach (Control c in PlaceHolder1.Controls)
        //{
        //    if (c is TextBox)
        //    {
        //        string[] arr = (string[])ViewState["txtArray"];
        //        controlid = arr[i].ToString();
        //        TextBox tb = PlaceHolder1.FindControl(controlid) as TextBox;
        //        if (tb != null)
        //        {
        //            tb.Text = "jibe";
        //        }
        //        i++;
        //    }
        //    if (c is DropDownList)
        //    {
        //        string[] arr = (string[])ViewState["DDLArray"];
        //        controlid = arr[j].ToString();
        //        DropDownList tb = PlaceHolder1.FindControl(controlid) as DropDownList;
        //        if (tb != null)
        //        {
        //            tb.SelectedIndex = 5;
        //        }
        //        j++;
        //    }
        //}

    }

    public void btnSearch_Click(object sender,EventArgs e)
    {
        bindGrid();
        txtSearchText.Text = "";
    }


   

    public void bindGrid()
    {

        int rowcount = ucCustomPagerItems.isCountRecord;

        string sortbycoloumn = (ViewState["SORTBYCOLOUMN"] == null) ? null : (ViewState["SORTBYCOLOUMN"].ToString());
        int? sortdirection = null; if (ViewState["SORTDIRECTION"] != null) sortdirection = Int32.Parse(ViewState["SORTDIRECTION"].ToString());
        DataTable dtAttFile = new DataTable();
        string search = txtSearchText.Text;
        dtAttFile = objINFO.Get_Files(Convert.ToInt32(ViewState["node"]), search, null, null, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        //  dtAttFile = objINFO.Get_Files(8, search, null, null, ucCustomPagerItems.CurrentPageIndex, ucCustomPagerItems.PageSize, ref  rowcount);
        if (ucCustomPagerItems.isCountRecord == 1)
        {
            ucCustomPagerItems.CountTotalRec = rowcount.ToString();
            ucCustomPagerItems.BuildPager();
        }

       
            GridView1.DataSource = dtAttFile;
            GridView1.DataBind();
        


    }
}