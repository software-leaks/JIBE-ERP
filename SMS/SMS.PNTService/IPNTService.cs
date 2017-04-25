using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
 
namespace SMS.PNTService
{
    
        [ServiceContract]
        public interface IPNTService
        {
            [OperationContract]
            PNTTables GetPNTTB(int PNTID);

            [OperationContract]
            DataSet Get_PntInfo(DataTable OPS_PNT_MAX_UDT, int Max_DDL_ID);

            [OperationContract]
            string Insert_RptInfo(DataSet RptInfo);

            [OperationContract]
            string TestR(int PNTID);

            [OperationContract]
            FileTransferResponse Put(FileTransferRequest fileToPush);
           
        }
       

        [DataContract]
        public class PNTTables
        {
            [DataMember]
            public DataTable EmployeeTable
            {
                get;
                set;
            }
        }

        [DataContract] 
        public class FileTransferRequest
        {
            [DataMember] 
            public string FileName { get; set; }

            [DataMember]
            public byte[] Content { get; set; }
        }

        public class FileTransferResponse
        {
            [DataMember]
            /// <summary>
            /// Gets or sets File Name
            /// </summary>
            public string FileName { get; set; }

            [DataMember]
            /// <summary>
            /// Gets or sets Created at
            /// </summary>
            public DateTime CreateAt { get; set; }

            [DataMember]
            /// <summary>
            /// Gets or sets Message
            /// </summary>
            public string Message { get; set; }

            [DataMember]
            /// <summary>
            /// Gets or sets Response Status
            /// </summary>               
            public string ResponseStatus { get; set; }
        }

}

