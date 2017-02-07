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
  public class TeamQry : HqlQuery, ISearchQry
  {
    public TeamQry()
      : base(typeof(Team))
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
      if (string.IsNullOrEmpty(this.aq_TeamName) == false)
        return true;


      if (aq_TeamOid.HasValue && aq_TeamOid.Value != Guid.Empty)
        return true;

      if (aq_TeamYearFrom.HasValue && aq_TeamYearFrom > 0)
        return true;

      if (aq_TeamYearTo.HasValue && aq_TeamYearTo > 0)
        return true;

      return false;
    }


    [DisplayName("Team Name")]
    public virtual string aq_TeamName { get; set; }

    [DisplayName("Team")]
    public Guid? aq_TeamOid { get; set; }

    [DisplayName("Team Year From")]
    public int? aq_TeamYearFrom { get; set; }

    [DisplayName("Team Year To")]
    public int? aq_TeamYearTo { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_TeamName)) ? teamNameString : null) +
      (aq_TeamOid.HasValue && aq_TeamOid.Value != Guid.Empty ? teamOidString : null) +
      (aq_TeamYearFrom.HasValue && aq_TeamYearFrom > 0 ? teamYearFromString : null) +
      (aq_TeamYearTo.HasValue && aq_TeamYearTo > 0 ? teamYearToString : null) +
      orderByString;
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_TeamName)))
        query.SetString("teamName", "%" + aq_TeamName + "%");

      if (aq_TeamOid.HasValue && aq_TeamOid.Value != Guid.Empty)
        query.SetGuid("teamOid", aq_TeamOid.Value);

      if (aq_TeamYearFrom.HasValue && aq_TeamYearFrom > 0)
        query.SetInt32("teamYearFrom", aq_TeamYearFrom.Value);

      if (aq_TeamYearTo.HasValue && aq_TeamYearTo > 0)
        query.SetInt32("teamYearTo", aq_TeamYearTo.Value);

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
      "select r from Club.Domain.Artifacts.Team as r where 1=1 " + Environment.NewLine;

    private readonly string teamNameString =
      "and r.Name like :teamName " + Environment.NewLine;

    private readonly string teamYearFromString =
      "and r.Year >= :teamYearFrom " + Environment.NewLine;

    private readonly string teamYearToString =
      "and r.Year <= :teamYearTo " + Environment.NewLine;

    private readonly string teamOidString =
      "and r.Oid = :teamOid " + Environment.NewLine;

    private readonly string orderByString =
      "order by r.Name desc";
  }
}
