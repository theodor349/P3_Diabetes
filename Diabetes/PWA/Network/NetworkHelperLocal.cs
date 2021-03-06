﻿using PWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PWA.Network
{
    public class NetworkHelperLocal : INetworkHelper
    {
        private readonly HttpClient _client;
        private LoginUser _user;

        public event NetworkHelper.SomethingChanged UpdateEverything;

        public NetworkHelperLocal(HttpClient client, string url)
        {
            _client = client;
        }

        public async Task<LoginUser> Login(LoginCredential credential)
        {
            _user = new LoginUser()
            {
                Email = "Theodor@thr.com",
                Token = "longtoken",
                FirstName = "Theodor",
                LastName = "Risager",
                NSLink = "Link.com",
                UserID = "LongUserID"
            };
            return _user;
        }

        public async Task<List<PermissionRequestModel>> GetPermissionRequests()
        {
            return new List<PermissionRequestModel>()
            {
                new PermissionRequestModel()
                {
                    Id = 1,
                    FirstName = "Hr",
                    LastName = "Gajhede",
                },
                new PermissionRequestModel()
                {
                    Id = 2,
                    FirstName = "Daniel",
                    LastName = "Møller",
                }
            };
        }

        public async Task<SubjectList> GetSubjectsData()
        {
            var bgAdd = 0;// DateTime.Now.Second % 14;
            return new SubjectList()
            {
                Subjects = new List<Subject>()
                {
                    new Subject()
                    {
                        ID = "abc1234",
                        FirstName = "Tais",
                        LastName = "Hors",
                        PumpData = new PumpData()
                        {
                            BloodGlucose = 1 + bgAdd,
                            BatteryStatus = 0.04f,
                            Status = ArrowDirection.DoubleDown,
                            InsulinStatus = 0.1f,
                            LastReceived = DateTime.Now.AddMinutes(-5),
                        },
                        NotificationDatas = new List<NotificationData>()
                        {
                            new NotificationData()
                            {
                                Type = NotificationType.Message,
                                Title = "High Blood Sugar",
                                Note = "You need to make sure Tais Hors consumes some insulin",
                                Threshold = 10.0f,
                                ThresholdType = ThresholdType.High,
                                IconClassName = "fas fa-syringe"
                            },
                            new NotificationData()
                            {
                                Type = NotificationType.Warning,
                                Title = "Low Blood Sugar",
                                Note = "You need to make sure Tais Hors consumes a juice",
                                Threshold = 4.0f,
                                ThresholdType = ThresholdType.Low,
                                IconClassName = "fas fa-cubes"
                            }
                        }
                    },
                    new Subject()
                    {
                        ID = "abc12341324654",
                        FirstName = "Theodor",
                        LastName = "Risager",
                        PumpData = new PumpData()
                        {
                            BloodGlucose = 3 + bgAdd,
                            BatteryStatus = 0.8f,
                            Status = ArrowDirection.DoubleUp,
                            InsulinStatus = 0.6f,
                            LastReceived = DateTime.Now.AddMinutes(-20),
                        },
                        NotificationDatas = new List<NotificationData>()
                        {
                            new NotificationData()
                            {
                                Type = NotificationType.Message,
                                Title = "High Blood Sugar",
                                Note = "You need to make sure Theodor Risager consumes some insulin",
                                Threshold = 10.0f,
                                ThresholdType = ThresholdType.High,
                                IconClassName = "fas fa-syringe"
                            },
                            new NotificationData()
                            {
                                Type = NotificationType.Warning,
                                Title = "Low Blood Sugar",
                                Note = "You need to make sure Theodor Risager consumes a juice",
                                Threshold = 4.0f,
                                ThresholdType = ThresholdType.Low,
                                IconClassName = "fas fa-cubes"
                            }
                        }
                    }
                }
            };
        }

        public async Task<List<Permission>> GetPermissions()
        {
            return new List<Permission>()
            {
                new Permission()
                {
                    Id = 1,
                    TargetId = "Hellow",
                    FirstName = "Hr",
                    LastName = "Gajhede",
                    IsOwner = false,
                    BloodGlucose = true,
                    Battery = true,
                    Insulin = true,
                    ExpireDate = new DateTime(2020, 10, 10, 10, 0, 0)
                },
                new Permission()
                {
                    Id = 2,
                    TargetId = "hej",
                    FirstName = "Daniel",
                    LastName = "Møller",
                    IsOwner = true,
                    BloodGlucose = false,
                    Battery = true,
                    Insulin = true,
                    ExpireDate = new DateTime(2021, 10, 10, 10, 0, 0)
                },
                new Permission()
                {
                    Id = 3,
                    TargetId = "Hellow",
                    FirstName = "Hr",
                    LastName = "Gajhede",
                    IsOwner = false,
                    BloodGlucose = true,
                    Battery = false,
                    Insulin = false,
                    ExpireDate = new DateTime(2022, 10, 10, 10, 0, 0)
                },
            };
        }

        public async Task DeletePermission(int id)
        {

        }

        public async Task AcceptRequest(int id)
        {

        }

        public async Task DeclineRequest(int id)
        {

        }

        public async Task SendRequest(RequestPermissionAPIModel request)
        {

        }

        public async Task<bool> TestNSLink(string link)
        {
            return false;
        }

        public async Task EditPermission(Permission p)
        {
        }

        public Task<PublicAccountModel> GetByPhoneNumber(string phoneNumber)
        {
            throw new NotImplementedException();
        }

        Task<bool> INetworkHelper.UpdateNSLink(string link) {
            throw new NotImplementedException();
        }

        public Task<List<NotificationData>> GetNotificationSettings(string userID) {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateNotificationSetting(int id, string note, float threshold, ThresholdType thresholdType, NotificationType notificationType) {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAccount(string firstName, string lastName, bool isEu) {
            throw new NotImplementedException();
        }

        public Task<LoginUser> GetUserData() {
            throw new NotImplementedException();
        }
    }
}
