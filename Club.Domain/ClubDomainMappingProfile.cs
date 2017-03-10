using AutoMapper;
using Club.Domain.Artifacts;
using Club.Domain.Models;
using Mallon.Core.Artifacts;

namespace Club.Domain
{
  public class ClubDomainMappingProfile : Profile
  {
    public ClubDomainMappingProfile()
    {
      CreateMap<Person, ModelPerson>()
        .AfterMap((source, dest) =>
        {
          if (source.LottoTicketDirectDebits.Count > 0)
          {
            dest.HasDirectDebits = true;
          }
        });

      CreateMap<ModelPerson, Person>();

      CreateMap<LottoTicketDirectDebit, ModelLottoTicketDirectDebit>().ReverseMap();

      CreateMap<ModelPersonMembershipType, PersonMembershipType>();

      CreateMap<PersonMembershipType, ModelPersonMembershipType>();

      CreateMap<PersonGuardian, ModelPersonGuardian>();

      CreateMap<Team, ModelTeam>();

      CreateMap<TeamMember, ModelTeamMember>().ReverseMap();

      CreateMap<Qualification, ModelQualification>().ReverseMap();

      CreateMap<PersonQualification, ModelPersonQualification>().ReverseMap();

      CreateMap<User, ModelUser>().ReverseMap();

      CreateMap<TeamAdmin, ModelTeamAdmin>().ReverseMap();

      CreateMap<Role, ModelRole>().ReverseMap();

      CreateMap<Role, ModelRoles>().ReverseMap();

      CreateMap<User, ModelUser>()
        .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.Password));
      //CreateMap<ModelTeamMember, TeamMember>();

      //CreateMap<LottoTicketDirectDebit, ModelLottoTicketDirectDebit>()
      //  .ForMember(dest => dest.PersonAddress, opt => opt.MapFrom(src => src.Person.AddressSingleLine));

      CreateMap<Committee, ModelCommittee>();

      CreateMap<CommitteeMember, ModelCommitteeMember>().ReverseMap();

      CreateMap<CommitteeMinute, ModelCommitteeMinute>();

      CreateMap<CommitteeAdmin, ModelCommitteeAdmin>().ReverseMap();

      CreateMap<LottoResult, ModelLottoResult>().ReverseMap();

      CreateMap<LottoResultWinner, ModelLottoResultWinner>().ReverseMap();

      CreateMap<TeamSheet, ModelTeamSheet>()
        .ForMember(dest => dest.TeamSheetOid, opt => opt.MapFrom(src => src.Oid))
        .ReverseMap();

      CreateMap<TeamSheetPerson, ModelTeamSheetPerson>().ReverseMap();
    }
  }
}
