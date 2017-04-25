using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS.Properties
{
    public class ElementProperties
    {
        public int ElementID
        {get; set;}

        public int DocID
        { get; set; }

        public string ElementName
        { get; set; }

        public int ElementDocID
        {get; set;}

        public int version
        {get; set;}

        public int SaveVersion
        {get; set;}

        public int ElementTypeID
        {get; set;}

        public int ParentID
        {get; set;}

        public int CreatedBy
        {get; set;}

        public DateTime DateOfCreation
        {get; set;}

        public int ModifiedBy
        {get; set;}

        public DateTime DateOfModification
        {get; set;}

    }
}
