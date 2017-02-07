using AutoMapper;
using Club.Services.Controllers;
using Club.Services.Models;
using FrRocks.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Mallon.Core.Artifacts;
using Mallon.Core.PersistentSupport;
using Mallon.Documents;
using Mallon.Documents.Artifacts;
using Mallon.Documents.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrRocks.Controllers
{
  public class AttachedDocsController : ControllerBase
  {
    public IFileFactory FileFactory { get; set; }

    public IAttachedDocsController attachDocsController { get; set; }

    public PartialViewResult _AttachedDocuments()
    {
      return PartialView("~/Views/Shared/_AttachedDocuments.cshtml");
    }

    public FileStreamResult AttachDocumentView(Guid attachedDocOid)
    {
      AttachedDocument doc = this.DbSession.Get<AttachedDocument>(attachedDocOid);
      if (doc.File == null)
      {
        this.ModelState.AddModelError("AttachDocumentView", "Unable to retrieve the File for the AttachedDocument using the Oid: " + attachedDocOid.ToString());
        throw new ModelStateException(this.ModelState);
      }

      return File(doc.File.Data, doc.File.MimeType, doc.File.Name);
    }

    public ActionResult AttachDocuments(IEnumerable<HttpPostedFileBase> uplAttachFiles, Guid appliesToOid, string appliesTo)
    {
      try
      {
        // The Name of the Upload component is "uplDocumentFiles"
        if (uplAttachFiles != null && uplAttachFiles.Count() > 0)
        {
          List<IUploadFile> files = new List<IUploadFile>();
          foreach (HttpPostedFileBase file in uplAttachFiles)
          {
            files.Add(new ModelUploadHttpFile(file));
          }

          Type type = PersistentHelper.GetType(appliesTo);
          this.attachDocsController.CreateAttachedDocuments(files, appliesToOid, type);
        }
        // Return an empty string to signify success
        return Content("");
      }
      catch (Exception err)
      {
        return Content(err.Message);
      }
    }

    public JsonResult AttachDocuments_GridRead([DataSourceRequest]DataSourceRequest request, Guid entityOid)
    {
      if (request.PageSize == 0)
      {
        request.PageSize = 50;
      }

      IList<AttachedDocument> attachedDocuments = DbSession.QueryOver<AttachedDocument>().Where(x => x.AppliesToOid == entityOid).List<AttachedDocument>();
      IMapper Mapper = AutoMapperConfig.MapperConfiguration.CreateMapper();
      IEnumerable<ModelAttachedDocument> m = Mapper.Map<IEnumerable<ModelAttachedDocument>>(attachedDocuments);
      return Json(m.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult AttachDocuments_GridDelete([DataSourceRequest] DataSourceRequest request, ModelAttachedDocument entity)
    {
      if (this.UserController.EffectiveUser.GetClassAccess(typeof(AttachedDocument)).Allows(Access.Delete) == false)
      {
        return RedirectToAction("View", "Error", new
        {
          errorMsg = "You do not have permission to 'Delete' the AttachedDocument.",
          redirectUrl = Request.UrlReferrer
        });
      }
      this.attachDocsController.DeleteAttachedDocument(entity.Oid);
      return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
    }

  }
}
