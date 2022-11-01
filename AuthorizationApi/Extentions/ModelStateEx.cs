using AuthorizationApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthorizationApi.Extentions
{
    public static class ModelStateEx
    {
        /// <summary>
        /// Сollect all model errors and convert to ErrorModel
        /// </summary>
        public static ValidationErrorModel GetErrorModel(this ModelStateDictionary model, string errMessage)
        {
            List<ValidationError> errors = new List<ValidationError>();

            foreach (var err in model)
            {
                if (err.Value.ValidationState == ModelValidationState.Invalid)
                {
                    var validationError = new ValidationError(
                                 param: err.Key,
                                 errors: err.Value.Errors.Select(e => e.ErrorMessage).ToList());

                    errors.Add(validationError);
                }
            }
            return new ValidationErrorModel(errMessage, errors);
        }
    }
}
