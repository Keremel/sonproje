using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    public class saleraporController : Controller
    {
        public IActionResult stokmaliyet()
        {
           


            return View();
        }    
        public IActionResult tahsilatraporusatis()
        {
            string tempsorgu = "";
            if (CurrentSession.Role("satis"))
            {
                tempsorgu = string.Format(" [TEMSİLCİ] ='{0}' AND", CurrentSession.Username);
            }

                dynamic model = string.Format(@"SELECT  TEMSİLCİ temsilci, [CH KODU] kod, [CH UNVANI] unvan, GECEN_GUN, CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END [MODÜL]  
    , SUM(TUTAR) TUTAR FROM ARY_XXX_TAHSILAT WHERE {0} [TEMSİLCİ] NOT IN ('PASİF','BOŞ','HUKUK') GROUP BY [CH KODU], [CH UNVANI],
  TEMSİLCİ,GECEN_GUN, CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END ORDER
 BY [CH UNVANI], CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END, SUM(TUTAR) DESC", tempsorgu).GetDynamicQuery("SCSlogo");

            return View(model);
        } 
            
        public IActionResult stokmaliyetsatis()
        {

 dynamic model = string.Format(@"SELECT  KOD
                               ,AD
                               ,[Marka Kodu] as MarkaKodu
                               ,[Malzeme Grup Kodu] MalzemeGrupKodu
                               ,Model
                               ,AltGrup
                               ,STOK
                               ,ONERI_SIP
                               ,ONAYLI_SIP
                               ,KLN_STOK as kalanstok,
                               GUNCEL_TARIH
                           FROM [tiger].[dbo].[ARY_XXX_STOK_DURUM_4] where KLN_STOK>0").GetDynamicQuery("SCSlogo");
                         

            return View(model);
        } 
        
        public IActionResult pazaryerirapor(string bastarih, string bistarih)
        {
            if (bastarih.isNull() || bistarih.isNull())
            {
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                bastarih = firstDayOfMonth.ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }


            List<pazaryeriraporModel> model = string.Format(@"SELECT fat.[Fatura Özel Kodu] as pazaryeri,  ISNULL(SUM([Satır Miktarı]),0) AS ADET, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT [Malzeme Kodu]) AS cesit, 
COUNT(DISTINCT [Marka Kodu]) AS marka FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHere  fat.[Fatura Özel Kodu] IN('CICEKSEPET','TRENDYOL','N11','GİTTİ','HEPSİ') AND  fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}'  GROUP BY fat.[Fatura Özel Kodu]",bastarih,bistarih).GetQuery<pazaryeriraporModel>("SCSlogo"); 


            return View(model);
        }  
        public IActionResult alislar(int gunselect=7)
        {
            
            alislarModel alislarModel = new alislarModel();
            alislarModel.alislar = string.Format(@"SELECT urun.[Malzeme Grup Kodu] tur, ISNULL(urun.[Marka Kodu],'?') marka, urun.Model model, urun.[Malzeme Adı] urun, urun.[Malzeme Kodu] kod,
 ROUND(alim.[Satır Net Tutarı KDV Dahil]/alim.[Satır Miktarı],0) AS fiyat, alim.[Satır Miktarı] adet, CONVERT(DATE,alim.[Fatura Tarihi],23) tarih, alim.[Cari Kodu] cari, alim.[Cari Ünvanı] unvan, ISNULL(temsilci.[Satış Temsilcisi],'?') kisi 
FROM ARY_017_01_AYRINTILI_FATURA_RAPORU urun LEFT JOIN ARY_017_01_AYRINTILI_FATURA_RAPORU alim ON alim.[Malzeme Kodu]=urun.[Malzeme Kodu] LEFT JOIN ARY_017_CARI_MUSTER_TEMSILCI temsilci ON alim.[Cari Kodu]=temsilci.[Cari Kodu] 
WHERE urun.[Fatura Tarihi]>GetDate()-{0} AND urun.[Fatura Türü]='Satınalma Faturası' And alim.[Fatura Tarihi]>GetDate()-{0} AND alim.[Fatura Türü]='Satınalma Faturası' GROUP BY urun.[Malzeme Grup Kodu], urun.[Marka Kodu], urun.Model, urun.[Malzeme Kodu], urun.[Malzeme Adı], alim.[Satır Net Tutarı KDV Dahil], alim.[Satır Miktarı], alim.[Fatura Tarihi], alim.[Cari Kodu], alim.[Cari Ünvanı], temsilci.[Satış Temsilcisi] 
ORDER BY urun.[Malzeme Grup Kodu], urun.[Marka Kodu], urun.[Malzeme Adı], alim.[Fatura Tarihi] DESC, alim.[Satır Miktarı] DESC",gunselect).GetQuery<Alis>("SCSlogo");

           alislarModel.markalar = alislarModel.alislar.GroupBy(r => new { r.marka }).Select(x => x.Key).Select(x=>x.marka);
            alislarModel.gunselect = gunselect;

            return View(alislarModel);
        } 


        public IActionResult aylikkarne(int ay=0,string markaselect = "Tum Markalar", string tips="")
        {
            List<aylikkarneModel> model = new List<aylikkarneModel>();

            string temsorgu = "";
            ViewBag.markaselect = markaselect;
            ViewBag.tips = tips;
            ViewBag.ay = ay;


            if (markaselect != "Tum Markalar")
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect);
            }

            if (!tips.isNull())
            {
                temsorgu = temsorgu + string.Format(" AND fat.[Malzeme Grup Kodu] IN ('{0}') ", tips.Replace(",", "','"));
            }


            for (int i = 0; i <= ay; i++)
            {
                aylikkarneModel aylikkarne = new aylikkarneModel();
               
                aylikkarne.aylikkarnes = string.Format(@"SELECT YEAR(DATEADD(MONTH,{0},GETDATE())) yil, CASE WHEN MONTH(DATEADD(MONTH,{0},GETDATE()))<10 THEN '0' + CONVERT(CHAR,MONTH(DATEADD(MONTH,{0},GETDATE()))) ELSE CONVERT(CHAR,MONTH(DATEADD(MONTH,{0},GETDATE()))) END ay, ISNULL(tem2.bayi,0) bayi, ISNULL(SUM([Satır Miktarı]),0) AS ADET, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Malzeme Kodu]) AS cesit, COUNT(DISTINCT [Marka Kodu]) AS marka, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat LEFT JOIN (SELECT [Satış Temsilcisi], COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI   GROUP BY [Satış Temsilcisi]) tem2 ON tem2.[Satış Temsilcisi]=fat.[Satış Temsilcisi] WHERE fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Yıl]=YEAR(DATEADD(MONTH,{0},GETDATE())) AND CONVERT(INT,LEFT(fat.AY,2))=CONVERT(CHAR,MONTH(DATEADD(MONTH,{0},GETDATE()))) {1} GROUP BY fat.[Satış Temsilcisi], tem2.bayi ", -i, temsorgu).GetQuery<Aylikkarne>("SCSlogo");
                if (aylikkarne.aylikkarnes.Count > 0)
                {
                    aylikkarne.ay = aylikkarne.aylikkarnes[0].ay.ToString();
                    aylikkarne.yil = aylikkarne.aylikkarnes[0].yil;
                    model.Add(aylikkarne);
                }
              

            }
         



            return View(model);
        } 

        public IActionResult yillikkarne(string yil = "2021", string ay = "1,2,3,4,5,6,7,8,9,10,11,12")
        {
            List<Yillikkarne> model = new List<Yillikkarne>();

            string temsorgu = "";
            ViewBag.ay = ay;
            ViewBag.yil = yil;


            model = string.Format(@"SELECT CONVERT(INT,LEFT(fat.AY,2)) AS ay, ISNULL(SUM([Satır Miktarı]),0) AS adet, ROUND(ISNULL(SUM([Satır Net Tutarı KDV Dahil]),0),0) AS ciro, ROUND(ISNULL(SUM([Satış Kar]),0),0) AS kar, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Malzeme Kodu]) AS cesit, COUNT(DISTINCT [Marka Kodu]) AS marka, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
 WHERE fat.[Satış Temsilcisi]!='' AND MONTH(fat.[Fatura Tarihi])  IN({0}) AND fat.[Yıl]={1} AND fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'  GROUP BY fat.[Satış Temsilcisi],CONVERT(INT,LEFT(fat.AY,2)) ORDER BY CONVERT(INT,LEFT(fat.AY,2))", ay,yil).GetQuery<Yillikkarne>("SCSlogo"); 

            
            return View(model);
        }


   
        public IActionResult bayiPerformans(string bastarih,string bistarih ,string temsilci="Tüm Kişiler", string tips = "")
        {
            string temsorgu="";
            ViewBag.select = temsilci;
            if (temsilci != "Tüm Kişiler")
            {
                temsorgu = string.Format(" AND fat.[Satış Temsilcisi]='{0}' ", temsilci) ;
            }
            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }

            dynamic model = string.Format(@"SELECT fat.[Satış Temsilcisi] SORUMLU, cari.[Cari Kodu] KODU, cari.[Cari Ünvanı] UNVAN, ISNULL(SUM([Satır Miktarı]),0) AS ADET, COUNT(DISTINCT fat.[Fatura No]) AS SIPARIS, ROUND(ISNULL(SUM(fat.[Satır Net Tutarı KDV Dahil]),0), 0) AS CIRO, COUNT(DISTINCT fat.[Model]) AS CESIT, ISNULL(FORMAT((SELECT MIN([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') ILK_SIPARIS,
 ISNULL(FORMAT((SELECT MAX([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') SON_SIPARIS FROM ARY_017_CARI_MUSTER_TEMSILCI cari LEFT JOIN ARY_XXX_AYRINTILI_SATIS_RAPORU fat ON fat.[Cari Kodu]=cari.[Cari Kodu] AND 
 fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' WHERE cari.[Cari Kodu]<>'' {2}  GROUP BY fat.[Satış Temsilcisi], cari.[Cari Kodu], cari.[Cari Ünvanı] Having  COUNT(DISTINCT fat.[Fatura No])>0 ORDER BY cari.[Cari Ünvanı]", bastarih, bistarih,temsorgu).GetDynamicQuery("SCSlogo");


            return View(model);
        }     
         public IActionResult bayiPerformansatis(string bastarih,string bistarih , string tips = "")
        {
            string temsorgu="";
    
        
                temsorgu = string.Format(" AND fat.[Satış Temsilcisi]='{0}' ", CurrentSession.Username) ;

            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }

            dynamic model = string.Format(@"SELECT fat.[Satış Temsilcisi] SORUMLU, cari.[Cari Kodu] KODU, cari.[Cari Ünvanı] UNVAN, ISNULL(SUM([Satır Miktarı]),0) AS ADET, COUNT(DISTINCT fat.[Fatura No]) AS SIPARIS, ROUND(ISNULL(SUM(fat.[Satır Net Tutarı KDV Dahil]),0), 0) AS CIRO, COUNT(DISTINCT fat.[Model]) AS CESIT, ISNULL(FORMAT((SELECT MIN([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') ILK_SIPARIS,
 ISNULL(FORMAT((SELECT MAX([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '{0}' AND '{1}'),'yyyy-MM-dd'),'') SON_SIPARIS FROM ARY_017_CARI_MUSTER_TEMSILCI cari LEFT JOIN ARY_XXX_AYRINTILI_SATIS_RAPORU fat ON fat.[Cari Kodu]=cari.[Cari Kodu] AND 
 fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' WHERE cari.[Cari Kodu]<>'' {2}  GROUP BY fat.[Satış Temsilcisi], cari.[Cari Kodu], cari.[Cari Ünvanı] Having  COUNT(DISTINCT fat.[Fatura No])>0 ORDER BY cari.[Cari Ünvanı]", bastarih, bistarih,temsorgu).GetDynamicQuery("SCSlogo");


            return View(model);
        }     
        
        public IActionResult personelPerformans(string bastarih,string bistarih ,string markaselect = "Tum Markalar", string tips="")
        {

            string temsorgu="";
            ViewBag.select = markaselect;
            ViewBag.tips = tips;
            
            if (markaselect != "Tum Markalar")
            {
                temsorgu = string.Format(" AND fat.[Marka Kodu]='{0}' ", markaselect) ;
            }

            if (!tips.isNull())
            {
                temsorgu = temsorgu+ string.Format(" AND fat.[Malzeme Grup Kodu] IN ('{0}') ", tips.Replace(",", "','")) ;
            }


            if (bastarih.isNull() || bistarih.isNull())
            {
                bastarih = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                bistarih = DateTime.Now.ToString("yyyy-MM-dd");
            }
            ViewBag.bastarih = bastarih;
            ViewBag.bistarih = bistarih;

            dynamic model = string.Format(@"SELECT  (COUNT(DISTINCT fat.[Cari Kodu])*100/tem2.bayi)  PENETRASYON , tem2.bayi BAYI, SUM([Satır Miktarı]) AS ADET, ROUND(SUM([Satır Net Tutarı KDV Dahil]),0) AS CIRO,
	COUNT(DISTINCT fat.[Cari Kodu]) AS FIRMA, COUNT(DISTINCT [Malzeme Kodu]) AS CESIT, COUNT(DISTINCT [Marka Kodu]) AS MARKA, ISNULL(fat.[Satış Temsilcisi],'?') AS KULLANICI 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
	LEFT JOIN (SELECT [Satış Temsilcisi], COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI GROUP BY [Satış Temsilcisi]) tem2 ON tem2.[Satış Temsilcisi]=fat.[Satış Temsilcisi] 
	WHERE fat.[Fatura Türü] LIKE 'Perakende Satış%' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' {2}
	GROUP BY fat.[Satış Temsilcisi], tem2.bayi 
	ORDER BY fat.[Satış Temsilcisi]", bastarih, bistarih,temsorgu).GetDynamicQuery("SCSlogo");


            return View(model);
        }
      
        public IActionResult personelSalestargets()
        {
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            List<Personelperformas> model = new List<Personelperformas>();
            List<Salestarget> targets = new List<Salestarget>();
            targets=string.Format(@"
   SELECT st.*,au.username,au.tel FROM salestargets st JOIN appusers au ON st.appuserid=au.id WHERE st.year={0} AND st.month={1}", DateTime.Now.Year,DateTime.Now.Month).GetQuery<Salestarget>();

            model = string.Format(@"SELECT ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", firstDayOfMonth.ToString(Formats.dateFormatsql), DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Personelperformas>("SCSlogo");

            foreach (var item in targets)
            {
                if (model.FirstOrDefault(x => x.kullanici == item.username)!=null)
                {
                    item.ciro = model.FirstOrDefault(x => x.kullanici == item.username).ciro;




                }
            
            
            }



            return View(targets);
        }



        [Route("{controller}/datatables")]
        [HttpPost]
        public IActionResult datatables([FromBody]DatatablesJS.DatatablesObject requestobj)
        {
            requestobj.dbtype = "SCSlogo";
            try
            {
                //var results = await _demoService.GetDataAsync(param);
                return new JsonResult(new DatatablesJS.DataTablesObjectResult().getresults(requestobj));
                //return new JsonResult(new { error = "aaa" });
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }

        }


    }
}