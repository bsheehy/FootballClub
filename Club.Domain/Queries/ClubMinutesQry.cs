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
  public class ClubMinutesQry : HqlQuery, ISearchQry
  {
    public ClubMinutesQry()
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
      if (string.IsNullOrEmpty(this.aq_ClubMinutesMinutesText) == false)
        return true;

      if (aq_ClubMinutesOid.HasValue && aq_ClubMinutesOid.Value != Guid.Empty)
        return true;

      if (aq_ClubMinutesDateFrom.HasValue && aq_ClubMinutesDateFrom != DateTime.MinValue)
        return true;

      if (aq_ClubMinutesDateTo.HasValue && aq_ClubMinutesDateTo != DateTime.MinValue)
        return true;

      return false;
    }


    [DisplayName("Minutes Text")]
    public virtual string aq_ClubMinutesMinutesText { get; set; }

    [DisplayName("Committee Minutes")]
    public Guid? aq_ClubMinutesOid { get; set; }

    [DisplayName("Date From")]
    public DateTime? aq_ClubMinutesDateFrom { get; set; }

    [DisplayName("Date To")]
    public DateTime? aq_ClubMinutesDateTo { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_ClubMinutesMinutesText)) ? clubMinutesMinutesTextString : null) +
      (aq_ClubMinutesOid.HasValue && aq_ClubMinutesOid.Value != Guid.Empty ? clubMinutesOidString : null) +
      (aq_ClubMinutesDateFrom.HasValue && aq_ClubMinutesDateFrom != DateTime.MinValue ? clubMinutesYearFromString : null) +
      (aq_ClubMinutesDateTo.HasValue && aq_ClubMinutesDateTo != DateTime.MinValue ? clubMinutesYearToString : null) +
      orderByString;
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_ClubMinutesMinutesText)))
        query.SetString("clubMinutesMinutesText", "%" + aq_ClubMinutesMinutesText + "%");

      if (aq_ClubMinutesOid.HasValue && aq_ClubMinutesOid.Value != Guid.Empty)
        query.SetGuid("clubMinutesOid", aq_ClubMinutesOid.Value);

      if (aq_ClubMinutesDateFrom.HasValue && aq_ClubMinutesDateFrom != DateTime.MinValue)
        query.SetDateTime("clubMinutesYearFrom", aq_ClubMinutesDateFrom.Value);

      if (aq_ClubMinutesDateTo.HasValue && aq_ClubMinutesDateTo != DateTime.MinValue)
        query.SetDateTime("clubMinutesYearTo", aq_ClubMinutesDateTo.Value);

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
      "select r from Club.Domain.Artifacts.ClubMinutes as r where 1=1 " + Environment.NewLine;

    private readonly string clubMinutesMinutesTextString =
      "and r.MinutesText like :clubMinutesMinutesText " + Environment.NewLine;

    private readonly string clubMinutesYearFromString =
      "and r.Date>= :clubMinutesYearFrom " + Environment.NewLine;

    private readonly string clubMinutesYearToString =
      "and r.Date<= :clubMinutesYearTo " + Environment.NewLine;

    private readonly string clubMinutesOidString =
      "and r.Oid = :clubMinutesOid " + Environment.NewLine;

    private readonly string orderByString =
      "order by r.Date desc";
  }
}
