using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class SolicitationStateService : ISolicitationStateService
    {
        private readonly DataContext _context;

        public SolicitationStateService(
            DataContext context
        )
        {
            _context = context;
        }

        public ServiceResult<AddFielNumberDto> AddFielNumber(AddFielNumberDto fields)
        {
            var solicitationState = _context.SolicitationStates
                .Where(x => x.SolicitationSubsidyId == fields.SolicitationSubsidyId)
                .OrderByDescending(x => x.ChangeDate)
                .FirstOrDefault();

            solicitationState.FileNumber = fields.FileNumber;

            _context.SolicitationStates.Update(solicitationState);
            _context.SaveChanges();

            return new ServiceResult<AddFielNumberDto>(fields);
        }


        public ServiceResult<bool> ItHasNumberFile(Guid solId)
        {
            var hasNumberFile = _context.SolicitationStates
                .Where(x => x.SolicitationSubsidyId == solId 
                            && x.StateId == State.Accepted 
                            && !string.IsNullOrEmpty(x.FileNumber))
                .FirstOrDefault();
            if (hasNumberFile != null)
            {
                return new ServiceResult<bool>(true);
            }

            return new ServiceResult<bool>(false);
        }

    }
}
