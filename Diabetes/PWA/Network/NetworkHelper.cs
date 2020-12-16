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
        public string ID { get; set; }
    }

    public class NetworkHelper : INetworkHelper
    {
        private readonly HttpClient _client;
        private LoginUser _user;

        public delegate void SomethingChanged();
        public event SomethingChanged UpdateEverything;

        public NetworkHelper(HttpClient client, string url)
        {
            _client = client;
            InitializeClient(url);
        }

        private void InitializeClient(string url)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task AcceptRequest(int id)
        {
            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/Permission/AcceptPermissionRequest", new IntValue(id)))
            {
                if (response.IsSuccessStatusCode)
                {
                    UpdateEverything?.Invoke();
                }
            }
        }

        public async Task DeclineRequest(int id)
        {
            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/Permission/DenyPermissionReqeust", new IntValue(id)))
            {
                if (response.IsSuccessStatusCode)
                {
                    UpdateEverything?.Invoke();
                }
            }
        }

        public async Task DeletePermission(int id)
        {
            using (HttpResponseMessage response = await _client.DeleteAsync("api/Permission/Delete/" + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    UpdateEverything?.Invoke();
                }
            }
        }

        public async Task EditPermission(Permission p)
        {
        }

        public async Task<List<PermissionRequestModel>> GetPermissionRequests()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Permission/GetPendingPermissions"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var res = (await response.Content.ReadAsAsync<PermissionRequestsModel>()).Requests;
                    return res;
                }
                else
                    return null;
            }
        }

        public async Task<List<Permission>> GetPermissions()
        {
            var res = new List<Permission>();
            using (HttpResponseMessage response = await _client.GetAsync("api/Permission/ByTarget"))
            {
                if (response.IsSuccessStatusCode)
                    res.AddRange((await response.Content.ReadAsAsync<Permissions>()).PermissionList);
            }
            using (HttpResponseMessage response = await _client.GetAsync("api/Permission/ByWatcher"))
            {
                if (response.IsSuccessStatusCode)
                    res.AddRange((await response.Content.ReadAsAsync<Permissions>()).PermissionList);
            }
            res = RemoveDuble(res, x => x.Id);
            res.ForEach(x => x.IsSelf = (x.WatcherId.Equals(_user.UserID) && x.TargetId.Equals(_user.UserID)));
            return res;
        }

        private List<T> RemoveDuble<T>(List<T> items, Func<T, int> idFunc)
        {
            var dic = new List<int>();
            var res = new List<T>();
            foreach (var item in items)
            {
                int id = idFunc(item);
                if (!dic.Contains(id))
                {
                    dic.Add(id);
                    res.Add(item);
                }
            }

            return res;
        }

        public async Task<SubjectList> GetSubjectsData()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Relay"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsAsync<SubjectList>();
                    foreach (var s in res.Subjects)
                    {
                        foreach (var n in s.NotificationDatas)
                        {
                            if (n.ThresholdType == ThresholdType.Low)
                                n.IconClassName = "fas fa-cubes";
                            else
                                n.IconClassName = "fas fa-syringe";
                        }
                    }
                    return res;
                }
                else
                    return new SubjectList() { Subjects = new List<Subject>() };
            }
        }

        public async Task<LoginUser> Login(LoginCredential credential)
        {
            var authData = await Authenticate(credential);
            if (string.IsNullOrWhiteSpace(authData.Item1))
                return null;
            _user = await GetUserData();
            _user.Token = authData.Item1;
            _user.UserID = authData.Item2;
            return _user;
        }

        public async Task<List<NotificationData>> GetNotificationSettings(string userID) {
            var res = new List<NotificationData>();
            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/NotificationSetting/GetNotificationSettings", new StringValue(userID))) {
                if (response.IsSuccessStatusCode)
                    res.AddRange((await response.Content.ReadAsAsync<NotificationDataList>()).NotificationDatas);
            }
            return res;
        }

        public async Task<bool> UpdateNotificationSetting(int id, string note, float threshold, ThresholdType thresholdType, NotificationType notificationType) {
            NotificationUpdate update = new NotificationUpdate {
                ID = id,
                Note = note,
                Threshold = threshold,
                ThresholdType = thresholdType,
                Type = notificationType
            };

            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/NotificationSetting", update)) {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<LoginUser> GetUserData()
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Account"))
            {
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<LoginUser>();
                else
                    return null;
            }
        }

        public async Task<bool> UpdateAccount(string firstName, string lastName, bool isEu) {
            AccountUpdate update = new AccountUpdate {
                FirstName = firstName,
                LastName = lastName,
                IsEU = isEu
            };

            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/Account", update)) {
                if (response.IsSuccessStatusCode)
                {
                    UpdateEverything?.Invoke();
                    return true;
                }
                else 
                    return false;
            }
        }

        private async Task<Tuple<string, string>> Authenticate(LoginCredential credential)
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
                    return new Tuple<string, string>(result.Access_Token, result.ID);
                }
                else
                {
                    return new Tuple<string, string>("", "");
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

        public async Task<PublicAccountModel> GetByPhoneNumber(string phoneNumber)
        {
            using (HttpResponseMessage response = await _client.GetAsync("api/Account/ByPhoneNumber/" + phoneNumber))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<PublicAccountModel>();
                }
                else
                {
                    return new PublicAccountModel();
                }
            }
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
            using (HttpResponseMessage response = await _client.PostAsJsonAsync("api/Account/TestNightScoutLink", new StringValue(link))) {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> UpdateNSLink(string link)
        {
            using (HttpResponseMessage response = await _client.PutAsJsonAsync("api/Account/UpdateNightScoutLink", new StringValue(link))) {
                return response.IsSuccessStatusCode;
            }
        }
    }

    public class StringValue
    {

        public StringValue(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }

    public class IntValue
    {
        public IntValue(int value)
        {
            Value = value;
        }
        public int Value { get; set; }
    }

    public class FloatValue {
        public FloatValue(float value) {
            Value = value;
        }
        public float Value { get; set; }
    }
}
