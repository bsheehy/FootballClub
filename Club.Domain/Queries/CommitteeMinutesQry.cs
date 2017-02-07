using Club.Domain.Artifacts;
using Club.Domain.Interfaces;
using Mallon.Core.Queries;
using Mallon.Core.Resources;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections;

namespace Club.Domain.Queries
{
  public class CommitteeMinutesQry : HqlQuery, ISearchQry
  {
    public CommitteeMinutesQry()
      : base(typeof(CommitteeMinute))
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
      if (string.IsNullOrEmpty(this.aq_CommitteeMinutesMinutesText) == false)
        return true;

      if (aq_CommitteeMinutesOid.HasValue && aq_CommitteeMinutesOid.Value != Guid.Empty)
        return true;

      if (aq_CommitteeMinutesDateFrom.HasValue && aq_CommitteeMinutesDateFrom != DateTime.MinValue)
        return true;

      if (aq_CommitteeMinutesDateTo.HasValue && aq_CommitteeMinutesDateTo != DateTime.MinValue)
        return true;

      return false;
    }

    [DisplayName("Minutes Text")]
    public virtual string aq_CommitteeMinutesMinutesText { get; set; }

    [DisplayName("Committee Minutes")]
    public Guid? aq_CommitteeMinutesOid { get; set; }

    [DisplayName("Committee")]
    public Guid? aq_CommitteeMinutesCommitteeOid { get; set; }

    [DisplayName("Date From")]
    public DateTime? aq_CommitteeMinutesDateFrom { get; set; }

    [DisplayName("Date To")]
    public DateTime? aq_CommitteeMinutesDateTo { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_CommitteeMinutesMinutesText)) ? CommitteeMinutesMinutesTextString : null) +
      (aq_CommitteeMinutesOid.HasValue && aq_CommitteeMinutesOid.Value != Guid.Empty ? CommitteeMinutesOidString : null) +
      (aq_CommitteeMinutesCommitteeOid.HasValue && aq_CommitteeMinutesCommitteeOid.Value != Guid.Empty ? CommitteeMinutesCommitteeOidString : null) +
      (aq_CommitteeMinutesDateFrom.HasValue && aq_CommitteeMinutesDateFrom != DateTime.MinValue ? CommitteeMinutesDateFromString : null) +
      (aq_CommitteeMinutesDateTo.HasValue && aq_CommitteeMinutesDateTo != DateTime.MinValue ? CommitteeMinutesDateToString : null) +
      orderByString;
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_CommitteeMinutesMinutesText)))
        query.SetString("CommitteeMinutesMinutesText", "%" + aq_CommitteeMinutesMinutesText + "%");

      if (aq_CommitteeMinutesOid.HasValue && aq_CommitteeMinutesOid.Value != Guid.Empty)
        query.SetGuid("CommitteeMinutesOid", aq_CommitteeMinutesOid.Value);

      if (aq_CommitteeMinutesCommitteeOid.HasValue && aq_CommitteeMinutesCommitteeOid.Value != Guid.Empty)
        query.SetGuid("CommitteeMinutesCommitteeOid", aq_CommitteeMinutesCommitteeOid.Value);

      if (aq_CommitteeMinutesDateFrom.HasValue && aq_CommitteeMinutesDateFrom != DateTime.MinValue)
        query.SetDateTime("CommitteeMinutesDateFrom", aq_CommitteeMinutesDateFrom.Value);

      if (aq_CommitteeMinutesDateTo.HasValue && aq_CommitteeMinutesDateTo != DateTime.MinValue)
        query.SetDateTime("CommitteeMinutesDateTo", aq_CommitteeMinutesDateTo.Value);

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
      "select r from Club.Domain.Artifacts.CommitteeMinute as r where 1=1 " + Environment.NewLine;

    private readonly string CommitteeMinutesMinutesTextString =
      "and r.MinutesText like :CommitteeMinutesMinutesText " + Environment.NewLine;

    private readonly string CommitteeMinutesDateFromString =
      "and r.Date>= :CommitteeMinutesDateFrom " + Environment.NewLine;

    private readonly string CommitteeMinutesDateToString =
      "and r.Date<= :CommitteeMinutesDateTo " + Environment.NewLine;

    private readonly string CommitteeMinutesOidString =
      "and r.Oid = :CommitteeMinutesOid " + Environment.NewLine;

    private readonly string CommitteeMinutesCommitteeOidString =
    "and r.Committee.Oid = :CommitteeMinutesCommitteeOid " + Environment.NewLine;

    private readonly string orderByString =
      "order by r.Date desc";
  }
}
