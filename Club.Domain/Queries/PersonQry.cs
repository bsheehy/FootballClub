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
  public class PersonQry : HqlQuery, ISearchQry
  {
    public PersonQry()
      : base(typeof(Person))
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

      if (string.IsNullOrEmpty(this.aq_Number) == false)
        return true;

      if (string.IsNullOrEmpty(this.aq_Postcode) == false)
        return true;

      if (aq_DobFrom.HasValue)
        return true;

      if (aq_DobTo.HasValue)
        return true;

      return false;
    }

    /// <summary>
    /// Used for model purposes only - dont search on it.
    /// </summary>
    public virtual Guid? aq_PersonOid { get; set; }

    /// <summary>
    /// Used for model purposes only - dont search on it.
    /// </summary>
    public virtual Guid? aq_TeamOid { get; set; }

    /// <summary>
    /// Used for model purposes only - dont search on it.
    /// </summary>
    public virtual Guid? aq_CommitteeOid { get; set; }

    /// <summary>
    /// Used for model purposes only - dont search on it.
    /// </summary>
    public virtual Guid? aq_MembershipTypeOid { get; set; }


    [DisplayName("Forename")]
    public virtual string aq_Forename { get; set; }

    [DisplayName("Surname")]
    public virtual string aq_Surname { get; set; }

    [DisplayName("House Number")]
    public string aq_Number { get; set; }

    [DisplayName("Street")]
    public string aq_Street { get; set; }

    [DisplayName("Address")]
    public string aq_Address { get; set; }

    [DisplayName("PostCode")]
    public string aq_Postcode { get; set; }

    [DisplayName("Sex")]
    public virtual enumSex? aq_Sex { get; set; }

    [DisplayName("Dob From")]
    public DateTime? aq_DobFrom { get; set; }

    [DisplayName("Dob To")]
    public DateTime? aq_DobTo { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_Forename)) ? forenameString : null) +
      (!(String.IsNullOrEmpty(aq_Surname)) ? surnameString : null) +
      (!(String.IsNullOrEmpty(aq_Address)) ? addressString : null) +
      (!(String.IsNullOrEmpty(aq_Number)) ? numberString : null) +
      (!(String.IsNullOrEmpty(aq_Street)) ? streetString : null) +
      (aq_DobFrom.HasValue ? ageFromString : null) +
      (aq_DobTo.HasValue ? ageToString : null) +
      (!(String.IsNullOrEmpty(aq_Postcode)) ? postcodeString : null);
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_Forename)))
        query.SetString("forename", "%" + aq_Forename + "%");

      if (!(String.IsNullOrEmpty(aq_Surname)))
        query.SetString("surname", "%" + aq_Surname + "%");

      if (!(String.IsNullOrEmpty(aq_Number)))
        query.SetString("aq_number", "%" + aq_Number + "%");

      if (!(String.IsNullOrEmpty(aq_Street)))
        query.SetString("aq_street", "%" + aq_Street + "%");

      if (!(String.IsNullOrEmpty(aq_Postcode)))
        query.SetString("aq_postcode", aq_Postcode + "%");

      if (!(String.IsNullOrEmpty(aq_Address)))
        query.SetString("aq_address", "%" + aq_Address + "%");

      if (aq_DobFrom.HasValue)
        query.SetDateTime("teamDobFrom", aq_DobFrom.Value);

      if (aq_DobTo.HasValue)
        query.SetDateTime("teamDobTo", aq_DobTo.Value);
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
      "select r from Club.Domain.Artifacts.Person as r where 1=1 " + Environment.NewLine;

    private readonly string forenameString =
      "and r.Forename like :forename " + Environment.NewLine;

    private readonly string surnameString =
      "and r.Surname like :surname " + Environment.NewLine;

    private readonly string numberString =
      "and r.AddressNumber like :aq_street " + Environment.NewLine;

    private readonly string postcodeString =
      "and r.AddressPostCode like :aq_postcode " + Environment.NewLine;

    private readonly string streetString =
      "and r.AddressStreet like :aq_street " + Environment.NewLine;

    private readonly string ageFromString =
  "and r.Dob >= :teamDobFrom " + Environment.NewLine;

    private readonly string ageToString =
      "and r.Dob <= :teamDobTo " + Environment.NewLine;

    private readonly string addressString =
      "and (r.AddressNumber like :aq_address " + Environment.NewLine +
      "or r.AddressStreet like :aq_address " + Environment.NewLine +
      "or r.AddressTown like :aq_address " + Environment.NewLine +
      "or r.AddressCounty like :aq_address) " + Environment.NewLine;

  }
}
