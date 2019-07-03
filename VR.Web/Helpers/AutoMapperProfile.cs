using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using VR.Data.Model;
using VR.Dto;
using VR.Dto.User;
using VR.Service.Helpers;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, SaveUserDto>();
            CreateMap<SaveUserDto, User>();
            CreateMap<SolicitationSubsidy, AllSolicitationSubsidyDto>()
                .ForMember(
                    x => x.State,
                    opt => opt.MapFrom(x => x.SolicitationStates.OrderByDescending(y => y.ChangeDate)
                        .FirstOrDefault().State.Description)
                )
                .ForMember(
                    x => x.MotiveReject,
                    opt => opt.MapFrom(x => x.SolicitationStates.OrderByDescending(y => y.ChangeDate)
                        .FirstOrDefault().MotiveReject)
                ).ForMember(x => x.FileNumber,
                    opt => opt.MapFrom(j => j.SolicitationStates.OrderByDescending(y => y.ChangeDate)
                        .FirstOrDefault().FileNumber)
                );
            CreateMap<AgentSolicitationBySupervisorResult, AllSolicitationSubsidyDto>();
            CreateMap<Expenditure, ExpenditureFromSolicitationSubsidyByIdDto>()
                .ForMember(x => x.SupportingDate, opt=> opt.MapFrom(c => new DateDto()
                {
                    Day = c.SupportingDate.Day,
                    Month = c.SupportingDate.Month,
                    Year = c.SupportingDate.Year
                }) )
                .ForMember(x=> x.SupportingDateTime, opt => opt.MapFrom(v => v.SupportingDate))
                .ForMember(x => x.UrlImages, 
                    opt => opt.MapFrom(v => 
                        v.Images
                            .Select(j => "data:" + j.MimeType + ";base64," + Convert.ToBase64String(j.Image))
                        .ToList() 
                    ));

            CreateMap<Expenditure, ExpenditureFromOnlySolicitationSubsidyByIdDto>()
                .ForMember(x => x.UrlImages,
                    opt => opt.MapFrom(v =>
                        v.Images
                            .Select(j => "data:" + j.MimeType + ";base64," + Convert.ToBase64String(j.Image))
                            .ToList()
                    ));
            CreateMap<User, UserDto>();
            CreateMap<SupplementaryCity, SupplementaryCityDto>();

            CreateMap<SupplementaryCity, SupplementaryCityOnlyId>()
                .ForMember(x => x.Name,
                    opt => opt.MapFrom(q => q.City.Name)
                    );

            CreateMap<Destiny, DestinyFromSolicitationSubsidyByIdDto>()
                .ForMember(
                    x => x.SupplementaryCities,
                    opt => opt.MapFrom(y => y.SupplementaryCities));

            CreateMap<SupervisorUserAgent, AllSupervisorAgentDto>();
            CreateMap<User, AllUserDto>();
            CreateMap<State, StateDto>();
            CreateMap<State, StateDescriptionDto>();
            CreateMap<SolicitationState, SolicitationStateDto>();
            CreateMap<Distribution, DistributionBaseDto>();
            CreateMap<Distribution, AllDistributionDto>();
            CreateMap<Distribution, FindByIdDistributionDto>();
            CreateMap<Distribution, DeleteDistributionDto>();
            CreateMap<Organism, OrganismBaseDto>();
            CreateMap<Organism, GetallOrganismDto>();
            CreateMap<Organism, FindByIdOrganismDto>();
            CreateMap<Organism, DeleteOrganismDto>();
            CreateMap<Destiny, DestinyBaseDto>()
                .ForMember(x => x.TimeStartDate, opt => opt.MapFrom(c => new TimeDto()
                {
                    Hour = c.StartDate.Hour,
                    Minute = c.StartDate.Minute
                }))
                .ForMember(x => x.TimeEndDate, opt => opt.MapFrom(c => new TimeDto()
                {
                    Hour = c.EndDate.Hour,
                    Minute = c.EndDate.Minute
                }));

            CreateMap<Destiny, DestinyFromSolicitationSubsidyFindByIdDto>()
                .ForMember(x=> x.StartDateToPrint, opt=> opt.MapFrom(m => m.StartDate))
                .ForMember(x => x.EndDateToPrint, opt => opt.MapFrom(m => m.EndDate))
                .ForMember(x=> x.DestinationStartDateToString, opt => opt.MapFrom(q =>
                
                    q.StartDate.Hour.ToString("00") + ":" +q.StartDate.Minute.ToString("00")
                ))
                .ForMember(x => x.DestinationEndDateToString, opt => opt.MapFrom(q =>
                
                    q.EndDate.Hour.ToString("00") + ":" + q.EndDate.Minute.ToString("00")
                ))
                .ForMember(x => x.TimeStartDate, opt => opt.MapFrom(c => new TimeDto()
                {
                    Hour = c.StartDate.Hour,
                    Minute = c.StartDate.Minute
                }))
                .ForMember(x => x.TimeEndDate, opt => opt.MapFrom(c => new TimeDto()
                {
                    Hour = c.EndDate.Hour,
                    Minute = c.EndDate.Minute
                }));
            
            CreateMap<Notification, NotificationDto>()
                .ForMember(x => x.Days, opt => opt.MapFrom(
                    m => TimeAgoClass.TimeAgo(m.CreationTime)
            ));

            CreateMap<SolicitationSubsidy, FindByIdSolicitationSubsidyDto>()
                .ForMember(x => x.LatestSolicitationStates,
                    opt => opt.MapFrom(y => y.SolicitationStates.OrderByDescending(x => x.ChangeDate).FirstOrDefault()
                    ))
                .ForMember(x => x.BeginDate, opt => opt.MapFrom(c => c.Destinies.OrderByDescending(v => 
                    v.StartDate).FirstOrDefault().StartDate
                ))
                .ForMember(x => x.FileNumber, opt => opt.MapFrom(c => 
                    c.SolicitationStates.OrderByDescending(y => y.ChangeDate).FirstOrDefault().FileNumber
                ))
                .ForMember(v => v.Observations, opt => opt.MapFrom(n => n.Observations));
            


            CreateMap<Transport, ListTransports>()
                .ForMember(c => c.Description, opt => opt.MapFrom(x => x.Brand + "-" + x.Model + "-" + x.CarPlate+"-"+x.Type));

            CreateMap<LegalRuling, LegalRulingsBaseDto>()
                .ForMember(x => x.Date, opt => opt.MapFrom(c => new DateDto()
                {
                    Day = c.Date.Day,
                    Month = c.Date.Month,
                    Year = c.Date.Year
                }));

            CreateMap<Transport, TransportCreateAccountForDto>()
                .ForMember(x => x.Description, opt => opt.MapFrom(j => j.Brand + j.Model + j.CarPlate + j.Type));

            CreateMap<Observation, ObservationDto>();
            CreateMap<Role, RoleDto>();
        }
    }
}
