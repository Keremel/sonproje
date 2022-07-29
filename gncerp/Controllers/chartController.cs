using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Utility;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    public class chartController : Controller
    {
        public IActionResult Index()
        {

            dynamic model = string.Format(@"SELECT (((SELECT  COUNT(DISTINCT fa.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fa WHERE 
      fa.[Satır Net Tutarı KDV Dahil]<2000 AND fa.[Fatura Türü]='Perakende Satış Faturası' AND fa.[Satır Türü]='Malzeme' AND fa.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fa.[Fatura Tarihi] BETWEEN '2020-6-01' AND '2020-6-6' AND fa.[Satış Temsilcisi] IS NOT NULL
  GROUP BY fa.[Satış Temsilcisi])*100)/(SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI WHERE [Satış Temsilcisi]=fat.[Satış Temsilcisi])) f
  ,  SUM([Satır Miktarı])*(1) AS ADET, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS CIRO, COUNT(DISTINCT fat.[Cari Kodu]) AS FIRMA, COUNT(DISTINCT [Model]) AS CESIT, ISNULL(fat.[Satış Temsilcisi],'?') AS KULLANICI FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '2020-6-01' AND '2020-6-6' AND 
    fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC").GetDynamicQuery();



//            SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI WHERE[Satış Temsilcisi] = 'SELDA YOLCUOĞLU'
//SELECT COUNT(DISTINCT fa.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fa WHERE
//fa.[Satır Net Tutarı KDV Dahil] < 2000 AND fa.[Fatura Türü]='Perakende Satış Faturası' AND fa.[Satır Türü]= 'Malzeme' AND fa.[Satış Temsilcisi]= 'SELDA YOLCUOĞLU' AND fa.[Fatura Tarihi] BETWEEN '2020-6-01' AND '2020-6-6' AND fa.[Satış Temsilcisi] IS NOT NULL
//GROUP BY fa.[Satış Temsilcisi]


            return View();
        }

        public IActionResult personmonth()
        {
            List<Personelperformas> mo = new List<Personelperformas>();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            mo = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", firstDayOfMonth.ToString(Formats.dateFormatsql), DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Personelperformas>("SCSlogo");

            return View(mo);
        } 

        public IActionResult personmonthnew()
        {
            List<Personelperformas> mo = new List<Personelperformas>();

            mo = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", "2020-07-01", "2020-07-16").GetQuery<Personelperformas>("SCSlogo");

            return View(mo);
        } 


        public IActionResult personday()
        {
            List<Personelperformas> mo = new List<Personelperformas>();

          
            mo = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", DateTime.Now.ToString(Formats.dateFormatsql), DateTime.Now.ToString(Formats.dateFormatsql)).GetQuery<Personelperformas>("SCSlogo");

            return View(mo);
        }  
        public IActionResult brandmonth()
        {
            List<Brandperformas> model = new List<Brandperformas>();
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model = string.Format(@"SELECT fat.[Marka Kodu] marka, SUM([Satır Miktarı])*(-1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) AS ciro, 
              COUNT(DISTINCT [Model]) AS cesit, COUNT(DISTINCT fat.[Cari Kodu]) AS firma,
                    COUNT(DISTINCT fat.[İl]) AS sehir, COUNT(DISTINCT tem.[Satış Temsilcisi]) AS sorumlu FROM ARY_017_01_AYRINTILI_FATURA_RAPORU
         fat LEFT OUTER JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON tem.[Cari Kodu]=fat.[Cari Kodu]
                WHERE fat.FATURA='SATIŞ VE DAĞITIM' AND fat.[Satır Türü]='Malzeme' AND fat.[Marka Kodu] IS NOT NULL AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' GROUP BY fat.[Marka Kodu]
          ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) DESC", firstDayOfMonth.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")).GetQuery<Brandperformas>("SCSlogo");


            return View(model);
        } 

        public IActionResult brandday()
        {
            List<Brandperformas> model = new List<Brandperformas>();
 
            model = string.Format(@"SELECT fat.[Marka Kodu] marka, SUM([Satır Miktarı])*(-1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) AS ciro, 
              COUNT(DISTINCT [Model]) AS cesit, COUNT(DISTINCT fat.[Cari Kodu]) AS firma,
                    COUNT(DISTINCT fat.[İl]) AS sehir, COUNT(DISTINCT tem.[Satış Temsilcisi]) AS sorumlu FROM ARY_017_01_AYRINTILI_FATURA_RAPORU
         fat LEFT OUTER JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON tem.[Cari Kodu]=fat.[Cari Kodu]
                WHERE fat.FATURA='SATIŞ VE DAĞITIM' AND fat.[Satır Türü]='Malzeme' AND fat.[Marka Kodu] IS NOT NULL AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' GROUP BY fat.[Marka Kodu]
          ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(-1) DESC",DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd")).GetQuery<Brandperformas>("SCSlogo");

            return View(model);
        }

        public IActionResult persontest()
        {
            List<Personelperformas> mo = new List<Personelperformas>();

            mo = string.Format(@"SELECT SUM([Satır Miktarı])*(1) AS adet, ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) AS ciro, COUNT(DISTINCT fat.[Cari Kodu]) AS firma, COUNT(DISTINCT [Model]) AS cesit, ISNULL(fat.[Satış Temsilcisi],'?') AS kullanici,CASE (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]) WHEN 0 THEN 0 ELSE
               ((SELECT  COUNT(DISTINCT fsayisi.[Cari Kodu]) AS FIRMA FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fsayisi 
     	WHERE fsayisi.[Satır Net Tutarı KDV Dahil]<2000 AND fsayisi.[Fatura Türü]='Perakende Satış Faturası' AND 
	  fsayisi.[Satır Türü]='Malzeme' AND fsayisi.[Satış Temsilcisi]=fat.[Satış Temsilcisi] AND fsayisi.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' 
	 GROUP BY fsayisi.[Satış Temsilcisi])*100/
	 (SELECT COUNT([Cari Kodu]) bayi FROM ARY_017_CARI_MUSTER_TEMSILCI cmt WHERE cmt.[Satış Temsilcisi]=fat.[Satış Temsilcisi]))  END AS penetrasyon
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat WHERE  fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme'
	AND fat.[Fatura Tarihi] BETWEEN '{0}' AND '{1}' AND fat.[Satış Temsilcisi] IS NOT NULL GROUP BY fat.[Satış Temsilcisi] ORDER BY ROUND(SUM([Satır Net Tutarı KDV Dahil]), 0)*(1) DESC", "2020-07-01", "2020-07-16").GetQuery<Personelperformas>("SCSlogo");

            return View(mo);
        }



    }
}