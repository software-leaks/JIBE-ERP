using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel.Activation;
using System.IO;

namespace SMS.PNTService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PNTService : IPNTService
    {
        public PNTTables GetPNTTB(int PNTID)
        { 
            DataTable table = new DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", PNTID.ToString(), DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            PNTTables lObj = new PNTTables();
            lObj.EmployeeTable = table;
            return lObj;
        }

        public DataSet Get_PntInfo(DataTable OPS_PNT_MAX_UDT, int Max_DDL_ID)
        {
            PNTTables lObj = new PNTTables();
            return DAL_PNT_Serviceops.Get_PntInfo(OPS_PNT_MAX_UDT,Max_DDL_ID);
           
        }

        public string TestR(int PNTID)
        {
            return PNTID++.ToString();
        }

        public string Insert_RptInfo(DataSet RptInfo)
        {
            try
            {
                DAL_PNT_Serviceops.INS_PNTRPTINFO(RptInfo.Tables[0], RptInfo.Tables[1], RptInfo.Tables[2], RptInfo.Tables[3]);
                return "true";
            }
            catch (Exception)
            {

                return "false"; ;
            }
            
        }
        public FileTransferResponse Put(FileTransferRequest fileToPush)
        {
            FileTransferResponse fileTransferResponse = this.CheckFileTransferRequest(fileToPush);
            if (fileTransferResponse.ResponseStatus == "FileIsValed")
            {
                try
                {
                    this.SaveFileStream(System.AppDomain.CurrentDomain.BaseDirectory + "\\Uploads\\PreArrivalAttachments\\" + fileToPush.FileName, new MemoryStream(fileToPush.Content));
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToPush.FileName,
                        Message = "File was transfered",
                        ResponseStatus = "Successful"
                    };
                }
                catch (Exception ex)
                {
                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = fileToPush.FileName,
                        Message = ex.Message,
                        ResponseStatus = "Error"
                    };
                }
            }

            return fileTransferResponse;
        }

        private void SaveFileStream(string filePath, Stream stream)
        {
            try
            {
                if (File.Exists(filePath))
                {
                   // File.Delete(filePath);
                }

                var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private FileTransferResponse CheckFileTransferRequest(FileTransferRequest fileToPush)
        {
            if (fileToPush != null)
            {
                if (!string.IsNullOrEmpty(fileToPush.FileName))
                {
                    if (fileToPush.Content != null)
                    {
                        return new FileTransferResponse
                        {
                            CreateAt = DateTime.Now,
                            FileName = fileToPush.FileName,
                            Message = string.Empty,
                            ResponseStatus = "FileIsValed"
                        };
                    }

                    return new FileTransferResponse
                    {
                        CreateAt = DateTime.Now,
                        FileName = "No Name",
                        Message = " File Content is null",
                        ResponseStatus = "Error"
                    };
                }

                return new FileTransferResponse
                {
                    CreateAt = DateTime.Now,
                    FileName = "No Name",
                    Message = " File Name Can't be Null",
                    ResponseStatus = "Error"
                };
            }

            return new FileTransferResponse
            {
                CreateAt = DateTime.Now,
                FileName = "No Name",
                Message = " File Can't be Null",
                ResponseStatus = "Error"
            };
        }
    }
}
