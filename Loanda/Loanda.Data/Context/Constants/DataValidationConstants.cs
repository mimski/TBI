using System;
using System.Collections.Generic;
using System.Text;

namespace Loanda.Data.Context.Constants
{
    internal static class DataValidationConstants
    { 
        public static class Applicant
        {
            public const int MaxFirstNameLenght = 120;

            public const int MaxMiddleNameLenght = 120;

            public const int MaxLastNameLenght = 120;

            public const int MaxAddressLenght = 255;

            public const int MaxCityLenght = 40;
        }

        public static class ApplicationStatus
        {
            public const int MaxNameLenght = 20;
        }

        public static class EmailStatus
        {
            public const int MaxNameLenght = 20;
        }
    }
}
