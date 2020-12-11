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

            using (WebResponse response = request.GetResponse()) {
                using (Stream dataStream = response.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(dataStream)) {
                        string[] args = reader.ReadLine().Split('	');
                        return float.Parse(args[2]);
                    }
                }
            }
        }

        public float GetBatteryStatus(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/devicestatus");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse()) {
                using (Stream dataStream = response.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(dataStream)) {
                        dynamic json = JsonConvert.DeserializeObject<object>(reader.ReadToEnd());
                        string status = reader.ReadLine();
                        return json[0].pump.battery.percent;
                    }
                }
            }
        }

        public int GetInsulinStatus(string NSLink, float maxReservoir)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/devicestatus");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse()) {
                using (Stream dataStream = response.GetResponseStream()) {
                    using(StreamReader reader = new StreamReader(dataStream)) {
                        dynamic json = JsonConvert.DeserializeObject<object>(reader.ReadToEnd());
                        return ((int)json[0].pump.reservoir / (int)maxReservoir) * 100;
                    }
                }
            }
        }
   
        public DateTime GetLastReceived(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/entries");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse()) {
                using (Stream dataStream = response.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(dataStream)) {
                        string[] args = reader.ReadLine().Split('	');

                        double ticks = double.Parse(args[1]);
                        TimeSpan time = TimeSpan.FromMilliseconds(ticks);
                        DateTime date = new DateTime(1970, 1, 1) + time;

                        return date;
                    }
                }
            }
        }

        public PumpDataModel.ArrowDirection GetStatus(string NSLink)
        {
            WebRequest request = WebRequest.Create(NSLink + "/api/v1/entries/current");
            request.Credentials = CredentialCache.DefaultCredentials;

            using (WebResponse response = request.GetResponse()) {
                using (Stream dataStream = response.GetResponseStream()) {
                    using (StreamReader reader = new StreamReader(dataStream)) {

                        string[] args = reader.ReadLine().Split('	');

                        switch (args[3].Trim('"'))
                        {
                            case "Flat": return PumpDataModel.ArrowDirection.Flat;
                            case "DoubleDown": return PumpDataModel.ArrowDirection.DoubleDown;
                            case "DoubleUp": return PumpDataModel.ArrowDirection.DoubleUp;
                            case "FortyFiveDown": return PumpDataModel.ArrowDirection.FortyFiveDown;
                            case "FortyFiveUp": return PumpDataModel.ArrowDirection.FortyFiveUp;
                            case "SingleUp": return PumpDataModel.ArrowDirection.SingleUp;
                            case "SingleDown": return PumpDataModel.ArrowDirection.SingleDown;
                            default: return PumpDataModel.ArrowDirection.Null;
                        }
                    }
                }
            }
        }

        public bool ConnectionOk(string NSLink)
        {
            try {
                WebRequest request = WebRequest.Create(NSLink + "/api/v1/status.json");
                request.Credentials = CredentialCache.DefaultCredentials;

                using (WebResponse response = request.GetResponse()) {
                    using (Stream dataStream = response.GetResponseStream()) {
                        using (StreamReader reader = new StreamReader(dataStream)) {
                            dynamic json = JsonConvert.DeserializeObject<object>(reader.ReadToEnd());
                            return ((string)json["status"]).Equals("ok");
                        }
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}