using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            string video_id = "VIDEO_ID";          // This should be obtained from DB
            string api_secret = System.Web.Configuration.WebConfigurationManager.AppSettings["VdoCipher_API_Key"]; ;
            string uri = "https://api.vdocipher.com/v2/otp/?video=" + video_id;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII))
            {
                writer.Write("clientSecretKey=" + api_secret);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            dynamic otp_data;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string json_otp = reader.ReadToEnd();
                otp_data = JObject.Parse(json_otp);
            }
            ViewBag.otp = otp_data.otp;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}