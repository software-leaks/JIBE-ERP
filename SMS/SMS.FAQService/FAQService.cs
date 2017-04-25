using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SMS.Business.FAQ;
using SMS.Properties;
using System.ServiceModel.Activation;
using System.Reflection;
using System.Data;
using System.IO;

namespace JiBeFAQService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FAQService : IFAQService
    {
        public string ADDFAQ(int FAQ_ID, string Question, string Answer, int UserID, int TopicID, string Topic_Description, int ModuleID, string Module_Description)
        {
            BLL_FAQ_Item.Upd_Faq_ServiceDetails(FAQ_ID, Question, Answer, UserID, TopicID, Topic_Description, ModuleID, Module_Description);
            return "true";
        }
        public string DeleteFAQ(int FAQ_ID, int UserID)
        {
            BLL_FAQ_Item.Del_Faq_ServiceDetails(FAQ_ID, UserID);
            return "true";
        }
        public string UploadFile(string FileName, string UploadPath, byte[] FileContent)
        {
            string FullFilename;
            try
            {
                
                if (!Directory.Exists(UploadPath))
                    Directory.CreateDirectory(UploadPath);

                Guid GUID = Guid.NewGuid();

                FullFilename = Path.Combine(UploadPath, FileName);
                using (FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(FileContent, 0, FileContent.Length);
                    fileStream.Close();
                }

                FullFilename = FileName;
            }
            catch
            {
                FullFilename = null;
            }

            return "true";

        }
    }
}
