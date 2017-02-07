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
  public class CommitteeMemberQry : HqlQuery, ISearchQry
  {
    public CommitteeMemberQry()
      : base(typeof(CommitteeMember))
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
      if (string.IsNullOrEmpty(this.aq_Forename) == false)
        return true;

      if (string.IsNullOrEmpty(this.aq_Surname) == false)
        return true;

      if (string.IsNullOrEmpty(this.aq_Address) == false)
        return true;

      if (aq_CommitteeOid.HasValue && aq_CommitteeOid.Value != Guid.Empty)
        return true;

      if (aq_CommitteeYear.HasValue && aq_CommitteeYear > 0)
        return true;

      return false;
    }

    /// <summary>
    /// Used for model purposes only - dont search on it.
    /// </summary>
    public virtual Guid? aq_PersonOid { get; set; }

    [DisplayName("Forename")]
    public virtual string aq_Forename { get; set; }

    [DisplayName("Surname")]
    public virtual string aq_Surname { get; set; }

    [DisplayName("Member Type")]
    public Guid? aq_CommitteeMemberTypeOid { get; set; }

    [DisplayName("Committee Name")]
    public Guid? aq_CommitteeOid { get; set; }

    [DisplayName("Address")]
    public string aq_Address { get; set; }

    [DisplayName("Committee Year")]
    public int? aq_CommitteeYear { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_Forename)) ? forenameString : null) +
      (!(String.IsNullOrEmpty(aq_Surname)) ? surnameString : null) +
      (!(String.IsNullOrEmpty(aq_Address)) ? addressString : null) +
      (aq_CommitteeMemberTypeOid.HasValue && aq_CommitteeMemberTypeOid.Value != Guid.Empty ? teamMemberTypeOidString : null) +
      (aq_CommitteeOid.HasValue && aq_CommitteeOid.Value != Guid.Empty ? teamOidString : null) +
      (aq_CommitteeYear.HasValue && aq_CommitteeYear > 0 ? teamYearString : null);
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_Forename)))
        query.SetString("forename", "%" + aq_Forename + "%");

      if (!(String.IsNullOrEmpty(aq_Surname)))
        query.SetString("surname", "%" + aq_Surname + "%");

      if (aq_CommitteeMemberTypeOid.HasValue && aq_CommitteeMemberTypeOid.Value != Guid.Empty)
        query.SetGuid("teamMemberTypeOid", aq_CommitteeMemberTypeOid.Value);

      if (aq_CommitteeOid.HasValue && aq_CommitteeOid.Value != Guid.Empty)
        query.SetGuid("teamOid", aq_CommitteeOid.Value);

      if (aq_CommitteeYear.HasValue && aq_CommitteeYear > 0)
        query.SetInt32("teamYear", aq_CommitteeYear.Value);

      if (!(String.IsNullOrEmpty(aq_Address)))
        query.SetString("address", "%" + aq_Address + "%");
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
      "select r from Club.Domain.Artifacts.CommitteeMember as r where 1=1 " + Environment.NewLine;

    private readonly string forenameString =
      "and r.Person.Forename like :forename " + Environment.NewLine;

    private readonly string surnameString =
      "and r.Person.Surname like :surname " + Environment.NewLine;

    private readonly string teamYearString =
      "and r.Committee.Year = :teamYear " + Environment.NewLine;

    private readonly string teamOidString =
      "and r.Committee.Oid = :teamOid " + Environment.NewLine;

    private readonly string teamMemberTypeOidString =
      "and r.CommitteeMemberType.Oid = :teamMemberTypeOid " + Environment.NewLine;

    private readonly string addressString =
      "and (r.Person.AddressNumber like :address " + Environment.NewLine +
      "or r.Person.AddressStreet like :address " + Environment.NewLine +
      "or r.Person.AddressTown like :address " + Environment.NewLine +
      "or r.Person.AddressPostCode like :address " + Environment.NewLine +
      "or r.Person.AddressCounty like :address) " + Environment.NewLine;

  }
}
