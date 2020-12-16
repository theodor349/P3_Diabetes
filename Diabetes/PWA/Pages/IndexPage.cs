using Microsoft.AspNetCore.Components;
using PWA.Components;
using PWA.Models;
using PWA.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Pages
{
    enum NotificationEvent { Nothing, Create, Remove }
    public class IndexPage : ComponentBase
    {
        public bool playSound = false;
        public string sound = "Next.mp3";

        public int page = 0;
        public LoginUser loggedInUser;
        public Subject targetSubject;
        public Dictionary<string, Subject> subjects = new Dictionary<string, Subject>();
        public Dictionary<string, ActiveNotification> activeNotifications = new Dictionary<string, ActiveNotification>();
        public Stack<ActiveNotification> focusedNotifications = new Stack<ActiveNotification>();
        [Inject] INetworkHelper Network { get; set; }

        protected override void OnInitialized()
        {
            Network.UpdateEverything += UpdateEverything;
        }

        private void UpdateEverything()
        {
            UpdateSubjects();
            UpdateAccount();
        }

        public async Task UpdateAccount()
        {
            AccountUpdated(await Network.GetUserData());
        }

        public async Task UpdateSubjects()
        {
            SubjectsUpdated(await GetData());
            RemoveOutdatedNotifications();
            UpdateSelf();
        }

        private void UpdateSelf()
        {
            bool foundUser = false;
            foreach (var id in subjects.Keys)
            {
                if (targetSubject.ID.Equals(id))
                    foundUser = true;
            }

            if (!foundUser)
                targetSubject = subjects.Values.FirstOrDefault();
        }

        private async Task<SubjectList> GetData()
        {
            var subjects = await Network.GetSubjectsData();
            return subjects;
        }

        private void RemoveOutdatedNotifications()
        {
            var ids = new List<string>();
            foreach (var n in activeNotifications)
            {
                if (!subjects.ContainsKey(n.Value.Subject.ID))
                    ids.Add(n.Key);
            }

            foreach (var id in ids)
            {
                activeNotifications.Remove(id);
            }
        }

        // TODO: Make Work
        public void PlaySound(bool isWarning)
        {
            playSound = false;
        }

        public void SetPage(int p)
        {
            page = p;
        }

        public void NewTargetSubject(string id)
        {
            targetSubject = subjects[id];
            SetPage(0);
        }

        public void SubjectsUpdated(SubjectList subjectList)
        {
            foreach (var subject in subjectList.Subjects)
            {
                UpdateSubject(subject);
                UpdateNotifications(subject);
            }

            subjects = new Dictionary<string, Subject>();
            foreach (var s in subjectList.Subjects)
            {
                subjects.Add(s.ID, s);
            }
        }

        public void UpdateNotifications(Subject s)
        {
            foreach (var n in activeNotifications.Values)
            {
                if (n.Subject.ID.Equals(s.ID))
                {
                    n.Subject = s;
                }
            }
        }

        private void UpdateSubject(Subject s)
        {
            if (targetSubject == null)
                targetSubject = s;
            else if (s.ID.Equals(targetSubject.ID))
                targetSubject = s;

            UpdateSubjectData(s);
        }

        private void UpdateSubjectData(Subject curr)
        {
            Subject old = null;
            if (subjects.ContainsKey(curr.ID))
                old = subjects[curr.ID];

            foreach (var n in curr.NotificationDatas)
            {
                BloodGlucoseNotification(old, curr, n);
            }
            BatteryNotification(old, curr);
            InsulinNotification(old, curr);
            ConnectionNotification(old, curr);

            subjects[curr.ID] = curr;
        }

        private void BloodGlucoseNotification(Subject old, Subject curr, NotificationData n)
        {
            var an = new ActiveNotification()
            {
                Subject = curr,
                Data = n,
                Active = true,
                Id = n.ThresholdType.ToString(),
            };
            Func<Subject, bool> func = default;
            if (n.ThresholdType == ThresholdType.High)
                func = (x) => x.PumpData.BloodGlucose > n.Threshold && !Outdated(x.PumpData.LastReceived);
            else 
                func = (x) => x.PumpData.BloodGlucose < n.Threshold && !Outdated(x.PumpData.LastReceived);
            HandleActiveNotificatio(an, old, curr, func);
        }

        private void BatteryNotification(Subject old, Subject curr)
        {
            var n = new NotificationData()
            {
                Title = "Battery Low",
                Note = curr.GetName() + "'s battery is low",
                Type = NotificationType.Message,
                IconClassName = "fas fa-battery-empty",
            };
            var an = new ActiveNotification()
            {
                Subject = curr,
                Data = n,
                Active = true,
                Id = "Battery",
            };

            HandleActiveNotificatio(an, old, curr, (x) => x.PumpData.BatteryStatus <= 0.5f && !Outdated(x.PumpData.LastReceived));
        }

        private int minThreshold = 30;

        private void ConnectionNotification(Subject old, Subject curr)
        {
            var n = new NotificationData()
            {
                Title = "Outdated Data",
                Note = string.Format("Last update received for {0} received {1} minutes ago", 
                    curr.GetName(), 
                    (int)DateTime.Now.Subtract(curr.PumpData.LastReceived).TotalMinutes),
                Type = NotificationType.Message,
                IconClassName = "fas fa-clock",
            };
            var an = new ActiveNotification()
            {
                Subject = curr,
                Data = n,
                Active = true,
                Id = "Connection",
            };

            HandleActiveNotificatio(an, old, curr, (x) => Outdated(x.PumpData.LastReceived));
        }

        private bool Outdated(DateTime date)
        {
            return Minutes(date) > minThreshold;
        }

        private int Minutes(DateTime lastRecieved)
        {
            var diff = DateTime.Now - lastRecieved;
            return diff.Minutes + diff.Hours * 60 + diff.Days * 1440;
        }

        private void InsulinNotification(Subject old, Subject curr)
        {
            var n = new NotificationData()
            {
                Title = "Insulin Low",
                Note = curr.GetName() + "'s pump is low on insulin",
                Type = NotificationType.Message,
                IconClassName = "fas fa-thermometer-empty",
            };
            var an = new ActiveNotification()
            {
                Subject = curr,
                Data = n,
                Active = true,
                Id = "Insulin",
            };

            HandleActiveNotificatio(an, old, curr, (x) => x.PumpData.BatteryStatus <= 0.5f && !Outdated(x.PumpData.LastReceived));
        }

        private void HandleActiveNotificatio(ActiveNotification notification, Subject old, Subject curr, Func<Subject, bool> func)
        {
            var notificationEvent = CreateOrRemoveNotification(old, curr, func);
            if (notificationEvent == NotificationEvent.Create)
            {
                if (!notification.Id.Equals("Connection") && Minutes(curr.PumpData.LastReceived) > minThreshold)
                    return;
                AddActiveNotification(notification.ToString(), notification);
            }
            else if (notificationEvent == NotificationEvent.Remove)
            {
                RemoveActiveNotification(notification);
            }
        }

        private NotificationEvent CreateOrRemoveNotification(Subject old, Subject curr, Func<Subject, bool> func)
        {
            if(old == null)
            {
                if (func(curr))
                    return NotificationEvent.Create;
                else
                    return NotificationEvent.Nothing;
            }
            else
            {
                if (!func(old) && func(curr))
                    return NotificationEvent.Create;
                else if (func(old) && !func(curr))
                    return NotificationEvent.Remove;
                else
                    return NotificationEvent.Nothing;
            }
        }

        private void AddActiveNotification(string id, ActiveNotification notification)
        {
            if (activeNotifications.ContainsKey(id))
            {
                activeNotifications[id].Active = true;
            }
            else
            {
                focusedNotifications.Push(notification);
                activeNotifications.Add(id, notification);
            }
        }

        private void RemoveActiveNotification(ActiveNotification notification)
        {
            activeNotifications.Remove(notification.ToString());
        }

        public void NotificationButtonClicked(NotificationButtonType type)
        {
            var n = focusedNotifications.Pop();
            if (type == NotificationButtonType.Dismiss)
                activeNotifications.Remove(n.ToString());

            //Remove all inactive notifications from focusedNotifications
            while (focusedNotifications.Count > 0 && focusedNotifications.Peek()?.Active == false) {
                focusedNotifications.Pop();
            }
        }

        public void NotificationClicked(ActiveNotification data)
        {
            focusedNotifications.Push(data);
        }

        public void AccountUpdated(LoginUser updatedUser) {
            loggedInUser.FirstName = updatedUser.FirstName;
            loggedInUser.LastName = updatedUser.LastName;
            loggedInUser.Email = updatedUser.Email;
            loggedInUser.UnitOfMeasure = updatedUser.UnitOfMeasure;
        }
    }
}
