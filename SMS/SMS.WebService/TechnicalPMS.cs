using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Business.PMS;
using System.Web.Services;

public partial class JibeWebService
{
    //General Methods for Convert Datatable to json data
    #region CONVERT DATATABLE TO JSON DATA

    private string GetJsonArray(DataTable dt)
    {
        string res = "";
        string sColumnValues = "";

        foreach (DataRow dr in dt.Rows)
        {
            if (res.Length > 0) res += ",";
            res += "{";
            sColumnValues = "";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (sColumnValues.Length > 0) sColumnValues += ",";
                sColumnValues += dt.Columns[i].ColumnName.ToString() + ":'" + dr[i].ToString() + "'";
            }
            res += ReplaceSpecialCharacters(sColumnValues);
            res += "}";
        }
        if (!string.IsNullOrEmpty(res))
        {
            res = "[" + res + "]";
        }
        return res;

    }


    public string ConvertDataTabletoJsonData(DataTable dtData)
    {


        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dtData.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dtData.Columns)
            {
                if (col.DataType.ToString() == "System.DateTime")
                    row.Add(col.ColumnName, Convert.ToString(dr[col]));
                else
                    row.Add(col.ColumnName, ReplaceSpecialCharacters(Convert.ToString(dr[col])));
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);
    }
    public static string ReplaceSpecialCharacters(string str)
    {
        //return str.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;").Replace("\"", "&quot;");
        string ret = str.Replace(@"\", @"\\");
        return ret;
    }

    #endregion

    #region Class Files used in Running Hour
    public class SystemSubSytemTree
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
    }

    public List<SystemSubSytemTree> lstSystemSubSytemTree { get; set; }
    #endregion


    //Methods related to Running Hours Feature
    #region Web Methods for PMS - Running Hour Feature
    [WebMethod]
    public string PMS_Get_SourceSystemSubsystemFunction_Tree(string id, string vesselid, string equipmentid, string equipmenttype)
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        DataTable dt = objJobs.Get_System_SubsystemTreeData(int.Parse(vesselid));
        //for (int i = dt.Rows.Count - 1; i >= 0; i--)
        //{
        //    if (dt.Rows[i][0] == DBNull.Value && dt.Rows[i][2] == DBNull.Value)
        //        dt.Rows[i].Delete();
        //}
        //dt.AcceptChanges();

        int ID = int.Parse(id);
        if (ID > 0)
        {
            string sParentID = "";
            string sChildID = "";
            DataRow[] result = dt.Select("id = '" + ID + "'");
            for (int i = 0; i < result.Length; i++)
            {
                sParentID = result[i]["parent"].ToString();
                if (sParentID == "#")
                    sParentID = "0";
                dt.Rows.Remove(result[i]);
                dt.AcceptChanges();


                DataRow[] result2 = dt.Select("[parent] = '" + sParentID + "'");
                for (int k = 0; k < result2.Length; k++)
                {
                    sChildID = result2[k]["id"].ToString();
                    dt.Rows.Remove(result2[k]);
                    dt.AcceptChanges();

                    DataRow[] result4 = dt.Select("[id] = '" + sChildID + "'");
                    for (int j = 0; j < result4.Length; j++)
                    {
                        dt.Rows.Remove(result4[j]);
                        dt.AcceptChanges();
                    }
                }

                DataRow[] result3 = dt.Select("[id] = '" + sParentID + "'");
                for (int j = 0; j < result3.Length; j++)
                {
                    dt.Rows.Remove(result3[j]);
                    dt.AcceptChanges();
                }

                DataRow[] result5 = dt.Select("[parent] = '" + ID + "'");
                for (int j = 0; j < result5.Length; j++)
                {
                    dt.Rows.Remove(result5[j]);
                    dt.AcceptChanges();
                }


            }


        }
        if (int.Parse(equipmentid) > 0)
        {
            if (equipmenttype == "1")
            {
                DataRow[] result = dt.Select("id = '" + equipmentid + "'");
                for (int j = 0; j < result.Length; j++)
                {
                    dt.Rows.Remove(result[j]);
                    dt.AcceptChanges();
                }
                DataRow[] result2 = dt.Select("[parent] = '" + equipmentid + "'");
                for (int i = 0; i < result2.Length; i++)
                {
                    dt.Rows.Remove(result2[i]);
                    dt.AcceptChanges();
                }
            }
            else
            {
                int ParentID = 0;
                DataRow[] result = dt.Select("id = '" + equipmentid + "'");
                for (int j = 0; j < result.Length; j++)
                {
                    ParentID = int.Parse(Convert.ToString(result[j]["parent"]));
                    dt.Rows.Remove(result[j]);
                    dt.AcceptChanges();
                }
                DataRow[] result2 = dt.Select("[id] = '" + ParentID + "'");
                for (int i = 0; i < result2.Length; i++)
                {
                    dt.Rows.Remove(result2[i]);
                    dt.AcceptChanges();
                }
                DataRow[] result3 = dt.Select("[parent] = '" + ParentID + "'");
                for (int i = 0; i < result3.Length; i++)
                {
                    dt.Rows.Remove(result3[i]);
                    dt.AcceptChanges();
                }
            }
        }
        return ConvertDataTabletoJsonData(dt);

    }
    [WebMethod]
    public string PMS_Get_DestinationSystemSubsystemFunction_Tree(string id, string vesselid)
    {
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();
        DataTable dt = objJobs.PMS_Get_DestinationSystemSubsystemTreeData(int.Parse(vesselid));
        string sParentID = "";
        string sChildID = "";
        DataRow[] result = dt.Select("id = '" + id + "'");
        for (int i = 0; i < result.Length; i++)
        {
            sParentID = result[i]["parent"].ToString();
            if (sParentID == "#")
                sParentID = "";
            dt.Rows.Remove(result[i]);
            dt.AcceptChanges();


            DataRow[] result2 = dt.Select("[parent] = '" + sParentID + "'");
            for (int k = 0; k < result2.Length; k++)
            {
                sChildID = result2[k]["id"].ToString();
                dt.Rows.Remove(result2[k]);
                dt.AcceptChanges();

                DataRow[] result4 = dt.Select("[id] = '" + sChildID + "'");
                for (int j = 0; j < result4.Length; j++)
                {
                    dt.Rows.Remove(result4[j]);
                    dt.AcceptChanges();
                }
            }

            DataRow[] result3 = dt.Select("[id] = '" + sParentID + "'");
            for (int j = 0; j < result3.Length; j++)
            {
                dt.Rows.Remove(result3[j]);
                dt.AcceptChanges();
            }



        }
        return ConvertDataTabletoJsonData(dt);

    }


    [WebMethod]
    public string PMS_Get_IsSystemSubSystemRunHourBased(string systemid, string subsystemid, string vesselid)
    {
        int SystemID = int.Parse(systemid);
        int SubSystemID = int.Parse(subsystemid);
        int VesselID = int.Parse(vesselid);
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

        int Result = objJobs.PMS_GET_IsJobRunHourBased(SystemID, SubSystemID, VesselID);
        return Result.ToString();

    }


    [WebMethod]
    public string PMS_Get_IsSystemRunHourBased(string systemid, string vesselid)
    {
        int SystemID = int.Parse(systemid);
        int VesselID = int.Parse(vesselid);
        BLL_PMS_Library_Jobs objJobs = new BLL_PMS_Library_Jobs();

        int Result = objJobs.PMS_Get_IsSystemRunHourBased(SystemID, VesselID);
        return Result.ToString();
    }

    #endregion

}
