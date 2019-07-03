using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IFileService
    {
        Task<ServiceResult<UpdateMyImageDto>> UpdateMyImage(UpdateMyImageDto model);
        Task<ServiceResult<UpdateMyImageDto>> HolographSignUpdate(UpdateMyImageDto model);
        ServiceResult<FileByIdDto> GetByIdFile(Guid userId);
        ServiceResult<string> GetCompletePath(string p_path, Guid userId, int width, int height);
        ServiceResult<FileByIdDto> RemoveProfilePhoto(Guid userId, int width, int height);
        ServiceResult<FileByIdDto> GetCompletePathHolographSign(Guid userId);
        ServiceResult<FileByIdDto> RemoveHolographSign(Guid userId);
        ServiceResult<FileCreateFromRefundDto> AddExpenditureRefundImage(FileCreateFromRefundDto image);
        ServiceResult<List<ImageDto>> GetUrlExpenditureRefundFile(Guid expenditureId);
        ServiceResult<string> UrlSignHolograph(Guid userId);
    }
}
