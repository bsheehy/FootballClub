using Club.Domain.Artifacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Club.Domain.Models
{
  public class ModelLottoResult
  {
    public ModelLottoResult()
    {
      Winners = new List<ModelLottoResultWinner>();
    }

    public ModelLottoResult(LottoResult lottoResult)
    {
      Winners = new List<ModelLottoResultWinner>();
      this.Oid = lottoResult.Oid;
      this.Orev = lottoResult.Orev;
      this.No1 = lottoResult.No1;
      this.No2 = lottoResult.No2;
      this.No3 = lottoResult.No3;
      this.No4 = lottoResult.No4;

      if (lottoResult.Lotto != null)
      {
        this.LottoOid = lottoResult.Lotto.Oid;
        this.LottoDrawDate = lottoResult.Lotto.DrawDate;
        this.LottoJackpot = lottoResult.Lotto.Jackpot;
        this.LottoMessage = lottoResult.Lotto.Message;
      }
    }

    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [Required]
    public virtual int No1 { get; set; }

    [Required]
    public virtual int No2 { get; set; }

    [Required]
    public virtual int No3 { get; set; }

    [Required]
    public virtual int No4 { get; set; }

    public virtual string WinnersToString
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        foreach (ModelLottoResultWinner winner in this.Winners)
        {
          sb.AppendLine(winner.PersonNameSingleLine);
        }
        return sb.ToString();
      }
    }

    public virtual IList<ModelLottoResultWinner> Winners { get; set; }

    public virtual Guid LottoOid { get; set; }

    public virtual DateTime LottoDrawDate { get; set; }

    public virtual string LottoMessage { get; set; }

    public virtual decimal LottoJackpot { get; set; }
  }
}
