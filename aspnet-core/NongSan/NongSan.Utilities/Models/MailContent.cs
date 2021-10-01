using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NongSan.Utilities.Models
{
    public class MailContent
    {
        public MailContent(string to, string subject, string fileName, Dictionary<string, string> bodyKeyValue = null)
        {
            To = to;
            Subject = subject;
            BodyKeyValue = bodyKeyValue;
            FileName = fileName;
        }

        public string To { get; set; }

        public string Subject { get; set; }

        public Dictionary<string, string> BodyKeyValue { get; set; }

        public string FileName { get; set; }
    }
}
