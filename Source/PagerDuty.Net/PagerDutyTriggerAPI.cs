using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PagerDuty.Net
{
    public class PagerDutyTriggerAPI
    {

        public int Timeout { get; set; }

        public PagerDutyTriggerAPI()
        {
            
        }


        protected virtual IRestClient GetEventsClient()
        {

            IRestClient client = new RestClient()
            {
                Timeout = this.Timeout,
                BaseUrl = "https://events.pagerduty.com/generic/2010-04-15/create_event.json"
            };

            return client;
        }

        protected virtual IRestRequest GetRequest()
        {

            RestRequest request = new RestRequest();

            request.RequestFormat = DataFormat.Json;

            return request;
        }

        public TriggerEventResponse RaiseTrigger(String serviceKey,
                                                 String incidentKey,
                                                 String description,
                                                 String client,
                                                 String clientUrl,
                                                 String details) {

            TriggerEvent trig = new TriggerEvent
            {
                service_key = serviceKey,
                incident_key = incidentKey,
                event_type = "trigger",
                description = description,
                client = client,
                client_url = clientUrl,
                details = details
            };


            IRestClient restClient = this.GetEventsClient();

            IRestRequest req = this.GetRequest();

            req.Method = Method.POST;
            req.AddHeader("Content-Type", "application/json");
            req.AddBody(trig);
            
            IRestResponse<TriggerEventResponse> resp = restClient.Execute<TriggerEventResponse>(req);

            if (resp.Data == null)
            {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
            
        }

    }


}
