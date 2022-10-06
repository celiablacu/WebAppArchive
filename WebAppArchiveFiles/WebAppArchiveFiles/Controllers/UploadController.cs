using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebAppArchiveFiles.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        public HttpResponseMessage UploadFile(HttpPostedFileBase file)
        {
            HttpResponseMessage resultResponse = null;

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                    FileStream inputStream = new FileStream(_path, FileMode.Open);

                    //Send the file context to be archived as a stream

                    string url = ConfigurationManager.AppSettings["WebServiceURL"];
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";

                    byte[] fileData = null;
                    using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                    {
                        fileData = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                    }
                    request.ContentLength = fileData.Length;

                    Stream requestStream = request.GetRequestStream();
                    requestStream.Write(fileData, 0, fileData.Length);

                    WebResponse response = request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);

                    var result = reader.ReadToEnd();

                    stream.Dispose();
                    reader.Dispose();

                    //The response contains the archived file as stream
                    resultResponse = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new ByteArrayContent(Encoding.UTF8.GetBytes(result.ToString()))
                    }; 
                    
                    //User is going to be asked to download the archive
                    resultResponse.Content.Headers.ContentDisposition =
                    new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "ArchivedFile.zip"
                    };
                    resultResponse.Content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");
                    
                }
                return resultResponse;
            }
            catch
            {
                throw;
            }

        }
    }
}