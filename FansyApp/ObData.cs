using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FansyApp
{
  public class ObData : IObData
  {
    private readonly ISqlDataAccess _db;

    public ObData(ISqlDataAccess db)
    {
      _db = db;
    }
    public Task<List<decimal>> GetDURest(int TreatyID)
    {
      string sql = @"SET dateformat dmy;
declare @reg_3 int = @TreatyID-- Номер договора 
declare @num int -- номер договора в формате, понятном пользователю. В данный момент эта часть закоментирована
declare @start_date date = getdate() --дата на которую делаем выборку

select
ISNULL(sum(D_EQ_END_51),0) - ISNULL(sum(D_EQ_END_68_01),0) - ISNULL(sum(D_EQ_END_76_05),0) - ISNULL(sum(D_EQ_END_76_33),0) as result
from
(
select
case when R.BAL_ACC = 2774 then
cast(
(-sum( T.TYPE_*T.EQ_)
+ sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1+T.TYPE_)/2 else  0 end)
- sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1-T.TYPE_)/2 else  0 end)
) as dec(12,2)) end as D_EQ_END_51,
case when R.BAL_ACC = 2814 then
cast(
(-sum( T.TYPE_*T.EQ_)
+ sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1+T.TYPE_)/2 else  0 end)
- sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1-T.TYPE_)/2 else  0 end)
) as dec(12,2))
end D_EQ_END_68_01,
case when R.BAL_ACC = 2796 then
cast(
(-sum( T.TYPE_*T.EQ_)
+ sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1+T.TYPE_)/2 else  0 end)
- sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1-T.TYPE_)/2 else  0 end)
) as dec(12,2))
end D_EQ_END_76_05,
case when R.BAL_ACC = 3394 then
cast(
(-sum( T.TYPE_*T.EQ_)
+ sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1+T.TYPE_)/2 else  0 end)
- sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1-T.TYPE_)/2 else  0 end)
) as dec(12,2))
end D_EQ_END_76_33
from
OD_RESTS R with(readcommitted)
left join OD_TURNS T with(readcommitted)
on T.REST=R.ID
and T.WIRDATE>=DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') and T.WIRDATE<='01.01.9999'
and T.IS_PLAN='F'
where R.BAL_ACC in (2774, 2814, 2796, 3394)
and R.REG_3=@reg_3
and T.WIRING is not null
group by R.BAL_ACC
having (0=1)
or dbo.f_Round( -sum( T.TYPE_*T.EQ_),2)<>0
or dbo.f_Round( sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1+T.TYPE_)/2 else  0 end),2)<>0
or dbo.f_Round( sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.EQ_*(1-T.TYPE_)/2 else  0 end),2)<>0
or dbo.f_Round( -sum( T.TYPE_*T.AMOUNT_),7)<>0
or dbo.f_Round( sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.AMOUNT_*(1+T.TYPE_)/2 else  0 end),7)<>0
or dbo.f_Round( sum(case when T.WIRDATE<DATEADD(day, DATEDIFF(day, 0, @start_date), '23:59:59') then T.AMOUNT_*(1-T.TYPE_)/2 else  0 end),7)<>0

) a
option(loop join,force order)";
      return _db.LoadData<decimal, object>(sql, new { TreatyID = TreatyID });
    }

  }
}
