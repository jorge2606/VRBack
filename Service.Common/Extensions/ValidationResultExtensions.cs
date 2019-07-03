using System.Collections.Generic;
using FluentValidation.Results;
using Service.Common.ServiceResult;

namespace Service.Common.Extensions
{
    public static class ValidationResultExtensions
    {
        public static ServiceResult<TReturn> ToServiceResult<TReturn>(this ValidationResult validationResult, TReturn tReturn)
        {
            var result = new ServiceResult<TReturn>(tReturn);
            AddErrors(result, validationResult.Errors);
            return result;
        }

        private static void AddErrors(ServiceResultBase result, IEnumerable<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                result.AddNotification(NotificationType.Error, error.ErrorMessage);
            }
        }
    }
}
