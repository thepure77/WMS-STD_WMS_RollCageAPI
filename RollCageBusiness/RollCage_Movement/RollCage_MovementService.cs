using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Comone.Utils;
using RollCageDataAccess.Models;
using System.Data;


namespace RollCageBusiness.RollCage_Movement
{
    public class RollCage_MovementService
    {


        private RollCageDbContext db;

        public RollCage_MovementService()
        {
            db = new RollCageDbContext();
        }

        public RollCage_MovementService(RollCageDbContext db)
        {
            this.db = db;
        }

        #region find
        public List<RollCage_MovementViewModel> find(Guid id)
        {

            try
            {
                var result = new List<RollCage_MovementViewModel>();

                var queryResult = db.im_RollCage_Movement.Where(c => c.RollCage_Index == id && c.RollCage_Movement_Status != -1).ToList();

                foreach (var item in queryResult.OrderBy(c => c.RollCage_Id ))
                {
                    var resultItem = new RollCage_MovementViewModel();

                    //resultItem.planGoodsIssueItem_Index = item.PlanGoodsIssueItem_Index;
                    //resultItem.planGoodsIssue_Index = item.PlanGoodsIssue_Index;
                    //resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                    //resultItem.lineNum = item.LineNum;
                    //resultItem.product_Index = item.Product_Index;
                    //resultItem.product_Id = item.Product_Id;
                    //resultItem.product_Name = item.Product_Name;
                    //resultItem.product_SecondName = item.Product_SecondName;
                    //resultItem.product_ThirdName = item.Product_ThirdName;
                    //resultItem.product_Lot = item.Product_Lot;
                    //resultItem.itemStatus_Index = item.ItemStatus_Index;
                    //resultItem.itemStatus_Id = item.ItemStatus_Id;
                    //resultItem.itemStatus_Name = item.ItemStatus_Name;
                    //resultItem.qty = string.Format(String.Format("{0:N0}", item.Qty));
                    //resultItem.ratio = item.Ratio;
                    //resultItem.totalQty = item.TotalQty;
                    //resultItem.productConversion_Index = item.ProductConversion_Index;
                    //resultItem.productConversion_Id = item.ProductConversion_Id;
                    //resultItem.productConversion_Name = item.ProductConversion_Name;
                    //resultItem.mfg_Date = item.MFG_Date.toString();
                    //resultItem.exp_Date = item.EXP_Date.toString();
                    //resultItem.unitWeight = item.UnitWeight;
                    //resultItem.unitWeight_Index = item.UnitWeight_Index;
                    //resultItem.unitWeight_Id = item.UnitWeight_Id;
                    //resultItem.unitWeight_Name = item.UnitWeight_Name;
                    //resultItem.unitWeightRatio = item.UnitWeightRatio;
                    //resultItem.weight = item.Weight;
                    //resultItem.weight_Index = item.Weight_Index;
                    //resultItem.weight_Id = item.Weight_Id;
                    //resultItem.weight_Name = item.Weight_Name;
                    //resultItem.weightRatio = item.WeightRatio;
                    //resultItem.unitNetWeight = item.UnitNetWeight;
                    //resultItem.unitNetWeight_Index = item.UnitNetWeight_Index;
                    //resultItem.unitNetWeight_Id = item.UnitNetWeight_Id;
                    //resultItem.unitNetWeight_Name = item.UnitNetWeight_Name;
                    //resultItem.unitNetWeightRatio = item.UnitNetWeightRatio;
                    //resultItem.netWeight = item.NetWeight;
                    //resultItem.netWeight_Index = item.NetWeight_Index;
                    //resultItem.netWeight_Id = item.NetWeight_Id;
                    //resultItem.netWeight_Name = item.NetWeight_Name;
                    //resultItem.netWeightRatio = item.NetWeightRatio;
                    //resultItem.unitGrsWeight = item.UnitGrsWeight;
                    //resultItem.unitGrsWeight_Index = item.UnitGrsWeight_Index;
                    //resultItem.unitGrsWeight_Id = item.UnitGrsWeight_Id;
                    //resultItem.unitGrsWeight_Name = item.UnitGrsWeight_Name;
                    //resultItem.unitGrsWeightRatio = item.UnitGrsWeightRatio;
                    //resultItem.grsWeight = item.GrsWeight;
                    //resultItem.grsWeight_Index = item.GrsWeight_Index;
                    //resultItem.grsWeight_Id = item.GrsWeight_Id;
                    //resultItem.grsWeight_Name = item.GrsWeight_Name;
                    //resultItem.grsWeightRatio = item.GrsWeightRatio;
                    //resultItem.unitWidth = item.UnitWidth;
                    //resultItem.unitWidth_Index = item.UnitWidth_Index;
                    //resultItem.unitWidth_Id = item.UnitWidth_Id;
                    //resultItem.unitWidth_Name = item.UnitWidth_Name;
                    //resultItem.unitWidthRatio = item.UnitWidthRatio;
                    //resultItem.width = item.Width;
                    //resultItem.width_Index = item.Width_Index;
                    //resultItem.width_Id = item.Width_Id;
                    //resultItem.width_Name = item.Width_Name;
                    //resultItem.widthRatio = item.WidthRatio;
                    //resultItem.unitLength = item.UnitLength;
                    //resultItem.unitLength_Index = item.UnitLength_Index;
                    //resultItem.unitLength_Id = item.UnitLength_Id;
                    //resultItem.unitLength_Name = item.UnitLength_Name;
                    //resultItem.unitLengthRatio = item.UnitLengthRatio;
                    //resultItem.length = item.Length;
                    //resultItem.length_Index = item.Length_Index;
                    //resultItem.length_Id = item.Length_Id;
                    //resultItem.length_Name = item.Length_Name;
                    //resultItem.lengthRatio = item.LengthRatio;
                    //resultItem.unitHeight = item.UnitHeight;
                    //resultItem.unitHeight_Index = item.UnitHeight_Index;
                    //resultItem.unitHeight_Id = item.UnitHeight_Id;
                    //resultItem.unitHeight_Name = item.UnitHeight_Name;
                    //resultItem.unitHeightRatio = item.UnitHeightRatio;
                    //resultItem.height = item.Height;
                    //resultItem.height_Index = item.Height_Index;
                    //resultItem.height_Id = item.Height_Id;
                    //resultItem.height_Name = item.Height_Name;
                    //resultItem.heightRatio = item.HeightRatio;
                    //resultItem.unitVolume = item.UnitVolume;
                    //resultItem.volume = item.Volume;
                    //resultItem.unitPrice = item.UnitPrice;
                    //resultItem.unitPrice_Index = item.UnitPrice_Index;
                    //resultItem.unitPrice_Id = item.UnitPrice_Id;
                    //resultItem.unitPrice_Name = item.UnitPrice_Name;
                    //resultItem.price = item.Price;
                    //resultItem.price_Index = item.Price_Index;
                    //resultItem.price_Id = item.Price_Id;
                    //resultItem.price_Name = item.Price_Name;
                    //resultItem.documentRef_No1 = item.DocumentRef_No1;
                    //resultItem.documentRef_No2 = item.DocumentRef_No2;
                    //resultItem.documentRef_No3 = item.DocumentRef_No3;
                    //resultItem.documentRef_No4 = item.DocumentRef_No4;
                    //resultItem.documentRef_No5 = item.DocumentRef_No5;
                    //resultItem.document_Status = item.Document_Status;
                    //resultItem.documentItem_Remark = item.DocumentItem_Remark;
                    //resultItem.uDF_1 = item.UDF_1;
                    //resultItem.uDF_2 = item.UDF_2;
                    //resultItem.uDF_3 = item.UDF_3;
                    //resultItem.uDF_4 = item.UDF_4;
                    //resultItem.uDF_5 = item.UDF_5;
                    //resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;

                    result.Add(resultItem);

                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
