using FrRocks.Extensions;
using System;
using System.Web.Mvc;

namespace FrRocks.Models
{
  /// <summary>
  /// Any Controller action t5hats returns json should wrap the object up in this Serializable class
  /// </summary>
  [Serializable]
  public class ModelJsonResult
  {
    public ModelJsonResult()
    {
    }

    public ModelJsonResult(string error)
    {
      this.error = error;
      this.success = false;
    }

    public ModelJsonResult(object data)
    {
      this.data = data;
      this.success = true;
    }

    public ModelJsonResult(ModelStateException error)
    {
      this.error = error.Message;
      this.success = false;
    }

    public ModelJsonResult(ModelStateDictionary error)
    {
      this.error = error.ToSerializedErrString();
      this.success = false;
    }

    public bool success { get; set; }

    public string url { get; set; }

    public object data { get; set; }

    public string error { get; set; }
  }
}