﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Club.Domain.Models
{
  public class ModelCommitteeAdmin
  {
    public virtual Guid Oid { get; set; }

    public virtual int Orev { get; set; }

    [Required]
    public virtual Guid CommitteeOid { get; set; }

    public virtual string CommitteeName { get; set; }

    public virtual int? CommitteeYear { get; set; }

    public virtual string CommitteeNameSingleLine { get; set; }

    [Required]
    public virtual Guid UserOid { get; set; }

    [Mallon.Core.Resources.DisplayInfo("User's login name to access system")]
    [Mallon.Core.Resources.DisplayName("Login")]
    public virtual string UserLogin { get; set; }

    [Mallon.Core.Resources.DisplayInfo("The user's full name")]
    [Mallon.Core.Resources.DisplayName("Full Name")]
    public virtual string UserFullName { get; set; }

    [Mallon.Core.Resources.DisplayInfo("The user's email address")]
    [Mallon.Core.Resources.DisplayName("Email")]
    public virtual string UserEmail { get; set; }

    [System.ComponentModel.DefaultValue(false)]
    public virtual bool UserAccountLocked { get; set; }
  }
}
