using Club.Domain.Artifacts;
using Club.Domain.Interfaces;
using Mallon.Core.Queries;
using Mallon.Core.Resources;
using Newtonsoft.Json;
using NHibernate;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Club.Domain.Queries
{
  public class LottoTicketDirectDebitQry : HqlQuery, ISearchQry
  {
    public LottoTicketDirectDebitQry()
      : base(typeof(LottoTicketDirectDebit))
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

      if (aq_Number1.HasValue && aq_Number1.Value > 0)
        return true;

      if (aq_Number2.HasValue && aq_Number2.Value > 0)
        return true;

      if (aq_Number2.HasValue && aq_Number3.Value > 0)
        return true;

      if (aq_Number2.HasValue && aq_Number4.Value > 0)
        return true;

      return false;
    }

    private IList<int> choosenNumbers = new List<int>();

    [DisplayName("Forename")]
    public virtual string aq_Forename { get; set; }

    [DisplayName("Surname")]
    public virtual string aq_Surname { get; set; }

    [DisplayName("No 1")]
    public virtual int? aq_Number1 { get; set; }

    [DisplayName("No 2")]
    public virtual int? aq_Number2 { get; set; }

    [DisplayName("No 3")]
    public virtual int? aq_Number3 { get; set; }

    [DisplayName("No 4")]
    public virtual int? aq_Number4 { get; set; }

    protected override void PrepareHql()
    {
      Hql = (queryString) +
        (!(String.IsNullOrEmpty(aq_Forename)) ? forenameString : null) +
        (!(String.IsNullOrEmpty(aq_Surname)) ? surnameString : null) +
        ((aq_Number1.HasValue && aq_Number1.Value > 0) ? number1String : null) +
        ((aq_Number2.HasValue && aq_Number2.Value > 0) ? number2String : null) +
        ((aq_Number3.HasValue && aq_Number3.Value > 0) ? number3String : null) +
        ((aq_Number4.HasValue && aq_Number4.Value > 0) ? number4String : null);

      //if (aq_Number1.HasValue && aq_Number1.Value > 0
      //  || aq_Number2.HasValue && aq_Number2.Value > 0
      //  || aq_Number3.HasValue && aq_Number3.Value > 0
      //  || aq_Number4.HasValue && aq_Number4.Value > 0)


      if (aq_Number1.HasValue && aq_Number1.Value > 0)
        choosenNumbers.Add(aq_Number1.Value);

      if (aq_Number2.HasValue && aq_Number2.Value > 0)
        choosenNumbers.Add(aq_Number2.Value);

      if (aq_Number3.HasValue && aq_Number3.Value > 0)
        choosenNumbers.Add(aq_Number3.Value);

      if (aq_Number4.HasValue && aq_Number4.Value > 0)
        choosenNumbers.Add(aq_Number4.Value);
    }

    protected override void PrepareQuery(IQuery query)
    {
      if (!(String.IsNullOrEmpty(aq_Forename)))
        query.SetString("forename", "%" + aq_Forename + "%");

      if (!(String.IsNullOrEmpty(aq_Surname)))
        query.SetString("surname", "%" + aq_Surname + "%");

      if (aq_Number1.HasValue && aq_Number1.Value > 0)
        query.SetInt32("aq_number1", aq_Number1.Value);

      if (aq_Number2.HasValue && aq_Number2.Value > 0)
        query.SetInt32("aq_number2", aq_Number2.Value);

      if (aq_Number3.HasValue && aq_Number3.Value > 0)
        query.SetInt32("aq_number3", aq_Number3.Value);

      if (aq_Number4.HasValue && aq_Number4.Value > 0)
        query.SetInt32("aq_number4", aq_Number4.Value);

      //if (aq_Number1.HasValue && aq_Number1.Value > 0)
      //  query.SetParameterList("aq_number1", choosenNumbers);

      //if (aq_Number2.HasValue && aq_Number2.Value > 0)
      //  query.SetParameterList("aq_number2", choosenNumbers);

      //if (aq_Number3.HasValue && aq_Number3.Value > 0)
      //  query.SetParameterList("aq_number3", choosenNumbers);

      //if (aq_Number4.HasValue && aq_Number4.Value > 0)
      //  query.SetParameterList("aq_number4", choosenNumbers);
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
    "select r from Club.Domain.Artifacts.LottoTicketDirectDebit as r where 1=1 " + Environment.NewLine;

    private readonly string forenameString =
    "and r.Person.Forename like :forename " + Environment.NewLine;

    private readonly string surnameString =
    "and r.Person.Surname like :surname " + Environment.NewLine;

    private readonly string number1String =
    "and (r.No1 = :aq_number1 OR r.No2 = :aq_number1 OR r.No3 = :aq_number1  OR r.No4  = :aq_number1)" + Environment.NewLine;

    private readonly string number2String =
    "and (r.No1 = :aq_number2 OR r.No2 = :aq_number2 OR r.No3 = :aq_number2  OR r.No4  = :aq_number2)" + Environment.NewLine;

    private readonly string number3String =
    "and (r.No1 = :aq_number3 OR r.No2 = :aq_number3 OR r.No3 = :aq_number3  OR r.No4  = :aq_number3)" + Environment.NewLine;

    private readonly string number4String =
    "and (r.No1 = :aq_number4 OR r.No2 = :aq_number4 OR r.No3 = :aq_number4  OR r.No4  = :aq_number4)" + Environment.NewLine;

    //    private readonly string number1String =
    //    "and (r.No1 IN (:aq_number1) OR r.No2 IN (:aq_number1) OR r.No3 IN (:aq_number1)  OR r.No4 IN (:aq_number1))" + Environment.NewLine;

    //    private readonly string number2String =
    //    "and (r.No1 IN (:aq_number2) OR r.No2 IN (:aq_number2) OR r.No3 IN (:aq_number2)  OR r.No4 IN (:aq_number2))" + Environment.NewLine;

    //    private readonly string number3String =
    //    "and (r.No1 IN (:aq_number3) OR r.No2 IN (:aq_number3) OR r.No3 IN (:aq_number3)  OR r.No4 IN (:aq_number3))" + Environment.NewLine;

    //    private readonly string number4String =
    //    "and (r.No1 IN (:aq_number4) OR r.No2 IN (:aq_number4) OR r.No3 IN (:aq_number4)  OR r.No4 IN (:aq_number4))" + Environment.NewLine;

    //    private readonly string number1String =
    //"and (r.No1 IN (:aq_number1))" + Environment.NewLine;

    //    private readonly string number2String =
    //    "and (r.No2 IN (:aq_number2))" + Environment.NewLine;

    //    private readonly string number3String =
    //    "and (r.No3 IN (:aq_number3))" + Environment.NewLine;

    //    private readonly string number4String =
    //    "and (r.No4 IN (:aq_number4))" + Environment.NewLine;


  }
}
