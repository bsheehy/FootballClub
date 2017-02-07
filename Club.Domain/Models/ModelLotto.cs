using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Club.Domain.Models
{
  public class ModelLotto
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [DisplayName("Draw Date")]
    [Required]
    public virtual DateTime DrawDate { get; set; }

    public virtual ModelLottoResult LottoResult { get; set; }

    public virtual string Message { get; set; }

    [Required]
    public virtual decimal Jackpot { get; set; }
  }
}
