using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net
{
    [Serializable()]
    public class TriggerEventResponse
    {

        public TriggerEventResponse()
        {

        }
        
        public String status { get; set; }

        public String message { get; set; }

        public String incident_key { get; set; }

        public List<String> errors { get; set; }

    }
}
