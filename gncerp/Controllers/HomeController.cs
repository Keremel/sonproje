using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gncerp.Models;
using Microsoft.AspNetCore.Authentication;
using gncerp.Services;
using Microsoft.AspNetCore.Authorization;
using gncerp.Entities;
using gncerp.Utility;
using Microsoft.EntityFrameworkCore.Internal;
using gncerp.Data;
using System.Net;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace gncerp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        yurticikargoServices _yurticikargoServices = new yurticikargoServices();
        //private gncerpContext _gncerpContext = new gncerpContext();
        private bankServices _bankServices = new bankServices();
        private smsbackgroundServices _smsbackgroundServices = new smsbackgroundServices();
        public IActionResult intranet()
        {
            string result = "";
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://gencpatek.myideasoft.com/admin/user/auth?client_id=16_c60xzmsah7so88w8wsos8g44goco0gsock0k0c88g0s80gs0g&response_type=authorization_code&state=ASLAN&redirect_uri=https://gencpatek.myideasoft.com");
                httpWebRequest.Method = "Get";

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


            var i = DateTime.Now.DayOfWeek.ToString();

            return View();
        }


        public IActionResult Index()
        {
            indexModel _indexModel = new indexModel();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
           

            List<Personelperformas> gunluklist = new List<Personelperformas>();
            List<Personelperformas> ayliklist = new List<Personelperformas>();
            List<Personelperformas> haftaliklist = new List<Personelperformas>();
           string tempsorgu = "";

            if (CurrentSession.Role("satis"))
            {
                tempsorgu = string.Format("AND fat.[Satış Temsilcisi] ='{0}'", CurrentSession.Username);
                dynamic target = string.Format(@"SELECT st.target FROM salestargets st JOIN appusers au ON st.appuserid=au.id WHERE st.year={0} AND st.month={1} AND au.username='{2}'", DateTime.Now.Year, DateTime.Now.Month, CurrentSession.Username).GetDynamicQuery();

                if (target.Count != 0)
                {
                    _indexModel.salesmantarget = target[0].target;
                }
                else
                {
                    _indexModel.salesmantarget = 0;
                }
              
             
            }

            gunluklist = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici
            FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' 
            AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{0}'  {1}  GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC  ", DateTime.Now.ToString("yyyy-MM-dd"), tempsorgu).GetQuery<Personelperformas>("SCSlogo");

            ayliklist = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici
            FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' 
            AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}'  {2}  GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC  ", firstDayOfMonth.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), tempsorgu).GetQuery<Personelperformas>("SCSlogo");

            haftaliklist = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici
            FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' 
            AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}'  {2}  GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC  ", DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"), tempsorgu).GetQuery<Personelperformas>("SCSlogo");


            if (CurrentSession.Role("satis"))
            {

                List<Ciroprim> ciroprim = string.Format(@"SELECT Max([prim]) as prim FROM [gncpdb].[dbo].[Ciroprim] where ciro <={0}", ayliklist.Sum(x => x.ciro)).GetQuery<Ciroprim>();
                List<Penetrasyonprim>  penetrasyonprim = string.Format(@"SELECT MAX(prim) as prim FROM Penetrasyonprim where penetrasyon <={0}", ayliklist.Sum(x => x.firma)).GetQuery<Penetrasyonprim>();


                _indexModel.pprim = penetrasyonprim[0].prim;
                _indexModel.cprim = ciroprim[0].prim;
            }

      

            _indexModel.gunlukciro = gunluklist.Sum(x => x.ciro);
            _indexModel.gunlukadet = gunluklist.Sum(x => x.adet);

            _indexModel.aylikciro = ayliklist.Sum(x => x.ciro);
            _indexModel.aylikadet = ayliklist.Sum(x => x.adet);
            
            _indexModel.haftalikciro = haftaliklist.Sum(x => x.ciro);
            _indexModel.haftalikadet = haftaliklist.Sum(x => x.adet);


            return View(_indexModel);
        }


     



        [Route("{controller}/getdashboardrapor")]
        public dynamic getdashboardrapor(string bastarih,string bistarih,string markaselect,string turselect)
        {
            string temsorgu = "";


            if (markaselect != "Tüm Markalar" && !markaselect.isNull())
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect);
            }

            if (turselect != "Tüm Türler" && !turselect.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] ='{0}' ", turselect);
            }



            List<Personelperformas> mo = new List<Personelperformas>();
           
            mo = string.Format(@"SELECT fat.[Fatura Tarihi] as tarihdate , SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' {2}  
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Fatura Tarihi] ORDER BY fat.[Fatura Tarihi] ", bastarih, bistarih, temsorgu).GetQuery<Personelperformas>("SCSlogo");



            foreach (var item in mo)
            {
                item.tarih = item.tarihdate.ToString("yyyy-MM-dd");
                item.ciro = item.ciro / 1000;


            }

          

            return mo;
        } 


        [Route("{controller}/getdashboardrapor2")]
        [HttpPost]
        public List<Personelperformas> getdashboardrapor2([FromBody]getdashboardraporModel obj)
        {
            string temsorgu = "";


            if (obj.markaselect != "Tüm Markalar" && !obj.markaselect.isNull())
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", obj.markaselect);
            }

            if (obj.turselect!= "Tüm Türler" && !obj.turselect.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] ='{0}' ", obj.turselect);
            }



            List<Personelperformas> mo = new List<Personelperformas>();
           
            mo = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' {2}  
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", obj.bastarih,obj.bistarih, temsorgu).GetQuery<Personelperformas>("SCSlogo");



            return mo;
        }




        public IActionResult Privacy()
        {
            return View();
        }

   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
