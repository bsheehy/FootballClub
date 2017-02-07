
using AutoMapper;
using Club.Domain.Artifacts;
using Club.Domain.Models;
namespace Club.Domain.Extensions
{
  public static class MappingAutoConfig
  {
    public static ModelPerson MapToModel(this Person person)
    {
      Mapper.Initialize(cfg =>
        cfg.CreateMap<Person, ModelPerson>());

      ModelPerson result = Mapper.Map<Person, ModelPerson>(person);
      return result;
      //Mapper.Initialize(cfg =>
      //cfg.CreateMap<Person, ModelPerson>()
      //  .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.Date.Date))
      //  .ForMember(dest => dest.EventHour, opt => opt.MapFrom(src => src.Date.Hour))
      //  .ForMember(dest => dest.EventMinute, opt => opt.MapFrom(src => src.Date.Minute)));
    }
  }
}
