using AutoMapper;
using Club.Domain;
using Club.Domain.Artifacts;
using Club.Domain.Models;
using Club.Services.Utils;
using Mallon.Core.Interfaces;
using Mallon.Core.Web.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Club.Services.Controllers
{
  public class LottoServiceController
  {
    #region Properties

    public ISession session { get; set; }
    public IUserController userController { get; set; }
    public string sqlConnectionString { get; set; }
    #endregion

    #region Constructor
    public LottoServiceController()
    {
      this.sqlConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mallon.Default"].ConnectionString;
      if (string.IsNullOrEmpty(sqlConnectionString))
        throw new PropertyValueException("Please provide a valid SQL connection string in the app configuration connection string: 'mallon.Default'", "LottoServiceController", "sqlConnectionString");
    }

    #endregion

    #region Lotto Draw

    public bool DeleteLotto(IValidationDictionary validatonDictionary, Guid lottoOid)
    {
      CanUserEditLotto(validatonDictionary);
      if (validatonDictionary.IsValid)
      {
        Lotto lotto = this.session.Get<Lotto>(lottoOid);
        this.session.Delete(lotto);
        this.session.Flush();
        return true;
      }
      return false;
    }

    public bool SaveLotto(IValidationDictionary validatonDictionary, Lotto lotto)
    {
      try
      {
        ValidateLottoDraw(validatonDictionary, lotto);
        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(lotto);

          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }
      return false;
    }

    private void ValidateLottoDraw(IValidationDictionary validatonDictionary, Lotto lotto)
    {
      if (lotto.LottoDrawDate.HasValue == false || lotto.LottoDrawDate == DateTime.MinValue)
      {
        validatonDictionary.AddError("DrawDate", "Please supply a valid draw date.");
      }
      else
      {
        if (this.session.QueryOver<Lotto>().Where(x => x.LottoDrawDate.Value.Date == lotto.LottoDrawDate.Value.Date && x.Oid != lotto.Oid).RowCount() > 0)
        {
          validatonDictionary.AddError("DrawDate", "You can not have multiple Lotto Draws for the same day.");
        }
      }
    }

    #endregion

    #region Lotto Results

    public bool DeleteLottoResult(IValidationDictionary validatonDictionary, Guid lottoResultOid)
    {
      CanUserEditLotto(validatonDictionary);
      if (validatonDictionary.IsValid)
      {
        LottoResult lottoResult = this.session.Get<LottoResult>(lottoResultOid);
        this.session.Delete(lottoResult);
        this.session.Flush();
        return true;
      }
      return false;
    }

    public bool CreateLottoResult(IValidationDictionary validatonDictionary, LottoResult lottoResult)
    {
      ModelLottoTicketResult modelLottoTicketResult = new ModelLottoTicketResult(lottoResult);
      IList<ModelLottoTicketDirectDebit> winners;
      LottoTicketDirectDebitsGetWinners(validatonDictionary, modelLottoTicketResult, out winners);//This method calls lottoResult validate 
      try
      {
        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(lottoResult);

          Lotto lotto = this.session.Get<Lotto>(lottoResult.Lotto.Oid);
          lotto.LottoResult = lottoResult;
          this.session.SaveOrUpdate(lotto);

          foreach (ModelLottoTicketDirectDebit directDebitWinner in winners)
          {
            Person person = this.session.Get<Person>(directDebitWinner.PersonOid);
            LottoResultWinner winner = new LottoResultWinner(lottoResult, directDebitWinner, person);
            this.session.SaveOrUpdate(winner);
          }

          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }
      return false;
    }

    private void ValidateLottoTicketResult(IValidationDictionary validatonDictionary, LottoResult lottoResult)
    {
      if (ValidateLottoNumber(lottoResult.No1) == false)
      {
        validatonDictionary.AddError("No1", "No1 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(lottoResult.No2) == false)
      {
        validatonDictionary.AddError("No2", "No2 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(lottoResult.No3) == false)
      {
        validatonDictionary.AddError("No3", "No3 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(lottoResult.No4) == false)
      {
        validatonDictionary.AddError("No4", "No4 must be a number between 1 and 30.");
      }

      if (lottoResult.No1.Equals(lottoResult.No2) || lottoResult.No1.Equals(lottoResult.No3) || lottoResult.No1.Equals(lottoResult.No4)
        || lottoResult.No2.Equals(lottoResult.No3) || lottoResult.No2.Equals(lottoResult.No4)
        || lottoResult.No3.Equals(lottoResult.No4))
      {
        validatonDictionary.AddError("Duplicate Numbers", "Please choose 4 different numners.");
      }

      //Only 1 Result per draw
      if (this.session.QueryOver<LottoResult>().Where(x => x.Lotto.Oid == lottoResult.Lotto.Oid && x.Oid != lottoResult.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("Lotto", "You can not have multiple Lotto Results for the same Draw.");
      }

    }

    private void ValidateLottoTicketResult(IValidationDictionary validatonDictionary, ModelLottoTicketResult modelLottoTicketResult)
    {
      if (ValidateLottoNumber(modelLottoTicketResult.No1) == false)
      {
        validatonDictionary.AddError("No1", "No1 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(modelLottoTicketResult.No2) == false)
      {
        validatonDictionary.AddError("No2", "No2 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(modelLottoTicketResult.No3) == false)
      {
        validatonDictionary.AddError("No3", "No3 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(modelLottoTicketResult.No4) == false)
      {
        validatonDictionary.AddError("No4", "No4 must be a number between 1 and 30.");
      }

      if (modelLottoTicketResult.No1.Equals(modelLottoTicketResult.No2) || modelLottoTicketResult.No1.Equals(modelLottoTicketResult.No3) || modelLottoTicketResult.No1.Equals(modelLottoTicketResult.No4)
        || modelLottoTicketResult.No2.Equals(modelLottoTicketResult.No3) || modelLottoTicketResult.No2.Equals(modelLottoTicketResult.No4)
        || modelLottoTicketResult.No3.Equals(modelLottoTicketResult.No4))
      {
        validatonDictionary.AddError("Duplicate Numbers", "Please choose 4 different numners.");
      }

      //Only 1 Result per draw
      if (this.session.QueryOver<LottoResult>().Where(x => x.Lotto.Oid == modelLottoTicketResult.LottoOid && x.Oid != modelLottoTicketResult.Oid).RowCount() > 0)
      {
        validatonDictionary.AddError("Lotto", "You can not have multiple Lotto Results for the same Draw.");
      }
    }

    #endregion

    #region Lotto Result Winners

    public bool DeleteLottoResultWinner(IValidationDictionary validatonDictionary, ref LottoResultWinner entity)
    {
      try
      {
        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.Delete(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    //public bool SelectLottoResultWinner(IValidationDictionary validatonDictionary, Guid personOid, Guid lottoResultOid)
    //{
    //  Person person = this.session.Load<Person>(personOid);
    //  if (person == null)
    //  {
    //    validatonDictionary.AddError("Person", string.Format("Cannot locate Person with Id:{0}", personOid.ToString()));
    //  }
    //  LottoResult lottoResult = this.session.Load<LottoResult>(lottoResultOid);
    //  if (lottoResult == null)
    //  {
    //    validatonDictionary.AddError("Lotto", string.Format("Cannot locate LottoResult with Id:{0}", lottoResultOid.ToString()));
    //  }


    //  LottoResultWinner entity = new LottoResultWinner();
    //  entity.Person = person;
    //  entity.LottoResult = lottoResult;
    //  entity.Matches = matches;

    //  return SaveLottoResultWinner(validatonDictionary, ref entity);
    //}

    public bool SaveLottoResultWinner(IValidationDictionary validatonDictionary, ref LottoResultWinner entity)
    {
      try
      {
        ValidateLottoResultWinner(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          //MIGHT NEED TO ADD LottoResultWinner TO Person AND LottoResultWinnerType
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    private void ValidateLottoResultWinner(IValidationDictionary validatonDictionary, LottoResultWinner entity)
    {
      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("PersonOid", "Please specify the Person for this entity.");
      }
      if (entity.LottoResult == null || entity.LottoResult.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("LottoResult", "Please specify the Lotto Result for this entity.");
      }

      if (ValidateLottoNumber(entity.No1) == false)
      {
        validatonDictionary.AddError("No1", "No1 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(entity.No2) == false)
      {
        validatonDictionary.AddError("No2", "No2 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(entity.No3) == false)
      {
        validatonDictionary.AddError("No3", "No3 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(entity.No4) == false)
      {
        validatonDictionary.AddError("No4", "No4 must be a number between 1 and 30.");
      }

      if (entity.No1.Equals(entity.No2) || entity.No1.Equals(entity.No3) || entity.No1.Equals(entity.No4)
        || entity.No2.Equals(entity.No3) || entity.No2.Equals(entity.No4)
        || entity.No3.Equals(entity.No4))
      {
        validatonDictionary.AddError("Duplicate Numbers", "Please choose 4 different numners.");
      }
    }

    public bool CreateLottoResultWinner(IValidationDictionary validatonDictionary, ref LottoResultWinner entity)
    {
      ValidateLottoResultWinner(validatonDictionary, entity);
      try
      {
        if (validatonDictionary.IsValid)
        {
          //If Valid then set matches
          int[] drawResultNumbers = new int[] { entity.LottoResult.No1, entity.LottoResult.No2, entity.LottoResult.No3, entity.LottoResult.No4 };
          int[] winnerNumbers = new int[] { entity.No1, entity.No2, entity.No3, entity.No4 };
          var query = drawResultNumbers.Intersect(winnerNumbers);
          entity.Matches = query.Count();

          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }
      return false;
    }

    public bool CreateLottoResultWinner(IValidationDictionary validatonDictionary, ref TypedModel<LottoResultWinner> entity)
    {
      ValidateLottoResultWinner(validatonDictionary, entity.Entity);
      try
      {
        if (validatonDictionary.IsValid)
        {
          //If Valid then set matches
          int[] drawResultNumbers = new int[] { entity.Entity.LottoResult.No1, entity.Entity.LottoResult.No2, entity.Entity.LottoResult.No3, entity.Entity.LottoResult.No4 };
          int[] winnerNumbers = new int[] { entity.Entity.No1, entity.Entity.No2, entity.Entity.No3, entity.Entity.No4 };
          var query = drawResultNumbers.Intersect(winnerNumbers);
          entity.Entity.Matches = query.Count();

          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity.Entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }
      return false;
    }

    #endregion

    #region Direct Debits
    public IEnumerable<LottoTicketDirectDebit> LottoTicketDirectDebitsGet(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    {
      IEnumerable<LottoTicketDirectDebit> entities = getLottoTicketDirectDebits(validatonDictionary, personOid, isActive);
      return entities;
      //return ViewGridFireCertFee.GetMapping(entities);
    }

    private IEnumerable<LottoTicketDirectDebit> getLottoTicketDirectDebits(IValidationDictionary validatonDictionary, Guid personOid, bool? isActive = null)
    {
      if (isActive.HasValue)
      {
        return this.session.QueryOver<LottoTicketDirectDebit>().List<LottoTicketDirectDebit>().Where(x => x.Person.Oid == personOid && (x.EndDate == null || x.EndDate.Value >= DateTime.Now) && x.Active == isActive.Value);
      }
      else
      {
        return this.session.QueryOver<LottoTicketDirectDebit>().List<LottoTicketDirectDebit>().Where(x => x.Person.Oid == personOid && (x.EndDate == null || x.EndDate.Value >= DateTime.Now));
      }
    }

    public bool LottoTicketDirectDebitSave(IValidationDictionary validatonDictionary, ref LottoTicketDirectDebit entity)
    {
      try
      {
        ValidateLottoTicket(validatonDictionary, entity);

        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    /// <summary>
    /// DirecDebit Lotto tickets are never deleted - they are set to inactive
    /// </summary>
    /// <param name="validatonDictionary"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool LottoTicketDirectDebitDelete(IValidationDictionary validatonDictionary, ref LottoTicketDirectDebit entity)
    {
      try
      {
        if (entity.EndDate.HasValue == false)
        {
          validatonDictionary.AddError("EndDate", "Please provide an EndDate before deleting this entity.");
        }
        if (entity.EndDate.HasValue && entity.EndDate <= entity.StartDate)
        {
          validatonDictionary.AddError("EndDate", "The EndDate must be later than the StartDate.");
        }

        entity.Active = false;
        if (validatonDictionary.IsValid)
        {
          this.session.BeginTransaction();
          this.session.SaveOrUpdate(entity);
          this.session.Transaction.Commit();
          return true;
        }
      }
      catch (Exception)
      {
        if (session.Transaction.IsActive)
        {
          this.session.Transaction.Rollback();
        }
        throw;
      }

      return false;
    }

    public bool LottoTicketDirectDebitsGetWinners(IValidationDictionary validatonDictionary, ModelLottoTicketResult modelLottoTicketResult, out IList<ModelLottoTicketDirectDebit> winners)
    {
      DataTable dt = new DataTable();
      winners = new List<ModelLottoTicketDirectDebit>();
      ValidateLottoTicketResult(validatonDictionary, modelLottoTicketResult);

      if (validatonDictionary.IsValid)
      {
        using (SqlConnection connection = new SqlConnection(this.sqlConnectionString))
        {
          connection.Open();
          using (SqlCommand cmd = new SqlCommand("sp_LottoTicketDirectDebitGetWinners", connection))
          {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@No1", modelLottoTicketResult.No1));
            cmd.Parameters["@No1"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add(new SqlParameter("@No2", modelLottoTicketResult.No2));
            cmd.Parameters["@No2"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add(new SqlParameter("@No3", modelLottoTicketResult.No3));
            cmd.Parameters["@No3"].Direction = ParameterDirection.Input;

            cmd.Parameters.Add(new SqlParameter("@No4", modelLottoTicketResult.No4));
            cmd.Parameters["@No4"].Direction = ParameterDirection.Input;


            using (SqlDataReader dr = cmd.ExecuteReader())
            {
              dt.Load(dr);
            }
          }
        }

        foreach (DataRow dr in dt.Rows)
        {
          Guid oid = (Guid)dr["oid"];
          int resultMatches = (int)dr["TotalMatches"];
          LottoTicketDirectDebit lottoTicketDirectDebit = this.session.Get<LottoTicketDirectDebit>(oid);

          IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
          ModelLottoTicketDirectDebit result = Mapper.Map<LottoTicketDirectDebit, ModelLottoTicketDirectDebit>(lottoTicketDirectDebit, opt =>
            opt.AfterMap((src, dest) => dest.TicketMatches = resultMatches));

          winners.Add(result);
        }
        return true;
      }
      return false;
    }

    private void ValidateLottoTicket(IValidationDictionary validatonDictionary, LottoTicketDirectDebit entity)
    {
      //You must be an Admin to edit or create direct debits
      if (Utility.UserIsAdmin(this.userController) == false)
      {
        validatonDictionary.AddError("User", "You must have Administration priveleges to edit or add Lotto Direct Debits.");
      }

      if (entity.Person == null || entity.Person.Oid == Guid.Empty)
      {
        validatonDictionary.AddError("Person", "Please specify the Person for this entity.");
      }
      if (ValidateLottoNumber(entity.No1) == false)
      {
        validatonDictionary.AddError("No1", "No1 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(entity.No2) == false)
      {
        validatonDictionary.AddError("No2", "No2 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(entity.No3) == false)
      {
        validatonDictionary.AddError("No3", "No3 must be a number between 1 and 30.");
      }
      if (ValidateLottoNumber(entity.No4) == false)
      {
        validatonDictionary.AddError("No4", "No4 must be a number between 1 and 30.");
      }

      if (entity.No1.Equals(entity.No2) || entity.No1.Equals(entity.No3) || entity.No1.Equals(entity.No4)
        || entity.No2.Equals(entity.No3) || entity.No2.Equals(entity.No4)
        || entity.No3.Equals(entity.No4))
      {
        validatonDictionary.AddError("Duplicate Numbers", "Please choose 4 different numners.");
      }


      DateTime minDate = new DateTime(2015, 1, 1);
      if (entity.StartDate < minDate)
      {
        validatonDictionary.AddError("StartDate", "The minimum StartDate is 1 Jan 2015.");
      }

      if (entity.EndDate.HasValue && entity.EndDate <= entity.StartDate)
      {
        validatonDictionary.AddError("EndDate", "The EndDate must be later than the StartDate.");
      }

      if (entity.EndDate.HasValue && entity.EndDate < DateTime.Now)
      {
        entity.Active = false;
      }

    }

    private bool ValidateLottoNumber(int number)
    {
      if (number > 0 && number <= 30)
      {
        return true;
      }
      return false;
    }

    #endregion

    /// <summary>
    /// You must be a member of the Administrator group to Create or Edit Lotto Results
    /// </summary>
    /// <returns></returns>
    public bool CanUserEditLotto(IValidationDictionary validatonDictionary)
    {
      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.AdministrationRoleOids))
      {
        return true;
      }
      validatonDictionary.AddError("Access Permissions", "You must be a member of the Administrator role or group to Create or Edit Lotto Results");
      return false;
    }

    public bool CanUserEditLotto()
    {
      if (userController.LoggedOnUser.HasRole(userController.LoggedOnUser, Constants.AdministrationRoleOids))
      {
        return true;
      }
      return false;
    }

  }
}
