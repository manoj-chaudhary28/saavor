using System;
using System.Collections.Generic; 
using System.Net;
using System.Text;
using System.IO;
using System.Text.Json;

namespace saavor.Web.Services
{
    public class FCMNotifications
    {
        public FCMNotifications()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string SendFCMNotifications(string deviceId, string applicationID)
        {
            string response = string.Empty;
            try
            {
                //applicationID = "AIzaSyAGilhrBYaWbMewlU6IDwvyz4xLdrcD9rM";
                var senderId = "816399987209";
                // deviceId = "";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = "This is FCM testing",
                        title = "This is the title",
                        icon = "myicon"
                    },
                    priority = "high"
                };

                var json = JsonSerializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();

                            FCMResponse fcmResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                            if (fcmResponse.success == 1)
                            {
                                response = "Successfully sent." + " : " + sResponseFromServer;
                            }
                            else if (fcmResponse.failure == 1)
                            {
                                List<FCMResult> results = fcmResponse.results;
                                response = results[0].message_id + " : " + sResponseFromServer;
                            }

                            return response;
                            // Response.Write(sResponseFromServer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string SendBistroFCMNotifications(string deviceId, string message, string deviceType)
        {
            string response = string.Empty;
            string applicationID = "AIzaSyCvaXMfZ-YamBtxF-4Xp0UCqWGeGkwk2KE";
            var senderId = "90913733327";

            try
            {
                if (deviceType.ToUpper() == "IOS")
                {
                    applicationID = "AIzaSyAGilhrBYaWbMewlU6IDwvyz4xLdrcD9rM";
                    senderId = "816399987209";
                }
                else //for - Andriod -Bistro- updated credentials
                {
                    applicationID = "AIzaSyCvaXMfZ-YamBtxF-4Xp0UCqWGeGkwk2KE";
                    senderId = "90913733327";

                }

                // deviceId = "";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message,
                        title = "Bistro",
                        icon = "https://demosaavorapi.saavor.io/Images/notification.jpg",
                        key = "ricky"
                    },
                    priority = "high"
                };

                var json = JsonSerializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();

                            FCMResponse fcmResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                            if (fcmResponse.success == 1)
                            {
                                response = "Successfully sent." + " : " + sResponseFromServer;
                            }
                            else if (fcmResponse.failure == 1)
                            {
                                List<FCMResult> results = fcmResponse.results;
                                response = results[0].message_id + " : " + sResponseFromServer;
                            }

                            return response;
                            // Response.Write(sResponseFromServer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string SendDiscoverFCMNotifications(string deviceId, string message, string deviceType)
        {
            string response = string.Empty;
            string applicationID = "AIzaSyCvaXMfZ-YamBtxF-4Xp0UCqWGeGkwk2KE";
            var senderId = "90913733327";

            try
            {
                if (deviceType.ToUpper() == "IOS")
                {
                    applicationID = "AIzaSyAGilhrBYaWbMewlU6IDwvyz4xLdrcD9rM";
                    senderId = "816399987209";
                }
                else //for - Andriod
                {
                    applicationID = "AIzaSyCvaXMfZ-YamBtxF-4Xp0UCqWGeGkwk2KE";
                    senderId = "90913733327";
                }

                // deviceId = "";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message,
                        title = "Discover Now",
                        icon = "https://demosaavorapi.saavor.io/Images/notification.jpg",
                        key = "ricky"
                    },
                    priority = "high"
                };

                var json = JsonSerializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();

                            FCMResponse fcmResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                            if (fcmResponse.success == 1)
                            {
                                response = "Successfully sent." + " : " + sResponseFromServer;
                            }
                            else if (fcmResponse.failure == 1)
                            {
                                List<FCMResult> results = fcmResponse.results;
                                response = results[0].message_id + " : " + sResponseFromServer;
                            }

                            return response;
                            // Response.Write(sResponseFromServer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string SendUserFCMNotifications(string deviceId, string message, string deviceType)
        {
            // deviceId = "fEDxq9w6hnc:APA91bFUCyP50kFmHzINvYi8M-Raa72JJ7vKFmKiByckp9uqEDQyzGo1oKGIPfLvbls8dPmoxu7MN4P9XhxUV8YhGwgHvC4yPrmFbLckLTeyRUoci0g3aC8PCMRgDE2-xEr2ASjVLuvr";
            string response = string.Empty;
            //string applicationID = "AIzaSyD5nsOOBxXYkveK9pu6bDywp75uM3C0HXE";
            //var senderId = "210814977907";
            string applicationID = "AAAAJpLfqrk:APA91bHdeHHbvY1_I13Ghxei4HQwyHI5fNdTuJk_2JvX4AR9YGT3YSPNSW2OuHYAa2-bdjGYvh-2cuFVjq62ldpwWqcaiAFLXPz-lj2gJPkCNSX-WQ3JtKnEUQZGopRFeb16tli-6WAF";
            var senderId = "165672889017";

            try
            {
                if (deviceType.ToUpper() == "IOS")
                {
                    applicationID = "AIzaSyAGilhrBYaWbMewlU6IDwvyz4xLdrcD9rM";
                    senderId = "816399987209";
                }
                else //for - Andriod
                {
                    //applicationID = "AIzaSyD5nsOOBxXYkveK9pu6bDywp75uM3C0HXE";
                    //senderId = "210814977907";
                    applicationID = "AAAAJpLfqrk:APA91bHdeHHbvY1_I13Ghxei4HQwyHI5fNdTuJk_2JvX4AR9YGT3YSPNSW2OuHYAa2-bdjGYvh-2cuFVjq62ldpwWqcaiAFLXPz-lj2gJPkCNSX-WQ3JtKnEUQZGopRFeb16tli-6WAF";
                    senderId = "165672889017";
                }

                // deviceId = "";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message,
                        title = "Saavor",
                        icon = "https://demosaavorapi.saavor.io/Images/Logo_Saavor_App.png",
                        key = "ricky"
                    },
                    priority = "high"
                };
                                 
                var json = JsonSerializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();

                            FCMResponse fcmResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                            if (fcmResponse.success == 1)
                            {
                                response = "Successfully sent." + " : " + sResponseFromServer;
                            }
                            else if (fcmResponse.failure == 1)
                            {
                                List<FCMResult> results = fcmResponse.results;
                                response = results[0].message_id + " : " + sResponseFromServer;
                            }

                            return response;
                            // Response.Write(sResponseFromServer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
        public string SendPUCFCMNotifications(string deviceId, string message)
        {
            string response = string.Empty;
            string applicationID = "AIzaSyAR2vS5wvCgEW1BAJOlR3NlzdoAG-y68BY";
            var senderId = "509854333704";

            try
            {


                // deviceId = "";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    to = deviceId,
                    notification = new
                    {
                        body = message,
                        title = "PUC",
                        //icon = "http://saavor.co/Images/notification.jpg",
                        key = "ricky"
                    },
                    priority = "high"
                };

                var json = JsonSerializer.Serialize(data); 
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;

                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            String sResponseFromServer = tReader.ReadToEnd();

                            FCMResponse fcmResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<FCMResponse>(sResponseFromServer);
                            if (fcmResponse.success == 1)
                            {
                                response = "Successfully sent." + " : " + sResponseFromServer;
                            }
                            else if (fcmResponse.failure == 1)
                            {
                                List<FCMResult> results = fcmResponse.results;
                                response = results[0].message_id + " : " + sResponseFromServer;
                            }

                            return response;
                            // Response.Write(sResponseFromServer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
    public class FCMResponse
    {
        public long multicast_id { get; set; }
        public int success { get; set; }
        public int failure { get; set; }
        public int canonical_ids { get; set; }
        public List<FCMResult> results { get; set; }
    }
    public class FCMResult
    {
        public string message_id { get; set; }
    }

    public class NotificationRequests
    {
        public int ProfileId { get; set; }
        public string deviceToken { get; set; }
        public string message { get; set; }
        public string deviceType { get; set; }
    }
}
