using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Text;

namespace DLL
{
    [ComVisible(true)]
    public partial class Methode
    {
        #region variablen

        #endregion

        public string HttpPostpy(string url, string key, string strjson)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/json";
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.Accept = "*/*";
                request.Headers.Add("Accept-Language", "en");
                request.Headers.Add("Api-Key", key);

                byte[] data = Encoding.ASCII.GetBytes(strjson);
                request.ContentLength = data.Length;
                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                return "Goed";
            }
            catch (Exception e1)
            {
                return e1.Message;
            }
        }

        public string HttpGetFile(string url, string file)
        {
            try
            {
                File.Delete(file);
            }
            catch
            {
            }
            FileStream fs1 = new FileStream(file, FileMode.Create, FileAccess.Write);
            try
            {
                WebRequest request = WebRequest.Create(url);
                // If required by the server, set the credentials.
                request.Credentials = CredentialCache.DefaultCredentials;
                HttpRequestCachePolicy Policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                request.CachePolicy = Policy;

                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                int Length = 256;
                Byte[] buffer = new Byte[Length];
                int bytesRead = dataStream.Read(buffer, 0, Length);
                // write the required bytes
                while (bytesRead > 0)
                {
                    fs1.Write(buffer, 0, bytesRead);
                    bytesRead = dataStream.Read(buffer, 0, Length);
                }
                dataStream.Close();
                fs1.Close();
                response.Close();
                Console.WriteLine("Policy is {0}.", Policy.ToString());
                Console.WriteLine("Is the response from the cache? {0}", response.IsFromCache);
                return "Goed";
            }
            catch (Exception e)
            {
                fs1.Close();
                Bericht(System.Reflection.MethodBase.GetCurrentMethod().Name + " " + e.Message);
                return e.Message;
            }
        }

        private void Bericht(string txt)
        {
            try
            {
                string log = System.IO.Path.GetTempPath() + "log.txt";
                StreamWriter sw = new StreamWriter(log, true);
                sw.WriteLine(String.Format("{0:d/M/yyyy HH:mm:ss}", DateTime.Now) + " " + txt);
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
