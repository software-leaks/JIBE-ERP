using System;
using System.Web;
using System.Data;
using System.Web.Services;
using SMS.Data.Infrastructure;
using SMS.Business.PURC;
using System.Collections.Generic;
using SMS.Data.PURC;
using System.Text;


public partial class JibeWebService_Purc
{

    public class DepartmentType
    {
        public string SHORT_CODE { get; set; }
        public string DESCRIPTION { get; set; }


    }
    public class PURCDepartment
    {
        public string ID { get; set; }
        public string DEPT_NAME { get; set; }
        public string CODE { get; set; }
        public string FORM_TYPE { get; set; }

    }

    [WebMethod]
    public int PURC_Insert_AutoRequision(int Company_Code, bool IsAutoReqsn, Boolean IsReqSupplier_Confirm)
    {
        using (BLL_PURC_Purchase objBl = new BLL_PURC_Purchase())
        {
            int a = objBl.INSERT_AUTOMATIC_REQUISTION(Company_Code, IsAutoReqsn, IsReqSupplier_Confirm);
            return a;
        }
    }

    [WebMethod]
    public int PURC_Get_AutoRequision(int Company_Code)
    {
        using (BLL_PURC_Purchase objBl = new BLL_PURC_Purchase())
        {
            DataTable dt = objBl.GET_AUTOMATIC_REQUISTION(Company_Code);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["Is_Auto_Requisition"]);
            }
            else
            {
                return 0;
            }

        }
    }
    public List<DepartmentType> DeptType { get; set; }
    public List<PURCDepartment> Dept { get; set; }



    [WebMethod]
    public List<DepartmentType> PURC_Get_DeptType()
    {



        using (BLL_PURC_Purchase objBl = new BLL_PURC_Purchase())
        {
            DeptType = new List<DepartmentType>();
            DataTable dt = objBl.GetDeptType();
            dt = dt.DefaultView.ToTable();
            try
            {
                if (dt != null)
                {

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DeptType.Add(new DepartmentType()
                            {
                                SHORT_CODE = dr["Short_Code"].ToString(),
                                DESCRIPTION = dr["Description"].ToString(),

                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        return DeptType;
    }
    [WebMethod]
    public List<PURCDepartment> PURC_Get_Dept(string DeptType, string DeptTypeVal)
    {



        using (BLL_PURC_Purchase objBl = new BLL_PURC_Purchase())
        {
            Dept = new List<PURCDepartment>();

            DataTable dtDepartment = new DataTable();
            dtDepartment = objBl.SelectDepartment();

            DataView dv = new DataView();
            try
            {
                if (DeptTypeVal != null)     //instead of DeptType, using DeptTypeVal in each if condition because DeptType might be change
                {


                    if (DeptTypeVal == "SP")
                    {

                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + DeptTypeVal + "'";
                        dv = dtDepartment.DefaultView;
                    }
                    else if (DeptTypeVal == "ST")
                    {
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + DeptTypeVal + "'";
                        dv = dtDepartment.DefaultView;
                    }
                    else if (DeptTypeVal == "RP")
                    {
                        dtDepartment.DefaultView.RowFilter = "Form_Type='" + DeptTypeVal + "'";
                        dv = dtDepartment.DefaultView;

                    }
                }
                if (dv.ToTable().Rows != null)
                {

                    if (dv.ToTable().Rows.Count > 0)
                    {
                        foreach (DataRow dr in dv.ToTable().Rows)
                        {
                            Dept.Add(new PURCDepartment()
                            {
                                ID = dr["ID"].ToString(),
                                DEPT_NAME = dr["name_dept"].ToString(),
                                CODE = dr["code"].ToString(),
                                FORM_TYPE = dr["Form_Type"].ToString()

                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        return Dept;
    }
    [WebMethod]
    public DataTable Get_Pending_Reqsn(int User_ID)
    {
        return DAL_Infra_DashBoard.Get_Pending_Reqsn_DL(User_ID);
    }

    [WebMethod]
    public string Get_Reqsn_Processing_Time_BY_Reqsn(string Requisition_Code)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_PURC_Report.GET_Reqsn_Processing_Time_BY_Reqsn(Requisition_Code),
             new string[] { "Quotation", "Approval", "Raise PO", "Delivery" },
             new string[] { "QTN_TIME", "APP_TIME", "PO_TIME", "DLV_TIME" },

             new string[] { "center", "center", "center", "center" },
            "Processing Time(in days)");
    }

    [WebMethod]
    public int asyncUPD_Item_Quantity(int Vessel_ID, string Item_Ref_Code, int ID, decimal Min_Qty, decimal Max_Qty, string Effective_Date, int User_ID)
    {

        return BLL_PURC_Report.UPD_Item_Quantity(Vessel_ID, Item_Ref_Code, ID, Min_Qty, Max_Qty, Effective_Date, User_ID);
    }

    [WebMethod]
    public string asyncGet_Inventory_UpdatedBy(int ID, int Office_ID, int Vessel_ID)
    {

        UDCHyperLink aLink = new UDCHyperLink("", "../Crew/CrewDetails.aspx", new string[] { "ID" }, new string[] { "ID" }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("Staff_Code", aLink);

        return UDFLib.CreateHtmlTableFromDataTable(BLL_PURC_Report.Get_Inventory_UpdatedBy(ID, Office_ID, Vessel_ID),
           new string[] { "Staff Code", "Name", "Rank", "Updated On" },
           new string[] { "Staff_Code", "Staff_Name", "Rank_Short_Name", "UpdatedOn" },
           dicLink,
           new Dictionary<string, UDCToolTip>(),
           new string[] { "center", "left", "left", "left" },
           "CreateHtmlTableFromDataTable-table",
           "CreateHtmlTableFromDataTable-DataHedaer",
           "CreateHtmlTableFromDataTable-Data");

    }


    [WebMethod]
    public string async_GetToopTipsForQtnRecve(string DocumentCode)
    {
        return UDFLib.CreateHtmlTableFromDataTable(DAL_PURC_Report.GetToopTipsForQtnRecve(DocumentCode),
             new string[] { "S.N.", "Supplier" },
             new string[] { "RNUM", "SHORT_NAME" },

             new string[] { "center", "left" },
            "Quotation(s) Received");
    }


    [WebMethod]
    public string async_GetToopTipsForQtnSent(string DocumentCode)
    {
        return UDFLib.CreateHtmlTableFromDataTable(DAL_PURC_Report.GetToopTipsForQtnSent(DocumentCode),
             new string[] { "S.N.", "Supplier" },
             new string[] { "RNUM", "SHORT_NAME" },

             new string[] { "center", "left" },
            "Quotation(s) Sent");
    }

    [WebMethod]
    public string async_GetToopTipsForQtnINProgress(string DocumentCode)
    {
        return UDFLib.CreateHtmlTableFromDataTable(DAL_PURC_Report.GetToopTipsForQtnINProgress(DocumentCode),
             new string[] { "S.N.", "Supplier" },
             new string[] { "RNUM", "SHORT_NAME" },

             new string[] { "center", "left" },
            "Quotation(s) in Progress");
    }


    [WebMethod]
    public string async_Get_ToopTipsForQtnDeclined(string DocumentCode)
    {
        return UDFLib.CreateHtmlTableFromDataTable(DAL_PURC_Report.Get_ToopTipsForQtnDeclined(DocumentCode),
             new string[] { "S.N.", "Supplier" },
             new string[] { "RNUM", "SHORT_NAME" },

             new string[] { "center", "left" },
            "Declined by Supplier(s)");
    }



    [WebMethod]
    public string async_GetReqsn_Remarks(string DocumentCode, int RemarkType)
    {
        return UDFLib.CreateHtmlTableFromDataTable(BLL_PURC_Common.GET_Remarks(DocumentCode, RemarkType),
             new string[] { "Name", "Type", "Date", "Remark" },
             new string[] { "name", "Stage", "dateCr", "Remark" },

             new string[] { "left", "left", "left", "left" },
            null);
    }

    [WebMethod]
    public string async_Upd_Provisions_Approval_Limit(string Item_Ref_Code, decimal Max_Qty, decimal Max_Cost, int UserID, int Vessel_ID)
    {
        return BLL_PURC_Common.Upd_Provisions_Approval_Limit(Item_Ref_Code, Max_Qty, Max_Cost, UserID, Vessel_ID).ToString();
    }

    [WebMethod]
    public string Get_Check_Provision_Limit(string Document_Code)
    {
        DataTable dt = BLL_PURC_Common.Get_Check_Provision_Limit(Document_Code);
        StringBuilder str = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {

            if (str.Length > 0)
                str.Append(",");

            str.Append(dr["ITEM_REF_CODE"]);

        }

        return str.ToString();
    }
    /*Provision Library Catalogue IsMeat (Addes by Pranali_14042015)*/
    [WebMethod]
    public int UpdateIsMeat(string ID, int isMeat, int USERID)
    {
        BLL_PURC_Common.UpdateIsMeat(ID, isMeat, USERID);
        return 1;

    }
    [WebMethod]
    public int UpdateSlopChestItems(string id, int isSlopChest, int UserID)
    {
        BLL_PURC_Common.UpdateSlopChestItems(id, Convert.ToBoolean(isSlopChest), UserID);
        return 1;

    }

    [WebMethod]
    public string async_Purc_Supplier_Attachments(string Reqsnno, int vesselID, string SuppCode)
    {
        BLL_PURC_Purchase objPurch = new BLL_PURC_Purchase();

        UDCHyperLink aLink = new UDCHyperLink("File_Path", "../Uploads/Purchase", new string[] { }, new string[] { }, "_blank");
        Dictionary<string, UDCHyperLink> dicLink = new Dictionary<string, UDCHyperLink>();
        dicLink.Add("File_Name", aLink);

        return UDFLib.CreateHtmlTableFromDataTableNew(objPurch.Purc_Get_Reqsn_Attachments_Supplier(Reqsnno, vesselID, SuppCode),
           new string[] { },
           new string[] { "File_Name" },
           dicLink,
            new Dictionary<string, UDCToolTip>(),
           new string[] { "left" },
           "CreateHtmlTableFromDataTable-table",
           "CreateHtmlTableFromDataTable-DataHedaer",
           "CreateHtmlTableFromDataTable-Data");
    }

}