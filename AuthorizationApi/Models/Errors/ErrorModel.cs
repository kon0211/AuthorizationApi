namespace AuthorizationApi.Models
{
    /// <summary>
    /// Returned object in case of error
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// Main error message
        /// </summary>
        public string Message { get; }

        public ErrorModel(string errDescription)
        {
            Message = errDescription;
        }
    }
}
