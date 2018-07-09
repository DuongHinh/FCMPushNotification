using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;

namespace FCMPushNotification.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route("sendmessage")]
        public IHttpActionResult SendMessage()
        {

            var data = new
            {
                to = "BOF1Zo_js1NYkY_cvneGSUxQPFOFbxb_brYj2s2kBOPrD6DIOnH3sMkpFZnJvGeJmzNscXTzLihdIZ4uVSywqpM",
                data = new {
                    message = "Your message",
                    name = "Your name",
                    id = 1,
                    status = true

                }
            };

            SendNotification(data);

            return Ok();
        }

        public void SendNotification(object data)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                string server_api_key = ConfigurationManager.AppSettings["SERVER_API_KEY"];
                var sender_id = ConfigurationManager.AppSettings["SENDER_ID"];
                // Create Request
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");     // FCM link
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add($"Authorization: key ={server_api_key}");     //Server Api Key Header
                tRequest.Headers.Add($"Sender: id ={sender_id}");     // Sender Id Header
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                String sResponseFromServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
