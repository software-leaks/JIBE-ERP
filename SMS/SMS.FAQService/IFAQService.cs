using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace JiBeFAQService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFAQService
    {
        [OperationContract]
        string ADDFAQ(int FAQ_ID, string Question, string Answer, int UserID, int TopicID, string Topic_Description, int ModuleID, string Module_Description);

        [OperationContract]
        string DeleteFAQ(int FAQ_ID, int UserID);

        [OperationContract(Action = "UploadFile", Name = "UploadFile")]
        [return: MessageParameter(Name = "UploadFileReturn")]
        string UploadFile(string FileName, string UploadPath, byte[] FileContent);
        // TODO: Add your service operations here
    }

   
}
