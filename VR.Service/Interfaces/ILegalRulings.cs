using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VR.Data.Model;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface ILegalRulings
    {
        ServiceResult<LegalRulingsBaseDto> Create(LegalRulingsBaseDto legalRuling);
        ServiceResult<IQueryable<LegalRulingsBaseDto>> GetAll();
        ServiceResult<LegalRulingsBaseDto> FindById(Guid id);
        ServiceResult<DeleteLegalDto> Delete(Guid Id);
    }
}
