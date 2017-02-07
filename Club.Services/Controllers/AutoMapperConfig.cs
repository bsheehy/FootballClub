using AutoMapper;
using Club.Domain;
using Club.Services.Models;
using Mallon.Documents.Artifacts;

namespace Club.Services.Controllers
{
  public static class AutoMapperConfig
  {
    public static MapperConfiguration MapperConfiguration;

    public static void RegisterMappings()
    {
      MapperConfiguration = new MapperConfiguration(cfg =>
      {
        //cfg.CreateMap<Person, ModelPerson>();
        cfg.AddProfile<ClubDomainMappingProfile>();

        //cfg.AddProfile<GuiMappingProfile>();

        cfg.CreateMap<AttachedDocument, ModelAttachedDocument>();
      });
    }
  }
}