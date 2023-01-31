using DataAccess;
using RollCageBusiness.RollCage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RollCageBusiness.CheckTagoutWithRollcage
{
    public class CheckTagoutWithRollcageService
    {
        private RollCageDbContext db;

        public CheckTagoutWithRollcageService()
        {
            db = new RollCageDbContext();
        }

        public CheckTagoutWithRollcageService(RollCageDbContext db)
        {
            this.db = db;
        }

        #region CheckTagout
        public actionResultViewModel CheckTagout(SearchCheckTagoutWithRollcage model)
        {
            var ResultCheckout = new actionResultViewModel();
            try
            {
                var query = db.View_CheckTagoutWithRollcage.AsQueryable();

                if (!string.IsNullOrEmpty(model.goodsIssue_No))
                {
                    query = query.Where(c => c.GoodsIssue_No == model.goodsIssue_No);
                }

                var CheckGI = query.ToList();

                if (CheckGI.Count == 0)
                {
                    ResultCheckout.resultIsUse = false;
                    ResultCheckout.resultMsg = "ไม่สามารถแสดงข้อมูลใบเบิกสินค้าที่เกินกว่า 3 วัน";
                    return ResultCheckout;
                }

                if (!string.IsNullOrEmpty(model.locationType) && model.locationType != "ALL")
                {
                    query = query.Where(c => c.LocationType == model.locationType);
                }


                var flag = (model.isScan) ? "X" : "";
                query = query.Where(c => c.FlagScanIN == flag);
       

                var resultquery = query.OrderBy(c => c.LocationType).ToList() ;
                if (resultquery.Count == 0)
                {
                    ResultCheckout.resultIsUse = false;
                    ResultCheckout.resultMsg = (model.isScan) ? "ยังไม่มีข้อมูลที่สแกน" : "สแกนทั้งหมดเเล้ว";
                    return ResultCheckout;
                }

                var result = new List<ResultCheckTagoutWithRollcage>();

                foreach (var item in resultquery)
                {
                    var resultItem = new ResultCheckTagoutWithRollcage();
                    resultItem.goodsIssue_no = item.GoodsIssue_No;
                    resultItem.truck_no = item.TruckLoad_No;
                    resultItem.location_type = item.LocationType;
                    resultItem.chute = item.Chute_No;
                    resultItem.tag = item.TagOut_No;
                    resultItem.productid = item.Product_Id;
                    resultItem.product_name = item.Product_Name;
                    resultItem.flag_scan = (item.FlagScanIN == "X") ? "สแกนแล้ว" : "ยังไม่ได้สแกน";
                    resultItem.rollcage = item.RollCage_Id;
                    resultItem.branch_code = item.BranchCode;
                    resultItem.drop_order_seq = item.DropSeq + "/" + item.OrderSeq;
                    resultItem.plangoodsissue_no = item.PlanGoodsIssue_No;


                    result.Add(resultItem);
                }

                ResultCheckout.itemsCheckout = result.ToList();
                ResultCheckout.resultIsUse = true;

                return ResultCheckout;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
    }
}
