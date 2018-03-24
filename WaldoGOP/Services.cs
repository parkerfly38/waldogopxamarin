using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Threading.Tasks;
using WaldoGOP.Models;

namespace WaldoGOP
{
    public class Services
    {
        static string access_token = "";
        static DateTime? expires_in;

        public Services()
        {
            if (access_token.Length <= 0)
            {
                Models.AppData appdata = App.Database.GetAppDataAsync().Result;

                if (appdata == null)
                {
                    getTokens();
                }
                else
                {
                    if (appdata.Expires_In <= DateTime.Now)
                    {
                        getTokens();
                    }
                }
            }
        }

        public void addRSVP(Models.EventRSVP rsvp)
        {
            if (expires_in <= DateTime.Now)
                getTokens();

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization", "Bearer " + access_token);
            client.AddDefaultHeader("Content-type","application/json");

            var request = new RestRequest("events", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(rsvp);

            client.Execute(request);
        }

        public Models.Event getEvent(int eventId)
        {
            if (expires_in <= DateTime.Now)
                getTokens();

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization", "Bearer " + access_token);
            client.AddDefaultHeader("Content-type", "application/json");

            var request = new RestRequest("events/" + eventId.ToString(), Method.GET);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<Models.Event>(response.Content);
        }

        public IEnumerable<Models.Event> getAllEvents()
        {
            if (expires_in <= DateTime.Now)
                getTokens();

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization", "Bearer " + access_token);
            client.AddDefaultHeader("Content-type", "application/json");

            var request = new RestRequest("events", Method.GET);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<IEnumerable<Models.Event>>(response.Content);
        }

        public IEnumerable<Models.Volunteer> getAllVolunteers()
        {
            if (expires_in <= DateTime.Now)
                getTokens();
            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization","Bearer " + access_token);
            client.AddDefaultHeader("Content-type", "application/json");

            var request = new RestRequest("volunteers", Method.GET);

            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Models.Volunteer>>(response.Content);
        }

        public IEnumerable<Models.MaineVoter> getAllVoters()
        {
            if (expires_in <= DateTime.Now)
                getTokens();

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization","Bearer " + access_token);
            client.AddDefaultHeader("Content-type", "application/json");

            var request = new RestRequest("voters", Method.GET);

            var response = client.Execute(request);

            IEnumerable<Models.MaineVoter> voters = JsonConvert.DeserializeObject<IEnumerable<Models.MaineVoter>>(response.Content);

            foreach (var voter in voters)
            {
                DoGeoCode(voter.PrimaryAddress1 + ' ' + voter.PrimaryCity + ' ' + voter.PrimaryState + ' ' + voter.PrimaryZip, voter);
            }

            return voters;
        }

        private async void DoGeoCode(string address, MaineVoter voter)
        {
            Geocoder geocoder = new Geocoder();

            var pos = await geocoder.GetPositionsForAddressAsync(address);

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization", "Bearer " + access_token);
            client.AddDefaultHeader("Content-type", "application/json");

            if (pos.First() != null)
            {
                voter.Latitude = pos.First().Latitude;
                voter.Longitude = pos.First().Longitude;

                var newrequest = new RestRequest("voters", Method.PUT);
                newrequest.RequestFormat = DataFormat.Json;
                newrequest.AddJsonBody(voter);
                client.Execute(newrequest);
            }
        }

        public IEnumerable<Models.MyWalkList> getMyWalkList()
        {
            if (expires_in <= DateTime.Now)
                getTokens();

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Authorization", "Bearer " + access_token);
            client.AddDefaultHeader("Content-type", "application/json");

            var request = new RestRequest("walklists/mywalklist/" + App.Database.GetAppDataAsync().Result.DeviceID, Method.GET);

            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<IEnumerable<Models.MyWalkList>>(response.Content);
        }

        public bool getLoginTokens(string login, string password)
        {
            Models.AppData appdata = App.Database.GetAppDataAsync().Result;

            /*if (appdata != null)
            {
                deviceid = appdata.DeviceID;
            }*/

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Content-type", "application/json");
            var request = new RestRequest("token", Method.POST);
            var requestbody = new { login = login, password = password };
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(requestbody);

            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return false;
            }

            var newappdata = JsonConvert.DeserializeObject<Models.AppData>(response.Content);

            App.Database.UpdateAppData(newappdata);

            access_token = newappdata.Access_Token;
            expires_in = newappdata.Expires_In;

            return true;

        }

        private void getTokens()
        {
            Models.AppData appdata = App.Database.GetAppDataAsync().Result;

            string deviceid = "";

            if (appdata != null)
            {
                deviceid = appdata.DeviceID;
            }

            var client = new RestClient("https://www.kresgefor98.com/rest/CFCandidate/");
            client.AddDefaultHeader("Content-type", "application/json");
            var request = new RestRequest("token", Method.POST);
            var requestbody = new { deviceid = deviceid };
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(requestbody);

            var response = client.Execute(request);
            var newappdata = JsonConvert.DeserializeObject<Models.AppData>(response.Content);

            if (deviceid.Length > 0)
            {
                newappdata.DeviceID = deviceid;
            }

            App.Database.UpdateAppData(newappdata);

            access_token = newappdata.Access_Token;
            expires_in = newappdata.Expires_In;
         }
    }
}
