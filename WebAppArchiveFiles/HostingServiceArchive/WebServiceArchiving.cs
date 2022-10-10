using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Web;

namespace Microsoft.ServiceModel.Samples
{
    public class WebServiceArchiving : IWebServiceArchiving
    {
        private Stream ConvertStringToStream(string _inputString)
        {
            byte[] bytes = Convert.FromBase64String(_inputString);
            MemoryStream stream = new MemoryStream(bytes);
            return stream;
        }
        private bool CompressFile(Stream _fileToCompress, out Stream _resultedArchive)
        {
            bool _result = true;

            try
            {
                var compressedFileStream = new MemoryStream();
                _resultedArchive = new GZipStream(
                           compressedFileStream, CompressionMode.Compress);
                _fileToCompress.CopyTo(_resultedArchive);
                // Flush to make sure all data written by compression stream.
                _resultedArchive.Flush();
                _resultedArchive.Position = 0;

            }
            catch (Exception e)
            {
                _result = false;
                _resultedArchive = ConvertStringToStream(e.Message);
            }

            return _result;
        }

        private void SaveResultsToDatabase(string _fileName, DateTime _date, decimal _duration, bool _success)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"Insert Into InterviuTabel (ID, FileName, DateOfArchive, Duration, Status) 
                        Values (@ID,@FileName,@DateOfArchive,@Duration,@Status)";

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add(new SqlParameter("ID", Guid.NewGuid()));
                    cmd.Parameters.Add(new SqlParameter("FileName", _fileName));
                    cmd.Parameters.Add(new SqlParameter("DateOfArchive", _date));
                    cmd.Parameters.Add(new SqlParameter("Duration", _duration));
                    cmd.Parameters.Add(new SqlParameter("Status", _success));
                    cmd.ExecuteScalar();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DoWork()
        {
            if (HttpContext.Current.Request.Files[0] != null)
            {
                Stream _response = null;
                bool _ok = true;
                decimal _duration = DateTime.Now.Ticks;
                _ok = CompressFile(HttpContext.Current.Request.InputStream, out _response);
                _duration -= DateTime.Now.Ticks;

                //Todo: extract file name from the stream and fix SP error
                //SaveResultsToDatabase("", DateTime.Now, _duration, _ok);

                HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                HttpContext.Current.Response.Write(_response);
            }

        }
    }
}
