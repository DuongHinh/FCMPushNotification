using Newtonsoft.Json;
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
            //device token, if multiple device token split by ','
            var deviceTokens = "fB1LI8dwZVo:APA91bHdxC6kd4KFMvhFIpyyn1hNMS2dZVDVScBgGwWe1E8sVSAv9YnYhXNxG5lvXXvyc-MKj8sk7hL_H79tbF05UcCg_d_tAzAyAUuNZ_Tc71Ae3_nv8AZPhlb0jQa0r62nxoig8LNq7gEmZCtgl7C3DbQbFGOBTA";
            var data = new
            {    
                to = deviceTokens,
                data = new {
                    message = "Firebase Notifications",
                    status = true
                }
            };

            object jsonResponse = null;

            var response = SendNotification(data);

            if (response != null)
            {
                jsonResponse = JsonConvert.DeserializeObject<object>(response);
            }

            return Ok(jsonResponse);
        }

        public string SendNotification(object data)
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(data);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);

            var server_api_key = ConfigurationManager.AppSettings["FCM_SeverApiKey"].ToString();
            var sender_id = ConfigurationManager.AppSettings["FCM_SenderId"].ToString();
            var uriSendNotification = ConfigurationManager.AppSettings["FCM_UriSendNotification"].ToString();

            // Create Request
            var request = WebRequest.Create(uriSendNotification) as HttpWebRequest;     // FCM link

            if (request != null)
            {
                request.KeepAlive = true;
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add($"Authorization: key ={server_api_key}");     //Server Api Key Header
                request.Headers.Add($"Sender: id ={sender_id}");     // Sender Id Header
                request.ContentLength = byteArray.Length;

                string responseFromServer = null;

                try
                {
                    using (var dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseFromServer = reader.ReadToEnd();
                        }
                    }


                }
                catch (WebException ex)
                {

                    throw new WebException(ex.Message);
                }

                return responseFromServer;
            }

            return null;
        }
    }
}
