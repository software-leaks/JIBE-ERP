using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class AttributeProperties
    {
        public int AttributeID
        { get; set; }

        public int DocID
        { get; set; }

        public string AttributeName
        { get; set; }

        public string AttributeValue
        { get; set; }

        public int ElementDocID
        { get; set; }

        public int version
        { get; set; }

        public int SaveVersion
        { get; set; }

        public int AttributeTypeID
        { get; set; }
       
        public int CreatedBy
        { get; set; }

        public DateTime DateOfCreation
        { get; set; }

        public int ModifiedBy
        { get; set; }

        public DateTime DateOfModification
        { get; set; }
    }
}
