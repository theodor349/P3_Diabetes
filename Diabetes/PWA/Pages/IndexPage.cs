using Microsoft.AspNetCore.Components;
using PWA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWA.Pages
{
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

        // TODO: Make Work
        public void PlaySound(bool isWarning)
        {
            playSound = false;
        }

        public void SetPage(int p)
        {
            page = p;
        }

        public void SubjectsUpdated(SubjectList subjectList)
        {
            foreach (var subject in subjectList.Subjects)
            {
                Console.WriteLine(subject);
                UpdateSubject(subject);
                UpdateNotifications(subject);
            }
        }

        private void UpdateSubject(Subject s)
        {
            if (targetSubject == null)
                targetSubject = s;
            else if (s.ID.Equals(targetSubject.ID))
                targetSubject = s;

            if (subjects.ContainsKey(s.ID))
                subjects[s.ID] = s;
            else
                subjects.Add(s.ID, s);
        }

        public void NewTargetSubject(string id)
        {
            targetSubject = subjects[id];
            SetPage(0);
        }


        // Notification
        private void UpdateNotifications(Subject s)
        {
            foreach (var n in s.NotificationDatas)
            {
                var notification = new ActiveNotification() { Subject = s, Data = n };
                var id = notification.ToString();
                if (n.ThresholdType == ThresholdType.High)
                {
                    if (n.Threshold <= s.PumpData.BloodGlucose)
                    {
                        if (!activeNotifications.ContainsKey(id))
                            AddActiveNotification(id, notification);
                    }
                    else
                    {
                        if (activeNotifications.ContainsKey(id))
                            DeactiveActiveNotification(notification);
                    }
                }
                else
                {
                    if (n.Threshold >= s.PumpData.BloodGlucose)
                    {
                        if (!activeNotifications.ContainsKey(id))
                            AddActiveNotification(id, notification);
                    }
                    else
                    {
                        if (activeNotifications.ContainsKey(id))
                            DeactiveActiveNotification(notification);
                    }
                }
            }

            AddStandardNotifications(s);
        }

        private void AddStandardNotifications(Subject s)
        {
            AddBattery(s);
            AddInsulin(s);
        }

        private void AddBattery(Subject s)
        {
            var n = new NotificationData()
            {
                Title = "Battery Low",
                Note = s.GetName() + "'s battery is low",
                Type = NotificationType.Message,
                IconClassName = "fa fa-battery-0",
            };
            var an = new ActiveNotification()
            {
                Subject = s,
                Data = n,
                Active = true,
            };
            if (s.PumpData.BatteryStatus <= 0.05)
                AddActiveNotification(an.ToString(), an);
            else
                DeactiveActiveNotification(an);
        }

        private void AddInsulin(Subject s)
        {
            var n = new NotificationData()
            {
                Title = "Insulin Low",
                Note = s.GetName() + "'s pump is low on insulin",
                Type = NotificationType.Message,
                IconClassName = "fa fa-thermometer-half",
            };
            var an = new ActiveNotification()
            {
                Subject = s,
                Data = n,
                Active = true,
            };
            if (s.PumpData.BatteryStatus <= 0.05)
                AddActiveNotification(an.ToString(), an);
            else
                DeactiveActiveNotification(an);
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

        private void DeactiveActiveNotification(ActiveNotification notification)
        {
            notification.Active = false;
        }

        public void NotificationButtonClicked(NotificationButtonType type)
        {
            var n = focusedNotifications.Pop();
            if (type == NotificationButtonType.Dismiss)
                activeNotifications.Remove(n.ToString());

            if (focusedNotifications.Count != 0 && focusedNotifications.Peek()?.Active == false)
                focusedNotifications.Pop();
        }

        public void NotificationClicked(ActiveNotification data)
        {
            focusedNotifications.Push(data);
        }
    }
}
