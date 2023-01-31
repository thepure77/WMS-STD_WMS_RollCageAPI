using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RollCageDataAccess.Models
{

    public partial class im_RollCage_Movement
    {
        [Key]
        public Guid RollCage_Movement_Index { get; set; }

        public DateTime RollCage_Movement_Date { get; set; }

        public Guid? Process_Index { get; set; }

        [StringLength(50)]
        public string Process_Id { get; set; }

        [StringLength(200)]
        public string Process_Name { get; set; }

        public Guid RollCage_Index { get; set; }

        [StringLength(50)]
        public string RollCage_Id { get; set; }

        [StringLength(200)]
        public string RollCage_Name { get; set; }

        public Guid? Location_Index { get; set; }

        [StringLength(50)]
        public string Location_Id { get; set; }

        [StringLength(200)]
        public string Location_Name { get; set; }

        public Guid? Location_Index_To { get; set; }

        [StringLength(50)]
        public string Location_Id_To { get; set; }

        [StringLength(200)]
        public string Location_Name_To { get; set; }

        public Guid Equipment_Index { get; set; }

        [StringLength(50)]
        public string Equipment_Id { get; set; }

        [StringLength(200)]
        public string Equipment_Name { get; set; }

        [StringLength(50)]
        public string PlanGoodsIssue_No { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsIssue_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsIssue_Due_Date { get; set; }

        public Guid? TruckLoad_Index { get; set; }

        [StringLength(50)]
        public string TruckLoad_No { get; set; }

        public DateTime? TruckLoad_Date { get; set; }

        [StringLength(200)]
        public string Ref_Document_No { get; set; }

        public Guid? Ref_Document_Index { get; set; }

        public Guid? Ref_DocumentItem_Index { get; set; }

        [StringLength(200)]
        public string UDF_1 { get; set; }

        [StringLength(200)]
        public string UDF_2 { get; set; }

        [StringLength(200)]
        public string UDF_3 { get; set; }

        [StringLength(200)]
        public string UDF_4 { get; set; }

        [StringLength(200)]
        public string UDF_5 { get; set; }

        public DateTime? Request_Date { get; set; }

        public DateTime? Commited_Date { get; set; }

        public int? RollCage_Movement_Status { get; set; }

        [Required]
        [StringLength(200)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }
    }
}
