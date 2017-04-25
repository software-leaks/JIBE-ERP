using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class DocumentPoperties

    {
        # region public Property

        public int DocID
        { get; set; }

        public string DocNo
        { get; set; }

        public string DocName
        { get; set; }

        public string DocFileName
        { get; set; }

        public string DocFilePath
        { get; set; }

        public int DocTypeID
        { get; set; }

        public string DocTypeName
        { get; set; }

        public int CurrentVersion
        { get; set; }

        public string DocHeader
        { get; set; }

        public DateTime DateOfIssue
        { get; set; }

        public string PlaceOfIssue
        { get; set; }

        public string IssuingAuthority
        { get; set; }

        public string CountryOfIssue
        { get; set; }
        
        public DateTime DateOfExpiry
        { get; set; }
        
        public int ApproveStatus
        { get; set; }

        public int ApprovedBy
        { get; set; }

        public string ApprovedUserName
        { get; set; }

        public int Active_Status
        { get; set; }

        public int CreatedBy
        { get; set; }

        public DateTime DateOfCreation
        { get; set; }       

        public int ModifiedBy
        { get; set; }

        public DateTime Date_Of_Modification
        { get; set; }       

        public int DeletedBy
        { get; set; }

        public DateTime DateOfDelete
        { get; set; }        

        public double SizeByte
        { get; set; }

        public int CurrentStatus
        { get; private set; }

        public int CheckOutBy
        { get; set; }

        public DateTime CheckOutDate
        { get; set; }

        public int ParentFolderId
        { get; set; }

        public int VoyageId
        { get; set; }

        #endregion public Property
    }
}
