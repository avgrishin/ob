﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MO5.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MiddleOfficeEntities : DbContext
    {
        public MiddleOfficeEntities()
            : base("name=MiddleOfficeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<taLib> taLib { get; set; }
        public virtual DbSet<tObjClassifier> tObjClassifier { get; set; }
        public virtual DbSet<tConseilHoraire> tConseilHoraire { get; set; }
        public virtual DbSet<tEnvoiHoraire> tEnvoiHoraire { get; set; }
        public virtual DbSet<tEnvoiHoraireType> tEnvoiHoraireType { get; set; }
        public virtual DbSet<tEnvoiExec> tEnvoiExec { get; set; }
        public virtual DbSet<tRiskMap> tRiskMap { get; set; }
        public virtual DbSet<tRiskMapHoraire> tRiskMapHoraire { get; set; }
        public virtual DbSet<tWorkDate> tWorkDate { get; set; }
        public virtual DbSet<tInvestDeclWhere> tInvestDeclWhere { get; set; }
        public virtual DbSet<tInvestDecl> tInvestDecl { get; set; }
        public virtual DbSet<tInvestDeclGroupType> tInvestDeclGroupType { get; set; }
        public virtual DbSet<tInvestDeclType> tInvestDeclType { get; set; }
        public virtual DbSet<tSecurity> tSecurity { get; set; }
        public virtual DbSet<tSecurityGroup> tSecurityGroup { get; set; }
        public virtual DbSet<tFinInst> tFinInst { get; set; }
        public virtual DbSet<tInvestDeclSec> tInvestDeclSec { get; set; }
        public virtual DbSet<tFinInstGroup> tFinInstGroup { get; set; }
        public virtual DbSet<tWarrant> tWarrant { get; set; }
        public virtual DbSet<tRegDoc> tRegDoc { get; set; }
        public virtual DbSet<tSecType> tSecType { get; set; }
        public virtual DbSet<tEnvoi> tEnvoi { get; set; }
        public virtual DbSet<tSecuritySecurityGroup> tSecuritySecurityGroup { get; set; }
        public virtual DbSet<tTreaty> tTreaty { get; set; }
        public virtual DbSet<tObjClsRelation> tObjClsRelation { get; set; }
        public virtual DbSet<aspnet_Applications> aspnet_Applications { get; set; }
        public virtual DbSet<aspnet_Membership> aspnet_Membership { get; set; }
        public virtual DbSet<aspnet_Paths> aspnet_Paths { get; set; }
        public virtual DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUsers { get; set; }
        public virtual DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        public virtual DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        public virtual DbSet<aspnet_Roles> aspnet_Roles { get; set; }
        public virtual DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        public virtual DbSet<aspnet_Users> aspnet_Users { get; set; }
        public virtual DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        public virtual DbSet<tInvestDeclLink> tInvestDeclLink { get; set; }
        public virtual DbSet<tAmortization> tAmortization { get; set; }
        public virtual DbSet<tConseil> tConseil { get; set; }
        public virtual DbSet<tCoupon> tCoupon { get; set; }
        public virtual DbSet<tReestr> tReestr { get; set; }
        public virtual DbSet<tRegDocContr> tRegDocContr { get; set; }
        public virtual DbSet<tODRests> tODRests { get; set; }
        public virtual DbSet<tODTurns> tODTurns { get; set; }
        public virtual DbSet<tPortfolioType> tPortfolioType { get; set; }
        public virtual DbSet<tPortfolio> tPortfolio { get; set; }
        public virtual DbSet<tPortfolioTreaty> tPortfolioTreaty { get; set; }
        public virtual DbSet<tExchPrice> tExchPrice { get; set; }
        public virtual DbSet<tExchSecurity> tExchSecurity { get; set; }
        public virtual DbSet<tRate> tRate { get; set; }
        public virtual DbSet<tSecurityRate> tSecurityRate { get; set; }
        public virtual DbSet<tModDeal> tModDeal { get; set; }
        public virtual DbSet<tEDO> tEDO { get; set; }
        public virtual DbSet<tEnregistrement> tEnregistrement { get; set; }
        public virtual DbSet<tEnregDTSteps> tEnregDTSteps { get; set; }
        public virtual DbSet<tEnregSteps> tEnregSteps { get; set; }
        public virtual DbSet<tEnregistrementLog> tEnregistrementLog { get; set; }
        public virtual DbSet<tLimitList> tLimitList { get; set; }
        public virtual DbSet<tQuik> tQuik { get; set; }
        public virtual DbSet<tQuikUser> tQuikUser { get; set; }
        public virtual DbSet<tQuikDealStatus> tQuikDealStatus { get; set; }
        public virtual DbSet<tQuikDealType> tQuikDealType { get; set; }
        public virtual DbSet<tQuikRate> tQuikRate { get; set; }
        public virtual DbSet<tQuikDeal> tQuikDeal { get; set; }
        public virtual DbSet<tTreatyCode> tTreatyCode { get; set; }
        public virtual DbSet<tMyTreatyCode> tMyTreatyCode { get; set; }
        public virtual DbSet<tInvestDeclErr> tInvestDeclErr { get; set; }
        public virtual DbSet<tODFaceAccs> tODFaceAccs { get; set; }
        public virtual DbSet<tQUIK_UK_Deal> tQUIK_UK_Deal { get; set; }
        public virtual DbSet<tPayment> tPayment { get; set; }
        public virtual DbSet<tBank> tBank { get; set; }
    
        public virtual ObjectResult<upCheckDecl_Result> upCheckDecl(Nullable<int> investDeclID, Nullable<System.DateTime> dt, Nullable<bool> withMD, Nullable<System.Guid> userID, Nullable<bool> errorsOnly, Nullable<bool> notSelect, Nullable<bool> saveResult)
        {
            var investDeclIDParameter = investDeclID.HasValue ?
                new ObjectParameter("InvestDeclID", investDeclID) :
                new ObjectParameter("InvestDeclID", typeof(int));
    
            var dtParameter = dt.HasValue ?
                new ObjectParameter("dt", dt) :
                new ObjectParameter("dt", typeof(System.DateTime));
    
            var withMDParameter = withMD.HasValue ?
                new ObjectParameter("withMD", withMD) :
                new ObjectParameter("withMD", typeof(bool));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(System.Guid));
    
            var errorsOnlyParameter = errorsOnly.HasValue ?
                new ObjectParameter("ErrorsOnly", errorsOnly) :
                new ObjectParameter("ErrorsOnly", typeof(bool));
    
            var notSelectParameter = notSelect.HasValue ?
                new ObjectParameter("NotSelect", notSelect) :
                new ObjectParameter("NotSelect", typeof(bool));
    
            var saveResultParameter = saveResult.HasValue ?
                new ObjectParameter("SaveResult", saveResult) :
                new ObjectParameter("SaveResult", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<upCheckDecl_Result>("upCheckDecl", investDeclIDParameter, dtParameter, withMDParameter, userIDParameter, errorsOnlyParameter, notSelectParameter, saveResultParameter);
        }
    
        [DbFunction("MiddleOfficeEntities", "tfAddWorkDate")]
        public virtual IQueryable<Nullable<System.DateTime>> tfAddWorkDate(Nullable<System.DateTime> dt, Nullable<int> days)
        {
            var dtParameter = dt.HasValue ?
                new ObjectParameter("dt", dt) :
                new ObjectParameter("dt", typeof(System.DateTime));
    
            var daysParameter = days.HasValue ?
                new ObjectParameter("days", days) :
                new ObjectParameter("days", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Nullable<System.DateTime>>("[MiddleOfficeEntities].[tfAddWorkDate](@dt, @days)", dtParameter, daysParameter);
        }
    
        public virtual int upGetRests(Nullable<System.DateTime> dt, Nullable<bool> withMD, Nullable<System.Guid> userID)
        {
            var dtParameter = dt.HasValue ?
                new ObjectParameter("dt", dt) :
                new ObjectParameter("dt", typeof(System.DateTime));
    
            var withMDParameter = withMD.HasValue ?
                new ObjectParameter("withMD", withMD) :
                new ObjectParameter("withMD", typeof(bool));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("upGetRests", dtParameter, withMDParameter, userIDParameter);
        }
    
        public virtual ObjectResult<upGetRests1_Result> upGetRests1(Nullable<System.DateTime> dt, Nullable<bool> withMD, Nullable<System.Guid> userID, Nullable<bool> isGroupSec)
        {
            var dtParameter = dt.HasValue ?
                new ObjectParameter("dt", dt) :
                new ObjectParameter("dt", typeof(System.DateTime));
    
            var withMDParameter = withMD.HasValue ?
                new ObjectParameter("withMD", withMD) :
                new ObjectParameter("withMD", typeof(bool));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(System.Guid));
    
            var isGroupSecParameter = isGroupSec.HasValue ?
                new ObjectParameter("IsGroupSec", isGroupSec) :
                new ObjectParameter("IsGroupSec", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<upGetRests1_Result>("upGetRests1", dtParameter, withMDParameter, userIDParameter, isGroupSecParameter);
        }
    
        public virtual ObjectResult<upRepCheckDecl_Result> upRepCheckDecl(Nullable<System.DateTime> d)
        {
            var dParameter = d.HasValue ?
                new ObjectParameter("d", d) :
                new ObjectParameter("d", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<upRepCheckDecl_Result>("upRepCheckDecl", dParameter);
        }
    
        public virtual ObjectResult<upCheckDeclModDeal_Result> upCheckDeclModDeal(Nullable<System.DateTime> createDate, Nullable<System.Guid> userID)
        {
            var createDateParameter = createDate.HasValue ?
                new ObjectParameter("CreateDate", createDate) :
                new ObjectParameter("CreateDate", typeof(System.DateTime));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(System.Guid));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<upCheckDeclModDeal_Result>("upCheckDeclModDeal", createDateParameter, userIDParameter);
        }
    }
}
