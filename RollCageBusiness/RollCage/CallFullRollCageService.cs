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
using RollCageBusiness.PlanGoodsIssue;
using Business.Library;
using Newtonsoft.Json;
using static RollCageBusiness.PlanGoodsIssue.CallFullRollCageViewModel;
using System.Threading;

namespace RollCageBusiness.RollCage
{
    public class CallFullRollCageService
    {
        #region
        private RollCageDbContext db;
        private MasterDbContext dbMaster;

        public CallFullRollCageService()
        {
            db = new RollCageDbContext();
            dbMaster = new MasterDbContext();
        }

        public CallFullRollCageService(RollCageDbContext db)
        {
            this.db = db;
        }
        #endregion

        #region ScanLocation
        public CallFullRollCageViewModel ScanLocation(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                
                var result = new CallFullRollCageViewModel();
                log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "fine Location : " + data.Location);
                var getcontinue = dbMaster.ms_RollCage.Where(c => c.Location_Name_Before == data.Location).ToList();
                log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Result fine Location : " + getcontinue.Count + " : "+DateTime.Now);
                if (getcontinue.Count > 0)
                {
                    log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + DateTime.Now);
                    result.mess = "E";
                    return result;
                }
                var DataLocation = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == Guid.Parse("64341969-E596-4B8B-8836-395061777490") && c.Location_Name == data.Location).ToList();
                
                if (DataLocation.Count > 0)
                {
                    DataLocation = DataLocation.Where(c => c.RollCage_Index == null).ToList(); // ถ้าเจอคือไม่ว่าง
                    if (DataLocation.Count <= 0)
                    {
                        //result.mess = "E";
                        result.mess = "Location ที่ค้นหาไม่ว่าง";
                        log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + result.mess);
                    }
                }
                else
                {
                    result.mess = "ไม่พบ Location ที่ค้นหา";
                    log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + result.mess);
                }
                return result;
            }
            catch (Exception ex)
            {
                log.DataLogLines("ScanLocation", "ScanLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Exception : " + ex.Message + " : " + DateTime.Now);
                throw ex;
            }
        }
        #endregion

        #region scanEmptyLocation
        public CallFullRollCageViewModel scanEmptyLocation(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                var result = new CallFullRollCageViewModel();
                log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "fine Location : " + data.Location);
                var DataLocation = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == Guid.Parse("A706D789-F5C9-41A6-BEC7-E57034DFC166") && c.Location_Name == data.Location).ToList();
                if (DataLocation.Count > 0)
                {
                    DataLocation = DataLocation.Where(c => c.RollCage_Index == null).ToList();
                    if (DataLocation.Count <= 0)
                    {
                        //result.mess = "E";
                        result.mess = "Location ที่ค้นหาไม่ว่าง";
                        log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + result.mess);
                    }
                }
                else
                {
                    result.mess = "ไม่พบ Location ที่ค้นหา";
                    log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + result.mess);
                }
                return result;
            }
            catch (Exception ex)
            {
                log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Exception : " + ex.Message + " : " + DateTime.Now);
                throw ex;
            }
        }
        #endregion

        #region scanEmptyLocation
        public CallFullRollCageViewModel scanEmptyLocation_staging(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                List<View_LocationRollCage> DataLocation = new List<View_LocationRollCage>();
                var result = new CallFullRollCageViewModel();
                log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "fine Location : " + data.Location);
                if (data.value == "3")
                {
                    DataLocation = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == Guid.Parse("A706D789-F5C9-41A6-BEC7-E57034DFC166") && c.Location_Name == data.Location).ToList();
                }
                else {
                    DataLocation = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == Guid.Parse("94D86CEA-3D04-4304-9E97-28E954F03C35") && c.Location_Name == data.Location).ToList();
                }
                 
                if (DataLocation.Count > 0)
                {
                    DataLocation = DataLocation.Where(c => c.RollCage_Index == null).ToList();
                    if (DataLocation.Count <= 0)
                    {
                        //result.mess = "E";
                        result.mess = "Location ที่ค้นหาไม่ว่าง";
                        log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + result.mess);
                    }
                }
                else
                {
                    result.mess = "ไม่พบ Location ที่ค้นหา";
                    log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Error fine Location : " + data.Location + " : " + result.mess);
                }
                return result;
            }
            catch (Exception ex)
            {
                log.DataLogLines("scanEmptyLocation", "scanEmptyLocation" + DateTime.Now.ToString("yyyy-MM-dd"), "Exception : " + ex.Message + " : " + DateTime.Now);
                throw ex;
            }
        }
        #endregion

        #region CallRollCage
        public CallFullRollCageViewModel CallRollCage(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                var result = new CallFullRollCageViewModel();
                log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Move To Location : " + data.Location);
                var location_to = dbMaster.View_LocationRollCage.FirstOrDefault(c => c.Location_Name == data.Location);
                log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Check Rollcage : " + data.rollcage_id);
                var checkrollcage = dbMaster.ms_RollCage.Where(c => c.RollCage_Id == data.rollcage_id).ToList();
                if (checkrollcage.Count <= 0)
                {
                    result.mess = "ไม่พบ Rollcage Id : "+ data.rollcage_id;
                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error Check Rollcage : " + result.mess);
                    return result;
                }

                var checkrollcage_location = dbMaster.ms_RollCage.Where(c => c.RollCage_Id == data.rollcage_id && c.Location_Name.Contains("DK")).ToList();
                if (checkrollcage_location.Count > 0)
                {
                    result.mess = "Rollcage ไม่อยู่ในตำแหน่ง ที่ถูกเรียกได้ : " + data.rollcage_id;
                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error Check Rollcage : " + result.mess);
                    return result;
                }

                var tmp_SuggestRoll = dbMaster.tmp_SuggestRollCageLocation.Where(c => c.RollCage_ID == data.rollcage_id).ToList();
                if (tmp_SuggestRoll.Count > 0)
                {
                    result.mess = "Rollcage ไม่อยู่ในตำแหน่ง ที่ถูกเรียกได้ : " + data.rollcage_id;
                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error Check Rollcage : " + result.mess);
                    return result;
                }

                if (!string.IsNullOrEmpty(data.Shipment_no))
                {
                    var rollcageOrder = db.im_RollCageOrder.Where(c => c.RollCage_Id == data.rollcage_id && c.TruckLoad_No == data.Shipment_no && !string.IsNullOrEmpty(c.UDF_3)).ToList();
                    if (rollcageOrder.Count > 0)
                    {
                        result.mess = "Rollcage เคยถูกเรียกไปแล้ว : " + data.rollcage_id;
                        log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error Check Rollcage : " + result.mess);
                        return result;
                    }


                }
                
                if (data.location_rollcage == location_to.Location_Id)
                {
                    return result;
                }

                var moveRollCageModel = new
                {
                    rollCageID = data.rollcage_id,
                    movementType = 2,
                    sourceStation = Convert.ToInt16(data.location_rollcage),
                    destStation = Convert.ToInt16(location_to.Location_Id),
                    dock_Index = "",
                    dock_Id = "",
                    dock_Name = ""
                };
                log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "createMoveRollCage : " + JsonConvert.SerializeObject(moveRollCageModel));

                CallFullRollCageViewModel response_MoveRollCage = utils.SendDataApi<CallFullRollCageViewModel>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));
                log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "createMoveRollCage Status : " + response_MoveRollCage.status);
                if (response_MoveRollCage.status == "10") {

                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Rollcage : ");
                    var Update_rollcageOrder = db.im_RollCageOrder.Where(c => c.RollCage_Id == data.rollcage_id && c.TruckLoad_No == data.Shipment_no).ToList();
                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Rollcage : " + Update_rollcageOrder.Count);
                    foreach (var item in Update_rollcageOrder)
                    {
                        item.UDF_3 = "1";
                        item.UDF_4 = "Call Rollcage";
                    }
                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Rollcage : transactionX");
                    var transactionX = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        db.SaveChanges();
                        transactionX.Commit();
                        log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Rollcage : transactionX : True");
                    }
                    catch (Exception ex)
                    {
                        log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Rollcage : transactionX : ex : " + ex.Message);
                        transactionX.Rollback();
                        throw ex;
                    }

                    RollCageService rollCageService = new RollCageService();
                    LocationRollCageViewModel model = new LocationRollCageViewModel();

                    model.rollCage_Index = data.RollCage_Index;
                    model.location_Index_To = location_to.Location_Index;
                    model.location_Id_To = location_to.Location_Id;
                    model.location_Name_To = location_to.Location_Name;

                    var update_location = rollCageService.updateLocationRollCage(model);
                    if (!update_location)
                    {
                        result.mess = "ไม่สามารถอัปเดตที่อยู่ได้";
                        log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error : " + result.mess);
                    }

                    var modelRollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Id == data.rollcage_id);
                    modelRollCage.Location_Name_Before = data.Location;

                    var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        dbMaster.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                } else {
                    result.mess = "ไม่สามารถทำการเรียกได้";
                    log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error : " + result.mess);
                }


                return result;
            }
            catch (Exception ex)
            {
                log.DataLogLines("CallRollCage", "CallRollCage" + DateTime.Now.ToString("yyyy-MM-dd"), "Error : " + ex.Message);
                throw ex;
            }
        }
        #endregion
        
        #region Scanshipment
        public List<CallFullRollCageViewModel> Scanshipment(CallFullRollCageViewModel data)
        {
            var result = new List<CallFullRollCageViewModel>();
            try
            {
                var checkDock = dbMaster.View_DockLocationRollCage.Where(c => c.Ref_Document_No == data.Shipment_no && c.Location_Name == data.Location).ToList();
                if (checkDock.Count == 0)
                {
                    var result_mess = new CallFullRollCageViewModel();
                    result_mess.resultIsUse = false;
                    result_mess.resultMsg = "Dock ที่จะทำการเรียก Rollcage ไม่ถูกต้อง";
                    result.Add(result_mess);
                    return result;
                } else if (checkDock[0].CheckIn_Index == null) {
                    var result_mess = new CallFullRollCageViewModel();
                    result_mess.resultIsUse = false;
                    result_mess.resultMsg = "กรุณาทำการ Checkin Booking ก่อนทำการเรียก";
                    result.Add(result_mess);
                    return result;
                } else if (checkDock[0].CheckOut_Index != null)
                {
                    var result_mess = new CallFullRollCageViewModel();
                    result_mess.resultIsUse = false;
                    result_mess.resultMsg = "Shipment ที่จะทำการเรียก Rollcage ทำการ Checkout ไปแล้ว";
                    result.Add(result_mess);
                    return result;
                }
                var checkall = false;
                
                var findshipment = (from TL in db.im_TruckLoad.AsQueryable()
                                    join TLI in db.im_TruckLoadItem.AsQueryable() on TL.TruckLoad_Index equals TLI.TruckLoad_Index
                                    join PGI in db.im_PlanGoodsIssue.AsQueryable() on TLI.PlanGoodsIssue_Index equals PGI.PlanGoodsIssue_Index
                                    join TGO in db.wm_TagOut.AsQueryable() on PGI.PlanGoodsIssue_Index equals Guid.Parse(TGO.UDF_4)
                                    select new
                                    {
                                        TL.TruckLoad_No,
                                        TL.TruckLoad_Index,
                                        TLI.PlanGoodsIssue_No,
                                        PGI.PlanGoodsIssue_Index,
                                        PGI.Route_Name,
                                        TGO.TagOut_No,
                                        TGO.LocationType
                                    }).Where(c=> c.TruckLoad_No == data.Shipment_no && (c.LocationType != "Selective" && c.LocationType != "ByPass")).GroupBy(c=> new {
                                        c.TruckLoad_No,
                                        c.TruckLoad_Index,
                                        c.PlanGoodsIssue_No,
                                        c.PlanGoodsIssue_Index,
                                        c.Route_Name,
                                        c.TagOut_No
                                    }).Select(c=> new {
                                        c.Key.TruckLoad_No,
                                        c.Key.TruckLoad_Index,
                                        c.Key.PlanGoodsIssue_No,
                                        c.Key.PlanGoodsIssue_Index,
                                        c.Key.Route_Name,
                                        c.Key.TagOut_No
                                    }).ToList();

                

                var TruckLoad = findshipment.GroupBy(c => c.TruckLoad_Index).Select(c => c.Key).ToList();
                var PlanGoodsIssue = findshipment.GroupBy(c => c.PlanGoodsIssue_No).Select(c => c.Key).ToList();

                var getrollcage = db.im_RollCageOrder.Where(c => PlanGoodsIssue.Contains(c.PlanGoodsIssue_No)).GroupBy(c=> new {
                    c.RollCage_Index,
                    c.Document_Status,
                    c.RollCage_Id,
                }).Select(c=> new {
                    c.Key.RollCage_Index,
                    c.Key.RollCage_Id,
                    c.Key.Document_Status,
                }).ToList();

                var getrollcagecount = getrollcage;

                if (data.again)
                {
                    var location_rollcage = dbMaster.ms_RollCage.Where(c => c.Location_Name_Before == data.Location);
                    getrollcagecount = getrollcage.Where(c => location_rollcage.Select(x=> x.RollCage_Index).Contains(c.RollCage_Index) && c.Document_Status != -1 && c.Document_Status == 0).ToList();
                    if (getrollcagecount.Count() <= 0 )
                    {
                        getrollcagecount = getrollcage.Where(c => location_rollcage.Select(x => x.RollCage_Index).Contains(c.RollCage_Index) && c.Document_Status == 1).ToList();
                        if (getrollcagecount.Count() > 0)
                        {
                            checkall = true;
                        }
                        
                    }
                }
                else
                {
                    getrollcagecount = getrollcage.Where(c => c.Document_Status == 0).ToList();
                }

                var CountRollCageOrder = db.im_RollCageOrder.Where(c => PlanGoodsIssue.Contains(c.PlanGoodsIssue_No) && c.Document_Status == 0 ).ToList();


                foreach (var item in getrollcagecount)
                {

                    var RollCageOrder = CountRollCageOrder.Where(c => c.RollCage_Index == item.RollCage_Index).ToList();

                    var locationrollcage = dbMaster.View_LocationRollCage.FirstOrDefault(c => c.RollCage_Index == item.RollCage_Index);
                    var resultitem = new CallFullRollCageViewModel();
                    resultitem.bill_amount = findshipment.GroupBy(c => c.PlanGoodsIssue_No).Count().ToString();
                    resultitem.route_Name = findshipment[0].Route_Name;
                    resultitem.qty_tagBox = findshipment.GroupBy(c => c.TagOut_No).Count().ToString();
                    resultitem.qty_tagTote = "0";

                    resultitem.RollCage_Index = item.RollCage_Index;
                    if (locationrollcage != null)
                    {
                        if (locationrollcage.LocationType_Id == "1" || locationrollcage.LocationType_Id == "2" || locationrollcage.LocationType_Id == "3" || locationrollcage.LocationType_Id == "4")
                        {
                            continue;
                        }
                        resultitem.location_rollcage = locationrollcage.Location_Id;
                        resultitem.location_rollcage_Name = locationrollcage.Location_Name;
                    }
                    resultitem.document_Status = item.Document_Status == 0 ? "Stored" : "Arrived";
                    resultitem.rollcage_id = item.RollCage_Id;
                    resultitem.carton = RollCageOrder.Count().ToString();
                    resultitem.tote_box = "0";
                    resultitem.TruckLoad_No = findshipment[0].TruckLoad_No;
                    resultitem.checkall = checkall;
                    resultitem.resultIsUse = true;
                    result.Add(resultitem);
                }




                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Scantagout
        public CallFullRollCageViewModel Scantagout(CallFullRollCageViewModel data)
        {
            try
            {
                var result = new CallFullRollCageViewModel();

                var rollcageby_id = db.im_RollCageOrder.Where(c => c.RollCage_Index == data.RollCage_Index && c.Document_Status != -1 && c.Document_Status == 0 && c.TruckLoad_No == data.TruckLoad_No).ToList();
                if (rollcageby_id.Count > 0)
                {
                    im_RollCageOrder update_roll = rollcageby_id.FirstOrDefault(c => c.DocumentRef_No1 == data.tagout_no);

                    if (update_roll != null)
                    {
                        if (update_roll.Document_Status == 1)
                        {
                            result.mess = " QR นี้ถูกแสกน ไปแล้ว!! ";
                            return result;
                        }
                        update_roll.Document_Status = 1;
                        update_roll.Update_By = data.user;
                        update_roll.Update_Date = DateTime.Now;

                        wm_TagOut update_tagout = db.wm_TagOut.FirstOrDefault(c => c.TagOut_No == data.tagout_no && c.TagOut_Status == 1);

                        if (update_tagout == null)
                        {
                            result.mess = " QR นี้ยังไม่ถูก Scan In !! ";
                            return result;
                        }

                        update_tagout.TagOut_Status = 2;

                        var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                        try
                        {
                            db.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception exy)
                        {
                            transaction.Rollback();
                            result.mess = "ไม่สามารถ Scan tag ได้";
                        }
                    }
                    else {
                        var tag_rollcage = db.im_RollCageOrder.FirstOrDefault(c => c.DocumentRef_No1 == data.tagout_no );
                        if (tag_rollcage != null)
                        {

                            if (tag_rollcage.Document_Status == 1)
                            {
                                result.mess = " QR นี้ถูกแสกน ไปแล้ว!! RollCage No. : " + tag_rollcage.RollCage_Id + " Shipment No. : " + tag_rollcage.TruckLoad_No;
                                return result;
                            }
                            result.mess = "QR นี้เป็นของ RollCage No. : "+ tag_rollcage.RollCage_Id + " Shipment No. : "+tag_rollcage.TruckLoad_No;
                            return result;
                        }
                        else {
                            result.mess = "ไม่พบ Tag ที่ Scan ";
                            return result;
                        }

                        
                    }
                    var rollcageby_status = db.im_RollCageOrder.Where(c => c.RollCage_Index == data.RollCage_Index && c.Document_Status != -1 && c.TruckLoad_No == data.TruckLoad_No).ToList();
                    var rollcageby_Countall = db.im_RollCageOrder.Where(c => c.RollCage_Index == data.RollCage_Index && c.Document_Status == 1 && c.TruckLoad_No == data.TruckLoad_No).ToList();
                    result.planGoodsIssue_No = update_roll.PlanGoodsIssue_No;
                    result.Shipment_no = update_roll.TruckLoad_No;
                    result.route_Name = update_roll.Route_Name;
                    result.branch_Code = update_roll.Branch_Code;
                    result.qty_tagBox = rollcageby_Countall.Count().ToString() + "/" + rollcageby_status.Count().ToString();
                    result.qty_tagTote = "0" + "/" + "0";
                    result.scan_all = (rollcageby_Countall.Count() == rollcageby_status.Count());
                    if (result.scan_all)
                    {
                        var rollcage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Index == data.RollCage_Index);
                        rollcage.RollCage_Status = 0;
                        var transactionMS = dbMaster.Database.BeginTransaction(IsolationLevel.Serializable);
                        try
                        {
                            dbMaster.SaveChanges();
                            transactionMS.Commit();
                        }
                        catch (Exception exy)
                        {
                            transactionMS.Rollback();
                        }
                    }
                }
                else { result.mess = "ไม่พบ RollCage ที่ต้องการ"; }

               

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region movestaging
        public CallFullRollCageViewModel movestaging(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("movestaging", "movestaging" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                RollCageService rollCageService = new RollCageService();
                var result = new CallFullRollCageViewModel();
                LocationRollCageViewModel model = new LocationRollCageViewModel();
                model.rollCage_Id = data.rollcage_id;
                var SuggestionArea = findSuggestionStagingAreainDock(model);

                #region suggestion rollcage location

                tmp_SuggestRollCageLocation tmpSuggestDest = new tmp_SuggestRollCageLocation();
                tmpSuggestDest.Temp_Index = new Guid();
                tmpSuggestDest.RollCage_ID = data.rollcage_id;
                tmpSuggestDest.Location_Index = SuggestionArea.location_Index;
                tmpSuggestDest.Location_Id = SuggestionArea.location_Id;
                tmpSuggestDest.Location_Name = SuggestionArea.location_Name;
                tmpSuggestDest.Location_Type = "DESTSTATION";
                tmpSuggestDest.Create_By = "SUGGESTION SEND ROLLCAGE BY SEND STAGING ROLLCAGE";
                tmpSuggestDest.Create_Date = DateTime.Now;

                dbMaster.tmp_SuggestRollCageLocation.Add(tmpSuggestDest);

                var transInsTemp = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    dbMaster.SaveChanges();
                    transInsTemp.Commit();
                }
                catch (Exception ex)
                {
                    transInsTemp.Rollback();
                }
                #endregion

                var checkLocation = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == SuggestionArea.location_Index && c.RollCage_Id == data.rollcage_id);
                if (checkLocation != null)
                {
                    checkLocation.RollCage_Id = null;

                    var transaction = dbMaster.Database.BeginTransaction();
                    try
                    {
                        dbMaster.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }

                var get_id_location = dbMaster.ms_Location.FirstOrDefault(C => C.Location_Name == data.Location);
                log.DataLogLines("movestaging", "movestaging" + DateTime.Now.ToString("yyyy-MM-dd"), "Check Rollcage : " + data.rollcage_id);
                var checkrollcage = dbMaster.ms_RollCage.Where(c => c.RollCage_Id == data.rollcage_id).ToList();
                if (checkrollcage.Count <= 0)
                {
                    result.mess = "ไม่พบ Rollcage Id : " + data.rollcage_id;
                    log.DataLogLines("movestaging", "movestaging" + DateTime.Now.ToString("yyyy-MM-dd"), "Error Check Rollcage : " + result.mess);
                    return result;
                }
                var moveRollCageModel = new
                {
                    rollCageID = data.rollcage_id,
                    movementType = data.value,
                    sourceStation = Convert.ToInt16(get_id_location.Location_Id),
                    destStation = Convert.ToInt16(SuggestionArea.location_Id),
                    dock_Index = "",
                    dock_Id = "",
                    dock_Name = ""
                };
                log.DataLogLines("movestaging", "movestaging" + DateTime.Now.ToString("yyyy-MM-dd"), "Data To WCS : " + JsonConvert.SerializeObject(moveRollCageModel));
                CallFullRollCageViewModel response_MoveRollCage = utils.SendDataApi<CallFullRollCageViewModel>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));
                
               
                if (response_MoveRollCage.status != "10")
                {
                    result.mess = "ไม่สามารถอัปเดตที่อยู่ได้";
                }
                else
                {
                    var modelRollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Id == data.rollcage_id);
                    modelRollCage.Location_Name_Before = null;

                    var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        dbMaster.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                    result.status = "S";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region movePallet
        public CallFullRollCageViewModel movePallet(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                RollCageService rollCageService = new RollCageService();
                var result = new CallFullRollCageViewModel();
                LocationRollCageViewModel model = new LocationRollCageViewModel();
                model.rollCage_Id = data.rollcage_id;
                model.location_Name = data.Location;
                var SuggestionArea = findSuggestionStagingAreainDock_staging(model);

                #region suggestion rollcage location

                tmp_SuggestRollCageLocation tmpSuggestDest = new tmp_SuggestRollCageLocation();
                tmpSuggestDest.Temp_Index = new Guid();
                //tmpSuggestSource.MovementType = model.movementType;
                tmpSuggestDest.RollCage_ID = data.rollcage_id;
                tmpSuggestDest.Location_Index = SuggestionArea.location_Index;
                tmpSuggestDest.Location_Id = SuggestionArea.location_Id;
                tmpSuggestDest.Location_Name = SuggestionArea.location_Name;
                tmpSuggestDest.Location_Type = "DESTSTATION";
                tmpSuggestDest.Create_By = "SUGGESTION SEND ROLLCAGE BY CALL FULL ROLLCAGE";
                tmpSuggestDest.Create_Date = DateTime.Now;

                dbMaster.tmp_SuggestRollCageLocation.Add(tmpSuggestDest);

                var transInsTemp = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    dbMaster.SaveChanges();
                    transInsTemp.Commit();
                }
                catch (Exception ex)
                {
                    transInsTemp.Rollback();
                }
                #endregion

                var checkLocation = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == SuggestionArea.location_Index && c.RollCage_Id == data.rollcage_id);
                if (checkLocation != null)
                {
                    checkLocation.RollCage_Id = null;

                    var transaction = dbMaster.Database.BeginTransaction();
                    try
                    {
                        dbMaster.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }


                var get_id_location = dbMaster.ms_Location.FirstOrDefault(C => C.Location_Name == data.Location);
                log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Check Rollcage : " + data.rollcage_id);
                var checkrollcage = dbMaster.ms_RollCage.Where(c => c.RollCage_Id == data.rollcage_id).ToList();
                if (checkrollcage.Count <= 0)
                {
                    result.mess = "ไม่พบ Rollcage Id : " + data.rollcage_id;
                    log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Error Check Rollcage : " + result.mess);
                    return result;
                }
                var moveRollCageModel = new
                {
                    rollCageID = data.rollcage_id,
                    movementType = 3,
                    sourceStation = Convert.ToInt16(get_id_location.Location_Id),
                    destStation = Convert.ToInt16(SuggestionArea.location_Id),
                    dock_Index = "",
                    dock_Id = "",
                    dock_Name = ""
                };
                log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Data To WCS : " + JsonConvert.SerializeObject(moveRollCageModel));
                CallFullRollCageViewModel response_MoveRollCage = utils.SendDataApi<CallFullRollCageViewModel>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));

                log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Check Rollcage WCS : " + response_MoveRollCage.status);
                if (response_MoveRollCage.status != "10")
                {
                    result.mess = "ไม่สามารถอัปเดตที่อยู่ได้";
                }
                else
                {
                    var modelRollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Id == data.rollcage_id);
                    modelRollCage.Location_Name_Before = null;
                    log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Check Rollcage ID : " + data.rollcage_id);
                    log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Check Rollcage Truckload No : " + data.Shipment_no);
                    List<im_RollCageOrder> rollCageOrders = db.im_RollCageOrder.Where(c => c.RollCage_Id == data.rollcage_id && c.Document_Status == 1 && string.IsNullOrEmpty(c.UDF_1)).ToList();
                    log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "rollCageOrders : " + rollCageOrders.Count);
                    foreach (var item in rollCageOrders)
                    {
                        item.UDF_1 = "1";
                        item.UDF_2 = "Send Rollcage";
                    }
                    log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "SAVE : " + data.rollcage_id);
                    var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    var transactiondb = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        dbMaster.SaveChanges();
                        db.SaveChanges();
                        transaction.Commit();
                        transactiondb.Commit();
                        log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "SAVE : True" + data.rollcage_id);
                    }
                    catch (Exception ex)
                    {
                        log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "SAVE : EX" + ex.Message + data.rollcage_id);
                        transaction.Rollback();
                        transactiondb.Rollback();
                        throw ex;
                    }



                    result.status = "S";
                }

                return result;
            }
            catch (Exception exy)
            {
                log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "SAVE : EXY" + exy.Message + data.rollcage_id);
                throw exy;
            }
        }
        #endregion

        #region movePalletmovePallettest
        public CallFullRollCageViewModel movePallettest(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            log.DataLogLines("movePallet", "movePallet" + DateTime.Now.ToString("yyyy-MM-dd"), "Start : " + DateTime.Now);
            try
            {
                RollCageService rollCageService = new RollCageService();
                var result = new CallFullRollCageViewModel();
                LocationRollCageViewModel model = new LocationRollCageViewModel();
                model.rollCage_Id = data.rollcage_id;
                var SuggestionArea = findSuggestionStagingAreainDock(model);

                var checkLocation = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == SuggestionArea.location_Index && c.RollCage_Id == data.rollcage_id);
                if (checkLocation != null)
                {
                    checkLocation.RollCage_Id = null;

                    var transaction = dbMaster.Database.BeginTransaction();
                    try
                    {
                        dbMaster.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }

                }
                result.Location = SuggestionArea.location_Name;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region checkRollcage
        public CallFullRollCageViewModel checkRollcage(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            try
            {
                CallFullRollCageViewModel result = new CallFullRollCageViewModel();
                ms_RollCage rollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Id == data.rollcage_id);
                if (rollCage != null)
                {
                    if (!rollCage.Location_Name.Contains("-"))
                    {
                        result.resultIsUse = false;
                        result.resultMsg = "location ที่ Rollcage อยู่ในระบบ ไม่สามารถส่งได้";
                    }
                    else {
                        result.resultIsUse = true;
                    }
                }
                else {
                    result.resultIsUse = false;
                    result.resultMsg = "ไม่พบ Rollcage ที่สแกน";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region checkfromcallfullRollcage
        public CallFullRollCageViewModel checkfromcallfullRollcage(CallFullRollCageViewModel data)
        {
            logtxt log = new logtxt();
            try
            {
                CallFullRollCageViewModel result = new CallFullRollCageViewModel();
                ms_RollCage rollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Id == data.rollcage_id);
                if (rollCage != null)
                {
                    if (!rollCage.Location_Name.Contains("-"))
                    {
                        result.resultIsUse = false;
                        result.resultMsg = "location ที่ Rollcage อยู่ในระบบ ไม่สามารถส่งได้";
                    } else if (rollCage.RollCage_Status == 1)
                    {
                        List<im_RollCageOrder> rollCageOrders = db.im_RollCageOrder.Where(c => c.RollCage_Id == rollCage.RollCage_Id && c.Document_Status == 0).ToList();
                        if (rollCageOrders.Count() <= 0)
                        {
                            rollCage.RollCage_Status = 0;
                            var tran = dbMaster.Database.BeginTransaction();
                            try
                            {
                                dbMaster.SaveChanges();
                                tran.Commit();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                            }

                            result.resultIsUse = true;
                        }
                        else {
                            result.resultIsUse = false;
                            result.resultMsg = "Rollcage ที่ทำการสแกนยังมีของอยู่ กรุณาทำการสแกนของออกให้ครบก่อนทำการส่งกลับ";
                        }
                        
                    }
                    else
                    {
                        result.resultIsUse = true;
                    }
                }
                else
                {
                    result.resultIsUse = false;
                    result.resultMsg = "ไม่พบ Rollcage ที่สแกน";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region findSuggestionStagingAreainDock
        public LocationRollCageViewModel findSuggestionStagingAreainDock(LocationRollCageViewModel data)
        {
            try
            {

                var resultItem = new LocationRollCageViewModel();

                int i = 0;
                View_LocationRollCage viewLocationRollCage = new View_LocationRollCage();
                ms_Staging_location checkLocation = new ms_Staging_location();
                View_LocationRollCage LocationRollCage = new View_LocationRollCage();
                do
                {
                    viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && string.IsNullOrEmpty(c.RollCage_Index.ToString()))
                     .OrderBy(c => c.Location_Id).FirstOrDefault();

                    if (viewLocationRollCage == null)
                    {
                        return resultItem;
                    }

                    ms_Staging_location staging_Location = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == null);
                    if (staging_Location != null)
                    {
                        staging_Location.RollCage_Id = data.rollCage_Id;

                        var tran = dbMaster.Database.BeginTransaction();
                        try
                        {
                            dbMaster.SaveChanges();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }
                    
                    Thread.Sleep(1000);

                    checkLocation = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == data.rollCage_Id);
                    i++;
                } while (checkLocation == null && i != 10) ;

            if (checkLocation == null)
                {
                    return resultItem;
                }

                resultItem.rollCage_Index = viewLocationRollCage.RollCage_Index;
                resultItem.rollCage_Id = viewLocationRollCage.RollCage_Id;
                resultItem.rollCage_Name = viewLocationRollCage.RollCage_Name;
                resultItem.rollCage_Status = viewLocationRollCage.RollCage_Status;
                resultItem.location_Index = viewLocationRollCage.Location_Index;
                resultItem.location_Id = viewLocationRollCage.Location_Id;
                resultItem.location_Name = viewLocationRollCage.Location_Name;

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region findSuggestionStagingAreainDock_staging
        public LocationRollCageViewModel findSuggestionStagingAreainDock_staging(LocationRollCageViewModel data)
        {
            try
            {
                var resultItem = new LocationRollCageViewModel();
                int _locationDock = 0;
                if (string.IsNullOrEmpty(data.location_Name) || string.IsNullOrEmpty(data.rollCage_Id))
                {
                    return resultItem;
                }
                var get_id_location = dbMaster.ms_Location.FirstOrDefault(C => C.Location_Name == data.location_Name);
                _locationDock = Convert.ToInt16(get_id_location.Location_Id);

                int _locationMin = 0;
                int _locationMax = 0;

                int i = 0;
                View_LocationRollCage viewLocationRollCage = new View_LocationRollCage();
                ms_Staging_location checkLocation = new ms_Staging_location();
                View_LocationRollCage LocationRollCage = new View_LocationRollCage();

                switch (_locationDock)
                {

                    case int n when (n <= 74 && n >= 54):
                        _locationMin = 119;
                        _locationMax = 212;
                        break;

                    case int n when (n <= 96 && n >= 76):
                        _locationMin = 213;
                        _locationMax = 306;
                        break;

                    case int n when (n <= 118 && n >= 98):
                        _locationMin = 307;
                        _locationMax = 396;
                        break;
                }

                do
                {

                    viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && string.IsNullOrEmpty(c.RollCage_Index.ToString()) && int.Parse(c.Location_Id) >= _locationMin && int.Parse(c.Location_Id) <= _locationMax)
                     .OrderBy(c => c.Location_Id).FirstOrDefault();
                    
                    if (viewLocationRollCage == null)
                    {
                        viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && string.IsNullOrEmpty(c.RollCage_Index.ToString()))
                        .OrderBy(c => c.Location_Id).FirstOrDefault();

                        if (viewLocationRollCage == null)
                        {
                            return resultItem;
                        }
                    }

                    ms_Staging_location staging_Location = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == null);
                    if (staging_Location != null)
                    {
                        staging_Location.RollCage_Id = data.rollCage_Id;

                        var tran = dbMaster.Database.BeginTransaction();
                        try
                        {
                            dbMaster.SaveChanges();
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                        }
                    }

                    Thread.Sleep(1000);

                    checkLocation = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == data.rollCage_Id);
                    i++;
                } while (checkLocation == null && i != 10);

                if (checkLocation == null)
                {
                    return resultItem;
                }

                resultItem.rollCage_Index = viewLocationRollCage.RollCage_Index;
                resultItem.rollCage_Id = viewLocationRollCage.RollCage_Id;
                resultItem.rollCage_Name = viewLocationRollCage.RollCage_Name;
                resultItem.rollCage_Status = viewLocationRollCage.RollCage_Status;
                resultItem.location_Index = viewLocationRollCage.Location_Index;
                resultItem.location_Id = viewLocationRollCage.Location_Id;
                resultItem.location_Name = viewLocationRollCage.Location_Name;

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region updateActiveRollCageBuff    
        public bool updateActiveRollCageBuff(CallFullRollCageViewModel data)
        {
            try
            {
                bool result = false;

                var modelRollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Index == data.RollCage_Index && c.Location_Name.Contains("DK"));

                if (modelRollCage == null)
                {
                    return result;
                }

                var rollCageId = "";
                rollCageId = modelRollCage.Location_Id;

                modelRollCage.Location_Index = new Guid("2238f02a-f256-44f3-8ad5-8a189f39f7b3");
                modelRollCage.Location_Id = "BUF-DK";
                modelRollCage.Location_Name = "BUF-DK";
                if (!string.IsNullOrEmpty(data.Location))
                {
                    modelRollCage.Location_Name_Before = data.Location;
                }
                modelRollCage.RollCage_Status = 1;
                modelRollCage.Update_By = data.create_By;
                modelRollCage.Update_Date = DateTime.Now;

                var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    dbMaster.SaveChanges();
                    transaction.Commit();
                    result = true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region Check UnScan Out Rollcage
        public ListScanOut Check_unscan_out_Rollcage(CallFullRollCageViewModel data)
        {
            try
            {
                ListScanOut result = new ListScanOut();

                List<string> rollCageOrder = db.im_RollCageOrder.Where(c => c.RollCage_Id == data.rollcage_id && c.TruckLoad_No == data.TruckLoad_No && c.Document_Status == 0).Select(c => c.DocumentRef_No1).ToList();
                List<View_CheckTagoutWithRollcage> checkTagoutWithRollcage = db.View_CheckTagoutWithRollcage.Where(c => rollCageOrder.Contains(c.TagOut_No)).ToList();

                if (checkTagoutWithRollcage.Count() > 0)
                {
                    foreach (var item in checkTagoutWithRollcage)
                    {
                        ListScanOutmodel listScanOut = new ListScanOutmodel();
                        listScanOut.TagOut_No = item.TagOut_No;
                        listScanOut.Product_Id = item.Product_Id;
                        listScanOut.OrderSeq = item.OrderSeq;
                        listScanOut.DropSeq = item.DropSeq;
                        result.ListScanOutmodel.Add(listScanOut);

                    }
                    result.resultIsUse = true;
                }
                else
                {
                    result.resultIsUse = false;
                    result.resultMsg = "Rollcage Is Empty";
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region send RollCage Dock To Staging
        //public bool sendRollCageDockToStaging(LocationRollCageViewModel data)
        //{
        //    try
        //    {
        //        bool result = false;

        //        var resLocationDock = dbMaster.View_DockLocationRollCage.Where(c => c.Ref_Document_No == resTruckLoad.TruckLoad_No).FirstOrDefault();
        //        if (resLocationDock == null)
        //        {
        //            return result;
        //        }

        //        int _locationDock = 0;
        //        _locationDock = Convert.ToInt16(resLocationDock.Location_Id);

        //        int _locationMin = 0;
        //        int _locationMax = 0;

        //        switch (_locationDock)
        //        {

        //            case int n when (n <= 73 && n >= 53):
        //                _locationMin = 119;
        //                _locationMax = 212;
        //                break;

        //            case int n when (n <= 95 && n >= 75):
        //                _locationMin = 213;
        //                _locationMax = 306;
        //                break;

        //            case int n when (n <= 117 && n >= 97):
        //                _locationMin = 307;
        //                _locationMax = 396;
        //                break;
        //        }

        //        var _locationRollCage = dbMaster.ms_RollCage.AsQueryable().ToList(); // Add new

        //        var _viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
        //        && !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
        //        && string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).ToList();

        //        var viewLocationRollCage = _viewLocationRollCage.Where(c => Convert.ToInt16(c.Location_Id) >= _locationMin && Convert.ToInt16(c.Location_Id) <= _locationMax).OrderBy(o => o.Location_Id).FirstOrDefault();

        //        if (viewLocationRollCage == null)
        //        {
        //            viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
        //            && !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
        //            && string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).FirstOrDefault();

        //            if (viewLocationRollCage == null)
        //            {
        //                return result;
        //            }
        //        }

        //        if (viewLocationRollCage == null)
        //        {
        //            return result;
        //        }

        //        #region suggestion rollcage location

        //        tmp_SuggestRollCageLocation tmpSuggestDest = new tmp_SuggestRollCageLocation();
        //        tmpSuggestDest.Temp_Index = new Guid();
        //        //tmpSuggestSource.MovementType = model.movementType;
        //        tmpSuggestDest.RollCage_ID = data.rollCage_Id;
        //        tmpSuggestDest.Location_Index = viewLocationRollCage.Location_Index;
        //        tmpSuggestDest.Location_Id = viewLocationRollCage.Location_Id;
        //        tmpSuggestDest.Location_Name = viewLocationRollCage.Location_Name;
        //        tmpSuggestDest.Location_Type = "DESTSTATION";
        //        tmpSuggestDest.Create_By = "SUGGESTION SEND ROLLCAGE";
        //        tmpSuggestDest.Create_Date = DateTime.Now;

        //        dbMaster.tmp_SuggestRollCageLocation.Add(tmpSuggestDest);

        //        var transInsTemp = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        //        try
        //        {
        //            dbMaster.SaveChanges();
        //            transInsTemp.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transInsTemp.Rollback();
        //        }
        //        #endregion

        //        #endregion

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion
    }
}
