namespace AuthorizationApi.Models
{
    /// <summary>
    /// Returned object in case of validation errors
    /// </summary>
    public class ValidationErrorModel : ErrorModel
    {
        #region Members

        private List<ValidationError> errors = new List<ValidationError>();

        #endregion //members

        #region Properties
        
        /// <summary>
        /// Detailed error description
        /// </summary>
        public IReadOnlyList<ValidationError> ValidationErrors => errors;

        #endregion //properties

        public ValidationErrorModel(string errDescription, List<ValidationError> errors)
            : base(errDescription)
        {
            this.errors = errors;
        }
    }

   
    public class ValidationError
    {
        /// <summary>
        /// Parameter name that failed validation
        /// </summary>
        public string Param { get; set; }

        /// <summary>
        /// Description of errors (one or more)
        /// </summary>
        public List<string> Errors { get; set; }

        public ValidationError(string param, List<string> errors)
        {
            Param = param;
            Errors = errors;
        }
        public ValidationError(string param, string errorDescription)
        {
            Param = param;
            Errors = new List<string> { errorDescription };
        }
    }
}
