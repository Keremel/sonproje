using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

namespace gncerp.Services
{
    public class yurticikargoServices
    {
        public string getdetay(string key)
        {
            string url = "http://webservices.yurticikargo.com:8080/KOPSWebServices/ShippingOrderDispatcherServices?wsdl";


    
            XmlDocument soapEnvelopeXml = new XmlDocument();

       

            var xmlStr = string.Format(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ship='http://yurticikargo.com.tr/ShippingOrderDispatcherServices'> <soapenv:Header/>
                   <soapenv:Body>
                      <ship:queryShipment>
                         <wsUserName>1048N306926592G</wsUserName>
                         <wsPassword>MC5uu8yP632ud7jR</wsPassword>
                         <wsLanguage>TR</wsLanguage>
                         <keys>{0}</keys>
                         <keyType>1</keyType>
                         <addHistoricalData>false</addHistoricalData>
                         <onlyTracking>false</onlyTracking>
                      </ship:queryShipment >
                      </soapenv:Body>
                       </soapenv:Envelope>
                        ", key);

            soapEnvelopeXml.LoadXml(xmlStr);

            // Create the web request
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            //webRequest.Headers.Add("Authorization", "Basic " + encoded);
            // Insert SOAP envelope
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            // Send request and retrieve result
            string result;
            try
            {
                using (WebResponse response = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        result = rd.ReadToEnd();
                    }
                }

            }
            catch (Exception e)
            {
                result = e.Message;
            }




            return result;
        }
    }
}
