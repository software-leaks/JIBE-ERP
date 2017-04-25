using System;
using System.Web;
using System.Data;
using System.Web.Services;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using SMS.Business.Infrastructure;
using System.Web.Script.Services;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;


/// <summary>
/// Summary description for JibeWebService
/// </summary>
[WebService(Namespace = "JibeWebServiceINF")]
//[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public partial class JibeWebServiceInfra : System.Web.Services.WebService
{

    BLL_Infra_MenuManagement objMenuBLL = new BLL_Infra_MenuManagement();
    BLL_Infra_UserCredentials objBllUser = new BLL_Infra_UserCredentials();
    BLL_Infra_Company objBllCompany = new BLL_Infra_Company();

    public JibeWebServiceInfra()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod]
    public string GetCompanyList()
    {
        try
        {

            return UDFLib.CreateHtmlTableFromDataTable(objBllCompany.Get_CompanyListMini(),
          new string[] { "", "" },
          new string[] { "company_name", "id" }, "");
        }

        catch
        {
            throw;
        }
    }


    [WebMethod]
    public string GetRoleList()
    {
        try
        {

            return UDFLib.CreateHtmlTableFromDataTable(objMenuBLL.Get_Role(),
          new string[] { "", "" },
          new string[] { "Role", "Role_Id" }, "");
        }

        catch
        {
            throw;
        }
    }


    [WebMethod]
    public string GetUserList(int CompanyID, string FilterText, int UserID)
    {
        try
        {

            return UDFLib.CreateHtmlTableFromDataTable(objBllUser.Get_UserList(CompanyID, FilterText, UserID),
            new string[] { "", "" },
            new string[] { "UserName", "UserID" }, "");
        }

        catch
        {
            throw;
        }
    }



    [WebMethod]
    public string GetUserDDL(int CompanyID, string FilterText, int UserID)
    {
        try
        {

            return UDFLib.CreateHtmlTableFromDataTable(objBllUser.Get_UserList(CompanyID, FilterText, UserID),
            new string[] { "", "" },
            new string[] { "UserName", "UserID" }, "");
        }

        catch
        {
            throw;
        }
    }


    public class UserList
    {
        public string CompanyID { get; set; }
        public string FilterText { get; set; }
        public string UserID { get; set; }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string AsyncGetUserList(List<UserList> UserList)
    {
        int CompanyID;
        string FilterText;
        int UserID;

        CompanyID = Convert.ToInt32(UserList[0].CompanyID);
        FilterText = UserList[0].FilterText;
        UserID = Convert.ToInt32(UserList[0].UserID);



        return DataTableToJSONWithJavaScriptSerializer(objBllUser.Get_UserList(CompanyID, FilterText, UserID));
    }

    public class CopyMenuFromUser
    {
        public string CopyFromUserID { get; set; }
        public string CopyToUserID { get; set; }
        public string AppendMode { get; set; }
        public string Selected_Mod_Code { get; set; }
        public string Created_By { get; set; }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void AsyncCopyMenuAccessFromUser(List<CopyMenuFromUser> CopyMenuFromUser)
    {
        try
        {

            int CopyFromUserID;
            int CopyToUserID;
            int AppendMode;
            int Selected_Mod_Code;
            int Created_By;

            if (CopyMenuFromUser != null)
            {
                for (int i = 0; i < CopyMenuFromUser.Count; i++)
                {

                    CopyFromUserID = Convert.ToInt32(CopyMenuFromUser[i].CopyFromUserID);
                    CopyToUserID = Convert.ToInt32(CopyMenuFromUser[i].CopyToUserID);
                    AppendMode = Convert.ToInt32(CopyMenuFromUser[i].AppendMode);
                    Selected_Mod_Code = Convert.ToInt32(CopyMenuFromUser[i].Selected_Mod_Code);
                    Created_By = Convert.ToInt32(CopyMenuFromUser[i].Created_By);

                    {
                        objMenuBLL.Copy_MenuAccessFromUser(CopyFromUserID, CopyToUserID, AppendMode, Selected_Mod_Code, Created_By);

                    }
                }
            }






        }


        catch
        {
            throw;
        }

    }

    public class CopyMenuFromRole
    {
        public string RoleId { get; set; }
        public string CopyToUserID { get; set; }
        public string AppendMode { get; set; }
        public string Selected_Mod_Code { get; set; }
        public string Created_By { get; set; }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void AsyncCopyMenuAccessFromRole(List<CopyMenuFromRole> CopyMenuFromRole)
    {
        try
        {

            int RoleID;
            int CopyToUserID;
            int AppendMode;
            int Selected_Mod_Code;
            int Created_By;

            if (CopyMenuFromRole != null)
            {
                for (int i = 0; i < CopyMenuFromRole.Count; i++)
                {

                    RoleID = Convert.ToInt32(CopyMenuFromRole[i].RoleId);
                    CopyToUserID = Convert.ToInt32(CopyMenuFromRole[i].CopyToUserID);
                    AppendMode = Convert.ToInt32(CopyMenuFromRole[i].AppendMode);
                    Selected_Mod_Code = Convert.ToInt32(CopyMenuFromRole[i].Selected_Mod_Code);
                    Created_By = Convert.ToInt32(CopyMenuFromRole[i].Created_By);

                    {
                        objMenuBLL.Copy_MenuAccessFromRole(RoleID, CopyToUserID, 0, 0, Created_By);

                    }
                }
            }






        }


        catch
        {
            throw;
        }

    }

    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    }

    [WebMethod]
    public DataTable BindchkDDLUser(int CompanyID, string FilterText, int UserID)
    {

        return objBllUser.Get_UserList(CompanyID, FilterText, UserID);
    }

    [WebMethod]
    public int CopyMenuAccessFromUser(int CopyFromUserID, int CopyToUserID, int AppendMode, int Selected_Mod_Code, int Created_By)
    {
        try
        {
            return objMenuBLL.Copy_MenuAccessFromUser(CopyFromUserID, CopyFromUserID, AppendMode, Selected_Mod_Code, Created_By);
        }

        catch
        {
            throw;
        }
    }


    public class UserMenu
    {

        public string UID { get; set; }

        public string MCode { get; set; }

        public string Menu { get; set; }

        public string View { get; set; }

        public string Add { get; set; }

        public string Edit { get; set; }

        public string Delete { get; set; }

        public string Approve { get; set; }

        public string Admin { get; set; }

        public string Unverify { get; set; }

        public string Revoke { get; set; }

        public string Unclose { get; set; }

        public string Urgent { get; set; }

        public string Close { get; set; }

        public string SessionUserID { get; set; }


    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public int UpdateUserMenu(List<UserMenu> UserMenu)
    {


        try
        {

            string UserID;
            string Menu_Code;
            string Menu;
            string View;
            string Add;
            string Edit;
            string Delete;
            string Approve;
            string Admin;
            string Unverify;
            string Revoke;
            string Unclose;
            string Urgent;
            string Close;
            string Created_By;



            DataTable dtUpdate = new DataTable();

            dtUpdate.Columns.Add("UserID");
            dtUpdate.Columns.Add("Menu_Code");
            dtUpdate.Columns.Add("Menu");
            dtUpdate.Columns.Add("View");
            dtUpdate.Columns.Add("Add");
            dtUpdate.Columns.Add("Edit");
            dtUpdate.Columns.Add("Delete");
            dtUpdate.Columns.Add("Approve");
            dtUpdate.Columns.Add("Admin");
            dtUpdate.Columns.Add("Unverify");
            dtUpdate.Columns.Add("Revoke");
            dtUpdate.Columns.Add("Unclose");
            dtUpdate.Columns.Add("Urgent");
            dtUpdate.Columns.Add("Close");
            dtUpdate.Columns.Add("Created_By");


            if (UserMenu != null)
            {
                for (int i = 1; i < UserMenu.Count; i++)
                {

                    UserID = UserMenu[i].UID;
                    Menu_Code = UserMenu[i].MCode;
                    Menu = UserMenu[i].Menu;
                    View = UserMenu[i].View;
                    Add = UserMenu[i].Add;
                    Edit = UserMenu[i].Edit;
                    Delete = UserMenu[i].Delete;
                    Approve = UserMenu[i].Approve;
                    Admin = UserMenu[i].Admin;
                    Unverify = UserMenu[i].Unverify;
                    Revoke = UserMenu[i].Revoke;
                    Unclose = UserMenu[i].Unclose;
                    Urgent = UserMenu[i].Urgent;
                    Close = UserMenu[i].Close;
                    Created_By = UserMenu[i].SessionUserID;


                    DataRow dr = dtUpdate.NewRow();
                    dr["UserID"] = UserID;
                    dr["Menu_Code"] = Menu_Code;
                    dr["Menu"] = Menu;
                    dr["View"] = View;
                    dr["Add"] = Add;
                    dr["Edit"] = Edit;
                    dr["Delete"] = Delete;
                    dr["Approve"] = Approve;
                    dr["Admin"] = Admin;
                    dr["Unverify"] = Unverify;
                    dr["Revoke"] = Revoke;
                    dr["Unclose"] = Unclose;
                    dr["Urgent"] = Urgent;
                    dr["Close"] = Close;
                    dr["Created_By"] = Created_By;
                    dtUpdate.Rows.Add(dr);



                }




            }

            return objMenuBLL.Update_User_Menu_Access_DL_New(dtUpdate);
        }


        catch
        {
            throw;
        }



    }




    [WebMethod]
    public DataSet GetUserMenuApproach(int mod_code, int userid, int LoginUser)
    {
        try
        {
            return objMenuBLL.Get_UserMenuApproach(mod_code, userid, LoginUser);
        }

        catch
        {
            throw;
        }
    }

    [WebMethod]
    public DataTable GetMenuAccess(int? User_Id, int? Menu_Id)
    {
        try
        {
            return objMenuBLL.Get_MenuAccess(User_Id, Menu_Id);

        }

        catch
        {
            throw;
        }
    }


    [WebMethod(EnableSession = true)]
    public string CreateTableMenu(int ListUserID, int ModId)
    {
        int mod_code = ModId;

        int lstuserid = ListUserID;
        StringBuilder strTable = new StringBuilder();

        strTable.Append("<div style='overflow: auto'><table id='tblmenu'  CELLPADDING='2' CELLSPACING='4' style='color:#333333;width:100%;border-collapse:collapse;'>");
        strTable.Append("<tbody>");
        strTable.Append("<tr style='color:White;background-color:#507CD1;font-weight:bold;' >");

        strTable.Append("<th>Menu Description</th>");
        strTable.Append("<th>Menu Link</th>");
        strTable.Append("<th align='center'>Select All</th>");
        strTable.Append("<th style='display:none;'>Menu_Id</th>");
        strTable.Append("<th>View</th>");
        strTable.Append("<th>Add</th>");
        strTable.Append("<th>Edit</th>");
        strTable.Append("<th>Delete</th>");
        strTable.Append("<th>Approve</th>");
        strTable.Append("<th>Admin</th>");
        strTable.Append("<th>Unverify</th>");
        strTable.Append("<th>Revoke</th>");
        strTable.Append("<th>Urgent</th>");
        strTable.Append("<th>Close</th>");
        strTable.Append("<th>Unclose</th>");

        strTable.Append("</tr>");

        DataTable dt = objMenuBLL.Get_UserMenuApproach(mod_code, lstuserid, Convert.ToInt32(Session["USERID"].ToString())).Tables[0];
        int inc = 0;



        if (dt.Rows.Count > 0)
        {

            foreach (DataRow dr in dt.Rows)
            {
                inc++;
                if (dr["Menu_Link"].ToString().ToLower() == "infrastructure/dashboard_common.aspx" || dr["Menu_Link"].ToString().ToLower() == "infrastructure/dashboard.aspx")
                {
                    strTable.Append("<tr style='background-color:#EFF3FB;'>");
                    strTable.Append("<td>");
                    strTable.Append("<label id = lblDesc_" + inc + " >" + dr["Menu_Short_Discription"] + "</label>");
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    strTable.Append("<label id= lblLink_" + inc + ">" + dr["Menu_Link"] + "</label>");
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    strTable.Append("<input id= chkAll_" + inc + " type= 'checkbox' align='center' style='width:50px;' disabled />");
                    strTable.Append("</td>");
                    strTable.Append("<td style='display:none;'>");
                    strTable.Append("<input type=hidden id=lblMenu_Id_" + inc + " value=" + dr["Menu_Code"] + " runat=server />");
                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id = chkAccess_View_" + inc + " type='checkbox' value='1' checked disabled />");

                    strTable.Append("</td>");
                    strTable.Append("<td>");


                    strTable.Append("<input id= chkAccess_Add_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id= chkAccess_Edit_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id = chkAccess_Delete_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id = chkAccess_Approve_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");


                    strTable.Append("<input id = chkAccess_Admin_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id = chkUnverify_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input  id= chkRevoke_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id= chkUrgent_" + inc + " type='checkbox'  value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id = chkClose_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    strTable.Append("<input id = chkUnclose_" + inc + " type='checkbox' value='1' checked disabled/>");

                    strTable.Append("</td>");
                    strTable.Append("</tr>");

                }
                else
                {
                    strTable.Append("<tr style='background-color:#EFF3FB;'>");
                    strTable.Append("<td>");
                    strTable.Append("<label id = lblDesc_" + inc + " >" + dr["Menu_Short_Discription"] + "</label>");
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    strTable.Append("<label id= lblLink_" + inc + ">" + dr["Menu_Link"] + "</label>");
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    strTable.Append("<input id= chkAll_" + inc + " type= 'checkbox' align='center' style='width:50px;' onchange='checkAll(this)' />");
                    strTable.Append("</td>");
                    strTable.Append("<td style='display:none;'>");
                    strTable.Append("<input type=hidden id=lblMenu_Id_" + inc + " value=" + dr["Menu_Code"] + " runat=server />");
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Access_View"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkAccess_View_" + inc + " type='checkbox' value='1' checked />");
                    }

                    else
                    {
                        strTable.Append("<input id = chkAccess_View_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    if (dr["Access_Add"].ToString() == "1")
                    {
                        strTable.Append("<input id= chkAccess_Add_" + inc + " type='checkbox' value='1' checked/>");
                    }
                    else
                    {
                        strTable.Append("<input id= chkAccess_Add_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Access_Edit"].ToString() == "1")
                    {
                        strTable.Append("<input id= chkAccess_Edit_" + inc + " type='checkbox' value='1' checked/>");
                    }
                    else
                    {
                        strTable.Append("<input id= chkAccess_Edit_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Access_Delete"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkAccess_Delete_" + inc + " type='checkbox' value='1' checked/>");
                    }

                    else
                    {
                        strTable.Append("<input id = chkAccess_Delete_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Access_Approve"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkAccess_Approve_" + inc + " type='checkbox' value='1' checked />");
                    }
                    else
                    {
                        strTable.Append("<input id = chkAccess_Approve_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");

                    if (dr["Access_Admin"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkAccess_Admin_" + inc + " type='checkbox' value='1' checked/>");
                    }
                    else
                    {
                        strTable.Append("<input id = chkAccess_Admin_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Unverify"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkUnverify_" + inc + " type='checkbox' value='1' checked />");
                    }
                    else
                    {
                        strTable.Append("<input id = chkUnverify_" + inc + " type='checkbox' value='0'  />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Revoke"].ToString() == "1")
                    {
                        strTable.Append("<input  id= chkRevoke_" + inc + " type='checkbox' value='1' checked/>");
                    }

                    else
                    {
                        strTable.Append("<input  id= chkRevoke_" + inc + " type='checkbox' value='0' />");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Urgent"].ToString() == "1")
                    {
                        strTable.Append("<input id= chkUrgent_" + inc + " type='checkbox'  value='1' checked/>");
                    }
                    else
                    {
                        strTable.Append("<input id= chkUrgent_" + inc + " type='checkbox'  value='0'/>");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Close"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkClose_" + inc + " type='checkbox' value='1' checked/>");
                    }
                    else
                    {
                        strTable.Append("<input id = chkClose_" + inc + " type='checkbox' value='0'/>");
                    }
                    strTable.Append("</td>");
                    strTable.Append("<td>");
                    if (dr["Unclose"].ToString() == "1")
                    {
                        strTable.Append("<input id = chkUnclose_" + inc + " type='checkbox' value='1' checked/>");
                    }
                    else
                    {
                        strTable.Append("<input id = chkUnclose_" + inc + " type='checkbox' value='0'/>");
                    }
                    strTable.Append("</td>");
                    strTable.Append("</tr>");


                }
            }
        }

        strTable.Append("</tbody>");
        strTable.Append("</table>");
        strTable.Append("</div>");


        return strTable.ToString();
    }

    public class FunctionalTree
    {

        public string id { get; set; }

        public string parent { get; set; }

        public string text { get; set; }

        public string icon { get; set; }

        public string value { get; set; }

    }
    [WebMethod(EnableSession = true)]
    [ScriptMethod(UseHttpGet = true)]
    public List<FunctionalTree> CreateTreeStructure(int UserID)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        List<FunctionalTree> NH = new List<FunctionalTree>();
        try
        {

            DataTable dtMenu = null;

            dtMenu = objBllUser.Get_Menu_Lib_Access(UserID);
            FunctionalTree nh = new FunctionalTree();

            int meni = 0;
            int child = 0;
            DataRow[] drs = dtMenu.Select("[Parent] is null");

            foreach (DataRow dr in drs)
            {
                nh = new FunctionalTree();
                nh.id = Convert.ToString(dr["id"]);


                nh.parent = Convert.ToString(dr["parent"]);
                nh.text = Convert.ToString(dr["text"]);
                nh.icon = Convert.ToString(dr["icon"]);
                nh.value = Convert.ToString(dr["value"]);
                NH.Add(nh);

                DataRow[] drInners = dtMenu.Select("[Parent] ='" + dr["ID"].ToString() + "' ");
                if (drInners.Length != 0)
                {

                    foreach (DataRow drInner in drInners)
                    {
                        FunctionalTree nhChild = new FunctionalTree();
                        nhChild.id = Convert.ToString(drInner["id"]);


                        nhChild.parent = Convert.ToString(drInner["parent"]);
                        nhChild.text = Convert.ToString(drInner["text"]);
                        nhChild.icon = Convert.ToString(drInner["icon"]);
                        nhChild.value = Convert.ToString(drInner["value"]);
                        NH.Add(nhChild);

                        string filter = "[Parent] = '" + drInner["ID"].ToString() + "'";

                        dtMenu.AcceptChanges();
                        DataRow[] drInnerLinks = dtMenu.Select(filter);

                        if (drInnerLinks.Length != 0)
                        {
                            child = 0;
                            foreach (DataRow drInnerLink in drInnerLinks)
                            {
                                FunctionalTree nhChildnodes = new FunctionalTree();
                                nhChildnodes.id = Convert.ToString(drInnerLink["id"]);


                                nhChildnodes.parent = Convert.ToString(drInnerLink["parent"]);
                                nhChildnodes.text = Convert.ToString(drInnerLink["text"]);
                                nhChildnodes.icon = Convert.ToString(drInnerLink["icon"]);
                                nhChildnodes.value = Convert.ToString(drInnerLink["value"]);
                                NH.Add(nhChildnodes);
                            }
                            child++;
                        }
                        meni++;
                    }


                }

            }
        }

        catch (Exception ex)
        {
            string s = ex.Message;
        }
        return NH;
    }

}


