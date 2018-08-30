using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Common
{
    public class Message
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }

        public Boolean HasMessage
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Body) && !string.IsNullOrWhiteSpace(Type);
            }
        }

        public void Clear()
        {
            Title = string.Empty;
            Body = string.Empty;
            Type = string.Empty;
        }
    }

}
