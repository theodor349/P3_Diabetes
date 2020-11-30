using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using APIDataAccess.Models.Relay;

namespace APIDataAccess.DataAccess
{
    public class RelayAccess : IRelayAccess
    {
        public float GetBloodGlucose(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/entries/current");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string[] args = reader.ReadLine().Split('	');
                    return float.Parse(args[2]);
                }
            }
        }

        public float GetBatteryStatus(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/devicestatus");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    dynamic json = JsonConvert.DeserializeObject<object>(reader.ReadToEnd());
                    string status = reader.ReadLine();
                    return json[0].pump.battery.percent;
                }
            }
        }

        public string GetInsulinStatus(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/devicestatus");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);

                    dynamic json = JsonConvert.DeserializeObject<object>(reader.ReadToEnd());
                    return json[0].pump.status.status;
                }
            }
        }

        public DateTime GetLastReceived(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/entries");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string[] args = reader.ReadLine().Split('	');

                    double ticks = double.Parse(args[1]);
                    TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                    DateTime date = new DateTime(1970, 1, 1) + time;

                    return date;
                }
            }
        }

        public StatusArrow.ArrowDirection GetStatus(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/entries/current");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);

                    string[] args = reader.ReadLine().Split('	');

                    switch (args[3])
                    {
                        case "Flat": return StatusArrow.ArrowDirection.Flat;
                        case "DoubleDown": return StatusArrow.ArrowDirection.DoubleDown;
                        case "DoubleUp": return StatusArrow.ArrowDirection.DoubleUp;
                        case "FortyFiveDown": return StatusArrow.ArrowDirection.FortyFiveDown;
                        case "FortyFiveUp": return StatusArrow.ArrowDirection.FortyFiveUp;
                        case "SingleUp": return StatusArrow.ArrowDirection.SingleUp;
                        case "SingleDown": return StatusArrow.ArrowDirection.SingleDown;
                        default: return StatusArrow.ArrowDirection.Null;
                    }
                }
            }
        }
    }
}
