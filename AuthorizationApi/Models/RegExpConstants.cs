namespace AuthorizationApi.Models
{
    public static class RegExp
    {
        /// <summary>
        /// Reg exp for email
        /// </summary>
        public const string Email = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";


        /// <summary>
        /// Reg. exp. for standard 10-digit format, 
        /// for example, 123-456-7890, (123) 456-7890, 123 456 7890, 123.456.7890.
        /// </summary>
        public const string Phone = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";
    }
}
