using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;


namespace eFaxRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            string stlhost = @"http://10.184.57.34/efax/"; //@"http://localhost:9796/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(stlhost);
            string st_uri = stlhost + @"api/InboundFax/"; //@"api/InboundFax/Receive"                        
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
           
            FileStream stream = new FileStream(@"C:\Users\kyuan\Projects\efaxChecker\rawin.txt", FileMode.Open);
            //byte[] arrayxml = File.ReadAllBytes(@"C:\Users\kyuan\Projects\efaxChecker\rawin.xml");
            MediaTypeFormatter xmlFormatter = new XmlMediaTypeFormatter();                        
            xmlFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
            

            try
            {
                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, st_uri);
                //StreamContent streamContent = new StreamContent(stream);
                //request.Content = streamContent;
                //request.Headers.TransferEncodingChunked = true;
                //HttpResponseMessage response = (client.PostAsync(st_uri, streamContent)).Result;

                ObjectContent hc_new = new ObjectContent<FileStream>(stream, xmlFormatter);
                //hc_new.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                //hc_new.Headers.ContentDisposition.FileName = "rawin.xml";
                //hc_new.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                HttpResponseMessage response = client.PostAsync(st_uri, hc_new).Result;

                var reader = response.Content.ReadAsStreamAsync().Result;

                //HttpResponseMessage response_stream = response.Content.ReadAsStreamAsync().Result;
                //using (var reader = new StreamReader(response_stream))
                //{
                //    while (!reader.EndOfStream)
                //    {
                //        //We are ready to read the stream
                //        var currentLine = reader.ReadLine();
                //        Console.WriteLine(currentLine);
                //        Console.WriteLine("pause");
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
        }
    }
}
