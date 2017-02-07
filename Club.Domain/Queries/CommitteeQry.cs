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
  public class CommitteeQry : HqlQuery, ISearchQry
  {
    public CommitteeQry()
      : base(typeof(Committee))
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
      if (string.IsNullOrEmpty(this.aq_CommitteeName) == false)
        return true;


      if (aq_CommitteeOid.HasValue && aq_CommitteeOid.Value != Guid.Empty)
        return true;

      if (aq_CommitteeYearFrom.HasValue && aq_CommitteeYearFrom > 0)
        return true;

      if (aq_CommitteeYearTo.HasValue && aq_CommitteeYearTo > 0)
        return true;

      return false;
    }


    [DisplayName("Committee Name")]
    public virtual string aq_CommitteeName { get; set; }

    [DisplayName("Committee")]
    public Guid? aq_CommitteeOid { get; set; }

    [DisplayName("Committee Year From")]
    public int? aq_CommitteeYearFrom { get; set; }

    [DisplayName("Committee Year To")]
    public int? aq_CommitteeYearTo { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_CommitteeName)) ? teamNameString : null) +
      (aq_CommitteeOid.HasValue && aq_CommitteeOid.Value != Guid.Empty ? teamOidString : null) +
      (aq_CommitteeYearFrom.HasValue && aq_CommitteeYearFrom > 0 ? teamYearFromString : null) +
      (aq_CommitteeYearTo.HasValue && aq_CommitteeYearTo > 0 ? teamYearToString : null) +
      orderByString;
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_CommitteeName)))
        query.SetString("teamName", "%" + aq_CommitteeName + "%");

      if (aq_CommitteeOid.HasValue && aq_CommitteeOid.Value != Guid.Empty)
        query.SetGuid("teamOid", aq_CommitteeOid.Value);

      if (aq_CommitteeYearFrom.HasValue && aq_CommitteeYearFrom > 0)
        query.SetInt32("teamYearFrom", aq_CommitteeYearFrom.Value);

      if (aq_CommitteeYearTo.HasValue && aq_CommitteeYearTo > 0)
        query.SetInt32("teamYearTo", aq_CommitteeYearTo.Value);

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
      "select r from Club.Domain.Artifacts.Committee as r where 1=1 " + Environment.NewLine;

    private readonly string teamNameString =
      "and r.Name like :teamName " + Environment.NewLine;

    private readonly string teamYearFromString =
      "and r.Committee.Year >= :teamYearFrom " + Environment.NewLine;

    private readonly string teamYearToString =
      "and r.Committee.Year <= :teamYearTo " + Environment.NewLine;

    private readonly string teamOidString =
      "and r.Oid = :teamOid " + Environment.NewLine;

    private readonly string orderByString =
      "order by r.Name desc";
  }
}
