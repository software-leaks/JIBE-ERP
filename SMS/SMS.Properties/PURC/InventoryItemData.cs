using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

using System.Xml.Linq;
using System.Runtime.Serialization;


namespace SMS.Properties
{
   

    public class IventoryItemData
    {
        public IventoryItemData()
        {

        }

        public string _constring;
        public string _InventoryID;
        public string _Vessel_Code;
        public string _Department_Code;
        public string _System_Code;
        public string _Subsystem_Code;
        public string _Item_Ref_Code;
        public string _Inventory_Date;
        public string _Inventory_Qty;
        public string _TO_Repair_Qty;
        public string _Working_Qty;
        public string _Min_Qty;
        public string _Max_Qty;
        public string _Delivered_Items;
        public string _Inventory_Type;
        public string _Used_Items;
        public string _Price_Book;
        public string _Storage_Place_1;
        public string _Storage_Place_2;
        public string _Prev_Item_Ref_Code;
        public string _Sync_Flag;
        public string _Link;
        public string _Created_By;
        public string _Date_Of_Creatation;
        public string _Modified_By;
        public string _Date_Of_Modification;
        public string _Deleted_By;
        public string _Date_Of_Deletion;
        public string _Active_Status;
        public string _Department;
        public string _DocumentCode;
        public string _DocumentNumber;
        public string _LineType;
        public string _RequiType;
        public int _Totalitem;
        private string _part_number;
        private string _short_description;
        private string _long_description;
        private string _Unit_and_Packings;
        private string _drawing_number;
        private string _ItemAddress;
        private string _User_Name;
        private string _Urgency_Code;

        public string _Item_Serial_No;
        public string _Item_InternRef;
        public string _ROB;
        public string _reqested_Qty;

        public string _item_FullDesc;
        public string _item_ShortDesc;
        public string _Saved_Line;
        public string _Drawing_Link;
        public string _Item_Comment;

        private string _RequisitionCode;
        private string _RequisitionComment;

        private string _POType;
        private string _Account_Type;
        private int _Delivery_Port;
        private DateTime _Delivery_Date;
        private int _Port_Call;
        private string _Owner_Code;
        private int _Discount;
        private string _WithHoldTax;
        private string _VAT;
        private int _Advance;
        private string _Currency;
        private string _SupplierID;


        public string Owner_Code
        {
            get { return _Owner_Code; }
            set { _Owner_Code = value; }
        }
        public int Port_Call
        {
            get { return _Port_Call; }
            set { _Port_Call = value; }
        }
        public string POType
        {
            get { return _POType; }
            set { _POType = value; }
        }
        public string Account_Type
        {
            get { return _Account_Type; }
            set { _Account_Type = value; }
        }
        public int Delivery_Port
        {
            get { return _Delivery_Port; }
            set { _Delivery_Port = value; }
        }
        public DateTime Delivery_Date
        {
            get { return _Delivery_Date; }
            set { _Delivery_Date = value; }
        } 

       



          
        public string RequisitionCode
        {
            get { return _RequisitionCode; }
            set { _RequisitionCode = value; }
        }
          
        public string RequisitionComment
        {
            get { return _RequisitionComment; }
            set { _RequisitionComment = value; }
        }


          
        public string ItemSerialNo
        {
            get { return _Item_Serial_No; }
            set { _Item_Serial_No = value; }
        }
          
        public string ItemInternRef
        {
            get { return _Item_InternRef; }
            set { _Item_InternRef = value; }
        }
          
        public string ROB
        {
            get { return _ROB; }
            set { _ROB = value; }
        }
          
        public string reqestedQty
        {
            get { return _reqested_Qty; }
            set { _reqested_Qty = value; }
        }
          
        public string itemFullDesc
        {
            get { return _item_FullDesc; }
            set { _item_FullDesc = value; }
        }
          
        public string itemShortDesc
        {
            get { return _item_ShortDesc; }
            set { _item_ShortDesc = value; }
        }
          
        public string SavedLine
        {
            get { return _Saved_Line; }
            set { _Saved_Line = value; }
        }
          
        public string DrawingLink
        {
            get { return _Drawing_Link; }
            set { _Drawing_Link = value; }
        }
          
        public string ItemComment
        {
            get { return _Item_Comment; }
            set { _Item_Comment = value; }
        }

   
          
        public string UserName
        {
            get { return _User_Name; }
            set { _User_Name = value; }

        }

           
        public string UrgencyCode
        {
            get { return _Urgency_Code; }
            set { _Urgency_Code = value; }

        }




          
        public string InventoryID
        {
            get { return _InventoryID; }
            set { _InventoryID = value; }

        }
         
        public string VesselCode
        {
            get { return _Vessel_Code; }
            set { _Vessel_Code = value; }
        }
         
        public string DepartmentCode
        {
            get { return _Department_Code; }
            set { _Department_Code = value; }
        }
         
        public string SystemCode
        {
            get { return _System_Code; }
            set { _System_Code = value; }

        }
         
        public string SubSystemCode
        {
            get { return _Subsystem_Code; }
            set { _Subsystem_Code = value; }

        }
         
        public string ItemRefCode
        {
            get { return _Item_Ref_Code; }
            set { _Item_Ref_Code = value; }
        }
         
        public string InventoryDate
        {
            get { return _Inventory_Date; }
            set { _Inventory_Date = value; }
        }
         
        public string InventoryQuantity
        {
            get { return _Inventory_Qty; }
            set { _Inventory_Qty = value; }
        }
         
        public string ToRepaireQuantity
        {
            get { return _TO_Repair_Qty; }
            set { _TO_Repair_Qty = value; }
        }
         
        public string WorkingQuantity
        {
            get { return _Working_Qty; }
            set { _Working_Qty = value; }
        }
         
        public string MaxinumQuantity
        {
            get { return _Max_Qty; }
            set { _Max_Qty = value; }
        }
         
        public string MininumQuantity
        {
            get { return _Min_Qty; }
            set { _Min_Qty = value; }
        }
         
        public string DeliveredItem
        {
            get { return _Delivered_Items; }
            set { _Delivered_Items = value; }
        }
         
        public string InventoryType
        {
            get { return _Inventory_Type; }
            set { _Inventory_Type = value; }
        }
         
        public string UsedItem
        {
            get { return _Used_Items; }
            set { _Used_Items = value; }
        }
         
        public string PriceBook
        {
            get { return _Price_Book; }
            set { _Price_Book = value; }
        }
         
        public string StoragePlace1
        {
            get { return _Storage_Place_1; }
            set { _Storage_Place_1 = value; }
        }
         
        public string StoragePlace2
        {
            get { return _Storage_Place_2; }
            set { _Storage_Place_2 = value; }
        }
         
        public string PrevItemRefCode
        {
            get { return _Prev_Item_Ref_Code; }
            set { _Prev_Item_Ref_Code = value; }
        }
         
        public string SyncFlag
        {
            get { return _Sync_Flag; }
            set { _Sync_Flag = value; }
        }
         
        public string Link
        {
            get { return _Link; }
            set { _Link = value; }
        }
         
        public string CreatedBy
        {
            get { return _Created_By; }
            set { _Created_By = value; }
        }
         
        public string DateOfCreation
        {
            get { return _Date_Of_Creatation; }
            set { _Date_Of_Creatation = value; }
        }
         
        public string ModifiedBy
        {
            get { return _Modified_By; }
            set { _Modified_By = value; }
        }
         
        public string DateOfModification
        {
            get { return _Date_Of_Deletion; }
            set { _Date_Of_Deletion = value; ;}

        }
         
        public string DeletedBy
        {
            get { return _Deleted_By; }
            set { _Deleted_By = value; }
        }
         
        public string DateOfDeletion
        {
            get { return _Date_Of_Creatation; }
            set { _Date_Of_Creatation = value; ;}
        }
         
        public string ActiveStatus
        {
            get { return _Active_Status; }
            set { _Active_Status = value; }

        }
         
        public string Constring
        {
            get { return _constring; }
            set { _constring = value; }
        }
         
        public string Department
        {

            get { return _Department; }
            set { _Department = value; }
        }
         
        public string DocumentCode
        {
            get { return _DocumentCode; }
            set { _DocumentCode = value; }

        }
         
        public string DocumentNumber
        {
            get { return _DocumentNumber; }
            set { _DocumentNumber = value; }
        }
         
        public string RequisitionType
        {
            get { return _RequiType; }
            set { _RequiType = value; }

        }
         
        public int Totalitem
        {
            get { return _Totalitem; }
            set { _Totalitem = value; }
        }
         
        public string LineType
        {
            get { return _LineType; }
            set { _LineType = value; }

        }
         

       
        public string ItemAddress
        {
            get { return _ItemAddress; }
            set { _ItemAddress = value; }
        }
         
        public string Part_Number
        {
            get
            { return _part_number; }
            set
            { _part_number = value; }
        }
         
        public string Short_Description
        {
            get
            { return _short_description; }
            set
            { _short_description = value; }
        }
         
        public string Long_Description
        {
            get
            { return _long_description; }
            set
            { _long_description = value; }
        }
         
        public string Unit_and_Packings
        {
            get { return _Unit_and_Packings; }
            set { _Unit_and_Packings = value; }

        }
         
        public string Drawing_Number
        {
            get { return _drawing_number; }
            set { _drawing_number = value; }
        }


        public Int32 ReqsnType
        {
            get;
            set;
        }
        public int Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }
        public string WithHoldTax
        {
            get { return _WithHoldTax; }
            set { _WithHoldTax = value; }
        }
        public string VAT
        {
            get { return _VAT; }
            set { _VAT = value; }
        }
        public int Advance
        {
            get { return _Advance; }
            set { _Advance = value; }
        }
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }
        public string SupplierID
        {
            get { return _SupplierID; }
            set { _SupplierID = value; }
        }
        
    }

}
