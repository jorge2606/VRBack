using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Data.Model.ModelStoreProcedure;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class DestinyService : IDestinyService
    {
        private readonly DataContext _context;
        public DestinyService(DataContext context)
        {
            _context = context;
        }

        public ServiceResult<List<DestinyBaseDto>> Create(List<DestinyBaseDto> Destinies)
        {
            foreach (var destiny in Destinies)
            {
                if (destiny.Id == Guid.Empty)//si no existe en la DB
                {
                    Destiny newDestiny = new Destiny()
                    {
                        Id = new Guid(),
                        TransportId = destiny.TransportId,
                        CategoryId = destiny.CategoryId,
                        CityId = destiny.CityId,
                        CodeLiquidationId = destiny.CodeLiquidationId,
                        CountryId = destiny.CountryId,
                        SolicitationSubsidyId = destiny.SolicitationSubsidyId,
                        Days = destiny.Days,
                        StartDate = destiny.StartDate.ToDateTime(),
                        ProvinceId = destiny.ProvinceId,
                        AdvanceCategory = destiny.AdvanceCategory,
                        PercentageCodeLiquidation = destiny.PercentageCodeLiquidation
                    };

                    _context.Destinies.Add(newDestiny);
                }

            }
            _context.SaveChanges();
            return new ServiceResult<List<DestinyBaseDto>>(Destinies);
        }

        public ServiceResult<DestinyBaseDto> Delete(Guid destinyId)
        {
            var destiny = _context.Destinies.FirstOrDefault(x => x.Id == destinyId);

            if (destiny == null)
            {
                return new ServiceResult<DestinyBaseDto>(null);
            }
           
            var destinies_solitation = _context.Destinies
                .Where(x => x.SolicitationSubsidyId == destiny.SolicitationSubsidyId)
                .ToList();

            if (destinies_solitation.Count() == 1)
            {
                ServiceResult<DestinyBaseDto> result = new ServiceResult<DestinyBaseDto>();
                result
                    .AddNotification(NotificationType.Error, "Una solicitud de viático debe contener al menos 1 destino");
                return result;
            }

            _context.Destinies.Remove(destiny);
            _context.SaveChanges();


            return new ServiceResult<DestinyBaseDto>(new DestinyBaseDto());
        }


        public ServiceResult<List<Get_DestiniesResult>> Get_DestiniesProcedure(Guid solicitationId)
        {
           return new ServiceResult<List<Get_DestiniesResult>>(
               _context.Get_DestiniesProcedure(solicitationId)
               );
        }


    }
}
