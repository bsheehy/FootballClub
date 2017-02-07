using FrRocks.Extensions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Web.Mvc;

namespace FrRocks.Models
{
  /// <summary>
  /// This exception that is thrown when there are model state errors.
  /// http://erraticdev.blogspot.co.uk/2010/11/handling-validation-errors-on-ajax.html
  /// </summary>
  [Serializable]
  public class ModelStateException : Exception
  {
    /// <summary> 
    /// Gets the model errors.
    /// </summary>
    /// 
    //public List<KeyValuePair<string, string[]>> Errors { get; private set; }
    public Dictionary<string, string[]> Errors { get; private set; }

    /// <summary>
    /// If this is set then the Error handler will attempt to redirect to this Url
    /// </summary>
    public string RedirectUrl { get; set; }

    public JsonResult JsonResult { get; set; }

    public bool IsPartialView { get; set; }

    public bool DisplayKeyInError { get; set; }

    /// <summary>
    /// Gets a message that describes the current exception and is related to the first model state error.
    /// </summary>
    /// <value></value>
    public override string Message
    {
      get
      {
        StringBuilder sb = new StringBuilder();
        foreach (string error in this.Errors.Keys)
        {
          sb.AppendLine(error + ": " + this.Errors[error]);
        }
        return sb.ToString();
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelStateException"/> class.
    /// </summary>
    public ModelStateException()
      : base()
    {
      this.Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelStateException"/> class.
    /// </summary>
    /// <param name="jsonResult"></param>
    /// <param name="displayKeyInError">If True then Key (control name) will be shown in the browser Error message.</param>
    /// <param name="isPartialView">If True then this Ajax error came from a PartialView</param>
    public ModelStateException(JsonResult jsonResult, bool displayKeyInError = true, bool isPartialView = false)
      : this()
    {
      this.IsPartialView = isPartialView;
      this.DisplayKeyInError = displayKeyInError;
      this.JsonResult = jsonResult;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelStateException"/> class.
    /// </summary>
    /// <param name="modelState"></param>
    /// <param name="displayKeyInError">If True then Key (control name) will be shown in the browser Error message.</param>
    /// <param name="isPartialView">If True then this Ajax error came from a PartialView</param>
    public ModelStateException(ModelStateDictionary modelState, bool displayKeyInError = true, bool isPartialView = false)
      : this()
    {
      if (modelState == null)
      {
        throw new ArgumentNullException("modelState");
      }
      this.IsPartialView = isPartialView;
      this.DisplayKeyInError = displayKeyInError;

      //this.ModelState = modelState;
      if (!modelState.IsValid)
      {
        this.Errors = modelState.GetErrorDictionary();
        //StringBuilder errors;
        //foreach (KeyValuePair<string, ModelState> state in modelState)
        //{
        //  if (state.Value.Errors.Count > 0)
        //  {
        //    errors = new StringBuilder();
        //    foreach (ModelError err in state.Value.Errors)
        //    {
        //      string myerr = err.ErrorMessage.Trim();
        //      if (myerr.EndsWith(".") == false)
        //      {
        //        myerr = string.Concat(myerr, ". ");
        //      }
        //      else
        //      {
        //        myerr = string.Concat(myerr, " ");
        //      }
        //      errors.Append(myerr);
        //    }
        //    this.Errors.Add(state.Key, errors.ToString());
        //  }
        //}
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelStateException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <param name="displayKeyInError">If True then Key (control name) will be shown in the browser Error message.</param>
    /// <param name="isPartialView">If True then this Ajax error came from a PartialView</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The <paramref name="info"/> parameter is null.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
    /// </exception>
    protected ModelStateException(SerializationInfo info, StreamingContext context, bool displayKeyInError = true, bool isPartialView = false)
      : base(info, context)
    {
      if (info == null)
      {
        throw new ArgumentNullException("info");
      }

      this.IsPartialView = isPartialView;
      this.DisplayKeyInError = displayKeyInError;
      // deserialize
      this.Errors = info.GetValue("ModelStateException.Errors", typeof(Dictionary<string, string[]>)) as Dictionary<string, string[]>;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelStateException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="displayKeyInError">If True then Key (control name) will be shown in the browser Error message.</param>
    /// <param name="isPartialView">If True then this Ajax error came from a PartialView</param>
    public ModelStateException(string message, bool displayKeyInError = true, bool isPartialView = false)
      : base(message)
    {
      this.IsPartialView = isPartialView;
      this.DisplayKeyInError = displayKeyInError;
      this.Errors = new Dictionary<string, string[]>();
      this.Errors.Add(string.Empty, new string[] { message });
      //this.Errors.Add(new KeyValuePair<string, string[]>(string.Empty, new string[] { message }));
    }


    /// <summary>
    /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
    /// </exception>
    /// <PermissionSet>
    ///     <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
    ///     <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
    /// </PermissionSet>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
      {
        throw new ArgumentNullException("info");
      }
      // serialize errors
      info.AddValue("ModelStateException.Errors", this.Errors, typeof(Dictionary<string, string>));
      base.GetObjectData(info, context);
    }

  }
}