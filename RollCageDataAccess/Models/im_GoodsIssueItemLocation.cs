using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class im_GoodsIssueItemLocation
    {
        [Key]
        public Guid GoodsIssueItemLocation_Index { get; set; }

        public Guid GoodsIssue_Index { get; set; }


        public string LineNum { get; set; }

        public Guid? TagItem_Index { get; set; }

        public Guid? Tag_Index { get; set; }


        public string Tag_No { get; set; }

        public Guid Product_Index { get; set; }



        public string Product_Id { get; set; }



        public string Product_Name { get; set; }


        public string Product_SecondName { get; set; }


        public string Product_ThirdName { get; set; }


        public string Product_Lot { get; set; }

        public Guid? ItemStatus_Index { get; set; }



        public string ItemStatus_Id { get; set; }



        public string ItemStatus_Name { get; set; }

        public Guid? Location_Index { get; set; }


        public string Location_Id { get; set; }


        public string Location_Name { get; set; }


        public decimal? QtyPlan { get; set; }


        public decimal Qty { get; set; }


        public decimal Ratio { get; set; }


        public decimal TotalQty { get; set; }

        public Guid ProductConversion_Index { get; set; }



        public string ProductConversion_Id { get; set; }



        public string ProductConversion_Name { get; set; }


        public DateTime? MFG_Date { get; set; }


        public DateTime? EXP_Date { get; set; }


        public decimal? UnitWeight { get; set; }

        public Guid? UnitWeight_Index { get; set; }


        public string UnitWeight_Id { get; set; }


        public string UnitWeight_Name { get; set; }


        public decimal? UnitWeightRatio { get; set; }


        public decimal Weight { get; set; }

        public Guid? Weight_Index { get; set; }


        public string Weight_Id { get; set; }


        public string Weight_Name { get; set; }


        public decimal? WeightRatio { get; set; }


        public decimal? UnitNetWeight { get; set; }

        public Guid? UnitNetWeight_Index { get; set; }


        public string UnitNetWeight_Id { get; set; }


        public string UnitNetWeight_Name { get; set; }


        public decimal? UnitNetWeightRatio { get; set; }


        public decimal? NetWeight { get; set; }

        public Guid? NetWeight_Index { get; set; }


        public string NetWeight_Id { get; set; }


        public string NetWeight_Name { get; set; }


        public decimal? NetWeightRatio { get; set; }


        public decimal? UnitGrsWeight { get; set; }

        public Guid? UnitGrsWeight_Index { get; set; }


        public string UnitGrsWeight_Id { get; set; }


        public string UnitGrsWeight_Name { get; set; }


        public decimal? UnitGrsWeightRatio { get; set; }


        public decimal? GrsWeight { get; set; }

        public Guid? GrsWeight_Index { get; set; }


        public string GrsWeight_Id { get; set; }


        public string GrsWeight_Name { get; set; }


        public decimal? GrsWeightRatio { get; set; }


        public decimal UnitWidth { get; set; }

        public Guid? UnitWidth_Index { get; set; }


        public string UnitWidth_Id { get; set; }


        public string UnitWidth_Name { get; set; }


        public decimal? UnitWidthRatio { get; set; }


        public decimal? Width { get; set; }

        public Guid? Width_Index { get; set; }


        public string Width_Id { get; set; }


        public string Width_Name { get; set; }


        public decimal? WidthRatio { get; set; }


        public decimal UnitLength { get; set; }

        public Guid? UnitLength_Index { get; set; }


        public string UnitLength_Id { get; set; }


        public string UnitLength_Name { get; set; }


        public decimal? UnitLengthRatio { get; set; }


        public decimal? Length { get; set; }

        public Guid? Length_Index { get; set; }


        public string Length_Id { get; set; }


        public string Length_Name { get; set; }


        public decimal? LengthRatio { get; set; }


        public decimal UnitHeight { get; set; }

        public Guid? UnitHeight_Index { get; set; }


        public string UnitHeight_Id { get; set; }


        public string UnitHeight_Name { get; set; }


        public decimal? UnitHeightRatio { get; set; }


        public decimal? Height { get; set; }

        public Guid? Height_Index { get; set; }


        public string Height_Id { get; set; }


        public string Height_Name { get; set; }


        public decimal? HeightRatio { get; set; }


        public decimal UnitVolume { get; set; }


        public decimal Volume { get; set; }


        public decimal UnitPrice { get; set; }

        public Guid? UnitPrice_Index { get; set; }


        public string UnitPrice_Id { get; set; }


        public string UnitPrice_Name { get; set; }


        public decimal Price { get; set; }

        public Guid? Price_Index { get; set; }


        public string Price_Id { get; set; }


        public string Price_Name { get; set; }


        public string DocumentRef_No1 { get; set; }


        public string DocumentRef_No2 { get; set; }


        public string DocumentRef_No3 { get; set; }


        public string DocumentRef_No4 { get; set; }


        public string DocumentRef_No5 { get; set; }

        public int? Document_Status { get; set; }


        public string UDF_1 { get; set; }


        public string UDF_2 { get; set; }


        public string UDF_3 { get; set; }


        public string UDF_4 { get; set; }


        public string UDF_5 { get; set; }

        public Guid? Ref_Process_Index { get; set; }


        public string Ref_Document_No { get; set; }


        public string Ref_Document_LineNum { get; set; }

        public Guid? Ref_Document_Index { get; set; }

        public Guid? Ref_DocumentItem_Index { get; set; }

        public Guid GoodsReceiveItem_Index { get; set; }


        public string Create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }


        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }


        public DateTime? Cancel_Date { get; set; }

        public int? Picking_Status { get; set; }


        public string Picking_By { get; set; }


        public DateTime? Picking_Date { get; set; }


        public string Picking_Ref1 { get; set; }


        public string Picking_Ref2 { get; set; }


        public decimal? Picking_Qty { get; set; }


        public decimal? Picking_Ratio { get; set; }


        public decimal? Picking_TotalQty { get; set; }

        public Guid? Picking_ProductConversion_Index { get; set; }

        public int? Mashall_Status { get; set; }


        public decimal? Mashall_Qty { get; set; }

        public int? Cancel_Status { get; set; }


        public string GoodsIssue_No { get; set; }

        public Guid? BinBalance_Index { get; set; }


        public string Invoice_No { get; set; }


        public string Invoice_No_Out { get; set; }


        public string Declaration_No { get; set; }


        public string Declaration_No_Out { get; set; }


        public string HS_Code { get; set; }


        public string Conutry_of_Origin { get; set; }


        public decimal? Tax1 { get; set; }

        public Guid? Tax1_Currency_Index { get; set; }


        public string Tax1_Currency_Id { get; set; }


        public string Tax1_Currency_Name { get; set; }


        public decimal? Tax2 { get; set; }

        public Guid? Tax2_Currency_Index { get; set; }


        public string Tax2_Currency_Id { get; set; }


        public string Tax2_Currency_Name { get; set; }


        public decimal? Tax3 { get; set; }

        public Guid? Tax3_Currency_Index { get; set; }


        public string Tax3_Currency_Id { get; set; }


        public string Tax3_Currency_Name { get; set; }


        public decimal? Tax4 { get; set; }

        public Guid? Tax4_Currency_Index { get; set; }


        public string Tax4_Currency_Id { get; set; }


        public string Tax4_Currency_Name { get; set; }


        public decimal? Tax5 { get; set; }

        public Guid? Tax5_Currency_Index { get; set; }


        public string Tax5_Currency_Id { get; set; }


        public string Tax5_Currency_Name { get; set; }


        public string ERP_Location { get; set; }

        public virtual im_GoodsIssueItemLocation im_GoodsIssueItemLocation1 { get; set; }

        public virtual im_GoodsIssueItemLocation im_GoodsIssueItemLocation2 { get; set; }
    }
}
