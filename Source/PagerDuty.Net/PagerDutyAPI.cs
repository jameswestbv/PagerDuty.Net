﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagerDuty.Net {

    [Serializable()]
    public class PagerDutyAPI {

        public string AccessToken { get; set; }
        public string Subdomain { get; set; }
        public int Timeout { get; set; }

        public PagerDutyAPI(string domain, string token) {

            if (String.IsNullOrEmpty(domain)) {
                throw new ArgumentNullException("domain");
            }

            if (String.IsNullOrEmpty(token)) {
                throw new ArgumentNullException("token");
            }

            Subdomain = domain;
            AccessToken = token;
            Timeout = 10000;
        }
        
        /// <summary>
        /// Returns a client object with the default values, virtual for testability
        /// </summary>
        /// <param name="url">The API endpoint you are hitting</param>
        /// <returns></returns>
        protected virtual IRestClient GetClient(string url) {

            IRestClient client = new RestClient(){ 
                                                    Timeout = this.Timeout, 
                                                    BaseUrl = String.Format("https://{0}.pagerduty.com/api{1}",Subdomain,url) 
                                                };

            client.AddDefaultParameter(new Parameter { 
                                                        Name = "Authorization", 
                                                        Value = String.Format("Token token={0}", this.AccessToken), 
                                                        Type = ParameterType.HttpHeader });
            return client;
        }
        
        /// <summary>
        /// Returns a request object with the default values, virtual for testability
        /// </summary>
        /// <returns></returns>
        protected virtual IRestRequest GetRequest() {

            RestRequest request = new RestRequest();
            
            request.RequestFormat = DataFormat.Json;
            
            return request;
        }

        /// <summary>
        /// Makes a call to https://<subdomain>.pagerduty.com/api/v1/alerts
        /// </summary>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="filter">Filter by SMS, Email, Phone or Push</param>
        /// <returns></returns>
        public AlertsResponse GetAlerts(DateTime since, DateTime until, Filter filter) {

            return GetAlerts(since, until, filter, 0, 100);

        }

        /// <summary>
        /// Makes a call to https://<subdomain>.pagerduty.com/api/v1/alerts
        /// </summary>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="filter">Filter by SMS, Email, Phone or Push</param>
        /// <param name="offSet">The offset of the first incident record returned. Default is 0.</param>
        /// <param name="limit">The number of incidents returned. Default (and max limit) is 100</param>
        /// <returns></returns>
        public AlertsResponse GetAlerts(DateTime since, DateTime until, Filter filter, int offSet, int limit) {

            IRestClient client = this.GetClient("/v1/alerts");

            IRestRequest req = this.GetRequest();

            req.AddParameter("since", since.ToString("s"));
            req.AddParameter("until", until.ToString("s"));

            if (filter != Filter.Unspecified) {
                req.AddParameter("filter", "{:type=>\"" + filter.ToString() + "\"}");
            }

            req.AddParameter("offset", offSet);
            req.AddParameter("limit", limit);

            IRestResponse<AlertsResponse> resp = client.Execute<AlertsResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Get high level statistics about the number of alerts (SMSes, phone calls and emails) sent for the desired time period, summed daily, weekly or monthly
        /// </summary>
        /// <param name="since">The start of the date range over which you want to search. The time element is optional.</param>
        /// <param name="until">The end of the date range over which you want to search.</param>
        /// <param name="rollup">Specifies the bucket duration for each summation</param>
        /// <returns></returns>
        public AlertReport GetAlertsReport(DateTime? since, DateTime? until, Rollup rollup) {

            IRestClient client = this.GetClient("/v1/reports/alerts_per_time");

            IRestRequest req = this.GetRequest();

            if(since != null){
                req.AddParameter("since", since.Value.ToString("s"));
            }
            if (until != null) {
                req.AddParameter("until", until.Value.ToString("s"));
            }
            req.AddParameter("rollup", rollup.ToString());

            IRestResponse<AlertReport> resp = client.Execute<AlertReport>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Retrieve an incident
        /// </summary>
        /// <param name="id">ID of the incident</param>
        /// <returns></returns>
        public Incident GetIncident(string id) {

            IRestClient client = this.GetClient("/v1/incidents/" + id);

            IRestRequest req = this.GetRequest();
            
            IRestResponse<Incident> resp = client.Execute<Incident>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        ///  Used to query current and historical PagerDuty incidents over a date range
        /// </summary>
        /// <param name="since">Start date</param>
        /// <param name="until">End date</param>
        /// <param name="fields">Specify which fields return, all fields return by default</param>
        /// <param name="status">Returns only the incidents currently in the passed status(es). Valid status options are triggered, acknowledged, and resolved</param>
        /// <param name="incident_key">Returns only the incidents with the passed de-duplication key</param>
        /// <param name="service">Returns only the incidents associated with the passed service(s). This expects one or more service IDs.  Separate multiple ids by a comma</param>
        /// <param name="assigned_to_user">Returns only the incidents currently assigned to the passed user(s). This expects one or more user IDs. </param>
        /// <param name="sort_by">Which field to sort by</param>
        /// <param name="sort_direction">Sort direction (default is asc)</param>
        /// <param name="offSet">The offset of the first incident record returned</param>
        /// <param name="limit">The number of incidents returned.</param>
        /// <returns></returns>
        public IncidentsResponse GetIncidents(IncidentFilter filters, IncidentSortBy sort_by, SortDirection sort_direction, int offSet, int limit) {

            IRestClient client = this.GetClient("/v1/incidents");

            IRestRequest req = this.GetRequest();

            if (filters.ReturnAll) {
                req.AddParameter("date_range", "all");
            } 
            else {
                req.AddParameter("since", filters.since.ToString("s"));
                req.AddParameter("until", filters.until.ToString("s"));
            }
            
            if (!String.IsNullOrEmpty(filters.fields)) {
                req.AddParameter("fields", filters.fields);
            }

            if (!String.IsNullOrEmpty(filters.status)) {
                req.AddParameter("status", filters.fields);
            }

            if (!String.IsNullOrEmpty(filters.incident_key)) {
                req.AddParameter("incident_key", filters.incident_key);
            }

            if (!String.IsNullOrEmpty(filters.service)) {
                req.AddParameter("service", filters.service);
            }

            if (!String.IsNullOrEmpty(filters.assigned_to_user)) {
                req.AddParameter("assigned_to_user", filters.assigned_to_user);
            }

            if (sort_by != IncidentSortBy.unspecified) {
                req.AddParameter("sort_by", sort_by.ToString() + ":" + sort_direction.ToString());
            }
            
            req.AddParameter("offset", offSet);
            req.AddParameter("limit", limit);

            IRestResponse<IncidentsResponse> resp = client.Execute<IncidentsResponse>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Retrieve an incident
        /// </summary>
        /// <param name="id">ID of the incident</param>
        /// <returns></returns>
        public List<Note> GetNotesForIncident(string id) {

            IRestClient client = this.GetClient("/v1/incidents/" + id + "/notes");

            IRestRequest req = this.GetRequest();

            var resp = client.Execute<List<Note>>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }

        /// <summary>
        /// Add a new note to an incident
        /// </summary>
        /// <param name="note">Content of the note</param>
        /// <param name="incident_id">The incident you are attaching to</param>
        /// <param name="requestor_id">User you are posting the note on behalf of</param>
        /// <returns></returns>
        public Note PostNoteForIncident(string note,string incident_id, string requestor_id) {

            IRestClient client = this.GetClient("/v1/incidents/" + incident_id + "/notes");

            IRestRequest req = this.GetRequest();

            req.Method = Method.POST;

            req.AddParameter("application/json; charset=utf-8", "{\"requester_id\":\"" + requestor_id + "\",\"note\":{\"content\":\"" + note + "\"}}", ParameterType.RequestBody);
            
            IRestResponse<Note> resp = client.Execute<Note>(req);

            if (resp.Data == null) {
                throw new PagerDutyAPIException(resp);
            }

            return resp.Data;
        }
        
    }
}
