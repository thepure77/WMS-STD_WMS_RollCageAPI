using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class im_GoodsIssue
    {
        [Key]
        public Guid GoodsIssue_Index { get; set; }

        public Guid Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }


        public string DocumentType_Name { get; set; }

        public string GoodsIssue_No { get; set; }

        public DateTime GoodsIssue_Date { get; set; }

        public string GoodsIssue_Time { get; set; }

        public DateTime? Document_Date { get; set; }


        public string DocumentRef_No1 { get; set; }


        public string DocumentRef_No2 { get; set; }


        public string DocumentRef_No3 { get; set; }


        public string DocumentRef_No4 { get; set; }


        public string DocumentRef_No5 { get; set; }


        public string Document_Remark { get; set; }

        public int? Document_Status { get; set; }


        public string UDF_1 { get; set; }


        public string UDF_2 { get; set; }


        public string UDF_3 { get; set; }


        public string UDF_4 { get; set; }


        public string UDF_5 { get; set; }

        public int? DocumentPriority_Status { get; set; }

        public int? Picking_Status { get; set; }


        public string Create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }


        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }


        public DateTime? Cancel_Date { get; set; }

        public Guid? Warehouse_Index { get; set; }


        public string Warehouse_Id { get; set; }


        public string Warehouse_Name { get; set; }

        public int? Cancel_Status { get; set; }

        public int? RunWave_Status { get; set; }

        public Guid? Wave_Index { get; set; }

        public virtual im_GoodsIssue im_GoodsIssue1 { get; set; }

        public virtual im_GoodsIssue im_GoodsIssue2 { get; set; }
    }
}
