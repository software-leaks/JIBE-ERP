using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace JiBEWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        [OperationContract(Action = "UploadFile", Name = "UploadFile")]
        [return: MessageParameter(Name = "UploadFileReturn")]
        string UploadFile(string FileName, string UploadPath, string FilePrefix, byte[] FileContent);

        [OperationContract(Action = "DownloadFile", Name = "DownloadFile")]
        [return: MessageParameter(Name = "DownloadFileReturn")]
        byte[] DownloadFile(string FileFullName);

        [OperationContract(Action = "DeleteFile", Name = "DeleteFile")]
        [return: MessageParameter(Name = "DeleteFileReturn")]
        bool DeleteFile(string FileFullName);


        [OperationContract(Action = "GetFiles", Name = "GetFiles")]
        [return: MessageParameter(Name = "GetFilesReturn")]
        string[] GetFiles(string DirectoryFullName);

        [OperationContract(Action = "MoveFile", Name = "MoveFile")]
        [return: MessageParameter(Name = "MoveFileReturn")]
        bool MoveFile(string FileName, string UploadPath, byte[] FileContent);
          

        
    }

}
