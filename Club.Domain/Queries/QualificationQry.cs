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
  public class QualificationQry : HqlQuery, ISearchQry
  {
    public QualificationQry()
      : base(typeof(Qualification))
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
      if (string.IsNullOrEmpty(this.aq_QualificationName) == false)
        return true;

      if (string.IsNullOrEmpty(this.aq_QualificationDescription) == false)
        return true;

      if (aq_QualificationOid.HasValue && aq_QualificationOid.Value != Guid.Empty)
        return true;


      return false;
    }

    [DisplayName("Qualification Name")]
    public virtual string aq_QualificationName { get; set; }

    [DisplayName("Qualification Description")]
    public virtual string aq_QualificationDescription { get; set; }

    [DisplayName("Qualification")]
    public Guid? aq_QualificationOid { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_QualificationName)) ? qualificationNameString : null) +
      (aq_QualificationOid.HasValue && aq_QualificationOid.Value != Guid.Empty ? qualificationOidString : null) +
      (!(String.IsNullOrEmpty(aq_QualificationDescription)) ? aq_QualificationDescription : null) +
      orderByString;
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_QualificationName)))
        query.SetString("qualificationName", "%" + aq_QualificationName + "%");

      if (!(String.IsNullOrEmpty(aq_QualificationDescription)))
        query.SetString("qualificationDescription", "%" + aq_QualificationDescription + "%");

      if (aq_QualificationOid.HasValue && aq_QualificationOid.Value != Guid.Empty)
        query.SetGuid("qualificationOid", aq_QualificationOid.Value);


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
      "select r from Club.Domain.Artifacts.Qualification as r where 1=1 " + Environment.NewLine;

    private readonly string qualificationNameString =
      "and r.Name like :qualificationName " + Environment.NewLine;

    private readonly string qualificationDescriptionString =
      "and r.Description like :qualificationDescription " + Environment.NewLine;

    private readonly string qualificationOidString =
      "and r.Oid = :qualificationOid " + Environment.NewLine;

    private readonly string orderByString =
      "order by r.Name desc";
  }
}
