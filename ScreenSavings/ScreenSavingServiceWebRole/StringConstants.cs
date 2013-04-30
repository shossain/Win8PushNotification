using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScreenSavingServiceWebRole.Constants
{
    public class ReturnType
    {
        public const string SUCCESS = "Success";

        public const string FAIL = "Fail";
        public const string INVALID_INPUT = "Invalid Input";
    }

    public class LogLevel
    {
        public const string ERROR = "Error";
        public const string INFORMATION = "Information";
    }

    public class NotificationType
    {
        public class Raw
        {
            public const string TYPE = "raw";
            public const string CONTENT = "<?xml version=\"1.0\" encoding=\"utf-8\"?><root></root>";
        }
    }
}