using Mallon.Core.PersistentSupport;
using Mallon.Documents.Artifacts;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Web.Mvc;

namespace FrRocks.Utils
{
  public static class UtilsAttachedDocuments
  {
    public static void SetViewDataAttachedDocuments(ISession session, ViewDataDictionary viewData, Type appliesTo, Guid appliesToOid)
    {
      viewData.Add("IsCustomData", false);
      viewData.Add("IsAttachedDocs", hasAttachedDocuments(session, appliesToOid));
      viewData.Add("AttachedDocsAppliesToOid", appliesToOid.ToString());
      viewData.Add("AttachedDocsAppliesTo", PersistentHelper.GetType(appliesTo.FullName));
    }

    /// <summary>
    /// Returns True if the specific entity has AttachedDocuments
    /// </summary>
    /// <param name="session"></param>
    /// <param name="oid"></param>
    /// <returns></returns>
    public static bool hasAttachedDocuments(ISession session, Guid oid)
    {
      int i = session.QueryOver<AttachedDocument>()
        .Where(x => x.AppliesToOid == oid)
        .Select(Projections.RowCount())
        .FutureValue<int>()
        .Value;

      return (i > 0 ? true : false);
    }
  }
}