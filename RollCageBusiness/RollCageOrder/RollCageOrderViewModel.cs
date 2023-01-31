using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RollCageBusiness.RollCageOrder
{
    public  class RollCageOrderViewModel
    {
        [Key]
        public Guid RollCageOrder_Index { get; set; }

        public Guid RollCage_Index { get; set; }

        [StringLength(50)]
        public string RollCage_Id { get; set; }

        [StringLength(200)]
        public string RollCage_Name { get; set; }

        [Required]
        [StringLength(50)]
        public string PlanGoodsIssue_No { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime PlanGoodsIssue_Date { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? PlanGoodsIssue_Due_Date { get; set; }

        public Guid Owner_Index { get; set; }

        [StringLength(50)]
        public string Owner_Id { get; set; }

        [StringLength(200)]
        public string Owner_Name { get; set; }

        public Guid? DocumentType_Index { get; set; }

        [StringLength(50)]
        public string DocumentType_Id { get; set; }

        [StringLength(200)]
        public string DocumentType_Name { get; set; }

        public Guid SoldTo_Index { get; set; }

        [StringLength(50)]
        public string SoldTo_Id { get; set; }

        [StringLength(200)]
        public string SoldTo_Name { get; set; }

        [StringLength(500)]
        public string SoldTo_Address { get; set; }

        [StringLength(200)]
        public string SoldTo_Contact_Person { get; set; }

        public Guid ShipTo_Index { get; set; }

        [StringLength(50)]
        public string ShipTo_Id { get; set; }

        [StringLength(200)]
        public string ShipTo_Name { get; set; }

        [StringLength(500)]
        public string ShipTo_Address { get; set; }

        [StringLength(200)]
        public string ShipTo_Contact_Person { get; set; }

        [StringLength(50)]
        public string Branch_Code { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Box_No { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Total_Box { get; set; }

        public Guid? Chute_Index { get; set; }

        [StringLength(50)]
        public string Chute_Id { get; set; }

        [StringLength(200)]
        public string Chute_Name { get; set; }

        [StringLength(200)]
        public string ChuteUserCreate_By { get; set; }

        public DateTime? ChuteUserCreate_Date { get; set; }

        public Guid? GoodsIssue_Index { get; set; }

        [StringLength(50)]
        public string GoodsIssue_No { get; set; }

        public DateTime? GoodsIssue_Date { get; set; }

        public Guid? Round_Index { get; set; }

        [StringLength(50)]
        public string Round_Id { get; set; }

        [StringLength(200)]
        public string Round_Name { get; set; }

        public Guid? Route_Index { get; set; }

        [StringLength(50)]
        public string Route_Id { get; set; }

        [StringLength(200)]
        public string Route_Name { get; set; }

        public Guid? SubRoute_Index { get; set; }

        [StringLength(50)]
        public string SubRoute_Id { get; set; }

        [StringLength(200)]
        public string SubRoute_Name { get; set; }

        public Guid Dock_Index { get; set; }

        [Required]
        [StringLength(50)]
        public string Dock_Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Dock_Name { get; set; }

        [StringLength(200)]
        public string DockUserCreate_By { get; set; }

        public DateTime? DockUserCreate_Date { get; set; }

        public Guid? TruckLoad_Index { get; set; }

        [StringLength(50)]
        public string TruckLoad_No { get; set; }

        public DateTime? TruckLoad_Date { get; set; }

        public Guid? BoxType_Index { get; set; }

        [StringLength(50)]
        public string BoxType_Id { get; set; }

        [StringLength(200)]
        public string BoxType_Name { get; set; }

        public Guid? BoxSize_Index { get; set; }

        [StringLength(50)]
        public string BoxSize_Id { get; set; }

        [StringLength(200)]
        public string BoxSize_Name { get; set; }

        [StringLength(200)]
        public string DocumentRef_No1 { get; set; }

        [StringLength(200)]
        public string DocumentRef_No2 { get; set; }

        [StringLength(200)]
        public string DocumentRef_No3 { get; set; }

        [StringLength(200)]
        public string DocumentRef_No4 { get; set; }

        [StringLength(200)]
        public string DocumentRef_No5 { get; set; }

        [StringLength(500)]
        public string Document_Remark { get; set; }

        public int? Document_Status { get; set; }

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

        public Guid? SoldTo_SubDistrict_Index { get; set; }

        public Guid? SoldTo_District_Index { get; set; }

        public Guid? SoldTo_Province_Index { get; set; }

        public Guid? SoldTo_Country_Index { get; set; }

        public Guid? SoldTo_Postcode_Index { get; set; }

        public Guid? SubDistrict_Index { get; set; }

        public Guid? District_Index { get; set; }

        public Guid? Province_Index { get; set; }

        public Guid? Country_Index { get; set; }

        public Guid? Postcode_Index { get; set; }

        [StringLength(200)]
        public string UserAssign { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        [StringLength(200)]
        public string Confrim_By { get; set; }

        public DateTime? Confrim_Date { get; set; }
    }
}
