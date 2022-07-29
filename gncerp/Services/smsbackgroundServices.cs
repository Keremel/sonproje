using gncerp.Entities;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;


namespace gncerp.Services
{
    public class smsbackgroundServices : BackgroundService
    {
         bool issendmesajoppersin;
         bool issendmesajbayihatırlatma;
         bool ishedefhatırlatma;

        bankhelperServices _bankhelperServices = new bankhelperServices();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

          
                //int ss = DateTime.Now.Hour;

                //if (DateTime.Now.Hour != 17)
                //    issendmesajoppersin = false;

                //if (DateTime.Now.Hour == 17 && issendmesajoppersin == false)
                //{

                //    sendmesajoppersin();
                //    issendmesajoppersin = true;
                //}


                //if (DateTime.Now.DayOfWeek.ToString() == "Wednesday" || DateTime.Now.DayOfWeek.ToString() == "Friday")
                //{
                //    if (DateTime.Now.Hour != 11)
                //        issendmesajbayihatırlatma = false;

                //    if (DateTime.Now.Hour == 11 && issendmesajbayihatırlatma == false)
                //    {

                //        sendmesajbayihatırlatma();
                //        issendmesajbayihatırlatma = true;
                //    }
        
                 
                //} 

                //if (DateTime.Now.DayOfWeek.ToString() == "Thursday" || DateTime.Now.DayOfWeek.ToString() == "Monday")
                //{
                //    if (DateTime.Now.Hour != 14)
                //        ishedefhatırlatma = false;

                //    if (DateTime.Now.Hour == 14 && issendmesajbayihatırlatma == false)
                //    {

                //        hedefhatırlatma();
                //        ishedefhatırlatma = true;
                //    }
        
                 
                //}

               


                  await Task.Delay((Convert.ToInt32(ConfigurationManager.AppSettings["BackgroundServiceDelayMinute"])) * 60 , stoppingToken);
            }

        

        }

        public void sendmesajoppersin()
        {

            List<Appuser> appuserlist = string.Format("SELECT * FROM appusers au WHERE au.role='satis' and tel!=''").GetQuery<Appuser>();
            List<Aylikkarne> aylikkarne = string.Format(@"SELECT   ISNULL(tem2.bayi,0) bayi, ISNULL(SUM([Satır Miktarı]),0) AS adet, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Malzeme Kodu]) AS cesit,
       COUNT(DISTINCT [Marka Kodu]) AS marka, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat LEFT JOIN (SELECT [Satış Temsilcisi], COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI   GROUP BY [Satış Temsilcisi]) tem2 ON tem2.[Satış Temsilcisi]=fat.[Satış Temsilcisi] WHERE 
    fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Satış Temsilcisi] NOT IN('FIRAT KAYACAN','SAVAŞ KAYACAN') AND fat.[Fatura Tarihi]='{0}'   GROUP BY fat.[Satış Temsilcisi], tem2.bayi ", DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Aylikkarne>("SCSlogo");




            foreach (var item in aylikkarne)
            {
                var model = appuserlist.FirstOrDefault(x => x.username.Contains(item.kullanici));
                if (model != null)
                {
                    string text = string.Format(@"Merhaba {0} , Günün  Raporu Ciro: {1} , Bayi:{2} , Adet:{3} . İyi Akşamlar   ", item.kullanici, item.ciro, item.firma, item.adet);
                    var m = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", model.tel, text);
                }

            }

            var cirobiricisi = aylikkarne.FirstOrDefault(x => x.ciro== aylikkarne.Max(c=>c.ciro));

            if (cirobiricisi != null)
            {
                var user = appuserlist.FirstOrDefault(x => x.username == cirobiricisi.kullanici);
                string text = string.Format(@"Tebrikler {0} , Günün  Ciro biricisi oldunuz. Başarınız daim olsun  ", user.username);
                var m = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", user.tel, text);
                var a = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5322383343", text);
                var tu = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5422026516", text);
            }

            var bayibiricisi = aylikkarne.FirstOrDefault(x => x.firma== aylikkarne.Max(c=>c.firma));

            if (bayibiricisi != null)
            {
                var user = appuserlist.FirstOrDefault(x => x.username == bayibiricisi.kullanici);
                string text = string.Format(@"Tebrikler {0} , Günün  Bayi biricisi oldunuz. Başarınız daim olsun  ", user.username);
                var m = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", user.tel, text); 
                var a = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5322383343", text);
                var tu = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5422026516", text);
            }

        }       
        
        public void sendmesajbayihatırlatma()
        {

            List<Appuser> appuserlist = string.Format("SELECT * FROM appusers au WHERE au.role='satis' and tel!=''").GetQuery<Appuser>();




            foreach (var item in appuserlist)
            {
                    string text = string.Format(@"Merhaba {0} , Günlük aranması gereken bayi sayi 150 dir.   ", item.username);

                    var m = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", item.tel, text);
                var a = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5322383343", text);
                var tu = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5422026516", text);
            }

        } 
        public void hedefhatırlatma()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            List<Personelperformas> model = new List<Personelperformas>();
            List<Salestarget> targets = new List<Salestarget>();
            targets = string.Format(@"
   SELECT st.*,au.username ,au.tel FROM salestargets st JOIN appusers au ON st.appuserid=au.id WHERE au.role='satis' and  st.year={0} AND st.month={1}", DateTime.Now.Year, DateTime.Now.Month).GetQuery<Salestarget>();

            model = string.Format(@"SELECT ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", firstDayOfMonth.ToString(Formats.dateFormatsql), DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Personelperformas>("SCSlogo");

            foreach (var item in targets)
            {
                if (model.FirstOrDefault(x => x.kullanici == item.username) != null)
                {
                    item.ciro = model.FirstOrDefault(x => x.kullanici == item.username).ciro;
                    string text = string.Format(@"Merhaba {0} , Bu ayki hederfin {1}. Şimdiye kadar yapılan ciron {2}. Hedefine kalan ciro {3}.  ", item.username, string.Format("{0:C0}", item.target) , string.Format("{0:C0}", item.ciro), string.Format("{0:C0}", (item.target - item.ciro)));
                    var m = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", item.tel, text);
                    var a = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5322383343", text);
                    var tu = sendmesaj("GENCPA", "Ny2erd2020", "GENCPA", "5422026516", text);
                }


            }

        }



        public string sendmesaj(string username, string password, string title, string phoneNumber, string stringcontent)
        {
     
            var obj = new smsModel.Root
            {
                header = new smsModel.Header
                {
                    username = username,
                    password = password

                },
                body = new smsModel.Body
                {
                    messages = new List<smsModel.Message>(){new smsModel.Message {
                        phoneNumber = phoneNumber,
                        content=stringcontent
                    }},
                    title= title
                }

            };


            var resultt = JsonConvert.SerializeObject(obj);

            string url = string.Format("http://api.cagrisms.com/api/SMS");

            string result;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(resultt);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }


            }
            catch (WebException webEx)
            {
                var response = ((HttpWebResponse)webEx.Response);
                StreamReader content = new StreamReader(response.GetResponseStream());
                result = content.ReadToEnd();
            }
            catch (Exception ex)
            {
                result = ex.Message.ToString();
            }



            return result;
        }



    }
}
