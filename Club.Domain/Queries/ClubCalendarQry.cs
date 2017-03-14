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
  public class ClubCalendarQry : HqlQuery, ISearchQry
  {
    public ClubCalendarQry()
      : base(typeof(ClubCalendar))
    {
      this.PageSize = 5000;
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
    private int maxRecords = 5000;

    public int PageSize { get; set; }

    public int Page { get; set; }

    public bool HasSearchCriteria()
    {
      if (aq_ClubCalendarEventTypeOid.HasValue && aq_ClubCalendarEventTypeOid.Value != Guid.Empty)
        return true;

      if (aq_TeamOid.HasValue && aq_TeamOid.Value != Guid.Empty)
        return true;

      if (string.IsNullOrEmpty(aq_TeamName) == false)
        return true;

      if (aq_YearFrom.HasValue && aq_YearFrom > 0)
        return true;

      if (aq_YearTo.HasValue && aq_YearTo > 0)
        return true;

      if (this.aq_StartFrom.HasValue)
        return true;

      if (this.aq_StartTo.HasValue)
        return true;

      if (this.aq_EndFrom.HasValue)
        return true;

      if (this.aq_EndTo.HasValue)
        return true;

      return false;
    }

    [DisplayName("Year From")]
    public int? aq_YearFrom { get; set; }

    [DisplayName("Year To")]
    public int? aq_YearTo { get; set; }

    [DisplayName("Start Date From")]
    public DateTime? aq_StartFrom { get; set; }

    [DisplayName("Start Date To")]
    public DateTime? aq_StartTo { get; set; }

    [DisplayName("End Date From")]
    public DateTime? aq_EndFrom { get; set; }

    [DisplayName("End Date To")]
    public DateTime? aq_EndTo { get; set; }

    /// <summary>
    /// Used for model purposes only - dont search on it.
    /// </summary>
    [DisplayName("Event Type")]
    public virtual Guid? aq_ClubCalendarEventTypeOid { get; set; }

    [DisplayName("Team")]
    public virtual Guid? aq_TeamOid { get; set; }

    [DisplayName("Team Name")]
    public virtual string aq_TeamName { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
        (!(String.IsNullOrEmpty(aq_TeamName)) ? teamNameString : null) +
        (aq_TeamOid.HasValue && aq_TeamOid.Value != Guid.Empty ? teamOidString : null) +
        (aq_ClubCalendarEventTypeOid.HasValue && aq_ClubCalendarEventTypeOid.Value != Guid.Empty ? clubCalendarEventTypeOidString : null) +
        (aq_StartFrom.HasValue ? startFromString : null) +
        (aq_StartTo.HasValue ? startToString : null) +
        (aq_EndFrom.HasValue ? endFromString : null) +
        (aq_EndTo.HasValue ? endToString : null);
    }

    protected override void PrepareQuery(IQuery query)
    {

      if (aq_TeamOid.HasValue && aq_TeamOid != Guid.Empty)
      {
        query.SetGuid("teamOid", aq_TeamOid.Value);
      }

      if (!(String.IsNullOrEmpty(aq_TeamName)))
        query.SetString("teamName", "%" + aq_TeamName + "%");

      if (aq_StartFrom.HasValue)
        query.SetDateTime("startFrom", aq_StartFrom.Value);

      if (aq_StartTo.HasValue)
        query.SetDateTime("startTo", aq_StartTo.Value);

      if (aq_EndFrom.HasValue)
        query.SetDateTime("endrom", aq_EndFrom.Value);

      if (aq_EndTo.HasValue)
        query.SetDateTime("endTo", aq_EndTo.Value);

      if (aq_ClubCalendarEventTypeOid.HasValue && aq_ClubCalendarEventTypeOid != Guid.Empty)
      {
        query.SetGuid("clubCalendarEventTypeOid", aq_ClubCalendarEventTypeOid.Value);
      }
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
      "select r from Club.Domain.Artifacts.ClubCalendar as r where 1=1 " + Environment.NewLine;

    private readonly string queryStringMandatoryWhere =
      " where 1=1 " + Environment.NewLine;

    private readonly string teamNameString =
      "and r.Team.Name LIKE :teamName " + Environment.NewLine;

    private readonly string startFromString =
      "and r.Start >= :startFrom " + Environment.NewLine;

    private readonly string startToString =
      "and r.Start <= :startTo " + Environment.NewLine;

    private readonly string endFromString =
      "and r.End >= :endFrom " + Environment.NewLine;

    private readonly string endToString =
      "and r.End <= :endTo " + Environment.NewLine;

    private readonly string teamOidString =
      "and r.Team.Oid = :teamOid " + Environment.NewLine;

    private readonly string clubCalendarEventTypeOidString =
      "and r.ClubCalendarEventType.Oid = :clubCalendarEventTypeOid " + Environment.NewLine;

  }
}
