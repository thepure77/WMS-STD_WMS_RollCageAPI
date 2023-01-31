using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class wm_TagOutPick
    {
        [Key]
        public Guid TagOutPick_Index { get; set; }


        public string TagOutPick_No { get; set; }


        public string TagOutPickRef_No1 { get; set; }


        public string TagOutPickRef_No2 { get; set; }


        public string TagOutPickRef_No3 { get; set; }


        public string TagOutPickRef_No4 { get; set; }


        public string TagOutPickRef_No5 { get; set; }

        public int? TagOutPick_Status { get; set; }


        public string UDF_1 { get; set; }


        public string UDF_2 { get; set; }


        public string UDF_3 { get; set; }


        public string UDF_4 { get; set; }


        public string UDF_5 { get; set; }

        public Guid? Zone_Index { get; set; }

        public Guid? Ref_Process_Index { get; set; }


        public string Ref_Document_No { get; set; }

        public Guid? Ref_Document_Index { get; set; }

        public Guid? Ref_DocumentItem_Index { get; set; }


        public string Create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }


        public DateTime? Cancel_Date { get; set; }

        public Guid? Equipment_Index { get; set; }


        public string Equipment_Id { get; set; }


        public string Equipment_Name { get; set; }

        public Guid? EquipmentItem_Index { get; set; }


        public string EquipmentItem_Id { get; set; }


        public string EquipmentItem_Name { get; set; }


        public decimal? ConfirmTagOutQty { get; set; }


        public string SuggestLocation_Name { get; set; }


        public string TagOutPick_Size { get; set; }


        public string Picking_By { get; set; }

        public DateTime? Picking_Date { get; set; }

        public int? Picking_Status { get; set; }


        public string UserAssign { get; set; }
    }
}
