using Club.Domain.Artifacts;
using Mallon.Core.Resources;
using System;
namespace Club.Domain.Models
{
  public class ModelTeamSheet
  {

    public ModelTeamSheet()
    {
    }

    public ModelTeamSheet(ModelTeam team)
    {
      this.MatchDate = DateTime.Now.Date;
      this.TeamOid = team.Oid;
      this.TeamOrev = team.Orev;
      this.TeamName = team.Name;
      this.TeamYear = team.Year;
      this.TeamNameSingleLine = team.NameSingleLine;
    }

    public ModelTeamSheet(Team team)
    {
      this.MatchDate = DateTime.Now.Date;
      this.TeamOid = team.Oid;
      this.TeamOrev = team.Orev;
      this.TeamName = team.Name;
      this.TeamYear = team.Year;
      this.TeamNameSingleLine = team.NameSingleLine;
    }


    public virtual Guid TeamSheetOid { get; set; }

    public virtual DateTime? MatchDate { get; set; }

    [DisplayName("Notes")]
    public virtual string Notes { get; set; }

    [DisplayName("Opponent")]
    public virtual string Opponent { get; set; }

    [DisplayName("Result")]
    public virtual enumResult? Result { get; set; }

    public virtual Guid TeamOid { get; set; }

    public virtual int TeamOrev { get; set; }

    public virtual string TeamName { get; set; }

    public virtual int? TeamYear { get; set; }

    public virtual string TeamNameSingleLine { get; set; }

  }
}
