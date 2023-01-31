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
using RollCageBusiness.Libs;
using Business.Library;
using Newtonsoft.Json;
using System.Threading;

namespace RollCageBusiness.RollCage
{
    public class RollCageService
    {
        private RollCageDbContext db;
        private MasterDbContext dbMaster;

        public RollCageService()
        {
            db = new RollCageDbContext();
            dbMaster = new MasterDbContext();
        }

        public RollCageService(RollCageDbContext db)
        {
            this.db = db;
        }

        #region autoCall
        public string autoCall()
        {
            var result = "";
            try
            {
                //'94D86CEA-3D04-4304-9E97-28E954F03C35'-- Full Chute
                //'D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF'-- Empty Chute
                //'64341969-E596-4B8B-8836-395061777490'-- Full Dock
                //'A706D789-F5C9-41A6-BEC7-E57034DFC166'-- Empty Dock
                //'E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC'-- RollCage Staging

                #region getEmptyChuteLocation
                var lstEmptyChute = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF") && string.IsNullOrEmpty(c.RollCage_Index.ToString())).ToList(); // Empty Chute

                if (lstEmptyChute.Count > 0)
                {
                    foreach (var item in lstEmptyChute)
                    {
                        //หา RollCage ที่อยู่ใน 'E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC'-- RollCage Staging
                        var lstRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && c.RollCage_Status == 0).ToList(); // RollCage Staging

                        if (lstRollCage.Count == 0)
                        {
                            return "Not has rollCage empty.";
                        }

                        View_LocationRollCage locationCallRollCage = new View_LocationRollCage();
                        locationCallRollCage = lstRollCage.FirstOrDefault(); // เอาตัวแรกมาใส่

                        //เตรียม update location ให้เป็น '94D86CEA-3D04-4304-9E97-28E954F03C35' -- Full Chute
                        //var modelLocation = dbMaster.ms_Location.Where(c => c.Location_Index == item.Location_Index && c.IsActive == 1).FirstOrDefault();
                        //if (modelLocation != null)
                        //{
                        //    modelLocation.LocationType_Index = new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35");
                        //    modelLocation.Update_By = "AutoCall RC : " + locationCallRollCage.RollCage_Id.ToString();
                        //    modelLocation.Update_Date = DateTime.Now;
                        //}

                        var modelRollCage = dbMaster.ms_RollCage.Where(c => c.RollCage_Index == locationCallRollCage.RollCage_Index && c.IsActive == 1).FirstOrDefault();
                        if (modelRollCage != null)
                        {
                            // เอา location มา update ใส่ rollCage
                            //modelRollCage.Location_Index = item.Location_Index;
                            //modelRollCage.Location_Id = item.Location_Id;
                            //modelRollCage.Location_Name = item.Location_Name;
                            //modelRollCage.Update_By = "AutoCall from Location : " + item.Location_Id.ToString();
                            //modelRollCage.Update_Date = DateTime.Now;

                            //var locationIsUse = dbMaster.ms_Location.Where(c => c.Location_Index == item.Location_Index).FirstOrDefault();

                            //locationIsUse.Ref_No2 = "x";

                            var moveRollCageModel = new
                            {
                                rollCageID = locationCallRollCage.RollCage_Id,
                                movementType = 4,
                                sourceStation = Convert.ToInt16(locationCallRollCage.Location_Id),
                                destStation = Convert.ToInt16(item.Location_Id),
                            };

                            var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));
                        }

                        var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                        try
                        {
                            dbMaster.SaveChanges();
                            transaction.Commit();
                            result = "Auto call complete.";
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }

                    //return result;
                }
                #endregion
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region autoCall
        public string autoCallOne(string locationIdOld)
        {
            var result = "";
            try
            {
                //'94D86CEA-3D04-4304-9E97-28E954F03C35'-- Full Chute
                //'D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF'-- Empty Chute
                //'64341969-E596-4B8B-8836-395061777490'-- Full Dock
                //'A706D789-F5C9-41A6-BEC7-E57034DFC166'-- Empty Dock
                //'E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC'-- RollCage Staging

                #region getEmptyChuteLocation
                var lstEmptyChute = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF") && string.IsNullOrEmpty(c.RollCage_Index.ToString()) && c.Location_Id == locationIdOld).FirstOrDefault(); // Empty Chute

                if (lstEmptyChute == null)
                {
                    return result;
                }


                //หา RollCage ที่อยู่ใน 'E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC'-- RollCage Staging
                var lstRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && c.RollCage_Status == 0).FirstOrDefault(); // RollCage Staging

                if (lstRollCage == null)
                {
                    return "Not has rollCage empty.";
                }

                View_LocationRollCage locationCallRollCage = new View_LocationRollCage();
                locationCallRollCage = lstRollCage; // เอาตัวแรกมาใส่


                var modelRollCage = dbMaster.ms_RollCage.Where(c => c.RollCage_Index == locationCallRollCage.RollCage_Index && c.IsActive == 1).FirstOrDefault();
                if (modelRollCage != null)
                {
                    // เอา location มา update ใส่ rollCage
                    //modelRollCage.Location_Index = item.Location_Index;
                    //modelRollCage.Location_Id = item.Location_Id;
                    //modelRollCage.Location_Name = item.Location_Name;
                    //modelRollCage.Update_By = "AutoCall from Location : " + item.Location_Id.ToString();
                    //modelRollCage.Update_Date = DateTime.Now;

                    var locationIsUse = dbMaster.ms_Location.Where(c => c.Location_Index == locationCallRollCage.Location_Index).FirstOrDefault();

                    //locationIsUse.Ref_No2 = "x";

                    if (locationIsUse == null)
                    {
                        return "Not found location IsUse";
                    }

                    var moveRollCageModel = new
                    {
                        rollCageID = locationCallRollCage.RollCage_Id,
                        movementType = 4,
                        sourceStation = Convert.ToInt16(locationCallRollCage.Location_Id),
                        destStation = Convert.ToInt16(locationIdOld),
                    };

                    var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));
                }

                var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    dbMaster.SaveChanges();
                    transaction.Commit();
                    result = "Auto call complete.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }

                #endregion
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public bool updateActiveRollCage(RollCageViewModel data)
        {
            try
            {
                bool result = false;

                var modelRollCage = dbMaster.ms_RollCage.Where(c => c.RollCage_Index == new Guid(data.rollCage_Index.ToString())).FirstOrDefault();

                if (modelRollCage == null)
                {
                    return result;
                }

                var locationIdOld = "";
                locationIdOld = modelRollCage.Location_Id;

                modelRollCage.Location_Index = new Guid("1b8794d6-1c1f-400f-8837-bca40299e7f7");
                modelRollCage.Location_Id = "BUF-CH";
                modelRollCage.Location_Name = "BUF-CH";
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

                if(!data.isRework)
                {
                    //autoCall();
                    autoCallOne(locationIdOld);
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RollCageViewModel> findRollCage(RollCageViewModel data)
        {
            try
            {
                var items = new List<RollCageViewModel>();

                var query = dbMaster.ms_RollCage.AsQueryable();

                if (!string.IsNullOrEmpty(data.rollCage_Id))
                {
                    query = query.Where(c => c.RollCage_Id == data.rollCage_Id);
                }

                var result = query.Take(100).OrderByDescending(o => o.Create_Date).ToList();

                foreach (var item in result)
                {
                    var resultItem = new RollCageViewModel();

                    resultItem.rollCage_Index = item.RollCage_Index;
                    resultItem.rollCage_Id = item.RollCage_Id;
                    resultItem.rollCage_Name = item.RollCage_Name;
                    resultItem.rollCage_SecondName = item.RollCage_SecondName;
                    resultItem.rollCage_Status = item.RollCage_Status;
                    resultItem.rollCageType_Index = item.RollCageType_Index;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.isSystem = item.IsSystem;
                    resultItem.status_Id = item.Status_Id;

                    items.Add(resultItem);
                }

                return items;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RollCageViewModel> findRollCageBUF(RollCageViewModel data)
        {
            try
            {
                var items = new List<RollCageViewModel>();

                var query = dbMaster.ms_RollCage.AsQueryable();

                if (!string.IsNullOrEmpty(data.rollCage_Id))
                {
                    query = query.Where(c => c.RollCage_Id == data.rollCage_Id && c.Location_Id == "BUF-CH");
                }

                var result = query.Take(100).OrderByDescending(o => o.Create_Date).ToList();

                foreach (var item in result)
                {
                    var resultItem = new RollCageViewModel();

                    resultItem.rollCage_Index = item.RollCage_Index;
                    resultItem.rollCage_Id = item.RollCage_Id;
                    resultItem.rollCage_Name = item.RollCage_Name;
                    resultItem.rollCage_SecondName = item.RollCage_SecondName;
                    resultItem.rollCage_Status = item.RollCage_Status;
                    resultItem.rollCageType_Index = item.RollCageType_Index;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.isActive = item.IsActive;
                    resultItem.isDelete = item.IsDelete;
                    resultItem.isSystem = item.IsSystem;
                    resultItem.status_Id = item.Status_Id;

                    items.Add(resultItem);
                }

                return items;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RollCageViewModel> findRollCageScanIn(RollCageViewModel data)
        {
            try
            {
                var items = new List<RollCageViewModel>();

                var query = dbMaster.View_LocationRollCage.AsQueryable();

                if (!string.IsNullOrEmpty(data.rollCage_Id))
                {
                    query = query.Where(c => c.RollCage_Id == data.rollCage_Id && c.LocationType_Index == new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF") && c.RollCage_Status == 0);
                }

                var result = query.Take(100).OrderByDescending(o => o.Create_Date).ToList();

                foreach (var item in result)
                {
                    var resultItem = new RollCageViewModel();

                    resultItem.rollCage_Index = (item.RollCage_Index == null) ? Guid.Empty : new Guid(item.RollCage_Index.ToString());
                    resultItem.rollCage_Id = item.RollCage_Id;
                    resultItem.rollCage_Name = item.RollCage_Name;
                    resultItem.rollCage_Status = item.RollCage_Status;
                    //resultItem.rollCageType_Index = item.RollCageType_Index;
                    resultItem.location_Index = item.Location_Index;
                    resultItem.location_Id = item.Location_Id;
                    resultItem.location_Name = item.Location_Name;

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RollCageViewModel> findRollCageActiveEmptyChute(RollCageViewModel data)
        {
            try
            {
                var items = new List<RollCageViewModel>();

                if (!string.IsNullOrEmpty(data.rollCage_Id))
                {
                    var query = dbMaster.View_LocationRollCage.AsQueryable();
                    //var modelRollCage = db.im_RollCageOrder.Where(c => c.RollCage_Id == data.rollCage_Id && c.Document_Status == 0).ToList();
                    //if(modelRollCage.Count > 0)
                    //{
                    //    query = query.Where(c => c.RollCage_Id == data.rollCage_Id && c.LocationType_Index == new Guid("6407D989-D643-45D8-9434-1176761663BC") && c.RollCage_Status == 1); // D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF
                    //}
                    //else
                    //{
                    //    query = query.Where(c => c.RollCage_Id == data.rollCage_Id && (c.LocationType_Index == new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF") || c.LocationType_Index == new Guid("6407D989-D643-45D8-9434-1176761663BC"))); // && c.RollCage_Status == 0
                    //}
                    query = query.Where(c => c.RollCage_Id == data.rollCage_Id && (c.LocationType_Index == new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF") || c.LocationType_Index == new Guid("6407D989-D643-45D8-9434-1176761663BC"))); // && c.RollCage_Status == 0
                    var result = query.Take(100).OrderByDescending(o => o.Create_Date).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new RollCageViewModel();

                        resultItem.rollCage_Index = (item.RollCage_Index == null) ? Guid.Empty : new Guid(item.RollCage_Index.ToString());
                        resultItem.rollCage_Id = item.RollCage_Id;
                        resultItem.rollCage_Name = item.RollCage_Name;
                        resultItem.rollCage_Status = item.RollCage_Status;
                        //resultItem.rollCageType_Index = item.RollCageType_Index;
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;

                        items.Add(resultItem);
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RollCageViewModel> findRollCageActiveReworkChute(RollCageViewModel data)
        {
            try
            {
                var items = new List<RollCageViewModel>();

                if (!string.IsNullOrEmpty(data.rollCage_Id))
                {
                    var query = dbMaster.View_LocationRollCage.AsQueryable();
                    query = query.Where(c => c.RollCage_Id == data.rollCage_Id); // && c.RollCage_Status == 0
                    var result = query.Take(100).OrderByDescending(o => o.Create_Date).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new RollCageViewModel();

                        resultItem.rollCage_Index = (item.RollCage_Index == null) ? Guid.Empty : new Guid(item.RollCage_Index.ToString());
                        resultItem.rollCage_Id = item.RollCage_Id;
                        resultItem.rollCage_Name = item.RollCage_Name;
                        resultItem.rollCage_Status = item.RollCage_Status;
                        //resultItem.rollCageType_Index = item.RollCageType_Index;
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;

                        items.Add(resultItem);
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GoodsIssueTruckloadRollCageViewModel findQRCodeGoodsIssueTruckload(LocationRollCageViewModel data)
        {
            try
            {
                //var items = new List<GoodsIssueTruckloadRollCageViewModel>();

                var query = db.View_GoodsIssueTruckload_RollCage.AsQueryable();

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    query = query.Where(c => c.TagOut_No == data.qrcode && c.TagOut_Status == 0);
                }

                //var result = query.Take(100).OrderBy(o => o.TagOut_No).ToList();

                var item = query.FirstOrDefault();

                //foreach (var item in result)
                //{

                var resultItem = new GoodsIssueTruckloadRollCageViewModel();

                if (item == null)
                {
                    return resultItem;
                }

                resultItem.truckLoad_No = item.TruckLoad_No;
                resultItem.truckLoad_Index = item.TruckLoad_Index;
                resultItem.planGoodsIssue_Index = item.PlanGoodsIssue_Index;
                resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                resultItem.shipTo_Index = item.ShipTo_Index;
                resultItem.shipTo_Id = item.ShipTo_Id;
                resultItem.branch_Name = item.Branch_Name;
                resultItem.branch_Code = item.Branch_Code;
                resultItem.shipTo_Address = item.ShipTo_Address;
                resultItem.goodsIssue_No = item.GoodsIssue_No;
                resultItem.route_Index = item.Route_Index;
                resultItem.route_Id = item.Route_Id;
                resultItem.route_Name = item.Route_Name;
                resultItem.round_Index = item.Round_Index;
                resultItem.round_Id = item.Round_Id;
                resultItem.round_Name = item.Round_Name;
                resultItem.goodsIssueItemLocation_Index = item.GoodsIssueItemLocation_Index;
                resultItem.tagOut_Index = item.TagOut_Index;
                resultItem.tagOut_No = item.TagOut_No;
                resultItem.tagOut_Status = item.TagOut_Status;
                resultItem.tagOutRef_No4 = item.TagOutRef_No4;
                resultItem.tagOutRef_No5 = item.TagOutRef_No5;
                //resultItem.countScanBOX = item.CountScanBOX;
                resultItem.totalBOX = item.TotalBOX;

                //var lstScan = findScanSummary(data);

                //if(lstScan.Count > 0)
                //{
                //    resultItem.lstScanSummary = lstScan;
                //}

                if (resultItem != null)
                {
                    updateTagOut(data, 1);
                    createOrUpdateRollCageOrder(data, resultItem, 0);
                }

                var truckLoadNo = "";

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    truckLoadNo = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == data.qrcode).FirstOrDefault().TruckLoad_No.ToString();
                }

                var groupData = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TruckLoad_No == truckLoadNo)
                .GroupBy(c => new
                {
                    c.PlanGoodsIssue_No,
                    c.TruckLoad_No,
                    c.Route_Id,
                    c.ShipTo_Id,
                    c.Branch_Name,
                    c.ShipTo_Address,
                    c.GoodsIssue_No,
                    c.Round_Name,
                    c.CountScanBOX,
                    c.TotalBOX
                }).Select(c => new
                {
                    Iscomplete = c.Key.CountScanBOX != c.Key.TotalBOX ? 0 : 1,
                    c.Key.PlanGoodsIssue_No,
                    c.Key.TruckLoad_No,
                    c.Key.Route_Id,
                    c.Key.ShipTo_Id,
                    c.Key.Branch_Name,
                    c.Key.ShipTo_Address,
                    c.Key.GoodsIssue_No,
                    c.Key.Round_Name,
                    c.Key.CountScanBOX,
                    c.Key.TotalBOX
                }).FirstOrDefault();

                resultItem.countScanBOX = groupData == null ? 0 : groupData.CountScanBOX;

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public GoodsIssueTruckloadRollCageViewModel findQRCodeGoodsIssueTruckloadScanOut(LocationRollCageViewModel data)
        {
            try
            {
                //var items = new List<GoodsIssueTruckloadRollCageViewModel>();

                var resultItem = new GoodsIssueTruckloadRollCageViewModel();

                var orderQRCode = db.im_RollCageOrder.Where(c => c.DocumentRef_No1 == data.qrcode && c.RollCage_Id == data.rollCage_Id && c.Document_Status == 0).FirstOrDefault(); // comment old code

                var tmpQRCode = db.tmp_RollCageOrder.Where(c => c.TagOut_No == data.qrcode && c.RollCage_ID == data.rollCage_Id).FirstOrDefault();

                if (orderQRCode == null && tmpQRCode == null)
                {
                    return resultItem;
                }

                var query = db.View_GoodsIssueTruckload_RollCage.AsQueryable();

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    query = query.Where(c => c.TagOut_No == data.qrcode && c.TagOut_Status == 1);
                }

                var item = query.FirstOrDefault();

                if (item == null)
                {
                    return resultItem;
                }

                resultItem.truckLoad_No = item.TruckLoad_No;
                resultItem.truckLoad_Index = item.TruckLoad_Index;
                resultItem.planGoodsIssue_Index = item.PlanGoodsIssue_Index;
                resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                resultItem.shipTo_Index = item.ShipTo_Index;
                resultItem.shipTo_Id = item.ShipTo_Id;
                resultItem.branch_Name = item.Branch_Name;
                resultItem.branch_Code = item.Branch_Code;
                resultItem.shipTo_Address = item.ShipTo_Address;
                resultItem.goodsIssue_No = item.GoodsIssue_No;
                resultItem.route_Index = item.Route_Index;
                resultItem.route_Id = item.Route_Id;
                resultItem.route_Name = item.Route_Name;
                resultItem.round_Index = item.Round_Index;
                resultItem.round_Id = item.Round_Id;
                resultItem.round_Name = item.Round_Name;
                resultItem.goodsIssueItemLocation_Index = item.GoodsIssueItemLocation_Index;
                resultItem.tagOut_Index = item.TagOut_Index;
                resultItem.tagOut_No = item.TagOut_No;
                resultItem.tagOut_Status = item.TagOut_Status;
                resultItem.tagOutRef_No4 = item.TagOutRef_No4;
                resultItem.tagOutRef_No5 = item.TagOutRef_No5;
                //resultItem.countScanBOX = item.CountScanBOX;
                resultItem.totalBOX = item.TotalBOX;

                //var lstScan = findScanSummary(data);

                //if(lstScan.Count > 0)
                //{
                //    resultItem.lstScanSummary = lstScan;
                //}

                if (resultItem != null && tmpQRCode != null)
                {
                    updateTagOut(data, 0); // comment old code
                    //createOrUpdateRollCageOrder(data, resultItem, 1);
                    #region remove temp rollcage
                    tmp_RollCageOrder dataModelTempDel = new tmp_RollCageOrder();
                    var modelDel = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id && c.TagOut_No == data.qrcode).ToList();

                    if (modelDel.Count == 0)
                    {
                        return resultItem;
                    }

                    db.tmp_RollCageOrder.RemoveRange(modelDel);

                    var transactionTemp = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        db.SaveChanges();
                        transactionTemp.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionTemp.Rollback();
                        throw ex;
                    }
                    #endregion
                }

                if (resultItem != null && orderQRCode != null)
                {
                    updateTagOut(data, 0);
                    createOrUpdateRollCageOrder(data, resultItem, 1);

                    var transactionOrder = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                    try
                    {
                        db.SaveChanges();
                        transactionOrder.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionOrder.Rollback();
                        throw ex;
                    }
                }

                var truckLoadNo = "";

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    truckLoadNo = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == data.qrcode).FirstOrDefault().TruckLoad_No.ToString();
                }

                var groupData = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TruckLoad_No == truckLoadNo)
                .GroupBy(c => new
                {
                    c.PlanGoodsIssue_No,
                    c.TruckLoad_No,
                    c.Route_Id,
                    c.ShipTo_Id,
                    c.Branch_Name,
                    c.ShipTo_Address,
                    c.GoodsIssue_No,
                    c.Round_Name,
                    c.CountScanBOX,
                    c.TotalBOX
                }).Select(c => new
                {
                    Iscomplete = c.Key.CountScanBOX != c.Key.TotalBOX ? 0 : 1,
                    c.Key.PlanGoodsIssue_No,
                    c.Key.TruckLoad_No,
                    c.Key.Route_Id,
                    c.Key.ShipTo_Id,
                    c.Key.Branch_Name,
                    c.Key.ShipTo_Address,
                    c.Key.GoodsIssue_No,
                    c.Key.Round_Name,
                    c.Key.CountScanBOX,
                    c.Key.TotalBOX
                }).FirstOrDefault();

                resultItem.countScanBOX = groupData == null ? 0 : groupData.CountScanBOX;

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool updateTagOut(LocationRollCageViewModel data, int tagOut_Status)
        {
            try
            {
                bool result = false;

                var modelRollCage = db.wm_TagOut.Where(c => c.TagOut_No == data.qrcode).FirstOrDefault();

                if (modelRollCage == null)
                {
                    return result;
                }

                modelRollCage.TagOut_Status = tagOut_Status;
                modelRollCage.Update_By = data.create_By;
                modelRollCage.Update_Date = DateTime.Now;

                //im_RollCageOrder resultItem = new im_RollCageOrder();

                //db.im_RollCageOrder.Add(resultItem);

                var transaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
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

        public List<ScanSummaryViewModel> findScanSummary(LocationRollCageViewModel data)
        {
            try
            {
                var items = new List<ScanSummaryViewModel>();

                var query = db.View_GoodsIssueTruckload_RollCage.AsQueryable();

                var truckLoadNo = "";

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    truckLoadNo = query.Where(c => c.TagOut_No == data.qrcode).FirstOrDefault().TruckLoad_No.ToString();
                }

                if (!string.IsNullOrEmpty(data.rollCage_Id))
                {
                    var resultOrder = db.im_RollCageOrder.Where(c => c.RollCage_Id == data.rollCage_Id && c.Document_Status == 0).OrderByDescending(o => o.Create_Date).ToList();
                    if (resultOrder.Count > 0)
                    {
                        truckLoadNo = resultOrder.FirstOrDefault().TruckLoad_No.ToString();
                    }
                }

                //if (!string.IsNullOrEmpty(data.qrcode) && !string.IsNullOrEmpty(truckLoadNo))
                //{
                if (string.IsNullOrEmpty(truckLoadNo))
                {
                    return items;
                }

                var groupData = query.Where(c => c.TruckLoad_No == truckLoadNo)
                    .GroupBy(c => new
                    {
                        c.PlanGoodsIssue_No,
                        c.TruckLoad_No,
                        c.Route_Id,
                        c.ShipTo_Id,
                        c.Branch_Name,
                        c.ShipTo_Address,
                        c.GoodsIssue_No,
                        c.Round_Name,
                        c.CountScanBOX,
                        c.TotalBOX
                    }).Select(c => new
                    {
                        Iscomplete = c.Key.CountScanBOX != c.Key.TotalBOX ? 0 : 1,
                        c.Key.PlanGoodsIssue_No,
                        c.Key.TruckLoad_No,
                        c.Key.Route_Id,
                        c.Key.ShipTo_Id,
                        c.Key.Branch_Name,
                        c.Key.ShipTo_Address,
                        c.Key.GoodsIssue_No,
                        c.Key.Round_Name,
                        c.Key.CountScanBOX,
                        c.Key.TotalBOX
                    }).ToList();


                //}



                //query = groupData.Where(c => c.TagOut_No == data.qrcode && c.TruckLoad_No == truckLoadNo);


                //var result = query.Take(100).OrderBy(o => o.TagOut_No).ToList();

                //var groupData = result
                //    .GroupBy(c => new
                //    {
                //        c.PlanGoodsIssue_No,
                //        c.TruckLoad_No,
                //        c.Route_Id,
                //        c.ShipTo_Id,
                //        c.Branch_Name,
                //        c.ShipTo_Address,
                //        c.GoodsIssue_No,
                //        c.Round_Name,
                //        c.CountScanBOX,
                //        c.TotalBOX
                //    }).Select(c => new {
                //        Iscomplete = c.Key.CountScanBOX != c.Key.TotalBOX ? 0 : 1,
                //        c.Key.PlanGoodsIssue_No,
                //        c.Key.TruckLoad_No,
                //        c.Key.Route_Id,
                //        c.Key.ShipTo_Id,
                //        c.Key.Branch_Name,
                //        c.Key.ShipTo_Address,
                //        c.Key.GoodsIssue_No,
                //        c.Key.Round_Name,
                //        c.Key.CountScanBOX,
                //        c.Key.TotalBOX
                //    }).ToList();

                foreach (var item in groupData)
                {
                    var resultItem = new ScanSummaryViewModel();

                    resultItem.isComplete = item.Iscomplete;
                    resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                    resultItem.truckLoad_No = item.TruckLoad_No;
                    resultItem.route_Id = item.Route_Id;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.branch_Name = item.Branch_Name;
                    resultItem.shipTo_Address = item.ShipTo_Address;
                    resultItem.goodsIssue_No = item.GoodsIssue_No;
                    resultItem.round_Name = item.Round_Name;
                    resultItem.countScanBOX = item.CountScanBOX;
                    resultItem.totalBOX = item.TotalBOX;

                    items.Add(resultItem);
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public List<GoodsIssueTruckloadRollCageViewModel> findQRScanSummary(LocationRollCageViewModel data)
        //{
        //    try
        //    {
        //        var items = new List<GoodsIssueTruckloadRollCageViewModel>();
        //        if (!string.IsNullOrEmpty(data.rollCage_Id))
        //        { 
        //            var resultOrder = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).OrderBy(o => o.TagOut_No).ToList();
        //            if (resultOrder.Count > 0)
        //            {
        //                var query = db.View_GoodsIssueTruckload_RollCage.ToList();
        //                query = query.Where(c => resultOrder.Select(s => s.TagOut_No).Contains(c.TagOut_No)).OrderBy(o => o.TagOut_No).ToList();
        //                foreach (var item in query)
        //                {
        //                    var resultItem = new GoodsIssueTruckloadRollCageViewModel();

        //                    resultItem.tagOut_No = item.TagOut_No;
        //                    resultItem.truckLoad_No = item.TruckLoad_No;

        //                    resultItem.countScanBOX = item.CountScanBOX;
        //                    resultItem.totalBOX = item.TotalBOX;
        //                    resultItem.tagOutRef_No4 = item.TagOutRef_No4;

        //                    items.Add(resultItem);
        //                }
        //            }
        //        }

        //        return items;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public List<GoodsIssueTruckloadRollCageViewModel> findQRScanSummary(LocationRollCageViewModel data)
        {
            try
            {
                var items = new List<GoodsIssueTruckloadRollCageViewModel>();
                if (!string.IsNullOrEmpty(data.rollCage_Id) && !string.IsNullOrEmpty(data.goodsIssue_No))
                {
                    var resultOrder = db.View_RollCage_With_Tag
                        //.Where(c => c.RollCage_Id == data.rollCage_Id && c.GI_NO == data.goodsIssue_No)
                        .Where(c =>  c.GI_NO == data.goodsIssue_No && c.Chute_No == data.chute_Id)
                        .OrderBy(o => o.TruckLoad_No)
                        .ThenBy(c=> c.Product_Name)
                        .ThenBy(c=> c.RollCage_Id)
                        .ThenBy(c=> c.TagOut_No)
                        .ThenBy(c=> c.box_no).ToList();

                    if (resultOrder.Count > 0)
                    {
                        var query = db.View_GoodsIssueTruckload_RollCage.Where(c => resultOrder.Select(s => s.TagOut_No).Contains(c.TagOut_No)).OrderBy(o => o.TagOut_No).ToList();
                        foreach (var item in resultOrder)
                        {
                            if (!string.IsNullOrEmpty(data.rollCage_Id))
                            {
                                if (!string.IsNullOrEmpty(item.FlagScanIN) && item.RollCage_Id != data.rollCage_Id)
                                {
                                    continue;
                                }

                            }

                            var resultItem = new GoodsIssueTruckloadRollCageViewModel();

                            resultItem.RollCage_Id = item.RollCage_Id;
                            resultItem.tagOut_No = item.TagOut_No;
                            resultItem.truckLoad_No = item.TruckLoad_No;
                            resultItem.Product_Name = item.Product_Name;
                            resultItem.tagOutRef_No4 = item.box_no;
                            if (string.IsNullOrEmpty(item.FlagScanIN))
                            {
                                resultItem.FlagScanIN = "Y";
                            }
                            else{
                                resultItem.FlagScanIN = "X";
                            }
                            

                            resultItem.countScanBOX = query[0].CountScanBOX;
                            resultItem.totalBOX = query[0].TotalBOX;


                            items.Add(resultItem);
                        }
                    }
                }

                return items.OrderBy(o => o.truckLoad_No)
                        .ThenBy(c => c.Product_Name)
                        .ThenBy(c => c.RollCage_Id)
                        .ThenBy(c => c.tagOut_No)
                        .ThenBy(c => c.tagOutRef_No4).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public GoodsIssueTruckloadRollCageViewModel findCheckQRScanSummary(LocationRollCageViewModel data)
        {
            try
            {
                var items = new GoodsIssueTruckloadRollCageViewModel();
                
                var resultOrder = db.View_RollCage_CheckQr.FirstOrDefault(c=> c.TagOut_No == data.qrcode);

                if (resultOrder != null)
                {
                    items.Status = resultOrder.status_rollcage;
                    items.Product_ID = resultOrder.Product_ID;
                    items.Product_Name = resultOrder.Product_Name;
                    items.RollCage_Id = resultOrder.RollCage_Id;
                    items.branch_Code = resultOrder.BranchCode;
                    items.truckLoad_No = resultOrder.TruckLoad_No;
                    items.planGoodsIssue_No = resultOrder.UDF_1;
                    items.pick_location = resultOrder.pick_location;
                    items.Dock = resultOrder.Dock;
                    items.location_rollcage = resultOrder.location_rollcage;
                }
                else {
                    items.mess = "ไม่พบ QR ที่ทำการ Scan";
                }
                

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<LocationRollCageViewModel> findFullChute(LocationRollCageViewModel data)
        {
            try
            {
                var items = new List<LocationRollCageViewModel>();

                var query = dbMaster.View_LocationRollCage.AsQueryable();

                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    query = query.Where(c => c.Location_Name == data.location_Name && (c.LocationType_Index == new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35") || c.LocationType_Index == new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7")));
                    var result = query.Take(100).OrderByDescending(o => o.Location_Id).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new LocationRollCageViewModel();

                        resultItem.rollCage_Index = item.RollCage_Index;
                        resultItem.rollCage_Id = item.RollCage_Id;
                        resultItem.rollCage_Name = item.RollCage_Name;
                        resultItem.rollCage_Status = item.RollCage_Status;
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name;

                        items.Add(resultItem);
                    }
                }

                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LocationRollCageViewModel findSuggestionStagingArea(LocationRollCageViewModel data)
        {
            try
            {

                var resultItem = new LocationRollCageViewModel();

                var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && string.IsNullOrEmpty(c.RollCage_Index.ToString()))
                    .OrderBy(c=> c.Location_Id).FirstOrDefault();

                if (viewLocationRollCage == null)
                {
                    return resultItem;
                }

                /// wait insert tmp_location
                /// 


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

        public bool updateLocationRollCage(LocationRollCageViewModel data)
        {
            try
            {
                bool result = false;

                //var resultItem = new LocationRollCageViewModel();

                //var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && c.Location_Id == data.location_Id_To).FirstOrDefault();

                //if (viewLocationRollCage == null)
                //{
                //    return result;
                //}

                var modelRollCage = dbMaster.ms_RollCage.Where(c => c.RollCage_Index == new Guid(data.rollCage_Index.ToString())).FirstOrDefault();

                if (modelRollCage == null)
                {
                    return result;
                }

                var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && string.IsNullOrEmpty(c.RollCage_Index.ToString())).FirstOrDefault();

                if (viewLocationRollCage == null)
                {
                    return result;
                }

                //modelRollCage.Location_Index = data.location_Index_To;
                //modelRollCage.Location_Id = data.location_Id_To;
                //modelRollCage.Location_Name = data.location_Name_To;
                //modelRollCage.Update_By = data.create_By;
                //modelRollCage.Update_Date = DateTime.Now;

                //var locationIsUse = dbMaster.ms_Location.Where(c => c.Location_Index == data.location_Index_To).FirstOrDefault();

                //locationIsUse.Ref_No2 = "x";

                var moveRollCageModel = new
                {
                    rollCageID = data.rollCage_Id,
                    movementType = 1,
                    sourceStation = Convert.ToInt16(data.location_Id),
                    destStation = Convert.ToInt16(viewLocationRollCage.Location_Id),
                };

                var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));

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

        //public bool sendRollCageToStaging(LocationRollCageViewModel data)
        //{
        //    try
        //    {
        //        bool result = false;

        //        //var resultItem = new LocationRollCageViewModel();

        //        //var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && c.Location_Id == data.location_Id_To).FirstOrDefault();

        //        //if (viewLocationRollCage == null)
        //        //{
        //        //    return result;
        //        //}

        //        var modelRollCage = dbMaster.ms_RollCage.Where(c => c.RollCage_Index == new Guid(data.rollCage_Index.ToString())).FirstOrDefault();

        //        if (modelRollCage == null)
        //        {
        //            return result;
        //        }

        //        #region suggest location near dock

        //        var _tagNo = "";

        //        if (data.location_Name != "RC-IN-001" && data.location_Name != "RC-IN-002" && data.location_Name != "RC-IN-003" && data.location_Name != "RC-IN-004")
        //        {
        //            var resTmpQRCode = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == modelRollCage.RollCage_Id).FirstOrDefault();
        //            if (resTmpQRCode == null)
        //            {
        //                return result;
        //            }
        //            else
        //            {
        //                _tagNo = resTmpQRCode.TagOut_No;
        //            }
        //        } else {

        //            var resTmpQRCode = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == modelRollCage.RollCage_Id).FirstOrDefault();
        //            if (resTmpQRCode == null)
        //            {
        //                var resQRCode = db.im_RollCageOrder.Where(c => c.RollCage_Id == modelRollCage.RollCage_Id && c.Document_Status == 0).FirstOrDefault();
        //                if (resQRCode == null)
        //                {
        //                    return result;
        //                }
        //                else
        //                {
        //                    _tagNo = resQRCode.DocumentRef_No1;
        //                }
        //            }
        //            else
        //            {
        //                _tagNo = resTmpQRCode.TagOut_No;
        //            }

        //        }

        //        //var resTmpQRCode = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == modelRollCage.RollCage_Id).FirstOrDefault();
        //        //if (resTmpQRCode == null)
        //        //{
        //        //    return result;
        //        //}

        //        //var resTruckLoad = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == resTmpQRCode.TagOut_No).FirstOrDefault();
        //        //if (resTruckLoad == null)
        //        //{
        //        //    return result;
        //        //}

        //        var resTruckLoad = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == _tagNo).FirstOrDefault();
        //        if (resTruckLoad == null)
        //        {
        //            return result;
        //        }

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

        //        //return result;

        //        #endregion

        //        //var _locationRollCage = dbMaster.ms_RollCage.AsQueryable().ToList(); // Add new

        //        //var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
        //        //&& !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
        //        //&& string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).FirstOrDefault();

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
        //            //throw ex;
        //        }
        //        #endregion

        //        var resTmpRollCage = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).ToList();

        //        //if (resTmpRollCage.Count > 0)
        //        //{
        //        //    return result;
        //        //}

        //        if (resTmpRollCage.Count > 0)
        //        {
        //            foreach (var item in resTmpRollCage)
        //            {
        //                var chkDataRollcageOrder = db.im_RollCageOrder.Where(c => c.DocumentRef_No1 == item.TagOut_No && c.Document_Status != -1).ToList();
        //                if(chkDataRollcageOrder.Count > 0)
        //                {
        //                    // have data in rollcageOrder
        //                    continue;
        //                }

        //                var resDataQRCode = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == item.TagOut_No).FirstOrDefault();

        //                var resultItem = new GoodsIssueTruckloadRollCageViewModel();

        //                if (resDataQRCode == null)
        //                {
        //                    continue;
        //                }

        //                data.qrcode = resDataQRCode.TagOut_No;

        //                resultItem.truckLoad_No = resDataQRCode.TruckLoad_No;
        //                resultItem.truckLoad_Index = resDataQRCode.TruckLoad_Index;
        //                resultItem.planGoodsIssue_No = resDataQRCode.PlanGoodsIssue_No;
        //                resultItem.shipTo_Index = resDataQRCode.ShipTo_Index;
        //                resultItem.shipTo_Id = resDataQRCode.ShipTo_Id;
        //                resultItem.goodsIssue_No = resDataQRCode.GoodsIssue_No;
        //                resultItem.chuteId = resDataQRCode.Chute_Id;

        //                if (resultItem != null)
        //                {
        //                    //updateTagOut(data, 1);
        //                    createOrUpdateRollCageOrder(data, resultItem, 0);
        //                }

        //            }
        //        }

        //        #region remove temp location ที่ ต้นทาง
        //        tmp_RollCageOrder dataModelTempDel = new tmp_RollCageOrder();
        //        var modelDel = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).ToList();

        //        if (modelDel.Count > 0)
        //        {
        //            db.tmp_RollCageOrder.RemoveRange(modelDel);
        //            //return result;
        //        }

        //        var transactionTemp = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        //        try
        //        {
        //            db.SaveChanges();
        //            transactionTemp.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transactionTemp.Rollback();
        //            throw ex;
        //        }
        //        #endregion
        //        //}

        //        var moveRollCageModel = new
        //        {
        //            rollCageID = data.rollCage_Id,
        //            movementType = 1,
        //            sourceStation = Convert.ToInt16(data.location_Id),
        //            destStation = Convert.ToInt16(viewLocationRollCage.Location_Id),
        //        };

        //        var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));

        //        var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        //        try
        //        {
        //            dbMaster.SaveChanges();
        //            transaction.Commit();
        //            result = true;

        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool sendRollCageToStaging(LocationRollCageViewModel data)
        {
            try
            {
                bool result = false;

                //var resultItem = new LocationRollCageViewModel();

                //var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC") && c.Location_Id == data.location_Id_To).FirstOrDefault();

                //if (viewLocationRollCage == null)
                //{
                //    return result;
                //}

                var modelRollCage = dbMaster.ms_RollCage.Where(c => c.RollCage_Index == new Guid(data.rollCage_Index.ToString())).FirstOrDefault();

                if (modelRollCage == null)
                {
                    return result;
                }

                if(Convert.ToInt16(data.location_Id) == 0)
                {
                    return result;
                }

                #region suggest location near dock

                var _tagNo = "";

                if (data.location_Name != "RC-IN-001" && data.location_Name != "RC-IN-002" && data.location_Name != "RC-IN-003" && data.location_Name != "RC-IN-004")
                {
                    var resTmpQRCode = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == modelRollCage.RollCage_Id).FirstOrDefault();
                    if (resTmpQRCode == null)
                    {
                        return result;
                    }
                    else
                    {
                        _tagNo = resTmpQRCode.TagOut_No;
                    }
                }
                else
                {

                    var resTmpQRCode = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == modelRollCage.RollCage_Id).FirstOrDefault();
                    if (resTmpQRCode == null)
                    {
                        var resQRCode = db.im_RollCageOrder.Where(c => c.RollCage_Id == modelRollCage.RollCage_Id && c.Document_Status == 0).FirstOrDefault();
                        if (resQRCode == null)
                        {
                            return result;
                        }
                        else
                        {
                            _tagNo = resQRCode.DocumentRef_No1;
                        }
                    }
                    else
                    {
                        _tagNo = resTmpQRCode.TagOut_No;
                    }

                }

                //var resTmpQRCode = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == modelRollCage.RollCage_Id).FirstOrDefault();
                //if (resTmpQRCode == null)
                //{
                //    return result;
                //}

                //var resTruckLoad = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == resTmpQRCode.TagOut_No).FirstOrDefault();
                //if (resTruckLoad == null)
                //{
                //    return result;
                //}

                var resTruckLoad = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == _tagNo).FirstOrDefault();
                if (resTruckLoad == null)
                {
                    return result;
                }

                var resLocationDock = dbMaster.View_DockLocationRollCage.Where(c => c.Ref_Document_No == resTruckLoad.TruckLoad_No).FirstOrDefault();
                if (resLocationDock == null)
                {
                    return result;
                }

                int _locationDock = 0;
                _locationDock = Convert.ToInt16(resLocationDock.Location_Id);

                int _locationMin = 0;
                int _locationMax = 0;

                switch (_locationDock)
                {

                    case int n when (n <= 73 && n >= 53):
                        _locationMin = 119;
                        _locationMax = 212;
                        break;

                    case int n when (n <= 95 && n >= 75):
                        _locationMin = 213;
                        _locationMax = 306;
                        break;

                    case int n when (n <= 117 && n >= 97):
                        _locationMin = 307;
                        _locationMax = 396;
                        break;
                }

                var _locationRollCage = dbMaster.ms_RollCage.AsQueryable().ToList(); // Add new


                int i = 0;
                View_LocationRollCage viewLocationRollCage = new View_LocationRollCage();
                ms_Staging_location checkLocation = new ms_Staging_location();
                do
                {
                    var _viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
                    && !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
                    && string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).ToList();

                    viewLocationRollCage = _viewLocationRollCage.Where(c => Convert.ToInt16(c.Location_Id) >= _locationMin && Convert.ToInt16(c.Location_Id) <= _locationMax).OrderBy(o => o.Location_Id).FirstOrDefault();

                    if (viewLocationRollCage == null)
                    {
                        //do
                        //{
                        //    viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
                        //    && !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
                        //    && string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).FirstOrDefault();

                        //    if (viewLocationRollCage == null)
                        //    {
                        //        return result;
                        //    }

                        //    ms_Staging_location staging_Location_2 = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == null);
                        //    if (staging_Location_2 != null)
                        //    {
                        //        staging_Location_2.RollCage_Id = data.rollCage_Id;

                        //        var tran_2 = dbMaster.Database.BeginTransaction();
                        //        try
                        //        {
                        //            dbMaster.SaveChanges();
                        //            tran_2.Commit();
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            tran_2.Rollback();
                        //        }
                        //    }

                        //    Thread.Sleep(1000);

                        //    checkLocation = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == data.rollCage_Id);
                        //    i++;
                        //} while (checkLocation == null && i != 10);

                        viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
                            && !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
                            && string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).FirstOrDefault();

                        if (viewLocationRollCage == null)
                        {
                            return result;
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

                //var _viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
                //&& !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
                //&& string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).ToList();

                //var viewLocationRollCage = _viewLocationRollCage.Where(c => Convert.ToInt16(c.Location_Id) >= _locationMin && Convert.ToInt16(c.Location_Id) <= _locationMax).OrderBy(o => o.Location_Id).FirstOrDefault();

                //if (viewLocationRollCage == null)
                //{
                //    viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
                //    && !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
                //    && string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).FirstOrDefault();

                //    if (viewLocationRollCage == null)
                //    {
                //        return result;
                //    }
                //}







                //return result;

                #endregion

                //var _locationRollCage = dbMaster.ms_RollCage.AsQueryable().ToList(); // Add new

                //var viewLocationRollCage = dbMaster.View_LocationRollCage.Where(c => c.LocationType_Index == new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC")
                //&& !_locationRollCage.Select(x => x.Location_Id).Contains(c.Location_Id) // Add new
                //&& string.IsNullOrEmpty(c.RollCage_Index.ToString())).OrderBy(o => o.Location_Id).FirstOrDefault();

                if (viewLocationRollCage == null)
                {
                    return result;
                }

                #region suggestion rollcage location
                tmp_SuggestRollCageLocation tmpSuggestDest = new tmp_SuggestRollCageLocation();
                tmpSuggestDest.Temp_Index = new Guid();
                //tmpSuggestSource.MovementType = model.movementType;
                tmpSuggestDest.RollCage_ID = data.rollCage_Id;
                tmpSuggestDest.Location_Index = viewLocationRollCage.Location_Index;
                tmpSuggestDest.Location_Id = viewLocationRollCage.Location_Id;
                tmpSuggestDest.Location_Name = viewLocationRollCage.Location_Name;
                tmpSuggestDest.Location_Type = "DESTSTATION";
                tmpSuggestDest.Create_By = "SUGGESTION SEND ROLLCAGE";
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
                    //throw ex;
                }
                #endregion

                var removeRollcageReserve = dbMaster.ms_Staging_location.FirstOrDefault(c => c.Location_Index == viewLocationRollCage.Location_Index && c.RollCage_Id == data.rollCage_Id);
                if (removeRollcageReserve != null)
                {
                    removeRollcageReserve.RollCage_Id = null;

                    var transactionRemoveRollcageReserve = dbMaster.Database.BeginTransaction();
                    try
                    {
                        dbMaster.SaveChanges();
                        transactionRemoveRollcageReserve.Commit();
                    }
                    catch (Exception ex)
                    {
                        transactionRemoveRollcageReserve.Rollback();
                        throw ex;
                    }

                }

                var resTmpRollCage = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).ToList();

                //if (resTmpRollCage.Count > 0)
                //{
                //    return result;
                //}

                var moveRollCageModel = new
                {
                    rollCageID = data.rollCage_Id,
                    movementType = 1,
                    sourceStation = Convert.ToInt16(data.location_Id),
                    destStation = Convert.ToInt16(viewLocationRollCage.Location_Id),
                };

                var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));

                if (resTmpRollCage.Count > 0)
                {
                    //var moveRollCageModel = new
                    //{
                    //    rollCageID = data.rollCage_Id,
                    //    movementType = 1,
                    //    sourceStation = Convert.ToInt16(data.location_Id),
                    //    destStation = Convert.ToInt16(viewLocationRollCage.Location_Id),
                    //};

                    //var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));

                    foreach (var item in resTmpRollCage)
                    {
                        var chkDataRollcageOrder = db.im_RollCageOrder.Where(c => c.DocumentRef_No1 == item.TagOut_No && c.Document_Status != -1).ToList();
                        if (chkDataRollcageOrder.Count > 0)
                        {
                            // have data in rollcageOrder
                            continue;
                        }

                        var resDataQRCode = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == item.TagOut_No).FirstOrDefault();

                        var resultItem = new GoodsIssueTruckloadRollCageViewModel();

                        if (resDataQRCode == null)
                        {
                            continue;
                        }

                        data.qrcode = resDataQRCode.TagOut_No;

                        resultItem.truckLoad_No = resDataQRCode.TruckLoad_No;
                        resultItem.truckLoad_Index = resDataQRCode.TruckLoad_Index;
                        resultItem.planGoodsIssue_No = resDataQRCode.PlanGoodsIssue_No;
                        resultItem.shipTo_Index = resDataQRCode.ShipTo_Index;
                        resultItem.shipTo_Id = resDataQRCode.ShipTo_Id;
                        resultItem.goodsIssue_No = resDataQRCode.GoodsIssue_No;
                        resultItem.chuteId = resDataQRCode.Chute_Id;

                        if (resultItem != null)
                        {
                            //updateTagOut(data, 1);
                            createOrUpdateRollCageOrder(data, resultItem, 0);
                        }

                    }
                }

                #region remove temp location ที่ ต้นทาง
                tmp_RollCageOrder dataModelTempDel = new tmp_RollCageOrder();
                var modelDel = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).ToList();

                if (modelDel.Count > 0)
                {
                    db.tmp_RollCageOrder.RemoveRange(modelDel);
                    //return result;
                }

                var transactionTemp = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    db.SaveChanges();
                    transactionTemp.Commit();
                }
                catch (Exception ex)
                {
                    transactionTemp.Rollback();
                    throw ex;
                }
                #endregion
                //}

                //var moveRollCageModel = new
                //{
                //    rollCageID = data.rollCage_Id,
                //    movementType = 1,
                //    sourceStation = Convert.ToInt16(data.location_Id),
                //    destStation = Convert.ToInt16(viewLocationRollCage.Location_Id),
                //};

                //var response_MoveRollCage = utils.SendDataApi<dynamic>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));

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

        public bool createOrUpdateRollCageOrder(LocationRollCageViewModel data, GoodsIssueTruckloadRollCageViewModel dataOrder, int type) // 0 = create , 1 = update
        {
            try
            {
                bool result = false;

                if (string.IsNullOrEmpty(data.rollCage_Index.ToString()) || string.IsNullOrEmpty("00000000-0000-0000-0000-000000000000"))
                {
                    return result;
                }

                if (type == 1)
                {
                    if (!string.IsNullOrEmpty(data.qrcode.ToString()))
                    {
                        var modelRollCageOrder = db.im_RollCageOrder.Where(c => c.DocumentRef_No1 == data.qrcode && c.Document_Status == 0).FirstOrDefault();

                        modelRollCageOrder.Document_Status = -1;
                        modelRollCageOrder.Update_By = data.create_By;
                        modelRollCageOrder.Update_Date = DateTime.Now;
                    }
                }
                else
                {
                    im_RollCageOrder itemHeader = new im_RollCageOrder();

                    itemHeader.RollCageOrder_Index = Guid.NewGuid();
                    itemHeader.RollCage_Index = data.rollCage_Index.ToGuid();
                    itemHeader.RollCage_Id = data.rollCage_Id;
                    itemHeader.RollCage_Name = data.rollCage_Name;
                    itemHeader.PlanGoodsIssue_No = dataOrder.planGoodsIssue_No; //
                    itemHeader.PlanGoodsIssue_Date = DateTime.Now;//
                                                                  //itemHeader.PlanGoodsIssue_Due_Date = dataOrder.planGoodsIssue_Due_Date;//
                    itemHeader.Owner_Index = new Guid("02B31868-9D3D-448E-B023-05C121A424F4"); //
                    itemHeader.Owner_Id = "3419"; //
                    itemHeader.Owner_Name = "Amazon"; //
                    //itemHeader.DocumentType_Index
                    //itemHeader.DocumentType_Id
                    //itemHeader.DocumentType_Name
                    itemHeader.SoldTo_Index = new Guid("00000000-0000-0000-0000-000000000000");
                    //itemHeader.SoldTo_Id
                    //itemHeader.SoldTo_Name
                    //itemHeader.SoldTo_Address
                    //itemHeader.SoldTo_Contact_Person
                    itemHeader.ShipTo_Index = dataOrder.shipTo_Index;
                    //itemHeader.ShipTo_Id
                    //itemHeader.ShipTo_Name
                    //itemHeader.ShipTo_Address
                    //itemHeader.ShipTo_Contact_Person
                    //itemHeader.Branch_Code
                    //itemHeader.Box_No
                    //itemHeader.Total_Box
                    //itemHeader.Chute_Index
                    itemHeader.Chute_Id = dataOrder.chuteId;
                    //itemHeader.Chute_Name
                    //itemHeader.ChuteUserCreate_By
                    //itemHeader.ChuteUserCreate_Date
                    //itemHeader.GoodsIssue_Index = dataOrder.g
                    itemHeader.GoodsIssue_No = dataOrder.goodsIssue_No;
                    //itemHeader.GoodsIssue_Date
                    //itemHeader.Round_Index
                    //itemHeader.Round_Id
                    //itemHeader.Round_Name
                    //itemHeader.Route_Index
                    //itemHeader.Route_Id
                    //itemHeader.Route_Name
                    //itemHeader.SubRoute_Index
                    //itemHeader.SubRoute_Id
                    //itemHeader.SubRoute_Name
                    //itemHeader.Dock_Index
                    //itemHeader.Dock_Id
                    //itemHeader.Dock_Name
                    //itemHeader.DockUserCreate_By
                    //itemHeader.DockUserCreate_Date
                    itemHeader.TruckLoad_Index = dataOrder.truckLoad_Index;
                    itemHeader.TruckLoad_No = dataOrder.truckLoad_No;
                    //itemHeader.TruckLoad_Date
                    //itemHeader.BoxType_Index
                    //itemHeader.BoxType_Id
                    //itemHeader.BoxType_Name
                    //itemHeader.BoxSize_Index
                    //itemHeader.BoxSize_Id
                    //itemHeader.BoxSize_Name
                    itemHeader.DocumentRef_No1 = data.qrcode;
                    //itemHeader.DocumentRef_No2
                    //itemHeader.DocumentRef_No3
                    //itemHeader.DocumentRef_No4
                    //itemHeader.DocumentRef_No5
                    //itemHeader.Document_Remark
                    itemHeader.Document_Status = 0;
                    //itemHeader.UDF_1
                    //itemHeader.UDF_2
                    //itemHeader.UDF_3
                    //itemHeader.UDF_4
                    //itemHeader.UDF_5
                    //itemHeader.SoldTo_SubDistrict_Index
                    //itemHeader.SoldTo_District_Index
                    //itemHeader.SoldTo_Province_Index
                    //itemHeader.SoldTo_Country_Index
                    //itemHeader.SoldTo_Postcode_Index
                    //itemHeader.SubDistrict_Index
                    //itemHeader.District_Index
                    //itemHeader.Province_Index
                    //itemHeader.Country_Index
                    //itemHeader.Postcode_Index
                    //itemHeader.UserAssign
                    itemHeader.Create_By = data.create_By;
                    itemHeader.Create_Date = DateTime.Now;
                    //itemHeader.Update_By
                    //itemHeader.Update_Date
                    //itemHeader.Cancel_By
                    //itemHeader.Cancel_Date
                    //itemHeader.Confrim_By
                    //itemHeader.Confrim_Date

                    //im_RollCageOrder resultItem = new im_RollCageOrder();

                    db.im_RollCageOrder.Add(itemHeader);
                }

                var transaction = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                try
                {
                    db.SaveChanges();
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

        #region scanQRCodeOrder
        public string scanQRCodeOrder(LocationRollCageViewModel data)
        {
            try
            {
                string result = "";

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    // Check Rollcage BUF-CH
                    var dataRollCageBUFCH = dbMaster.ms_RollCage.Where(c => c.RollCage_Id == data.rollCage_Id && c.Location_Id == "BUF-CH").FirstOrDefault();
                    if (dataRollCageBUFCH == null)
                    {
                        return result = "RollCage : " + data.rollCage_Id + " ข้อมูลไม่อยู่ในตำแหน่งที่สแกนเข้าระบบได้";
                    }

                    // Check RollCage Use by Chute
                    var dataTmpRollCage = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).OrderBy(o => o.Create_Date).FirstOrDefault();
                    if (dataTmpRollCage != null)
                    {
                        if (dataTmpRollCage.Location_Id != data.chute_Id)
                        {
                            return result = "RollCage เบอร์ : " + data.rollCage_Id + " ถูกใช้งานโดย Chute : " + dataTmpRollCage.Location_Id;
                        }
                    }

                    // Check RollCage Send Staging
                    var chkRollCageSending = dbMaster.tmp_SuggestRollCageLocation.Where(c => c.RollCage_ID == data.rollCage_Id).ToList();
                    if (chkRollCageSending.Count > 0)
                    {
                        return result = "ไม่สามารถ scan ได้เนื่องจาก RollCage : " + data.rollCage_Id + " กำลังส่งงาน";
                    }

                    // Check pallet inspection
                    var chkInspection = new
                    {
                        toteID = data.qrcode,
                    };

                    string response_chkInspection = utils.SendDataApi<string>(new AppSettingConfig().GetUrl("chkInspection"), JsonConvert.SerializeObject(chkInspection));

                    if(response_chkInspection != "")
                    {
                        return result = response_chkInspection;
                    }

                    var resData = db.tmp_RollCageOrder.Where(c => c.TagOut_No == data.qrcode).ToList();
                    if(resData.Count > 0)
                    {
                        return result = "QRCode นี้ถูก Scan แล้ว โดย RollCage เบอร์ : " + resData[0].RollCage_ID;
                    }

                    var resDataOrder = db.im_RollCageOrder.Where(c => c.DocumentRef_No1 == data.qrcode && c.Document_Status == 0).ToList();
                    if (resDataOrder.Count > 0)
                    {
                        return result = "QRCode นี้ถูก Scan แล้ว โดย RollCage เบอร์ : " + resDataOrder[0].RollCage_Id;
                    }

                    else
                    {
                        var resCheckQRCode = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == data.qrcode && c.TagOut_Status == 0 && c.Chute_Id == data.chute_Id).ToList();
                        if (resCheckQRCode.Count == 0)
                        {
                            return result = "ไม่พบ QRCode";
                        }

                        if (resCheckQRCode[0].CountScanBOX >= resCheckQRCode[0].TotalBOX)
                        {
                            return result = "ไม่สามารถ Scan ได้ เนื่องจากครบตามจำนวนที่ Scan แล้ว";
                        }

                        if (resCheckQRCode.Count > 0)
                        {
                            #region addorder
                            tmp_RollCageOrder tmpDataQRCode = new tmp_RollCageOrder();
                            tmpDataQRCode.Temp_Index = new Guid();
                            //tmpSuggestSource.MovementType = model.movementType;
                            tmpDataQRCode.Location_Id = data.chute_Id;
                            tmpDataQRCode.RollCage_ID = data.rollCage_Id;
                            tmpDataQRCode.TagOut_No = data.qrcode;
                            tmpDataQRCode.Create_By = data.create_By;
                            tmpDataQRCode.Create_Date = DateTime.Now;

                            db.tmp_RollCageOrder.Add(tmpDataQRCode);

                            //Update TagOut
                            updateTagOut(data, 1);

                            var updatePalletSortDest = new
                            {
                                qrCode = data.qrcode,
                            };

                            ReworkRollCageViewModel response_MoveRollCage = utils.SendDataApi<ReworkRollCageViewModel>(new AppSettingConfig().GetUrl("updatePalletSortDest"), JsonConvert.SerializeObject(updatePalletSortDest));

                            var transIns = db.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                            try
                            {
                                db.SaveChanges();
                                transIns.Commit();
                            }
                            catch (Exception ex)
                            {
                                transIns.Rollback();
                                throw ex;
                            }
                            #endregion
                        }
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public GoodsIssueTruckloadRollCageViewModel findQRCodeDataTruckload(LocationRollCageViewModel data)
        {
            try
            {
                var resultItem = new GoodsIssueTruckloadRollCageViewModel();

                if (!string.IsNullOrEmpty(data.qrcode))
                {
                    var query = db.View_GoodsIssueTruckload_RollCage.AsQueryable();
                    query = query.Where(c => c.TagOut_No == data.qrcode && c.TagOut_Status == 1);
                    var item = query.FirstOrDefault();

                    if (item == null)
                    {
                        return resultItem;
                    }

                    resultItem.truckLoad_No = item.TruckLoad_No;
                    resultItem.truckLoad_Index = item.TruckLoad_Index;
                    resultItem.planGoodsIssue_Index = item.PlanGoodsIssue_Index;
                    resultItem.planGoodsIssue_No = item.PlanGoodsIssue_No;
                    resultItem.shipTo_Index = item.ShipTo_Index;
                    resultItem.shipTo_Id = item.ShipTo_Id;
                    resultItem.branch_Name = item.Branch_Name;
                    resultItem.branch_Code = item.Branch_Code;
                    resultItem.shipTo_Address = item.ShipTo_Address;
                    resultItem.goodsIssue_No = item.GoodsIssue_No;
                    resultItem.route_Index = item.Route_Index;
                    resultItem.route_Id = item.Route_Id;
                    resultItem.route_Name = item.Route_Name;
                    resultItem.round_Index = item.Round_Index;
                    resultItem.round_Id = item.Round_Id;
                    resultItem.round_Name = item.Round_Name;
                    resultItem.goodsIssueItemLocation_Index = item.GoodsIssueItemLocation_Index;
                    resultItem.tagOut_Index = item.TagOut_Index;
                    resultItem.tagOut_No = item.TagOut_No;
                    resultItem.tagOut_Status = item.TagOut_Status;
                    resultItem.tagOutRef_No4 = item.TagOutRef_No4;
                    resultItem.tagOutRef_No5 = item.TagOutRef_No5;
                    resultItem.countScanBOX = item.CountScanBOX;
                    resultItem.totalBOX = item.TotalBOX;

                    var _countScanTmpBOX = 0;

                    //var resultOrder = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id).OrderBy(o => o.TagOut_No).ToList();

                    var resultOrder = db.tmp_RollCageOrder.Where(c => c.RollCage_ID == data.rollCage_Id)
                        .GroupBy(c => new
                        {
                            c.TagOut_No
                        }).Select(c => new
                        {
                            c.Key.TagOut_No
                        }).ToList();

                    if (resultOrder.Count > 0)
                    {
                        _countScanTmpBOX = resultOrder.Count;
                    }

                    resultItem.countTmpRollcageOrder = _countScanTmpBOX;
                    resultItem.chuteId = item.Chute_Id;

                    //if (resultOrder.Count > 0)
                    //{
                    //    //resultItem.countScanBOX = resultOrder.Count;
                    //    resultItem.countScanBOX = _countScanTmpBOX + resultOrder.Count;
                    //}




                    //var truckLoadNo = "";

                    //if (!string.IsNullOrEmpty(data.qrcode))
                    //{
                    //    truckLoadNo = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TagOut_No == data.qrcode).FirstOrDefault().TruckLoad_No.ToString();
                    //}

                    //var groupData = db.View_GoodsIssueTruckload_RollCage.Where(c => c.TruckLoad_No == truckLoadNo)
                    //.GroupBy(c => new
                    //{
                    //    c.PlanGoodsIssue_No,
                    //    c.TruckLoad_No,
                    //    c.Route_Id,
                    //    c.ShipTo_Id,
                    //    c.Branch_Name,
                    //    c.ShipTo_Address,
                    //    c.GoodsIssue_No,
                    //    c.Round_Name,
                    //    c.CountScanBOX,
                    //    c.TotalBOX
                    //}).Select(c => new
                    //{
                    //    Iscomplete = c.Key.CountScanBOX != c.Key.TotalBOX ? 0 : 1,
                    //    c.Key.PlanGoodsIssue_No,
                    //    c.Key.TruckLoad_No,
                    //    c.Key.Route_Id,
                    //    c.Key.ShipTo_Id,
                    //    c.Key.Branch_Name,
                    //    c.Key.ShipTo_Address,
                    //    c.Key.GoodsIssue_No,
                    //    c.Key.Round_Name,
                    //    c.Key.CountScanBOX,
                    //    c.Key.TotalBOX
                    //}).FirstOrDefault();

                    //resultItem.countScanBOX = groupData == null ? 0 : groupData.CountScanBOX + _countScanTmpBOX;
                }

                return resultItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}