using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class ApproveOfAuthorityThatOrderCommissionsService : IApproveOfAuthorityThatOrderCommissionsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ApproveOfAuthorityThatOrderCommissionsService(
            DataContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<ApproveOfAuthorityThatOrderCommissionsDto>> GetApproved(Guid Id)
        {
            var allApproved = _context.ApproveOfAuthorityThatOrderCommissions
                .ToList();
            var result = new List<ApproveOfAuthorityThatOrderCommissionsDto>();
            foreach (var i in allApproved)
            {
                var newApproved = new ApproveOfAuthorityThatOrderCommissionsDto()
                {
                    Id = i.Id,
                    Description = i.Description,
                    Order = i.Order,
                    Default = i.Default,
                    OrderReport = i.OrderReport
                };
                var exist = _context.ApproveOfAuthoritySolicitations.FirstOrDefault(x =>
                    x.AprApproveOfAuthorityThatOrderCommissionId == i.Id && x.SolicitationSubsidyId == Id);
                if (exist != null || i.Default)
                {
                    newApproved.Checked = true;
                }
                result.Add(newApproved);
            }

            return new ServiceResult<List<ApproveOfAuthorityThatOrderCommissionsDto>>(result.OrderBy(x => x.Order).ThenBy(c=> c.OrderReport).ToList());
        }
    }
}
