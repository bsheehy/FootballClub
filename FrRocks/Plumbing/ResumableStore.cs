using System.Collections.Generic;
using System.Web;
using Mallon.Core.Interfaces;

namespace DiamondFireWeb.Gui.Plumbing
{
  /// <summary>
  /// Implements helper methods to store and retrive resume state for resumable
  /// controllers using the session state.
  /// </summary>
  public static class ResumableStore
  {
    /// <summary>
    /// List of IResumable instances backed by the current HttpContext.
    /// </summary>
    /// <remarks>
    /// This is really a helper method to access a list of IResumable instances
    /// throught a web request. A new list is allocated and stored in the current
    /// HttpContext the first time this method is called during a web request
    /// and is reused until the end of the web request.
    /// </remarks>
    public static IList<IResumable> Instances
    {
      get
      {
        IList<IResumable> result = (IList<IResumable>)HttpContext.Current.Items[typeof(ResumableStore)];
        if (result == null)
        {
          result = new List<IResumable>();
          HttpContext.Current.Items[typeof(ResumableStore)] = result;
        }

        return result;
      }
    }

    /// <summary>
    /// Helper method to get the resume state from the current session and restore
    /// it to the specified IResumable instance.
    /// </summary>
    /// <param name="instance">New IResumable to call <see cref="IResumable.Resume"/> on.</param>
    public static void Resume(IResumable instance)
    {
      // Get the resume state for the object
      object resumeState = HttpContext.Current.Session[instance.GetType().FullName];
      // Restore the state
      ((IResumable)instance).Resume(resumeState);
    }

    /// <summary>
    /// Helper method to get the resume from an IResumable instance and store it in the
    /// current session.
    /// </summary>
    /// <param name="instance">IResumable instance to call <see cref="IResumable.Pause"/> on.</param>
    public static void Pause(IResumable instance)
    {
      // Get the resume state for the object
      object resumeState = ((IResumable)instance).Pause();
      // Store it in the session
      HttpContext.Current.Session[instance.GetType().FullName] = resumeState;
    }


  }
}