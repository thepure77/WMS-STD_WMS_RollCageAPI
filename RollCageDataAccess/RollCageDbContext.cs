
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RollCageDataAccess.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class RollCageDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public virtual DbSet<im_PlanGoodsIssue> im_PlanGoodsIssue { get; set; }
        public virtual DbSet<im_PlanGoodsIssueItem> im_PlanGoodsIssueItem { get; set; }
        public virtual DbSet<im_PlanGoodsIssueChute> im_PlanGoodsIssueChute { get; set; }
        public virtual DbSet<View_PlanGIWithWave> View_PlanGIWithWave { get; set; }
        public virtual DbSet<View_PlanGI> View_PlanGI { get; set; }

        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }
        public virtual DbSet<View_PlanGiProcessStatus> View_PlanGiProcessStatus { get; set; }

        public virtual DbSet<im_Signatory_log> im_Signatory_log { get; set; }

        public DbSet<im_DocumentFile> im_DocumentFile { get; set; }

        public virtual DbSet<im_Pack> im_Pack { get; set; }
        public virtual DbSet<im_PackItem> im_PackItem { get; set; }
        public virtual DbSet<View_PrintOutPlanGI> View_PrintOutPlanGI { get; set; }

        public virtual DbSet<im_RollCage_Movement> im_RollCage_Movement { get; set; }

        public virtual DbSet<im_RollCageOrder> im_RollCageOrder { get; set; }
        public virtual DbSet<im_GoodsIssue> im_GoodsIssue { get; set; }
        public virtual DbSet<im_GoodsIssueItem> im_GoodsIssueItem { get; set; }
        public virtual DbSet<im_GoodsIssueItemLocation> im_GoodsIssueItemLocation { get; set; }
        public virtual DbSet<im_TruckLoad> im_TruckLoad { get; set; }
        public virtual DbSet<im_TruckLoadImages> im_TruckLoadImages { get; set; }
        public virtual DbSet<im_TruckLoadItem> im_TruckLoadItem { get; set; }
        public virtual DbSet<View_GoodsIssuecount_tag> View_GoodsIssuecount_tag { get; set; }
        public virtual DbSet<View_GoodsIssueItemLocation_tag> View_GoodsIssueItemLocation_tag { get; set; }
        public virtual DbSet<View_GoodsIssueTruckload> View_GoodsIssueTruckload { get; set; }
        public virtual DbSet<View_GoodsIssueTruckload_RollCage> View_GoodsIssueTruckload_RollCage { get; set; }
        public virtual DbSet<wm_TagOut> wm_TagOut { get; set; }
        public virtual DbSet<wm_TagOutItem> wm_TagOutItem { get; set; }
        public virtual DbSet<wm_TagOutPick> wm_TagOutPick { get; set; }
        public virtual DbSet<im_Task> Im_Task { get; set; }
        public virtual DbSet<im_TaskItem> Im_TaskItem { get; set; }
        public virtual DbSet<tmp_RollCageOrder> tmp_RollCageOrder { get; set; }
        public virtual DbSet<View_RollCage_With_Tag> View_RollCage_With_Tag { get; set; }
        public virtual DbSet<View_RollCage_CheckQr> View_RollCage_CheckQr { get; set; }
        public virtual DbSet<View_CheckTagoutWithRollcage> View_CheckTagoutWithRollcage { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
