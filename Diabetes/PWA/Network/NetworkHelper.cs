using PWA.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PWA.Network
{
    class AuthenticatedUser
    {
        public string Access_Token { get; set; }
        public string UserName { get; set; }
    }

    public class NetworkHelper : INetworkHelper
    {
        private readonly HttpClient _client;
        private LoginUser _user;

        public NetworkHelper(HttpClient client, string url)
        {
            _client = client;
            InitializeClient(url);
        }

        private void InitializeClient(string url)
        {
            _client.BaseAddress = new Uri(url);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task AcceptRequest(int id)
        {
            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/Permission/AcceptPermissionRequest", id))
            {
                if (response.IsSuccessStatusCode)
                {

                }
            }
        }

        public async Task DeclineRequest(int id)
        {
            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/Permission/DenyPermissionReqeust", id))
            {
                if (response.IsSuccessStatusCode)
                {

                }
            }
        }

        public async Task DeletePermission(int id)
        {
            using (HttpResponseMessage response = await _client.DeleteAsync("api/Permission/" + id))
            {
                if (response.IsSuccessStatusCode)
                {

                }
            }
        }

        public async Task<List<PermissionRequestModel>> GetPermissionRequests()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Permission/GetPendingPermissions"))
            {
                if (response.IsSuccessStatusCode)
                    return (await response.Content.ReadAsAsync<PermissionRequestsModel>()).Requests;
                else
                    return null;
            }
        }

        public async Task<List<Permission>> GetPermissions()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Permission/GetPendingPermissions"))
            {
                if (response.IsSuccessStatusCode)
                    return (await response.Content.ReadAsAsync<Permissions>()).PermissionList;
                else
                    return null;
            }
        }

        public async Task<SubjectList> GetSubjectsData()
        {
            var res = new SubjectList()
            {
                Subjects = new List<Subject>()
            };

            var ids = await SubjectIds();
            foreach (var id in ids)
            {
                var name = await GetName(id);
                var data = await GetPumpData(id);
                var notificationSettings = await GetNotificaitonSettings(id);
                res.Subjects.Add(new Subject()
                {
                    ID = id,
                    FirstName = name.FirstName,
                    LastName = name.LastName,
                    PumpData = data,
                    NotificationDatas = notificationSettings,
                });
            }
            return res;
        }

        private async Task<List<NotificationData>> GetNotificaitonSettings(string id)
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Relay/" + id))
            {
                if (response.IsSuccessStatusCode)
                    return (await response.Content.ReadAsAsync<NotificationSettings>()).Notifications;
                else
                    return null;
            }
        }

        private async Task<PumpData> GetPumpData(string id)
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Relay/" + id))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<PumpData>();
                else
                    return null;
            }
        }

        private async Task<Name> GetName(string id)
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Account/GetName/" + id))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<Name>();
                else
                    return null;
            }
        }

        private async Task<List<string>> SubjectIds()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Account"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<List<string>>();
                else
                    return null;
            }
        }

        public async Task<LoginUser> Login(LoginCredential credential)
        {
            await Authenticate(credential);
            return await GetUserData();
        }

        private async Task<LoginUser> GetUserData()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Account"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<LoginUser>();
                else
                    return null;
            }
        }

        private async Task Authenticate(LoginCredential credential)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", credential.Email),
                new KeyValuePair<string, string>("password", credential.Password),
            });

            using (HttpResponseMessage response = await _client.PostAsync("token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    SetupHeader(result.Access_Token);
                }
            }
        }

        private void SetupHeader(string token)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");
        }

        public async Task SendRequest(RequestPermissionAPIModel request)
        {
            using (HttpResponseMessage response = await _client.PostAsJsonAsync("api/Permission/RequestPermission", request))
            {
                if (response.IsSuccessStatusCode)
                {
                }
            }
        }

        public async Task<bool> TestNSLink(string link)
        {
            using (HttpResponseMessage response = await _client.PostAsJsonAsync("api/Permission/RequestPermission", link))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<bool>();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task UpdateNSLink(string link)
        {
            using (HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/UpdateNightScoutLink", link))
            {
                if (response.IsSuccessStatusCode)
                {
                }
            }
        }
    }

    class Name
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    class NotificationSettings
    {
        public List<NotificationData> Notifications { get; set; }
    }
}
