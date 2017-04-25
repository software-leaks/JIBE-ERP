// =====================================================================
//
// Sample project for "Microsoft Indexing Service HOW-TO" article
//
// by Ilya Verbitskiy, iverbitskiy@yahoo.com
// Copyright Ilya Verbitskiy, July 2007
// http://ilich-workshop.blogspot.com/
//
// =====================================================================

using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace SMS.Business.QMS
{
    public struct SearchParameters
    {
        private string Storage_;
        private string Query_;
        private string SearchFolder_;
        private string FileSizeFilter_;
        private string FileNameFilter_;
        private string DateModifiedFrom_;
        private string DateModifiedTo_;
        private int UserID_;

        public string Storage
        {
            get { return Storage_; }
            set { Storage_ = value; }
        }
        public string Query
        {
            get { return Query_; }
            set { Query_ = value; }
        }
        public string SearchFolder
        {
            get { return SearchFolder_; }
            set { SearchFolder_ = value; }
        }
        public string FileSizeFilter
        {
            get { return FileSizeFilter_; }
            set { FileSizeFilter_ = value; }
        }
        public string FileNameFilter
        {
            get { return FileNameFilter_; }
            set { FileNameFilter_ = value; }
        }
        public string DateModifiedFrom
        {
            get { return DateModifiedFrom_; }
            set { DateModifiedFrom_ = value; }
        }
        public string DateModifiedTo
        {
            get { return DateModifiedTo_; }
            set { DateModifiedTo_ = value; }
        }
        public int UserID
        {
            get { return UserID_; }
            set { UserID_ = value; }
        }
    }

    public class BLL_QMS_SearchCatalog
    {
        static void Main(string[] args)
        {
            //QMS_SearchIndex program = new QMS_SearchIndex();

            //SearchParameters query = program.GetSearchParameters();
            //ArrayList files = program.ExecuteQuery(query);
            //program.ShowResults(files);
            //Console.WriteLine("\n\n\nPress [Enter] to continue ... ");
            //Console.Read();
        }

        #region Execute query

        private const string CONNECTION_STRING = "Provider= \"MSIDXS\";Data Source=\"{0}\";";

        public DataTable ExecuteQuery(SearchParameters Params, DataTable FileID)
        {
            DataTable dt = new DataTable();

            try
            {
                string strQuery = "";
                int UserID = Params.UserID;

                //strQuery = "Select DocTitle,Filename,Size,PATH,URL from Scope()  where FREETEXT('" + Query + "')";
                strQuery = "SELECT doctitle, Filename, vpath, Path, Write, Size, Rank ";
                strQuery += "FROM \"" + Params.Storage + "\"..SCOPE() ";
                strQuery += "WHERE Filename != '' ";
                if (Params.Query.ToString() != "")
                {
                    strQuery += " AND CONTAINS(Contents, '" + Params.Query + "') ";
                    if (FileID.Rows.Count > 0)
                    {
                        foreach (DataRow row in FileID.Rows)
                        {
                            //  strQuery += " OR CONTAINS(FileName, '" + Params.Query + "') ";
                            strQuery += " OR CONTAINS(FileName, '" + row["FileID"].ToString() + "') ";
                        }
                    }
                }

                strQuery += "ORDER BY Rank DESC";

                string connstring = "Provider=MSIDXS.1;Integrated Security .='';Data Source=" + Params.Storage;

                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connstring);
                conn.Open();

                System.Data.OleDb.OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);
                System.Data.DataSet testDataSet = new System.Data.DataSet();

                cmd.Fill(testDataSet);

                dt = testDataSet.Tables[0];

                string sFilter = "";
                if (Params.SearchFolder != "")
                    sFilter = "PATH like '%\\" + Params.SearchFolder.ToLower().Replace("/", "\\") + "%'";

                if (Params.DateModifiedFrom != null && Params.DateModifiedTo != null)
                {
                    sFilter += (sFilter.Length > 0) ? " AND " : "";
                    sFilter += " ( Write >= '" + Params.DateModifiedFrom + "' AND Write <='" + Params.DateModifiedTo + "')";
                }

                DataRow[] drSearchRes;
                drSearchRes = dt.Select(sFilter);
                conn.Close();

                return getResultTable(drSearchRes, UserID);

            }
            catch
            {
                throw; //Console.WriteLine("Error: {0}", err.Message);
            }
        }

        #endregion

        private DataTable getResultTable(DataRow[] drDataRows, int UserID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FileID", typeof(string)));
            dt.Columns.Add(new DataColumn("Filename", typeof(string)));
            dt.Columns.Add(new DataColumn("File Size", typeof(int)));
            dt.Columns.Add(new DataColumn("Read Date", typeof(string)));
            dt.Columns.Add(new DataColumn("FilePath", typeof(string)));
            dt.Columns.Add(new DataColumn("Write", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Version", typeof(string)));
            DataRow dr;
            BLL_QMS_Document objQMS = new BLL_QMS_Document();

            foreach (DataRow dtR in drDataRows)
            {

                DataTable dt_FileDtl = objQMS.getFileIDByFullPath(FormatFilePath(dtR["Path"].ToString()));
                if (dt_FileDtl.Rows.Count > 0)
                {
                    int fileID = Convert.ToInt32(dt_FileDtl.Rows[0]["FileID"].ToString());
                    int Access = objQMS.Get_UserAccess_OnFile(fileID, UserID);

                    if (Access > 0)
                    {
                        string LastVersion = "1";
                        DataTable dtVersionInfo = objQMS.getFileVersion(fileID).Tables[1];
                        if (dtVersionInfo.Rows.Count > 0)
                            LastVersion = dtVersionInfo.Rows[0][0].ToString();


                        for (int i = 0; i < int.Parse(LastVersion); i++)
                        {
                            if (dtR["Path"].ToString().IndexOf(fileID.ToString() + "\\" + i.ToString() + "\\" + dtR["Filename"].ToString()) > 0)
                            {
                                LastVersion = i.ToString();
                            }
                        }

                        dr = dt.NewRow();
                        dr["FileID"] = fileID.ToString();
                        //dr["Filename"] = dtR["Filename"].ToString();
                        dr["Filename"] = dt_FileDtl.Rows[0]["LogFileID"].ToString();
                        dr["File Size"] = Convert.ToInt32(dtR["size"].ToString()) / (1024);
                        dr["Read Date"] = ((DateTime)dtR["write"]).ToString("dd/MM/yyyy");
                        dr["FilePath"] = FormatFilePath(dtR["Path"].ToString());
                        dr["Write"] = dtR["write"];
                        dr["Version"] = LastVersion;

                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;

        }

        private string FormatFilePath(string FilePath)
        {
            int iDocPos = FilePath.ToLower().IndexOf("\\documents\\");
            string strVPath = "";

            string[] arFilePath = FilePath.Substring(iDocPos + 1).Split('\\');

            for (int i = 0; i < arFilePath.Length; i++)
            {
                if (strVPath.Length > 0)
                    strVPath += "/";

                strVPath += arFilePath[i];
            }
            return strVPath;

            //if (arFilePath.Length > 5)
            //{
            //    string strPath = "Documents\\...\\" + arFilePath[arFilePath.Length - 2].ToString() + "\\" + arFilePath[arFilePath.Length - 1].ToString();
            //    return strPath;
            //}
            //else
            //{
            //    return FilePath.Substring(iDocPos + 1);
            //}
        }




    }
}
