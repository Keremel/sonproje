using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ServiceReference1;
using finansbankServiceReference;
namespace gncerp.Services
{
    public class bankServices
    {

        public async void getgar()
        {
            SecureToken secureToken = new SecureToken();
            secureToken.UserId = "GENCPA";
            secureToken.Encoded = "m2ApSMh0M+tGwcl/R+Wxrg==";

            FirmAccountActivity firmAccountActivity = new FirmAccountActivity();
            firmAccountActivity.securetoken = secureToken;

            firmAccountActivity.FirmCode = "275399";
            firmAccountActivity.BranchNum = "723";
            firmAccountActivity.AccountNum = "6297209";
            firmAccountActivity.StartDate = "2020-09-30-00.00.00.000000";
            firmAccountActivity.EndDate = "2020-09-30-23.59.59.999999";




            FirmAccountActivitySoapClient firmAccountActivitySoapClient = new FirmAccountActivitySoapClient();

            FirmAccountActivityResponse1 firmAccountActivityResponse1 = await firmAccountActivitySoapClient.FirmAccountActivityAsync(firmAccountActivity);

          
           
        }
        public string getziraataccountactivity(string no)
        {
            string url = "https://hesap.ziraatbank.com.tr/HEK_NKYWS/HesapHareketleri.asmx?wsdl";

            // Create the SOAP envelope
            XmlDocument soapEnvelopeXml = new XmlDocument();
            var xmlStr = string.Format(@"
              <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:tem=""http://tempuri.org/"">
             <soap:Header/>
             <soap:Body>
              <tem:HesapEkstre>
              <tem:KurumKod>GENCPA</tem:KurumKod>
              <tem:Sifre>Lzz37XfOGO</tem:Sifre>
              <tem:HesapNo>{0}</tem:HesapNo>
              <tem:BaslangicTarihi>{1}</tem:BaslangicTarihi>
              <tem:BitisTarihi>{1}</tem:BitisTarihi>
              </tem:HesapEkstre>
              </soap:Body>
             </soap:Envelope>", no,DateTime.Now.ToString("yyyy.MM.dd"));

            soapEnvelopeXml.LoadXml(xmlStr);

            //  Create the web request
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            //webRequest.Headers.Add("Authorization", "Basic " + encoded);
            //// Insert SOAP envelope
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
            catch (System.Exception e)
            {
                result = e.Message;
            }




            return result;
        }
      
        public string getgaranticcountmove()
        {
            string url = "https://inboundrstintws.garanti.com.tr/services/FirmAccountActivitySoap?wsdl";

            // Create the SOAP envelope
            XmlDocument soapEnvelopeXml = new XmlDocument();
            var xmlStr = string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:odem=""http://odemeler.garanti.com.tr/"">
   <soapenv:Header/>
   <soapenv:Body>
      <odem:FirmAccountActivity>
         <odem:securetoken>
            <odem:UserId>INTUDE</odem:UserId>
            <odem:Password></odem:Password>
            <odem:CreateTimestamp></odem:CreateTimestamp>
            <odem:Encoded>rpLUdKy3cUt/nVwvehJRRA==</odem:Encoded>
         </odem:securetoken>
         <odem:FirmCode>50953</odem:FirmCode>
         <odem:StartDate>2010-08-01-09.00.00.000001</odem:StartDate>
         <odem:EndDate>2010-08-03-19.00.00.000001</odem:EndDate>
         <odem:BranchNum> 999 </odem:BranchNum>
         <odem:AccountNum> 6999999</odem:AccountNum>
         <odem:IBAN></odem:IBAN>
         <odem:TransactionId></odem:TransactionId>
      </odem:FirmAccountActivity>
   </soapenv:Body>
</soapenv:Envelope>");

            soapEnvelopeXml.LoadXml(xmlStr);

            //  Create the web request
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            //webRequest.Headers.Add("Authorization", "Basic " + encoded);
            //// Insert SOAP envelope
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
            catch (System.Exception e)
            {
                result = e.Message;
            }




            return result;
        }

        public string getgaranticcountmove2()
        {
            string url = "https://inboundrstintws.garanti.com.tr/services/FirmAccountActivitySoap?wsdl";

            // Create the SOAP envelope
            XmlDocument soapEnvelopeXml = new XmlDocument();
            var xmlStr = string.Format(@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:odem=""http://odemeler.garanti.com.tr/"">
             <soapenv:Header/>
               <soapenv:Body>
               <odem:FirmAccountActivity>
                 <odem:securetoken>
                  <odem:UserId>GENCPA</odem:UserId>
                     <odem:Password></odem:Password>
                      <odem:CreateTimestamp></odem:CreateTimestamp>
                   <odem:Encoded>m2ApSMh0M+tGwcl/R+Wxrg==</odem:Encoded>
                   </odem:securetoken>
                  <odem:FirmCode>275399</odem:FirmCode>
                   <odem:StartDate>2020-09-24-09</odem:StartDate>
                    <odem:EndDate>2020-09-24-30</odem:EndDate>
                   <odem:BranchNum>723</odem:BranchNum>
                   <odem:AccountNum>6297209</odem:AccountNum>
                   <odem:IBAN></odem:IBAN>
                    <odem:TransactionId></odem:TransactionId>
                    </odem:FirmAccountActivity>
                    </soapenv:Body>
                 </soapenv:Envelope>");

            soapEnvelopeXml.LoadXml(xmlStr);

            //  Create the web request
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            //webRequest.Headers.Add("Authorization", "Basic " + encoded);
            //// Insert SOAP envelope
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }

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
            catch (System.Exception e)
            {
                result = e.Message;
            }




            return result;
        }




    }
}
