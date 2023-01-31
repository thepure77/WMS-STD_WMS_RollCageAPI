using DataAccess;
using System;
using RollCageBusiness.FilmCutting;
using Business.Library;
using Comone.Utils;
using System.Linq;

namespace RollCageBusiness.RollCage
{
    public class FilmCuttingService
    {
        private RollCageDbContext db;
        private MasterDbContext dbMaster;

        public FilmCuttingService()
        {
            db = new RollCageDbContext();
            dbMaster = new MasterDbContext();
        }

        public FilmCuttingService(RollCageDbContext db)
        {
            this.db = db;
        }

        #region findtag
        public FilmCuttingViewModel findtag(FilmCuttingViewModel data)
        {
            FilmCuttingViewModel model = new FilmCuttingViewModel();
            try
            {
                
                model.tag_no = data.tag_no;
                model.docNo = data.tag_no;
                model.station = data.value;
                var result = utils.SendDataApi<bool>(new AppSettingConfig().GetUrl("findtagno_filmcut"), model.sJson());
                if (result)
                {
                    var getBinbalance = utils.SendDataApi<BinBalanceViewModel>(new AppSettingConfig().GetUrl("getBinbalance"), model.sJson());
                    if (getBinbalance != null)
                    {
                        model.resultIsUse = true;
                        model.product_id = getBinbalance.product_Id;
                        model.product_name = getBinbalance.product_Name;
                    }
                    else {
                        model.resultIsUse = false;
                        model.resultMsg = "ไม่พบ Tag ที่ทำการแสกน";
                    }
                }
                else {
                    model.resultIsUse = false;
                    model.resultMsg = "ไม่พบ Tag ที่ทำการแสกน";
                }

                
                return model;
            }
            catch (Exception ex)
            {
                model.resultIsUse = false;
                model.resultMsg = ex.Message;
                return model;
            }
        }
        #endregion

        #region Cutting
        public Result  Cutting(FilmCuttingViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "data : " + data.sJson());
            try
            {
                Result result = new Result();
                FilmCuttingViewModel model = new FilmCuttingViewModel();
                model.docNo = data.tag_no;
                model.station = data.value;
                //string result = utils.SendDataApi<String>(new AppSettingConfig().GetUrl("FilmCutting"), model.sJson());
                //var result = "SUCCESS";
                //if (result == "SUCCESS")
                //{
                log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 1 : " + DateTime.Now);
                log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "model : " + model.sJson());
                var resultDataFilmcut = utils.SendDataApi<bool>(new AppSettingConfig().GetUrl("findtagno_filmcut"), model.sJson());
                if (!resultDataFilmcut)
                {
                    log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 1 Error : " + resultDataFilmcut);
                    result.resultIsUse = false;
                    result.resultMsg = "ไม่พบ Tag No ที่ ค้นหา";
                    log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 1 Error : " + result.resultMsg);
                    return result;
                }
                log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 2 : " + DateTime.Now);
                var gettask = db.Im_TaskItem.Where(c=> c.Tag_No == data.tag_no && (c.Picking_Status == null ? 0 : c.Picking_Status) == 0 && c.Document_Status == 0).ToList();
                if (gettask.Count <= 0)
                {
                    
                    result.resultIsUse = false;
                    result.resultMsg = "ไม่พบ Tag No ที่ ค้นหา";
                    log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 2 Error : " + result.resultMsg);
                    return result;
                }
                else {
                    var get_barcode = dbMaster.MS_ProductConversionBarcode.FirstOrDefault(c => c.Product_Index == gettask[0].Product_Index && c.ProductConversion_Index == gettask[0].ProductConversion_Index);
                    if (get_barcode == null)
                    {
                        result.resultIsUse = false;
                        result.resultMsg = "ไม่พบ Tag No ที่ ค้นหา";
                        return result;
                    }
                    ScanPickViewModel saveScanPickViewModel = new ScanPickViewModel();
                    saveScanPickViewModel.task_Index = gettask.FirstOrDefault().Task_Index.ToString();
                    saveScanPickViewModel.tag_Index = gettask.FirstOrDefault().Tag_Index;
                    saveScanPickViewModel.product_Id = gettask.FirstOrDefault().Product_Id;
                    saveScanPickViewModel.product_Index = gettask.FirstOrDefault().Product_Index;
                    saveScanPickViewModel.picking_Qty = gettask.Sum(c=> c.Qty ?? 0);
                    saveScanPickViewModel.pick_ProductConversion_Ratio = gettask.FirstOrDefault().Ratio;
                    saveScanPickViewModel.userName = "AS/RS";
                    saveScanPickViewModel.planGoodsIssue_Index = gettask.FirstOrDefault().PlanGoodsIssue_Index;
                    saveScanPickViewModel.planGoodsIssueItem_Index = gettask.FirstOrDefault().PlanGoodsIssueItem_Index;
                    saveScanPickViewModel.planGoodsIssue_No = gettask.FirstOrDefault().PlanGoodsIssue_No;
                    saveScanPickViewModel.pick_ProductConversion_Index = gettask.FirstOrDefault().ProductConversion_Index;
                    saveScanPickViewModel.pick_ProductConversion_Id = gettask.FirstOrDefault().ProductConversion_Id;
                    saveScanPickViewModel.pick_ProductConversion_Name = gettask.FirstOrDefault().ProductConversion_Name;
                    saveScanPickViewModel.productConversionBarcode = get_barcode.ProductConversionBarcode;
                    saveScanPickViewModel.tagOut_Index = gettask.FirstOrDefault().TagOut_Index;
                    saveScanPickViewModel.tagOut_No = gettask.FirstOrDefault().TagOut_No;
                    saveScanPickViewModel.reasonCode_Index = null;
                    saveScanPickViewModel.ref_Document_Index = gettask.FirstOrDefault().Ref_Document_Index;
                    saveScanPickViewModel.ref_DocumentItem_Index = gettask.FirstOrDefault().Ref_DocumentItem_Index;
                    saveScanPickViewModel.product_Name = gettask.FirstOrDefault().Product_Name;
                    saveScanPickViewModel.product_SecondName = gettask.FirstOrDefault().Product_SecondName;
                    saveScanPickViewModel.product_ThirdName = gettask.FirstOrDefault().Product_ThirdName;
                    saveScanPickViewModel.product_Lot = gettask.FirstOrDefault().Product_Lot;
                    saveScanPickViewModel.itemStatus_Index = gettask.FirstOrDefault().ItemStatus_Index;
                    saveScanPickViewModel.itemStatus_Id = gettask.FirstOrDefault().ItemStatus_Id;
                    saveScanPickViewModel.itemStatus_Name = gettask.FirstOrDefault().ItemStatus_Name;
                    saveScanPickViewModel.pick_Qty = gettask.Sum(c => c.Qty ?? 0);
                    saveScanPickViewModel.picking_Ratio = gettask.FirstOrDefault().Ratio;
                    saveScanPickViewModel.mfg_Date = gettask.FirstOrDefault().MFG_Date;
                    saveScanPickViewModel.exp_Date = gettask.FirstOrDefault().EXP_Date;
                    saveScanPickViewModel.weight = gettask.FirstOrDefault().Weight;
                    saveScanPickViewModel.volume = gettask.FirstOrDefault().Volume;
                    saveScanPickViewModel.task_No = gettask.FirstOrDefault().Task_No;
                    actionResultScanPickViewModel resultScanPick = utils.SendDataApi<actionResultScanPickViewModel>(new AppSettingConfig().GetUrl("ScanPick"), saveScanPickViewModel.sJson());
                    if (resultScanPick.resultIsUse)
                    {
                        ScanPickViewModel saveScanLocationViewModel = new ScanPickViewModel();
                        saveScanLocationViewModel.location_Id = "WH7TOASRS";
                        saveScanLocationViewModel.location_Index = Guid.Parse("DF3D2410-FCBD-4C82-9F2E-65EE51A83F51");
                        saveScanLocationViewModel.location_Name = "WH7TOASRS";
                        saveScanLocationViewModel.task_Index = gettask.FirstOrDefault().Task_Index.ToString();
                        saveScanLocationViewModel.userName = "AS/RS";
                        log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 3  : " + saveScanLocationViewModel.sJson());
                        bool resultScanLocation = utils.SendDataApi<bool>(new AppSettingConfig().GetUrl("ScanConfirmLocation"), saveScanLocationViewModel.sJson());

                        if (!resultScanLocation)
                        {
                            log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 3 Error : " + resultScanLocation);
                            result.resultIsUse = false;
                            result.resultMsg = "ไม่สามารถทำการตัด Pallet ได้";
                            log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 3 Error : " + result.resultMsg);
                            return result;
                        }
                        else
                        {
                            //return "T";
                            log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 4  : Send WCS" + model.sJson());
                            string result_wcs = utils.SendDataApi<String>(new AppSettingConfig().GetUrl("FilmCutting"), model.sJson());
                            log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "step 4  : Send WCS Result" + result_wcs);
                            if (result_wcs == "ไม่พบเลขที่ Pallet นี้้")
                            {
                                result.resultIsUse = false;
                                result.resultMsg = result_wcs;
                            }
                            else {
                                result.resultIsUse = true;
                            }
                            
                            return result;
                        }

                    }
                    else {

                        result.resultIsUse = false;
                        result.resultMsg = "ไม่พบ Tag No ที่ ค้นหา";
                        return result;
                    }
                }
                //}

            }
            catch (Exception ex)
            {
                log.DataLogLines("Cutting", "Cutting" + DateTime.Now.ToString("yyyy-MM-dd"), "EX  : " + ex.Message);
                throw ex;
            }
        }
        #endregion



    }
}
