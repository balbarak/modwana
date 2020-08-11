using System;
using System.Collections.Generic;
using System.Text;

namespace Modwana.Core.Helpers
{
    public class RegExHelper
    {
        public const string ARABIC_ONLY = @"^[\u0600-\u06FF\s]+$";

        public const string ENGLISH_ONLY = @"^[A-Za-z\s]+$";

        public const string IBAN_FORMAT = @"[A-Z]{2}\d{2} [a-zA-Z0-9]{4} [a-zA-Z0-9]{4} [a-zA-Z0-9]{4} [a-zA-Z0-9]{4} [a-zA-Z0-9]{4}";

        public const string SAUDI_MOBILE = @"^5[0-9]{8}$";

        public const string NUMBER = @"^[0-9]+$";

        public const string NATIONAL_IQAMA_ID = "^[12][0-9]{9}$";

        public const string NATIONAL_ID = "^1[0-9]{9}$";

        public const string IQAMA_ID = "^2[0-9]{9}$";

        public const string POSTIVE_DECIMAL_NUMBER = @"^\-1?$|^([0-9])*[.]?[0-9]{0,2}$";

        public const string PercentageNumber = "^[1-9][0-9]?$|^100$";
    }
}
