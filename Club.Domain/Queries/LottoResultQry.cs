using Club.Domain.Artifacts;
using Club.Domain.Interfaces;
using Mallon.Core.Queries;
using Mallon.Core.Resources;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Club.Domain.Queries
{
  public class LottoResultQry : HqlQuery, ISearchQry
  {
    public LottoResultQry()
      : base(typeof(LottoResult))
    {
      this.PageSize = 10;
      this.Page = 0;
    }

    public string JsonSerializeObject()
    {
      return JsonConvert.SerializeObject(this);
    }

    public T JsonDeserializeString<T>(string jsonString) where T : ISearchQry
    {
      return JsonConvert.DeserializeObject<T>(jsonString);
    }

    [DisplayName("Max Records")]
    [DisplayInfo("This is the Maximum number of records returned by the query.<br/> Be careful and patient when setting it above 500.")]
    public int MaxRecords
    {
      get
      {
        return maxRecords;
      }
      set
      {
        if (value < 1)
        {
          maxRecords = 200;
          return;
        }
        maxRecords = value;
      }
    }
    private int maxRecords = 200;

    public int PageSize { get; set; }

    public int Page { get; set; }

    public bool HasSearchCriteria()
    {
      if (this.aq_DateFrom.HasValue && this.aq_DateFrom.Value != DateTime.MinValue)
        return true;

      if (this.aq_DateTo.HasValue && this.aq_DateTo.Value != DateTime.MinValue)
        return true;

      if (aq_Number1.HasValue && aq_Number1.Value > 0)
        return true;

      if (aq_Number2.HasValue && aq_Number2.Value > 0)
        return true;

      if (aq_Number2.HasValue && aq_Number3.Value > 0)
        return true;

      if (aq_Number2.HasValue && aq_Number4.Value > 0)
        return true;

      return false;
    }

    private IList<int> choosenNumbers = new List<int>();

    [DisplayName("Draw Date From")]
    public virtual DateTime? aq_DateFrom { get; set; }

    [DisplayName("Draw Date To")]
    public virtual DateTime? aq_DateTo { get; set; }

    [DisplayName("No 1")]
    public virtual int? aq_Number1 { get; set; }

    [DisplayName("No 2")]
    public virtual int? aq_Number2 { get; set; }

    [DisplayName("No 3")]
    public virtual int? aq_Number3 { get; set; }

    [DisplayName("No 4")]
    public virtual int? aq_Number4 { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
        ((this.aq_DateFrom.HasValue && this.aq_DateFrom.Value != DateTime.MinValue) ? dateFromString : null) +
        ((this.aq_DateTo.HasValue && this.aq_DateTo.Value != DateTime.MinValue) ? dateToString : null) +
        ((aq_Number1.HasValue && aq_Number1.Value > 0) ? number1String : null) +
        ((aq_Number2.HasValue && aq_Number2.Value > 0) ? number2String : null) +
        ((aq_Number3.HasValue && aq_Number3.Value > 0) ? number3String : null) +
        ((aq_Number4.HasValue && aq_Number4.Value > 0) ? number4String : null);

      if (aq_Number1.HasValue && aq_Number1.Value > 0)
        choosenNumbers.Add(aq_Number1.Value);

      if (aq_Number2.HasValue && aq_Number2.Value > 0)
        choosenNumbers.Add(aq_Number2.Value);

      if (aq_Number3.HasValue && aq_Number3.Value > 0)
        choosenNumbers.Add(aq_Number3.Value);

      if (aq_Number4.HasValue && aq_Number4.Value > 0)
        choosenNumbers.Add(aq_Number4.Value);
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (this.aq_DateFrom.HasValue && this.aq_DateFrom.Value != DateTime.MinValue)
        query.SetDateTime("dateFrom", aq_DateFrom.Value);

      if (this.aq_DateTo.HasValue && this.aq_DateTo.Value != DateTime.MinValue)
        query.SetDateTime("dateTo", aq_DateTo.Value);

      if (aq_Number1.HasValue && aq_Number1.Value > 0)
        query.SetInt32("aq_number1", aq_Number1.Value);

      if (aq_Number2.HasValue && aq_Number2.Value > 0)
        query.SetInt32("aq_number2", aq_Number2.Value);

      if (aq_Number3.HasValue && aq_Number3.Value > 0)
        query.SetInt32("aq_number3", aq_Number3.Value);

      if (aq_Number4.HasValue && aq_Number4.Value > 0)
        query.SetInt32("aq_number4", aq_Number4.Value);
    }

    protected override IList List(ISession session)
    {
      PrepareHql();
      IQuery query = CreateQuery(session);
      //aq_uery.SetFirstResult(PageSize * Page).SetMaxResults(MaxRecords);
      query.SetMaxResults(MaxRecords);
      PrepareQuery(query);
      return query.List();
    }

    private readonly string queryString =
    "select r from Club.Domain.Artifacts.LottoResult as r where 1=1 " + Environment.NewLine;

    private readonly string dateFromString =
    "and r.DrawDate >=:dateFrom " + Environment.NewLine;

    private readonly string dateToString =
    "and r.DrawDate <= :dateTo " + Environment.NewLine;

    private readonly string number1String =
    "and (r.No1 = :aq_number1 OR r.No2 = :aq_number1 OR r.No3 = :aq_number1  OR r.No4  = :aq_number1)" + Environment.NewLine;

    private readonly string number2String =
    "and (r.No1 = :aq_number2 OR r.No2 = :aq_number2 OR r.No3 = :aq_number2  OR r.No4  = :aq_number2)" + Environment.NewLine;

    private readonly string number3String =
    "and (r.No1 = :aq_number3 OR r.No2 = :aq_number3 OR r.No3 = :aq_number3  OR r.No4  = :aq_number3)" + Environment.NewLine;

    private readonly string number4String =
    "and (r.No1 = :aq_number4 OR r.No2 = :aq_number4 OR r.No3 = :aq_number4  OR r.No4  = :aq_number4)" + Environment.NewLine;

  }
}
