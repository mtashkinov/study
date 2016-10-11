namespace RegexpValidator
{
    internal sealed class Validator
    {
        private const string emailPattern = @"^[a-zA-Z_]([.]?[\w\-\d]){0,30}[@]([\w\d]{1,}[\.])+(([a-zA-Z]{2,4}|marketing|sales|support|abuse|security|postmaster|hostmaster|usenet|webmaster|museum))$";
        private const string zipCodeRusPattern = @"^([\d]){6}$";
        private const string zipCodeUSPattern = @"^([\d]){5}[-]([\d]){4}$";
        private const string phonePattern = @"^((\+7|8)( )?(\()?([\d]){3}(\))?( )?)?([\d]){3}(-| )?([\d]){2}(-| )?([\d]){2}$";
        private const string emergencyPhone = @"^((01|02|03|04)(0)?|(9)?(01|02|03|04))$";

        private const string resultEmail = "It's a valid e-mail adress";
        private const string resultZip = "It's a valid zip code";
        private const string resultPhone = "It's a valid phone number";
        private const string resultInvalid = "It's an invalid input";

        public static string Validate(string input)
        {
            if (input == null) return null;

            var emailRegexp = new System.Text.RegularExpressions.Regex(emailPattern);
            var zipCodeRusRegexp = new System.Text.RegularExpressions.Regex(zipCodeRusPattern);
            var zipCodeUSRegexp = new System.Text.RegularExpressions.Regex(zipCodeUSPattern);
            var phoneRegexp = new System.Text.RegularExpressions.Regex(phonePattern);
            var emergencyPhoneRegexp = new System.Text.RegularExpressions.Regex(emergencyPhone);

            if (emailRegexp.IsMatch(input))
            {
                return resultEmail;
            } else if (zipCodeRusRegexp.IsMatch(input) || zipCodeUSRegexp.IsMatch(input))
            {
                return resultZip;
            } else if (phoneRegexp.IsMatch(input) || emergencyPhoneRegexp.IsMatch(input))
            {
                return resultPhone;
            } else
            {
                return resultInvalid;
            }
        }
    }
}
