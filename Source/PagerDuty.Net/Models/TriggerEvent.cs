using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net
{
    public class TriggerEvent
    {

        public String service_key { get; set; }

        public String incident_key { get; set; }

        public String event_type { get; set; }

        public String description { get; set; }

        public String client { get; set; }

        public String client_url { get; set; }

        public String details { get; set; }

    }
}
