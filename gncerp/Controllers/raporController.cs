using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using gncerp.Entities;
using gncerp.Models;
using gncerp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gncerp.Controllers
{
    [Authorize(Roles = "admin,yonetici")]
    public class raporController : Controller
    {
        Helpertype helpertype = new Helpertype();
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult gelirgiderraporu(string yil="2020",string ay= "1,2,3,4,5,6,7,8,9,10,11,12")
        {

            ViewBag.ay = ay;
            ViewBag.yil = yil;
            

            List<Accountcodemapping> accmaplist = new List<Accountcodemapping>();

            List<GelirgiderraporuModel> listmodel = new List<GelirgiderraporuModel>();
            accmaplist = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>();


            foreach (var item in accmaplist)
            {
                string[] dizi = item.accountcode.Split(",");

                string codenew = "";
                foreach (var s in dizi)
                {
                    codenew = codenew + string.Format("'{0}',", s);
                }

                GelirgiderraporuModel newgelir = new GelirgiderraporuModel();

                newgelir.accountcodemapping = item;
                newgelir.tgider = string.Format(@"SELECT   CAST(MONTH_ AS varchar) ay, SUM(DEBIT) toplam from LG_017_01_EMFLINE
                          WHERE YEAR_ = {1} AND ACCOUNTCODE IN({0}) AND MONTH_ IN({2})  GROUP BY  MONTH_ ORDER BY MONTH_", codenew.Substring(0, codenew.Length - 1), yil, ay).GetQuery<TGelir>("SCSlogo");
                newgelir.budget = string.Format(@"SELECT * FROM budget WHERE accountcodemappingid={0}", item.id).GetQuery<Budget>();



                listmodel.Add(newgelir);
            }





            return View(listmodel);
        }

        public IActionResult exceltorapor()
        {

            dynamic bigmodel = new ExpandoObject();
            bigmodel.personel = string.Format(@"SELECT KEBIRCODE, MONTH_,SUM(CEILING(CREDIT) - CEILING(DEBIT))   Toplam from LG_017_01_EMFLINE
                          WHERE YEAR_ = 2020 AND KEBIRCODE = 760 GROUP BY  MONTH_ , KEBIRCODE ORDER BY MONTH_").GetDynamicQuery("SCSlogo");

            bigmodel.tasit = string.Format(@"SELECT  KEBIRCODE, MONTH_,SUM(CEILING(CREDIT) - CEILING(DEBIT)) Toplam from LG_017_01_EMFLINE
                          WHERE YEAR_ = 2020 AND KEBIRCODE = 254 GROUP BY  MONTH_ , KEBIRCODE ORDER BY MONTH_").GetDynamicQuery("SCSlogo");
            bigmodel.pos = string.Format(@"SELECT  KEBIRCODE, MONTH_,SUM(CREDIT - DEBIT) Toplam from LG_017_01_EMFLINE
                          WHERE YEAR_ = 2020 AND KEBIRCODE = 643 GROUP BY  MONTH_ , KEBIRCODE ORDER BY MONTH_").GetDynamicQuery("SCSlogo");

            return View(bigmodel);
        }

        public IActionResult karzararraporu(string yil = "2021", string ay = "1,2,3,4,5,6,7,8,9,10,11,12")
        {
            ViewBag.ay = ay;
            ViewBag.yil = yil;


            List<string> moonlist = new List<string>();

            foreach (var item in ay.Split(","))
            {
                moonlist.Add(item);
            }

            KarzararraporuModel kzmodel = new KarzararraporuModel();

            kzmodel.tGelir = string.Format(@"SELECT [Malzeme Grup Kodu] grupKodu ,CAST(month([Fatura Tarihi]) AS varchar) ay, Sum([Satır Net Tutarı]*1.18) toplam FROM [tiger].[dbo].[ARY_XXX_AYRINTILI_SATIS_RAPORU] WHERE Yıl={1} AND  month([Fatura Tarihi]) IN({0})  GROUP BY  [Malzeme Grup Kodu] , month([Fatura Tarihi]) ORDER BY  month([Fatura Tarihi])", ay,yil).GetQuery<TGelir>("SCSlogo");

            kzmodel.tKar = string.Format(@"SELECT [Malzeme Grup Kodu] grupKodu , CAST(month([Fatura Tarihi]) AS varchar) ay ,Sum([Satış Kar]*1.18) toplam FROM [tiger].[dbo].[ARY_XXX_AYRINTILI_SATIS_RAPORU] WHERE Yıl={1} AND month([Fatura Tarihi]) IN({0})  GROUP BY  [Malzeme Grup Kodu] , month([Fatura Tarihi]) ORDER BY  month([Fatura Tarihi])", ay,yil).GetQuery<TKar>("SCSlogo");

            kzmodel.tMaliyet = string.Format(@"SELECT [Malzeme Grup Kodu] grupKodu , CAST(month([Fatura Tarihi]) AS varchar) ay ,Sum(([Satır Net Tutarı]-[Satış Kar])*1.18) toplam FROM [tiger].[dbo].[ARY_XXX_AYRINTILI_SATIS_RAPORU] WHERE Yıl={1} AND  month([Fatura Tarihi]) IN({0})  GROUP BY  [Malzeme Grup Kodu] , month([Fatura Tarihi]) ORDER BY  month([Fatura Tarihi])", ay,yil).GetQuery<TMaliyet>("SCSlogo");

            kzmodel.teletFiyatfarki = string.Format(@"SELECT ACCOUNTCODE muhasebekodu ,CAST(MONTH_ AS varchar)   ay ,SUM(CREDIT) toplam from LG_017_01_EMFLINE WHERE YEAR_ = {1} AND ACCOUNTCODE='602.01.01.001' AND MONTH_ IN({0})   GROUP BY  MONTH_ , ACCOUNTCODE ORDER BY MONTH_", ay,yil).GetQuery<TGelirgider>("SCSlogo");

            List<Accountcodemapping> accmaplist = new List<Accountcodemapping>();
            List<GelirgiderraporuModel> listmodel = new List<GelirgiderraporuModel>();
            accmaplist = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>();

            foreach (var item in accmaplist)
            {
                string[] dizi = item.accountcode.Split(",");

                string codenew = "";
                foreach (var s in dizi)
                {
                    codenew = codenew + string.Format("'{0}',", s);
                }


                GelirgiderraporuModel newgelir = new GelirgiderraporuModel();



                newgelir.accountcodemapping = item;
                if (item.title == "POS KOMİSYON GELİRLERİ" 
                    || item.title == "TEMİNAT MEKTUBU MASRAF YANSITMA GELİRİ"
                    || item.title.Contains("KİRA GELİRLERİ")
                    || item.title.Contains("FİYAT FARKI GELİRLERİ")  
                    )
                {
                    newgelir.tgider = string.Format(@"SELECT   CAST(MONTH_ AS varchar) ay, SUM(CREDIT) toplam from LG_017_01_EMFLINE
                          WHERE YEAR_ = {1} AND ACCOUNTCODE IN({0}) AND MONTH_ IN({2})  GROUP BY  MONTH_ ORDER BY MONTH_", codenew.Substring(0, codenew.Length - 1), yil, ay).GetQuery<TGelir>("SCSlogo");
                }
                else
                {

                    newgelir.tgider = string.Format(@"SELECT   CAST(MONTH_ AS varchar) ay, SUM(DEBIT) toplam from LG_017_01_EMFLINE
                          WHERE YEAR_ = {1} AND ACCOUNTCODE IN({0}) AND MONTH_ IN({2})  GROUP BY  MONTH_ ORDER BY MONTH_", codenew.Substring(0, codenew.Length - 1), yil,ay).GetQuery<TGelir>("SCSlogo");
                }
                listmodel.Add(newgelir);
            }

            kzmodel.gelirgiderraporuModel = listmodel;

            kzmodel.kira_gelirleri = listmodel.Where(x =>
                           x.accountcodemapping.title.Contains("KİRA GELİRLERİ")).ToList(); 
            kzmodel.fiyat_farki_gelirleri = listmodel.Where(x =>
                           x.accountcodemapping.title.Contains("FİYAT FARKI GELİRLERİ")).ToList();    
            kzmodel.fiyat_farki_giderleri = listmodel.Where(x =>
                           x.accountcodemapping.title.Contains("FİYAT FARKI GİDERLERİ")).ToList();


            kzmodel.personel_giderler = listmodel.Where(x =>
                             x.accountcodemapping.title.Contains("PERSONEL ÜCRETLERİ")
                             || x.accountcodemapping.title.Contains("PERSONEL SOSYAL HAKLAR")
                             || x.accountcodemapping.title.Contains("PERSONEL EĞİTİM GİDERLERİ")
                             || x.accountcodemapping.title.Contains("KIDEM & İHBAR TAZMİNATI")).ToList();

            kzmodel.buro_sabit_giderler = listmodel.Where(x =>
                                    x.accountcodemapping.title.Contains("KİRALAR")
                                    || x.accountcodemapping.title.Contains("ELEKTRİK,SU,YAKIT")
                                    || x.accountcodemapping.title.Contains("BİNA YÖNETİM GİDERLERİ")).ToList();

            kzmodel.genel_idare_giderleri = listmodel.Where(x =>
                               x.accountcodemapping.title.Contains("TELEFON SABİT")
                             || x.accountcodemapping.title.Contains("TELEFON GSM")
                             || x.accountcodemapping.title.Contains("İNTERNET")
                             || x.accountcodemapping.title.Contains("KIRTASİYE & MATBUAT")
                             || x.accountcodemapping.title.Contains("TEMİZLİK GİDERLERİ")
                             || x.accountcodemapping.title.Contains("OTO MASRAFLARI")
                             || x.accountcodemapping.title.Contains("HUKUK DANISMANLIK GİDERİ")
                             || x.accountcodemapping.title.Contains("DENETİM & SMMM DANIŞMANLIK")
                             || x.accountcodemapping.title.Contains("DANIŞMANLIK-( İTHALAT ve BELGELENDİRME)")
                             || x.accountcodemapping.title.Contains("DANIŞMANLIK GİDERİ-( MARKA )")
                             || x.accountcodemapping.title.Contains("DANIŞMANLIK GİDERİ-( İTHALAT )")
                             || x.accountcodemapping.title.Contains("DANIŞMANLIK-( DİĞER )")
                             || x.accountcodemapping.title.Contains("BİLGİ İŞLEM GİDERLERİ")
                             || x.accountcodemapping.title.Contains("K.K.E.GİDER")
                             || x.accountcodemapping.title.Contains("DİĞER İDARİ GİDERLER")
                             || x.accountcodemapping.title.Contains("MUTFAK GİDERLERİ")
                             || x.accountcodemapping.title.Contains("ÜYELİK AİDAT VE ABONELİK GİDERLERİ")
                             || x.accountcodemapping.title.Contains("OFİS BAKIM ONARIM GİDERLERİ")
                             ).ToList();


            kzmodel.satis_dagitim_giderleri = listmodel.Where(x =>
                               x.accountcodemapping.title.Contains("YURTİÇİ NAKLİYE")
                           || x.accountcodemapping.title.Contains("DİĞER VERGİ VE HARÇLAR")
                           || x.accountcodemapping.title.Contains("VERİLEN TEMİNAT KOM.")
                           || x.accountcodemapping.title.Contains("POS KOMİSYON GİDERLERİ")
                           || x.accountcodemapping.title.Contains("FAİZ / KAR PAYI GİDERLERİ")
                           || x.accountcodemapping.title.Contains("İTHALAT TEST VE DENETİM GİDERLERİ")
                           || x.accountcodemapping.title.Contains("PAZARLAMA  GİDERLERİ")
                           || x.accountcodemapping.title.Contains("HAVALE")
                           || x.accountcodemapping.title.Contains("YURTİÇİ SEYAHAT GİDERLERİ")
                           || x.accountcodemapping.title.Contains("YURTDIŞI SEYAHAT GİDERLERİ")
                           || x.accountcodemapping.title.Contains("TEMSİL VE AĞIRLAMA")
                           ).ToList();

            kzmodel.bagis_ve_yardimlar = listmodel.Where(x =>
                               x.accountcodemapping.title.Contains("BAĞIŞ VE YARDIMLAR")
                                   ).ToList();

            kzmodel.amortisman_giderleri = listmodel.Where(x =>
                             x.accountcodemapping.title.Contains("AMORTİSMAN GİDERLERİ")
                   || x.accountcodemapping.title.Contains("DEMİRBAŞ GİDERLERİ")
                                 ).ToList();

            kzmodel.toplam_giderler = listmodel.Where(x =>
                           x.accountcodemapping.title.Contains("PERSONEL ÜCRETLERİ")
                        || x.accountcodemapping.title.Contains("PERSONEL SOSYAL HAKLAR")
                        || x.accountcodemapping.title.Contains("PERSONEL EĞİTİM GİDERLERİ")
                        || x.accountcodemapping.title.Contains("KIDEM & İHBAR TAZMİNATI")
                        || x.accountcodemapping.title.Contains("KİRALAR")
                        || x.accountcodemapping.title.Contains("ELEKTRİK,SU,YAKIT")
                        || x.accountcodemapping.title.Contains("BİNA YÖNETİM GİDERLERİ")
                        || x.accountcodemapping.title.Contains("TELEFON SABİT")
                        || x.accountcodemapping.title.Contains("TELEFON GSM")
                        || x.accountcodemapping.title.Contains("İNTERNET")
                        || x.accountcodemapping.title.Contains("KIRTASİYE & MATBUAT")
                        || x.accountcodemapping.title.Contains("TEMİZLİK GİDERLERİ")
                        || x.accountcodemapping.title.Contains("OTO MASRAFLARI")
                        || x.accountcodemapping.title.Contains("DEMİRBAŞ GİDERLERİ")
                        || x.accountcodemapping.title.Contains("HUKUK DANISMANLIK GİDERİ")
                        || x.accountcodemapping.title.Contains("DENETİM & SMMM DANIŞMANLIK")
                        || x.accountcodemapping.title.Contains("DANIŞMANLIK-( İTHALAT ve BELGELENDİRME)")
                        || x.accountcodemapping.title.Contains("DANIŞMANLIK GİDERİ-( MARKA )")
                        || x.accountcodemapping.title.Contains("DANIŞMANLIK GİDERİ-( İTHALAT )")
                        || x.accountcodemapping.title.Contains("DANIŞMANLIK-( DİĞER )")
                        || x.accountcodemapping.title.Contains("BİLGİ İŞLEM GİDERLERİ")
                        || x.accountcodemapping.title.Contains("K.K.E.GİDER")
                        || x.accountcodemapping.title.Contains("DİĞER İDARİ GİDERLER")
                        || x.accountcodemapping.title.Contains("MUTFAK GİDERLERİ")
                        || x.accountcodemapping.title.Contains("ÜYELİK AİDAT VE ABONELİK GİDERLERİ")
                        || x.accountcodemapping.title.Contains("OFİS BAKIM ONARIM GİDERLERİ")
                        || x.accountcodemapping.title.Contains("YURTİÇİ NAKLİYE")
                        || x.accountcodemapping.title.Contains("DİĞER VERGİ VE HARÇLAR")
                        || x.accountcodemapping.title.Contains("VERİLEN TEMİNAT KOM.")
                        || x.accountcodemapping.title.Contains("POS KOMİSYON GİDERLERİ")
                        || x.accountcodemapping.title.Contains("FAİZ / KAR PAYI GİDERLERİ")
                        || x.accountcodemapping.title.Contains("İTHALAT TEST VE DENETİM GİDERLERİ")
                        || x.accountcodemapping.title.Contains("PAZARLAMA  GİDERLERİ")
                        || x.accountcodemapping.title.Contains("HAVALE")
                        || x.accountcodemapping.title.Contains("YURTİÇİ SEYAHAT GİDERLERİ")
                        || x.accountcodemapping.title.Contains("YURTDIŞI SEYAHAT GİDERLERİ")
                        || x.accountcodemapping.title.Contains("TEMSİL VE AĞIRLAMA")
                        || x.accountcodemapping.title.Contains("AMORTİSMAN GİDERLERİ")
                        || x.accountcodemapping.title.Contains("BAĞIŞ VE YARDIMLAR")).ToList();

            kzmodel.pos_kom_gelirleri = listmodel.Where(x =>
                      x.accountcodemapping.title.Contains("POS KOMİSYON GELİRLERİ")
                            ).ToList();

            kzmodel.pos_kom_giderleri = listmodel.Where(x =>
                      x.accountcodemapping.title.Contains("POS KOMİSYON GİDERLERİ")
                            ).ToList();
            kzmodel.faiz_kar_payi_giderleri = listmodel.Where(x =>
                             x.accountcodemapping.title.Contains("FAİZ / KAR PAYI GİDERLERİ")
                                   ).ToList();

            kzmodel.teminat_mektubu_masraf_yansitma = listmodel.Where(x =>
                       x.accountcodemapping.title.Contains("TEMİNAT MEKTUBU MASRAF YANSITMA GELİRİ")
                             ).ToList();

            List<Toplamay> degil_gelirler_Listtoplamay = new List<Toplamay>();

            for (int i = 0; i < moonlist.Count; i++)
            {
                Toplamay toplamay = new Toplamay();

                toplamay.toplam = kzmodel.kira_gelirleri.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam)) +
             (  kzmodel.fiyat_farki_gelirleri.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam))
             -
             kzmodel.fiyat_farki_giderleri.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam))
            );
                   
                toplamay.ay = moonlist[i];

                degil_gelirler_Listtoplamay.Add(toplamay);
            }

            kzmodel.diger_gelirler_toplam = degil_gelirler_Listtoplamay;


            List<Toplamay> Listtoplamay = new List<Toplamay>();

               for (int i = 0; i < moonlist.Count; i++)
                     {
                         Toplamay toplamay = new Toplamay();
                  
                         toplamay.toplam = (kzmodel.pos_kom_gelirleri.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam))
                               +
                            kzmodel.teminat_mektubu_masraf_yansitma.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam))
                            -
                        kzmodel.pos_kom_giderleri.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam))
                             -
                        kzmodel.faiz_kar_payi_giderleri.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam))
                            
                            );
                         toplamay.ay = moonlist[i];
                  
                         Listtoplamay.Add(toplamay);
                     }
                kzmodel.finasmantoplam = Listtoplamay;
            List<Toplamay> toplam_brut_kar_toplamay = new List<Toplamay>();

            for (int i = 0; i < moonlist.Count; i++)
            {
                Toplamay toplamay = new Toplamay();

                toplamay.toplam = kzmodel.diger_gelirler_toplam.FirstOrDefault(x => x.ay == moonlist[i]).toplam +
                                 kzmodel.tKar.Where(x => x.ay == moonlist[i]).Sum(x => x.toplam)
                ;
                toplamay.ay = moonlist[i];

                toplam_brut_kar_toplamay.Add(toplamay);
            }
            kzmodel.toplam_brut_kar = toplam_brut_kar_toplamay;



            List<Toplamay> bigmantoplamay = new List<Toplamay>();

              for (int i = 0; i < moonlist.Count; i++)
                     {
                         Toplamay toplamay = new Toplamay();
                  
                            toplamay.toplam =
                             (-kzmodel.toplam_giderler.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam)))
                             +
                             (kzmodel.finasmantoplam.FirstOrDefault(x => x.ay == moonlist[i]).toplam)
                            ;
                            toplamay.ay = moonlist[i];

                            bigmantoplamay.Add(toplamay);
                     }

              kzmodel.big_toplam_gider = bigmantoplamay;

            List<Toplamay> karzarartoplam = new List<Toplamay>();

            for (int i = 0; i < moonlist.Count; i++)
            {
                Toplamay toplamay = new Toplamay();

                toplamay.toplam = (kzmodel.toplam_brut_kar.FirstOrDefault(x => x.ay == moonlist[i]).toplam
                       - kzmodel.toplam_giderler.Sum(x => x.tgider.Where(b => b.ay == moonlist[i]).Sum(a => a.toplam)));
                         ;

                toplamay.ay = moonlist[i];

                karzarartoplam.Add(toplamay);
            }

            kzmodel.kar_zarar = karzarartoplam;

            List<Toplamay> ayliklarzarartoplamay = new List<Toplamay>();

             for (int i = 0; i < moonlist.Count; i++)
                 {
                     Toplamay toplamay = new Toplamay();

                   toplamay.toplam = kzmodel.kar_zarar.FirstOrDefault(x => x.ay == moonlist[i]).toplam
                     +
                   kzmodel.finasmantoplam.FirstOrDefault(x => x.ay == moonlist[i]).toplam;
                        toplamay.ay = moonlist[i];

                    ayliklarzarartoplamay.Add(toplamay);
                 }
                
            kzmodel.aylik_kar_zarar = ayliklarzarartoplamay;  
            List<Toplamay> vergi_karsiligi_toplamay = new List<Toplamay>();

             for (int i = 0; i < moonlist.Count; i++)
                     {
                         Toplamay toplamay = new Toplamay();
                       if (kzmodel.big_toplam_gider.FirstOrDefault(x => x.ay == moonlist[i]).toplam > 0)
                       {
                           toplamay.toplam = kzmodel.aylik_kar_zarar.FirstOrDefault(x => x.ay == moonlist[i]).toplam * 0.2;
                       }
                       else {
                           toplamay.toplam = 0;
                       }

                            toplamay.ay = moonlist[i];

                        vergi_karsiligi_toplamay.Add(toplamay);
                     }

               kzmodel.vergi_karsiligi = vergi_karsiligi_toplamay;

            List<Toplamay> vsayliklarzarartoplamay = new List<Toplamay>();   
            List<Toplamay> kumulatifayliklarzarartoplamay = new List<Toplamay>();
            double kumulatiftopalam = 0;

            for (int i = 0; i < moonlist.Count; i++)
            {
                Toplamay toplamay = new Toplamay();
                Toplamay kumulatiftoplamay = new Toplamay();
                toplamay.toplam = kzmodel.vergi_karsiligi.FirstOrDefault(x => x.ay == moonlist[i]).toplam
                                       +
                                       kzmodel.aylik_kar_zarar.FirstOrDefault(x => x.ay == moonlist[i]).toplam;
                toplamay.ay = moonlist[i];
               
                vsayliklarzarartoplamay.Add(toplamay);
                kumulatiftopalam = kumulatiftopalam + toplamay.toplam;
                kumulatiftoplamay.ay = moonlist[i];
                kumulatiftoplamay.toplam = kumulatiftopalam;
                kumulatifayliklarzarartoplamay.Add(kumulatiftoplamay);
            }

            kzmodel.vs_aylik_kar_zarar = vsayliklarzarartoplamay;
            kzmodel.kumulatif_aylik_kar_zarar = kumulatifayliklarzarartoplamay;






            return View(kzmodel);
        }

        public IActionResult bilanco(string yil = "2020", string ay = "1,2,3,4,5,6,7,8,9,10,11,12")
        {
            ViewBag.ay = ay;
            ViewBag.yil = yil;

            List<Kebircodemapping> kebircodemappings = new List<Kebircodemapping>();

            kebircodemappings= string.Format(@"SELECT id,title,kebircode FROM kebircodemapping").GetQuery<Kebircodemapping>();

           
            
            BilancoModel model = new BilancoModel();

            model.Bilanco_List_Aktif = string.Format(@"SELECT KEBIRCODE code,SUM(DEBIT-CREDIT)  toplam from LG_017_01_EMFLINE  e WHERE e.YEAR_={0}  AND e.MONTH_ IN({1}) AND e.KEBIRCODE IN(100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144,145,146,147,148,149,150,151,152,153,154,155,156,157,158,159,160,161,162,163,164,165,166,167,168,169,170,171,172,173,174,175,
176,177,178,179,180,181,182,183,184,185,186,187,188,189,190,192,193,194,195,196,197,198,199,200,201,202,203,204,205,206,207,208,209,210,211,212,213,214,215,216,217,218,219,220,221,222,223,224,225,226,227,228,229,230,231,232,233,234,235,236,237,238,239,240,241,242,243,244,245,246,247,248,249,250,251,252,253,254,255,256,257,258,259,260,261,262,263,264,265,266,267,268,269,270,271,272,273,274,
275,276,277,278,279,280,281,282,283,284,285,286,287,288,289,290,291,292,293,294,295,296,297,298,299) GROUP BY  KEBIRCODE",yil,ay).GetQuery<Bilanco>("SCSlogo");
            model.Bilanco_List_Aktif.AddRange(string.Format(@"SELECT KEBIRCODE code,SUM(CREDIT-DEBIT)  toplam from LG_017_01_EMFLINE  e WHERE e.YEAR_={0}  AND e.MONTH_ IN({1}) AND e.KEBIRCODE IN(191) GROUP BY  KEBIRCODE", yil, ay).GetQuery<Bilanco>("SCSlogo"));
            foreach (var item in model.Bilanco_List_Aktif)
            {
                item.title = kebircodemappings.Find(x => x.kebircode == Convert.ToInt32(item.code)).title;

          
            
            }




            model.Bilanco_List_Pasif = string.Format(@"SELECT KEBIRCODE code , SUM(CREDIT-DEBIT) toplam from LG_017_01_EMFLINE  e WHERE e.YEAR_={0}  AND e.MONTH_ IN({1}) AND e.KEBIRCODE IN(300,301,302,303,304,305,306,307,308,309,310,311,312,313,314,315,316,317,318,319,320,321,322,323,324,325,326,327,328,329,330,331,332,333,334,335,336,337,338,339,340,341,342,343,344,345,346,347,348,349,350,351,352,353,354,355,356,357,358,359,360,361,362,363,364,365,366,367,368,369,370,371,372,373,374,375,376,377,378,379,380,381,382,383,384,385,386,387,388,389,390,391,392,
393,394,395,396,397,398,399,400,401,402,403,404,405,406,407,408,409,410,411,412,413,414,415,416,417,418,419,420,421,422,423,424,425,426,427,428,429,430,431,432,433,434,435,436,437,438,439,440,441,442,443,444,
445,446,447,448,449,450,451,452,453,454,455,456,457,458,459,460,461,462,463,464,465,466,467,468,469,470,471,472,473,474,475,476,477,478,479,480,481,482,483,484,485,486,487,488,489,490,491,492,493,494,495,496,497,498,499,500,501,502,503,504,505,506,507,508,509,510,511,512,513,514,515,516,517,518,519,520,521,522,523,524,525,526,527,528,529,530,531,532,533,534,535,536,537,538,539,540,541,542,543,544,545,546,547,548,549,550,551,552,553,554,555,556,557,558,559,560,561,562,563,564,565,
566,567,568,569,571,572,573,574,575,576,577,578,579,581,582,583,584,585,586,587,588,589,590,591,592,593,594,595,596,597,598,599,570,580)  GROUP BY  KEBIRCODE", yil, ay).GetQuery<Bilanco>("SCSlogo");


          
            foreach (var item in model.Bilanco_List_Pasif)
            {
                item.title = kebircodemappings.Find(x => x.kebircode == Convert.ToInt32(item.code)).title;



            }


     




            return View(model);
        }


        public IActionResult butce(string yil = "2020", string ay = "1,2,3,4,5,6,7,8,9,10,11,12")
        {

            ViewBag.ay = ay;
            ViewBag.yil = yil;


            List<Accountcodemapping> accmaplist = new List<Accountcodemapping>();

            List<butceModel> listmodel = new List<butceModel>();
            accmaplist = string.Format(@"SELECT * FROM accountcodemapping").GetQuery<Accountcodemapping>();


            foreach (var item in accmaplist)
            {
                string[] dizi = item.accountcode.Split(",");

                string codenew = "";
                foreach (var s in dizi)
                {
                    codenew = codenew + string.Format("'{0}',", s);
                }

                butceModel newbutce = new butceModel();

                newbutce.accountcodemapping = item;
                newbutce.budget = string.Format(@"SELECT * FROM budget WHERE accountcodemappingid={0}", item.id).GetQuery<Budget>();




                listmodel.Add(newbutce);
            }





            return View(listmodel);
        }

        [Route("{controller}/setbutce")]
        [HttpPost]
        public Result<string> setbutce([FromBody]Budget requestobj)
        {

            Result<string> result = new Result<string>();



            if (requestobj.id == 0)
            {

                try
                {
                    SqlCommand cmd = new GenericDataAccess().CreateCommand();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "INSERT INTO budget(accountcodemappingid,moon,year,cost) OUTPUT INSERTED.id VALUES(@accountcodemappingid,@moon,@year,@cost)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@accountcodemappingid", requestobj.accountcodemappingid));
                    cmd.Parameters.Add(new SqlParameter("@moon", requestobj.moon));
                    cmd.Parameters.Add(new SqlParameter("@year", requestobj.year));
                    cmd.Parameters.Add(new SqlParameter("@cost", requestobj.cost));
                    

                    int recid = (int)new GenericDataAccess().ExecuteScalar(cmd);
                    if (recid > 0)
                    {
                        result.status = true;
                    }


                }
                catch (Exception Ex)
                {
                    result.message = Ex.Message;
                    result.status = false;
                }

            }
            else {
                try
                {
                    SqlCommand cmd = new GenericDataAccess().CreateCommand();
                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE  budget Set cost=@cost Where id=@id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@cost", requestobj.cost));
                    cmd.Parameters.Add(new SqlParameter("@id", requestobj.id));

                    int recid = (int)new GenericDataAccess().ExecuteNonQuery(cmd);
                    if (recid > 0)
                    {
                        result.status = true;
                    }


                }
                catch (Exception Ex)
                {
                    result.message = Ex.Message;
                    result.status = false;
                }

            }

           
            return result;
        }





    }
}