using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RollCageDataAccess.Models
{

    public partial class im_RollCageOrder
    {
        [Key]
        public Guid RollCageOrder_Index { get; set; }

        public Guid RollCage_Index { get; set; }

        public string RollCage_Id { get; set; }

        public string RollCage_Name { get; set; }

        public string PlanGoodsIssue_No { get; set; }

        public DateTime PlanGoodsIssue_Date { get; set; }

        public DateTime? PlanGoodsIssue_Due_Date { get; set; }

        public Guid Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public Guid? DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public Guid SoldTo_Index { get; set; }

        public string SoldTo_Id { get; set; }

        public string SoldTo_Name { get; set; }

        public string SoldTo_Address { get; set; }

        public string SoldTo_Contact_Person { get; set; }

        public Guid ShipTo_Index { get; set; }

        public string ShipTo_Id { get; set; }

        public string ShipTo_Name { get; set; }

        public string ShipTo_Address { get; set; }

        public string ShipTo_Contact_Person { get; set; }

        public string Branch_Code { get; set; }

        public decimal? Box_No { get; set; }

        public decimal? Total_Box { get; set; }

        public Guid? Chute_Index { get; set; }

        public string Chute_Id { get; set; }

        public string Chute_Name { get; set; }

        public string ChuteUserCreate_By { get; set; }

        public DateTime? ChuteUserCreate_Date { get; set; }

        public Guid? GoodsIssue_Index { get; set; }

        public string GoodsIssue_No { get; set; }

        public DateTime? GoodsIssue_Date { get; set; }

        public Guid? Round_Index { get; set; }

        public string Round_Id { get; set; }

        public string Round_Name { get; set; }

        public Guid? Route_Index { get; set; }

        public string Route_Id { get; set; }

        public string Route_Name { get; set; }

        public Guid? SubRoute_Index { get; set; }

        public string SubRoute_Id { get; set; }

        public string SubRoute_Name { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public string DockUserCreate_By { get; set; }

        public DateTime? DockUserCreate_Date { get; set; }

        public Guid? TruckLoad_Index { get; set; }

        public string TruckLoad_No { get; set; }

        public DateTime? TruckLoad_Date { get; set; }

        public Guid? BoxType_Index { get; set; }

        public string BoxType_Id { get; set; }

        public string BoxType_Name { get; set; }

        public Guid? BoxSize_Index { get; set; }

        public string BoxSize_Id { get; set; }

        public string BoxSize_Name { get; set; }

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

        public string UserAssign { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        public string Confrim_By { get; set; }

        public DateTime? Confrim_Date { get; set; }
    }
}
