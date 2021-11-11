using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates; 
using System.IO; 
using System.Security.Authentication;
using saavor.Shared.ViewModel;
using Microsoft.AspNetCore.Hosting;

namespace saavor.Web.Services
{
    public class APNSNotification
    {
        /// <summary>
        /// environment
        /// </summary>
        private static IWebHostEnvironment _environment;
        /// <summary>
        /// APNSNotification
        /// </summary>
        /// <param name="envInitiator"></param>
        public APNSNotification(IWebHostEnvironment envInitiator)
        {
            _environment = envInitiator;
        }

        public static NotificationResponse ApnsNotificationDev(string deviceToken, string message)
        {
            NotificationResponse serviceResponse = new NotificationResponse();
            serviceResponse.ReturnCode = "0";
            serviceResponse.ReturnMessage = "Notification not sent.";

            string response = "Sent successfully.";
            try
            {

                //ApplePushNotification obj = new ApplePushNotification();
                //obj.sendMsg(deviceToken);

                int port = 2195;
                String hostname = "gateway.sandbox.push.apple.com";
                //hostname = "gateway.push.apple.com";
                string p12FileLocation = GetFileLocationDev();
                //string certificatePath = Server.MapPath("final.p12");

                string certificatePath = p12FileLocation;

                string certificatePassword = "Bistro@123";

                X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(
                                client.GetStream(),
                                false,
                                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                                null
                );

                try
                {
                    sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
                }
                catch (AuthenticationException ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Console.WriteLine("Authentication failed");
                    //client.Close();
                    // Request.SaveAs(Server.MapPath("Authenticationfailed.txt"), true);
                    // return;
                }
                //// Encode a test message into a byte array.
                MemoryStream memoryStream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(memoryStream);

                writer.Write((byte)0);  //The command
                writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
                writer.Write((byte)32); //The deviceId length (big-endian second byte)

                byte[] b0 = HexString2Bytes(deviceToken);
                WriteMultiLineByteArray(b0);

                writer.Write(b0);
                String payload;
                string strmsgbody = "";
                int totunreadmsg = 1;
                strmsgbody = message;

                //   Debug.WriteLine("during testing via device!");
                // Request.SaveAs(Server.MapPath("APNSduringdevice.txt"), true);

                ///payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";
                ///
                payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";


                writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
                writer.Write((byte)payload.Length);     //payload length (big-endian second byte)

                byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                writer.Write(b1);
                writer.Flush();

                byte[] array = memoryStream.ToArray();
                //  Debug.WriteLine("This is being sent...\n\n");
                //  Debug.WriteLine(array);
                try
                {
                    sslStream.Write(array);
                    sslStream.Flush();
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Debug.WriteLine("Write failed buddy!!");
                    // Request.SaveAs(Server.MapPath("Writefailed.txt"), true);
                }

                client.Close();
                //Debug.WriteLine("Client closed.");

                string emailBody = message + " and DeviceToken is : " + deviceToken;
                Exception ex11 = null;
                //EmailUtility.SendEmailNew("surander.parkeee@gmail.com", "APNS iOS", "ios Notification", emailBody, out ex11, true);


                serviceResponse.ReturnCode = "1";
                serviceResponse.ReturnMessage = response;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.ReturnCode = "-1";
                serviceResponse.ReturnMessage = ex.Message;
               // ApplicationLogger.LogError(ex, "SaavorOperation", "ApnsNotification");
                // ExceptionUtility.LogException(ex, "APNSNotification");
                return serviceResponse;
            }

        }
        public static NotificationResponse ApnsBistroNotification(string deviceToken, string message)
        {
            NotificationResponse serviceResponse = new NotificationResponse();
            serviceResponse.ReturnCode = "0";
            serviceResponse.ReturnMessage = "Notification not sent.";

            string response = "Sent successfully.";
            try
            {

                //ApplePushNotification obj = new ApplePushNotification();
                //obj.sendMsg(deviceToken);

                int port = 2195;
                String hostname = "gateway.sandbox.push.apple.com";
                hostname = "gateway.push.apple.com";
                string p12FileLocation = GetBistroFileLocation();
                //string certificatePath = Server.MapPath("final.p12");

                string certificatePath = p12FileLocation;

                string certificatePassword = "saavor@2018";

                X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(
                                client.GetStream(),
                                false,
                                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                                null
                );

                try
                {
                    sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
                }
                catch (AuthenticationException ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Console.WriteLine("Authentication failed");
                    //client.Close();
                    // Request.SaveAs(Server.MapPath("Authenticationfailed.txt"), true);
                    // return;
                }
                //// Encode a test message into a byte array.
                MemoryStream memoryStream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(memoryStream);

                writer.Write((byte)0);  //The command
                writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
                writer.Write((byte)32); //The deviceId length (big-endian second byte)

                byte[] b0 = HexString2Bytes(deviceToken);
                WriteMultiLineByteArray(b0);

                writer.Write(b0);
                String payload;
                string strmsgbody = "";
                int totunreadmsg = 1;
                strmsgbody = message;

                //   Debug.WriteLine("during testing via device!");
                // Request.SaveAs(Server.MapPath("APNSduringdevice.txt"), true);

                ///payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";
                ///
                payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";


                writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
                writer.Write((byte)payload.Length);     //payload length (big-endian second byte)

                byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                writer.Write(b1);
                writer.Flush();

                byte[] array = memoryStream.ToArray();
                //  Debug.WriteLine("This is being sent...\n\n");
                //  Debug.WriteLine(array);
                try
                {
                    sslStream.Write(array);
                    sslStream.Flush();
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Debug.WriteLine("Write failed buddy!!");
                    // Request.SaveAs(Server.MapPath("Writefailed.txt"), true);
                }

                client.Close();
                //Debug.WriteLine("Client closed.");

                string emailBody = message + " and DeviceToken is : " + deviceToken;
                Exception ex11 = null;
                //EmailUtility.SendEmailNew("surander.parkeee@gmail.com", "APNS Bistro iOS", "ios Notification", emailBody, out ex11, true);


                serviceResponse.ReturnCode = "1";
                serviceResponse.ReturnMessage = response;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.ReturnCode = "-1";
                serviceResponse.ReturnMessage = ex.Message;
               // ApplicationLogger.LogError(ex, "SaavorOperation", "ApnsNotification");
                // ExceptionUtility.LogException(ex, "APNSNotification");
                return serviceResponse;
            }

        }
        public static NotificationResponse ApnsUserNotification(string deviceToken, string message)
        {

            NotificationResponse serviceResponse = new NotificationResponse();
            serviceResponse.ReturnCode = "0";
            serviceResponse.ReturnMessage = "Notification not sent.";

            string response = "Sent successfully.";
            try
            {

                //ApplePushNotification obj = new ApplePushNotification();
                //obj.sendMsg(deviceToken);

                int port = 2195;
                String hostname = "gateway.sandbox.push.apple.com";
                hostname = "gateway.push.apple.com";
                string p12FileLocation = GetUserFileLocation();
                //string certificatePath = Server.MapPath("final.p12");

                string certificatePath = p12FileLocation;

                string certificatePassword = "saavor@2021";

                X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(
                                client.GetStream(),
                                false,
                                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                                null
                );

                try
                {
                    sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
                }
                catch (AuthenticationException ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Console.WriteLine("Authentication failed");
                    //client.Close();
                    // Request.SaveAs(Server.MapPath("Authenticationfailed.txt"), true);
                    // return;
                }
                //// Encode a test message into a byte array.
                MemoryStream memoryStream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(memoryStream);

                writer.Write((byte)0);  //The command
                writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
                writer.Write((byte)32); //The deviceId length (big-endian second byte)

                byte[] b0 = HexString2Bytes(deviceToken);
                WriteMultiLineByteArray(b0);

                writer.Write(b0);
                String payload;
                string strmsgbody = "";
                int totunreadmsg = 1;
                strmsgbody = message;

                //   Debug.WriteLine("during testing via device!");
                // Request.SaveAs(Server.MapPath("APNSduringdevice.txt"), true);

                ///payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";
                ///
                payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";


                writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
                writer.Write((byte)payload.Length);     //payload length (big-endian second byte)

                byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                writer.Write(b1);
                writer.Flush();

                byte[] array = memoryStream.ToArray();
                //  Debug.WriteLine("This is being sent...\n\n");
                //  Debug.WriteLine(array);
                try
                {
                    sslStream.Write(array);
                    sslStream.Flush();
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Debug.WriteLine("Write failed buddy!!");
                    // Request.SaveAs(Server.MapPath("Writefailed.txt"), true);
                }

                client.Close();
                //Debug.WriteLine("Client closed.");

                string emailBody = message + " and DeviceToken is : " + deviceToken;
                Exception ex11 = null;
                //EmailUtility.SendEmailNew("surander.parkeee@gmail.com", "APNS User iOS", "ios Notification", emailBody, out ex11, true);


                serviceResponse.ReturnCode = "1";
                serviceResponse.ReturnMessage = response;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.ReturnCode = "-1";
                serviceResponse.ReturnMessage = ex.Message;
                //ApplicationLogger.LogError(ex, "SaavorOperation", "ApnsNotification");
                // ExceptionUtility.LogException(ex, "APNSNotification");
                return serviceResponse;
            }

        }
        public static NotificationResponse ApnsDiscoverNotification(string deviceToken, string message)
        {

            NotificationResponse serviceResponse = new NotificationResponse();
            serviceResponse.ReturnCode = "0";
            serviceResponse.ReturnMessage = "Notification not sent.";

            string response = "Sent successfully.";
            try
            {

                //ApplePushNotification obj = new ApplePushNotification();
                //obj.sendMsg(deviceToken);

                int port = 2195;
                String hostname = "gateway.sandbox.push.apple.com";
                hostname = "gateway.push.apple.com";
                string p12FileLocation = GetDiscoverFileLocation();
                //string certificatePath = Server.MapPath("final.p12");

                string certificatePath = p12FileLocation;

                //string certificatePassword = "Bistro@123";
                string certificatePassword = "saavor@2018";


                X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(
                                client.GetStream(),
                                false,
                                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                                null
                );

                try
                {
                    sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
                }
                catch (AuthenticationException ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Console.WriteLine("Authentication failed");
                    //client.Close();
                    // Request.SaveAs(Server.MapPath("Authenticationfailed.txt"), true);
                    // return;
                }
                //// Encode a test message into a byte array.
                MemoryStream memoryStream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(memoryStream);

                writer.Write((byte)0);  //The command
                writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
                writer.Write((byte)32); //The deviceId length (big-endian second byte)

                byte[] b0 = HexString2Bytes(deviceToken);
                WriteMultiLineByteArray(b0);

                writer.Write(b0);
                String payload;
                string strmsgbody = "";
                int totunreadmsg = 1;
                strmsgbody = message;

                //   Debug.WriteLine("during testing via device!");
                // Request.SaveAs(Server.MapPath("APNSduringdevice.txt"), true);

                ///payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";
                ///
                payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";


                writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
                writer.Write((byte)payload.Length);     //payload length (big-endian second byte)

                byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                writer.Write(b1);
                writer.Flush();

                byte[] array = memoryStream.ToArray();
                //  Debug.WriteLine("This is being sent...\n\n");
                //  Debug.WriteLine(array);
                try
                {
                    sslStream.Write(array);
                    sslStream.Flush();
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Debug.WriteLine("Write failed buddy!!");
                    // Request.SaveAs(Server.MapPath("Writefailed.txt"), true);
                }

                client.Close();
                //Debug.WriteLine("Client closed.");

                string emailBody = message + " and DeviceToken is : " + deviceToken;
                Exception ex11 = null;
                //EmailUtility.SendEmailNew("surander.parkeee@gmail.com", "APNS Discover iOS", "ios Notification", emailBody, out ex11, true);


                serviceResponse.ReturnCode = "1";
                serviceResponse.ReturnMessage = response;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.ReturnCode = "-1";
                serviceResponse.ReturnMessage = ex.Message;
               // ApplicationLogger.LogError(ex, "SaavorOperation", "ApnsNotification");
                // ExceptionUtility.LogException(ex, "APNSNotification");
                return serviceResponse;
            }

        }
        public static NotificationResponse ApnsNotification(string deviceToken, string message)
        {

            NotificationResponse serviceResponse = new NotificationResponse();
            serviceResponse.ReturnCode = "0";
            serviceResponse.ReturnMessage = "Notification not sent.";

            string response = "Sent successfully.";
            try
            {

                //ApplePushNotification obj = new ApplePushNotification();
                //obj.sendMsg(deviceToken);

                int port = 2195;
                String hostname = "gateway.sandbox.push.apple.com";
                hostname = "gateway.push.apple.com";
                string p12FileLocation = GetFileLocation();
                //string certificatePath = Server.MapPath("final.p12");

                string certificatePath = p12FileLocation;

                string certificatePassword = "Indian@123";

                X509Certificate2 clientCertificate = new X509Certificate2(certificatePath, certificatePassword, X509KeyStorageFlags.MachineKeySet);
                X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

                TcpClient client = new TcpClient(hostname, port);
                SslStream sslStream = new SslStream(
                                client.GetStream(),
                                false,
                                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                                null
                );

                try
                {
                    sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Default, false);
                }
                catch (AuthenticationException ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Console.WriteLine("Authentication failed");
                    //client.Close();
                    // Request.SaveAs(Server.MapPath("Authenticationfailed.txt"), true);
                    // return;
                }
                //// Encode a test message into a byte array.
                MemoryStream memoryStream = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(memoryStream);

                writer.Write((byte)0);  //The command
                writer.Write((byte)0);  //The first byte of the deviceId length (big-endian first byte)
                writer.Write((byte)32); //The deviceId length (big-endian second byte)

                byte[] b0 = HexString2Bytes(deviceToken);
                WriteMultiLineByteArray(b0);

                writer.Write(b0);
                String payload;
                string strmsgbody = "";
                int totunreadmsg = 1;
                strmsgbody = message;

                //   Debug.WriteLine("during testing via device!");
                // Request.SaveAs(Server.MapPath("APNSduringdevice.txt"), true);

                ///payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";
                ///
                payload = "{\"aps\":{\"alert\":\"" + strmsgbody + "\",\"badge\":" + totunreadmsg.ToString() + ",\"sound\":\"mailsent.wav\"},\"acme1\":\"bar\",\"acme2\":42}";


                writer.Write((byte)0); //First byte of payload length; (big-endian first byte)
                writer.Write((byte)payload.Length);     //payload length (big-endian second byte)

                byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
                writer.Write(b1);
                writer.Flush();

                byte[] array = memoryStream.ToArray();
                //  Debug.WriteLine("This is being sent...\n\n");
                //  Debug.WriteLine(array);
                try
                {
                    sslStream.Write(array);
                    sslStream.Flush();
                }
                catch (Exception ex)
                {
                    response = ex.Message;
                    serviceResponse.ReturnMessage = ex.Message;
                    //Debug.WriteLine("Write failed buddy!!");
                    // Request.SaveAs(Server.MapPath("Writefailed.txt"), true);
                }

                client.Close();
                //Debug.WriteLine("Client closed.");

                string emailBody = message + " and DeviceToken is : " + deviceToken;
                Exception ex11 = null;
                // EmailUtility.SendEmailNew("surander.parkeee@gmail.com", "APNS iOS", "ios Notification", emailBody, out ex11, true);


                serviceResponse.ReturnCode = "1";
                serviceResponse.ReturnMessage = response;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.ReturnCode = "-1";
                serviceResponse.ReturnMessage = ex.Message;
              //  ApplicationLogger.LogError(ex, "SaavorOperation", "ApnsNotification");
                // ExceptionUtility.LogException(ex, "APNSNotification");
                return serviceResponse;
            }

        }

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        public static void WriteMultiLineByteArray(byte[] bytes)
        {
            const int rowSize = 20;
            int iter;

            Console.WriteLine("initial byte array");
            Console.WriteLine("------------------");

            for (iter = 0; iter < bytes.Length - rowSize; iter += rowSize)
            {
                Console.Write(
                    BitConverter.ToString(bytes, iter, rowSize));
                Console.WriteLine("-");
            }

            Console.WriteLine(BitConverter.ToString(bytes, iter));
            Console.WriteLine();
        }
        private static byte[] HexString2Bytes(string hexString)
        {
            //check for null
            if (hexString == null) return null;
            //get length
            int len = hexString.Length;
            if (len % 2 == 1) return null;
            int len_half = len / 2;
            //create a byte array
            byte[] bs = new byte[len_half];
            try
            {
                //convert the hexstring to bytes
                for (int i = 0; i != len_half; i++)
                {
                    bs[i] = (byte)Int32.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception : " + ex.Message);
            }
            //return the byte array
            return bs;
        }
        public static string GetFileLocationDev()
        {

            string filePath = "";
            //string fileName = System.Configuration.ConfigurationManager.AppSettings["p12file"];
            string fileName = "bistro_dev.p12";

            string root = string.Empty;
            try
            {
                root = Path.Combine(_environment.WebRootPath, "certificate");
            }
            catch
            {
                root = AppDomain.CurrentDomain.BaseDirectory;
                root = root + "P12File\\";

                //root = System.Web.HttpContext.Current.Server.MapPath("..");
            }
            filePath = root + fileName;
            return filePath;
        }
        public static string GetFileLocation()
        {

            string filePath = "";
            //string fileName = System.Configuration.ConfigurationManager.AppSettings["p12file"];
            string fileName = "r4s_pro.p12";


            string root = string.Empty;
            try
            {
                //System.Web.Hosting.HostingEnvironment.MapPath("~/Owner/");
                // string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Owner/AppUsers/");

                root = Path.Combine(_environment.WebRootPath, "certificate");

            }
            catch
            {
                root = AppDomain.CurrentDomain.BaseDirectory;
                root = root + "P12File\\";

                //root = System.Web.HttpContext.Current.Server.MapPath("..");
            }
            filePath = root + fileName;
            return filePath;
        }
        public static string GetBistroFileLocation()
        {

            string filePath = "";
            //string fileName = System.Configuration.ConfigurationManager.AppSettings["p12file"];
            string fileName = "BistroProAPNS.p12";


            string root = string.Empty;
            try
            {
                //System.Web.Hosting.HostingEnvironment.MapPath("~/Owner/");
                // string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Owner/AppUsers/");

                root = Path.Combine(_environment.WebRootPath, "certificate");

            }
            catch
            {
                root = AppDomain.CurrentDomain.BaseDirectory;
                root = root + "P12File\\";

                //root = System.Web.HttpContext.Current.Server.MapPath("..");
            }
            filePath = root + fileName;
            return filePath;
        }
        public static string GetDiscoverFileLocation()
        {

            string filePath = "";
            //string fileName = System.Configuration.ConfigurationManager.AppSettings["p12file"];
            // string fileName = "bistro_dev.p12";
            string fileName = "DiscoverProAPNS.p12";


            string root = string.Empty;
            try
            {
               
                root = Path.Combine(_environment.WebRootPath, "certificate");
            }
            catch
            {
                root = AppDomain.CurrentDomain.BaseDirectory;
                root = root + "P12File\\";

                //root = System.Web.HttpContext.Current.Server.MapPath("..");
            }
            filePath = root + fileName;
            return filePath;
        }
        public static string GetUserFileLocation()
        {

            string filePath = "";
            //string fileName = System.Configuration.ConfigurationManager.AppSettings["p12file"];
            string fileName = "saavor.p12";


            string root = string.Empty;
            try
            {
                root = Path.Combine(_environment.WebRootPath, "certificate");
            }
            catch
            {
                root = AppDomain.CurrentDomain.BaseDirectory;
                root = root + "P12File\\";

                //root = System.Web.HttpContext.Current.Server.MapPath("..");
            }
            filePath = root +"/"+ fileName;
            return filePath;
        }
    }
}