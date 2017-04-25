using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using System.ServiceModel.Activation;

namespace JiBEWCFService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {
        public string UploadFile(string FileName, string UploadPath, string FilePrefix, byte[] FileContent)
        {
            string FullFilename;
            try
            {

                if (!Directory.Exists(UploadPath))
                    Directory.CreateDirectory(UploadPath);

                Guid GUID = Guid.NewGuid();

                FullFilename = Path.Combine(UploadPath, FilePrefix + GUID.ToString() + Path.GetExtension(FileName));
                using (FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(FileContent, 0, FileContent.Length);
                    fileStream.Close();
                }

                FullFilename = FilePrefix + GUID.ToString() + Path.GetExtension(FileName);
            }
            catch
            {
                FullFilename = null;
            }

            return FullFilename;

        }

        public byte[] DownloadFile(string FileFullName)
        {
            byte[] FileContent = null;
            try
            {
                using (FileStream fileStream = new FileStream(FileFullName, FileMode.Open, FileAccess.Read))
                {
                    FileContent = new byte[fileStream.Length];
                    fileStream.Read(FileContent, 0, (int)fileStream.Length);
                    fileStream.Close();
                    fileStream.Dispose();
                    return FileContent;
                }
            }
            catch { }

            return FileContent;
        }

        public bool DeleteFile(string FileFullName)
        {
            bool sts = true;

            try
            {
                File.Delete(FileFullName);
            }
            catch
            {
                sts = false;
            }
            return sts;

        }

      

        // it returns all file of given directory (same as path.getfiles())
        public string[] GetFiles(string DirectoryFullName)
        {
            return Directory.GetFiles(DirectoryFullName);
        }

        public bool MoveFile(string FileName, string UploadPath, byte[] FileContent)
        {
            bool sts = true;
            string FullFilename;
            try
            {

                if (!Directory.Exists(UploadPath))
                    Directory.CreateDirectory(UploadPath);


                FullFilename = Path.Combine(UploadPath, FileName);
                using (FileStream fileStream = new FileStream(FullFilename, FileMode.Create, FileAccess.ReadWrite))
                {
                    fileStream.Write(FileContent, 0, FileContent.Length);
                    fileStream.Close();
                }
                                
            }
            catch
            {
                sts = false;
            }

            return sts;

        }
        
    }
}


