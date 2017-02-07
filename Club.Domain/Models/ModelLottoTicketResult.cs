using Club.Domain.Artifacts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelLottoTicketResult
  {
    public ModelLottoTicketResult()
    {
      Winners = new List<ModelLottoTicketDirectDebit>();
    }

    public ModelLottoTicketResult(LottoResult lottoResult)
    {
      Winners = new List<ModelLottoTicketDirectDebit>();
      this.No1 = lottoResult.No1;
      this.No2 = lottoResult.No2;
      this.No3 = lottoResult.No3;
      this.No4 = lottoResult.No4;
    }


    [Required]
    public virtual int No1 { get; set; }

    [Required]
    public virtual int No2 { get; set; }

    [Required]
    public virtual int No3 { get; set; }

    [Required]
    public virtual int No4 { get; set; }

    public virtual IList<ModelLottoTicketDirectDebit> Winners { get; set; }
  }
}
