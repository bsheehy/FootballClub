
namespace Club.Domain.Interfaces
{
  public interface ISearchQry
  {
    string JsonSerializeObject();

    T JsonDeserializeString<T>(string jsonString) where T : ISearchQry;

    int PageSize { get; set; }

    int Page { get; set; }

    int MaxRecords { get; set; }

    bool HasSearchCriteria();
  }
}
