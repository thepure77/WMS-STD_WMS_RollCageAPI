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
using RollCageBusiness.PlanGoodsIssue;

namespace RollCageBusiness.RollCage
{
    public class CheckRollcageService
    {
        private RollCageDbContext db;
        private MasterDbContext dbMaster;

        public CheckRollcageService()
        {
            db = new RollCageDbContext();
            dbMaster = new MasterDbContext();
        }

        public CheckRollcageService(RollCageDbContext db)
        {
            this.db = db;
        }

        #region CheckRollcage_TEMP
        public List<CheckRollcageViewModel> CheckRollcage_TEMP()
        {

            try {
                List<CheckRollcageViewModel> checkRollcages = new List<CheckRollcageViewModel>();
                var result = db.tmp_RollCageOrder.GroupBy(c => new
                {
                    c.RollCage_ID,
                    c.Location_Id
                }).Select(c => new
                {
                    RollCage_ID = c.Key.RollCage_ID,
                    Chute = c.Key.Location_Id,
                    CountTag = c.Count()
                }).OrderBy(c=> c.Chute).ThenBy(c=> c.RollCage_ID).ToList();

                foreach (var item in result)
                {
                    CheckRollcageViewModel checkRollcage = new CheckRollcageViewModel();
                    checkRollcage.RollCage_ID = item.RollCage_ID;
                    checkRollcage.Chute = item.Chute;
                    checkRollcage.CountTag = item.CountTag;
                    checkRollcages.Add(checkRollcage);
                }
                return checkRollcages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}