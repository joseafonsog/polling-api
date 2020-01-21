using System;
using System.Collections.Generic;
using System.Text;

namespace PollingApp.Commons.Settings
{
    public class EmailSettings : IEmailSettings
    {
        public string Sender { get; set; }
        public string LocalDirectoryPath { get; set; }
        public string Subject { get; set; }
    }
}
