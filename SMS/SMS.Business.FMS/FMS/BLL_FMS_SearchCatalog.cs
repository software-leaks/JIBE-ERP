using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.IO;
namespace SMS.Business.FMS
{
    public struct FMSSearchParameters
    {
        private string FMSStorage_;
        private string FMSQuery_;
        private string FMSSearchFolder_;
        private string FMSFileSizeFilter_;
        private string FMSFileNameFilter_;
        private string FMSDateModifiedFrom_;
        private string FMSDateModifiedTo_;
        private int FMSUserID_;

        public string FMSStorage
        {
            get { return FMSStorage_; }
            set { FMSStorage_ = value; }
        }
        public string FMSQuery
        {
            get { return FMSQuery_; }
            set { FMSQuery_ = value; }
        }
        public string FMSSearchFolder
        {
            get { return FMSSearchFolder_; }
            set { FMSSearchFolder_ = value; }
        }
        public string FMSFileSizeFilter
        {
            get { return FMSFileSizeFilter_; }
            set { FMSFileSizeFilter_ = value; }
        }
        public string FMSFileNameFilter
        {
            get { return FMSFileNameFilter_; }
            set { FMSFileNameFilter_ = value; }
        }
        public string FMSDateModifiedFrom
        {
            get { return FMSDateModifiedFrom_; }
            set { FMSDateModifiedFrom_ = value; }
        }
        public string FMSDateModifiedTo
        {
            get { return FMSDateModifiedTo_; }
            set { FMSDateModifiedTo_ = value; }
        }
        public int FMSUserID
        {
            get { return FMSUserID_; }
            set { FMSUserID_ = value; }
        }
    }
    /// <summary>
    /// Method is used to search documnet according to givan filter inputs
    /// </summary>
    public class BLL_FMS_SearchCatalog
    {
        static void Main(string[] args)
        {
            //FMS_SearchIndex program = new FMS_SearchIndex();

            //SearchParameters query = program.GetSearchParameters();
            //ArrayList files = program.ExecuteQuery(query);
            //program.ShowResults(files);
            //Console.WriteLine("\n\n\nPress [Enter] to continue ... ");
            //Console.Read();
        }

        #region Execute query

        private const string CONNECTION_STRING = "Provider= \"MSIDXS\";Data Source=\"{0}\";";

        public DataTable ExecuteQuery(FMSSearchParameters Params, DataTable FileID)
        {
            DataTable dt = new DataTable();

            try
            {
                string strQuery = "";
                int UserID = Params.FMSUserID;               
                strQuery = "SELECT doctitle, Filename, vpath, Path, Write, Size, Rank ";
                strQuery += "FROM \"" + Params.FMSStorage + "\"..SCOPE() ";
              
                string strsQry = "";
                if (Params.FMSQuery.ToString() != "")
                {
             
                    if (FileID.Rows.Count > 0)
                    {
                        foreach (DataRow row in FileID.Rows)
                        {
                         
                            if (strsQry == "")
                            {
                                strsQry += " where CONTAINS(FileName, '" + row["FileID"].ToString() + "') ";
                            }
                            else
                            {
                                strsQry += " OR CONTAINS(FileName, '" + row["FileID"].ToString() + "') ";
                            }
                        }
                    }
                }
                strQuery +=strsQry;
                strQuery += "ORDER BY Rank DESC";

                string connstring = "Provider=MSIDXS.1;Integrated Security .='';Data Source=" + Params.FMSStorage;

                System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connstring);
                conn.Open();

                System.Data.OleDb.OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter(strQuery, conn);
                System.Data.DataSet testDataSet = new System.Data.DataSet();

                cmd.Fill(testDataSet);

                dt = testDataSet.Tables[0];

                DataColumn colDateTime = new DataColumn("DateStr");
                colDateTime.DataType = System.Type.GetType("System.DateTime");
                dt.Columns.Add(colDateTime);


                foreach (DataRow dr in dt.Rows)
                {
                    dr["DateStr"] = Convert.ToDateTime(dr["Write"]).Date;
                }
                 
                string sFilter = "";
                if (Params.FMSSearchFolder != "")
                    sFilter = "PATH like '%\\" + Params.FMSSearchFolder.ToLower().Replace("/", "\\") + "%'";

                if (Params.FMSDateModifiedFrom != null && Params.FMSDateModifiedTo != null)
                {
                    sFilter += (sFilter.Length > 0) ? " AND " : "";
                    sFilter += " ( DateStr  >=  '" + Convert.ToDateTime(Params.FMSDateModifiedFrom) + "' AND DateStr <= '" + Convert.ToDateTime(Params.FMSDateModifiedTo) + "')";                   
                              
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
        /// <summary>
        /// Filter data
        /// </summary>
        /// <param name="drDataRows">Fetch details from </param>
        /// <param name="UserID">Login user ID</param>
        /// <returns>retrun again filter data</returns>
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
            BLL_FMS_Document objFMS = new BLL_FMS_Document();

            foreach (DataRow dtR in drDataRows)
            {

                DataTable dt_FileDtl = objFMS.getFileIDByFullPath("uploads/fmsl/"+dtR["Filename"].ToString());
                if (dt_FileDtl.Rows.Count > 0)
                {
                    int fileID = Convert.ToInt32(dt_FileDtl.Rows[0]["FileID"].ToString());
                    int Access = objFMS.Get_UserAccess_OnFile(fileID, UserID);

                    if (Access > 0)
                    {
                        string LastVersion = "1";
                        DataTable dtVersionInfo = objFMS.getFileVersion(fileID).Tables[1];
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
                        dr["FilePath"] = "../fmsl/" + Path.GetFileName(dtR["Path"].ToString());// FormatFilePath(dtR["Path"].ToString());
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
