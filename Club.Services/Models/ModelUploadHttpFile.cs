using Mallon.Documents.Interfaces;
using System.IO;
using System.Web;

namespace Club.Services.Models
{
  public class ModelUploadHttpFile : HttpPostedFileBase, IUploadFile
  {
    string _contentType;
    string _filename;
    Stream _inputStream;

    public ModelUploadHttpFile(HttpPostedFileBase file)
    {
      _inputStream = file.InputStream;
      _contentType = file.ContentType;
      _filename = file.FileName;
    }

    public ModelUploadHttpFile(Stream inputStream, string contentType = null, string filename = null)
    {
      _inputStream = inputStream;
      _contentType = contentType;
      _filename = filename;
    }

    public override int ContentLength { get { return (int)_inputStream.Length; } }

    public override string ContentType { get { return _contentType; } }

    /// <summary>
    ///  Summary:
    ///     Gets the fully qualified name of the file on the client.
    ///  Returns:
    ///      The name of the file on the client, which includes the directory path. 
    /// </summary>     
    public override string FileName { get { return _filename; } }

    public override Stream InputStream { get { return _inputStream; } }

    public override void SaveAs(string filename)
    {
      using (var stream = File.OpenWrite(filename))
      {
        InputStream.CopyTo(stream);
      }
    }
  }
}
