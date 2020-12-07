using Microsoft.AspNetCore.Components;
using PWA.Models;
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
                //UpdateNotifications(subject);
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
                func = (x) => x.PumpData.BloodGlucose > n.Threshold;
            else 
                func = (x) => x.PumpData.BloodGlucose < n.Threshold;
            HandleActiveNotificatio(an, old, curr, func);
        }

        private void HandleActiveNotificatio(ActiveNotification notification, Subject old, Subject curr, Func<Subject, bool> func)
        {
            var notificationEvent = CreateOrRemoveNotification(old, curr, func);
            if (notificationEvent == NotificationEvent.Create)
            {
                AddActiveNotification(notification.ToString(), notification);
            }
            else if (notificationEvent == NotificationEvent.Remove)
            {
                RemoveActiveNotification(notification);
            }
        }

        private void BatteryNotification(Subject old, Subject curr)
        {
            var n = new NotificationData()
            {
                Title = "Battery Low",
                Note = curr.GetName() + "'s battery is low",
                Type = NotificationType.Message,
                IconClassName = "fa fa-battery-0",
            };
            var an = new ActiveNotification()
            {
                Subject = curr,
                Data = n,
                Active = true,
                Id = "Battery",
            };

            HandleActiveNotificatio(an, old, curr, (x) => x.PumpData.BatteryStatus <= 0.5f);
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
                Console.WriteLine("Added: " + id);
                focusedNotifications.Push(notification);
                activeNotifications.Add(id, notification);
            }
        }

        private void RemoveActiveNotification(ActiveNotification notification)
        {
            Console.WriteLine("Remove: " + notification.ToString());
            activeNotifications.Remove(notification.ToString());
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
