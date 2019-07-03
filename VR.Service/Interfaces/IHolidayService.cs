using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Dto;
using VR.Web.Helpers;

namespace VR.Service.Interfaces
{
    public interface IHolidayService
    {
        ServiceResult<CreateHolidayDto> CreateHoliday(CreateHolidayDto holidayDto);
        ServiceResult<UpdateHolidayDto> UpdateHoliday(UpdateHolidayDto holidayDto);
        ServiceResult<DeleteHolidayDto> DeleteHoliday(Guid HolidayId);
        ServiceResult<List<AllHolidayDto>> AllHoliday();
        ServiceResult<FindByIdHolidayDto> FindByIdHoliday(Guid id);
        ServiceResult<PagedResult<AllHolidayDto>> Pagination(FilterHolidayDto filters);
        ServiceResult<int> HaveHoliday(DateTime date, int haveHolidays);
    }
}
