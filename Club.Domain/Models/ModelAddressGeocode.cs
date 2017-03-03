using Geocoding.Google;
using Mallon.Core.Resources;
namespace Club.Domain.Models
{
  public class ModelAddressGeocode
  {
    public ModelAddressGeocode()
    {

    }

    public ModelAddressGeocode(GoogleAddress googleAddress)
    {
      bool exitLoop;

      if (googleAddress != null && googleAddress.Components != null)
      {
        this.AddressSingleLine = googleAddress.FormattedAddress;
        foreach (GoogleAddressComponent comp in googleAddress.Components)
        {
          exitLoop = false;
          foreach (GoogleAddressType addressType in comp.Types)
          {
            switch (addressType)
            {
              case GoogleAddressType.StreetNumber:
                this.AddressNumber = comp.LongName;
                exitLoop = true;
                break;
              case GoogleAddressType.Route:
              case GoogleAddressType.StreetAddress:
                this.AddressStreet = comp.LongName;
                exitLoop = true;
                break;
              case GoogleAddressType.PostalTown:
              case GoogleAddressType.AdministrativeAreaLevel2:
                this.AddressTown = comp.LongName;
                exitLoop = true;
                break;
              case GoogleAddressType.AdministrativeAreaLevel1:
              case GoogleAddressType.Country:
                this.AddressCountry = comp.LongName;
                exitLoop = true;
                break;
              case GoogleAddressType.PostalCode:
                this.AddressPostCode = comp.LongName;
                exitLoop = true;
                break;
              default:
                break;
            }
            if (exitLoop) break;
          }
        }
      }

      if (googleAddress != null && googleAddress.Coordinates != null)
      {
        this.AddressYlatCoord = googleAddress.Coordinates.Latitude;
        this.AddressXlngCoord = googleAddress.Coordinates.Longitude;
      }

    }

    [DisplayName("Name or Number")]
    public virtual string AddressNumber { get; set; }

    [DisplayName("Street")]
    public virtual string AddressStreet { get; set; }

    [DisplayName("Town")]
    public virtual string AddressTown { get; set; }

    [DisplayName("County")]
    public virtual string AddressCounty { get; set; }

    [DisplayName("Country")]
    public virtual string AddressCountry { get; set; }

    [DisplayName("Post Code")]
    public virtual string AddressPostCode { get; set; }

    /// <summary>
    /// Stores the X coordinate.
    /// </summary>
    [DisplayName("X Longitude")]
    public virtual double? AddressXlngCoord { get; set; }

    /// <summary>
    /// Stores the Y coordinate.
    /// </summary>
    [DisplayName("Y Latitude")]
    public virtual double? AddressYlatCoord { get; set; }

    [DisplayName("Address")]
    public virtual string AddressSingleLine { get; set; }
  }
}
