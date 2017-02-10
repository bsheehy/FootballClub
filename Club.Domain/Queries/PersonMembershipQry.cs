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
  public class PersonMembershipQry : HqlQuery, ISearchQry
  {
    public PersonMembershipQry()
      : base(typeof(PersonMembershipType))
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

      if (this.aq_MembershipOid.HasValue && this.aq_MembershipOid.Value != Guid.Empty)
        return true;

      if (this.aq_YearFrom.HasValue)
        return true;

      if (this.aq_YearTo.HasValue)
        return true;

      if (this.aq_StartDateFrom.HasValue)
        return true;

      if (this.aq_StartDateTo.HasValue)
        return true;

      if (this.aq_EndDateFrom.HasValue)
        return true;

      if (this.aq_EndDateTo.HasValue)
        return true;

      return false;
    }

    [DisplayName("Membership Type")]
    public virtual Guid? aq_MembershipOid { get; set; }

    [DisplayName("Forename")]
    public virtual string aq_Forename { get; set; }

    [DisplayName("Surname")]
    public virtual string aq_Surname { get; set; }

    [DisplayName("Start Date From")]
    public DateTime? aq_StartDateFrom { get; set; }

    [DisplayName("Start Date To")]
    public DateTime? aq_StartDateTo { get; set; }

    [DisplayName("End Date From")]
    public DateTime? aq_EndDateFrom { get; set; }

    [DisplayName("End Date To")]
    public DateTime? aq_EndDateTo { get; set; }

    [DisplayName("Year From")]
    public int? aq_YearFrom { get; set; }

    [DisplayName("Year To")]
    public int? aq_YearTo { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_Forename)) ? forenameString : null) +
      (!(String.IsNullOrEmpty(aq_Surname)) ? surnameString : null) +
      (aq_MembershipOid.HasValue ? membershipOidString : null) +
      (aq_StartDateFrom.HasValue ? startDateFromString : null) +
      (aq_YearFrom.HasValue ? startDateToString : null) +
      (aq_YearTo.HasValue ? yearToString : null) +
      (aq_StartDateTo.HasValue ? yearFromString : null) +
      (aq_EndDateFrom.HasValue ? endDateFromString : null) +
      (aq_EndDateTo.HasValue ? endDateToString : null);
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (aq_MembershipOid.HasValue && aq_MembershipOid.Value != Guid.Empty)
        query.SetGuid("membershipOid", aq_MembershipOid.Value);

      if (!(String.IsNullOrEmpty(aq_Forename)))
        query.SetString("forename", "%" + aq_Forename + "%");

      if (!(String.IsNullOrEmpty(aq_Surname)))
        query.SetString("surname", "%" + aq_Surname + "%");

      if (aq_StartDateFrom.HasValue)
        query.SetDateTime("startDateFrom", aq_StartDateFrom.Value);

      if (aq_StartDateTo.HasValue)
        query.SetDateTime("startDateTo", aq_StartDateTo.Value);

      if (aq_YearFrom.HasValue)
        query.SetInt32("yearFrom", aq_YearFrom.Value);

      if (aq_YearTo.HasValue)
        query.SetInt32("yearTo", aq_YearTo.Value);

      if (aq_EndDateFrom.HasValue)
        query.SetDateTime("endDateFrom", aq_EndDateFrom.Value);

      if (aq_EndDateTo.HasValue)
        query.SetDateTime("endDateTo", aq_EndDateTo.Value);
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

    //private readonly string queryString =
    //  "select r from Club.Domain.Artifacts.PersonMembershipType as r INNER JOIN r.Person as pPerson INNER JOIN r.Membership as pMembership where 1=1 " + Environment.NewLine;

    private readonly string queryString =
      "select r from Club.Domain.Artifacts.PersonMembershipType as r where 1=1 " + Environment.NewLine;

    private readonly string membershipOidString =
      "and r.MembershipType.Oid = :membershipOid " + Environment.NewLine;

    private readonly string forenameString =
      "and r.Person.Forename like :forename " + Environment.NewLine;

    private readonly string surnameString =
      "and r.Person.Surname like :surname " + Environment.NewLine;

    private readonly string startDateFromString =
      "and r.MembershipType.StartDate > :startDateFrom" + Environment.NewLine;

    private readonly string startDateToString =
      "and r.MembershipType.StartDate < :startDateTo" + Environment.NewLine;

    private readonly string endDateFromString =
      "and r.MembershipType.EndDate > :endDateFrom" + Environment.NewLine;

    private readonly string endDateToString =
      "and r.MembershipType.EndDate < :endDateTo" + Environment.NewLine;

    private readonly string yearFromString =
      "and r.MembershipType.Year >= :yearFrom" + Environment.NewLine;

    private readonly string yearToString =
      "and r.MembershipType.Year <= :yearTo" + Environment.NewLine;

  }
}
