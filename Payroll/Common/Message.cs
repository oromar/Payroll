using System;

namespace Payroll.Common
{
    public class Message
    {
        public string Body { get; set; }
        public string Type { get; set; }

        public bool HasMessage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Body) && !string.IsNullOrWhiteSpace(Type);
            }
        }

        public void Clear()
        {
            Body = string.Empty;
            Type = string.Empty;
        }
    }
}
