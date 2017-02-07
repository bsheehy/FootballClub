using Club.Domain.Interfaces;
using Mallon.Core.Artifacts;
using Mallon.Core.Queries;
using Mallon.Core.Resources;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections;

namespace Club.Domain.Queries
{
  public class UserQry : HqlQuery, ISearchQry
  {
    public UserQry()
      : base(typeof(User))
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
      if (string.IsNullOrEmpty(this.aq_Login) == false)
        return true;

      if (string.IsNullOrEmpty(this.aq_Email) == false)
        return true;

      if (string.IsNullOrEmpty(this.aq_FullName) == false)
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



    [DisplayName("Login")]
    public virtual string aq_Login { get; set; }

    [DisplayName("Email")]
    public virtual string aq_Email { get; set; }

    [DisplayName("House Number")]
    public string aq_FullName { get; set; }


    protected override void PrepareHql()
    {
      Hql = (queryString) +
      (!(String.IsNullOrEmpty(aq_Login)) ? loginString : null) +
      (!(String.IsNullOrEmpty(aq_Email)) ? emailString : null) +
      (!(String.IsNullOrEmpty(aq_FullName)) ? fullNameString : null);
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_Login)))
        query.SetString("login", "%" + aq_Login + "%");

      if (!(String.IsNullOrEmpty(aq_Email)))
        query.SetString("email", "%" + aq_Email + "%");

      if (!(String.IsNullOrEmpty(aq_FullName)))
        query.SetString("fullName", "%" + aq_FullName + "%");

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
      "select r from Mallon.Core.Artifacts.User as r where 1=1 " + Environment.NewLine;

    private readonly string loginString =
      "and r.Login like :login " + Environment.NewLine;

    private readonly string emailString =
      "and r.Email like :email " + Environment.NewLine;

    private readonly string fullNameString =
      "and r.FullName like :fullName " + Environment.NewLine;
  }
}
