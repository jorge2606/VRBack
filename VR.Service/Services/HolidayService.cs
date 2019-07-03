using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using FluentValidation.Results;
using Service.Common.Extensions;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;
using VR.Web.Helpers;

namespace VR.Service.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<HolidayBaseDto> _fluentValidator;
        private readonly IMapper _mapper;
        
        public HolidayService(
            DataContext dataContext,
            IValidator<HolidayBaseDto> fluentValidator,
            IMapper mapper
            )
        {
            _dataContext = dataContext;
            _fluentValidator = fluentValidator;
            _mapper = mapper;
        }

        public ServiceResult<CreateHolidayDto> CreateHoliday(CreateHolidayDto holidayDto)
        {
            var validate = _fluentValidator.Validate(holidayDto);
            if (!validate.IsValid)
            {
                return  validate.ToServiceResult<CreateHolidayDto>(null);
            }
            
            _dataContext.Holidays.Add(
                new Holiday()
                {
                    Id = new Guid(),
                    Description = holidayDto.Description,
                    Date = holidayDto.Date.ToDateTime()
                }
            );

            _dataContext.SaveChanges();

            return new ServiceResult<CreateHolidayDto>(holidayDto);
        }

        public ServiceResult<UpdateHolidayDto> UpdateHoliday(UpdateHolidayDto holidayDto)
        {

            var validate = _fluentValidator.Validate(holidayDto);
            
            if (!validate.IsValid)
            {
                return validate.ToServiceResult<UpdateHolidayDto>(null);
            }

            var exist = _dataContext.Holidays.FirstOrDefault(x => x.Id == holidayDto.Id);

            if (exist == null)
            {
                return new ServiceResult<UpdateHolidayDto>(null);
            }

            exist.Date = holidayDto.Date.ToDateTime();
            exist.Description = holidayDto.Description;

            _dataContext.Holidays.Update(exist);

            _dataContext.SaveChanges();

            return new ServiceResult<UpdateHolidayDto>(holidayDto);

        }

        public ServiceResult<DeleteHolidayDto> DeleteHoliday(Guid HolidayId)
        {
            var exist = _dataContext.Holidays.FirstOrDefault(x => x.Id == HolidayId);

            if (exist == null)
            {
                return new ServiceResult<DeleteHolidayDto>(null);
            }

            exist.IsDeleted = true;
            _dataContext.Holidays.Update(exist);
            _dataContext.SaveChanges();

            return new ServiceResult<DeleteHolidayDto>(_mapper.Map<DeleteHolidayDto>(exist));
        }

        public ServiceResult<List<AllHolidayDto>> AllHoliday()
        {
            return _mapper.Map< ServiceResult<List<AllHolidayDto>> >(
                _dataContext.Holidays
                    .Where(x => x.IsDeleted != true)
                    .ToList());
        }

        public ServiceResult<FindByIdHolidayDto> FindByIdHoliday(Guid id)
        {
            var exist = _dataContext.Holidays
                .Where(x => x.IsDeleted != true)
                .FirstOrDefault(x => x.Id == id);
            if (exist == null)
            {
                return new ServiceResult<FindByIdHolidayDto>(null);
            }

            return new ServiceResult<FindByIdHolidayDto>(_mapper.Map <FindByIdHolidayDto>(exist));
        }

        public ServiceResult<PagedResult<AllHolidayDto>> Pagination(FilterHolidayDto filters)
        {
            const int pageSize = 10;
            var newDatetime = new DateTime();
            if (filters.Date.Year == 0 || filters.Date.Month == 0 || filters.Date.Day == 0)
            {
                filters.Date = null;
            }
            else
            {
                newDatetime = new DateTime(filters.Date.Year, filters.Date.Month, filters.Date.Day);
            }

            var resultFull = _dataContext.Holidays
                .Where(x => x.IsDeleted != true);

            var resultPage = resultFull
                .Where(
                    x => (string.IsNullOrEmpty(filters.Description) || x.Description.ToUpper().Contains(filters.Description.ToUpper()))
                         &&
                    //(filters.Date == null || (DateTime.Compare(x.Date, new DateTime(filters.Date.Year, filters.Date.Month, filters.Date.Day)) == 0))
                         
                         (filters.Date == null || (x.Date.Day == newDatetime.Day && x.Date.Month == newDatetime.Month && x.Date.Year == newDatetime.Year) )
                ).Skip((filters.Page ?? 0) * pageSize)
                .Take(pageSize)
                .ProjectTo<AllHolidayDto>()
                .ToList();

            if (resultPage.Count() == 0 && filters.Page > 0)
            {
                resultPage = resultFull
                    .Where(
                        x => (string.IsNullOrEmpty(filters.Description) || x.Description.ToUpper().Contains(filters.Description.ToUpper()))
                             &&
                             //(filters.Date == null || (DateTime.Compare(x.Date, new DateTime(filters.Date.Year, filters.Date.Month, filters.Date.Day)) == 0))

                             (filters.Date == null || (x.Date.Day == newDatetime.Day && x.Date.Month == newDatetime.Month && x.Date.Year == newDatetime.Year))
                    ).Skip( ( (filters.Page ?? 0) - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<AllHolidayDto>()
                    .ToList();
            }

            return new ServiceResult<PagedResult<AllHolidayDto>>(new PagedResult<AllHolidayDto>()
            {
                List = resultPage,
                TotalRecords = resultFull.Count()
            });
        }

        public ServiceResult<int> HaveHoliday(DateTime date, int amountDays)
        {
            var amountHolidays = 0;
            for (int i = 0; i <= amountDays; i++)
            {
                var isDate = _dataContext.Holidays.FirstOrDefault(x => x.Date.CompareTo(date.AddDays(i)) == 0);
                if (isDate != null)
                {
                    amountHolidays = amountHolidays + 1;
                }
            }

            return new ServiceResult<int>(amountHolidays);
        }
    }
}
