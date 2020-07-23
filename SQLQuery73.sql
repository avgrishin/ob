USE [MiddleOffice]
GO
/****** Object:  StoredProcedure [dbo].[up_avgCheckClnDecl7]    Script Date: 13.01.2020 17:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Grishin A.V.
-- ALTER  date: 05.02.2010
-- Description:	Проверка (онлайн) декларации клиента пакетная 
-- =============================================
ALTER PROCEDURE [dbo].[up_avgCheckClnDecl7]
	@DateCreate datetime,
	@ID uniqueidentifier output,
	@TransID int = null,
	@Date smalldatetime = null,
	@debug tinyint = null
	--with recompile
AS
	set nocount on
	print convert(varchar, getdate(), 113)
	declare @DateCreate_ datetime = @DateCreate
	declare @CurDate smalldatetime, @CloseDate smalldatetime, @RateDate smalldatetime, @DealDate smalldatetime, @Online tinyint
	declare @Direction tinyint
	set @Date = coalesce(@Date, cast(getdate() as date))
	set @CurDate = cast(getdate() as date)
	set @Online = case when @Date = @CurDate then 2 else 1 end
	if @Online = 2
	begin
		set @RateDate  = (select top 1 WorkDate from tWorkDate where WorkDate < @CurDate order by WorkDate desc)
		set @CloseDate = @CurDate - 1 --@RateDate
		set @DealDate = case when cast(getdate() as float) - floor(cast(getdate() as float)) > 0 then @Date else @RateDate end
	end
	else
	begin
		set @RateDate  = @Date
		set @CloseDate = @Date
		set @DealDate = @Date
	end
  /*
	if exists(
		select null
		from tModPortfolioDeal md
		 join tSecurity s on s.SecurityID = md.SecurityID and s.IssuerID in (25068, 1640, 14466, 6582, 24701, 130598)
		 left join tTreaty t on t.TreatyID = md.TreatyID
		where md.DateCreate = @DateCreate_
			and md.IsDeleted = 0
			--and (md.PortfolioID not in  (9853, 8779) /*and md.FinancialInstitutionID not in (261, 262) 05.05.17 */)
			--and not (s.IssuerID = 1640 and DealPrice = 3.18 and (md.PortfolioID in (8779, 8780) or md.FinancialInstitutionID in (262, 263) or t.FinancialInstitutionID  in (262, 263)))
		)
	begin
		raiserror('WesternZagros, ПАО "ДВМП", М.видео, ПАО "Банк "Санкт-Петербург", Far East Capital Limited, BSPB Finance P.L.C. только по согласованию', 16,1)
		return 0
	end
	if exists(
		select null
		from tModPortfolioDeal md
		 join tSecurity s on s.SecurityID = md.SecurityID and s.IssuerID in (14466)
		 left join tTreaty t on t.TreatyID = md.TreatyID
		where md.DateCreate = @DateCreate_
			and md.IsDeleted = 0
			and md.Direction = 1
			and md.DealPrice < 56.5*dbo.uf_GetFundRate(md.FundID, cast(getdate() as date), 39191)
			and (md.PortfolioID = 9860 or md.FinancialInstitutionID = 6629 or t.FinancialInstitutionID = 6629))
	begin
		raiserror('цена продажи не ниже 56,5 рублей по ценной бумаге Банк СПБ ISIN RU0009100945', 16,1)
		return 0
	end
  */
  /*
	if exists(
		select null
		from (
			select
				s.IssuerID,
				Qty = round((pd.Quantity*pd.DealPrice*case when s.SecType = 2 then dbo.uf_GetFundRate(s.RatedSecurityID, @CurDate, 39191)*coalesce((select top 1 Nominal from tAmortization where SecurityID = pd.SecurityID and AmortizationDate <= cast(pd.DateDeal as date) order by AmortizationDate desc), s.Nominal)/100 else dbo.uf_GetFundRate(pd.FundID, @CurDate, 39191) end+coalesce(pd.NKD, 0)*dbo.uf_GetFundRate(pd.FundID, @CurDate, 39191)), 2)
			from tPortfolioDeal pd
				join tSecurity s on s.SecurityID = pd.SecIssuerID and s.IssuerID in (22695, 14466, 130598) and s.IssuerID in (
					select
						s.IssuerID
					from tModPortfolioDeal pd
					 join tSecurity s on s.SecurityID = pd.SecIssuerID and s.IssuerID in (22695, 14466, 130598)
					where pd.DateCreate = @DateCreate_ and pd.IsDeleted = 0 and pd.RawDataProviderID = 5
				)
			where pd.DateDeal >= @CurDate and pd.DateDeal < @CurDate + 1 and pd.IsDeleted = 0 and pd.RawDataProviderID <> 19

			union all

			select
				s.IssuerID,
				Qty = round((pd.Quantity*pd.DealPrice*case when s.SecType = 2 then dbo.uf_GetFundRate(s.RatedSecurityID, @CurDate, 39191)*coalesce((select top 1 Nominal from tAmortization where SecurityID = pd.SecurityID and AmortizationDate <= cast(pd.DateDeal as date) order by AmortizationDate desc), s.Nominal)/100 else dbo.uf_GetFundRate(pd.FundID, @CurDate, 39191) end+coalesce(pd.NKD, 0)*dbo.uf_GetFundRate(pd.FundID, @CurDate, 39191)), 2)
			from tModPortfolioDeal pd
				join tSecurity s on s.SecurityID = pd.SecIssuerID and s.IssuerID in (22695, 14466, 130598)
			where pd.DateCreate = @DateCreate_ and pd.IsDeleted = 0 and pd.RawDataProviderID = 5
		) t
		group by t.IssuerID
		having sum(Qty) > 25000000
	)
	begin
		raiserror('ПАО "Банк "Санкт-Петербург", ПАО Протек, BSPB Finance P.L.C. только по согласованию при объеме более 25 млн. р.', 16,1)
		return 0
	end
  */
/*
	if exists(
		select null
		from tModPortfolioDeal md
		 join tSecurity s on s.SecurityID = md.SecurityID and s.IssuerID in (14466)
		 left join tTreaty t on t.TreatyID = md.TreatyID
		where md.DateCreate = @DateCreate_
			and md.IsDeleted = 0
			and md.Direction = 1
			and md.DealPrice < 56.5*dbo.uf_GetFundRate(md.FundID, cast(getdate() as date), 39191)
			and (md.PortfolioID = 9860 or md.FinancialInstitutionID = 6629 or t.FinancialInstitutionID = 6629))
	begin
		raiserror('цена продажи не ниже 56,5 рублей по ценной бумаге Банк СПБ ISIN RU0009100945', 16,1)
		return 0
	end
*/
	select mpd.PortfolioDealID, mpd.SecurityID, mpd.Direction, mpd.DealPrice, Price = coalesce(dbo.uf_avgGetMP2Online(mpd.SecurityID), (
		select mr.mr
		from tTreaty t join tTreatyTreatyType ttt on t.TreatyID = ttt.TreatyID and ttt.TreatyTypeID = 1
			join tTradeSystem ts on ts.NameBrief = 'ММВБ_'+t.Name3
			outer apply (select top 1 rp.CourseCurrent, rp.ActualizationDateTime from tRate rp where rp.TradeSystemID = ts.TradeSystemID and rp.RawDataProviderID = 1 and rp.SecurityID = mpd.SecurityID and rp.ActualizationDateTime < cast(getdate() as date) order by rp.ActualizationDateTime desc) rp
			cross apply (
			select (coalesce(p0.p0*rp.CourseCurrent, 0)+coalesce(pd.q1, 0))/nullif(coalesce(p0.p0, 0)+coalesce(pd.p1, 0), 0) mr
			from (select 1 a) a
				outer apply (
				select sum(ab.OutcomeBalanceF) p0
				from tAccount a
					join tAccountBalance ab on ab.AccountID = a.AccountID and ab.BalanceDate = rp.ActualizationDateTime
				where a.TreatyID = t.TreatyID
					and a.SecIssuerID = s.SecurityID
			) p0
				outer apply (
				select sum(d1.Quantity) p1, sum(d1.Quantity*d1.DealPrice) q1
				from tPortfolioDeal d1
					join tTreaty t1 on t1.TreatyID = d1.TreatyID and t1.FinancialInstitutionID = t.FinancialInstitutionID
				where d1.SecurityID = s.SecurityID
					and d1.DateDeal >= cast(getdate() as date) and d1.DateDeal < cast(getdate()+1 as date)
					and d1.RawDataProviderID in (5, 7)
			) pd 
		) mr
		where t.IsDisabled = 0 and t.DateFinish = '19000101' and t.FinancialInstitutionID = fi.FinancialInstitutionID
		), null)
	into #rp
	from tModPortfolioDeal mpd
		join tSecurity s on s.SecurityID = mpd.SecurityID
		left join tTreaty t1 on t1.TreatyID = mpd.TreatyID
		join tFinancialInstitution fi on fi.FinancialInstitutionID = coalesce(mpd.FinancialInstitutionID, t1.FinancialInstitutionID) and fi.NameBrief like '%ПФР%'
	where mpd.RawDataProviderID = 5 and mpd.DateCreate = @DateCreate_
	
	if exists (select null from #rp where (1-cast(DealPrice as float)/Price)*(2*Direction-1) > 0.05)
	begin
		raiserror('Цена заявки отличается от рыночной на более 5 процентов', 16,1)
		return 0
	end

	if (select count(*) from (select distinct Direction from tModPortfolioDeal where DateCreate = @DateCreate_ and IsDeleted = 0 and RawDataProviderID = 5) t) > 1
	begin
		raiserror('Разнонаправленные сделки в пакете', 16,1)
		return 0
	end
	if exists(select 1 from tModPortfolioDeal where DateCreate = @DateCreate_ and IsDeleted = 0 and FinancialInstitutionID is null)
	begin
		raiserror('Не указан клиент в моделируемой сделке', 16,1)
		return 0
	end
	if exists(select 1 from tModPortfolioDeal md join tSecurity s on s.SecurityID = md.SecurityID and s.SecType = 2 and not exists(select 1 from tCouponPeriod where SecurityID = s.SecurityID and DateEnd > @Date and CouponValue > 0) where md.DateCreate = @DateCreate_ and md.IsDeleted = 0 and md.FinancialInstitutionID = 22653)
	begin
		raiserror('Не заполнен график купонных выплат', 16,1)
		return 0
	end

	--print 'проверка существенного отклонения'

	--if exists(
	--	select 1
	--	from tModPortfolioDeal d
	--	 join tSecurity s on s.SecurityID = d.SecIssuerID and (s.SecType = 0 or (s.SecType = 2 and s.DateEnd > cast(getdate() as date))) 
	--	 join tExchangeSecurity es on es.RawDataProviderID = 11 and es.SecurityID = d.SecIssuerID
	--	where d.DateCreate = @DateCreate_ and d.IsDeleted = 0 /*and d.SecurityID not in (92728)*/)
	--begin
	--	declare @ret int
	--	exec @ret = up_avgCheckEssentialDeviation @DateCreate_
	--	if @ret = 1
	--	begin
	--		raiserror('Обнаружено существенное отклонение цены заявки.', 16,1)
	--		return 0
	--	end
	--end

	--print 'проверка существенного отклонения завершена'	

	set @Direction = coalesce((select top 1 Direction from tModPortfolioDeal where DateCreate = @DateCreate_ and IsDeleted = 0), 0)

	declare @d table (FinInstID int, FinInstID1 int, InvestDeclID int, InvestDeclLinkID int, SecurityID int, PortfolioDealOrderID int, TreatyID int, DealPrice float, AccType int, BankID int);
	with m (FinInstID, TreatyID, SecurityID, PortfolioDealOrderID, DealPrice, AccType, BankID) as (
		select
			FinancialInstitutionID,
			TreatyID,
			SecurityID,
			min(PortfolioDealOrderID),
			max(DealPrice),
			coalesce(AccType, 0),
			coalesce(case when AccType in (1,2,3, 4) then ContractorID end, 0) BankID
		from tModPortfolioDeal
		where DateCreate = @DateCreate_
			and IsDeleted = 0
		group by FinancialInstitutionID, TreatyID, SecurityID, coalesce(AccType, 0), coalesce(case when AccType in (1,2,3, 4) then ContractorID end, 0)
	)
	insert @d (FinInstID, FinInstID1, InvestDeclID, InvestDeclLinkID, SecurityID, PortfolioDealOrderID, TreatyID, DealPrice, AccType, BankID)
	select idl1.ObjID, case idl1.ObjID when t0.FinInstID then t0.FinInstID end, id.InvestmentDeclarationID, idl1.InvestmentDeclarationLinkID, t0.SecurityID, t0.PortfolioDealOrderID, t0.TreatyID, t0.DealPrice, t0.AccType, t0.BankID
	from m t0
	 join sInvestmentDeclarationLink idl on idl.ObjID = t0.FinInstID and idl.FL_Obj = 2 and idl.Start_Date <= getdate() and idl.Finish_Date > getdate() and idl.Enb = 'T'
	 join sInvestmentDeclaration id on id.InvestmentDeclarationID = idl.InvestmentDeclarationID and id.Enb = 'T' and id.InvestmentDeclarationTypeID in (1,3,4,6,8) and id.InvestmentDeclarationID not in (528, 409)
	 join sInvestmentDeclarationLink idl1 on idl1.InvestmentDeclarationID = idl.InvestmentDeclarationID and idl1.FL_Obj = idl.FL_Obj and idl1.Start_Date <= getdate() and idl1.Finish_Date > getdate() and idl1.Enb = 'T'	and (id.InvestmentDeclarationTypeID in (80) or idl.ObjID = idl1.ObjID)
	union
	select t0.FinInstID, t0.FinInstID, id.InvestmentDeclarationID, idl.InvestmentDeclarationLinkID, t0.SecurityID, t0.PortfolioDealOrderID, t0.TreatyID, t0.DealPrice, t0.AccType, t0.BankID
	from m t0
	 join tTreaty t on t.FinancialInstitutionID = t0.FinInstID and t.IsDisabled = 0
	 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.ENB = 'T'
	 join sInvestmentDeclarationLink idl on idl.ObjID = ttt.TreatyTypeID and idl.FL_Obj = 3 and idl.Start_Date <= getdate() and idl.Finish_Date > getdate() and idl.Enb = 'T'
	 join sInvestmentDeclaration id on id.InvestmentDeclarationID = idl.InvestmentDeclarationID and id.Enb = 'T' and id.InvestmentDeclarationTypeID in (1,3,4,6,8) and id.InvestmentDeclarationID not in (528, 409)
	union
	select t0.FinInstID, t0.FinInstID, id.InvestmentDeclarationID, idl.InvestmentDeclarationLinkID, t0.SecurityID, t0.PortfolioDealOrderID, t0.TreatyID, t0.DealPrice, t0.AccType, t0.BankID
	from m t0
   join tTreaty tr on tr.FinancialInstitutionID = t0.FinInstID and tr.IsDisabled = 0
   join tAccountPortfolio ap on ap.TreatyID = tr.TreatyID and ap.StartDate <= cast(getdate() as date) and ap.FinishDate > cast(getdate() as date)
	 join sInvestmentDeclarationLink idl on idl.ObjID = ap.PortfolioID and idl.FL_Obj = 1 and idl.Start_Date <= cast(getdate() as date) and idl.Finish_Date > cast(getdate() as date) and idl.Enb = 'T'
	 join sInvestmentDeclaration id on id.InvestmentDeclarationID = idl.InvestmentDeclarationID and id.Enb = 'T' and id.InvestmentDeclarationTypeID in (1,3,4,6,8) and id.InvestmentDeclarationID not in (528, 409)
	declare @calcDur bit = 0
	if exists(select 1 from @d d join sInvestmentDeclarationWhere w on w.InvestmentDeclarationID = d.InvestDeclID where w.FLAG_Group in (26,27) and w.Enb = 'T')
		set @calcDur = 1
		
	declare @b table (
		FinInstID int,
		TreatyID int,
		TreatyTypeID int,
		BankID int,
		SecurityID int,
		SecurityID1 int,
		SecBaseID int,
		SecType tinyint,
		AccType int,
		IssuerID int,
		IssuerID1 int,
		RestF decimal(20,8),
		RestP decimal(20,8),
		RestFP decimal(20,8),
		RestPP decimal(20,8),
		PriceN float,
		PriceD float,
		Coupon float,
		Duration float,
		DateEnd smalldatetime
	)
print 'Остатки'
print convert(varchar, getdate(), 113)

  declare @cUSD float = coalesce(case when @Online = 2 then (select top 1 CourseLast from tQUIK_Rate where SecurityID = 39192 and DateCourse = @CurDate) end, (select top 1 CourseBid from tRate where RawDataProviderID = 1 and TradeSystemID = 1 and SecurityID = 39192 and CourseBid > 0 and ActualizationDateTime <= @CurDate order by ActualizationDateTime desc))
  declare @cEUR float = coalesce(case when @Online = 2 then (select top 1 CourseLast from tQUIK_Rate where SecurityID = 39199 and DateCourse = @CurDate) end, (select top 1 CourseBid from tRate where RawDataProviderID = 1 and TradeSystemID = 1 and SecurityID = 39199 and CourseBid > 0 and ActualizationDateTime <= @CurDate order by ActualizationDateTime desc))
  declare @cGBP float = coalesce(case when @Online = 2 then (select top 1 CourseLast from tQUIK_Rate where SecurityID = 39188 and DateCourse = @CurDate) end, (select top 1 CourseBid from tRate where RawDataProviderID = 1 and TradeSystemID = 1 and SecurityID = 39188 and CourseBid > 0 and ActualizationDateTime <= @CurDate order by ActualizationDateTime desc))

	;with ti (FinancialInstitutionID, TreatyID, TreatyID1, TreatyTypeID, Factor) as (
		select t.FinancialInstitutionID, t.TreatyID, t.TreatyID TreatyID1, null TreatyTypeID, 1 Factor
		from tTreaty t
		 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (1,2, 340)
		where t.IsDisabled = 0 and t.FinancialInstitutionID in (select FinInstID from @d)

		union

		select t.FinancialInstitutionID, t.TreatyID, t.TreatyID, ttt.TreatyTypeID, 1
		from tTreaty t
		 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (3)
		where t.IsDisabled = 0 and t.FinancialInstitutionID in (select FinInstID from @d)

		union

		select t.FinancialInstitutionID, t.TreatyID, t1.TreatyID, null, -1
		from tTreaty t
		 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (3)
		 join tTreaty t1 on t1.TreatyID = (
			select top 1 t2.TreatyID 
			from tTreaty t2
			 join tTreatyTreatyType ttt2 on ttt2.TreatyID = t2.TreatyID and ttt2.TreatyTypeID in (1,2,340)
			where t2.FinancialInstitutionID = t.FinancialInstitutionID and t2.IsDisabled = 0
		)
		where t.IsDisabled = 0 and t.FinancialInstitutionID in (select FinInstID from @d)
	)
	insert @b (FinInstID, TreatyID, TreatyTypeID, BankID, SecurityID, SecurityID1, SecType, SecBaseID, IssuerID, IssuerID1, AccType, RestF, RestP, RestFP, RestPP, PriceN, PriceD, Coupon, Duration, DateEnd)
	select
		t.FinancialInstitutionID,
		t.TreatyID,
		t.TreatyTypeID,
		coalesce(t.BankID, 0),
		t.SecurityID,
		t.SecurityID1,
		t.SecType,
		t.SecBaseID,
		t.IssuerID,
		t.IssuerID1,
		coalesce(t.AccType, 0),
		t.RestF,
		t.RestP,
		t.RestFP,
		t.RestPP,
		PriceN = coalesce(t.PriceB, t.Price),
		PriceD = t.Price,
		t.Coupon,
		case when @calcDur = 1 and t.SecType = 2 then coalesce(
			case when ocr2.ObjClassifierID is null then cast(nullif(t.Duration, 0) as float) end/365,
			case when ocr2.ObjClassifierID is null then (select top 1 cast(er.Duration as float)/365 from tExchangeSecurity es join tExchangeRate er on er.ExchangeSecurityID = es.ExchangeSecurityID and er.RateDate = @Date where es.RawDataProviderID = 11 and es.SecurityID = t.SecurityID order by es.IsDisabled) end,
			case when 1=1 then (select Duration from dbo.uf_avgGetYieldDuration(@Date, t.SecurityID, coalesce(t.PriceB, t.Price)*100/dbo.uf_GetFundRate(t.RatedSecurityID, @Date, 39191)/coalesce((select top 1 Nominal from tAmortization where SecurityID = t.SecurityID and AmortizationDate <= @Date order by AmortizationDate desc), t.Nominal), dbo.uf_avgGetCoupon(t.SecurityID, @Date, t.RatedSecurityID), case when t.RatedSecurityID = 39191 then 1 else 0 end)) end,
			0) end,
		t.DateEnd
	from (
		select 
			t1.FinancialInstitutionID,
			t1.TreatyID,
			t1.TreatyTypeID,
			t1.BankID,
			t1.SecIssuerID SecurityID,
			t1.SecurityID SecurityID1,
			s.SecType,
			s.Nominal,
			s.RatedSecurityID,
			qr.Duration,
			SecBaseID = coalesce(sb1.SecurityID, sb.SecurityID, s.SecurityID),
			IssuerID  = coalesce(case when s.SecType = 4 and t1.AccType in (1, 3, 4) then t1.BankID when s.SecType = 4 then -t1.AccType end, sb1.IssuerID, sb.IssuerID, s.IssuerID, sb.SecurityID+10000000, sb1.SecurityID+10000000, t1.SecurityID+10000000),
			IssuerID1 = coalesce(case when s.SecType = 4 and t1.AccType in (1, 3, 4) then t1.BankID when s.SecType = 4 then -t1.AccType else s.IssuerID end, 0),
			t1.AccType,
			RestF  = t1.RestFA,
			RestP  = t1.RestPA /* *case when s.SecType = 2 and s.DateEnd < @CurDate then case when (select sum(case when abt.AccountBalanceTurnTypeID = 3 then 1 else -1 end*abt.TurnValue)	from tDeal d join tAccountBalanceTurn abt on abt.DealID = d.DealID and abt.AccountBalanceTurnTypeID in (3,7) and abt.TurnDate <= @CurDate	where d.DealDate >= s.DateEnd	and d.LeftSideID in (select TreatyID from tTreatyTreatyType where (TreatyTypeID in (1,2, 340) or t1.FinancialInstitutionID in (3394, 3395, 3396, 1790)) and TreatyID in (select TreatyID from tTreaty where FinancialInstitutionID = t1.FinancialInstitutionID and IsDisabled = 0)) and d.SecurityID = s.SecurityID and d.DealTypeID = 19) = 0 then 0 else 1 end else 1 end */,
			RestFP  = t1.RestFAP,
			RestPP  = t1.RestPAP /* *case when s.SecType = 2 and s.DateEnd < @CurDate then case when (select sum(case when abt.AccountBalanceTurnTypeID = 3 then 1 else -1 end*abt.TurnValue)	from tDeal d join tAccountBalanceTurn abt on abt.DealID = d.DealID and abt.AccountBalanceTurnTypeID in (3,7) and abt.TurnDate <= @CurDate	where d.DealDate >= s.DateEnd	and d.LeftSideID in (select TreatyID from tTreatyTreatyType where (TreatyTypeID in (1,2, 340) or t1.FinancialInstitutionID in (3394, 3395, 3396, 1790)) and TreatyID in (select TreatyID from tTreaty where FinancialInstitutionID = t1.FinancialInstitutionID and IsDisabled = 0)) and d.SecurityID = s.SecurityID and d.DealTypeID = 19) = 0 then 0 else 1 end else 1 end */,
			Price = coalesce(
				case when t1.SecurityID = 39191 then 1 end,
        case when s.SecType = 4 then nullif(qr.CourseLast, 0) end,
        cv.CourseCurrent,
				case when s.SecType in (18,24) then 0 end,
				nullif(qr.AverageWeightedPrice, 0),
        r.Price*case s.RatedSecurityID when 39192 then @cUSD when 39199 then @cEUR when 39188 then @cGBP else r.PriceCourse end,
				--r.Price*r.PriceCourse,
        case when s.SecType = 2 then 100 end*case s.RatedSecurityID when 39192 then @cUSD when 39199 then @cEUR when 39188 then @cGBP else dbo.uf_GetFundRate(s.RatedSecurityID, @Date, 39191) end
			)*coalesce(rt.CourseCurrent/100, 1)*case when s.SecType = 2 then coalesce((select top 1 Nominal from tAmortization where SecurityID = t1.SecurityID and AmortizationDate <= @CurDate order by AmortizationDate desc), s.Nominal)/100 else 1 end,
			PriceB = case when s.SecType in (18, 24) then 
          case when s.SecType = 24 and s.PayType = 1 then -1 else 1 end*coalesce(
				    case when t1.SecurityID = 39191 then 1 end,
            cv.CourseCurrent,
				    case when coalesce(sb1.SecurityID, sb.SecurityID) = 59803 then (select Close_Value*2 from tExchangeIndexValue where ExchangeIndexID = 53 and [Date] = (select top 1 [Date] from tExchangeIndexValue where ExchangeIndexID = 53 and [Date] <= @Date order by [Date] desc)) end,
				    qrb.AverageWeightedPrice*coalesce(s.NominalOriginal, 1)*coalesce(case when sb.SecType in (18,24) then sb.NominalOriginal end, 1),
				    rb.Price*rb.PriceCourse*coalesce(s.NominalOriginal, 1)*coalesce(case when sb.SecType in (18,24) then sb.NominalOriginal end, 1)
          )
        end,
      Coupon = case when s.SecType = 2 then coalesce(dbo.uf_avgGetCoupon(t1.SecurityID, @Date, s.RatedSecurityID), 0)*case s.RatedSecurityID when 39191 then 1 when 39192 then @cUSD when 39199 then @cEUR when 39188 then @cGBP else dbo.uf_GetFundRate(s.RatedSecurityID, @CurDate, 39191) end else 0 end,
			DateEnd = t1.DepDateEnd
		from (
			select
				t2.FinancialInstitutionID,
				t2.TreatyID,
				t2.TreatyTypeID,
				t2.SecurityID,
				t2.SecIssuerID,
				t2.SecID2,
				t2.DepDateEnd,
				t2.AccType,
				BankID = case when t2.AccType in (1,2,3, 4) then t2.BankID end,
				--DiasoftDealID = case when t2.AccType = 3 then 0 else DiasoftDealID end,
				RestF    = sum(t2.RestF),
				RestP    = sum(t2.RestP),
				DiaDeals = sum(t2.DiaDeals),
				MoDeals  = sum(t2.MoDeals),
				Orders   = sum(t2.Orders),
				RestFA   = sum(t2.RestF+t2.DiaDeals+t2.MoDeals+t2.ModDealsF+t2.Orders+t2.Quik),
				RestPA   = sum(case when t2.AccType = 3 then t2.RestF else t2.RestP end+t2.DiaDeals+t2.MoDeals+t2.ModDealsP+t2.Orders+t2.Quik),
				RestFAP  = sum(t2.RestF+t2.DiaDeals+t2.MoDeals              +t2.Orders+t2.Quik),
				RestPAP   = sum(case when t2.AccType = 3 then t2.RestF else t2.RestP end+t2.DiaDeals+t2.MoDeals+t2.Orders+t2.Quik)
			from (
--TreatyTypeID - (только для ДС) для Открытия берем со счета, для остальных тип договора
--AccType      - (только для ДС) если тип не найден для брокерского договора ставим 2
--BankID       - (только для ДС) для брокерского договора берем ContractorID, для ДУ если AccType <> 2 берем CityID со счета 
				select
					t.FinancialInstitutionID,
					t.TreatyID1 TreatyID,
					TreatyTypeID = case when s.SecType = 4 then ttt1.TreatyTypeID end,
					a.SecurityID,
					a.SecIssuerID,
					AccType = case when s.SecType = 4 then case when coalesce(oc.UniqueFlag, 0) = 0 and t.TreatyTypeID = 3 then 2 else oc.UniqueFlag end end,
					BankID = case when s.SecType = 4 then case when t.TreatyTypeID = 3 then tt.ContractorID when oc.UniqueFlag <> 2 then (select CityID from tFinancialInstitution where FinancialInstitutionID = oc.ObjectID) end end,
					--BankID = case when s.SecType = 4 then coalesce((select CityID from tFinancialInstitution where FinancialInstitutionID = oc.ObjectID), case when t.TreatyTypeID = 3 then tt.ContractorID end) end,
					--DiasoftDealID = case when s.SecType = 4 then coalesce(ab.ID1, 0) else 0 end,
					SecID2 = s1.SecurityID,
					DepDateEnd = s1.DateEnd,
					RestF = ab.OutcomeBalanceF*t.Factor,
					RestP = case when d.DealTypeID in (788, 865, 19, 789) then ab.OutcomeBalanceF else ab.OutcomeBalanceP end*t.Factor,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from ti t
				 join tAccount a on a.TreatyID = t.TreatyID
				 join tTreaty tt on tt.TreatyID = t.TreatyID
				 join tTreaty tr on tr.TreatyID = t.TreatyID1
				 join tSecurity s on s.SecurityID = a.SecurityID
				 join tAccountBalance ab on ab.AccountID = a.AccountID and ab.BalanceDate = @CloseDate
				 outer apply (select top 1 oc3.UniqueFlag, ocr3.ObjectID from tObjClsRelation ocr3 join tObjClassifier oc3 on oc3.ObjClassifierID = ocr3.ObjClassifierID and oc3.ObjType = 741604640 and oc3.ParentID = 125 where s.SecType = 4 and ocr3.ObjType = 741604640 and ocr3.ObjectID = a.BalanceInstitutionID) oc
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID1 and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2)
 				 left join tObjClsRelation ocr on s.SecType = 4 and ocr.ObjClsRelationID = (select top 1 ObjClsRelationID from tObjClsRelation where ObjType = 741604640 and ObjectID = a.BalanceInstitutionID and ObjClassifierID in (121, 122, 124, 764, 792, 797))
				 left join tDictionariesConnection dc on ab.ID1 > 0 and dc.DictionariesConnectionID = (select top 1 DictionariesConnectionID from tDictionariesConnection where DiasoftBOID = ab.ID1 and Dictionary = case when ocr.ObjClassifierID = 764 then 1104993180 else -461522885 end)
				 left join tDeal d on dc.Dictionary = -461522885 and d.DealID = dc.CompositeID
				 left join tSecurity s1 on dc.DictionariesConnectionID is not null and s1.SecurityID = case when dc.Dictionary = -461522885 then d.SecurityID else dc.CompositeID end
				where (tt.ContractorID <> 9100 or s.SecType <> 4 or not exists(select 1 from tTreatyTreatyType where TreatyID = t.TreatyID and TreatyTypeID = 328)) /* Не брать ДС с ФОРТС */
					and coalesce(oc.UniqueFlag, 0) not in (6, 7)
	
				union all

				select
					t.FinancialInstitutionID FinInstID,
					TreatyID = case when f.TreatyType = 1 then t.TreatyID else tdu.TreatyID end,
					TreatyTypeID = case when f.TreatyType = 1 then 328 end,
					39191,
					39191,
					AccType = 2,
					BankID = case when f.TreatyType = 1 then t.ContractorID end,
					--DiasoftDealID = 0,
					SecID2 = null,
					DepDateEnd = null,
					RestF = q.CurPos*f.coef,
					RestP = q.CurPos*f.coef,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tQUIK_Forts_UK q
				 join tTreaty t on t.NameBrief like q.TradeAccount+'/%' and t.ContractorID = 9100 and t.IsDisabled = 0 and t.FinancialInstitutionID in (select FinInstID from @d)
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (3)
				 join tTreaty tdu on tdu.FinancialInstitutionID = t.FinancialInstitutionID and tdu.IsDisabled = 0
				 join tTreatyTreatyType tttdu on tttdu.TreatyID = tdu.TreatyID and tttdu.TreatyTypeID in (1,2, 340)
				 join (select 1 coef, 1 TreatyType union select -1, 2) f on 1=1
				where @Online = 2 and q.CurPos <> 0

				union all

				select
					t.FinancialInstitutionID,
					case when tn.n = 2 then coalesce(t1.TreatyID, t.TreatyID) else t.TreatyID end, /* Для Открытия берем ДУ договор*/
					TreatyTypeID = case when tn.n = 2 then ttt1.TreatyTypeID else case when s.SecType = 4 then ttt1.TreatyTypeID end end,
					case when tn.n = 1 then coalesce(s.ParentID, s.SecurityID) else pd.FundID end,
					case when tn.n = 1 then pd.SecIssuerID else pd.FundID end,
					AccType = case when tn.n = 2 then 2 else case when s.SecType = 4 then 2 end end,
					BankID = case when tn.n = 2 then t.ContractorID else case when s.SecType = 4 then t.ContractorID end end,
					SecID2 = null,
					DepDateEnd = null,
					RestF = case when pd.DateValue > @CurDate then case when tn.n = 1 then (1-2*pd.Direction)*pd.Quantity else case when s.SecType in (18,24) then 0 else 1 end*round((2*pd.Direction-1)*(pd.Quantity*pd.DealPrice*case when s.SecType = 2 then coalesce((select top 1 Nominal from tAmortization where SecurityID = pd.SecurityID and AmortizationDate <= cast(pd.DateDeal as date) order by AmortizationDate desc), s.Nominal)/100 else 1 end+coalesce(pd.NKD, 0)) - coalesce(pd.CommissionTradeSystem, 0) - coalesce(pd.CommissionBroker, 0), 2) end else 0 end,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = case when pd.DateValue = @CurDate then case when tn.n = 1 then (1-2*pd.Direction)*pd.Quantity else case when s.SecType in (18,24) then 0 else 1 end*round((2*pd.Direction-1)*(pd.Quantity*pd.DealPrice*case when s.SecType = 2 then coalesce((select top 1 Nominal from tAmortization where SecurityID = pd.SecurityID and AmortizationDate <= cast(pd.DateDeal as date) order by AmortizationDate desc), s.Nominal)/100 else 1 end+coalesce(pd.NKD, 0)) - coalesce(pd.CommissionTradeSystem, 0) - coalesce(pd.CommissionBroker, 0), 2) end else 0 end,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tPortfolioDeal pd on pd.TreatyID = t.TreatyID and pd.DateDeal >= @RateDate+1 /*@DealDate*/ and pd.DateDeal < @Date + 1 and pd.IsDeleted = 0 and pd.RawDataProviderID <> 19 /*and pd.IsStockExchange = 1*/
				 join tSecurity s on s.SecurityID = pd.SecIssuerID
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2 and TreatyTypeID <> 328)
				 outer apply (select top 1 t2.TreatyID from tTreaty t2 join tTreatyTreatyType ttt2 on ttt2.TreatyID = t2.TreatyID and ttt2.TreatyTypeID in (1,2, 340) where ttt1.TreatyID is null and t2.FinancialInstitutionID = t.FinancialInstitutionID and t2.IsDisabled = 0) t1
				 join (select 1 n union select 2) tn on 1 = 1
				where @Online = 2 and t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0

				union all

				select
					t.FinancialInstitutionID FinInstID,
					TreatyID = t.TreatyID,
					TreatyTypeID = case when (tn.n = 2) or (tn.n = 1 and s.SecType = 4) then ttt1.TreatyTypeID end,
					case when tn.n = 1 then coalesce(s.ParentID, s.SecurityID) else d.FundID end,
					case when tn.n = 1 then d.SecurityID else d.FundID end,
					AccType = case when tn.n = 2 then 2 end,
					BankID = case when tn.n = 2 then t.ContractorID end,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0.0,
					RestP = case when tn.n = 1 then (1-d.Direction*2)*d.Quantity else (d.Direction*2-1)*d.CouponQuantity end,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (3)
				 join tDeal d on d.LeftSideID = t.TreatyID and d.ValueDate = @CurDate and d.DealTypeID in (870, 27)
				 join tSecurity s on s.SecurityID = d.SecurityID
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2)
				 join (select 1 n union select 2) tn on 1 = 1
				where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0

				union all

				select
					t.FinancialInstitutionID,
					TreatyID = t.TreatyID,
					TreatyTypeID = pd.TreatyTypeID,
					coalesce(s.ParentID, s.SecurityID),
					pd.SecIssuerID,
					AccType = pd.AccType,
					BankID = pd.ContractorID, 
					SecID2 = null,
					DepDateEnd = case when pd.AccType in (3,4) then pd.DateDep end,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = (2*pd.Direction-1)*pd.Quantity*pd.DealPrice,
					ModDealsP =  0, --case when pd.DateValue = @CurDate then (2*pd.Direction-1)*pd.Quantity*pd.DealPrice else 0.0 end, /* Переводы по наличию приходят из Диасофта */
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tPortfolioDeal pd on pd.TreatyID = t.TreatyID and pd.DateDeal >= @RateDate+1 /*@DealDate*/ and pd.DateDeal < @Date + 1 and pd.IsDeleted = 0 and pd.RawDataProviderID = 19 /*and pd.IsStockExchange = 1*/
				 join tSecurity s on s.SecurityID = pd.SecIssuerID
				where @Online = 2 and t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0

				union all

				select
					pd.FinancialInstitutionID,
					pd.TreatyID,
					TreatyTypeID = case when tn.n = 2 then ttt1.TreatyTypeID end,
					SecurityID = case when tn.n = 1 then coalesce(s.ParentID, s.SecurityID) else pd.FundID end,
					SecIssuerID = case when tn.n = 1 then pd.SecIssuerID else pd.FundID end,
					AccType = case when tn.n = 2 then 2 end,
					BankID = case when tn.n = 2 then t.ContractorID end,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = case when tn.n = 1 then (1-2*pd.Direction)*pd.Quantity else case when s.SecType in (18,24) then 0 else 1 end*round((2*pd.Direction-1)*(pd.Quantity*pd.DealPrice*case when s.SecType = 2 then coalesce((select top 1 Nominal from tAmortization where SecurityID = pd.SecurityID and AmortizationDate <= cast(pd.DateDeal as date) order by AmortizationDate desc), s.Nominal)/100 else 1 end+coalesce(pd.NKD, 0)) - coalesce(pd.CommissionTradeSystem, 0) - coalesce(pd.CommissionBroker, 0), 2) end,
					ModDealsP = case when pd.DateValue = @CurDate then case when tn.n = 1 then (1-2*pd.Direction)*pd.Quantity else case when s.SecType in (18,24) then 0 else 1 end*round((2*pd.Direction-1)*(pd.Quantity*pd.DealPrice*case when s.SecType = 2 then coalesce((select top 1 Nominal from tAmortization where SecurityID = pd.SecurityID and AmortizationDate <= cast(pd.DateDeal as date) order by AmortizationDate desc), s.Nominal)/100 else 1 end+coalesce(pd.NKD, 0)) - coalesce(pd.CommissionTradeSystem, 0) - coalesce(pd.CommissionBroker, 0), 2) end else 0 end,
					Orders = 0.0,
					Quik = 0.0
				from tModPortfolioDeal pd
				 join tSecurity s on s.SecurityID = pd.SecurityID
				 left join tTreaty t on t.TreatyID = pd.TreatyID
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = pd.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2)
				 join (select 1 n union select 2) tn on 1 = 1
				where @Online = 2 and pd.DateCreate = @DateCreate_ and pd.IsDeleted = 0 and pd.RawDataProviderID = 5

				union all

				select
					pd.FinancialInstitutionID,
					pd.TreatyID,
					TreatyTypeID = pd.TreatyTypeID,
					pd.SecurityID,
					pd.SecIssuerID,
					AccType = pd.AccType,
					BankID = pd.ContractorID,
					SecID2 = null,
					DepDateEnd = case when pd.AccType in (3,4) then pd.DateDep end,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = (2*pd.Direction-1)*pd.Quantity*pd.DealPrice,
					ModDealsP =  0, --case when pd.DateValue = @CurDate then (2*pd.Direction-1)*pd.Quantity*pd.DealPrice else 0.0 end, /* Переводы по наличию приходят из Диасофта */
					Orders = 0.0,
					Quik = 0.0
				from tModPortfolioDeal pd
				where @Online = 2 and pd.DateCreate = @DateCreate_ and pd.IsDeleted = 0 and pd.RawDataProviderID = 19

				union all

				select
					t.FinancialInstitutionID,
					t.TreatyID,
					TreatyTypeID = case when tn.n = 2 then ttt1.TreatyTypeID end,
					case when tn.n = 1 then coalesce(s.ParentID, s.SecurityID) else 39191 end,
					case when tn.n = 1 then s.SecurityID else 39191 end,
					AccType = case when tn.n = 2 then 2 end,
					BankID = case when tn.n = 2 then t.ContractorID end,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = case when tn.n = 1 then case q.Operation when 'B' then 1 else -1 end*round(case when s.SecType = 2 then q.Rest else cast(q.Capacity / q.OrderPrice + 0.5 as int) end, 2) else case q.Operation when 'B' then -1 else 1 end*round((q.Capacity+q.NKD)*1.002*q.Rest/q.Quantity, 2) end
				from tTreaty t
				 join tQUIK_UK_Order q on ((t.TreatyID = 6440 and q.ClientCode = 'UU') or (q.ClientCode = coalesce(nullif(rtrim(t.DocAttr2Value)+'/', '/')+t.FinancialInstitutionPortal, t.FinancialInstitutionPortal))) and q.OrderDate = @CurDate and q.Operation = case when @Direction = 0 then 'B' else 'S' end and q.State = 'ACTIVE'
				 join tExchangeSecurity es on es.ExchangeSecurityID = (select top 1 es1.ExchangeSecurityID from tExchangeSecurity es1 where es1.RawDataProviderID = 11 and es1.SecurityName = q.SecurityNumber)
				 join tSecurity s on s.SecurityID = es.SecurityID
				 join (select 1 n union select 2) tn on 1 = 1
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2)
				where @Online = 2 and t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0 and t.ContractorID in (9100)

				union all

				select
					t.FinancialInstitutionID,
					t.TreatyID,
					TreatyTypeID = case when tn.n = 2 then ttt1.TreatyTypeID end,
					case when tn.n = 1 then coalesce(s.ParentID, s.SecurityID) else 39191 end,
					case when tn.n = 1 then s.SecurityID else 39191 end,
					AccType = case when tn.n = 2 then 2 end,
					BankID = case when tn.n = 2 then t.ContractorID end,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = case when tn.n = 1 then case q.Operation when 'B' then 1 else -1 end*round(case when s.SecType = 2 then q.Rest else cast(q.Capacity / q.OrderPrice + 0.5 as int) end, 2) else case q.Operation when 'B' then -1 else 1 end*round((q.Capacity+q.NKD)*1.002*q.Rest/q.Quantity, 2) end
				from tTreaty t
				 join tQUIK_UK_BFA_Order q on q.ClientCode = case when t.DocAttr2Value != '' then t.DocAttr2Value+'/' else '' end+t.FinancialInstitutionPortal and q.OrderDate = @CurDate and q.Operation = case when @Direction = 0 then 'B' else 'S' end and q.State = 'ACTIVE'
				 join tExchangeSecurity es on es.ExchangeSecurityID = (select top 1 es1.ExchangeSecurityID from tExchangeSecurity es1 where es1.RawDataProviderID = 11 and es1.SecurityName = q.SecurityNumber)
				 join tSecurity s on s.SecurityID = es.SecurityID
				 join (select 1 n union select 2) tn on 1 = 1
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2)
				where @Online = 2 and t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0 and t.ContractorID in (9200)

				union all

				select
					t.FinancialInstitutionID,
					t.TreatyID,
					TreatyTypeID = case when tn.n = 2 then ttt1.TreatyTypeID end,
					case when tn.n = 1 then coalesce(s.ParentID, s.SecurityID) else 39191 end,
					case when tn.n = 1 then s.SecurityID else 39191 end,
					AccType = case when tn.n = 2 then 2 end,
					BankID = case when tn.n = 2 then t.ContractorID end,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = case when tn.n = 1 then case q.Operation when 'B' then 1 else -1 end*round(case when s.SecType = 2 then q.Rest else cast(q.Capacity / q.OrderPrice + 0.5 as int) end, 2) else case q.Operation when 'B' then -1 else 1 end*round((q.Capacity+q.NKD)*1.002*q.Rest/q.Quantity, 2) end
				from tTreaty t
				 join tQUIK_Otkr_Order q on q.ClientCode = t.FinancialInstitutionPortal and q.OrderDate = @CurDate and q.Operation = case when @Direction = 0 then 'B' else 'S' end and q.State = 'ACTIVE'
				 join tExchangeSecurity es on es.ExchangeSecurityID = (select top 1 es1.ExchangeSecurityID from tExchangeSecurity es1 where es1.RawDataProviderID = 11 and es1.SecurityName = q.SecurityNumber)
				 join tSecurity s on s.SecurityID = es.SecurityID
				 join (select 1 n union select 2) tn on 1 = 1
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2)
				where @Online = 2 and t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0 and t.ContractorID in (3996)

				union all

				select
					t.FinancialInstitutionID FinInstID,
					t.TreatyID,
					TreatyTypeID = null,
					d.FundID,
					d.FundID,
					--AccType = case when tn.n = 1 then 3 else 1 end,
          AccType = case when tn.n = 1 then d.AccType else case when d.AccType = 4 then 1 else 1 /*3*/ end end,
					BankID = case when tn.n = 1 then d.RightSideBrokerID else r.CityID end,
					SecID2 = case when tn.n = 1 then d.SecurityID end,
					DepDateEnd = case when tn.n = 1 then s.DateEnd end,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = (1-2*d.Direction)*case when tn.n = 1 then 1 else -1 end*abt.TurnValue,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (1,2,340)
				 join tDeal d on d.LeftSideID = t.TreatyID and d.DealDate <= @CurDate and d.ValueDate = @CurDate and d.DealTypeID = 5
				 join tSecurity s on s.SecurityID = d.SecurityID
				 join tAccountBalanceTurn abt on abt.DealID = d.DealID and abt.AccountBalanceTurnTypeID = 7 and abt.TurnDate = @CurDate
				 left join tFinancialInstitution r on r.NameBrief = d.ResBrief
				 join (select 1 n union select 2) tn on 1 = 1
				where @Online = 2 and t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0
				
				union all
									
				select
					d.FinancialInstitutionID,
					d.TreatyID,
					d.TreatyTypeID,
					d.FundID,
					d.FundID,
					AccType = case when f.coef = 1 then d.AccType else case when d.AccType = 4 then 1 else 3 end end,
					BankID = coalesce(d.RightSideBrokerID, 0),
					--DiasoftDealID = 0,
					SecID2 = case when f.coef = 1 then d.SecurityID end,
					DepDateEnd = case when f.coef = 1 then s.DateEnd end,
					RestF = 0.0,
					RestP = 0.0,
					DiaDeals = f.coef*d.Rest,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from (
					select
						t.TreatyID,
						t.FinancialInstitutionID,
						ttt1.TreatyTypeID,
						d.RightSideBrokerID,
						d.FundID,
						d.AccType,
						d.SecurityID,
						Rest = sum(case when abt.AccountBalanceTurnTypeID = 3 then 1 when abt.AccountBalanceTurnTypeID = 7 and abt.TurnDate <= @CloseDate then -1 else 0 end*abt.TurnValue)
					from tTreaty t
					 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2 and TreatyTypeID <> 371)
					 join tDeal d on d.LeftSideID = t.TreatyID and d.DealDate <= @CloseDate and d.ValueDate >= @CloseDate and d.DealTypeID = 5 --and d.AccType = 4
					 join tAccountBalanceTurn abt on abt.DealID = d.DealID
					where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0
					group by t.TreatyID, t.FinancialInstitutionID, ttt1.TreatyTypeID, d.RightSideBrokerID, d.FundID, d.AccType, d.SecurityID
				) d
				 join tSecurity s on s.SecurityID = d.SecurityID
				 join (select -1 coef union select 1) f on 1=1

				union all

				select
					t.FinancialInstitutionID FinInstID,
					t.TreatyID,
					TreatyTypeID = null,
					pd.FundID,
					pd.FundID,
					AccType = coalesce(oc.UniqueFlag, 1),
					BankID = r.CityID,
					--DiasoftDealID = 0,
					SecID2 = null,
					DepDateEnd = null,
					RestF = (2*pd.Direction-1)*pd.CouponQuantity,
					RestP = case when pd.ValueDate = @CurDate then /*case when pd.DealTypeID in (10 /* Ввод ДС */, 12 /* ДопДоход */, 18 /* ДивНач */, 324 /* НДФЛ_зачис */) then*/ (2*pd.Direction-1)*pd.CouponQuantity else 0 end,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (1,2,340)
				 join tDeal pd on pd.LeftSideID = t.TreatyID and pd.DealDate = @CurDate and pd.DealTypeID in (10 /* Ввод ДС */, 12 /* ДопДоход */, 18 /* ДивНач */, 324 /* НДФЛ_зачис */, 789 /* Амортизац */, 788 /* КупПог */, 19 /* ПогОбл */, 15/* НДФЛ_спис */)
				 left join tFinancialInstitution r on r.NameBrief = pd.ResBrief
				 left join tObjClsRelation ocr on ocr.ObjectID = r.FinancialInstitutionID and ocr.ObjType = 741604640
				 left join tObjClassifier oc on oc.ObjClassifierID = ocr.ObjClassifierID and oc.ObjType = 741604640 and oc.ParentID = 125
				where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0 and coalesce(oc.UniqueFlag, 1) <> 2

				union all

				select
					t.FinancialInstitutionID FinInstID,
					t.TreatyID,
					TreatyTypeID = ttt1.TreatyTypeID,
					case when pd.DealTypeID in (312) then pd.SecurityID else pd.FundID end,
					case when pd.DealTypeID in (312) then pd.SecurityID else pd.FundID end,
					AccType = 2,
					BankID = t.ContractorID,
					--DiasoftDealID = 0,
					SecID2 = null,
					DepDateEnd = null,
					RestF = case when pd.DealTypeID = 312 then (1-pd.Direction*2)*pd.Quantity else (2*pd.Direction-1)*pd.CouponQuantity end,
					RestP = /*case when pd.DealTypeID in (10 /* Ввод ДС */, 12 /* ДопДоход */, 18 /* ДивНач */, 324 /* НДФЛ_зачис */) then*/ case when pd.DealTypeID = 312 then (1-pd.Direction*2)*pd.Quantity else (2*pd.Direction-1)*pd.CouponQuantity end /*else 0 end*/,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (3)
				 join tDeal pd on pd.LeftSideID = t.TreatyID and pd.DealDate = @CurDate and pd.DealTypeID in (10 /* Ввод ДС */, 12 /* ДопДоход */, 18 /* ДивНач */, 324 /* НДФЛ_зачис */, 789 /* Амортизац */, 788 /* КупПог */, 19 /* ПогОбл */, 865 /* КупПогБр */, 312 /* Ввод/вывЦБ */)
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2 and TreatyTypeID <> 371)
				where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0

				union all

				select /* меняем по наличию по факту ду договор*/
					t.FinancialInstitutionID FinInstID,
					t.TreatyID,
					TreatyTypeID = null,
					pd.FundID,
					pd.FundID,
					AccType = coalesce(oc.UniqueFlag, 1),
					BankID = r.CityID,
					--DiasoftDealID = 0,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0,
					RestP = (2*pd.Direction-1)*abt.TurnValue,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (1,2,340)
				 join tDeal pd on pd.LeftSideID = t.TreatyID and pd.DealTypeID in (/*789*/ /* Амортизац *//*, 788*/ /* КупПог *//*, 19 *//* ПогОбл *//*,*/ 111 /* 1 ДогДлВнш */, 772 /* Перевод ДС */)
				 join tAccountBalanceTurn abt on abt.DealID = pd.DealID and abt.AccountBalanceTurnTypeID = 7 and abt.TurnDate = @CurDate
				 left join tFinancialInstitution r on r.NameBrief = pd.ResBrief
				 left join tObjClsRelation ocr on ocr.ObjectID = r.FinancialInstitutionID and ocr.ObjType = 741604640
				 left join tObjClassifier oc on oc.ObjClassifierID = ocr.ObjClassifierID and oc.ObjType = 741604640 and oc.ParentID = 125
				where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0 and coalesce(oc.UniqueFlag, 1) <> 2
					
				union all

				select /* меняем по наличию по факту брок договор*/
					t.FinancialInstitutionID FinInstID,
					t.TreatyID,
					TreatyTypeID = ttt1.TreatyTypeID,
					pd.FundID,
					pd.FundID,
					UniqueFlag = 2,
					BankID = t.ContractorID,
					SecID2 = null,
					DepDateEnd = null,
					RestF = 0,
					RestP = (2*pd.Direction-1)*abt.TurnValue,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (3)
				 join tDeal pd on pd.LeftSideID = t.TreatyID and pd.DealTypeID in (/*789*/ /* Амортизац *//*, 788*/ /* КупПог *//*, 19*/ /* ПогОбл *//*,*/ 111 /* 1 ДогДлВнш */, 772 /* Перевод ДС */)
				 join tAccountBalanceTurn abt on abt.DealID = pd.DealID and abt.AccountBalanceTurnTypeID = 7 and abt.TurnDate = @CurDate
				 left join tTreatyTreatyType ttt1 on ttt1.TreatyID = t.TreatyID and ttt1.TreatyTypeID in (select TreatyTypeID from tTreatyType where TreatyTypeGroupID = 2 and TreatyTypeID <> 371)
				where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0

				union all

				select
					t.FinancialInstitutionID FinInstID,
					t.TreatyID,
					TreatyTypeID = null,
					d.FundID,
					d.FundID,
					AccType = case when tn.n = 1 then d.AccType else coalesce(oc.UniqueFlag, 1) end,
					BankID = case when tn.n = 1 then d.RightSideBrokerID else r.CityID end,
					SecID2 = case when tn.n = 1 then d.SecurityID end,
					DepDateEnd = case when tn.n = 1 then s.DateEnd end,
					RestF = (1-2*d.Direction)*case when tn.n = 1 then 1*dq.DepQty else -1*d.CouponQuantity end,
					RestP = (1-2*d.Direction)*case when tn.n = 1 then 0 else -1*d.CouponQuantity end,
					DiaDeals = 0.0,
					MoDeals = 0.0,
					ModDealsF = 0.0,
					ModDealsP = 0.0,
					Orders = 0.0,
					Quik = 0.0
				from tTreaty t
				 join tTreatyTreatyType ttt on ttt.TreatyID = t.TreatyID and ttt.TreatyTypeID in (1,2,340)
				 join tDeal d on d.LeftSideID = t.TreatyID and d.DealTypeID = 844 and d.DealDate = @CurDate
				 left join tFinancialInstitution r on r.NameBrief = d.ResBrief
				 left join tObjClsRelation ocr on ocr.ObjectID = r.FinancialInstitutionID and ocr.ObjType = 741604640
				 left join tObjClassifier oc on oc.ObjClassifierID = ocr.ObjClassifierID and oc.ObjType = 741604640 and oc.ParentID = 125
				 outer apply (
					select sum(ab.OutcomeBalanceF) DepQty
					from tDictionariesConnection dc
					 join tAccount a on a.TreatyID = d.LeftSideID and a.SecurityID = d.FundID
					 join tAccountBalance ab on ab.AccountID = a.AccountID and ab.BalanceDate = @CloseDate and ab.ID1 = dc.DiasoftBOID
					where dc.Dictionary = -461522885 and dc.CompositeID = d.DealID
				) dq
				 join tSecurity s on s.SecurityID = d.SecurityID
				 join (select 1 n union select 2) tn on 1 = 1
				where t.FinancialInstitutionID in (select FinInstID from @d) and t.IsDisabled = 0

			) t2
			where coalesce(t2.AccType, -1) not in (6, 7)
			group by t2.FinancialInstitutionID, t2.TreatyID, t2.TreatyTypeID, t2.SecurityID, t2.SecIssuerID, t2.SecID2, t2.DepDateEnd, t2.AccType, case when t2.AccType in (1,2,3,4) then t2.BankID end
		) t1
		 join tSecurity s on s.SecurityID = t1.SecurityID
		 join tSecurity si on si.SecurityID = t1.SecIssuerID
		 left join tSecurity sb on sb.SecurityID = s.BaseSecurityID and s.SecType in (18,24)
		 left join tSecurity sb1 on sb1.SecurityID = sb.BaseSecurityID and sb1.SecurityID <> 70802
		 --left join (select distinct FinInstID1 FinInstID, SecurityID, DealPrice from @d) mpd on mpd.FinInstID = t1.FinancialInstitutionID and mpd.SecurityID = t1.SecurityID
		 outer apply (select top 1 AverageWeightedPrice, Duration, CourseLast from tQUIK_Rate where @Online = 2 and SecurityID = t1.SecurityID and DateCourse = @CurDate order by AverageWeightedPrice desc) qr
		 left join tObjClsRelation ocr1 on ocr1.ObjClassifierID in (129, 130, 139) and ocr1.ObjectID = (select top 1 TreatyID from tTreatyTreatyType where TreatyTypeID in (1,2, 340) and TreatyID in (select TreatyID from tTreaty where FinancialInstitutionID = t1.FinancialInstitutionID and IsDisabled = 0)) and ocr1.ObjType = 1631275800
		 left join tSecurityRate r on r.SecurityRateID = (select top 1 SecurityRateID from tSecurityRate where Date = @RateDate and SecurityID = case when s.SecType = 5 or ocr1.ObjClassifierID = 139 then t1.SecIssuerID else t1.SecurityID end and RateType = case when ocr1.ObjClassifierID = 129 then 1 when ocr1.ObjClassifierID = 130 then 2 when ocr1.ObjClassifierID = 139 then 3 else 0 end and FundID = 39191 and FinInstID = case when ocr1.ObjClassifierID = 139 then t1.FinancialInstitutionID else 0 end)
		 outer apply (select top 1 CourseCurrent from tRate where ActualizationDateTime = @Date and SecurityID = t1.SecIssuerID and TradeSystemID = 1 and RawDataProviderID = 1) cv
     outer apply (select top 1 AverageWeightedPrice from tQUIK_Rate where @Online = 2 and coalesce(sb1.SecurityID, sb.SecurityID) is not null and SecurityID = coalesce(sb1.SecurityID, sb.SecurityID) and DateCourse = @CurDate and AverageWeightedPrice > 0 order by QuantityDeals desc) qrb
     outer apply (select top 1 PriceCourse, Price from tSecurityRate where coalesce(sb1.SecurityID, sb.SecurityID) is not null and Date = @RateDate and SecurityID = coalesce(sb1.SecurityID, sb.SecurityID) and RateType = 0 and FundID = 39191) rb
		 left join tSecurity s2 on s2.SecurityID = t1.SecID2
     outer apply (select top 1 CourseCurrent from tRate where SecurityID = coalesce(t1.SecID2, t1.SecurityID) and TradeSystemID = 16218 and ActualizationDateTime < @CurDate+1 order by ActualizationDateTime desc) rt
		where t1.RestFA <> 0 or t1.RestPA <> 0
	) t
	 left join tObjClsRelation ocr2 on ocr2.ObjClassifierID = 30814 and ocr2.ObjectID = t.SecurityID

print 'Остатки конец'
print convert(varchar, getdate(), 113)

	if exists(
		select null
		from tModPortfolioDeal md
		 left join tTreaty t on t.TreatyID = md.TreatyID
		where md.DateCreate = @DateCreate_
			and md.IsDeleted = 0
			and md.Direction = 0
			and (md.FinancialInstitutionID = 9039 or t.FinancialInstitutionID = 9039)
			and md.RawDataProviderID = 5
	)		
	begin
		declare @de1 smalldatetime = @RateDate
		declare @db1 smalldatetime = dateadd(m, -3, @RateDate)+1
		declare @d2 int = datediff(day, @db1, @de1)+1
		if exists(
			select null
			from (
				select c, [1], [2]
				from (
					select a, c, p = cast(s as float)/nullif(sum(s) over (partition by a), 0)*100
					from (
						select
							a.a,
							c = case a.a when 1 then cF else cFP end,
							s = sum(case a.a when 1 then RestF else RestFP end*PriceN)
						from (
							select
								t.SecurityID,
								t.PriceN,
								t.RestF,
								t.RestFP,
								cF  = case when t.ynormF = 100 then 1 when t.ynormF >= 80 then 2 when t.ynormF >= 40 then 3 else 4 end,
								cFP = case when t.ynormFP = 100 then 1 when t.ynormFP >= 80 then 2 when t.ynormFP >= 40 then 3 else 4 end
							from (
								select
									b.SecurityID,
									b.RestF,
									b.RestFP,
									b.PriceN,
									ynormF  = case when ocr.Value is not null then ocr.Value/100 when s.SecType = 4 or b.RestF < 0 then 1 when s.SecType = 2 and coalesce(ofr.RedemptionDate, s.DateEnd) < @de1+14 then 1 when coalesce(t1.Volume*14/@d2, 0)/nullif(b.RestF*b.PriceN,0) > 1 then 1 else coalesce(t1.Volume, 0)*14/@d2/nullif(b.RestF*b.PriceN,0) end*100,
									ynormFP = case when ocr.Value is not null then ocr.Value/100 when s.SecType = 4 or b.RestFP < 0 then 1 when s.SecType = 2 and coalesce(ofr.RedemptionDate, s.DateEnd) < @de1+14 then 1 when coalesce(t1.Volume*14/@d2, 0)/nullif(b.RestFP*b.PriceN,0) > 1 then 1 else coalesce(t1.Volume, 0)*14/@d2/nullif(b.RestFP*b.PriceN,0) end*100
								from @b b
								 join tSecurity s on s.SecurityID = b.SecurityID
								 outer apply (select top 1 RedemptionDate from tRedemption rd where s.SecType = 2 and s.RatedSecurityID = 39191 and rd.SecurityID = b.SecurityID and rd.RedemptionDate >= @de1 order by RedemptionDate) ofr
								 left join tObjClsRelation ocr on ocr.ObjClassifierID = 49297 /*Ликвидность активов*/ and ocr.ObjType = 1104993180 and ocr.ObjectID = s.SecurityID
								 outer apply (
									select Volume = sum(Volume)
									from (
										select Volume = er.Volume*dbo.uf_GetFundRate(coalesce(es.FundID, 39191), er.RateDate, 39191)
										from tExchangeSecurity es
										 join tExchangeRate er on er.ExchangeSecurityID = es.ExchangeSecurityID and er.RateDate between @db1 and @de1 and er.Volume > 0
										where es.SecurityID = b.SecurityID and es.RawDataProviderID in (11, 25)

										union all
							
										select Volume = r.TradeVolume*r.CourseCurrent*rv.CourseCurrent
										from tRate r
										 cross apply (select top 1 * from tRate where SecurityID = 39192 and TradeSystemID = 1 and ActualizationDateTime <= r.ActualizationDateTime order by ActualizationDateTime desc) rv
										where r.TradeSystemID = 36 and r.ActualizationDateTime between case when s.PrimeDispositionDateEnd+7 > @db1 then s.PrimeDispositionDateEnd+7 else @db1 end and @de1 and r.TradeVolume > 0
											and r.SecurityID = coalesce(s.ParentID, s.SecurityID)
									) t
								) t1
								where b.FinInstID = 9039
							) t
						) t
						 cross join (select 1 a union select 2) a
						group by a.a, case a.a when 1 then cF else cFP end
					) t
				) t
				pivot (sum(p) for a in ([1], [2])) pvt
			) t
			where (c = 2 and [1] > 50 and [1] > [2]) or (c = 3 and [1] > 30 and [1] > [2]) or (c = 4 and [1] > 10 and [1] > [2])
		)
		begin
			raiserror('Нарушение рисков ликвидности', 16,1)
			return 0
		end
	end

print 'Риски ликвидности конец'
print convert(varchar, getdate(), 113)

	if 2=1
	begin
		declare @msg varchar(200) = null
		select @msg = 'Макс.кол-во покупки = (Объем сделок за месяц ('+cast(coalesce(v.VolumeNum, 0) as varchar)+') - Объем сделок с УК ('+cast(cast(coalesce(u.Num1, 0) as bigint) as varchar)+')) * 0.7 - Остаток ('+cast(cast(coalesce(o.Rest, 0) as bigint) as varchar)+') = '+cast(floor(0.7*(coalesce(v.VolumeNum, 0)- coalesce(u.Num1, 0))-coalesce(o.Rest, 0)) as varchar)
		from (
			select
				pd.SecurityID,
				DateDeal = cast(cast(pd.DateDeal as date) as smalldatetime),
				pd.Quantity
			from tModPortfolioDeal pd
			 join tSecurity s on s.SecurityID = pd.SecurityID and s.SecType in (0,2,15)
			where pd.FinancialInstitutionID = 24112 and pd.DateCreate = @DateCreate_ and pd.IsDeleted = 0 and Direction = 0
				and pd.SecurityID not in (select ObjectID from tObjClsRelation where ObjType = 1104993180 and ObjClassifierID = 18560)
		) t
		 cross apply (select sum(RestF) Rest from @b where FinInstID = 24112 and SecurityID = t.SecurityID) o
		 outer apply (
			select VolumeNum = sum(er.VolumeNum)
			from tExchangeSecurity es
			 join tExchangeRate er on er.ExchangeSecurityID = es.ExchangeSecurityID and er.RateDate between t.DateDeal - 30 and t.DateDeal
			where es.RawDataProviderID = 11
				and es.SecurityID = t.SecurityID		
		) v
		 outer apply (
			select Num1 = sum(d1.Quantity)
			from tDeal d1
			where d1.LeftSideID in (select tr.TreatyID from tTreaty tr join tTreatyTreatyType ttt on ttt.TreatyID = tr.TreatyID and ttt.TreatyTypeID = 340 where tr.FinancialInstitutionID = 24112)
				and d1.SecurityID = t.SecurityID
				and d1.DealTypeID in (782, 27, 870)
				and d1.DealDate between t.DateDeal - 30 and t.DateDeal
				and exists(select null from tDeal d2 join tTreaty tr on tr.TreatyID = d2.LeftSideID and tr.ContractorID = 1790 where d2.DealDate = d1.DealDate and d2.NameBrief = d1.NameBrief and d2.DealTypeID = d1.DealTypeID and d2.Direction = 1-d1.Direction)
		) u
		where coalesce(o.Rest, 0)+coalesce(t.Quantity, 0) > 0.7*(coalesce(v.VolumeNum, 0)- coalesce(u.Num1, 0))
		if @msg is not null
		begin
			raiserror(@msg, 16,1)
			return 0
		end

	end

print 'Заполнение таблицы r'
print convert(varchar, getdate(), 113)
	--declare @db smalldatetime = (select min(WorkDate) from (select top 45 WorkDate from tWorkDate where WorkDate <= @RateDate order by WorkDate desc) t)
	
	--if exists(
	--	select 1
	--	from @b b
	--	 join tSecurity s on s.SecurityID = b.SecurityID
	--	 left join taOptionCoef opt on s.SecType = 24 and opt.OptionName = s.NameBrief and opt.OptDate = @RateDate
	--	 outer apply (
	--		select Qty = sum(case when s.SecType = 24 then case when s.PayType = 1 then 1-p.Delta else p.Delta end else 1 end* n.NNum1*p.PriceN*case when n.NNum2 > 1.2 then 1.2 else n.NNum2 end)
	--		from
	--		(
	--			select max(NDate1) NDate1, NID2
	--			from taNab
	--			where NConcept = 2012 and NID1 = b.FinInstID and NID3 = b.SecurityID and NDate1 <= @Date
	--			group by NID2
	--		) n1
	--		 join taNab n on n.NID2 = n1.NID2 and n.NDate1 = n1.NDate1 and n.NConcept = 2012 and n.NID1 = b.FinInstID and n.NID3 = b.SecurityID
	--		 join tSecurity s on s.SecurityID = n1.NID2
	--		 outer apply (
	--			select SecBaseID, opt.Delta, PriceN, Nominal from @b b1 left join taOptionCoef opt on b1.SecType = 24 and opt.OptionName = s.NameBrief and opt.OptDate = @RateDate	where FinInstID = b.FinInstID and SecurityID = n.NID2) p
	--		 outer apply dbo.uf_avgGetCorrelBeta(@db, @RateDate, p.SecBaseID, b.SecBaseID) c
	--	) cv
	--	where b.SecType in (18, 24)
	--		and b.RestF*b.PriceN < 0
	--		and -case when b.SecType = 24 then coalesce(case when s.PayType = 1 then 1-opt.Delta else opt.Delta end, 1) else 1 end*b.PriceN*b.RestF-coalesce(cv.Qty, 0) > 0
	--)	
	--begin
	--	raiserror('Есть непривязанная короткая позиция', 16,1)
	--	return 0
	--end	

--------------------------------------------------------------

	create table #r (
		InvestDeclID int,
		InvestDeclLinkID int,
		InvestDeclWhereID int,
		FinInstID int,
		TreatyID int,
		TreatyTypeID int,
		BankID int,
		Flag_Group int,
		Flag_Div int,
		SecurityID int,
		SecurityID1 int,
		AccType int,
		SecType tinyint,
		IssuerID int,
		IssuerID1 int,
		RestF decimal(20,8),
		RestP decimal(20,8),
		RestFP decimal(20,8),
		RestPP decimal(20,8),
		PriceN float,
		PriceD float,
		Coupon float,
		Duration float
	)
	
	insert #r (InvestDeclID, InvestDeclLinkID, InvestDeclWhereID, FinInstID, TreatyID, TreatyTypeID, BankID, Flag_Group, Flag_Div, SecurityID, SecurityID1, AccType, SecType, IssuerID, IssuerID1, RestF, RestP, RestFP, RestPP, PriceN, PriceD, Coupon, Duration)
	select
		d.InvestDeclID,
		d.InvestDeclLinkID,
		InvestDeclWhereID = coalesce(idw1.InvestmentDeclarationWhereID, -1),
		b.FinInstID,
		b.TreatyID,
		b.TreatyTypeID,
		b.BankID,
		idw1.FLAG_Group,
		fd.Flag_Div,
		b.SecurityID,
		b.SecurityID1,
		b.AccType,
		b.SecType,
		b.IssuerID,
		b.IssuerID1,
		b.RestF,
		b.RestP,
		b.RestFP,
		b.RestPP,
		b.PriceN,
		b.PriceD,
		b.Coupon,
		b.Duration
		--case when idw1.FLAG_Group in (26,27) then (select Duration from dbo.uf_avgGetYieldDuration(@CurDate, b.SecurityID, b.PriceN*100/coalesce((select top 1 Nominal from tAmortization where SecurityID = b.SecurityID and AmortizationDate <= @CurDate order by AmortizationDate desc), si.Nominal), dbo.uf_avgGetCoupon(b.SecurityID, @CurDate, si.RatedSecurityID), case when si.RatedSecurityID = 39191 then 1 else 0 end)) end
		--case when idw1.FLAG_Group in (26,27) then dbo.uf_avgGetDuration(@CurDate, b.SecurityID, b.PriceN*100/coalesce((select top 1 Nominal from tAmortization where SecurityID = b.SecurityID and AmortizationDate <= @CurDate order by AmortizationDate desc), si.Nominal), b.Coupon) end,
		--
	from @b b
	 join (select 0 Flag_Div union select 1) fd on 1=1
	 join (select distinct InvestDeclID, InvestDeclLinkID, FinInstID from @d) d on d.FinInstID = b.FinInstID
	 join tSecurity si on si.SecurityID = b.SecurityID
	 left join sInvestmentDeclarationWhere idw1 on idw1.InvestmentDeclarationID = d.InvestDeclID and idw1.Enb = 'T' and (idw1.Flag_Group <> 1 or coalesce(PeriodDay, 0) = 0)
		and (
			(idw1.Flag_Group in (/*7,*/ 10) and (b.AccType = 3 or fd.Flag_Div = 1 or (idw1.Flag_Group = 10 and b.IssuerID1 in (select IssuerID1 from @b where FinInstID = b.FinInstID and AccType = 3))))
			or (idw1.Flag_Group in (20) and (b.AccType in (1,3) or fd.Flag_Div = 1))
			or (idw1.FLAG_Group in (26,27) and si.SecType = 2 and (idw1.FLAG_Calculation = 1 or fd.Flag_Div = 0))
			or (idw1.Flag_Group = 8 and (b.AccType = 1 or fd.Flag_Div = 1))
			or (idw1.Flag_Group = 28 and fd.Flag_Div = 1 and (b.AccType = 3 or si.SecType = 2))
			or (idw1.Flag_Group in (12) and (b.AccType = 3 or fd.Flag_Div = 1))
			or (idw1.Flag_Group in (13) and (b.AccType = 1 or fd.Flag_Div = 1))
			or
			(
				(idw1.Flag_Group not in (7, 8, 10, 12, 13, 14, 15, 16, 19, 20, 21, 22, 24, 25, 28, 29, 32, 33)
					or (idw1.Flag_Group in (14,15,7) and (b.AccType = 3 or fd.Flag_Div = 1))
				  or (idw1.Flag_Group in (21) and (b.SecType = 4 or fd.Flag_Div = 1)) 
					or (idw1.Flag_Group in (22) and (b.AccType = 1 or fd.Flag_Div = 1))
					or (idw1.Flag_Group in (16,19) and si.SecType = 2 and fd.Flag_Div = 0)
					or (idw1.Flag_Group in (24,25) and (b.AccType = 3 or si.SecType = 2 or fd.Flag_Div = 1))
					or (idw1.Flag_Group = 28 and fd.Flag_Div = 0 and (b.AccType = 3 or si.SecType = 2))
					or (idw1.Flag_Group in (32,33) and (b.AccType in (1,3, 4) or si.SecType <> 4 or fd.Flag_Div = 1)) 
					or (idw1.Flag_Group = 29 and si.SecType = 2)
				)
				and (fd.Flag_Div = 0 or idw1.FLAG_Calculation = 1)
				and (idw1.FLAG_Long = 0 or fd.Flag_Div = 1 or idw1.FLAG_Long = sign(case when si.SecType = 24 and si.PayType = 1 then -1 else 1 end*b.RestF) )
				and (idw1.Flag_Group in (15, 7) or
				exists (
					select 1 from sInvestmentDeclarationSecurity ids
					 left join tSecuritySecurityGroup ssg on ssg.SecurityID = si.SecurityID and ssg.SecurityGroupID = ids.ObjID and ssg.Enb = 'T' and ssg.Online in (0,@Online)
					where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 1 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 2 and ((ids.ObjID = 150) or ssg.SecurityGroupID is not null or (ids.ObjID = 823 and fd.Flag_Div = 0 and b.AccType = 2) or (ids.ObjID = 834 and fd.Flag_Div = 0 and b.AccType = 1) or (ids.ObjID = 847 and fd.Flag_Div = 0 and b.AccType = 3) or (ids.ObjID = 848 and fd.Flag_Div = 0 and b.AccType = 4) or (ids.ObjID = 897 and fd.Flag_Div = 0 and b.AccType in (3,4) and b.DateEnd <= dateadd(m, 3, @Date)))
					union all
					select 1 from sInvestmentDeclarationSecurity ids
					where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 1 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 1 and ids.ObjID = si.SecurityID
					union all
					select 1 from sInvestmentDeclarationSecurity ids
					where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 1 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 3 and ids.ObjID = b.IssuerID
					union all
					select 1 from sInvestmentDeclarationSecurity ids
					 join tObjClsRelation ocr on ocr.ObjClassifierID = ids.ObjID and ocr.ObjectID = b.IssuerID1 and ocr.ObjType = 741604640
					where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 1 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 5
				)
			)
			and not exists (
				select 1 from sInvestmentDeclarationSecurity ids
				 left join tSecuritySecurityGroup ssg on ssg.SecurityID = si.SecurityID and ssg.SecurityGroupID = ids.ObjID and ssg.Enb = 'T' and ssg.Online in (0,@Online)
				where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 0 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 2 and (ssg.SecurityGroupID is not null or (ids.ObjID = 823 and fd.Flag_Div = 0 and b.AccType = 2) or (ids.ObjID = 834 and fd.Flag_Div = 0 and b.AccType = 1) or (ids.ObjID = 847 and fd.Flag_Div = 0 and b.AccType = 3) or (ids.ObjID = 848 and fd.Flag_Div = 0 and b.AccType = 4) or (ids.ObjID = 897 and fd.Flag_Div = 0 and b.AccType in (3,4) and b.DateEnd <= dateadd(m, 3, @Date)))
				union all
				select 1 from sInvestmentDeclarationSecurity ids
				where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 0 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 1 and ids.ObjID = si.SecurityID
				union all
				select 1 from sInvestmentDeclarationSecurity ids
				where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 0 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 3 and ids.ObjID = b.IssuerID
				union all
				select 1 from sInvestmentDeclarationSecurity ids
				 join tObjClsRelation ocr on ocr.ObjClassifierID = ids.ObjID and ocr.ObjectID = b.IssuerID1 and ocr.ObjType = 741604640
				where ids.InvestmentDeclarationWhereID = idw1.InvestmentDeclarationWhereID and ids.FLAG_Div = fd.Flag_Div and ids.Flag_Not = 0 and ids.FLAG_CheckBad = 'T' and ids.ObjType = 5
			)
		)
	)

	create clustered index IDX_r ON #r (InvestDeclWhereID, Flag_Div, InvestDeclLinkID);
	
  delete #r
	from #r r
	where r.Flag_Group = 2 and r.Flag_Div = 0
		and exists(select 1 from #r r1 where r1.Flag_Group = 5 and r1.Flag_Div = 0 and r1.SecurityID = r.SecurityID)

print 'Заполнение таблицы r конец'
print 'Заполнение таблицы CheckDecl'
print convert(varchar, getdate(), 113)

  if @debug = 2
    select * from #r where InvestDeclWhereID = 7746

	set @ID = newid()
		
	insert tCheckClnDeclLog	(ID, InvestDeclLinkID, InvestmentDeclarationID, IDName, InvestmentDeclarationWhereID, SecurityID, StartValue, StopValue, fiName, numeratorF, denominatorF, partF, numeratorP, denominatorP, partP, FinInstID, NameWhere, Flag_Group, Name, GroupName, ErrorF, ErrorP, SecName, DateEnd, RestP, RestF, Price, QtyP, QtyF, coefP, coefF, ModPortfolioDealOrderID, TransID, [Version])
	select
		@ID,
		t3.InvestDeclLinkID,
		id.InvestmentDeclarationID,
		id.Name IDName,
		idw.InvestmentDeclarationWhereID,
		t6.SecurityID1,
		idw.StartValue,
		idw.StopValue,
		fi.Name fiName,
		t3.numeratorF,
		t3.denominatorF,
		t3.partF,
		t3.numeratorP,
		t3.denominatorP,
		t3.partP,
		t3.FinInstID,
		idw.NameWhere,
		idw.Flag_Group,
		coalesce(idg.Name, ''),
		coalesce(idg.Name, '') GroupName,
		ErrorF = cast(case when t3.partF not between idw.StartValue and idw.StopValue then 1 else 0 end as bit),
		ErrorP = cast(case when t3.partP not between idw.StartValue and idw.StopValue then 1 else 0 end as bit),
		s.Name1+case s.SecType when 4 then '(' + case t6.AccType when 1 then 'Р/С' when 2 then 'БИРЖА' when 3 then 'ДЕПОЗИТ'+coalesce(' '+(select rtrim(fic.NameBrief) from tFinancialInstitution fic where fic.FinancialInstitutionID = t6.BankID)/*ocr.Comment*/, '') when 4 then 'НСО' else 'ПРОЧЕЕ' end + ')' else '' end SecName,
		DateEnd = case when s.SecType = 2 and s.DateEnd < @CurDate then ' ('+convert(varchar, s.DateEnd, 4)+')' end,
		t6.RestP,
		t6.RestF,
		t6.PriceN,
		t6.QtyP,
		t6.QtyF,
		t6.coefP,
		t6.coefF,
		t6.PortfolioDealOrderID,
		@TransID,
		1
	from (
		select
			t4.InvestmentDeclarationID,
			t4.InvestDeclLinkID,
			t4.InvestDeclWhereID,
			t4.FinInstID,
			numeratorF = coalesce(t4.numeratorF, 0),
			t4.denominatorF,
			partF = case t4.FLAG_Calculation when 1 then round(case when t4.denominatorF <> 0 then coalesce(t4.numeratorF, 0)*100/t4.denominatorF end, 5) when 0 then coalesce(t4.numeratorF, 0) end,
			numeratorP = coalesce(t4.numeratorP, 0),
			t4.denominatorP,
			partP = case t4.FLAG_Calculation when 1 then round(case when t4.denominatorP <> 0 then coalesce(t4.numeratorP, 0)*100/t4.denominatorP end, 5) when 0 then coalesce(t4.numeratorP, 0) end
		from (
			select
				InvestmentDeclarationID = mpd.InvestDeclID,
				InvestDeclLinkID = mpd.InvestDeclLinkID,
				InvestDeclWhereID = idw1.InvestmentDeclarationWhereID,
				FinInstID = mpd.FinInstID,
				idw1.FLAG_Calculation,
				numeratorF = case
					when idw1.Flag_Group in (0,1,12,13,14, 15, 20,21,22,24,28,33) then (select sum(RestF*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
					when idw1.Flag_Group in (2,5) then (select max(RestF*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
					when idw1.Flag_Group in (3,4,25,32) then (select max(Qty) from (select sum(RestF*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 group by r.IssuerID) t)
					when idw1.Flag_Group in (7)   then (select max(Qty) from (select sum(RestF*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.AccType = 3 group by r.BankID) t)
					when idw1.Flag_Group in (8)   then (select max(Qty) from (select sum(RestF*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.AccType = 1 group by r.BankID) t)
					when idw1.Flag_Group in (10)  then (select max(Qty) from (select sum(RestF*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.IssuerID1 in (select IssuerID1 from #r where InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and Flag_Div = 0 and AccType = 3) group by r.IssuerID1) t)
					when idw1.Flag_Group in (16)  then (
						select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
						from (
							select 
								sum(bc.mv) Rest,
								min(bc.mva) RestD
							from #r r
							 join tSecurity s on s.SecurityID = r.SecurityID
							 left join tSecuritySecurityGroup ssg on ssg.SecurityGroupID = 4 and ssg.SecurityID = r.SecurityID
							 cross apply (
								select bcda.mva, bcda.mv
								from tRDBondCommonData bcd 
									cross apply (
									select mva = sum(mva), mv = sum(mv)
									from (
										select
											mva = MARKET_VOL*Nominal,
											mv = case when t.ID = bcd.ID then r.RestF*Nominal end 
										from (
											select
												ID,
												MARKET_VOL,
												Nominal = ((FACEVALUE-case when amortised_mty = 1 then coalesce((select sum(PAY_PER_BOND) from tRDAmorts a where ID_FINTOOL = bcd1.ID and MTY_DATE <= @Date), 0) else 0 end))
											from tRDBondCommonData bcd1 
											where bcd1.FININSTID = bcd.FININSTID and (ssg.SecurityGroupID is null or bcd1.ID = bcd.ID)
										) t
									) t
								) bcda
								where bcd.MARKET_VOL is not null
									and bcd.ISIN = s.Number
							) bc
							where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
								and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                and r.FinInstID = mpd.FinInstID
								and r.Flag_Div = 0
							group by case when ssg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
						) t
					)
					when idw1.Flag_Group in (19) then (
						select max(t.Rest/nullif(bcd.MARKET_VOL, 0))
						from (
							select
								r.SecurityID,
								sum(r.RestF) Rest
							from #r r
							where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
								and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                and r.FinInstID = mpd.FinInstID
								and r.Flag_Div = 0
							group by r.SecurityID
						) t
						join tSecurity s on s.SecurityID = t.SecurityID
						join tRDBondCommonData bcd on bcd.ISIN = s.Number
					)
					when idw1.Flag_Group in (26) then (select sum(Duration*RestF*(PriceN+Coupon))/nullif(sum(RestF*(PriceN+Coupon)),0) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
					when idw1.Flag_Group in (27) then (select max(Duration) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.RestF > 0)
					when idw1.Flag_Group in (29) then (select sum(RestF*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.Flag_Div = 0)
					end,
				denominatorF = case
					when idw1.Flag_Group in (16, 19, 26, 27, 29) then 1
					when idw1.FLAG_Calculation = 0 then idw1.StopValue
					else (select sum(RestF*(PriceD+Coupon)) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 1)
					end,
				numeratorP = case
					when idw1.Flag_Group in (0,1,12,13,14,15, 20,21,22,24,28,33) then (select sum(RestP*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
					when idw1.Flag_Group in (2,5) then (select max(RestP*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
					when idw1.Flag_Group in (3,4,25,32) then (select max(Qty) from (select sum(RestP*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 group by r.IssuerID) t)
					when idw1.Flag_Group in (7)   then (select max(Qty) from (select sum(RestP*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.AccType = 3 group by r.BankID) t)
					when idw1.Flag_Group in (8)   then (select max(Qty) from (select sum(RestP*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.AccType = 1 group by r.BankID) t)
					when idw1.Flag_Group in (10)  then (select max(Qty) from (select sum(RestP*PriceN) Qty from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.IssuerID1 in (select IssuerID1 from #r where InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and Flag_Div = 0 and AccType = 3) group by r.IssuerID1) t)
					when idw1.Flag_Group in (16)  then (
						select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
						from (
							select 
								sum(bc.mv) Rest,
								min(bc.mva) RestD
							from #r r
							 join tSecurity s on s.SecurityID = r.SecurityID
							 left join tSecuritySecurityGroup ssg on ssg.SecurityGroupID = 4 and ssg.SecurityID = r.SecurityID
							 cross apply (
								select bcda.mva, bcda.mv
								from tRDBondCommonData bcd 
									cross apply (
									select mva = sum(mva), mv = sum(mv)
									from (
										select
											mva = MARKET_VOL*Nominal,
											mv = case when t.ID = bcd.ID then r.RestP*Nominal end 
										from (
											select
												ID,
												MARKET_VOL,
												Nominal = ((FACEVALUE-case when amortised_mty = 1 then coalesce((select sum(PAY_PER_BOND) from tRDAmorts a where ID_FINTOOL = bcd1.ID and MTY_DATE <= @Date), 0) else 0 end))
											from tRDBondCommonData bcd1 
											where bcd1.FININSTID = bcd.FININSTID and (ssg.SecurityGroupID is null or bcd1.ID = bcd.ID)
										) t
									) t
								) bcda
								where bcd.MARKET_VOL is not null
									and bcd.ISIN = s.Number
							) bc
							where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
								and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                and r.FinInstID = mpd.FinInstID
								and r.Flag_Div = 0
							group by case when ssg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
						) t
						--select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
						--from (
						--	select
						--		Rest,
						--		case when SecurityID = 0 then (
						--			select sum(lBInt1) EmitentVolume
						--			from (
						--				select top 1 l.LName from taLib l where l.LConcept = 18 and l.LInt1 in (1) and l.LDate1 > @Date and l.LName2 in (select Number from tSecurity where IssuerID = t.IssuerID)) t11
						--			 join taLib l on l.lconcept = 18 and l.lint1 in (1) and l.ldate1 > @Date and l.LName = t11.LName
						--			)
						--		else (select EmissionValue from tSecurity where SecurityID = t.SecurityID) end RestD
						--	from (
						--		select 
						--			case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end SecurityID,
						--			r.IssuerID,
						--			sum(RestP*s.Nominal) Rest
						--		from #r r
						--		 left join tSecurity s on s.SecurityID = r.SecurityID
						--		 left join tSecurityGroup sg on sg.SecurityGroupID = (select top 1 ssg.SecurityGroupID from tSecuritySecurityGroup ssg where ssg.SecurityID = r.SecurityID and ssg.SecurityGroupID in (select SecurityGroupID from tSecurityGroup where SecurityGroupTypeID = 1)) 
						--		where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
						--			and r.InvestDeclLinkID = idl.InvestmentDeclarationLinkID
						--			and r.Flag_Div = 0
						--		group by case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
						--	) t
						--) t1
					)
					when idw1.Flag_Group in (19) then (
						select max(t.Rest/nullif(bcd.MARKET_VOL, 0))
						from (
							select
								r.SecurityID,
								sum(r.RestP) Rest
							from #r r
							where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
								and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                and r.FinInstID = mpd.FinInstID
								and r.Flag_Div = 0
							group by r.SecurityID
						) t
						join tSecurity s on s.SecurityID = t.SecurityID
						join tRDBondCommonData bcd on bcd.ISIN = s.Number
					)
					when idw1.Flag_Group in (26) then (select sum(Duration*RestP*(PriceN+Coupon))/nullif(sum(RestP*(PriceN+Coupon)),0) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
					when idw1.Flag_Group in (27) then (select max(Duration) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.RestP > 0)
					when idw1.Flag_Group in (29) then (select sum(RestP*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.Flag_Div = 0)
				end,
				denominatorP = case
					when idw1.Flag_Group in (16,19,26,27,29) then 1
					when idw1.FLAG_Calculation = 0 then idw1.StopValue
					else (select sum(RestP*(PriceD+Coupon)) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 1)
					end
			from (select distinct InvestDeclID, InvestDeclLinkID, FinInstID from @d where FinInstID1 is not null) mpd
			 join sInvestmentDeclarationLink idl on idl.InvestmentDeclarationLinkID = mpd.InvestDeclLinkID and idl.Enb = 'T'
			 join sInvestmentDeclarationWhere idw1 on idw1.InvestmentDeclarationID = mpd.InvestDeclID and idw1.Enb = 'T'
		) t4
	) t3
	 left join (
		select
			t5.InvestmentDeclarationID,
			t5.InvestDeclLinkID,
			t5.InvestDeclWhereID,
			t5.FinInstID,
			t5.coefF,
			t5.coefP,
			t5.coefFP,
			t5.coefPP,
			t5.RestP,
			t5.RestF,
			t5.PriceN,
			t5.QtyP,
			t5.QtyF,
			t5.AccType,
			t5.BankID,
			t5.SecurityID1,
			t5.PortfolioDealOrderID
		from (
			select
				t4.InvestmentDeclarationID,
				t4.InvestDeclLinkID,
				t4.InvestDeclWhereID,
				t4.FinInstID,
				t4.StartValue,
				t4.StopValue,
				coefF = case t4.FLAG_Calculation when 1 then round(coalesce(t4.numeratorFS, 0)*100/nullif(t4.denominatorF, 0), 6) when 0 then coalesce(t4.numeratorFS, 0) end,
				coefP = case t4.FLAG_Calculation when 1 then round(coalesce(t4.numeratorPS, 0)*100/nullif(t4.denominatorP, 0), 6) when 0 then coalesce(t4.numeratorPS, 0) end,
				coefFP = case t4.FLAG_Calculation when 1 then round(coalesce(t4.numeratorFSP, 0)*100/nullif(t4.denominatorF, 0), 6) when 0 then coalesce(t4.numeratorFSP, 0) end,
				coefPP = case t4.FLAG_Calculation when 1 then round(coalesce(t4.numeratorPSP, 0)*100/nullif(t4.denominatorP, 0), 6) when 0 then coalesce(t4.numeratorPSP, 0) end,
				t4.RestP,
				t4.RestF,
				t4.PriceN,
				t4.QtyP,
				t4.QtyF,
				t4.AccType,
				t4.BankID,
				t4.SecurityID1,
				t4.PortfolioDealOrderID
			from (
				select
					InvestmentDeclarationID = mpd.InvestDeclID,
					InvestDeclLinkID = mpd.InvestDeclLinkID,
					InvestDeclWhereID = idw1.InvestmentDeclarationWhereID,
					idw1.StartValue,
					idw1.StopValue,
					FinInstID = mpd.FinInstID,
					idw1.FLAG_Calculation,
					numeratorFS = case
						when idw1.Flag_Group in (0,1,12,13,14,15,20,21,22,24,28,33) then (select sum(RestF*PriceN) from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0)
						when idw1.Flag_Group in (2,5) then r.RestF*r.PriceN
						when idw1.Flag_Group in (3,4,25,32,7,8) then (select sum(RestF*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID = r.IssuerID)
						when idw1.Flag_Group in (10) then (select sum(RestF*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID1 = r.IssuerID1)
						when idw1.Flag_Group in (16) then (
							select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							from (
								select 
									sum(bc.mv) Rest,
									min(bc.mva) RestD
								from #r r
								 join tSecurity s on s.SecurityID = r.SecurityID
								 left join tSecuritySecurityGroup ssg on ssg.SecurityGroupID = 4 and ssg.SecurityID = r.SecurityID
								 cross apply (
									select bcda.mva, bcda.mv
									from tRDBondCommonData bcd 
										cross apply (
										select mva = sum(mva), mv = sum(mv)
										from (
											select
												mva = MARKET_VOL*Nominal,
												mv = case when t.ID = bcd.ID then r.RestF*Nominal end 
											from (
												select
													ID,
													MARKET_VOL,
													Nominal = ((FACEVALUE-case when amortised_mty = 1 then coalesce((select sum(PAY_PER_BOND) from tRDAmorts a where ID_FINTOOL = bcd1.ID and MTY_DATE <= @Date), 0) else 0 end))
												from tRDBondCommonData bcd1 
												where bcd1.FININSTID = bcd.FININSTID and (ssg.SecurityGroupID is null or bcd1.ID = bcd.ID)
											) t
										) t
									) bcda
									where bcd.MARKET_VOL is not null
										and bcd.ISIN = s.Number
								) bc
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by case when ssg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							) t
							--select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							--from (
							--	select
							--		Rest,
							--		RestD = case when SecurityID = 0 then (
							--			select sum(lBInt1) EmitentVolume
							--			from (
							--				select top 1 l.LName from taLib l where l.LConcept = 18 and l.LInt1 in (1) and l.LDate1 > @Date and l.LName2 in (select Number from tSecurity where IssuerID = t.IssuerID)) t11
							--			 join taLib l on l.lconcept = 18 and l.lint1 in (1) and l.ldate1 > @Date and l.LName = t11.LName
							--			)
							--		else (select EmissionValue from tSecurity where SecurityID = t.SecurityID) end
							--	from (
							--		select 
							--			case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end SecurityID,
							--			r.IssuerID,
							--			Rest = sum(RestF*s.Nominal)
							--		from #r r
							--		 left join tSecurity s on s.SecurityID = r.SecurityID
							--		 left join tSecurityGroup sg on sg.SecurityGroupID = (select top 1 ssg.SecurityGroupID from tSecuritySecurityGroup ssg where ssg.SecurityID = r.SecurityID and ssg.SecurityGroupID in (select SecurityGroupID from tSecurityGroup where SecurityGroupTypeID = 1)) 
							--		where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
							--			and r.InvestDeclLinkID = idl.InvestmentDeclarationLinkID
							--			and r.Flag_Div = 0
							--		group by case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							--	) t
							--) t1
						)
						when idw1.Flag_Group in (19) then (
							select max(t.Rest/nullif(bcd.MARKET_VOL, 0))
							from (
								select
									r.SecurityID,
									sum(r.RestF) Rest
								from #r r
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by r.SecurityID
							) t
							join tSecurity s on s.SecurityID = t.SecurityID
							join tRDBondCommonData bcd on bcd.ISIN = s.Number
						)
						when idw1.Flag_Group in (26) then (select sum(Duration*RestF*(PriceN+Coupon))/nullif(sum(RestF*(PriceN+Coupon)),0) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
						when idw1.Flag_Group in (27) then (select max(Duration) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.RestF > 0)
						when idw1.Flag_Group in (29) then (select sum(RestF*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.Flag_Div = 0)
					end,
					numeratorPS = case
						when idw1.Flag_Group in (0,1,12,13,14,15,20,21,22,24,28,33) then (select sum(RestP*PriceN) from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0)
						when idw1.Flag_Group in (2,5) then r.RestP*r.PriceN
						when idw1.Flag_Group in (3,4,25,32,7,8) then (select sum(RestP*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID = r.IssuerID)
						when idw1.Flag_Group in (10)  then (select sum(RestP*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID1 = r.IssuerID1)
						when idw1.Flag_Group in (16)  then (
							select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							from (
								select 
									sum(bc.mv) Rest,
									min(bc.mva) RestD
								from #r r
								 join tSecurity s on s.SecurityID = r.SecurityID
								 left join tSecuritySecurityGroup ssg on ssg.SecurityGroupID = 4 and ssg.SecurityID = r.SecurityID
								 cross apply (
									select bcda.mva, bcda.mv
									from tRDBondCommonData bcd 
										cross apply (
										select mva = sum(mva), mv = sum(mv)
										from (
											select
												mva = MARKET_VOL*Nominal,
												mv = case when t.ID = bcd.ID then r.RestP*Nominal end 
											from (
												select
													ID,
													MARKET_VOL,
													Nominal = ((FACEVALUE-case when amortised_mty = 1 then coalesce((select sum(PAY_PER_BOND) from tRDAmorts a where ID_FINTOOL = bcd1.ID and MTY_DATE <= @Date), 0) else 0 end))
												from tRDBondCommonData bcd1 
												where bcd1.FININSTID = bcd.FININSTID and (ssg.SecurityGroupID is null or bcd1.ID = bcd.ID)
											) t
										) t
									) bcda
									where bcd.MARKET_VOL is not null
										and bcd.ISIN = s.Number
								) bc
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by case when ssg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							) t
							--select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							--from (
							--	select
							--		Rest,
							--		RestD = case when SecurityID = 0 then (
							--			select sum(lBInt1) EmitentVolume
							--			from (
							--				select top 1 l.LName from taLib l where l.LConcept = 18 and l.LInt1 in (1) and l.LDate1 > @Date and l.LName2 in (select Number from tSecurity where IssuerID = t.IssuerID)) t11
							--			 join taLib l on l.lconcept = 18 and l.lint1 in (1) and l.ldate1 > @Date and l.LName = t11.LName
							--			)
							--		else (select EmissionValue from tSecurity where SecurityID = t.SecurityID) end
							--	from (
							--		select 
							--			case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end SecurityID,
							--			r.IssuerID,
							--			Rest = sum(RestP*s.Nominal)
							--		from #r r
							--		 left join tSecurity s on s.SecurityID = r.SecurityID
							--		 left join tSecurityGroup sg on sg.SecurityGroupID = (select top 1 ssg.SecurityGroupID from tSecuritySecurityGroup ssg where ssg.SecurityID = r.SecurityID and ssg.SecurityGroupID in (select SecurityGroupID from tSecurityGroup where SecurityGroupTypeID = 1)) 
							--		where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
							--			and r.InvestDeclLinkID = idl.InvestmentDeclarationLinkID
							--			and r.Flag_Div = 0
							--		group by case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							--	) t
							--) t1
						)
						when idw1.Flag_Group in (19) then (
							select max(t.Rest/nullif(bcd.MARKET_VOL, 0))
							from (
								select
									r.SecurityID,
									sum(r.RestP) Rest
								from #r r
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by r.SecurityID
							) t
							join tSecurity s on s.SecurityID = t.SecurityID
							join tRDBondCommonData bcd on bcd.ISIN = s.Number
						)
						when idw1.Flag_Group in (26) then (select sum(Duration*RestP*(PriceN+Coupon))/nullif(sum(RestP*(PriceN+Coupon)),0) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
						when idw1.Flag_Group in (27) then (select max(Duration) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.RestP > 0)
						when idw1.Flag_Group in (29) then (select sum(RestP*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.Flag_Div = 0)
					end,
					numeratorFSP = case
						when idw1.Flag_Group in (0,1,12,13,14, 15,20,21,22,24,28,33) then (select sum(RestFP*PriceN) from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0)
						when idw1.Flag_Group in (2,5) then r.RestFP*r.PriceN
						when idw1.Flag_Group in (3,4,25,32,7,8) then (select sum(RestFP*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID = r.IssuerID)
						when idw1.Flag_Group in (10)  then (select sum(RestFP*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID1 = r.IssuerID1)
						when idw1.Flag_Group in (16)  then (
							select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							from (
								select 
									sum(bc.mv) Rest,
									min(bc.mva) RestD
								from #r r
								 join tSecurity s on s.SecurityID = r.SecurityID
								 left join tSecuritySecurityGroup ssg on ssg.SecurityGroupID = 4 and ssg.SecurityID = r.SecurityID
								 cross apply (
									select bcda.mva, bcda.mv
									from tRDBondCommonData bcd 
										cross apply (
										select mva = sum(mva), mv = sum(mv)
										from (
											select
												mva = MARKET_VOL*Nominal,
												mv = case when t.ID = bcd.ID then r.RestFP*Nominal end 
											from (
												select
													ID,
													MARKET_VOL,
													Nominal = ((FACEVALUE-case when amortised_mty = 1 then coalesce((select sum(PAY_PER_BOND) from tRDAmorts a where ID_FINTOOL = bcd1.ID and MTY_DATE <= @Date), 0) else 0 end))
												from tRDBondCommonData bcd1 
												where bcd1.FININSTID = bcd.FININSTID and (ssg.SecurityGroupID is null or bcd1.ID = bcd.ID)
											) t
										) t
									) bcda
									where bcd.MARKET_VOL is not null
										and bcd.ISIN = s.Number
								) bc
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by case when ssg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							) t
							--select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							--from (
							--	select
							--		Rest,
							--		RestD = case when SecurityID = 0 then (
							--			select sum(lBInt1) EmitentVolume
							--			from (
							--				select top 1 l.LName from taLib l where l.LConcept = 18 and l.LInt1 in (1) and l.LDate1 > @Date and l.LName2 in (select Number from tSecurity where IssuerID = t.IssuerID)) t11
							--			 join taLib l on l.lconcept = 18 and l.lint1 in (1) and l.ldate1 > @Date and l.LName = t11.LName
							--			)
							--		else (select EmissionValue from tSecurity where SecurityID = t.SecurityID) end
							--	from (
							--		select 
							--			case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end SecurityID,
							--			r.IssuerID,
							--			Rest = sum(RestFP*s.Nominal)
							--		from #r r
							--		 left join tSecurity s on s.SecurityID = r.SecurityID
							--		 left join tSecurityGroup sg on sg.SecurityGroupID = (select top 1 ssg.SecurityGroupID from tSecuritySecurityGroup ssg where ssg.SecurityID = r.SecurityID and ssg.SecurityGroupID in (select SecurityGroupID from tSecurityGroup where SecurityGroupTypeID = 1)) 
							--		where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
							--			and r.InvestDeclLinkID = idl.InvestmentDeclarationLinkID
							--			and r.Flag_Div = 0
							--		group by case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							--	) t
							--) t1
						)
						when idw1.Flag_Group in (19) then (
							select max(t.Rest/nullif(bcd.MARKET_VOL, 0))
							from (
								select
									r.SecurityID,
									sum(r.RestFP) Rest
								from #r r
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by r.SecurityID
							) t
							join tSecurity s on s.SecurityID = t.SecurityID
							join tRDBondCommonData bcd on bcd.ISIN = s.Number
						)
						when idw1.Flag_Group in (26) then (select sum(Duration*RestFP*(PriceN+Coupon))/nullif(sum(RestFP*(PriceN+Coupon)),0) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
						when idw1.Flag_Group in (27) then (select max(Duration) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.RestFP > 0)
						when idw1.Flag_Group in (29) then (select sum(RestFP*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.Flag_Div = 0)
					end,
					numeratorPSP = case
						when idw1.Flag_Group in (0,1,12,13,14, 15,20,21,22,24,28,33) then (select sum(RestPP*PriceN) from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0)
						when idw1.Flag_Group in (2,5) then r.RestPP*r.PriceN
						when idw1.Flag_Group in (3,4,25,32,7,8) then (select sum(RestPP*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID = r.IssuerID)
						when idw1.Flag_Group in (10)  then (select sum(RestPP*PriceN) Qty from #r r1 where r1.InvestDeclWhereID = r.InvestDeclWhereID and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 0 and r1.IssuerID1 = r.IssuerID1)
						when idw1.Flag_Group in (16)  then (
							select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							from (
								select 
									sum(bc.mv) Rest,
									min(bc.mva) RestD
								from #r r
								 join tSecurity s on s.SecurityID = r.SecurityID
								 left join tSecuritySecurityGroup ssg on ssg.SecurityGroupID = 4 and ssg.SecurityID = r.SecurityID
								 cross apply (
									select bcda.mva, bcda.mv
									from tRDBondCommonData bcd 
										cross apply (
										select mva = sum(mva), mv = sum(mv)
										from (
											select
												mva = MARKET_VOL*Nominal,
												mv = case when t.ID = bcd.ID then r.RestPP*Nominal end 
											from (
												select
													ID,
													MARKET_VOL,
													Nominal = ((FACEVALUE-case when amortised_mty = 1 then coalesce((select sum(PAY_PER_BOND) from tRDAmorts a where ID_FINTOOL = bcd1.ID and MTY_DATE <= @Date), 0) else 0 end))
												from tRDBondCommonData bcd1 
												where bcd1.FININSTID = bcd.FININSTID and (ssg.SecurityGroupID is null or bcd1.ID = bcd.ID)
											) t
										) t
									) bcda
									where bcd.MARKET_VOL is not null
										and bcd.ISIN = s.Number
								) bc
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by case when ssg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							) t
							--select max(case when RestD <> 0 then cast(Rest as float)/RestD end)
							--from (
							--	select
							--		Rest,
							--		RestD = case when SecurityID = 0 then (
							--			select sum(lBInt1) EmitentVolume
							--			from (
							--				select top 1 l.LName from taLib l where l.LConcept = 18 and l.LInt1 in (1) and l.LDate1 > @Date and l.LName2 in (select Number from tSecurity where IssuerID = t.IssuerID)) t11
							--			 join taLib l on l.lconcept = 18 and l.lint1 in (1) and l.ldate1 > @Date and l.LName = t11.LName
							--			)
							--		else (select EmissionValue from tSecurity where SecurityID = t.SecurityID) end
							--	from (
							--		select 
							--			case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end SecurityID,
							--			r.IssuerID,
							--			Rest = sum(RestPP*s.Nominal)
							--		from #r r
							--		 left join tSecurity s on s.SecurityID = r.SecurityID
							--		 left join tSecurityGroup sg on sg.SecurityGroupID = (select top 1 ssg.SecurityGroupID from tSecuritySecurityGroup ssg where ssg.SecurityID = r.SecurityID and ssg.SecurityGroupID in (select SecurityGroupID from tSecurityGroup where SecurityGroupTypeID = 1)) 
							--		where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
							--			and r.InvestDeclLinkID = idl.InvestmentDeclarationLinkID
							--			and r.Flag_Div = 0
							--		group by case when sg.SecurityGroupID = 4 then r.SecurityID else 0 end, r.IssuerID
							--	) t
							--) t1
						)
						when idw1.Flag_Group in (19) then (
							select max(t.Rest/nullif(bcd.MARKET_VOL, 0))
							from (
								select
									r.SecurityID,
									sum(r.RestPP) Rest
								from #r r
								where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID
									and r.InvestDeclLinkID = mpd.InvestDeclLinkID
                  and r.FinInstID = mpd.FinInstID
									and r.Flag_Div = 0
								group by r.SecurityID
							) t
							join tSecurity s on s.SecurityID = t.SecurityID
							join tRDBondCommonData bcd on bcd.ISIN = s.Number
						)
						when idw1.Flag_Group in (26) then (select sum(Duration*RestPP*(PriceN+Coupon))/nullif(sum(RestPP*(PriceN+Coupon)),0) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0)
						when idw1.Flag_Group in (27) then (select max(Duration) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.RestPP > 0)
						when idw1.Flag_Group in (29) then (select sum(RestPP*PriceN) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.Flag_Div = 0)					
					end,
					denominatorF = case
						when idw1.Flag_Group in (16,19,26,27,29) then 1
						when idw1.FLAG_Calculation = 0 then idw1.StopValue
						else (select sum(RestF*(PriceD+Coupon)) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 1)
						end,
					denominatorP = case
						when idw1.Flag_Group in (16,19,26,27,29) then 1
						when idw1.FLAG_Calculation = 0 then idw1.StopValue
						else (select sum(RestP*(PriceD+Coupon)) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 1)
						end,
					denominatorFP = case
						when idw1.Flag_Group in (16,19,26,27,29) then 1
						when idw1.FLAG_Calculation = 0 then idw1.StopValue
						else (select sum(RestFP*(PriceD+Coupon)) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 1)
						end,
					denominatorPP = case
						when idw1.Flag_Group in (16,19,26,27,29) then 1
						when idw1.FLAG_Calculation = 0 then idw1.StopValue
						else (select sum(RestPP*(PriceD+Coupon)) from #r r where r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 1)
						end,
						r.RestP,
						r.RestF,
						r.PriceN,
						QtyP = r.RestP*r.PriceN,
						QtyF = r.RestF*r.PriceN,
						r.SecurityID1,
						r.AccType,
						r.BankID,
						mpd.PortfolioDealOrderID
				from (select distinct InvestDeclID, InvestDeclLinkID, SecurityID, AccType, BankID, PortfolioDealOrderID, FinInstID from @d where FinInstID1 is not null) mpd
				 join sInvestmentDeclarationLink idl on idl.InvestmentDeclarationLinkID = mpd.InvestDeclLinkID and idl.Enb = 'T'
				 join sInvestmentDeclarationWhere idw1 on idw1.InvestmentDeclarationID = mpd.InvestDeclID and idw1.Enb = 'T'
				 join #r r on r.InvestDeclWhereID = idw1.InvestmentDeclarationWhereID and r.InvestDeclLinkID = mpd.InvestDeclLinkID and r.FinInstID = mpd.FinInstID and r.Flag_Div = 0 and r.SecurityID1 = mpd.SecurityID and r.AccType = mpd.AccType and r.BankID = mpd.BankID
			) t4	
		) t5
		where (t5.coefF > t5.StopValue and t5.coefFP >= t5.StartValue and t5.coefFP < t5.coefF)
			 or (t5.coefF < t5.StartValue and t5.coefFP <= t5.StopValue and t5.coefFP > t5.coefF)
			 or (t5.coefP > t5.StopValue and t5.coefPP >= t5.StartValue and t5.coefPP < t5.coefP)
			 or (t5.coefP < t5.StartValue and t5.coefPP <= t5.StopValue and t5.coefPP > t5.coefP)
	) t6 on t6.InvestmentDeclarationID = t3.InvestmentDeclarationID and t6.InvestDeclLinkID = t3.InvestDeclLinkID and t6.InvestDeclWhereID = t3.InvestDeclWhereID and t6.FinInstID = t3.FinInstID
	 join sInvestmentDeclaration id on id.InvestmentDeclarationID = t3.InvestmentDeclarationID
	 left join tFinancialInstitution fi on fi.FinancialInstitutionID = t3.FinInstID
	 left join sInvestmentDeclarationWhere idw on idw.InvestmentDeclarationWhereID = t3.InvestDeclWhereID
	 left join sInvestmentDeclarationGroupType idg on idg.ID = idw.Flag_Group
	 left join tSecurity s on s.SecurityID = t6.SecurityID1

	union all

	select
		@ID, 
		r.InvestDeclLinkID,
		id.InvestmentDeclarationID,
		id.Name IDName,
		r.InvestDeclWhereID,
		r.SecurityID,
		null StartValue,
		null StopValue,
		fi.Name fiName,
		t4.numeratorF,
		t4.denominatorF,
		round(case when t4.denominatorF <> 0 then coalesce(t4.numeratorF, 0)*100/t4.denominatorF end, 6) partF,
		t4.numeratorP,
		t4.denominatorP,
		round(case when t4.denominatorP <> 0 then coalesce(t4.numeratorP, 0)*100/t4.denominatorP end, 6) partP,
		r.FinInstID,
		'Недопустимый актив' NameWhere,
		null Flag_Group,
		null Name,
		null GroupName,
		ErrorF = cast(1 as bit),
		ErrorP = cast(1 as bit),
		s.Name1+case s.SecType when 4 then '(' + case r.AccType when 1 then 'Р/С' when 2 then 'БИРЖА' when 3 then 'ДЕПОЗИТ'+coalesce(' '+(select rtrim(fic.NameBrief) from tFinancialInstitution fic where fic.FinancialInstitutionID = r.BankID), '') when 4 then 'НСО' else 'ПРОЧЕЕ' end + ')' else '' end SecName,
		DateEnd = case when s.SecType = 2 and s.DateEnd < @CurDate then ' ('+convert(varchar, s.DateEnd, 4)+')' end,
		r.RestP,
		r.RestF,
		r.PriceN,
		r.RestP*r.PriceN QtyP,
		r.RestF*r.PriceN QtyF,
		round(case when t4.denominatorP <> 0 then	r.RestP*r.PriceN/t4.denominatorP*100	end, 2) coefP,
		round(case when t4.denominatorF <> 0 then	r.RestF*r.PriceN/t4.denominatorF*100 end, 2) coefF,
		mpd.PortfolioDealOrderID,
		@TransID,
		1
	from (
		select
			r.InvestDeclID,
			r.InvestDeclLinkID,
			r.FinInstID,
			r.SecurityID1,
			numeratorP = sum(r.RestP*r.PriceN),
			numeratorF = sum(r.RestF*r.PriceN),
			denominatorP = (select sum(RestP*(PriceD+Coupon)) from #r r1 where r1.InvestDeclWhereID = -1 and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 1),
			denominatorF = (select sum(RestF*(PriceD+Coupon)) from #r r1 where r1.InvestDeclWhereID = -1 and r1.InvestDeclLinkID = r.InvestDeclLinkID and r1.FinInstID = r.FinInstID and r1.Flag_Div = 1)
		from #r r
		 cross apply (select top 1 * from @d where InvestDeclID = r.InvestDeclID and InvestDeclLinkID = r.InvestDeclLinkID and FinInstID1 = r.FinInstID and SecurityID = r.SecurityID1) mpd
		 --join sInvestmentDeclaration id on id.InvestmentDeclarationID = r.InvestDeclID
		where (@Direction = 0 or r.SecType in (18,24)) and r.InvestDeclWhereID = -1 and r.FLAG_Div = 0 --and 1 != 1
		group by r.InvestDeclID, r.InvestDeclLinkID, r.FinInstID, r.SecurityID1
		having count(*) > 0
	) t4
	 join (select distinct InvestDeclLinkID, FinInstID = FinInstID1, SecurityID, PortfolioDealOrderID from @d where FinInstID1 is not null) mpd on mpd.InvestDeclLinkID = t4.InvestDeclLinkID and mpd.FinInstID = t4.FinInstID and mpd.SecurityID = t4.SecurityID1
	 join #r r on r.InvestDeclWhereID = -1 and r.InvestDeclLinkID = t4.InvestDeclLinkID and r.FinInstID = t4.FinInstID and r.FLAG_Div = 0
   join sInvestmentDeclaration id on id.InvestmentDeclarationID = r.InvestDeclID
	 left join tFinancialInstitution fi on fi.FinancialInstitutionID = r.FinInstID
	 left join tSecurity s on s.SecurityID = r.SecurityID

	union all

	select
		@ID, 
		r.InvestDeclLinkID,
		id.InvestmentDeclarationID,
		id.Name IDName,
		null InvestDeclWhereID,
		r.SecurityID,
		null StartValue,
		null StopValue,
		fi.Name fiName,
		r.QtyF,
		0 denominatorF,
		0 partF,
		r.QtyP,
		0 denominatorP,
		0 partP,
		r.FinInstID,
		'Недостаточный остаток на брокерском счёте' NameWhere,
		null Flag_Group,
		null Name,
		null GroupName,
		ErrorF = cast(1 as bit),
		ErrorP = cast(1 as bit),
		SecName = s.Name1+case s.SecType when 4 then '(БИРЖА)' else '' end,
		DateEnd = null,
		r.RestP,
		r.RestF,
		0 Price,
		r.QtyP,
		r.QtyF,
		0 coefP,
		0 coefF,
		mpd.PortfolioDealOrderID,
		@TransID,
		1
	from (
		select distinct
			r.InvestDeclLinkID,
			r.InvestDeclID,
			r.FinInstID,
			r.SecurityID,
			(r.RestF*r.PriceN) QtyF,
			(r.RestP*r.PriceN) QtyP,
			(r.RestF) RestF,
			(r.RestP) RestP
		from #r r
		where 1=2 and @Direction = 0 and r.AccType = 2 and r.RestP < -1 and r.FLAG_Div = 0 and r.TreatyID in (select TreatyID from @d) and TreatyTypeID in (7, 8, 10, 45) and suser_name() not in ('IvanovaID', 'KalenskyAA', 'SemenovaAS')
		--group by
		--	r.InvestDeclLinkID,
		--	r.InvestDeclID,
		--	r.FinInstID,
		--	r.SecurityID		
	) r
   join sInvestmentDeclaration id on id.InvestmentDeclarationID = r.InvestDeclID
   join @d mpd on mpd.InvestDeclID = r.InvestDeclID and mpd.InvestDeclLinkID = r.InvestDeclLinkID and mpd.FinInstID1 = r.FinInstID
	 left join tFinancialInstitution fi on fi.FinancialInstitutionID = r.FinInstID
	 left join tSecurity s on s.SecurityID = r.SecurityID
	order by IDName, fi.Name, idw.InvestmentDeclarationWhereID

print 'Заполнение таблицы CheckDecl конец'
print convert(varchar, getdate(), 113)
		--select row_number() over(order by l.ID) rn, l.*, d.QUIK_ClientN, d.QUIK_Account
		--from tCheckClnDeclLog l
		-- left join SQuikSetDeal d on d.SQuikSetDealID = coalesce(l.TransID, 0)
		--where l.ID = @ID --and l.SecurityID is not null

	--select @@rowcount
	
	--select * from #r where SecurityID = @SecurityID

