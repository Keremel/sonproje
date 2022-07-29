
using gncerp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class QueryHelper
{
    public static string GetQuery(string query)
    {
        string result = "";
        switch (query)
        {
          
            case "siparislistesiQuery":
                result = string.Format(@"(SELECT  fat.[Malzeme Grup Kodu] tip, fat.[Fatura No] faturano,  REPLACE(REPLACE(fat.[Cari Ünvanı],CHAR(13),''),CHAR(10),'') unvan, fat.[Satış Temsilcisi] temsilci, fat.[Fatura Tarihi] tarih,
     SUM(fat.[Satır Miktarı]) adet, fat.Vade vade, SUM(fat.[Satır Net Tutarı KDV Dahil]) tutar, SUM(CASE WHEN fat.[Çıkış Maliyeti KDV Dahil]>0 THEN (fat.[Satış Kar]) END) kar, CONVERT(INT,ISNULL(REPLACE(REPLACE(REPLACE(fat.Vade,'KREDİ KARTI',1),'PEŞİN',0),' GÜN VADE',''),0)) vadesi 
	FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat 
	WHERE fat.[Satır Miktarı]>0 AND fat.[Satır Türü]='Malzeme'
	GROUP BY fat.[Fatura No], fat.[Cari Kodu], fat.[Cari Ünvanı], fat.[Fatura Türü], fat.[İl], fat.[Satış Temsilcisi], fat.[Fatura Tarihi],fat.Vade,fat.[Malzeme Grup Kodu] ) t", query);
                break;
            case "bilancoQuery":
                result = string.Format(@"(SELECT LINEEXP,TRNET ,YEAR_,MONTH_ ,KEBIRCODE from LG_017_01_EMFLINE) t", query);
                break;
            case "siparisdetayQuery":
                result = string.Format(@"( SELECT  fat.[Malzeme Adı] malzemeadi, fat.[Fatura No] faturano,fat.[Satır Miktarı] satirmiktari,fat.[Satır Toplamı] satirtoplami,fat.[Çıkış Maliyeti KDV Dahil] maliyet,fat.[Satır Türü],(fat.[Satış Kar])/fat.[Satır Miktarı] kar,CASE  WHEN fat.Vade ='PEŞİN' THEN CONVERT(INT,ISNULL(REPLACE(REPLACE(fat.Vade,'PEŞİN',0),' GÜN VADE',''),0))  WHEN fat.Vade ='KREDİ KARTI' THEN CONVERT(INT,ISNULL(REPLACE(REPLACE(fat.Vade,'KREDİ KARTI',1),' GÜN VADE',''),0))  ELSE 0 END AS vadesi FROM ARY_XXX_AYRINTILI_SATIS_RAPORU fat ) t", query);
                break;  
            case "taskQuery":
                result = string.Format(@"( SELECT * from tasks ) t", query);
                break;  
            case "payplanQuery":
                result = string.Format(@"(  SELECT p.*,pm.title paymentmode FROM Payplan p JOIN Paymentmode pm ON p.id=pm.id ) t", query);
                break;    
            case "accountactivitiesQuery":
                result = string.Format(@"( SELECT * from Accountactivities ) t", query);
                break; 
            case "commentsQuery":
                result = string.Format(@"( SELECT c.*,au.username FROM comments c JOIN appusers au ON c.appuserid=au.id ) t", query);
                break; 
            case "salestargetsQuery":
                result = string.Format(@"( SELECT st.*,au.username FROM salestargets st JOIN appusers au ON st.appuserid=au.id ) t", query);
                break;
            case "bankIbanQuery":
                result = string.Format(@"(   SELECT * FROM BankIbans  ) t", query);
                break;  
            case "raporQuery":
                result = string.Format(@"(SELECT KEBIRCODE,YEAR_, MONTH_ Ay ,SUM(DEBIT) TopmalmBorc ,SUM(CREDIT) ToplamAlacak,SUM(CREDIT-DEBIT) Toplam from  LG_017_01_EMFLINE GROUP BY  MONTH_,KEBIRCODE,YEAR_ ) t", query);
                break;  
            case "rapordetayQuery":
                result = string.Format(@"(SELECT YEAR_,KEBIRCODE,MONTH_, LINEEXP tanim,  DATE_ tarih , (CREDIT - DEBIT) toplam from LG_017_01_EMFLINE ) t", query);
                break;  
            case "teamQuery":
                result = string.Format(@"(SELECT * FROM [company.team] where idCompany=1 AND del=0 ) t", query);
                break;
            case "list_questionQuery":
                result = string.Format(@"(select * from list_question where idCompany=1 AND del=0) t", query);
                break;  
              case "carihesapekstresiQuery":
                result = string.Format(@"( SELECT cel.[CH ÜNVANI] unvan, cel.TARIH tarih, cel.ISLEMNO islemno, CEILING(cel.BORÇ) borc, CEILING(cel.ALACAK) alacak, CEILING(cel.BAKIYE) bakiye, cel.TEMSİLCİ temsilci, cel.[CH KODU]carikodu, cel.HAREKET_TURU hareketturu
            FROM ARY_XXX_CARI_EKSTRE_LOGOB2B cel ) t", query);
                break;
            case "babsformQuery":
                result = string.Format(@"(SELECT  clt.[Cari Kodu] carikodu, bbf.FORM formtip,bbf.AY ay,bbf.YIL yil,bbf.BELGE_SAYISI belgesayisi,bbf.TOPLAM_BEDEL toplambedel FROM ARY_XXX_BA_BS_FORMU bbf JOIN ARY_017_CARI_LISTESI_TG clt ON bbf.VKN=clt.[Vergi No] ) t", query);
                break;    
            case "satisrfQuery":
                result = string.Format(@"(SELECT asr.[Malzeme Adı] adi, asr.[Cari Kodu] carikodu,asr.[Fatura No] faturano,asr.[Fatura Tarihi] faturatarihi from ARY_XXX_AYRINTILI_SATIS_RAPORU asr ) t", query);
                break;   
            case "appuserQuery":
                result = string.Format(@"( SELECT *  FROM appusers Where isdel=0 ) t", query);
                break;  
            case "stokmaliyetQuery":
                result = string.Format(@"( SELECT m.[Malzeme Grup Kodu] tip, smtb.MalzRef id, m.[Malz_Kodu] kod, m.[Marka Kodu] marka, m.[Model] model_kodu, m.Malz_Adı model, ROUND(smtb.[MIN_Fiyat]*(1+(smtb.[KDV_Oranı]/100)),0) mal_min, ROUND(smtb.[MAX_Fiyat]*(1+(smtb.[KDV_Oranı]/100)),0) mal_max, ROUND(smtb.[Son_Alış_Fiyatı]*(1+(smtb.[KDV_Oranı]/100)),0) mal_son, ROUND(smtb.[Ortalama_Değer]*(1+(smtb.[KDV_Oranı]/100)),0) mal_ort, m.Fiili_Stok stok
 FROM ARY_017_MALZEME_LISTESI m  LEFT JOIN ARY_XXX_STOK_MALIYET_TB smtb ON m.[Malz_Kodu]= smtb.[Malzeme Kodu] Where m.Fiili_Stok!=0 AND  m.Malz_Adı NOT LIKE '%NUMUNE%' AND  m.Model NOT LIKE '%NUMUNE%'  ) t", query);
                break;    
            case "stokmaliyetdetayQuery":
                result = string.Format(@"( SELECT urun.[Malzeme Grup Kodu] tur, ISNULL(urun.[Marka Kodu],'?') marka, urun.Model model, urun.[Malzeme Adı] urun, urun.[Malzeme Kodu] kod,
 ROUND(alim.[Satır Net Tutarı KDV Dahil]/alim.[Satır Miktarı],0) AS fiyat, alim.[Satır Miktarı] adet, CONVERT(DATE,alim.[Fatura Tarihi],23) tarih, alim.[Cari Kodu] cari, alim.[Cari Ünvanı] unvan, ISNULL(temsilci.[Satış Temsilcisi],'?') kisi 
FROM ARY_017_01_AYRINTILI_FATURA_RAPORU urun LEFT JOIN ARY_017_01_AYRINTILI_FATURA_RAPORU alim ON alim.[Malzeme Kodu]=urun.[Malzeme Kodu] LEFT JOIN ARY_017_CARI_MUSTER_TEMSILCI temsilci ON alim.[Cari Kodu]=temsilci.[Cari Kodu] 
WHERE urun.[Fatura Türü]='Satınalma Faturası'  AND alim.[Fatura Türü]='Satınalma Faturası' GROUP BY urun.[Malzeme Grup Kodu], urun.[Marka Kodu], urun.Model, urun.[Malzeme Kodu], urun.[Malzeme Adı], alim.[Satır Net Tutarı KDV Dahil], alim.[Satır Miktarı], alim.[Fatura Tarihi], alim.[Cari Kodu], alim.[Cari Ünvanı], temsilci.[Satış Temsilcisi] ) t", query);
                break;  
            case "bayiPerformansQuery":
                result = string.Format(@"( SELECT fat.[Satış Temsilcisi] SORUMLU, cari.[Cari Kodu] KODU, cari.[Cari Ünvanı] UNVAN, ISNULL(SUM([Satır Miktarı]),0) AS ADET, COUNT(DISTINCT fat.[Fatura No]) AS SIPARIS, ROUND(ISNULL(SUM(fat.[Satır Net Tutarı KDV Dahil]),0), 0) AS CIRO, COUNT(DISTINCT fat.[Model]) AS CESIT, ISNULL(FORMAT((SELECT MIN([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '2020-06-08' AND '2020-07-09'),'yyyy-MM-dd'),'') ILK_SIPARIS,
 ISNULL(FORMAT((SELECT MAX([Fatura Tarihi]) FROM ARY_XXX_AYRINTILI_SATIS_RAPORU WHERE [Cari Kodu]=cari.[Cari Kodu] and [Fatura Tarihi] BETWEEN '2020-06-08' AND '2020-07-09'),'yyyy-MM-dd'),'') SON_SIPARIS FROM ARY_017_CARI_MUSTER_TEMSILCI cari LEFT JOIN ARY_XXX_AYRINTILI_SATIS_RAPORU fat ON fat.[Cari Kodu]=cari.[Cari Kodu] AND 
 fat.[Fatura Türü]='Perakende Satış Faturası' AND fat.[Satır Türü]='Malzeme' AND fat.[Fatura Tarihi] BETWEEN '2020-06-08' AND '2020-07-09' WHERE cari.[Cari Kodu]<>''  GROUP BY fat.[Satış Temsilcisi], cari.[Cari Kodu], cari.[Cari Ünvanı] ORDER BY cari.[Cari Ünvanı] ) t", query);
                break;
            case "bigdataQuery":
                result = string.Format(@"( SELECT *  FROM BigData  ) t", query);
                break;   
            case "gtstemsilciQuery":
                result = string.Format(@"(  SELECT tem.[Satış Temsilcisi] Temsilcisi ,cl.DEFINITION_ Adi,cl.TELNRS1 Telefon , cl.LOGICALREF ,m.Isactive,m.IsApprove,m.Approvetext ,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF) as tislem,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=0) as red,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=1) as beklemekte,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=2) as onayli
                     from LG_017_CLCARD cl  JOIN AOMusteri m ON cl.LOGICALREF=m.LOGICALREF 
					 JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON cl.LOGICALREF=tem.LOGICALREF  ) t", query);
                break;  
            case "gtsyonetimQuery":
                result = string.Format(@"(SELECT  (SELECT COUNT(1) FROM AOMusteri m JOIN 
ARY_017_CARI_MUSTER_TEMSILCI mt on m.LOGICALREF=mt.LOGICALREF WHERE mt.[Satış Temsilcisi]=temp.Temsilcisi AND m.IsApprove=1) as toplamonyli,
(SELECT COUNT(1) FROM AOMusteri m JOIN 
ARY_017_CARI_MUSTER_TEMSILCI mt on m.LOGICALREF=mt.LOGICALREF WHERE mt.[Satış Temsilcisi]=temp.Temsilcisi AND m.IsApprove=0)  as toplamret,
temp.Temsilcisi ,COUNT(temp.onayli) toplambayi,SUM(temp.tislem) topalmislem,SUM(temp.red)as red,SUM(temp.onayli) onayli,SUM(temp.beklemekte) beklemekte FROM 
                   (SELECT tem.[Satış Temsilcisi] Temsilcisi,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF) as tislem,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=0) as red,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=1) as beklemekte,
                     (SELECT COUNT(1) FROM AOSiparis s WHERE s.Cariid=cl.LOGICALREF AND s.Status=2) as onayli,m.LOGICALREF
                     from LG_017_CLCARD cl  JOIN AOMusteri m ON cl.LOGICALREF=m.LOGICALREF 
					 JOIN ARY_017_CARI_MUSTER_TEMSILCI tem ON cl.LOGICALREF=tem.LOGICALREF ) as temp GROUP BY temp.Temsilcisi  ) t", query);
                break;  
            case "gtssiparisQuery":
                result = string.Format(@"(  SELECT m.Adi,s.Siparisdate,d.Fiyat,cmt.[Satış Temsilcisi] as Temsilci,s.Status  FROM AOSiparis s JOIN 
                             AOMusteri m ON s.Cariid=m.LOGICALREF
                                   JOIN AOSiparisDetay d ON d.Siparisid=s.id
								   JOIN ARY_017_CARI_MUSTER_TEMSILCI cmt ON cmt.LOGICALREF=s.Cariid ) t", query);
                break;


        }
        return result;
    }

    public static string GetFilter(string query, IEnumerable<string> additionalvalues)
    {
        string result = "";
        switch (query)
        {
           
             case "raporQuery":
                    result = string.Format("t.YEAR_={1} AND t.KEBIRCODE={0} ", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1));
                break;   
            case "rapordetayQuery":
                    result = string.Format("t.YEAR_={1} AND t.KEBIRCODE={0} AND t.MONTH_={2}", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1), additionalvalues.ElementAt(2));
                break;  
            case "carihesapekstresiQuery":

                
                  result = result + string.Format("  t.tarih  BETWEEN '{0}' AND  '{1}'  ", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1));
                

                if (!additionalvalues.ElementAt(2).isNull())
                {
                    result = result + string.Format(" AND t.carikodu = '{0}'  ", additionalvalues.ElementAt(2));
                }  
                if (!additionalvalues.ElementAt(3).isNull())
                {
                    result = result + string.Format(" AND t.temsilci ='{0}'  ", additionalvalues.ElementAt(3));
                }


                break;  
            case "babsformQuery":
                if (additionalvalues.ElementAt(1) != "Tüm Tipler")
                {
                    result = string.Format(" t.formtip='{0}' AND  ", additionalvalues.ElementAt(1));
                }
                if (additionalvalues.ElementAt(2) != "Tüm Yıllar")
                {
                    result = result + string.Format(" t.yil='{0}' AND ", additionalvalues.ElementAt(2));
                }  
                if (additionalvalues.ElementAt(3) != "Tüm Aylar")
                {
                    result = result + string.Format(" t.ay LIKE '%{0}%' AND ", additionalvalues.ElementAt(3)); 
                }
                result = result+ string.Format(" t.carikodu='{0}' ", additionalvalues.ElementAt(0));
                break;  
               case "satisrfQuery":
                    result = result+ string.Format(" t.carikodu='{0}' ", additionalvalues.ElementAt(0));
                break;
            case "stokmaliyetQuery":
                if (!additionalvalues.ElementAt(0).isNull())
                {
                  
                    result = result + string.Format(" t.tip='{0}' ", additionalvalues.ElementAt(0));
                }

                break; 

                 case "siparislistesiQuery":

                if (!additionalvalues.ElementAt(0).isNull() && !additionalvalues.ElementAt(0).isNull())
                {
                    result = result + string.Format(" t.tarih BETWEEN '{0}' AND '{1}' ", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1));
                }
                if (!additionalvalues.ElementAt(2).isNull())
                {
                    if (!result.isNull())
                        result = result + " AND ";

                        result = result + string.Format(" t.tip='{0}' ", additionalvalues.ElementAt(2));
                }
                if(!additionalvalues.ElementAt(3).isNull())
                {
                    if (!result.isNull())
                        result = result + " AND ";

                    result = result + string.Format(" t.temsilci='{0}' ", additionalvalues.ElementAt(3));
                }  
                if(!additionalvalues.ElementAt(4).isNull() && !additionalvalues.ElementAt(5).isNull())
                {
                    if (!result.isNull())
                        result = result + " AND ";

                    result = result + string.Format(" t.adet>={0} AND  t.adet<={1}  ", additionalvalues.ElementAt(4), additionalvalues.ElementAt(5));
                }
                if(!additionalvalues.ElementAt(6).isNull() && !additionalvalues.ElementAt(7).isNull())
                {
                    if (!result.isNull())
                        result = result + " AND ";

                    result = result + string.Format(" t.tutar>={0} AND  t.tutar<={1}  ", additionalvalues.ElementAt(6), additionalvalues.ElementAt(7));
                }
                if(!additionalvalues.ElementAt(8).isNull() && !additionalvalues.ElementAt(9).isNull())
                {
                    if (!result.isNull())
                        result = result + " AND ";

                    result = result + string.Format(" t.kar>={0} AND  t.kar<={1}  ", additionalvalues.ElementAt(8), additionalvalues.ElementAt(9));
                }
                if(!additionalvalues.ElementAt(10).isNull() && !additionalvalues.ElementAt(11).isNull())
                {
                    if (!result.isNull())
                        result = result + " AND ";

                    result = result + string.Format(" t.vadesi>={0} AND  t.vadesi<={1}  ", additionalvalues.ElementAt(10), additionalvalues.ElementAt(11));
                }
                   
                break; 
               case "bilancoQuery":
                result = string.Format(" t.KEBIRCODE='{0}' AND  t.YEAR_='{1}' AND  t.MONTH_ IN({2})  ", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1), additionalvalues.ElementAt(2));
                break; 
            case "bayiPerformansQuery":
                //result = string.Format(" t.KEBIRCODE='{0}' AND  t.YEAR_='{1}' AND  t.MONTH_ IN({2})  ", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1), additionalvalues.ElementAt(2));
                break;  
            case "taskQuery":
                result = string.Format(" t.appuserid={0} AND  t.taskstatus={1} ", additionalvalues.ElementAt(0), additionalvalues.ElementAt(1));
                break; 
            case "commentsQuery":
                result = string.Format(" t.taskid={0} ", additionalvalues.ElementAt(0));
                break;
            case "siparisdetayQuery":
                result = string.Format(" t.faturano='{0}' ", additionalvalues.ElementAt(0));
                break; 
            case "stokmaliyetdetayQuery":
                result = string.Format(" t.kod='{0}' ", additionalvalues.ElementAt(0));
                break; 
            case "gtstemsilciQuery":
                if (!additionalvalues.ElementAt(0).isNull())
                {
                    result = result + string.Format(" t.Temsilcisi='{0}' ", additionalvalues.ElementAt(0));
                }
              break; 
            case "gtssiparisQuery":
                if (!additionalvalues.ElementAt(0).isNull())
                {
                    result = result + string.Format(" t.Temsilci='{0}' AND ", additionalvalues.ElementAt(0));
                }
                result = result + string.Format(" t.Status='{0}' ", additionalvalues.ElementAt(1));

                break;
        }
        return result;

    }

  
}

