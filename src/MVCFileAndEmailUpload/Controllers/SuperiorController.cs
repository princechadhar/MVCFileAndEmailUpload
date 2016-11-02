using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCFileAndEmailUpload.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace MVCFileAndEmailUpload.Controllers
{
    public class SuperiorController : Controller
    {
        mvcprojectsdbContext context = null;
        IHostingEnvironment env = null;
        public SuperiorController(mvcprojectsdbContext _context, IHostingEnvironment _env)
        {
            context = _context;
            env = _env;
        }
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet]
        public IActionResult FileUpload()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FileUpload(Tblstudent S)
        {

            string CvFinalPath = env.WebRootPath + "/data/pps/";
            string PathToSave = "";
            foreach (var file in Request.Form.Files)
            {
                string FileName = file.FileName;
                string FileExt = System.IO.Path.GetExtension(file.FileName);
                //string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(file.FileName);
                PathToSave = CvFinalPath + DateTime.Now.ToString("ddMMyyhhmmss") + FileExt;
                System.IO.FileStream fs = new System.IO.FileStream(PathToSave, System.IO.FileMode.Create);
                file.CopyTo(fs);
                fs.Close();
            }
            {

                //For Email Sending
                var message = new MailMessage();
                message.From = new MailAddress("princesaim928@gmail.com", "PUGC");
                message.To.Add(new MailAddress(S.Email));
                // message.To.Add(new MailAddress("sajid0018@gmail.com"));
                message.Subject = "This is subject";
                message.Body = "<h1>Hi, " + S.FirstName + "</h1><br><br>This is email body.<br><br>-----<br>ABC Website.";
                message.IsBodyHtml = true;
                // For File Attachment..
                message.Attachments.Add(new Attachment(PathToSave));

                SmtpClient SC = new SmtpClient();
                SC.Credentials = new System.Net.NetworkCredential("princesaim928@gmail.com", "77100018");
                SC.Host = "smtp.gmail.com";
                SC.Port = 587;
                SC.EnableSsl = true;
                try
                {
                    SC.Send(message);
                }
                catch (Exception)
                {

                }
            }

            //For message sending
            String username = "sajidchadhar";
            String password = "********";
            String from = "03007710018";
            String to = "03474132928";
            String messageBody = "Hello! This is our test message from BsIT 7th Afternoon.";
            String URL = "http://Lifetimesms.com" +
            "/plain?" +
            "username=" + username +
            "&password=" + password +
            "&from=" + from +
            "&to=" + to +
            "&message=" + Uri.UnescapeDataString(messageBody);
            try
            {
                WebRequest req = WebRequest.Create(URL);
                WebResponse resp = req.GetResponse();

            }
            catch (Exception)
            {

            }

            context.Tblstudent.Add(S);
            context.SaveChanges();
            ViewBag.Message = "Data saved Successfully";
            return View();

        }
        public IActionResult Show()
        {
            ViewBag["show"] = "Sajid ali";
            return View();
        }


    }
}