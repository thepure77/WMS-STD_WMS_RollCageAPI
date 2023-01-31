using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{


    public partial class im_PlanGoodsIssue
    {

        [Key]
        public Guid PlanGoodsIssue_Index { get; set; }

        public Guid Owner_Index { get; set; }


        public string Owner_Id { get; set; }


        public string Owner_Name { get; set; }

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

        public Guid? DocumentType_Index { get; set; }


        public string DocumentType_Id { get; set; }


        public string DocumentType_Name { get; set; }


        public string PlanGoodsIssue_No { get; set; }


        public DateTime? PlanGoodsIssue_Date { get; set; }


        public DateTime? PlanGoodsIssue_Due_Date { get; set; }


        public string DocumentRef_No1 { get; set; }


        public string DocumentRef_No2 { get; set; }


        public string DocumentRef_No3 { get; set; }


        public string DocumentRef_No4 { get; set; }


        public string DocumentRef_No5 { get; set; }

        public string DocumentRef_No6 { get; set; }

        public string DocumentRef_No7 { get; set; }

        public string DocumentRef_No8 { get; set; }

        public string DocumentRef_No9 { get; set; }

        public string DocumentRef_No10 { get; set; }
        public string WMS_ID { get; set; }
        public string DOC_LINK { get; set; }
        public string Matdoc { get; set; }

        public string Document_Remark { get; set; }

        public int? Document_Status { get; set; }


        public string UDF_1 { get; set; }


        public string UDF_2 { get; set; }


        public string UDF_3 { get; set; }


        public string UDF_4 { get; set; }


        public string UDF_5 { get; set; }

        public int? DocumentPriority_Status { get; set; }

        public Guid? Warehouse_Index { get; set; }


        public string Warehouse_Id { get; set; }


        public string Warehouse_Name { get; set; }

        public Guid? Warehouse_Index_To { get; set; }


        public string Warehouse_Id_To { get; set; }


        public string Warehouse_Name_To { get; set; }

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


        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        public int? BackOrderStatus { get; set; }

        public Guid? Round_Index { get; set; }


        public string Round_Id { get; set; }


        public string Round_Name { get; set; }

        public Guid? Route_Index { get; set; }


        public string Route_Id { get; set; }


        public string Route_Name { get; set; }
        public Guid? SubRoute_Index { get; set; }


        public string SubRoute_Id { get; set; }


        public string SubRoute_Name { get; set; }

        public int? PrintTaxInvoice { get; set; }


        public string Payment_Type { get; set; }


        public string Payment_Code { get; set; }


        public decimal? Cal_GrandTotal { get; set; }

        public Guid? ReasonCode_Index { get; set; }


        public string ReasonCode_Id { get; set; }


        public string ReasonCode_Name { get; set; }

        public Guid? Ref_PlanGoodsIssue_Index { get; set; }


        public string Ref_PlanGoodsIssue_No { get; set; }


        public string Status_RMS { get; set; }


        public string Status_Desc_RMS { get; set; }


        public string Vendor_Id { get; set; }


        public DateTime? STPlanGoodsIssue_Due_Date { get; set; }


        public DateTime? Create_Date_File { get; set; }


        public string Status_EDI { get; set; }


        public string Status_Reason { get; set; }


        public string Round_Time { get; set; }


        public string Warehouse_Phone { get; set; }


        public string SoldTo_T1C { get; set; }


        public string SoldTo_T1CPhone { get; set; }


        public string SoldTo_Email { get; set; }


        public string SoldTo_Phone { get; set; }


        public string ShipTo_CompanyName { get; set; }


        public string ShipTo_Remark { get; set; }


        public string ShipTo_Telephone { get; set; }


        public string ShipTo_TaxNo { get; set; }


        public string Invoice_Name { get; set; }


        public string Invoice_CompanyName { get; set; }


        public string Invoice_Address { get; set; }


        public string Invoice_Remark { get; set; }


        public string Invoice_Telephone { get; set; }


        public string Invoice_TaxNo { get; set; }


        public string Payment_Issuer { get; set; }


        public decimal? Cal_PromotionDiscount { get; set; }


        public decimal? Cal_Cpn2Discount { get; set; }


        public decimal? Cal_Cpn9Discount { get; set; }


        public decimal? Cal_EvoucherDiscount { get; set; }


        public decimal? Cal_TotalAfterDiscount { get; set; }


        public string Document_Remark_Sub { get; set; }


        public string UserAssign { get; set; }


        public string SoldTo_Name_Cus { get; set; }


        public string SoldTo_Tel { get; set; }


        public string SoldTo_Email_Cus { get; set; }


        public string SoldTo_Phone_Cus { get; set; }


        public string SoldTo_Address_Cus { get; set; }

        public bool? IsPostPickConfirm { get; set; }


        public DateTime? IsPostPickConfirm_Date { get; set; }

        public bool? IsPostShippmentDispatch { get; set; }


        public DateTime? IsPostShippmentDispatch_Date { get; set; }


        public string StatusDropST { get; set; }

        public DateTime? StatusDropST_Date { get; set; }


        public string ShipTo_AddressName { get; set; }


        public string Invoice_AddressName { get; set; }

        public Guid? Ref_WavePick_index { get; set; }

        public string PlanGoodsIssue_Time { get; set; }

        public Guid? CostCenter_Index { get; set; }
        public string CostCenter_Id { get; set; }
        public string CostCenter_Name { get; set; }

        public Guid? Sloc_Index { get; set; }
        public string Sloc_Id { get; set; }
        public string Sloc_Name { get; set; }
        public Guid? MovementType_Index { get; set; }
        public string MovementType_Id { get; set; }
        public string MovementType_Name { get; set; }

        public Guid? ShippingTerms_Index { get; set; }
        public string ShippingTerms_Id { get; set; }
        public string ShippingTerms_Name { get; set; }
        public Guid? ShippingMethod_Index { get; set; }
        public string ShippingMethod_Id { get; set; }
        public string ShippingMethod_Name { get; set; }
        public string Shipping_Remark { get; set; }
        public Guid? PaymentType_Index { get; set; }
        public string PaymentType_Id { get; set; }
        public string PaymentType_Name { get; set; }
        public DateTime? PaymentType_Due_Date { get; set; }
        public string Credit_Term { get; set; }
        public Guid? Forwarder_Index { get; set; }
        public string Forwarder_Id { get; set; }
        public string Forwarder_Name { get; set; }
        public string Sales_Person { get; set; }
        public string Promotion_Code { get; set; }

        public Guid? Import_Index { get; set; }

    }
}
