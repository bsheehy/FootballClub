namespace Club.Domain.Interfaces
{
  interface IAddress
  {
    string AddressAllDetails { get; set; }
    string AddressCountry { get; set; }
    string AddressCounty { get; set; }
    string AddressNumber { get; set; }
    string AddressPostCode { get; set; }
    string AddressSingleLine { get; set; }
    string AddressStreet { get; set; }
    string AddressTown { get; set; }
    //GeoAPI.Geometries.IGeometry the_geom { get; set; }
    double? AddressXlngCoord { get; set; }
    double? AddressYlatCoord { get; set; }
  }
}
