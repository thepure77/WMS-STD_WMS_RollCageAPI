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

namespace RollCageBusiness.RollCage
{
    public class ReworkRollCageService
    {
        private RollCageDbContext db;
        private MasterDbContext dbMaster;

        public ReworkRollCageService()
        {
            db = new RollCageDbContext();
            dbMaster = new MasterDbContext();
        }

        public ReworkRollCageService(RollCageDbContext db)
        {
            this.db = db;
        }

        #region Scanshipment
        public List<ReworkRollCageViewModel> Scanshipment(ReworkRollCageViewModel data)
        {
            try
            {
                var checkall = false;
                var result = new List<ReworkRollCageViewModel>();
                var findshipment = (from TL in db.im_TruckLoad.AsQueryable()
                                    join TLI in db.im_TruckLoadItem.AsQueryable() on TL.TruckLoad_Index equals TLI.TruckLoad_Index
                                    join PGI in db.im_PlanGoodsIssue.AsQueryable() on TLI.PlanGoodsIssue_Index equals PGI.PlanGoodsIssue_Index
                                    join PGIC in db.im_PlanGoodsIssueChute.AsQueryable() on TLI.PlanGoodsIssue_Index equals PGIC.PlanGoodsIssue_Index
                                    join GIL in db.im_GoodsIssueItemLocation.AsQueryable() on TLI.PlanGoodsIssue_Index equals GIL.Ref_Document_Index
                                    join TGO in db.wm_TagOut on GIL.GoodsIssue_Index equals TGO.Ref_Document_Index
                                    select new
                                    {
                                        TL.TruckLoad_No,
                                        TL.TruckLoad_Index,
                                        GIL.GoodsIssue_Index,
                                        TLI.PlanGoodsIssue_No,
                                        PGI.PlanGoodsIssue_Index,
                                        PGI.Route_Name,
                                        TGO.TagOut_No,
                                        PGIC.Chute_No
                                    }).Where(c => c.TruckLoad_No == data.Shipment_no).GroupBy(c => new {
                                        c.TruckLoad_No,
                                        c.TruckLoad_Index,
                                        c.GoodsIssue_Index,
                                        c.PlanGoodsIssue_No,
                                        c.PlanGoodsIssue_Index,
                                        c.Route_Name,
                                        c.TagOut_No,
                                        c.Chute_No
                                    }).Select(c => new {
                                        c.Key.TruckLoad_No,
                                        c.Key.TruckLoad_Index,
                                        c.Key.GoodsIssue_Index,
                                        c.Key.PlanGoodsIssue_No,
                                        c.Key.PlanGoodsIssue_Index,
                                        c.Key.Route_Name,
                                        c.Key.TagOut_No,
                                        c.Key.Chute_No
                                    }).ToList();



                var TruckLoad = findshipment.GroupBy(c => c.TruckLoad_Index).Select(c => c.Key).ToList();
                var PlanGoodsIssue = findshipment.GroupBy(c => c.PlanGoodsIssue_No).Select(c => c.Key).ToList();
                var GoodsIssue = findshipment.GroupBy(c => c.GoodsIssue_Index).Select(c => c.Key).ToList();

                var getrollcage = db.im_RollCageOrder.Where(c => PlanGoodsIssue.Contains(c.PlanGoodsIssue_No)).GroupBy(c => new {
                    c.RollCage_Index,
                    c.Document_Status,
                    c.RollCage_Id,
                }).Select(c => new {
                    c.Key.RollCage_Index,
                    c.Key.RollCage_Id,
                    c.Key.Document_Status,
                }).ToList();

                var getrollcagecount = getrollcage;

                if (data.again)
                {
                    var location_rollcage = dbMaster.ms_RollCage.Where(c => c.Location_Name_Before == data.Location);
                    getrollcagecount = getrollcage.Where(c => location_rollcage.Select(x => x.RollCage_Index).Contains(c.RollCage_Index) && c.Document_Status != -1 && c.Document_Status == 0).ToList();
                    if (getrollcagecount.Count() <= 0)
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

                var CountRollCageOrder = db.im_RollCageOrder.Where(c => PlanGoodsIssue.Contains(c.PlanGoodsIssue_No) && c.Document_Status == 0).ToList();


                foreach (var item in getrollcagecount)
                {

                    var RollCageOrder = CountRollCageOrder.Where(c => c.RollCage_Index == item.RollCage_Index).ToList();

                    var locationrollcage = dbMaster.View_LocationRollCage.FirstOrDefault(c => c.RollCage_Index == item.RollCage_Index);
                    var resultitem = new ReworkRollCageViewModel();
                    resultitem.bill_amount = findshipment.GroupBy(c => c.PlanGoodsIssue_No).Count().ToString();
                    resultitem.route_Name = findshipment[0].Route_Name;
                    resultitem.qty_tagBox = findshipment.GroupBy(c => c.TagOut_No).Count().ToString();
                    resultitem.qty_tagTote = "0";

                    resultitem.RollCage_Index = item.RollCage_Index;
                    if (locationrollcage != null)
                    {
                        resultitem.location_rollcage = locationrollcage.Location_Id;
                        resultitem.location_rollcage_Name = locationrollcage.Location_Name;
                    }
                    resultitem.document_Status = item.Document_Status == 0 ? "Stored" : "Arrived";
                    resultitem.rollcage_id = item.RollCage_Id;
                    resultitem.carton = RollCageOrder.Count().ToString();
                    resultitem.tote_box = "0";
                    resultitem.TruckLoad_No = findshipment[0].TruckLoad_No;
                    resultitem.checkall = checkall;
                    resultitem.chute_no = findshipment[0].Chute_No;
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

        #region CallRollCage
        public ReworkRollCageViewModel CallRollCage(ReworkRollCageViewModel data)
        {
            try
            {
                var result = new ReworkRollCageViewModel();
                var location_to = dbMaster.View_LocationRollCage.FirstOrDefault(c => c.Location_Name == data.Location);

                var moveRollCageModel = new
                {
                    rollCageID = data.rollcage_id,
                    movementType = 4,
                    sourceStation = Convert.ToInt16(data.location_rollcage),
                    destStation = Convert.ToInt16(location_to.Location_Id),
                    dock_Index = "",
                    dock_Id = "",
                    dock_Name = ""
                };

                ReworkRollCageViewModel response_MoveRollCage = utils.SendDataApi<ReworkRollCageViewModel>(new AppSettingConfig().GetUrl("createMoveRollCage"), JsonConvert.SerializeObject(moveRollCageModel));
                //if (response_MoveRollCage.status == "10")
                //{

                //    RollCageService rollCageService = new RollCageService();
                //    LocationRollCageViewModel model = new LocationRollCageViewModel();

                //    model.rollCage_Index = data.RollCage_Index;
                //    model.location_Index_To = location_to.Location_Index;
                //    model.location_Id_To = location_to.Location_Id;
                //    model.location_Name_To = location_to.Location_Name;

                //    //var update_location = rollCageService.updateLocationRollCage(model);
                //    //if (!update_location)
                //    //{
                //    //    result.mess = "ไม่สามารถอัปเดตที่อยู่ได้";
                //    //}

                //    //var modelRollCage = dbMaster.ms_RollCage.FirstOrDefault(c => c.RollCage_Id == data.rollcage_id);
                //    //modelRollCage.Location_Name_Before = data.Location;

                //    var transaction = dbMaster.Database.BeginTransaction(IsolationLevel.ReadCommitted);
                //    try
                //    {
                //        dbMaster.SaveChanges();
                //        transaction.Commit();
                //    }
                //    catch (Exception ex)
                //    {
                //        transaction.Rollback();
                //        throw ex;
                //    }

                //}
                //else
                //{
                //    result.mess = "ไม่สามารถทำการเรียกได้";
                //}


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ScanLocation
        public ReworkRollCageViewModel ScanLocation(ReworkRollCageViewModel data)
        {
            try
            {
                var result = new ReworkRollCageViewModel();
                var DataLocation = dbMaster.View_LocationRollCage.Where(c => c.Location_Name == data.Location).ToList();
                if (DataLocation.Count > 0)
                {
                    DataLocation = DataLocation.Where(c => c.RollCage_Index == null).ToList(); // ถ้าเจอคือไม่ว่าง
                    if (DataLocation.Count <= 0)
                    {
                        //result.mess = "E";
                        result.mess = "Location ที่ค้นหาไม่ว่าง";
                    }
                }
                else
                {
                    result.mess = "Location อยู่ระหว่างการใช้งาน";
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